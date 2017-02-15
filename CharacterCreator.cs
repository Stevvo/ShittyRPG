using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkServer;
using GTANetworkShared;
using System.IO;
using System.Xml.Linq;

namespace IcaroRPG
{
    public class CharacterCreator : Script
    {
        public const int PED_OUTFIT = 50;
        public const int PED_CREATOR = 51;
        public const int PED_HEAD = 53;
        public static List<string> Males = new List<string>();
        public static List<string> Females = new List<string>();
        int[] maleheads = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 24, 26, 30, 41, 42, 43, 44 };
        int[] femaleheads = { 21, 22, 23, 24, 25, 26, 27, 28, 29, 31, 33, 34, 35, 36, 37, 38, 39, 40, 45 };
        public enum Component : int
        {
            Head = 0,
            Beard = 1,
            Hair = 2,
            Torso = 3,
            Legs = 4,
            Hands = 5,
            Shoes = 6,
            Accessories = 7,
            Undershirt = 8,
            Task = 9,
            CrewEmblem = 10,
            Top = 11
        }

        public CharacterCreator()
        {
            API.onClientEventTrigger += clientEvent;
            API.createPed((PedHash)(1885233650), new Vector3(413.840118, -979.650391, -98.9991608), 2.51195335f);
            API.createPed((PedHash)(-1667301416), new Vector3(404.549866, -979.655334, -99.0087051), -10.7427425f);
            API.createPed((PedHash)(1423699487), new Vector3(395.653625, -979.568298, -99.0094223), 0.571926892f);
            populateOutfitLists();
        }

        public void populateOutfitLists()
        {
            string[] fileEntries = Directory.GetFiles("Outfits/Males");
            foreach (string fileName in fileEntries)
            {
                Males.Add(fileName);
            }
            string[] fileEntriesf = Directory.GetFiles("Outfits/Females");
            foreach (string fileName in fileEntriesf)
            {
                Females.Add(fileName);
            }
        }


        public void showPedMenu(Client player)
        {
            
            if (player.IsNull) return;
            if (player.model == (int)PedHash.FreemodeMale01)
            {
                var itemsLen = Males.Count;
                object[] argumentList = new object[5 + itemsLen*2];
                argumentList[0] = PED_OUTFIT;
                argumentList[1] = "Outfit";
                argumentList[2] = "~r~Choose an outfit!";
                argumentList[3] = true;
                argumentList[4] = itemsLen;
                for (var i = 0; i < itemsLen; i++)
                {

               
                    argumentList[5 + i] = Males[i].Remove(Males[i].Length - 4, 4).Remove(0, 16);
                    argumentList[5 + itemsLen + i] = "";
                }
                API.triggerClientEvent(player, "menu_handler_create_menu2", argumentList);
            }
            else
            {
                var itemsLen = Females.Count;
                object[] argumentList = new object[5 + itemsLen*2];
                argumentList[0] = PED_OUTFIT;
                argumentList[1] = "Outfit";
                argumentList[2] = "~r~Choose an outfit!";
                argumentList[3] = true;
                argumentList[4] = itemsLen;
                for (var i = 0; i < itemsLen; i++)
                {
                    argumentList[5 + i] = Females[i].Remove(Females[i].Length - 4, 4).Remove(0, 17);
                    argumentList[5 + itemsLen + i] = "";
                }
                API.triggerClientEvent(player, "menu_handler_create_menu2", argumentList);
            }
        }

