using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkServer;
using GTANetworkShared;

namespace IcaroRPG
{
    public class Interaction : Script
    {
        public static List<Vector3> sprunkerspositions = new List<Vector3>();
        public static Dictionary<int, float> SittingOffsets = new Dictionary<int, float>(){
        {-1631057904, 0.45f},
        {-1317098115, 0.5f },
        {-171943901, -0.5f},
        {1805980844, 0.5f},
        {-403891623, 0.5f},
        {437354449, 0.5f},
        {291348133, 0.05f},
        {146905321, 0.5f},
        {-741944541, 0.5f},
        {-628719744, 0.3f }
        };
        public List<NetHandle> OccupiedObjects = new List<NetHandle>(); 
        public Interaction()
        {
            API.onClientEventTrigger += clientEvent;
            API.onEntityEnterColShape += ColHit;
            API.onPlayerEnterVehicle += onEnterVehicle;
            API.onPlayerExitVehicle += onExitVehicle;
            loadShapes();
            loadVends();
        }

        public void onEnterVehicle(Client player, NetHandle vehicle)
        {
            if (API.hasEntityData(vehicle, "Vehicle"))
            {
                Vehicle V = API.getEntityData(vehicle, "Vehicle");
                if (V.model == -1050465301)
                {
                    V.closeDoor(5);
                    object[] args = new object[sprunkerspositions.Count+1];
                    args[0] = sprunkerspositions.Count;
                    for (var i = 0; i < sprunkerspositions.Count; i++)
                    {
                        args[i+1] = sprunkerspositions[i];
                    }
                    API.triggerClientEvent(player, "makeBlips", args);
                }
            }
        }

        public void onExitVehicle(Client player, NetHandle vehicle)
        {
            if (API.hasEntityData(vehicle, "Vehicle"))
            {
                Vehicle V = API.getEntityData(vehicle, "Vehicle");
                if (V.model == -1050465301)
                {
                    V.closeDoor(5);
                    API.triggerClientEvent(player, "deleteBlips");
                }
            }
        }

        public void loadShapes()
        {
            string[] lines = System.IO.File.ReadAllLines("objects.txt");
            foreach (string line in lines)
            {
                string[] words = line.Split();
                var shape = API.createSphereColShape(new Vector3(Convert.ToDouble(words[0]), Convert.ToDouble(words[1]), Convert.ToDouble(words[2])), 4);
                shape.setData("Bench", true);
            }
        }

        public void loadVends()
        {
            string[] lines = System.IO.File.ReadAllLines("vending.txt");
            foreach (string line in lines)
            {
                string[] words = line.Split();
                var shape = API.createSphereColShape(new Vector3(Convert.ToDouble(words[0]), Convert.ToDouble(words[1]), Convert.ToDouble(words[2])), 3);
                shape.setData("vending", words[3]);
                shape.setData("filled", false);
                var blip = API.createBlip(new Vector3(Convert.ToDouble(words[0]), Convert.ToDouble(words[1]), Convert.ToDouble(words[2])));
                blip.transparency = 0;
                sprunkerspositions.Add(blip.position);
            }
        }

        public void ColHit(ColShape shape, NetHandle ent)
        {
            var player = API.getPlayerFromHandle(ent);
            if (player == null) return;
            if (shape.hasData("Bench"))
            {
                if (player.vehicle != null) return;
                API.sendNotificationToPlayer(player, "Press E to sit down");
            }
            else if (shape.hasData("vending") && shape.getData("filled") == false)
            {
                API.consoleOutput("vendingmachinhit");
                Items.InventoryHolder ih = API.shared.getEntityData(ent, "InventoryHolder");
                if (shape.getData("vending") == "3B21C5E7")
                {
                    foreach (Items.InventoryItem ii in ih.Inventory)
                    {
                        if (ii.Details.ID == 57)
                        {
                            ih.RemoveItemFromInventory(Items.ItemByID(57));
                            API.triggerClientEvent(player, "display_subtitle", "~g~Goods Delivered! ~r~$40", 5000);
                            shape.setData("filled", true);
                            API.delay(1000000, true, () => { shape.setData("filled", false); });
                        }
                    }
                }
                else if (shape.getData("vending") == "426A547C")
                {
                    foreach (Items.InventoryItem ii in ih.Inventory)
                    {
                        if (ii.Details.ID == 56)
                        {
                            ih.RemoveItemFromInventory(Items.ItemByID(56));
                            API.triggerClientEvent(player, "display_subtitle", "~g~Goods Delivered! ~r~$45", 5000);
                            shape.setData("filled", true);
                            API.delay(1100000, true, () => { shape.setData("filled", false); });
                        }
                    }
                }
                else if (shape.getData("vending") == "418F055A")
                {
                    foreach (Items.InventoryItem ii in ih.Inventory)
                    {
                        if (ii.Details.ID == 58)
                        {
                            
                            ih.RemoveItemFromInventory(Items.ItemByID(58));
                            API.triggerClientEvent(player, "display_subtitle", "~g~Goods Delivered! ~r~$60", 5000);
                            shape.setData("filled", true);
                            API.delay(1300000, true, () => { shape.setData("filled", false); });
                        }
                    }
                }
            }
            else if (shape.hasData("vending") && shape.getData("filled") == true)
            {
                API.sendNotificationToPlayer(player, "This Machine has already been filled");
            }
        }


        public void clientEvent(Client sender, string eventName, object[] args)
        {
             if (eventName == "Interact")
            {
                var model = (int)args[0];
                if (!OccupiedObjects.Contains((NetHandle)args[3]))
                {
                    if (SittingOffsets.ContainsKey(model))
                    {
                       
                        var benchpos = (Vector3)args[1];
                        benchpos.Z = benchpos.Z + 0.5f;
                        var benchrot = (Vector3)args[2];
                        benchrot.Z = benchrot.Z - 180;
                        API.freezePlayer(sender, true);
                        API.setEntityPosition(sender, benchpos);
                        API.setEntityRotation(sender, benchrot);
                        //   API.playPlayerScenario(sender, "PROP_HUMAN_SEAT_BENCH");
                        API.playPlayerAnimation(sender, 9, "amb@prop_human_seat_chair@male@generic@base", "base");
                      //  OccupiedObjects.Add((NetHandle)args[3]);
                    }
                    if (model == 1329570871)
                    {
                        var benchpos = (Vector3)args[1];
                        //  benchpos.X = benchpos.X + 0.1f;
                        benchpos.Y = benchpos.Y + 0.15f;
                        benchpos.Z = benchpos.Z + 1f;
                        var benchrot = (Vector3)args[2];
                        benchrot.Z = benchrot.Z - 180;
                        API.setEntityPosition(sender, benchpos);
                        API.setEntityRotation(sender, benchrot);
                        API.freezePlayer(sender, true);
                        API.playPlayerScenario(sender, "PROP_HUMAN_BUM_BIN");
                      //  OccupiedObjects.Add((NetHandle)args[3]);
                    }
                    if (model == -1126237515 || model == -1364697528 || model == 506770882)
                    {
                        //ATM
                    }

                }
            }
            else if(eventName == "CancelInteract")
            {
                    //   OccupiedObjects.Remove((NetHandle)args[3]);
                    API.freezePlayer(sender, false);
                    API.clearPlayerTasks(sender);
                    API.stopPlayerAnimation(sender);
                    API.resetEntityData(sender, "sat");
            }
           
        }
    }
}
