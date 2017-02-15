using System;
using System.Collections.Generic;
using System.IO;
using GTANetworkServer;
using GTANetworkShared;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace IcaroRPG
{

    public class Items : Script
    {

        public Items()
        {
            API.onResourceStart += onStart;
            API.onClientEventTrigger += clientEvent;
            API.onEntityEnterColShape += colShapeEvent;
        }

        public const int ITEM_ID_SNIPERRIFLE = 1;
        public const int ITEM_ID_FIREEXTINGUISHER = 2;
        public const int ITEM_ID_VINTAGEPISTOL = 3;
        public const int ITEM_ID_COMBATPDW = 4;
        public const int ITEM_ID_HEAVYSNIPER = 5;
        public const int ITEM_ID_MICROSMG = 6;
        public const int ITEM_ID_PISTOL = 7;
        public const int ITEM_ID_PUMPSHOTGUN = 8;
        public const int ITEM_ID_MOLOTOV = 9;
        public const int ITEM_ID_SMG = 10;
        public const int ITEM_ID_PETROLCAN = 11;
        public const int ITEM_ID_STUNGUN = 12;
        public const int ITEM_ID_DOUBLEBARRELSHOTGUN = 13;
        public const int ITEM_ID_GOLFCLUB = 15;
        public const int ITEM_ID_HAMMER = 16;
        public const int ITEM_ID_COMBATPISTOL = 17;
        public const int ITEM_ID_GUSENBERG = 18;
        public const int ITEM_ID_NIGHTSTICK = 19;
        public const int ITEM_ID_SAWNOFFSHOTGUN = 20;
        public const int ITEM_ID_CARBINERIFLE = 21;
        public const int ITEM_ID_CROWBAR = 22;
        public const int ITEM_ID_FLASHLIGHT = 23;
        public const int ITEM_ID_DAGGER = 24;
        public const int ITEM_ID_BAT = 25;
        public const int ITEM_ID_KNIFE = 26;
        public const int ITEM_ID_BZGAS = 27;
        public const int ITEM_ID_MUSKET = 28;
        public const int ITEM_ID_SNSPISTOL = 29;
        public const int ITEM_ID_ASSUALTRIFLE = 30;
        public const int ITEM_ID_REVOLVER = 31;
        public const int ITEM_ID_HEAVYPISTOL = 32;
        public const int ITEM_ID_KNUCKLEDUSTER = 33;
        public const int ITEM_ID_MARKSMANPISTOL = 34;
        public const int ITEM_ID_MACHETE = 35;
        public const int ITEM_ID_SWITCHBLADE = 36;
        public const int ITEM_ID_HATCHET = 37;
        public const int ITEM_ID_BOTTLE = 38;
        public const int ITEM_ID_SMOKEGRENADE = 39;
        public const int ITEM_ID_PARACHUTE = 40;
        public const int ITEM_ID_MORPHINE = 41;
        public const int ITEM_ID_HEROIN = 42;
        public const int ITEM_ID_SMALLBANDAGE = 43;
        public const int ITEM_ID_LARGEBANDAGE = 44;
        public const int ITEM_ID_COPBADGE = 45;
        public const int ITEM_ID_WEED28 = 46;
        public const int ITEM_ID_WEED = 47;
        public const int ITEM_ID_PLICENSE = 48;
        public const int ITEM_ID_FLICENSE = 49;
        public const int ITEM_ID_AMMOPISTOL = 49;
        public const int ITEM_ID_AMMOSMG = 50;
        public const int ITEM_ID_AMMOASSAULT = 51;
        public const int ITEM_ID_AMMOSNIPER = 52;
        public const int ITEM_ID_AMMOSHOTGUN = 53;
        public const int ITEM_ID_SPIKESTRIP = 54;
        public const int ITEM_ID_SPARETIRE = 55;
        public const int ITEM_ID_SPRUNKPACK = 56;
        public const int ITEM_ID_COLAPACK = 57;
        public const int ITEM_ID_WATERPACK = 58;
        public const int ITEM_ID_SPRUNKKEY = 59;
        public const int ITEM_ID_COPKEY = 60;
        public const int ITEM_ID_MILJETKEY = 61;
        public const int ITEM_ID_MILHELIKEY = 62;
        public const int ITEM_ID_SPRUNK = 63;
        public const int ITEM_ID_ECOLA = 64;
        public const int ITEM_ID_EWATER = 65;
        public const int ITEM_ID_REPAIRKIT = 66;
        public const int ITEM_ID_COMP_SNIPERBARREL = 67;
        public const int ITEM_ID_COMP_SNIPERSTOCK = 68;
        public const int ITEM_ID_COMP_SNIPERRECIEVER = 69;
        public const int ITEM_ID_COMP_MICROSMGBARREL = 70;
        public const int ITEM_ID_COMP_MICROSMGSTOCK = 71;
        public const int ITEM_ID_COMP_MICROSMGRECIEVER = 72;
        public const int ITEM_ID_COMP_SMGBARREL = 73;
        public const int ITEM_ID_COMP_SMGSTOCK = 74;
        public const int ITEM_ID_COMP_SMGRECIEVER = 75;
        public const int ITEM_ID_COMP_DOUBLEBARRELSHOTGUNBARREL = 76;
        public const int ITEM_ID_COMP_DOUBLEBARRELSHOTGUNSTOCK = 77;
        public const int ITEM_ID_COMP_DOUBLEBARRELSHOTGUNRECIEVER = 78;
        public const int ITEM_ID_COMP_AKBARREL = 79;
        public const int ITEM_ID_COMP_AKSTOCK = 80;
        public const int ITEM_ID_COMP_AKRECIEVER = 81;
        public const int ITEM_ID_COMP_SAWNOFFBARREL = 82;
        public const int ITEM_ID_COMP_SAWNOFFSTOCK = 83;
        public const int ITEM_ID_COMP_SAWNOFFRECIEVER = 84;
        public const int ITEM_ID_COMP_HEAVYPISTOLBARREL = 82;
        public const int ITEM_ID_COMP_HEAVYPISTOLSTOCK = 83;
        public const int ITEM_ID_COMP_HEAVYPISTOLRECIEVER = 84;
        public const int ITEM_ID_COMP_50CALBARREL = 82;
        public const int ITEM_ID_COMP_50CALSTOCK = 83;
        public const int ITEM_ID_COMP_50CALRECIEVER = 84;


        public static List<Item> items = new List<Item>();
        public const string INVENTORY_FOLDER = "cnc_inventories";

        public void PopulateItems()
        {
            items.Add(new Weapon(ITEM_ID_SNIPERRIFLE, "Sniper Rifle", "A long range rifle equiped with a high magnification scope.", "SniperHash"));
            items.Add(new Weapon(ITEM_ID_FIREEXTINGUISHER, "Fire Extinguisher", "For putting out fires.", "FireExtinguisher"));
            items.Add(new Weapon(ITEM_ID_COMBATPDW, "Combat PDW", "A tactical Sub-machine gun. A favourite of law enforcement.", "CombatPDW"));
            items.Add(new Weapon(ITEM_ID_HEAVYSNIPER, ".50 Sniper Rifle", "A High calibre sniper rifle for armor penetration at long range", "HeavySniper"));
            items.Add(new Weapon(ITEM_ID_MICROSMG, "Micro SMG", "A compact Sub-machine gun.", "MicroSMG"));
            items.Add(new Weapon(ITEM_ID_PISTOL, "Pistol", "A generic handgun.", "Pistol"));
            items.Add(new Weapon(ITEM_ID_PUMPSHOTGUN, "Pump Shotgun", "A pump action shotgun. A favourite among sport shooters.", "PumpShotgun"));
            items.Add(new Weapon(ITEM_ID_MOLOTOV, "Molotov Cocktail", "The hand grenade of revolutionaries.", "Molotov"));
            items.Add(new Weapon(ITEM_ID_SMG, "MP-5 SMG", "An SMG", "SMG"));
            items.Add(new Weapon(ITEM_ID_PETROLCAN, "Petrol can", "Warning: Flamable", "PetrolCan"));
            items.Add(new Weapon(ITEM_ID_STUNGUN, "Taser", "Standard Issue law enforcement taser", "Stungun"));
            items.Add(new Weapon(ITEM_ID_DOUBLEBARRELSHOTGUN, "Double Barrel shotgun", "An illeagally modified shotgun", "DoubleBarrelShotgun"));
            items.Add(new Weapon(ITEM_ID_GOLFCLUB, "Golf club", "For hitting golf balls... or heads.", "GolfClub"));
            items.Add(new Weapon(ITEM_ID_HAMMER, "Hammer", "A useful workman's tool", "Hammer"));
            items.Add(new Weapon(ITEM_ID_COMBATPISTOL, "Combat Pistol", "A combat pistol. For killing.", "CombatPistol"));
            items.Add(new Weapon(ITEM_ID_GUSENBERG, "Gusenberg", "A Gusenberg Sweeper. A favourite of the Italian mob", "Gusenberg"));
            items.Add(new Weapon(ITEM_ID_NIGHTSTICK, "Nightstick", "Standard issue law enforcement nightstick", "Nightstick"));
            items.Add(new Weapon(ITEM_ID_SAWNOFFSHOTGUN, "Sawnoff Shotgun", "An illeagally modified shotgun", "SawnoffShotgun"));
            items.Add(new Weapon(ITEM_ID_CARBINERIFLE, "M4-A1 Rifle", "A high powered rifle, a favourite of American military and law enforcement.", "CarbineRifle"));
            items.Add(new Weapon(ITEM_ID_CROWBAR, "Crowbar", "For smashing headcrabs", "Crowbar"));
            items.Add(new Weapon(ITEM_ID_FLASHLIGHT, "Flashlight", "Can be used as a weapon", "Flashlight"));
            items.Add(new Weapon(ITEM_ID_DAGGER, "Dagger", "An ancient dagger", "Dagger"));
            items.Add(new Weapon(ITEM_ID_BAT, "Baseball bat", "For hitting baseballs", "Bat"));
            items.Add(new Weapon(ITEM_ID_KNIFE, "Knife", "A knife", "Knife"));
            items.Add(new Weapon(ITEM_ID_BZGAS, "BZGAS", "Law enforcement BZGAS", "BZGas"));
            items.Add(new Weapon(ITEM_ID_MUSKET, "Musket", "A musket from simpler times", "Musket"));
            items.Add(new Weapon(ITEM_ID_SNSPISTOL, "SNS Pistol", "An SNS Pistol. easy to conceal.", "SNSPistol"));
            items.Add(new Weapon(ITEM_ID_ASSUALTRIFLE, "AK-47", "The classic AK-47 used by freedom fighters for more than 60 years", "AssaultRifle"));
            items.Add(new Weapon(ITEM_ID_REVOLVER, "Revolver", "A revolver", "Revolver"));
            items.Add(new Weapon(ITEM_ID_HEAVYPISTOL, "Heavy Pistol", "Packs a slightly heavier punch than your standard 9mm", "HeavyPistol"));
            items.Add(new Weapon(ITEM_ID_KNUCKLEDUSTER, "Knuckle Duster", "For hitting people. Easy to conceal", "KnuckleDuster"));
            items.Add(new Weapon(ITEM_ID_MARKSMANPISTOL, "Marksman Pistol", "An extremely accurate pistol", "MarksmanPistol"));
            items.Add(new Weapon(ITEM_ID_MACHETE, "Machete", "A machete. Useful in dense vegitation", "Machete"));
            items.Add(new Weapon(ITEM_ID_SWITCHBLADE, "Switchblade", "Easy to conceal weapon", "SwitchBlade"));
            items.Add(new Weapon(ITEM_ID_HATCHET, "Hatchet", "EMS standard issue hatchet", "Hatchet"));
            items.Add(new Weapon(ITEM_ID_BOTTLE, "Bottle", "Break to use as a weapon", "Bottle"));
            items.Add(new Weapon(ITEM_ID_PARACHUTE, "Parachute", "A parachute", "Parachute"));
            items.Add(new Narcotic(ITEM_ID_MORPHINE, "Morphine", "Provides temporary pain relief. Addictive.", 20, 2));
            items.Add(new Narcotic(ITEM_ID_HEROIN, "Heroin", "Provides temporary pain relief. Extremely addictive.", 70, 5));
            items.Add(new Narcotic(ITEM_ID_SMALLBANDAGE, "Small bandage", "Heals the character a small amount", 30, 0));
            items.Add(new Narcotic(ITEM_ID_LARGEBANDAGE, "Large bandage", "Heals the character a large amount", 50, 0));
            items.Add(new CopBadge(ITEM_ID_COPBADGE, "Police badge", "Idetifies the wearer as a police officer"));
            items.Add(new Narcotic(ITEM_ID_WEED, "Joint", "One Cannabis roll up", 1, 1));
            items.Add(new Multipack(ITEM_ID_WEED28, "1oz Cannabis", "28 Grams of Cannabis", 56, ITEM_ID_WEED));
            items.Add(new Multipack(ITEM_ID_SPRUNKPACK, "Sprunk x 72", "72 Cans of sprunky goodness", 72, ITEM_ID_SPRUNKPACK));
            items.Add(new Multipack(ITEM_ID_COLAPACK, "e-Cola x 72", "72 Cans of America's favorite elixir", 72, ITEM_ID_COLAPACK));
            items.Add(new Multipack(ITEM_ID_WATERPACK, "e-Water x 72", "72 Cans of special water", 72, ITEM_ID_WATERPACK));
            items.Add(new Item(ITEM_ID_PLICENSE, "Provisional Driver's License", "State of San Andreas provisional drivers license"));
            items.Add(new Ammo(ITEM_ID_AMMOPISTOL, "9mm Pistol Ammo x 30", "30 Rounds of 9mm ammo", 0x1B06D571, 30));
            items.Add(new Ammo(ITEM_ID_AMMOSHOTGUN, "Shotgun Cartridges x 15", "15 Shotgun cartridges", 0x1D073A89, 15));
            items.Add(new Ammo(ITEM_ID_AMMOSNIPER, "Sniper Rifle Ammo x 10", "10 Sniper rifle rounds", 0x05FC3C11, 10));
            items.Add(new Ammo(ITEM_ID_AMMOSMG, "Sub-Machine Gun Ammo x 30", "30 SMG rounds", 0x13532244, 30));
            items.Add(new Ammo(ITEM_ID_AMMOASSAULT, "Assault Rifle Ammo x 30", "30 Assault rifle rounds", 0x83BF0278, 30));
            items.Add(new Tire(ITEM_ID_SPARETIRE, "Spare Tire", "Repairs a vehicle's popped tires"));
            items.Add(new VehicleTypeKey(ITEM_ID_SPRUNKKEY, "Soda truck key", "Unlocks a nearby soda truck", -1050465301));
            items.Add(new VehicleTypeKey(ITEM_ID_COPKEY, "Police car key", "Unlocks a nearby police car", 2046537925));
            items.Add(new VehicleTypeKey(ITEM_ID_MILHELIKEY, "Helicopter key", "Military issue heli key", 837858166));
            items.Add(new Drink(ITEM_ID_SPRUNK, "Can of Sprunk", "Maybe radioactive", -5));
            items.Add(new Drink(ITEM_ID_ECOLA, "Can of E-Cola", "Warning: hazardous to health", -5));
            items.Add(new Drink(ITEM_ID_EWATER, "Bottle of Water", "Healthy water enhanced with e-vitamins", +1));
            API.consoleOutput("Items Populated");
        }

        public void PopulateShops()
        {
            new LicenseShop("Licenses", -539.348877, -209.099701, 37.758963, 368603149, 171, new List<KeyValuePair<int, int>>() { new KeyValuePair<int, int>(ITEM_ID_PLICENSE, 300) }, 0);
            List<KeyValuePair<int, int>> products = new List<KeyValuePair<int, int>>();
            products.Add(new KeyValuePair<int, int>(ITEM_ID_HEAVYPISTOL, 800));
            products.Add(new KeyValuePair<int, int>(ITEM_ID_PISTOL, 600));
            products.Add(new KeyValuePair<int, int>(ITEM_ID_PUMPSHOTGUN, 1200));
            products.Add(new KeyValuePair<int, int>(ITEM_ID_AMMOSHOTGUN, 60));
            products.Add(new KeyValuePair<int, int>(ITEM_ID_AMMOPISTOL, 40));
            new Ammunation("Ammunation", 253.72, -51.22, 69.93, 416176080, 25, products, 0);
            new Ammunation("Ammunation", -661.82, -933.24, 21.83, 416176080, 179, products, 0);
            new Ammunation("Ammunation", 1304.06, -395.23, 36.69, 416176080, 72, products, 0);
            new Ammunation("Ammunation", 842.01, -1035.64, 28.19, 416176080, 0, products, 0);
            new Ammunation("Ammunation", 809.73, -2159.34, 29.62, 416176080, 364, products, 0);
            new Ammunation("Ammunation", 23.73, -1105.47, 29.79, 416176080, 158, products, 0);
            int a = API.exported.doormanager.registerDoor(97297972, new Vector3(243.72, -45.22, 69.93));
            int b = API.exported.doormanager.registerDoor(97297972, new Vector3(835.25, -1036.3, 27.64));
            int c = API.exported.doormanager.registerDoor(97297972, new Vector3(17, -1115.8, 29.79));
            int d = API.exported.doormanager.registerDoor(97297972, new Vector3(-663.64, -945.5, 21.64));
            LoadPaints();
            new Jobgiver("Sprunk Co.", 868.601746, -1641.33887, 30.3404808, 1498487404, 90f, new List<KeyValuePair<int, int>>() { new KeyValuePair<int, int>(ITEM_ID_SPRUNKKEY, 100) }, 0, "index.html");
        }

        public void PopulateDealerships()
        {
            new Dealership("Compact Cars", 0, 187.5617, -1252.634, 29.19846, 416176080, 171, new List<KeyValuePair<int, int>>(), 1, 192.0181, -1256.243, 29.20);
            new Dealership("Coupes", 3, -47.72387, -1096.834, 26.42234, 416176080, 171, new List<KeyValuePair<int, int>>(), 1, 47.73209, -1094.138, 25.9519);
            new Dealership("SUVs", 2, -3026.67, -352, 14.47, 416176080, 171, new List<KeyValuePair<int, int>>(), 1, -3027.44, -354, 14.5);
            new Dealership("Muscle Cars", 4, -1134.78, -1984.87, 13.16, 416176080, 171, new List<KeyValuePair<int, int>>(), 1, -1134.78, -1974.87, 13.16);
            new Dealership("Sports Cars", 6, -803.05, -226, 37.21, 416176080, 171, new List<KeyValuePair<int, int>>(), 1, -802, -232.4, 36.69);
            new Dealership("Supercars", 7, -806.43, -223, 37.21, 416176080, 171, new List<KeyValuePair<int, int>>(), 1, -811, -223.6, 37.13);
            new Dealership("Motorcycles", 8, -69.62, -1829.72, 26.94, 416176080, 171, new List<KeyValuePair<int, int>>(), 1, -64.46, -1832.45, 26.87);
            new Dealership("Vans", 12, 460.4, -1987.7, 22.96, 416176080, 171, new List<KeyValuePair<int, int>>(), 1, 453.4, -1984, 23.2);
            new Dealership("Boats", 14, -855.75, -1350.95, 1.6, 416176080, 171, new List<KeyValuePair<int, int>>(), 1, -866.14, -1355.24, 0);
            new Dealership("Roflcopters", 15, -729.1, -1434.3, 5, 416176080, 171, new List<KeyValuePair<int, int>>(), 1, -724.59, -1443.7, 6);
            new Dealership("Aeroplanes", 16, -1121.1, -2454.1, 13.94, 416176080, 171, new List<KeyValuePair<int, int>>(), 1, -1127.1, -2426.87, 13.94);
            new Dealership("Commercials", 20, 485.43, -1884, 26, 416176080, 171, new List<KeyValuePair<int, int>>(), 1, 484.9, -1887.8, 26);
            new Dealership("Emergency", 18, 455.23, -994.57, 25.8, 416176080, 171, new List<KeyValuePair<int, int>>(), 1, 484.9, -1018, 28.54);
        }

        public void LoadPaints()
        {
            List<KeyValuePair<int, int>> products = new List<KeyValuePair<int, int>>();
            string[] lines = System.IO.File.ReadAllLines("paints.txt");
            foreach (string line in lines)
            {

                string[] words = line.Split();
                var id = Convert.ToInt32(words[1]) + 100;
                items.Add(new Paint(id, words[0] + " paint", "Paint a car."));
                products.Add(new KeyValuePair<int, int>(id, 300));
            }
            new Paintguy("Auto Paints", -518.6279, -257.534, 35.61364, 416176080, 25, products, 0);

        }

        public class Item
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

            public Item(int id, string name, string description)
            {
                ID = id;
                Name = name;
                Description = description;
            }

            public virtual void use(Client c)
            {
            }

        }

        public class RepairKit : Item
        {
            public RepairKit(int id, string name, string description) : base (id,name,description)
                {

                }

            public override void use(Client c)
            {
                if (c.vehicle == null) {
                    API.shared.sendNotificationToPlayer(c, "You must be in a vehicle to repair it");
                    return;
                }
                API.shared.setVehicleHealth(c.vehicle, 2000);
                InventoryHolder ih = c.getData("InventoryHolder");
                ih.RemoveItemFromInventory(this);
                API.shared.sendNotificationToPlayer(c, "~g~ Vehicle Repaired");
            }
        }

        public class Drink : Item
        {
            private int Healthmodifier;
            public Drink(int id, string name, string description, int healthmodifier) : base (id,name,description)
            {

            }

            public override void use(Client c)
            {
                API.shared.setPlayerHealth(c, API.shared.getPlayerHealth(c) - Healthmodifier);
            }
        }

        public class Paint : Item
        {
            public Paint(int id, string name, string description) : base(id, name, description)
            {

            }

            public override void use(Client c)
            {
                if (!API.shared.isPlayerInAnyVehicle(c))
                {
                    API.shared.triggerClientEvent(c, "display_subtitle", "You must be in a vehicle to paint it");
                }
                else
                {
                    API.shared.setVehiclePrimaryColor(c.vehicle, ID - 100);
                    InventoryHolder ih = API.shared.getEntityData(c, "InventoryHolder");
                    ih.RemoveItemFromInventory(this);
                }

            }
        }

        public class Tire : Item
        {
            public Tire(int id, string name, string description) : base (id,name,description)
            {

            }

            public override void use(Client c)
            {
                if(!API.shared.isPlayerInAnyVehicle(c))
                {
                    API.shared.triggerClientEvent(c, "display_subtitle", "You must be in a vehicle to repair its tires");
                }
                else
                {
                    for (var i = 0; i < 7; i++)
                    {
                        API.shared.popVehicleTyre(c.vehicle, i, false);
                    }
                    InventoryHolder ih = API.shared.getEntityData(c, "InventoryHolder");
                    ih.RemoveItemFromInventory(this);
                }

            }
        }

        public class VehicleKey : Item
        {

            public VehicleKey(int id, string name, string description) : base(id, name, description)
            {
            }

            public override void use(Client c)
            {
                foreach (NetHandle V in API.shared.getAllVehicles())
                {
                    if (API.shared.hasEntityData(V, "OwnedVehicleID"))
                    {
                        if (API.shared.getEntityData(V, "OwnedVehicleID") == this.ID)
                        {
                            if (IsInRangeOf(c.position, API.shared.getEntityPosition(V), 20f))
                            {
                              
                                API.shared.sendNotificationToPlayer(c, "Lock Triggered");
                                API.shared.setVehicleLocked(V, !API.shared.getVehicleLocked(V));
                              //  API.shared.playSoundFrontEnd(c, "HUD_MINI_GAME_SOUNDSET", "CONFIRM_BEEP");
                            }
                            else
                            {
                                API.shared.sendNotificationToPlayer(c, "Out of range");
                            }
                        }
                    }
                }
            }

        }


        public class VehicleTypeKey : Item
        {
            private int Hash;
            public VehicleTypeKey(int id, string name, string description, int hash) : base(id, name, description)
            {
                Hash = hash;
            }

            public override void use(Client c)
            {
                foreach (NetHandle V in API.shared.getAllVehicles())
                {
                    Vehicle car = API.shared.getEntityData(V, "Vehicle");
                    if (IsInRangeOf(c.position, API.shared.getEntityPosition(V), 5f) && car.model == Hash)
                    {
                        API.shared.sendNotificationToPlayer(c, "Lock Triggered");
                        API.shared.setVehicleLocked(V, !API.shared.getVehicleLocked(V));
                    //    API.shared.playSoundFrontEnd(c, "HUD_MINI_GAME_SOUNDSET", "CONFIRM_BEEP");
                    }
                }
            }

        }

        public class HouseKey : Item
        {

            public HouseKey(int id, string name, string description) : base(id, name, description)
            {
            }

            public override void use(Client c)
            {
                foreach (Property.Oldskool P in Property.Oldskoollist)
                {
                    if (P.HID == this.ID)
                    {
                        if (IsInRangeOf(c.position, P.Iv, 10f) || IsInRangeOf(c.position, P.Ev, 10f))
                        {
                            API.shared.sendNotificationToPlayer(c, "Lock Triggered");
                            P.Locked = !P.Locked;
                            P.updateTextLabel();
                        }
                        else
                        {
                            API.shared.sendNotificationToPlayer(c, "Out of range");
                        }
                    }
                }
            }

        }

        public class CopBadge : Item
        {
            public CopBadge(int id, string name, string description) : base(id, name, description)
            {

            }
            public override void use(Client c)
            {
                 if (!API.shared.getEntityData(c, "IS_COP"))
                 {
                    API.shared.setEntityData(c, "IS_COP", true);
                    API.shared.sendNotificationToPlayer(c , "ON DUTY: Press F3 to open police menu");
                    InventoryHolder ih = API.shared.getEntityData(c, "InventoryHolder");
                }
                else
                {
                    API.shared.sendNotificationToPlayer(c, "OFF DUTY");
                    API.shared.setEntityData(c, "IS_COP", false);
                }
            }
        }

        public class SpikeStrip : Item
        {
            bool count = false;
            public SpikeStrip(int id, string name, string description) : base(id, name, description)
            {

            }
            public override void use(Client c)
            {
                var spikeshape = API.shared.create3DColShape(new Vector3(c.position.X, c.position.Y - 4, c.position.Z), new Vector3(c.position.X, c.position.Y + 4, c.position.Z));
                spikeshape.setData("spikeshape", true);
                API.shared.sendNotificationToPlayer(c, "Spikestrip placed, expires in 3 minutes");
                API.shared.delay(180000, true, () => { API.shared.deleteColShape(spikeshape); });
                InventoryHolder ih = API.shared.getEntityData(c, "InventoryHolder");
                ih.RemoveItemFromInventory(this);
            }
        }

        public class Narcotic : Item
        {
            public int AmountToHeal { get; set; }
            public int AmountToDamage { get; set; }

            public Narcotic(int id, string name, string description, int amountToHeal, int amountToDamage) : base(id, name, description)
            {
                AmountToHeal = amountToHeal;
                AmountToDamage = amountToDamage;
            }
            public override void use(Client c)
            {
                API.shared.playPlayerScenario(c, "WORLD_HUMAN_SMOKING_POT");
            }

        }

        public class Multipack : Item
        {
            public int Multiple { get; set; }
            public int MId { get; set; }

            public Multipack(int id, string name, string description, int multiple, int mid) : base(id, name, description)
            {
                Multiple = multiple;
                MId = mid;
            }

            public override void use(Client c)
            {
                InventoryHolder ih = API.shared.getEntityData(c, "InventoryHolder");
                for (var i = 0; i < Multiple; i++)
                {
                    ih.AddItemToInventory(ItemByID(MId));
                }
                ih.RemoveItemFromInventory(this);
            }
        }

        public class Weapon : Item
        {
            public string InternalName { get; set; }

            public Weapon(int id, string name, string description, string internalname) : base(id, name, description)
            {
                InternalName = internalname;
            }
            public override void use(Client c)
            {
                API.shared.givePlayerWeapon(c, API.shared.weaponNameToModel(InternalName), 0, true, false);
            }
        }

        public class Ammo : Item
        {
            public int Qty { get; set; }
            public uint WHash { get; set; }

            public Ammo(int id, string name, string description, uint hash, int quantity) : base(id, name, description)
            {
                Qty = quantity;
                WHash = hash;
            }
            public override void use(Client c)
            {
                object[] args = new object[3];
                args[0] = c.handle;
                args[1] = WHash;
                args[2] = Qty;
                API.shared.sendNativeToPlayer(c, Hash.ADD_AMMO_TO_PED, args);
                InventoryHolder ih = API.shared.getEntityData(c, "InventoryHolder");
                ih.RemoveItemFromInventory(this);
            }
        }

        public void onStart()
        {
            PopulateItems();
            PopulateShops();
            PopulateDealerships();
        }

        public void clientEvent(Client sender, string eventName, object[] args)
        {
            
            if (eventName == "menu_handler_select_item")
            {
                var callbackId = (int)args[0];
                var index = (int)args[1];
                if (callbackId == 1)
                {
                    InventoryHolder ih = API.getEntityData(sender, "InventoryHolder");
                    var item = ih.Inventory[index];
                    API.setEntityData(sender, "LastSelectedItem", item);
                    object[] argumentList = new object[15];
                    argumentList[0] = 2;
                    argumentList[1] = "Details";
                    argumentList[2] = item.Details.Name;
                    argumentList[3] = false;
                    argumentList[4] = 5;
                    argumentList[5] = "Use/Equip";
                    argumentList[6] = "Description";
                    argumentList[7] = "Give";
                    argumentList[8] = "Show";
                    argumentList[9] = "Store in vehicle";
                    for (var i = 0; i < 5; i++)
                    {
                        argumentList[10 + i] = "";
                    }
                    API.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                }
                else if (callbackId == 2)
                {
                    var item = API.getEntityData(sender, "LastSelectedItem");
                    if (index == 0)
                    {
                        item.Details.use(sender);
                        API.resetEntityData(sender, "LastSelectedItem");
                    }
                    if (index == 1)
                    {
                        object[] ar = new object[2];
                        ar[0] = item.Details.Description;
                        ar[1] = 2000;
                        API.triggerClientEvent(sender, "display_subtitle", ar);
                    }
                    else if (index == 2)
                    {
                        var peopleNearby = API.getPlayersInRadiusOfPlayer(4, sender);
                        peopleNearby.Remove(sender);
                        API.setEntityData(sender, "NearbyList", peopleNearby);
                        var count = peopleNearby.Count;
                        object[] argumentList = new object[5 + count + count];
                        argumentList[0] = 3;
                        argumentList[1] = "Give";
                        argumentList[2] = "To player in range:";
                        argumentList[3] = false;
                        argumentList[4] = count;
                        var i = 0;
                        foreach (Client c in peopleNearby)
                        {
                            argumentList[5 + i] = c.name;
                            argumentList[5 + count + i] = "";
                            i++;
                        }
                        API.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                    }
                    else if (index == 3)
                    {
                        TextLabel txt = API.createTextLabel(sender.name + " shows his " + item.Details.Name, sender.position, 20, 0.7f);
                        API.setTextLabelColor(txt, 0, 0, 255, 255);
                        API.attachEntityToEntity(txt, sender, "IK_HEAD", new Vector3(0, 0, 0.4), new Vector3(0, 0, 0));
                        API.delay(5000, true, () => { API.deleteEntity(txt); });
                    }
                    else if (index == 4)
                    {
                        if (!API.isPlayerInAnyVehicle(sender))
                        {
                            API.triggerClientEvent(sender, "display_subtitle", "You are not in a vehicle", 2000);
                        }
                        else
                        {
                            InventoryHolder vih = API.getEntityData(sender.vehicle, "InventoryHolder");
                            InventoryHolder ih = API.getEntityData(sender, "InventoryHolder");
                            ih.RemoveItemFromInventory(item.Details);
                            vih.AddItemToInventory(item.Details);
                        }
                    }
                }
                else if (callbackId == 3)
                {
                    var item = API.getEntityData(sender, "LastSelectedItem");
                    var nearbylist = API.getEntityData(sender, "NearbyList");
                    var reciever = nearbylist[index];
                    InventoryHolder senderinventory = API.getEntityData(sender, "InventoryHolder");
                    InventoryHolder recieverinventory = API.getEntityData(reciever, "InventoryHolder");
                    senderinventory.RemoveItemFromInventory(item.Details);
                    recieverinventory.AddItemToInventory(item.Details);
                    API.resetEntityData(sender, "LastSelectedItem");
                    API.resetEntityData(sender, "LastNearbyList");
                    TextLabel txt = API.createTextLabel(sender.name + " gives their " + item.Details.Name + " to " + reciever.name, sender.position, 20, 0.7f);
                    API.setTextLabelColor(txt, 0, 0, 255, 255);
                    API.attachEntityToEntity(txt, sender, "IK_HEAD", new Vector3(0, 0, 0.4), new Vector3(0, 0, 0));
                    API.delay(5000, true, () => { API.deleteEntity(txt); });
                }
                else if (callbackId == 4)
                {
                    ItemSelected(sender, index);
                }
                else if (callbackId == 5)
                {
                    UniqueItemSelected(sender, index);
                }
                else if (callbackId == 6)
                {
                    VehicleSelected(sender, index);
                }
            }
        }



        public class Shop
        {
            public string Name;
            public double X;
            public double Y;
            public double Z;
            public int ModelHash;
            public float PedHeading;
            public List<KeyValuePair<int, int>> Products;
            public int Dimension;
            //   public int RequiredItem;

            public Shop(string name, double x, double y, double z, int modelhash, float pedheading, List<KeyValuePair<int, int>> products, int dimension)
            {
                Name = name;
                X = x; Y = y; Z = z;
                ModelHash = modelhash;
                PedHeading = pedheading;
                Products = products;
                Dimension = dimension;
                Init();

            }

            public virtual void Init()
            {

            }

            public virtual void Show(Client sender)
            {

            }
        }

        public class Jobgiver : Shop
        {
            public string InfoboxURL;
            public Jobgiver(string name, double x, double y, double z, int modelhash, float pedheading, List<KeyValuePair<int, int>> products, int dimension, string infoboxurl) : base(name, x, y, z, modelhash, pedheading, products, dimension)
            {
                InfoboxURL = infoboxurl;
            }

            public override void Show(Client sender)
            {
                var itemsLen = Products.Count;
                API.shared.consoleOutput(itemsLen.ToString());
                object[] argumentList = new object[5 + itemsLen + itemsLen];
                argumentList[0] = 5;
                argumentList[1] = Name;
                argumentList[2] = "So, do you want the job? Deliver soda.";
                argumentList[3] = false;
                argumentList[4] = itemsLen;
                var i = 0;
                foreach (KeyValuePair<int, int> entry in Products)
                {
                    Item item = ItemByID(entry.Key);
                    argumentList[5 + i] = "~g~Yes";
                    argumentList[5 + itemsLen + i] = "Deposit $" + entry.Value;
                    i++;
                }
                API.shared.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                if (sender.isCEFenabled == true)
                {
                    API.shared.triggerClientEvent(sender, "createInfobox", InfoboxURL);
                }
                else
                {
                    API.shared.triggerClientEvent(sender, "display_subtitle", "Hey there! Sprunk Co. is in need of delivery drivers. Think you've got what it takes? If you accept the job, I'll give you a key for the trucks, then get out there and fill up some soda machines. 1)Truck key will be added to your inventory. Can be used within a few meters of a truck. 2) Drive to Sprunk, E-Cola and Rainé machines around the city. <br> 3)Take a pallet of drinks from the truck's Inventory, then walk to the machine to fill it.", 10000);
                }
                API.shared.setEntityData(sender, "ProductsOfUsingShop", Products);
                API.shared.consoleOutput("loaded Jobgiver");
            }

            public override void Init()
            {
                var p = API.shared.createPed((PedHash)ModelHash, new Vector3(X, Y, Z), PedHeading, Dimension);
                var blip = API.shared.createBlip(new Vector3(X, Y, Z));
                blip.shortRange = true;
                blip.color = 38;
                blip.sprite = 85;
                var col = API.shared.createCylinderColShape(new Vector3(X, Y, Z), 3, 5);
                col.setData("Shop", this);
            }
        }

        public class Dealership : Shop
        {
            public double cX;
            public double cY;
            public double cZ;
            public int Vclass;
            public Dealership(string name, int vclass, double x, double y, double z, int modelhash, float pedheading, List<KeyValuePair<int, int>> products, int dimension, double cx, double cy, double cz) : base(name, x, y, z, modelhash, pedheading, products, dimension)
            {
                cX = cx;
                cY = cy;
                cZ = cz;
                Vclass = vclass;
                foreach (VehicleHash vroom in Enum.GetValues(typeof(VehicleHash)))
                {
                    if (API.shared.getVehicleClass(vroom) == Vclass && API.shared.getVehicleMaxAcceleration(vroom) != 0)
                    {
                        Products.Add(new KeyValuePair<int, int>((int)vroom, Vehicles.VehicleSpawner.getVehiclePrice(vroom)));
                    }
                }
                var p = API.shared.createPed((PedHash)ModelHash, new Vector3(X, Y, Z), PedHeading, Dimension);
                var blip = API.shared.createBlip(new Vector3(X,Y,Z));
                blip.shortRange = true;
                API.shared.setBlipSprite(blip, 225);

                switch (Vclass)
                {

                    case 2:
                        API.shared.setBlipSprite(blip, 67);
                        break;
                    case 12:
                        API.shared.setBlipSprite(blip, 318);
                        break;
                    case 8:
                        API.shared.setBlipSprite(blip, 226);
                        break;
                    case 15:
                        API.shared.setBlipSprite(blip, 43);
                        break;
                    case 16:
                        API.shared.setBlipSprite(blip, 251);
                        break;
                    case 20:
                        API.shared.setBlipSprite(blip, 198);
                        break;
                    case 18:
                        API.shared.setBlipTransparency(blip, 255);
                        break;
                    default:

                        break;
				}

                var col = API.shared.createCylinderColShape(new Vector3(X, Y, Z), 3, 5);
                col.setData("Shop", this);
            }

            public override void Init()
            {

            }

            public override void Show(Client sender)
            {
                var itemsLen = Products.Count;
                API.shared.consoleOutput(itemsLen.ToString());
                object[] argumentList = new object[5 + itemsLen + itemsLen];
                argumentList[0] = 6;
                argumentList[1] = Name;
                argumentList[2] = "What can I get you?";
                argumentList[3] = false;
                argumentList[4] = itemsLen;
                var i = 0;
                foreach (KeyValuePair<int, int> entry in Products)
                {
                    // Item item = ItemByID(entry.Key);
                    string name = API.shared.getVehicleDisplayName((VehicleHash)entry.Key);
                    argumentList[5 + i] = name;
                    argumentList[5 + itemsLen + i] = "$" + entry.Value.ToString();
                    i++;
                }
                API.shared.consoleOutput("2");
                API.shared.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                API.shared.setEntityData(sender, "ProductsOfUsingShop", Products);
                API.shared.setEntityData(sender, "cX", cX);
                API.shared.setEntityData(sender, "cY", cY);
                API.shared.setEntityData(sender, "cZ", cZ);
            }
        }

        public class Mechanic : Shop
        {
            public Mechanic(string name, double x, double y , double z, int model, float pedheading, List<KeyValuePair<int, int>> products, int dimension) : base(name, x,y,z,model,pedheading, products, dimension)
            {

            }

            public override void Show(Client sender)
            {
                var itemsLen = Products.Count;
                object[] argumentList = new object[5 + itemsLen + itemsLen];
                argumentList[0] = 5;
                argumentList[1] = Name;
                argumentList[2] = "~r~ Broken Car? ~p~ I can help";
                argumentList[3] = false;
                argumentList[4] = itemsLen;
                var i = 0;
                foreach (KeyValuePair<int, int> entry in Products)
                {
                    Item item = ItemByID(entry.Key);
                    argumentList[5 + i] = item.Name;
                    argumentList[5 + itemsLen + i] = "$" + entry.Value;
                    i++;
                }
                API.shared.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                API.shared.setEntityData(sender, "ProductsOfUsingShop", Products);
            }

            public override void Init()
            {
                var p = API.shared.createPed((PedHash)ModelHash, new Vector3(X, Y, Z), PedHeading, Dimension);
                var blip = API.shared.createBlip(new Vector3(X, Y, Z));
                blip.shortRange = true;
                blip.sprite = 402;
                var col = API.shared.createCylinderColShape(new Vector3(X, Y, Z), 3, 5);
                col.setData("Shop", this);
            }
        }

        public class Paintguy : Shop
        {
            public Paintguy(string name, double x, double y, double z, int modelhash, float pedheading, List<KeyValuePair<int, int>> products, int dimension) : base(name, x, y, z, modelhash, pedheading, products, dimension)
            {

            }

            public override void Show(Client sender)
            {
                var itemsLen = Products.Count;
                object[] argumentList = new object[5 + itemsLen + itemsLen];
                argumentList[0] = 4;
                argumentList[1] = Name;
                argumentList[2] = "~r~ Which ~p~ colour ~ b~?";
                argumentList[3] = false;
                argumentList[4] = itemsLen;
                var i = 0;
                foreach (KeyValuePair<int, int> entry in Products)
                {
                    Item item = ItemByID(entry.Key);
                    argumentList[5 + i] = item.Name;
                    argumentList[5 + itemsLen + i] = "$" + entry.Value;
                    i++;
                }
                API.shared.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                API.shared.setEntityData(sender, "ProductsOfUsingShop", Products);
            }

            public override void Init()
            {
                var p = API.shared.createPed((PedHash)ModelHash, new Vector3(X, Y, Z), PedHeading, Dimension);
                var blip = API.shared.createBlip(new Vector3(X, Y, Z));
                blip.shortRange = true;
                var col = API.shared.createCylinderColShape(new Vector3(X, Y, Z), 3, 5);
                col.setData("Shop", this);
            }
        }

        public class Ammunation : Shop
        {
            public Ammunation(string name, double x, double y, double z, int modelhash, float pedheading, List<KeyValuePair<int, int>> products, int dimension) : base(name, x, y, z, modelhash, pedheading, products, dimension)
            {

            }

            public override void Show(Client sender)
            {
                var itemsLen = Products.Count;
                API.shared.consoleOutput(itemsLen.ToString());
                object[] argumentList = new object[5 + itemsLen + itemsLen];
                argumentList[0] = 4;
                argumentList[1] = Name;
                argumentList[2] = "What can I get you?";
                argumentList[3] = false;
                argumentList[4] = itemsLen;
                var i = 0;
                foreach (KeyValuePair<int, int> entry in Products)
                {
                    Item item = ItemByID(entry.Key);
                    argumentList[5 + i] = item.Name;
                    argumentList[5 + itemsLen + i] = "$" + entry.Value;
                    i++;
                }
                API.shared.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                API.shared.setEntityData(sender, "ProductsOfUsingShop", Products);
            }

            public override void Init()
            {
                var p = API.shared.createPed((PedHash)ModelHash, new Vector3(X, Y, Z), PedHeading, Dimension);
                var blip = API.shared.createBlip(new Vector3(X, Y, Z));
                blip.shortRange = true;
                API.shared.setBlipSprite(blip, 110);
                var col = API.shared.createCylinderColShape(new Vector3(X, Y, Z), 3, 5);
                col.setData("Shop", this);
                int a = API.shared.exported.doormanager.registerDoor(97297972, new Vector3(X, Y, Z));
                int b = API.shared.exported.doormanager.registerDoor(-8873588, new Vector3(X, Y, Z));
            }
        }

        public class LicenseShop : Shop
        {
            public LicenseShop(string name, double x, double y, double z, int modelhash, float pedheading, List<KeyValuePair<int, int>> products, int dimension) : base(name,x,y,z,modelhash,pedheading,products,dimension)
            {
            
            }

            public override void Show(Client sender)
            {
                var itemsLen = Products.Count;
                object[] argumentList = new object[5 + itemsLen + itemsLen];
                argumentList[0] = 5;
                argumentList[1] = Name;
                argumentList[2] = "What can I get you?";
                argumentList[3] = false;
                argumentList[4] = itemsLen;
                var i = 0;
                foreach (KeyValuePair<int, int> entry in Products)
                {
                    Item item = ItemByID(entry.Key);
                    argumentList[5 + i] = item.Name;
                    argumentList[5 + itemsLen + i] = "$" + entry.Value;
                    i++;
                }
                API.shared.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                API.shared.setEntityData(sender, "ProductsOfUsingShop", Products);
            }

            public override void Init()
            {
                var p = API.shared.createPed((PedHash)ModelHash, new Vector3(X, Y, Z), PedHeading, Dimension);
                var blip = API.shared.createBlip(new Vector3(X, Y, Z));
                blip.shortRange = true;
                API.shared.setBlipSprite(blip, 181);
                var col = API.shared.createCylinderColShape(new Vector3(X, Y, Z), 3, 5);
                col.setData("Shop", this);
            }
        }


        public void ItemSelected(Client sender, int index)
        {
            var Products = API.getEntityData(sender, "ProductsOfUsingShop");
            var item = ItemByID(Products[index].Key);
            var price = Products[index].Value;
            API.resetEntityData(sender, "ProductsOfUsingShop");
            if (Money.TakeMoney(sender, price))
            {
                InventoryHolder ih = API.shared.getEntityData(sender, "InventoryHolder");
                ih.AddItemToInventory(item);
                API.shared.triggerClientEvent(sender, "display_subtitle", "Item added to Inventory, press F1 to view", 3000);
            }
            else
            {
                API.shared.triggerClientEvent(sender, "display_subtitle", "Sorry, you don't have enough money", 3000);
            }
        }

        public void VehicleSelected(Client sender, int index)
        {
            var Products = API.getEntityData(sender, "ProductsOfUsingShop");
            var car = (VehicleHash)Products[index].Key;
            var price = Products[index].Value;
            API.shared.resetEntityData(sender, "ProductsOfUsingShop");
            if (Money.TakeMoney(sender, price))
            {
                InventoryHolder ih = API.shared.getEntityData(sender, "InventoryHolder");
                double cx = API.getEntityData(sender, "cX");
                double cy = API.getEntityData(sender, "cY");
                double cz = API.getEntityData(sender, "cZ");
                var idd = Vehicles.VehicleSpawner.OwnedVehicleID;
                var car2 = API.createVehicle(car, new Vector3(cx,cy,cz), new Vector3(0, 0, 20), 111, 111);
                car2.invincible = true;
                API.shared.setEntityData(car2, "OwnedVehicleID", idd);
                API.setEntityData(car2, "RESPAWNABLE", true);
                API.setEntityData(car2, "SPAWN_POS", new Vector3(cx, cy, cz));
                items.Add(new VehicleKey(idd, "Vehicle Key", "Unlocks a vehicle"));
                ih.AddItemToInventory(ItemByID(idd));
                API.shared.triggerClientEvent(sender, "display_subtitle", "Sold! Car spawned and key added to inventory", 3000);
                Vehicles.VehicleSpawner.OwnedVehicleID++;
                Vehicles.VehicleSpawner.saveOwnedVehicles();
                loadVih(car2);
      
            }
            else
            {
                API.shared.triggerClientEvent(sender, "display_subtitle", "Sorry, you don't have enough money", 3000);
            }
        }

        public void UniqueItemSelected(Client sender, int index)
        {
            API.shared.triggerClientEvent(sender, "hideInfobox");
            var Products = API.getEntityData(sender, "ProductsOfUsingShop");
            var item = ItemByID(Products[index].Key);
            var price = Products[index].Value;
            API.resetEntityData(sender, "ProductsOfUsingShop");
            InventoryHolder ih = API.shared.getEntityData(sender, "InventoryHolder");
            if (!ih.Inventory.Exists(ii => ii.Details.ID == item.ID))
            {
                if (Money.TakeMoney(sender, price))
                {
                    ih.AddItemToInventory(item);
                    API.shared.triggerClientEvent(sender, "display_subtitle", "Item added to Inventory, press F1 to view", 3000);
                }
                else
                {
                    API.shared.triggerClientEvent(sender, "display_subtitle", "Sorry, you don't have enough money", 3000);
                }
                return;
            }
            API.shared.triggerClientEvent(sender, "display_subtitle", "You already have one.", 3000);
        }

        public void colShapeEvent(ColShape shape, NetHandle entity)
        {
            var player = API.getPlayerFromHandle(entity);
            if (player == null) return;
            if (shape.hasData("Shop"))
            {
                var ShopObject = shape.getData("Shop");


                    {
                        ShopObject.Show(player);
                    }
                
            }
        }


        public static Item ItemByID(int id)
        {
            foreach (Item item in items)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }
            return null;
        }

        public void load(Client player)
        {
            var path = Path.Combine(INVENTORY_FOLDER, player.socialClubName);
            if (!File.Exists(path))
            {
                InventoryHolder ih = new InventoryHolder();
                ih.Owner = player.handle;
                API.setEntityData(player, "InventoryHolder", ih);
                API.sendNotificationToPlayer(player, "set blank inventory");
            }
            else
            {
                InventoryHolder ih = new InventoryHolder();
                ih.Owner = player.handle;
                ih.InventoryFromXML(path);
                API.setEntityData(player, "InventoryHolder", ih);
            }
            
        }

        public static void saveVih(NetHandle V)
        {
            InventoryHolder ih = API.shared.getEntityData(V, "InventoryHolder");
            if(ih == null)
            {
                API.shared.consoleOutput("ih is null");
                return;
            }
            ih.VehicleInventoryToXML(V);
        }

        public static void loadVih(NetHandle V)
        {
            var path = Path.Combine(INVENTORY_FOLDER, API.shared.getEntityData(V, "OwnedVehicleID").ToString());
            if (!File.Exists(path))
            {
                InventoryHolder ih = new InventoryHolder();
                ih.Owner = V;
                API.shared.setEntityData(V, "InventoryHolder", ih);
            }
            else
            {
                InventoryHolder ih = new InventoryHolder();
                ih.InventoryFromXML(path);
                ih.Owner = V;
                API.shared.setEntityData(V, "InventoryHolder", ih);
            }

        }

        public static void save(Client player)
        {
            InventoryHolder ih = API.shared.getEntityData(player, "InventoryHolder");
            if (ih == null)
            {
                API.shared.consoleOutput("ih is null");
                return;
            }
            ih.InventoryToXML(player);
        }

        public class InventoryItem
        {
            public Item Details { get; set; }
            public int Quantity { get; set; }

            public InventoryItem(Item details, int quantity)
            {
                Details = details;
                Quantity = quantity;
            }
        }

        public class InventoryHolder
        {
            public List<InventoryItem> Inventory { get; set; }
            public NetHandle Owner { get; set; }

            public InventoryHolder()
            {
                Inventory = new List<InventoryItem>();
            }

            public void AddItemToInventory(Item itemToAdd)
            {
                if (itemToAdd != null)
                {
                    var player = API.shared.getPlayerFromHandle(Owner);
                    if (player != null) { API.shared.sendNotificationToPlayer(player, "~g~~h~ ITEM RECIEVED: ~w~" + itemToAdd.Name); }
                    foreach (InventoryItem ii in Inventory)
                    {
                        if (ii.Details.ID == itemToAdd.ID)
                        {
                            ii.Quantity++;
                            return;
                        }
                    }
                    Inventory.Add(new InventoryItem(itemToAdd, 1));
                }
            }

            public void RemoveItemFromInventory(Item itemToDel)
            {
                InventoryItem item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToDel.ID);

                if (item != null)
                {
                    item.Quantity--;
                    if(item.Quantity <= 0)
                    {
                        Inventory.Remove(item);
                    }
                }
            }

            public void InventoryToXML(Client player)
            {
                XDocument inventoryData = new XDocument(new XElement("InventoryItems"));
                var inventoryItems = inventoryData.Element("InventoryItems");
                foreach (InventoryItem i in Inventory)
                {
                    inventoryItems.Add(new XElement("InventoryItem", new XAttribute("ID", i.Details.ID), new XAttribute("Quantity", i.Quantity)));
                }
                var path = Path.Combine(INVENTORY_FOLDER, player.socialClubName);
                inventoryData.Save(path);
            }

            public void VehicleInventoryToXML(NetHandle Vehicle)
            {
                XDocument inventoryData = new XDocument(new XElement("InventoryItems"));
                var inventoryItems = inventoryData.Element("InventoryItems");
                foreach (InventoryItem i in Inventory)
                {
                    inventoryItems.Add(new XElement("InventoryItem", new XAttribute("ID", i.Details.ID), new XAttribute("Quantity", i.Quantity)));
                }
                var path = Path.Combine(INVENTORY_FOLDER, API.shared.getEntityData(Vehicle, "OwnedVehicleID").ToString());
                inventoryData.Save(path);
            }


            public void InventoryFromXML(string path)
            {
                XElement xelement = XElement.Load(path);
                IEnumerable<XElement> InventoryItems = xelement.Elements();
                foreach(var I in InventoryItems)
                {
                    int id = Convert.ToInt32(I.Attribute("ID").Value);
                    if (items.Contains(ItemByID(id)))
                    {
                        int quantity = Convert.ToInt32(I.Attribute("Quantity").Value);
                        for (int i = 0; i < quantity; i++)
                        {
                            AddItemToInventory(ItemByID(id));
                        }
                    }
                    else { API.shared.consoleOutput("attempted to load null iventory item"); }
                }
            }

        }

        public static bool IsInRangeOf(Vector3 playerPos, Vector3 target, float range)
        {
            var direct = new Vector3(target.X - playerPos.X, target.Y - playerPos.Y, target.Z - playerPos.Z);
            var len = direct.X * direct.X + direct.Y * direct.Y + direct.Z * direct.Z;
            return range * range > len;
        }
    }
}
