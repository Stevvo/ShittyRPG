using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GTANetworkServer;
using GTANetworkShared;

namespace IcaroRPG.Global
{
    public class LosSantosCustoms : Script
    {
        public const int LSC_ROOT = 126;
        public const int LSC_CHILD = 127;
        
        public const int SPOILER = 0;
        public const int FRONTBUMPER = 1;
        public const int REARBUMPER = 2;
        public const int SKIRT = 3;
        public const int EXHAUST = 4;
        public const int ROLLCAGE = 5;
        public const int GRILLE = 6;
        public const int HOOD = 7;
        public const int FENDER = 8;
        public const int RFENDER = 9;
        public const int ROOF = 10;
        public const int ENGINE = 11;
        public const int BRAKES = 12;
        public const int TRANSMISSION = 13;
        public const int HORNS = 14;
        public const int SUSPENSION = 15;
        public const int ARMOR = 16;
        public const int FWHEEL = 23;
        public const int BWHEEL = 24;
        

        private static readonly Dictionary<int, string> Mods = new Dictionary<int, string>()
        {
            {0, "Spoiler"},
            {1, "Front Bumper"},
            {2, "Rear Bumper"},
            {3, "Skirts"},
            {4, "Exhaust"},
            {5, "Rollcage"},
            {6, "Grille"},
            {7, "Hood"},
            {8, "Fender"},
            {9, "Rear Fender"},
            {10, "Roof"},
            {11, "Engine"},
            {12, "Brakes"},
            {13, "Transmission"},
            {14, "Horns"},
            {15, "Suspension"},
            {16, "Armor"},
            {18, "Turbo" },
            {22, "Xenon Lights" },
            {23, "Wheels"},
            {24, "Rear Wheel"},
            {25, "Plate Holder"},
            {27, "Trim Design" },
            {28, "Ornaments" },
            {30, "Dials" },
            {33, "Steering Wheel" },
            {34, "Shifter" },
            {35, "Plaque" },
            {38, "Hydraulics" },
            {48, "Livery" },
            {69, "Window Tint" },
        };

      private static readonly string[] Horns = new string[62]
      {
        "Truck",
        "Cop",
        "Clown",
        "Musical 1",
        "Musical 2",
        "Musical 3",
        "Musical 4",
        "Musical 5",
        "Sad Horn",
        "Classical 1",
        "Classical 2",
        "Classical 3",
        "Classical 4",
        "Classical 5",
        "Classical 6",
        "Classical 7",
        "Do",
        "Re",
        "Mi",
        "Fa",
        "Sol",
        "La",
        "Si",
        "Do (High)",
        "Jazz 1",
        "Jazz 2",
        "Jazz 3",
        "Jazz Loop",
        "Star Spangled Banner 1",
        "Star Spangled Banner 2",
        "Star Spangled Banner 3",
        "Star Spangled Banner 4",
        "Classical Horn Loop 1",
        "Classical Horn Loop 2",
        "Classical Horn Loop 3",
        "Classical Horn 8",
        "Classical Horn 9",
        "Classical Horn 10",
        "Funeral (Loop)",
        "Funeral",
        "Spooky (Loop)",
        "Spooky",
        "San Andreas (Loop)",
        "San Andreas",
        "Libery City (Loop)",
        "Libery City",
        "Christmas 1 (Loop)",
        "Christmas 1",
        "Christmas 2 (Loop)",
        "Christmas 2",
        "Christmas 3 (Loop)",
        "Christmas 3",
        "Unknown",
        "Unknown",
        "Unknown",
        "Unknown",
        "Unknown",
        "Unknown",
        "Unknown",
        "Unknown",
        "Unknown",
        "Unknown"
      };

        private static readonly string[] Engines = new string[5]
      {
        "EMS Upgrade, Level 1",
        "EMS Upgrade, Level 2",
        "EMS Upgrade, Level 3",
        "EMS Upgrade, Level 4",
        "EMS Upgrade, Level 5"
      };
        private static readonly string[] Brakes = new string[3]
      {
        "Street Brakes",
        "Sport Brakes",
        "Race Brakes"
      };
        private static readonly string[] Transmissions = new string[4]
      {
        "Street Transmission",
        "Sports Transmission",
        "Race Transmission",
        "Monster Transmission"
      };
        private static readonly string[] Suspensions  = new string[4]
      {
        "Lowered Suspension",
        "Street Suspension",
        "Sport Suspension",
        "Competition Suspension"
      };

        public LosSantosCustoms()
        {
            API.onClientEventTrigger += ClientEvent;
            API.onEntityEnterColShape += EnterColshape;
           var lsc = API.createCylinderColShape(new Vector3(-359, -134, 39),8, 3);
           lsc.setData("LSC", true);
        }

