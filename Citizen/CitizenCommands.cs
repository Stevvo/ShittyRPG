using System.Collections.Generic;
using GTANetworkServer;
using GTANetworkShared;
using IcaroRPG.Cops;

namespace IcaroRPG.Citizen
{
    public class CitizenCommands : Script
    {

    public List<KeyValuePair<string, string>> AnimationList = new List<KeyValuePair<string, string>>()
    {
        {new KeyValuePair<string,string>("finger", "mp_player_intfinger mp_player_int_finger")},
        {new KeyValuePair<string,string>("guitar", "anim@mp_player_intcelebrationmale@air_guitar air_guitar")},
        {new KeyValuePair<string,string>("shagging", "anim@mp_player_intcelebrationmale@air_shagging air_shagging")},
        {new KeyValuePair<string,string>("synth", "anim@mp_player_intcelebrationmale@air_synth air_synth")},
        {new KeyValuePair<string,string>("kiss", "anim@mp_player_intcelebrationmale@blow_kiss blow_kiss")},
        {new KeyValuePair<string,string>("bro", "anim@mp_player_intcelebrationmale@bro_love bro_love")},
        {new KeyValuePair<string,string>("chicken", "anim@mp_player_intcelebrationmale@chicken_taunt chicken_taunt")},
        {new KeyValuePair<string,string>("chin", "anim@mp_player_intcelebrationmale@chin_brush chin_brush")},
        {new KeyValuePair<string,string>("dj", "anim@mp_player_intcelebrationmale@dj dj")},
        {new KeyValuePair<string,string>("dock", "anim@mp_player_intcelebrationmale@dock dock")},
        {new KeyValuePair<string,string>("facepalm", "anim@mp_player_intcelebrationmale@face_palm face_palm")},
        {new KeyValuePair<string,string>("fingerkiss", "anim@mp_player_intcelebrationmale@finger_kiss finger_kiss")},
        {new KeyValuePair<string,string>("freakout", "anim@mp_player_intcelebrationmale@freakout freakout")},
        {new KeyValuePair<string,string>("jazzhands", "anim@mp_player_intcelebrationmale@jazz_hands jazz_hands")},
        {new KeyValuePair<string,string>("knuckle", "anim@mp_player_intcelebrationmale@knuckle_crunch knuckle_crunch")},
        {new KeyValuePair<string,string>("nose", "anim@mp_player_intcelebrationmale@nose_pick nose_pick")},
        {new KeyValuePair<string,string>("no", "anim@mp_player_intcelebrationmale@no_way no_way")},
        {new KeyValuePair<string,string>("peace", "anim@mp_player_intcelebrationmale@peace peace")},
        {new KeyValuePair<string,string>("photo", "anim@mp_player_intcelebrationmale@photography photography")},
        {new KeyValuePair<string,string>("rock", "anim@mp_player_intcelebrationmale@rock rock")},
        {new KeyValuePair<string,string>("salute", "anim@mp_player_intcelebrationmale@salute salute")},
        {new KeyValuePair<string,string>("shush", "anim@mp_player_intcelebrationmale@shush shush")},
        {new KeyValuePair<string,string>("slowclap", "anim@mp_player_intcelebrationmale@slow_clap slow_clap")},
        {new KeyValuePair<string,string>("surrender", "anim@mp_player_intcelebrationmale@surrender surrender")},
        {new KeyValuePair<string,string>("thumbs", "anim@mp_player_intcelebrationmale@thumbs_up thumbs_up")},
        {new KeyValuePair<string,string>("taunt", "anim@mp_player_intcelebrationmale@thumb_on_ears thumb_on_ears")},
        {new KeyValuePair<string,string>("vsign", "anim@mp_player_intcelebrationmale@v_sign v_sign")},
        {new KeyValuePair<string,string>("wank", "anim@mp_player_intcelebrationmale@wank wank")},
        {new KeyValuePair<string,string>("wave", "anim@mp_player_intcelebrationmale@wave wave")},
        {new KeyValuePair<string,string>("loco", "anim@mp_player_intcelebrationmale@you_loco you_loco")},
        {new KeyValuePair<string,string>("handsup", "missminuteman_1ig_2 handsup_base")},
    };


