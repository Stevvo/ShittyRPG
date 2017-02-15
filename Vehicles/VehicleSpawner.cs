using System;
using GTANetworkServer;
using GTANetworkShared;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using GTANetworkServer.Constant;

namespace IcaroRPG.Vehicles
{
    public class VehicleSpawner : Script
    {
        public static List<string> numberplates = new List<string>();
        public static int OwnedVehicleID = 1001;
        public VehicleSpawner()
        {
            API.onResourceStart += onResStart;
            API.onResourceStop += onResStop;
            API.onPlayerEnterVehicle += onEnterVehicle;
            API.onClientEventTrigger += ScriptEvent;
        }

        public void onResStart()
        {
            loadOwnedVehicles();
            loadVehicles();
            API.delay(300000, false, saveOwnedVehicles);
        }

        public void onEnterVehicle(Client player, NetHandle vehicle)
        {
            if (API.getEntityData(vehicle, "rental") == true)
            {
                API.setVehicleEngineStatus(vehicle, false);
                var p = getVehiclePrice((VehicleHash)API.getEntityModel(vehicle));
                var p1 = Convert.ToInt32((double)p / 200);
                API.sendNotificationToPlayer(player, "Press Enter to rent this vehicle for $" + p1, true);
                API.triggerClientEvent(player, "CarEnter");
            }
            else if(API.getEntityData(vehicle, "sale") == true)
            {
                int p = getVehiclePrice((VehicleHash)API.getEntityModel(vehicle));
            }
        }

        public void ScriptEvent(Client sender, string eventName, object[] args)
        {
            if (eventName == "Enter")
            {
                if (sender.vehicle != null)
                {
                    rentVehicle(sender);
                }
            }

            if (eventName == "VehicleMenuKeyPressed")
            {
                if (sender.vehicle != null)
                {
                    object[] argumentList = new object[15];
                    argumentList[0] = 7;
                    argumentList[1] = sender.vehicle.displayName;
                    argumentList[2] = "Vehicle options";
                    argumentList[3] = false;
                    argumentList[4] = 5;
                    argumentList[5] = "Lock doors";
                    argumentList[6] = "Unlock doors";
                    argumentList[7] = "Pop/shut trunk";
                    argumentList[8] = "Pop/shut hood";
                    argumentList[9] = "Inventory";
                    for (var i = 0; i < 5; i++)
                    {
                        argumentList[10 + i] = "";
                    }
                    API.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                }
            }
            if (eventName == "menu_handler_select_item")
            {
                var callbackId = (int)args[0];
                var index = (int)args[1];
                if (callbackId == 7)
                {
                    if (index == 0)
                    {
                        API.setVehicleLocked(sender.vehicle, true);
                        //   API.shared.playSoundFrontEnd(sender, "CONFIRM_BEEP", "HUD_MINI_GAME_SOUNDSET");
                    }
                    if (index == 1)
                    {
                        API.setVehicleLocked(sender.vehicle, false);
                        //    API.shared.playSoundFrontEnd(sender, "CONFIRM_BEEP", "HUD_MINI_GAME_SOUNDSET");
                    }
                    if (index == 2)
                    {
                        if (sender.vehicle.isDoorOpen(5))
                        {
                            sender.vehicle.closeDoor(5);
                        }
                        else
                        {
                            sender.vehicle.openDoor(5);
                        }
                    }
                    if (index == 3)
                    {
                        if (sender.vehicle.isDoorOpen(4))
                        {
                            sender.vehicle.closeDoor(4);
                        }
                        else
                        {
                            sender.vehicle.openDoor(4);
                        }
                    }
                    if (index == 4)
                    {
                        if (API.hasEntityData(sender.vehicle, "InventoryHolder"))
                        {
                            Items.InventoryHolder ih = API.getEntityData(sender.vehicle, "InventoryHolder");
                            if (ih != null)
                            {
                                var itemsLen = ih.Inventory.Count;
                                object[] argumentList = new object[5 + itemsLen * 2];
                                argumentList[0] = 8;
                                argumentList[1] = sender.vehicle.displayName;
                                argumentList[2] = "Inventory ~b~Select an Item ";
                                argumentList[3] = false;
                                argumentList[4] = itemsLen;
                                var i = 0;
                                foreach (Items.InventoryItem item in ih.Inventory)
                                {
                                    argumentList[5 + i] = item.Details.Name;
                                    argumentList[5 + itemsLen + i] = "Quantity: " + item.Quantity;
                                    i++;
                                }
                                API.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                            }
                        }
                    }
                }
                if (callbackId == 8)
                {
                    Items.InventoryHolder ih = API.getEntityData(sender.vehicle, "InventoryHolder");
                    Items.InventoryHolder pih = API.getEntityData(sender, "InventoryHolder");
                    Items.InventoryItem item = ih.Inventory[index];
                    if (item.Details.ID == 56 || item.Details.ID == 57 || item.Details.ID == 58)
                    {
                        if (pih.Inventory.Exists(ii => ii.Details.ID == item.Details.ID))
                        {
                            API.triggerClientEvent(sender, "show_subtitle", "~r~ You can only carry one of those", 3500);
                        }
                        else
                        {
                            ih.RemoveItemFromInventory(item.Details);
                            pih.AddItemToInventory(item.Details);
                        }
                    }
                    else
                    {
                        ih.RemoveItemFromInventory(item.Details);
                        pih.AddItemToInventory(item.Details);
                    }
                }
            }
        }