        public void clientEvent(Client sender, string eventName, object[] args)
        {
            if (eventName == "menu_handler_select_item")
            {
                var callbackId = (int)args[0];
                var ind = (int)args[1];
                if (callbackId == PED_HEAD)
                {
                    switch (ind)
                    {
                        case 0:
                            {
                                var count = sender.getData("Scount");
                                if (sender.model == (int)PedHash.FreemodeMale01)
                                {
                                    if (count < maleheads.Count())
                                    {
                                        sender.setData("GTAO_SHAPE_FIRST_ID", maleheads[count]);
                                        sender.setSyncedData("GTAO_SHAPE_FIRST_ID", maleheads[count]);
                                        sender.setData("GTAO_SKIN_FIRST_ID", maleheads[count]);
                                        sender.setSyncedData("GTAO_SKIN_FIRST_ID", maleheads[count]);
                                        API.call("GTAOnlineCharacter", "updatePlayerFace", sender.handle);

                                        count++;
                                        sender.setData("Scount", count);
                                    }
                                    else
                                    {
                                        count = 0;
                                        sender.setData("Scount", count);
                                    }
                                }
                                else
                                {
                                    if (count < femaleheads.Count())
                                    {
                                        sender.setData("GTAO_SHAPE_FIRST_ID", femaleheads[count]);
                                        sender.setSyncedData("GTAO_SHAPE_FIRST_ID", femaleheads[count]);
                                        sender.setData("GTAO_SKIN_FIRST_ID", femaleheads[count]);
                                        sender.setSyncedData("GTAO_SKIN_FIRST_ID", femaleheads[count]);
                                        API.call("GTAOnlineCharacter", "updatePlayerFace", sender.handle);

                                        count++;
                                        sender.setData("Scount", count);
                                    }
                                    else
                                    {
                                        count = 0;
                                        sender.setData("Scount", count);
                                    }
                                }
                                break;
                            }
                        case 1:
                            {
                                var count = sender.getData("Scount");
                                if (sender.model == (int)PedHash.FreemodeMale01)
                                {
                                    if (count < maleheads.Count())
                                    {
                                        sender.setData("GTAO_SHAPE_SECOND_ID", maleheads[count]);
                                        sender.setSyncedData("GTAO_SHAPE_SECOND_ID", maleheads[count]);
                                        sender.setData("GTAO_SKIN_SECOND_ID", maleheads[count]);
                                        sender.setSyncedData("GTAO_SKIN_SECOND_ID", maleheads[count]);
                                        API.call("GTAOnlineCharacter", "updatePlayerFace", sender.handle);

                                        count++;
                                        sender.setData("Scount", count);
                                    }
                                    else
                                    {
                                        count = 0;
                                        sender.setData("Scount", count);
                                    }
                                }
                                else
                                {
                                    if (count < femaleheads.Count())
                                    {
                                        sender.setData("GTAO_SHAPE_SECOND_ID", femaleheads[count]);
                                        sender.setSyncedData("GTAO_SHAPE_SECOND_ID", femaleheads[count]);
                                        sender.setData("GTAO_SKIN_SECOND_ID", femaleheads[count]);
                                        sender.setSyncedData("GTAO_SKIN_SECOND_ID", femaleheads[count]);
                                        API.call("GTAOnlineCharacter", "updatePlayerFace", sender.handle);

                                        count++;
                                        sender.setData("Scount", count);
                                    }
                                    else
                                    {
                                        count = 0;
                                        sender.setData("Scount", count);
                                    }
                                }
                                break;
                            }
                        case 2:
                            {
                                var count = sender.getData("Scount");
                                if (count < 31) {
                                    sender.setData("GTAO_EYE_COLOR", count);
                                    sender.setSyncedData("GTAO_EYE_COLOR", count);
                                    API.call("GTAOnlineCharacter", "updatePlayerFace", sender.handle);
                                    count++;
                                    sender.setData("Scount", count);
                                }
                                else {
                                    count = 0;
                                    sender.setData("Scount", count);
                                }
                                break;
                            }
                        case 3:
                            {
                                var count = sender.getData("Scount");
                                if (count < 33) {
                                    sender.setData("GTAO_EYEBROWS", count);
                                    sender.setSyncedData("GTAO_EYEBROWS", count);
                                    API.call("GTAOnlineCharacter", "updatePlayerFace", sender.handle);
                                    count++;
                                    sender.setData("Scount", count);
                                }
                                else
                                {
                                    count = 0;
                                    sender.setData("Scount", count);
                                }
                                break;
                            }
                        case 4:
                            {
                                var count = sender.getData("Scount");
                                if (count < 37) {
                                    API.setPlayerClothes(sender, 2, count, 0);
                                    sender.setData("draw2", count);
                                    count++;
                                    sender.setData("Scount", count);
                                }
                                else
                                {
                                    count = 0;
                                    sender.setData("Scount", count);
                                }
                                break;
                            }
                        case 5:
                            {
                                var count = sender.getData("Scount");
                                if (SetHairColor(sender, count)) {
                                    count++;
                                    sender.setData("Scount", count);
                                }
                                else {
                                    count = 0;
                                    sender.setData("Scount", count);
                                }
                                break;
                            }
                        case 6:
                            {
                                var count = sender.getData("Scount");
                                if (count < 29)
                                {
                                    if (sender.model == (int)PedHash.FreemodeMale01)
                                    {
                                        sender.setData("GTAO_BEARD", count);
                                        sender.setSyncedData("GTAO_BEARD", count);
                                    }
                                    else
                                    {
                                        sender.setData("GTAO_MAKEUP", count);
                                        sender.setSyncedData("GTAO_MAKEUP", count);
                                    }
                                    API.call("GTAOnlineCharacter", "updatePlayerFace", sender.handle);
                                    count++;
                                    sender.setData("Scount", count);
                                }
                                else
                                {
                                    count = 0;
                                    sender.setData("Scount", count);
                                }
                                break;
                            }
                        case 7:
                            {
                                var count = sender.getData("Scount");
                                if (count < 28)
                                {
                                    if (sender.model == (int)PedHash.FreemodeMale01)
                                    {
                                        sender.setData("GTAO_BEARD_COLOR", count);
                                        sender.setSyncedData("GTAO_BEARD_COLOR", count);
                                    }
                                    else
                                    {
                                        sender.setData("GTAO_MAKEUP_COLOR", count);
                                        sender.setSyncedData("GTAO_MAKEUP_COLOR", count);
                                        sender.setData("GTAO_MAKEUP_COLOR2", count);
                                        sender.setSyncedData("GTAO_MAKEUP_COLOR2", count);
                                    }
                                    API.call("GTAOnlineCharacter", "updatePlayerFace", sender.handle);
                                    count++;
                                    sender.setData("Scount", count);
                                }
                                else
                                {
                                    count = 0;
                                    sender.setData("Scount", count);
                                }
                                break;
                            }
                        case 8:
                            {
                                showPedMenu(sender);
                                Cams.interpolateCamera(sender, 4, 500, false, false);
                                API.triggerClientEvent(sender, "display_subtitle", "Press ~g~E ~w~ to confirm outfit and play!", 30000);
                                API.sendChatMessageToPlayer(sender, "Press ~g~E ~w~ to confirm outfit and play!");
                                API.setEntitySyncedData(sender, "LastStep", true);
                                break;
                            }
                    }
                }

                else if (callbackId == PED_CREATOR)
                {
                    switch (ind)
                    {
                        case 0:
                            {
                                // API.triggerClientEvent(sender, "skin_type_gtaom");
                                Cams.interpolateCamera(sender, 0, 1000, false, false);
                                showCreatorMenu(sender);
                                API.setPlayerSkin(sender, PedHash.FreemodeMale01);
                                API.call("GTAOnlineCharacter", "initializePedFace", sender.handle);
                                API.setEntitySyncedData(sender, "GTAO_HAS_CHARACTER_DATA", true);
                                API.setEntityData(sender, "GTAO_HAS_CHARACTER_DATA", true);
                                API.setEntityData(sender, "GTAO_SHAPE_MIX", 0.35);
                                API.setEntityData(sender, "GTAO_SKIN_MIX", 0.35);
                                API.setEntitySyncedData(sender, "GTAO_SHAPE_MIX", 0.35);
                                API.setEntitySyncedData(sender, "GTAO_SKIN_MIX", 0.35);
                                var skin = (int)PedHash.FreemodeMale01;
                                API.setEntityData(sender, "Skin", skin);
                            }
                            break;
                        case 1:
                            {
                               // API.triggerClientEvent(sender, "skin_type_gtaof");
                                Cams.interpolateCamera(sender, 1, 1000, false, false);
                                showCreatorMenu(sender);
                                API.setPlayerSkin(sender, PedHash.FreemodeFemale01);
                                API.call("GTAOnlineCharacter", "initializePedFace", sender.handle);
                                API.setEntitySyncedData(sender, "GTAO_HAS_CHARACTER_DATA", true);
                                API.setEntityData(sender, "GTAO_HAS_CHARACTER_DATA", true);
                                API.setEntityData(sender, "GTAO_SHAPE_MIX", 0.35);
                                API.setEntityData(sender, "GTAO_SKIN_MIX", 0.35);
                                API.setEntitySyncedData(sender, "GTAO_SHAPE_MIX", 0.65);
                                API.setEntitySyncedData(sender, "GTAO_SKIN_MIX", 0.65);
                                var skin = (int)PedHash.FreemodeFemale01;
                                API.setEntityData(sender, "Skin", skin);
                            }
                            break;
                        case 2:
                            {
                                // API.triggerClientEvent(sender, "skin_type_ped");
                                Cams.interpolateCamera(sender, 2, 1000, false, false);
                                showCreatorMenu(sender);
                                API.setPlayerSkin(sender, PedHash.Cow);
                                var skin = (int)PedHash.Cow;
                                API.setEntityData(sender, "Skin", skin);
                            }
                            break;
                        case 3:
                            {
                                API.triggerClientEvent(sender, "menu_handler_close_menu");
                                divergeCreator(sender);
                            }
                            break;
                    }
                }
                else if (callbackId == PED_OUTFIT)
                {
                    if (sender.model == (int)PedHash.FreemodeMale01)
                    {
                        setOutfit(sender, Males[ind]);
                        //  showPedMenu(sender);
                    }
                    if (sender.model == (int)PedHash.FreemodeFemale01)
                    {
                        setOutfit(sender, Females[ind]);
                        //    showPedMenu(sender);
                    }
                }
            }
            else if (eventName == "stopCreator")
            {
                API.sendNativeToPlayer(sender, Hash.DO_SCREEN_FADE_OUT, 200);
                API.triggerClientEvent(sender, "menu_handler_close_menu");
               // API.stopPlayerAnimation(sender);
                API.delay(800, true, () => {
                    Cams.clearCameras(sender);
                    sender.freeze(false);
                    var homeid = sender.getData("HomeID");
                    DimensionManager.DismissPrivateDimension(sender);
                    foreach (Homes.Home home in Homes.Homeslist)
                    {
                        if (homeid == home.ID)
                        {
                            home.teleportIn(sender, sender.getData("HomeDimension"));
                            API.triggerClientEvent(sender, "display_subtitle", "~r~F1 Player Menu ~g~F2 Vehicle Menu", 6000);
                        }
                    }
                    API.resetEntitySyncedData(sender, "LastStep");
                    API.sendNativeToPlayer(sender, Hash.DO_SCREEN_FADE_IN, 750);

                });
            }
        }