        public const int CHAR_ROOT = 15;
        public const int CHAR_PAY = 16;
        public const int CHAR_MESSENGER = 17;
        public const int CHAR_ANIM = 20;


        public CitizenCommands()
        {
            API.onClientEventTrigger += ScriptEvent;
        }

        public void ScriptEvent(Client sender, string eventName, object[] args)
        {
            if (eventName == "InventoryKeyPressed")
            {
                if (Database.IsPlayerLoggedIn(sender))
                {
                    object[] argumentList = new object[17];
                    argumentList[0] = CHAR_ROOT;
                    argumentList[1] = sender.name;
                    argumentList[2] = "~b~Level: " + "~r~" + sender.getData("Level");
                    argumentList[3] = false;
                    argumentList[4] = 6;
                    argumentList[5] = "Inventory";
                    argumentList[6] = "Give Cash";
                    argumentList[7] = "Messenger";
                    argumentList[8] = "Call 911";
                    argumentList[9] = "Animate Character";
                    argumentList[10] = "~y~ SWITCH TEAM";
                    for (var i = 0; i < 6; i++)
                    {
                        argumentList[11 + i] = "";
                    }
                    API.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                }
            }
            else if (eventName == "menu_handler_select_item")
            {
                var callbackId = (int)args[0];
                var index = (int)args[1];
                if (callbackId == CHAR_ROOT)
                {
                    if (index == 0)
                    {
                        if (API.hasEntityData(sender, "InventoryHolder") && !API.getEntityData(sender, "Jailed"))
                        {
                            Items.InventoryHolder ih = API.getEntityData(sender, "InventoryHolder");
                            var itemsLen = ih.Inventory.Count;
                            object[] argumentList = new object[5 + itemsLen * 2];
                            argumentList[0] = 1;
                            argumentList[1] = "Inventory";
                            argumentList[2] = "Select Item for Details";
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
                    else if (index == 1)
                    {
                        var nearbylist = API.getPlayersInRadiusOfPlayer(10, sender);
                        nearbylist.Remove(sender);
                        var itemsLen = nearbylist.Count;
                        object[] argumentList = new object[5 + itemsLen + itemsLen];
                        argumentList[0] = CHAR_PAY;
                        argumentList[1] = "Give cash";
                        argumentList[2] = "Select nearby ~g~player:";
                        argumentList[3] = false;
                        argumentList[4] = itemsLen;
                        var i = 0;
                        foreach (Client player in nearbylist)
                        {
                            argumentList[5 + i] = player.name;
                            argumentList[5 + itemsLen + i] = "";
                            i++;
                        }
                        API.shared.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                    }
                    else if (index == 2)
                    {
                        var list = API.getAllPlayers();
                        API.setEntityData(sender, "list", list);
                        list.Remove(sender);
                        var itemsLen = list.Count;
                        object[] argumentList = new object[5 + itemsLen + itemsLen];
                        argumentList[0] = CHAR_MESSENGER;
                        argumentList[1] = "Message";
                        argumentList[2] = "Select ~g~player:";
                        argumentList[3] = false;
                        argumentList[4] = itemsLen;
                       // var i = 0;

                        for(var i = 0; i < list.Count; i++)
                        {
                            argumentList[5 + i] = list[i].name;
                            argumentList[5 + itemsLen + i] = "";
                        }
                        API.shared.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                    }
                    else if (index == 3)
                    {
                        API.shared.triggerClientEvent(sender, "get_user_input", 1, "911 Emergency, how can I help?", 144, null);
                    }
                    else if (index == 4)
                    {
                        var itemsLen = AnimationList.Count;
                        object[] argumentList = new object[5 + itemsLen * 2];
                        argumentList[0] = CHAR_ANIM;
                        argumentList[1] = "Animate";
                        argumentList[2] = "Select Animation";
                        argumentList[3] = false;
                        argumentList[4] = itemsLen;
                        var i = 0;
                        foreach (KeyValuePair<string, string> anim in AnimationList)
                        {
                            argumentList[5 + i] = anim.Key;
                            argumentList[5 + itemsLen + i] = "";
                            i++;
                        }
                        API.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                    }
                    else if (index == 5)
                    {
                        if (API.shared.getEntityData(sender, "Crimes") == null && API.getEntityData(sender, "Jailed") == false)
                        {
                            if (sender.getData("IS_COP") == false)
                            {
                                if (sender.model == (int)PedHash.FreemodeMale01)
                                {
                                    CharacterCreator.setSpecialOutfit(sender, "MPol1.xml");
                                }
                                if (sender.model == (int)PedHash.FreemodeFemale01)
                                {
                                    CharacterCreator.setSpecialOutfit(sender, "FPol1.xml");
                                }
                                API.shared.setPlayerNametagColor(sender, 55, 135, 240);
                                API.shared.setEntityData(sender, "IS_COP", true);
                                API.shared.setEntityData(sender, "IS_CROOK", false);
                                API.shared.setEntityPosition(sender, new Vector3(447.1f, -984.21f, 30.69f));
                                API.shared.givePlayerWeapon(sender, WeaponHash.Nightstick, 1, false, true);
                                API.shared.givePlayerWeapon(sender, WeaponHash.StunGun, 500, true, true);
                                API.shared.triggerClientEvent(sender, "display_subtitle", "~r~F1 Player Menu ~g~F2 Vehicle Menu ~b~F3 COP Menu", 6000);
                                return;
                            }
                            else
                            {
                                API.shared.removeAllPlayerWeapons(sender);
                                API.shared.setEntityData(sender, "IS_COP", false);
                                API.shared.setEntityPosition(sender, new Vector3(-851.622314, -124.067039, 37.6538773));
                                API.shared.triggerClientEvent(sender, "display_subtitle", "~r~F1 Player Menu ~g~F2 Vehicle Menu", 6000);
                                for (var i = 3; i <= 11; i++)
                                {
                                    var drawable = sender.getData("draw" + i);
                                    var texture = sender.getData("tx" + i);
                                    if (drawable != -1)
                                    {
                                        API.setPlayerClothes(sender, i, drawable, texture);
                                    }
                                    else
                                    {
                                        API.setPlayerClothes(sender, i, 0, 0);
                                    }
                                }
                                for (var i = 0; i <= 9; i++)
                                {
                                    API.clearPlayerAccessory(sender, i);
                                }
                                    return;
                 
                            }
                        }
                        API.shared.triggerClientEvent(sender, "show_subtitle", "The Police won't take you!", 5000);
                    }
                }
                else if (callbackId == CHAR_MESSENGER)
                {
                    API.shared.triggerClientEvent(sender, "get_user_input", 2, "", 144, index);
                }
                else if (callbackId == CHAR_ANIM)
                {
                    API.playPlayerAnimation(sender, 0, AnimationList[index].Value.Split()[0], AnimationList[index].Value.Split()[1]);
                }
            }
            else if (eventName == "menu_handler_user_input")
            {
                if (args[1] != null)
                {
                    var icallback = (int)args[0];
                    var msg = (string)args[1];
                    if (icallback == 1)
                    {
                        var players = API.getAllPlayers();

                        foreach (var player in players)
                        {
                            if (API.getEntityData(player, "IS_COP") == true)
                            {
                                API.shared.triggerClientEvent(player, "display_subtitle", "~r~~h~[911 CALL] from " + sender.name + "~h~~y~ : " + msg + "\n ~w~ Waypoint set!", 13000);
                                float posX = sender.position.X;
                                float posY = sender.position.Y;
                                API.sendNativeToPlayer(player, Hash.SET_NEW_WAYPOINT, posX, posY);
                            }
                        }
                    }
                    if(icallback ==2)
                    {
                        List<Client> list = API.getEntityData(sender, "list");
                        int index = (int)args[2];
                        API.sendPictureNotificationToPlayer(list[index], msg, "CHAR_FACEBOOK", 0, 0, "Messenger", "From: " + sender.name);
                        API.resetEntityData(sender, "list");
                    }
                }
            }
        }

        [Command("acceptfine", Group = "Citizen Commands")]
        public void AcceptCopFine(Client sender)
        {
            var ticketOffered = API.getEntityData(sender, "FINE_OFFERED");
            var cop = API.getEntityData(sender, "FINE_OFFERED_BY");

            API.resetEntityData(sender, "FINE_OFFERED");
            API.resetEntityData(sender, "FINE_OFFERED_BY");

            if (API.getEntityData(sender, "WantedLevel") == 0 ||
                API.getEntityData(sender, "WantedLevel") > 2)
            {
                API.sendNotificationToPlayer(sender, "~r~You cant accept the ticket with your wanted level!");
                return;
            }

            if (ticketOffered != true)
            {
                API.sendNotificationToPlayer(sender, "~r~Nobody offered you to pay your fine!");
                return;
            }

            if (cop == null || !API.isPlayerConnected(cop) || API.getEntityPosition(cop).DistanceToSquared(API.getEntityPosition(sender)) > 100f)
            {
                API.sendNotificationToPlayer(sender, "~r~The cop has left!");
                return;
            }

            List<int> crimes = API.getEntityData(sender, "Crimes");
            int totalPrice = 0;

            foreach (var crime in crimes)
            {
                var crimeData = WantedLevelDataProvider.Crimes.Get(crime);

                totalPrice += crimeData.TicketCost;
            }

            var playerMoney = API.getEntityData(sender, "Money");

            if (playerMoney >= totalPrice)
            {
                API.setEntityData(sender, "Money", playerMoney - totalPrice);

                API.triggerClientEvent(sender, "update_money_display", API.getEntityData(sender, "Money"));

                API.sendNotificationToPlayer(sender, "You have paid your fine!");
                API.sendNotificationToPlayer(cop, sender.name + " has paid his fine!");

                API.setEntityData(sender, "WantedLevel", 0);
                API.resetEntityData(sender, "Crimes");
                API.setPlayerWantedLevel(sender, 0);
            }
            else
            {
                API.sendNotificationToPlayer(sender, "~r~You dont have enough money!");
            }
        }

        [Command("payfine", Group = "Citizen Commands")]
        public void PayOwnFine(Client sender)
        {
            if (API.getEntityData(sender, "IS_COP") == true)
            {
                API.sendNotificationToPlayer(sender, "~r~ERROR: You're a cop, you can't be fined!");
                return;
            }

            if (!(bool) API.call("PoliceStation", "IsInPoliceStation", (NetHandle)sender.handle))
            {
                API.sendNotificationToPlayer(sender, "~r~ERROR: You're not in a police station!");
                return;
            }

            if (API.getEntityData(sender, "WantedLevel") == 0 ||
                API.getEntityData(sender, "WantedLevel") > 2)
            {
                API.sendNotificationToPlayer(sender, "~r~You can't pay your fine with that wanted level!");
                return;
            }

            
            List<int> crimes = API.getEntityData(sender, "Crimes");
            int totalPrice = 0;

            foreach (var crime in crimes)
            {
                var crimeData = WantedLevelDataProvider.Crimes.Get(crime);

                totalPrice += crimeData.TicketCost;
            }

            var playerMoney = API.getEntityData(sender, "Money");

            if (playerMoney >= totalPrice)
            {
                API.setEntityData(sender, "Money", playerMoney - totalPrice);

                API.triggerClientEvent(sender, "update_money_display", API.getEntityData(sender, "Money"));

                API.sendNotificationToPlayer(sender, "You have paid your fine!");

                API.setEntityData(sender, "WantedLevel", 0);
                API.resetEntityData(sender, "Crimes");
                API.setPlayerWantedLevel(sender, 0);
            }
            else
            {
                API.sendNotificationToPlayer(sender, "~r~You dont have enough money!");
            }
        }

    }
}