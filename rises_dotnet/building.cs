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
        public int silver;
        public int gold;
        public int iron;
        public int rock;
        public int food;
        public int wood;
        //    public player owner;
        // public string image;
        public string name;
        public building()
        {
            walk = false;
        }
        public override void load_resouces(game gm, float fw,float fh)
        {
            resourceid = gm.map.load_resource($"resources/{army.name}/buildings/{image}", $"{army.name} - " + name, "", this, fw,fh);
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
        public override void read()
        {
            walk = false;
            if (buildtime_ms <= 0)
            {
                available = true;
            }
            if (!available)
            {
                buildtime_ms -= 1;
                return;
            }
        
            try
            {
                adddpiece(piecesallowed[0]);
            }
            catch { }
            if (pieces_tobuild.Count > 0)
            {
                if (pieces_tobuild[0].buildtime_ms == 0)
                {
                    engine.additem(pieces_tobuild[0]);
                    pieces_tobuild.RemoveAt(0);
                }
                else
                {
                    pieces_tobuild[0].buildtime_ms -= 1;
                }
            }
        }
    }
}
