using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkServer;
using GTANetworkShared;
using System.Xml.Linq;
using System.IO;
namespace IcaroRPG
{
    public class Homes : Script
    {

        public static List<Home> Homeslist = new List<Home>();

        public Homes()
        {
            API.onClientEventTrigger += API_onClientEventTrigger;
            loadHomes();
        }

        private void API_onClientEventTrigger(Client sender, string eventName, params object[] arguments)
        {
            if(eventName == "KeysEnter")
            {
                foreach(Home home in Homeslist)
                {
                    if (sender.getData("HomeID") == home.ID)
                    {
                        if(home.ExteriorColshape.containsEntity(sender.handle))
                        {
                            home.teleportIn(sender, sender.getData("HomeDimension"));
                        }
                        else if(home.InteriorColshape.containsEntity(sender.handle))
                        {
                            home.teleportOut(sender);
                        }
                    }
                }
                    
            }
        }

        public class Home
        {
            public int ID;
            string Name;
            public int Price;
            Vector3 ExteriorPosition;
            Vector3 InteriorPosition;
            public ColShape ExteriorColshape;
            public ColShape InteriorColshape;

            public Home(int id, string name, int price, Vector3 extpos, Vector3 intpos)

            {
                ID = id;
                Price = price;
                ExteriorPosition = extpos;
                InteriorPosition = intpos;
                Name = name;
                API.shared.createTextLabel("Press Enter to go inside your apartment", ExteriorPosition, 25, 1);
                API.shared.createTextLabel("Press Enter to leave your apartment", InteriorPosition, 25, 1);
                ExteriorColshape = API.shared.createCylinderColShape(ExteriorPosition, 3, 2);
                ExteriorColshape.setData("HomeExterior", this);

                InteriorColshape = API.shared.createCylinderColShape(InteriorPosition, 3, 2);
                InteriorColshape.setData("HomeInterior", this);
            }

            public void teleportIn(Client sender, int dimension)
            {
                Global.Util.TeleportWithFade(sender, InteriorPosition);
                API.shared.consoleOutput("telein");
                sender.dimension = dimension;
            }

            public void teleportOut(Client sender)
            {
                Global.Util.TeleportWithFade(sender, ExteriorPosition);
                sender.dimension = 1;
            }

        }

        public void loadHomes()
        {
            if (File.Exists("Homes.xml"))
            {
                API.consoleOutput("Loading homes...");
                XElement xelement = XElement.Load("Homes.xml");
                IEnumerable<XElement> homes = xelement.Elements();
                foreach (var p in homes)
                {
                    var id = Convert.ToInt32(p.Attribute("ID").Value);
                    var eX = Convert.ToDouble(p.Attribute("EX").Value);
                    var eY = Convert.ToDouble(p.Attribute("EY").Value);
                    var eZ = Convert.ToDouble(p.Attribute("EZ").Value);
                    var iX = Convert.ToDouble(p.Attribute("IX").Value);
                    var iY = Convert.ToDouble(p.Attribute("IY").Value);
                    var iZ = Convert.ToDouble(p.Attribute("IZ").Value);
                    var price = Convert.ToInt32(p.Attribute("Price").Value);
                    var name = p.Attribute("Name").Value;
                    var home = new Home(id, name, price, new Vector3(eX, eY, eZ), new Vector3(iX, iY, iZ));
                    API.consoleOutput("Loaded Home ID:" + id);
                    Homeslist.Add(home);
                }
            }
        }

    }
}
