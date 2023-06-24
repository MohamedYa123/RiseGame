using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise
{
    public enum zonetype { water, land, high, rock, beach }
    public class mapzone:item
    {
        public zonetype zonetype = zonetype.land;
        public bool isitsolid;
        public string refusemessage = "";
        public mapzone(string name,string image,int width,int height,GameEngine engine)
        {
            this.name = name;
            this.image = image;
            this.width = width;
            this.height = height;
            this.engine = engine;
        }
        public override void load_resouces(game gm, float fw, float fh)
        {
            type = type.building;
            var it = new building();
            it.width = width + 5;
            it.height = height + 5;
            it.engine = engine;
            resourceid= gm.map.load_resource("assets/mapzones/" + image, "", name, it, 1, 1);
            type = type.air;
        }
        public override Bitmap load(game gm, bool basic = false)
        {
            if(basic)
            {
                return gm.map.resources[resourceid].minibitmap;
            }
             prepareresourcebitmap(gm, false, 0,true);
            return resourcebitmap;
        }
    }
}