        private void EnterColshape(ColShape colshape, NetHandle entity)
        {
            
            if(colshape.hasData("LSC"))
            {

                var player = API.getPlayerFromHandle(entity);
                if (player == null) return;
                if (!player.isInVehicle) return;
                if (player.vehicleSeat != -1) return;
                else
                {

                    API.sendNativeToPlayer(player, Hash.DO_SCREEN_FADE_OUT, 200);
                    Task t = Task.Run(() => getVehicleModSlots(player, player.vehicle));
                    int dimension = DimensionManager.RequestPrivateDimension(player);
                    player.dimension = dimension;
                    player.vehicle.dimension = dimension;
                    Cams.createCameraAtGamecam(player);
                    API.delay(3000, true, () =>
                        {
                            API.sendNativeToPlayer(player, Hash.DO_SCREEN_FADE_IN, 200);
                            if (t.IsCompleted)
                            {
                                //API.triggerClientEvent(player, "activate_camera", new Vector3(-335.06, -134.72, 39.67));
                                player.vehicle.position = new Vector3(-338.24, -136.36, 38.66);
                                player.vehicle.rotation = new Vector3(0, 0, -108);
                                player.vehicle.freezePosition = true;
                                Cams.createCameraActive(player, new Vector3(-335.06, -134.72, 39.67), new Vector3(0, 0, 0));
                                Cams.pointCameraAtLocalPlayer(player, 1, new Vector3(0, 0, 0));
                               // Cams.interpolateCamera(player, 1, 1000, true, true);
                                menuRoot(player, player.vehicle);
                            }
                        });
                }
            }
        }

        public void leaveLSC(Client player)
        {
            API.sendNativeToPlayer(player, Hash.DO_SCREEN_FADE_OUT, 1);
            API.sendNativeToPlayer(player, Hash.DO_SCREEN_FADE_IN, 1000);
            API.triggerClientEvent(player, "kill_camera", new Vector3(-335.06, -134.72, 39.67));
            player.vehicle.freezePosition = false;
            DimensionManager.DismissPrivateDimension(player);
            player.dimension = 0;
            player.vehicle.dimension = 0;
            Cams.clearCameras(player);
        }

       private void ClientEvent(Client sender, string eventName, object[] args)
        {
            if (eventName == "menu_handler_select_item")
            {
                var callbackId = (int)args[0];
                var index = (int)args[1];
                if (callbackId == LSC_ROOT)
                {
                    if (index == 0) { leaveLSC(sender); return; }
                    List<int> slots = sender.vehicle.getData("Slots");
                    var slot = slots[index-1];
                    menuChild(sender, slot);
                    sender.vehicle.setData("SelectedSlot", slot);
                }
                else if (callbackId == LSC_CHILD)
                {
                    if (index == 0) { menuRoot(sender, sender.vehicle); return; }
                    var slot = sender.vehicle.getData("SelectedSlot");
                    API.setVehicleMod(sender.vehicle, slot, index-1);
                    menuRoot(sender, sender.vehicle);
                }
            }
        }

        public int getNumVehicleMods(Client player, NetHandle vehicle, int slot)
        {
            int x = API.shared.fetchNativeFromPlayer<int>(player, Hash.GET_NUM_VEHICLE_MODS, vehicle, slot);
            return x;
        }

        public void getVehicleModSlots(Client player, NetHandle vehicle)
        {
            List<int> availableslots = new List<int>();
            for (var i = 0; i < Mods.Count; i++)
            {
                int s = API.fetchNativeFromPlayer<int>(player, Hash.GET_NUM_VEHICLE_MODS, vehicle, i);
                if (s > 0)
                {
                    availableslots.Add(i);
                    API.setEntityData(vehicle, "Slot" + i, getNumVehicleMods(player, vehicle, i));
                }
            }
            API.setEntityData(vehicle, "Slots", availableslots);
            return;
        }

        public void menuChild(Client player, int slot)
        {
            var availablemods = API.getEntityData(player.vehicle, "Slot" + slot);
            object[] argslist = new object[7 + availablemods*2];
            argslist[0] = LSC_CHILD;
            argslist[1] = Mods[slot];
            argslist[2] = "Select mod";
            argslist[3] = true;
            argslist[4] = availablemods+1;
            argslist[5] = "~b~Back";
            for (var i = 0; i < availablemods; i++)
            {
                 var x = "mod";
                switch (slot)
                {
                    default:
                        x = "Mod" + i;
                        break;
                    case ENGINE:
                        x = Engines[i];
                        break;
                    case BRAKES:
                        x = Brakes[i];
                        break;
                    case TRANSMISSION:
                        x = Transmissions[i];
                        break;
                    case SUSPENSION:
                        x = Suspensions[i];
                        break;
                }

                argslist[6 + i] = x;
            }
            API.triggerClientEvent(player, "menu_handler_create_menu", argslist);
        }

        public void menuRoot(Client player, NetHandle vehicle)
        {
            List<int> slots = API.getEntityData(vehicle, "Slots");
            if (slots == null) return;
            object[] argslist = new object[7+slots.Count*2];
            argslist[0] = LSC_ROOT;
            argslist[1] = "Los Santos Customs";
            argslist[2] = "Select slot";
            argslist[3] = true;
            argslist[4] = slots.Count+1;
            argslist[5] = "~b~Back";
            var i = 0;
            foreach(var s in slots)
            {
                API.consoleOutput(s.ToString());
                string x = "Unknown";
                Mods.TryGetValue(s, out x);
                argslist[6 + i] = x;
                //argslist[6+ slots.Count + i] = "->";
                i++;
            }
            API.triggerClientEvent(player, "menu_handler_create_menu", argslist);
        }
    }
}