        public bool SetHairColor(Client player, int color)
        {
            if (color > 63) return false;
            API.setEntitySyncedData(player, "GTAO_HAIR_COLOR", color);
            API.setEntitySyncedData(player, "GTAO_HAIR_HIGHLIGHT_COLOR", color);
            API.setEntityData(player, "GTAO_HAIR_COLOR", color);
            API.setEntityData(player, "GTAO_HAIR_HIGHLIGHT_COLOR", color);
            API.call("GTAOnlineCharacter", "updatePlayerFace", player.handle);
            API.setEntityData(player, "haircolor", color);
            return true;
        }

        // [Command("customize")]
        public void divergeCreator(Client player)
        {
            if ((PedHash)player.getData("Skin") != PedHash.Cow)
            {
                    Cams.interpolateCamera(player, 3, 700, false, true);
                    player.setData("Scount", 0);
                    showPedHeadMenu(player);
            }
            else
            {
                API.sendNativeToPlayer(player, Hash.DO_SCREEN_FADE_IN, 1000);
                API.setEntityPosition(player, new Vector3(-851.6, -124, 38));
                player.freeze(false);
                API.sendChatMessageToPlayer(player, "~b~ Mooo! ~r~ Please choose a GTA:Online style character. Use /creator to return.");
            }
        }
        [Command("creator")]
        public void startCreator(Client player)
        {
            API.setEntityPosition(player, new Vector3(407.458771, -966.489685, -99.0090561));
            API.setEntityRotation(player, new Vector3(0, 0, 140.5));
            Cams.createCameraActive(player, new Vector3(413.93, -976.4, -98.35), new Vector3(0, 0, 179)); //gtam
            Cams.createCameraInactive(player, new Vector3(404.73, -976.4, -98.35), new Vector3(0, 0, 179)); //gtaof
            Cams.createCameraInactive(player, new Vector3(395.6, -976.4, -98.35), new Vector3(0, 0, 179)); //ped
            Cams.createCameraInactive(player, new Vector3(407, -967.22, -98.2), new Vector3(0, 0, -375)); //face
            Cams.createCameraInactive(player, new Vector3(406.33, -968.62, -98.67), new Vector3(0, 0, -41.5)); //clothes
            var dim = DimensionManager.RequestPrivateDimension(player);
            player.setSkin(PedHash.FreemodeMale01);
            player.dimension = dim;
            showCreatorMenu(player);
            player.freeze(true);
        }
        public void showCreatorMenu(Client player)
        {
          //  Cams.setActiveCamera(player, 0);
            object[] argumentList = new object[13];
            argumentList[0] = PED_CREATOR;
            argumentList[1] = "Character Creator";
            argumentList[2] = "Select type to view";
            argumentList[3] = true;
            argumentList[4] = 4;
            argumentList[5] = "Freemode Male";
            argumentList[6] = "Freemode Female";
            argumentList[7] = "Ped Male/Female";
            argumentList[8] = "~g~CONFIRM ~w~and continue";
            for (var i = 0; i < 4; i++)
            {
                argumentList[9 + i] = "";
            }
            API.triggerClientEvent(player, "menu_handler_create_menu2", argumentList);
        }