        public static int getVehiclePrice(VehicleHash vehicle)
        {
            int price = 0;
            int switchExpression = API.shared.getVehicleClass(vehicle);
            switch (switchExpression)
            {

                case 0:
                    price = 1500;
                    break;
                case 1:
                    price = 3250;
                    break;
                case 2:
                    price = 3500;
                    break;
                case 3:
                    price = 4500;
                    break;
                case 4:
                    price = 6000;
                    break;
                case 5:
                    price = 40000;
                    break;
                case 6:
                    price = 30000;
                    break;
                case 7:
                    price = 35000;
                    break;
                case 8:
                    price = 5000;
                    break;
                case 9:
                    price = 5000;
                    break;
                case 10:
                    price = 10000;
                    break;
                case 11:
                    price = 3000;
                    break;
                case 12:
                    price = 2000;
                    break;
                case 13:
                    price = 5000;
                    break;
                case 14:
                    price = 5000;
                    break;
                case 15:
                    price = 50000;
                    break;
                case 16:
                    price = 50000;
                    break;
                case 17:
                    price = 5000;
                    break;
                case 18:
                    price = 5000;
                    break;
                case 19:
                    price = 50000;
                    break;
                case 20:
                    price = 4500;
                    break;
                case 21:
                    price = 50000;
                    break;
            }
            price = price * (int)API.shared.getVehicleMaxSpeed(vehicle);
            price = Convert.ToInt32((double)price * API.shared.getVehicleMaxAcceleration(vehicle));
            price -= Convert.ToInt32((double)price % 10);
            return price;
        }

        public void rentVehicle(Client c)
        {
            var p = getVehiclePrice((VehicleHash)API.getEntityModel(c.vehicle));
            var p1 = Convert.ToInt32((double)p / 150);
            if (API.getEntityData(c.vehicle, "rental") == true)
            {
                if (Money.TakeMoney(c, p1))
                {
                    API.sendNotificationToPlayer(c, "Drive Safe!");
                    API.setEntityData(c.vehicle, "rental", false);
                    API.setVehicleEngineStatus(c.vehicle, true);
                    API.sendNotificationToPlayer(c, "Success!", true);
                    return;
                }
                API.sendNotificationToPlayer(c, "Not Enough Cash");
            }
       }

        public void onResStop()
        {
            //  saveOwnedVehicles();
            // saveVehicles();
        }


        public static void saveOwnedVehicles()
        {
            XDocument vehicleData = new XDocument(new XElement("Vehicles"));
            var Vehs = vehicleData.Element("Vehicles");
            foreach (NetHandle v in API.shared.getAllVehicles())
            {
                if (API.shared.hasEntityData(v, "OwnedVehicleID"))
                {

                    var pos = API.shared.getEntityPosition(v);
                    if (pos != new Vector3(0, 0, 0))
                    {
                        var rot = API.shared.getEntityRotation(v);
                        API.shared.consoleOutput("saving car at X:" + pos.X.ToString() + " Y:" + pos.Y.ToString() + " Z:" + pos.Z.ToString());
                        var c1 = API.shared.getVehiclePrimaryColor(v);
                        var dimension = 1;
                        var OwnedVehicleID = API.shared.getEntityData(v, "OwnedVehicleID");
                        var spawnpos = API.shared.getEntityData(v, "SPAWN_POS");
                        Vehs.Add(new XElement("Vehicle", new XAttribute("OwnedVehicleID", OwnedVehicleID), new XAttribute("Model", API.shared.getEntityModel(v)), new XAttribute("X", pos.X), new XAttribute("Y", pos.Y), new XAttribute("Z", pos.Z), new XAttribute("RZ", rot.Z), new XAttribute("spawnX", spawnpos.X), new XAttribute("spawnY", spawnpos.Y), new XAttribute("spawnZ", spawnpos.Z), new XAttribute("C1", c1), new XAttribute("Dimension", dimension)));
                        Items.saveVih(v);
                    }
                    else
                    {
                        API.shared.setEntityPosition(v, API.shared.getEntityData(v, "SPAWN_POS"));
                    }
                }
            }
            vehicleData.Save("OwnedVehicles.xml"); 
        }

        
        public static void saveVehicles()
        {
            XDocument vehicleData = new XDocument(new XElement("Vehicles"));
            var Vehs = vehicleData.Element("Vehicles");
            foreach (NetHandle v in API.shared.getAllVehicles())
            {
                var pos = API.shared.getEntityPosition(v);
                var rot = API.shared.getEntityRotation(v);
                API.shared.consoleOutput("saving car at X:" + pos.X.ToString() + " Y:" + pos.Y.ToString() + " Z:" + pos.Z.ToString());
                var c1 = API.shared.getVehiclePrimaryColor(v);
                var dimension = API.shared.getEntityDimension(v);
                Vehs.Add(new XElement("Vehicle", new XAttribute("Model", API.shared.getEntityModel(v)), new XAttribute("posX", pos.X), new XAttribute("posY", pos.Y), new XAttribute("posZ", pos.Z), new XAttribute("heading", rot.Z), new XAttribute("color", c1), new XAttribute("rental", false)));
               // Items.saveVih(v);
            }
            vehicleData.Save("Vehicles.xml");
        }
        

