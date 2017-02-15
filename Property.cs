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
    public class Property : Script
    {
        public static int DID = 50000;
        public static List<Oldskool> Oldskoollist = new List<Oldskool>();
        public static List<Newskool> Newskoollist = new List<Newskool>();

        public Property()
        {
            API.onEntityEnterColShape += ColHit;
            API.onResourceStart += OnStart;
            API.onResourceStop += OnStop;
        }

        public void OnStart()
        {
           // loadOldskools();
             loadNewskools();
          //  new Newskool(true, true, 11111, 50000, new Vector3(8.78, 540, 176), 308207762);
            //    loadFirstTime();
            API.delay(5000, false, clearDoors);
            ColShape vdebug = API.createSphereColShape(new Vector3(0, 0, 0), 30);
            vdebug.setData("debugshape", true);
        }


        public void clearDoors()
        {
            foreach (NetHandle user in API.getAllPlayers())
            {
                API.resetEntityData(user, "colhit");
            }
        }

        public void OnStop()
        {
            saveOldSkools();
            saveNewSkools();
        }

        public void ColHit(ColShape shape, NetHandle ent)
        {
            if (shape.hasData("debugshape"))
            {
                if (API.hasEntityData(ent, "RESPAWN_POS"))
                {
                    API.consoleOutput("Car went to Hell");
                    API.setEntityPosition(ent, API.getEntityData(ent, "RESPAWN_POS"));
                }
            }
            if (shape.hasData("Iv"))
            {
                Oldskool house = shape.getData("house");
                if (house.Locked == false)
                {
                    var player = API.getPlayerFromHandle(ent);
                    if (player == null) return;
                    if (!API.hasEntityData(ent, "colhit"))
                    {
                        {
                            API.setEntityData(ent, "eX", API.getEntityPosition(ent).X);
                            API.setEntityData(ent, "eY", API.getEntityPosition(ent).Y);
                            API.setEntityData(ent, "eZ", API.getEntityPosition(ent).Z);
                            API.setEntityPosition(player, shape.getData("Iv"));
                            API.setEntityDimension(player, shape.getData("HID"));
                            API.setEntityData(ent, "Dimension", shape.getData("HID"));
                            API.setEntityData(ent, "colhit", true);
                        }
                    }
                }
            }
            if (shape.hasData("Ev"))
            {
                var player = API.getPlayerFromHandle(ent);
                if (player == null) return;
                if (!API.hasEntityData(ent, "colhit"))
                {
                    Vector3 ExtPos = new Vector3(API.getEntityData(ent, "eX"), API.getEntityData(ent, "eY"), API.getEntityData(ent, "eZ"));
                    API.setEntityPosition(player, ExtPos);
                    API.setEntityData(ent, "colhit", true);
                    API.setEntityDimension(ent, 1);
                    API.setEntityData(ent, "Dimension", 1);
                }
            }
        }

        public abstract class House
        {
            public bool Locked { get; set; }
            public bool Forsale { get; set; }
            public int HID { get; set; }
            public Vector3 Ev { get; set; }
            public int Price;
            private Blip blip;
            public TextLabel exterior { get; set; }
            public House(bool locked, bool forsale, int hid, int price, Vector3 ev)
            {
                Forsale = forsale;
                Locked = locked;
                HID = hid;
                Price = price;
                Ev = ev;
                exterior = API.shared.createTextLabel("Property: Unlocked", Ev, 20, 1);
                API.shared.setEntityData(exterior, "Property", this);
                blip = API.shared.createBlip(Ev);
                blip.shortRange = true;
                API.shared.setBlipSprite(blip, 374);
                updateTextLabel();
            }

            public void updateTextLabel()
            {
                API.shared.setTextLabelText(exterior, "Property, ID:" + HID);
                if (this.Forsale == true)
                {
                    API.shared.setTextLabelText(exterior, "Property FOR SALE: $" + Price.ToString() + "\n Use /buy to get it!");
                }
                if (this.Locked == true)
                {
                    API.shared.setTextLabelText(exterior, "Property: Locked");
                }
            }

            public void removeBlip()
            {
                API.shared.deleteEntity(blip);
            }
        }

        public class Newskool : House
        {
            public int Doormodel { get; set; }
            public int Door { get; set; }
         //   public new bool Locked { get { return Locked; } set { Locked = value; Locker(); } }
            public Newskool(bool locked, bool forsale, int hid, int price, Vector3 ev, int doormodel) : base(locked,forsale,hid,price,ev)
            {
                Doormodel = doormodel;
                Door = API.shared.exported.doormanager.registerDoor(Doormodel, Ev);
             //   API.shared.exported.doormanager.setDoorState(Door, true, 0);
                Locker();
                Newskoollist.Add(this);
            }
            public void Locker()
            {
                if(Locked) { API.shared.exported.doormanager.setDoorState(Door, true, 0); }
                else { API.shared.exported.doormanager.setDoorState(Door, false, 1); }
            }
        }

        public class Oldskool : House
        {
            public Vector3 Iv;

            public Oldskool(bool locked, bool forsale, int hid, int price, Vector3 ev, Vector3 iv) : base(locked, forsale, hid, price, ev)
            {
                Iv = iv;
                ColShape Eshape = API.shared.createSphereColShape(Ev, 2);
                Eshape.setData("Locked", Locked);
                //   Eshape.setData("Property", this);
                Eshape.setData("Iv", Iv);
                Eshape.setData("house", this);
                ColShape Ishape = API.shared.createSphereColShape(Iv, 1);
                Ishape.setData("Ev", true);
                Ishape.setData("HID", HID);
                Ishape.dimension = HID;
                API.shared.createTextLabel("*", this.Iv, 5, 2, false);
                Oldskoollist.Add(this);
            }
        }

        [Command("/buy")]
        public void buy(Client sender)
        {
            foreach(NetHandle label in API.getAllLabels())
            {
                if (Global.Util.IsInRangeOf(sender.position, API.getEntityPosition(label), 5))
                {
                    Oldskool house = API.getEntityData(label, "Property");
                    if (house != null && house.Forsale == true)
                    {
                        if (Money.TakeBankMoney(sender, house.Price))
                        {
                            Items.InventoryHolder ih = API.shared.getEntityData(sender, "InventoryHolder");
                            ih.AddItemToInventory(Items.ItemByID(house.HID));
                            API.sendNotificationToPlayer(sender, "Congratulations new home owner! \n Money transfered from bank acount and Key added to inventory.");
                            house.Forsale = false;
                            house.updateTextLabel();
                        }
                        else
                        {
                            API.sendNotificationToPlayer(sender, "Bank balance too low! Deposit cash at any ATM.");
                        }
                    }
                }
            }
        }

        public void saveOldSkools()
        {
            XDocument Osk = new XDocument(new XElement("Oldskools"));
            var Oldskools= Osk.Element("Oldskools");
            foreach (Oldskool o in Oldskoollist)
            {   
               Oldskools.Add(new XElement("Oldskool", new XAttribute("ID", o.HID), new XAttribute("EX", o.Ev.X), new XAttribute("EY", o.Ev.Y), new XAttribute("EZ", o.Ev.Z), new XAttribute("IX", o.Iv.X), new XAttribute("IY", o.Iv.Y), new XAttribute("IZ", o.Iv.Z), new XAttribute("Price", o.Price), new XAttribute("Forsale", o.Forsale), new XAttribute("Locked", o.Locked)));
            }
            Osk.Save("Oldskools.xml");
        }

        public void saveNewSkools()
        {
            XDocument Nsk = new XDocument(new XElement("Newskools"));
            var Newskools = Nsk.Element("Newskools");
            foreach (Newskool n in Newskoollist)
            {
                Newskools.Add(new XElement("Newskool", new XAttribute("ID", n.HID), new XAttribute("EX", n.Ev.X), new XAttribute("EY", n.Ev.Y), new XAttribute("EZ", n.Ev.Z), new XAttribute("Price", n.Price), new XAttribute("Forsale", n.Forsale), new XAttribute("Locked", n.Locked), new XAttribute("Doormodel", n.Doormodel)));
            }
            Nsk.Save("Newskools.xml");
        }

        public void loadNewskools()
        {
            if (File.Exists("Newskools.xml"))
            {
                API.consoleOutput("Loading oldskool properties...");
                XElement xelement = XElement.Load("Newskools.xml");
                IEnumerable<XElement> newskools = xelement.Elements();
                foreach (var p in newskools)
                {
                    var did = Convert.ToInt32(p.Attribute("ID").Value);
                    var eX = Convert.ToDouble(p.Attribute("EX").Value);
                    var eY = Convert.ToDouble(p.Attribute("EY").Value);
                    var eZ = Convert.ToDouble(p.Attribute("EZ").Value);
                    var price = Convert.ToInt32(p.Attribute("Price").Value);
                    var forsale = Convert.ToBoolean(p.Attribute("Forsale").Value);
                    var locked = Convert.ToBoolean(p.Attribute("Locked").Value);
                    var doormodel = Convert.ToInt32(p.Attribute("Doormodel").Value);
                    new Newskool(locked, forsale, did, price, new Vector3(eX, eY, eZ), doormodel);
                }
            }
        }

        public void loadOldskools()
        {
            if (File.Exists("Oldskools.xml"))
            {
                API.consoleOutput("Loading oldskool properties...");
                XElement xelement = XElement.Load("Oldskools.xml");
                IEnumerable<XElement> oldskools = xelement.Elements();
                foreach (var p in oldskools)
                {
                    var did = Convert.ToInt32(p.Attribute("ID").Value);
                    var eX = Convert.ToDouble(p.Attribute("EX").Value);
                    var eY = Convert.ToDouble(p.Attribute("EY").Value);
                    var eZ = Convert.ToDouble(p.Attribute("EZ").Value);
                    var iX = Convert.ToDouble(p.Attribute("IX").Value);
                    var iY = Convert.ToDouble(p.Attribute("IY").Value);
                    var iZ = Convert.ToDouble(p.Attribute("IZ").Value);
                    var price = Convert.ToInt32(p.Attribute("Price").Value);
                    var forsale = Convert.ToBoolean(p.Attribute("Forsale").Value);
                    var locked = Convert.ToBoolean(p.Attribute("Locked").Value);
                    new Oldskool(locked, forsale, did, price, new Vector3(eX, eY, eZ), new Vector3(iX, iY, iZ));
                    Items.items.Add(new Items.HouseKey(did, "Property Key, ID:" + did, "Locks or unlocks a property."));
                }
            }
        }

        public void loadFirstTime()
        {
            new Oldskool(false, true, 0, 700000, new Vector3(-974.1464f, -1433.113f, 7.679172f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-1610.868f, -428.8814f, 40.46698f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-1535.237f, -325.281f, 47.48159f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-1439.59f, -550.6906f, 34.7418f), new Vector3(-1460.366f, -522.0636f, 56.929f));
            new Oldskool(false, true, 0, 700000, new Vector3(-757.82f, -753.8024f, 26.6554f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-773.282f, 312.275f, 84.698f), new Vector3(-780.152f, 340.443f, 207.621f));
            new Oldskool(false, true, 0, 700000, new Vector3(1665.579f, 4776.712f, 41.93869f), new Vector3(265.3285f, -1002.704f, -99.0085f));
            new Oldskool(false, true, 0, 700000, new Vector3(-1406.652f, 533.3694f, 122.9286f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-688.8965f, 598.6945f, 143.5084f), new Vector3(-680.1067f, 590.6495f, 145.393f));
            new Oldskool(false, true, 0, 700000, new Vector3(-751.1387f, 621.1008f, 142.2527f), new Vector3(-761.0836f, 617.9774f, 144.1539f));
            new Oldskool(false, true, 0, 700000, new Vector3(-853.2899f, 698.7006f, 148.7756f), new Vector3(-859.5645f, 688.7182f, 152.8571f));
            new Oldskool(false, true, 0, 700000, new Vector3(10.9781f, 86.45157f, 78.39816f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-44.99031f, -60.94749f, 63.58578f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-1294.228f, 456.4709f, 97.0794f), new Vector3(-1289.639f, 446.7739f, 97.8989f));
            new Oldskool(false, true, 0, 700000, new Vector3(-512.2141f, 111.8229f, 63.33881f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-558.0556f, 666.2042f, 145.1311f), new Vector3(-572.4428f, 658.958f, 145.8364f));
            new Oldskool(false, true, 0, 700000, new Vector3(349.893f, 442.8174f, 147.3472f), new Vector3(340.6531f, 436.7456f, 149.394f));
            new Oldskool(false, true, 0, 700000, new Vector3(371.9392f, 430.4312f, 145.1107f), new Vector3(373.2864f, 420.6612f, 145.9045f));
            new Oldskool(false, true, 0, 700000, new Vector3(-12.83225f, 6560.163f, 31.97093f), new Vector3(265.3285f, -1002.704f, -99.0085f));
            new Oldskool(false, true, 0, 700000, new Vector3(282.245f, -159.5781f, 63.62236f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-305.5824f, 6330.911f, 32.48935f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-108.5332f, 6531.883f, 29.80916f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-1562.17f, -408.0945f, 42.38398f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-933.4771f, -383.6144f, 38.9613f), new Vector3(-913.1502f, -384.5727f, 85.4804f));
            new Oldskool(false, true, 0, 700000, new Vector3(-205.4685f, 184.4087f, 80.32763f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-633.7015f, 169.4392f, 61.22641f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-814.8087f, -984.2986f, 14.03712f), new Vector3(265.3285f, -1002.704f, -99.0085f));
            new Oldskool(false, true, 0, 700000, new Vector3(-831.6166f, -856.5034f, 19.59702f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(2.387458f, 34.19853f, 71.16882f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(1344.532f, -1581.023f, 54.05513f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-197.6289f, 90.04182f, 69.65993f), new Vector3(346.5235f, -1002.901f, -99.1962f));
            new Oldskool(false, true, 0, 700000, new Vector3(-617.9388f, 35.7848f, 43.5558f), new Vector3(-598.9042f, 41.8059f, 93.6261f));
            new Oldskool(false, true, 0, 700000, new Vector3(-662.6467f, -851.4024f, 24.4296f), new Vector3(265.3285f, -1002.704f, -99.0085f));
            new Oldskool(false, true, 0, 700000, new Vector3(-914.3189f, -455.2902f, 39.5998f), new Vector3(-900.6082f, -431.0182f, 121.607f));
            new Oldskool(false, true, 0, 700000, new Vector3(118.8673f, 567.283f, 183.1295f), new Vector3(117.5057f, 557.3167f, 184.3022f));
            new Oldskool(false, true, 0, 700000, new Vector3(-177.3793f, 503.8313f, 136.8531f), new Vector3(-173.286f, 495.0179f, 137.667f));
            new Oldskool(false, true, 0, 700000, new Vector3(1901.745f, 3783.513f, 32.79797f), new Vector3(265.3285f, -1002.704f, -99.0085f));
            new Oldskool(false, true, 0, 700000, new Vector3(-258.1236f, -969.0657f, 31.2199f), new Vector3(-281.0908f, -943.2817f, 92.5108f));
            new Oldskool(false, true, 0, 700000, new Vector3(-49.3243f, -583.1716f, 37.0333f), new Vector3(-21.0966f, -580.4884f, 90.1148f));
        }
    }
}