        public void showPedHeadMenu(Client player)
        {
            object[] argumentList = new object[23];
            argumentList[0] = PED_HEAD;
            argumentList[1] = "Character Creator";
            argumentList[2] = "Select to cycle options";
            argumentList[3] = true;
            argumentList[4] = 9;
            argumentList[5] = "Face (father)";
            argumentList[6] = "Face (mother)";
            argumentList[7] = "Eye color";
            argumentList[8] = "Eyebrows";
            argumentList[9] = "Hair style";
            argumentList[10] = "Hair color";
            argumentList[11] = "Face detail";
            argumentList[12] = "Detail color";
            argumentList[13] = "~g~CONTINUE ~w~ to Clothing";
            for (var i = 0; i < 9; i++)
            {
                argumentList[14 + i] = "";
            }
            API.triggerClientEvent(player, "menu_handler_create_menu2", argumentList);
        }

        [Command("acctest")]
        public void acctest(Client sender, int a)
        {
            for (var i = 0; i <= 9; i++)
            {
                //API.setPlayerAccessory(sender, i, a, 0);
                API.sendNativeToPlayer(sender, Hash.SET_PED_PROP_INDEX, sender.handle, i, a, a);
            }
        }

     
        public static void setSpecialOutfit(Client sender, string filename)
        {
            XElement xelement = XElement.Load("Outfits/Special/" + filename);
            
            for (var i = 0; i <= 9; i++)
            {
                API.shared.clearPlayerAccessory(sender, i);
                var slot = xelement.Element("PedProperties").Element("PedProps").Element("_" + i).Value;
                string[] slotx = slot.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
                var drawable = Convert.ToInt32(slotx[0]);
                var texture = Convert.ToInt32(slotx[1]);
                if ((drawable != -1) && (drawable != 0))
                {
                    API.shared.setPlayerAccessory(sender, i, drawable,texture);
                }
                else
                {
                   
                }
            }
            

            for (var i = 3; i <= 11; i++)
            {
                API.shared.setPlayerClothes(sender, i, 0, 0);
                var slot = xelement.Element("PedProperties").Element("PedComps").Element("_" + i).Value;
                string[] slotx = slot.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
                var drawable = Convert.ToInt32(slotx[0]);
                var texture = Convert.ToInt32(slotx[1]);
                if (drawable != -1)
                {
                    API.shared.setPlayerClothes(sender, i, drawable, texture);
                }
                else
                {
                    API.shared.setPlayerClothes(sender, i, 0, 0);
                }
            }
        }