        [Command("park")]
        public void park(Client sender)
        {
            if (sender.vehicle.hasData("OwnedVehicleID"))
            {
                sender.vehicle.setData("SPAWN_POS", sender.vehicle.position);
                API.triggerClientEvent(sender, "display_subtitle", "Parked.", 3000);
            }
        }

        public void loadOwnedVehicles()
        {
            if (File.Exists("OwnedVehicles.xml"))
            {
                API.consoleOutput("Loading player vehicles...");
                XElement xelement = XElement.Load("OwnedVehicles.xml");
                IEnumerable<XElement> Vehicles = xelement.Elements();
                foreach (var v in Vehicles)
                {
                    API.consoleOutput("Loading player vehicles...");
                    var pX = Convert.ToDouble(v.Attribute("X").Value);
                    var pY = Convert.ToDouble(v.Attribute("Y").Value);
                    var pZ = Convert.ToDouble(v.Attribute("Z").Value);
                    var rZ = Convert.ToDouble(v.Attribute("RZ").Value);
                    var spawnX = Convert.ToDouble(v.Attribute("spawnX").Value);
                    var spawnY = Convert.ToDouble(v.Attribute("spawnY").Value);
                    var spawnZ = Convert.ToDouble(v.Attribute("spawnZ").Value);
                    var model = (VehicleHash)Convert.ToInt32(v.Attribute("Model").Value);
                    var c1 = Convert.ToInt32(v.Attribute("C1").Value);
                    var dimension = Convert.ToInt32(v.Attribute("Dimension").Value);
                    var car = API.createVehicle(model, new Vector3(pX, pY, pZ), new Vector3(0, 0, rZ), c1, dimension);
                    car.invincible = true;
                    Items.items.Add(new Items.VehicleKey(OwnedVehicleID, "Vehicle Key", "Unlocks a vehicle"));
                    //API.setEntityData(car, "RESPAWNABLE", true);
                    API.setEntityData(car, "SPAWN_POS", new Vector3(spawnX,spawnY,spawnZ));
                    API.setEntityData(car, "OwnedVehicleID", OwnedVehicleID);
                    Items.loadVih(car);
                    OwnedVehicleID++;
                    API.setVehicleLocked(car, true);
                }
            }
        }
        public void loadVehicles()
        {
            XElement xelement = XElement.Load("vehicles.xml");
            IEnumerable<XElement> Vehs = xelement.Elements();
            foreach (var v in Vehs)
            {
                var pX = Convert.ToDouble(v.Attribute("posX").Value);
                var pY = Convert.ToDouble(v.Attribute("posY").Value);
                var pZ = Convert.ToDouble(v.Attribute("posZ").Value);
                var rX = Convert.ToDouble(v.Attribute("heading").Value);
                var model = (VehicleHash)Convert.ToInt32(v.Attribute("Model").Value);
                var rental = Convert.ToBoolean(v.Attribute("rental").Value);
                var color = Convert.ToInt32(v.Attribute("color").Value);
                var car = API.createVehicle(model, new Vector3(pX, pY, pZ), new Vector3(0, 0, rX), color, 0);
                API.setEntityData(car, "rental", rental);
                Items.InventoryHolder ih = new Items.InventoryHolder();
                ih.Owner = car.handle;
                API.shared.setEntityData(car, "InventoryHolder", ih);
                if (rental == false)
                {
                    API.setVehicleLocked(car, true);
                    if (car.model == -1050465301)
                    {
                        car.setExtra(0, false);
                        car.setExtra(1, false);
                        car.setExtra(2, false);
                        car.setExtra(3, true);
                        car.setExtra(4, false);
                        car.setExtra(5, false);
                        car.setExtra(6, false);
                        car.setExtra(7, false);
                        car.openDoor(5);
                        for (var i = 0; i < 15; i++)
                        {
                            ih.AddItemToInventory(Items.ItemByID(56));
                            ih.AddItemToInventory(Items.ItemByID(57));
                            ih.AddItemToInventory(Items.ItemByID(58));
                        }
                    }
                    API.shared.setEntityData(car, "Vehicle", car);
               }
            }
        }
    }
}