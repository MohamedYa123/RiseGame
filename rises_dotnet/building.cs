using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise
{
    public class building : item
    {
        public building_type building_Type;
        public zonetype allowedzone;
        public List<piece> piecesallowed = new List<piece>();
        public List<piece> pieces_tobuild = new List<piece>();
        public List<building> buildingsallowed = new List<building>();
        public List<building> buildings_tobuild = new List<building>();
        public float producesmoney=0;
        //    public player owner;
        // public string image;
        public building()
        {
            walk = false;
            patience = 130;
        }
        public override void load_resouces(game gm, float fw,float fh)
        {
            resourceid = gm.map.load_resource($"resources/{army.name}/buildings/{image}", $"{army.name} - " + name, "", this, fw,fh);
            load(gm);
        }

        public void adddpiece(piece piece)
        {
            
            if (!available)
            {
                return;
            }
            if (pieces_tobuild.Count < 9)
            {
                piece = (piece)piece.clone();
                piece.buildtime_ms = piece.buildtime;
                var xxg = owner.silver - piece.silver;
                if (xxg < 0)
                {
                    return;
                }
                owner.silver = xxg;
                pieces_tobuild.Add(piece);
            }
        }
        public void removepiece(piece piece)
        {

            if (pieces_tobuild.Count > 0)
            {
                try
                {
                    pieces_tobuild.Remove(piece);
                }
                catch { }
            }
        }
        public int autobuildms = 0;
        int frames = 0;
        public float farmmode=-1;
        public float foodproduction;
        public override void read()
        {
            if ( dead)
            {
                return;
            }
            base.read();
            if (name == "farm"&&available)
            {
                farmmode++;
                if (farmmode > 700)
                {
                    farmmode = 0;
                   
                }
                if (farmmode > 300&& farmmode % 25==0)
                {
                    if (owner != null)
                    {
                        owner.food +=(float)Math.Round( foodproduction/(700-300)*25);
                    }
                }
            }
            walk = false;
            if (buildtime_ms <= 0)
            {
                available = true;
            }
            if (!available)
            {
                int wi = workersinside.Count;
                int cc = 0;
                foreach(var worker in workersinside)
                {
                    if (worker.buildin)
                    {
                        cc++;
                    }
                }
                buildtime_ms -= 1*cc;
                health+=maxhealth/buildtime*cc;
                if (cc < workersrequired)
                {
                    buildtime_ms = Math.Max(1, buildtime_ms);
                    health = Math.Min(maxhealth-1, health);
                }
                if (health > maxhealth)
                {
                    health = maxhealth;
                }
                if (buildtime_ms <0)
                {
                    buildtime_ms = 0;
                }
                return;
            }
            if (owner != null)
            {
                owner.silver += producesmoney;
            }
            try
            {
                if (autobuildms!=0&& frames % autobuildms == 0&&frames!=0)
                {
                    adddpiece(piecesallowed[0]);
                }
            }
            catch { return; }
            if (pieces_tobuild.Count > 0)
            {
                if (pieces_tobuild[0].buildtime_ms <= 0)
                {
                    pieces_tobuild[0].x = x + width / 2;
                    pieces_tobuild[0].y = y + height / 2;
                    var b= engine.additem(pieces_tobuild[0]);
                    if (b)
                    {
                        pieces_tobuild.RemoveAt(0);
                    }
                }
                else
                {
                    pieces_tobuild[0].buildtime_ms -= 1;
                }
            }
            frames++;
        }
    }
}