        public void setOutfit(Client sender, string filename)
        {
            XElement xelement = XElement.Load(filename);

            for (var i = 3; i <= 11; i++)
            {
                API.setPlayerClothes(sender, i, 0, 0);
                var slot = xelement.Element("PedProperties").Element("PedComps").Element("_" + i).Value;
                string[] slotx = slot.Split(new string[] { ",", " " }, StringSplitOptions.RemoveEmptyEntries);
                var drawable = Convert.ToInt32(slotx[0]);
                var texture = Convert.ToInt32(slotx[1]);
                if (drawable != -1)
                {
                    API.setPlayerClothes(sender, i, drawable, texture);
                    sender.setData("draw" + i, drawable);
                    sender.setData("tx" + i, texture);
                }
                else
                {
                    API.setPlayerClothes(sender, i, 0, 0);
                    sender.setData("draw" + i, 0);
                    sender.setData("tx" + i, 0);
                }
            }
        }

        public void LoadClothing(Client player)
        {
            for (int i = 0; i < 12; i++)
            {
                if (player.hasData("draw" + i))
                {
                    API.setPlayerClothes(player, i, player.getData("draw" + i), player.getData("tx" + i));
                }
                if (player.hasData("propdraw" + i))
                {
                 //   API.setPlayerAccessory(player, i, player.getData("propdraw" + i), player.getData("tx" + i));
                }
            }
            if (API.getEntityModel(player) == 1885233650 ||
            API.getEntityModel(player) == -1667301416)
            {
                API.call("GTAOnlineCharacter", "initializePedFace", player.handle);
                player.setSyncedData("GTAO_HAS_CHARACTER_DATA", player.getData("GTAO_HAS_CHARACTER_DATA"));
                player.setSyncedData("GTAO_SHAPE_FIRST_ID", player.getData("GTAO_SHAPE_FIRST_ID"));
                player.setSyncedData("GTAO_SHAPE_SECOND_ID", player.getData("GTAO_SHAPE_SECOND_ID"));
                player.setSyncedData("GTAO_SKIN_FIRST_ID", player.getData("GTAO_SKIN_FIRST_ID"));
                player.setSyncedData("GTAO_SKIN_SECOND_ID", player.getData("GTAO_SKIN_SECOND_ID"));
                player.setSyncedData("GTAO_SHAPE_MIX", player.getData("GTAO_SHAPE_MIX"));
                player.setSyncedData("GTAO_SKIN_MIX", player.getData("GTAO_SKIN_MIX"));
                player.setSyncedData("GTAO_HAIR_COLOR", player.getData("GTAO_HAIR_COLOR"));
                player.setSyncedData("GTAO_HAIR_HIGHLIGHT_COLOR", player.getData("GTAO_HAIR_HIGHLIGHT_COLOR"));
                player.setSyncedData("GTAO_EYE_COLOR", player.getData("GTAO_EYE_COLOR"));
                player.setSyncedData("GTAO_EYEBROWS", player.getData("GTAO_EYEBROWS"));
                player.setSyncedData("GTAO_EYEBROWS_COLOR", player.getData("GTAO_EYEBROWS_COLOR"));
                player.setSyncedData("GTAO_MAKEUP_COLOR", player.getData("GTAO_MAKEUP_COLOR"));
                player.setSyncedData("GTAO_LIPSTICK_COLOR", player.getData("GTAO_LIPSTICK_COLOR"));
                player.setSyncedData("GTAO_EYEBROWS_COLOR2", player.getData("GTAO_EYEBROWS_COLOR2"));
                player.setSyncedData("GTAO_MAKEUP_COLOR2", player.getData("GTAO_MAKEUP_COLOR2"));
                player.setSyncedData("GTAO_LIPSTICK_COLOR2", player.getData("GTAO_LIPSTICK_COLOR2"));
                API.call("GTAOnlineCharacter", "updatePlayerFace", player.handle);
            }
        }
    }

}
