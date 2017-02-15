using System;
using System.Collections.Generic;
using System.Linq;
using GTANetworkServer;
using GTANetworkShared;

namespace IcaroRPG.Cops
{
    public class CopCommands : Script
    {
        public const int COP_ROOT = 9;
        public const int COP_REPORTCRIME = 10;
        public const int COP_REPORTCRIMINAL = 11;
        public const int COP_TICKETCRIMINAL = 12;
        public const int COP_ARRESTCRIMINAL = 13;
        public const int COP_CUFFCRIMINAL = 14;

        public CopCommands()
        {
            API.onClientEventTrigger += ScriptEvent;
        }
        public void ScriptEvent(Client sender, string eventName, object[] args)
        {
            if(eventName == "cuffUpdate")
            {
                var player = API.getPlayerFromHandle((NetHandle)args[0]);
                API.shared.playPlayerAnimation(player , 49, "mp_arresting", "idle");
            }
            if (eventName == "GroupMenuKeyPressed")
            {
                if (Database.IsPlayerLoggedIn(sender))
                {
                    if (API.getEntityData(sender, "IS_COP"))
                    {
                        object[] argumentList = new object[15];
                        argumentList[0] = COP_ROOT;
                        argumentList[1] = "Police";
                        argumentList[2] = null;
                        argumentList[3] = false;
                        argumentList[4] = 4;
                        argumentList[5] = "Report";
                        argumentList[6] = "Fine";
                        argumentList[7] = "Arrest";
                        argumentList[8] = "Cuff/Uncuff";
                        for (var i = 0; i < 4; i++)
                        {
                            argumentList[9 + i] = "";
                        }
                        API.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                    }

                }
            }

            else if (eventName == "menu_handler_select_item")
            {
                var callbackId = (int)args[0];
                var index = (int)args[1];
                if (callbackId == COP_ROOT)
                {
                    if (index == 0)
                    {
                        var itemsLen = WantedLevelDataProvider.Crimes.Count;
                        object[] argumentList = new object[5 + itemsLen + itemsLen];
                        argumentList[0] = COP_REPORTCRIME;
                        argumentList[1] = "Report";
                        argumentList[2] = "Select crime:";
                        argumentList[3] = false;
                        argumentList[4] = itemsLen;
                        var i = 0;
                        foreach (KeyValuePair<int, CrimeData> entry in WantedLevelDataProvider.Crimes)
                        {
                            argumentList[5 + i] = entry.Value.Name;
                            if (entry.Value.TicketCost != 0)
                            {
                                argumentList[5 + itemsLen + i] = "~b~Ticket, $" + entry.Value.TicketCost;
                            }
                            else
                            {
                                argumentList[5 + itemsLen + i] = "~r~Arrest";
                            }
                            i++;
                        }
                        API.shared.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                    }
                    else if (index == 1)
                    {
                        var nearbylist = API.getPlayersInRadiusOfPlayer(20, sender);
                        nearbylist.Remove(sender);
                        API.setEntityData(sender, "NearbyList", nearbylist);
                        var itemsLen = nearbylist.Count;
                        object[] argumentList = new object[5 + itemsLen + itemsLen];
                        argumentList[0] = COP_TICKETCRIMINAL;
                        argumentList[1] = "Fine";
                        argumentList[2] = "Select ~r~suspect:";
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
                        var nearbylist = API.getPlayersInRadiusOfPlayer(5, sender);
                        nearbylist.Remove(sender);
                        API.setEntityData(sender, "NearbyList", nearbylist);
                        var itemsLen = nearbylist.Count;
                        API.shared.consoleOutput(itemsLen.ToString());
                        object[] argumentList = new object[5 + itemsLen + itemsLen];
                        argumentList[0] = COP_ARRESTCRIMINAL;
                        argumentList[1] = "Arrest";
                        argumentList[2] = "Select ~r~suspect:";
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
                    else if (index == 3)
                    {
                        var nearbylist = API.getPlayersInRadiusOfPlayer(5, sender);
                        nearbylist.Remove(sender);
                        API.setEntityData(sender, "NearbyList", nearbylist);
                        var itemsLen = nearbylist.Count;
                        API.shared.consoleOutput(itemsLen.ToString());
                        object[] argumentList = new object[5 + itemsLen + itemsLen];
                        argumentList[0] = COP_CUFFCRIMINAL;
                        argumentList[1] = "Cuff";
                        argumentList[2] = "Select ~r~suspect:";
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
                }
                else if (callbackId == COP_TICKETCRIMINAL)
                {
                    var nearbylist = API.getEntityData(sender, "NearbyList");
                    TicketPlayer(sender, nearbylist[index]);
                    API.resetEntityData(sender, "NearbyList");
                }
                else if (callbackId == COP_ARRESTCRIMINAL)
                {
                    var nearbylist = API.getEntityData(sender, "NearbyList");
                    ArrestPlayer(sender, nearbylist[index]);
                    API.resetEntityData(sender, "NearbyList");
                }
                else if (callbackId == COP_CUFFCRIMINAL)
                {
                    var nearbylist = API.getEntityData(sender, "NearbyList");
                    if (API.hasEntityData(nearbylist[index], "Cuffs"))
                    {
                        CopUtil.UnCuff(nearbylist[index]);
                    }
                    else
                    {
                        CopUtil.Cuff(nearbylist[index]);
                        API.resetEntityData(sender, "NearbyList");
                    }
                }
                else if (callbackId == COP_REPORTCRIMINAL)
                {
                    var nearbylist = API.getEntityData(sender, "NearbyList");
                    var item = API.getEntityData(sender, "LastSelectedItem");
                    CopUtil.ReportPlayer(nearbylist[index], item);
                    API.resetEntityData(sender, "NearbyList");
                }
                else if (callbackId == COP_REPORTCRIME)
                {
                    API.setEntityData(sender, "LastSelectedItem", index);
                    if(API.getPlayersInRadiusOfPlayer(50, sender) == null) return;
                    var nearbylist = API.getPlayersInRadiusOfPlayer(50, sender);
                    nearbylist.Remove(sender);
                    API.setEntityData(sender, "NearbyList", nearbylist);
                    var itemsLen = nearbylist.Count;
                    API.shared.consoleOutput(itemsLen.ToString());
                    object[] argumentList = new object[5 + itemsLen + itemsLen];
                    argumentList[0] = COP_REPORTCRIMINAL;
                    argumentList[1] = "Report";
                    argumentList[2] = "Select ~r~suspect:";
                    argumentList[3] = false;
                    argumentList[4] = itemsLen;
                    for(var i = 0; i < nearbylist.Count; i++)
                    {
                        argumentList[5 + i] = nearbylist[i].name;
                        argumentList[5 + itemsLen + i] = "";
                    }
                    API.shared.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                }
           }
        }


        public void ArrestPlayer(Client sender, Client target)
        {
            if (API.getEntityData(sender, "IS_COP") != true)
            {
                API.sendChatMessageToPlayer(sender, "~r~ERROR: You're not a cop!");
                return;
            }

            if (target == sender)
            {
                API.sendChatMessageToPlayer(sender, "~r~You cant arrest yourself!");
                return;
            }

            if (API.getEntityData(target, "IS_COP") == true)
            {
                API.sendChatMessageToPlayer(sender, "~r~You cant arrest a cop!");
                return;
            }

            if (API.getEntityPosition(sender).DistanceToSquared(API.getEntityPosition(target)) > 16f)
            {
                API.sendChatMessageToPlayer(sender, "~r~You're too far!");
                return;
            }

            if (API.getEntityData(target, "WantedLevel") == null ||
                API.getEntityData(target, "WantedLevel") <= 2)
            {
                API.sendChatMessageToPlayer(sender, "~r~The player doesn't have an arrest warrant!");
                return;
            }

            API.sendChatMessageToPlayer(sender, "~g~You have arrested " + target.name + "!");
            API.sendChatMessageToPlayer(target, "~g~You have been arrested by " + sender.name + "!");
            API.call("JailController", "jailPlayer", target,
                WantedLevelDataProvider.GetTimeFromWantedLevel(API.getEntityData(target, "WantedLevel")));

            CopUtil.BroadcastToCops("~b~Player ~h~" + target.name + "~h~ has been arrested!");
        }

        [Command("wanted", Group = "Cop Commands")]
        public void GetAllWantedPlayers(Client sender)
        {
            if (API.getEntityData(sender, "IS_COP") != true)
            {
                API.sendChatMessageToPlayer(sender, "~r~ERROR: You're not a cop!");
                return;
            }

            var players = API.getAllPlayers();

            API.sendChatMessageToPlayer(sender, "_________WANTED________");

            foreach (var player in players)
            {
                if (API.getEntityData(player, "LOGGED_IN") != true || API.getEntityData(player, "IS_COP") == true || API.getEntityData(player, "WantedLevel") <= 2) continue;

                var crimes = (List<int>)API.getEntityData(player, "Crimes");

                string crimeList = string.Join(", ", crimes.Select(i => WantedLevelDataProvider.Crimes.Get(i).Name));

                API.sendChatMessageToPlayer(sender,
                    string.Format("~b~{0}~w~ ~h~{1}~h~ -- {2}",
                    new String('*', API.getEntityData(player, "WantedLevel")),
                    player.name,
                    crimeList
                    ));
            }
        }

        
        [Command("report", Alias = "re", Group = "Cop Commands")]
        public void ReportPlayer(Client sender, Client criminal, int crimeId)
        {
            
            if (API.getEntityData(sender, "IS_COP") != true)
            {
                API.sendChatMessageToPlayer(sender, "~r~ERROR: You're not a cop!");
                return;
            }

            if (criminal == sender)
            {
                API.sendChatMessageToPlayer(sender, "~r~You cant report yourself!");
                return;
            }

            if (API.getEntityData(criminal, "IS_COP") == true)
            {
                API.sendChatMessageToPlayer(sender, "~r~You cant report a cop!");
                return;
            }

            if (!WantedLevelDataProvider.Crimes.ContainsKey(crimeId))
            {
                API.sendChatMessageToPlayer(sender, "~r~No such crime exists. Use /crimelist for a full list of crime IDs.");
                return;
            }

            CopUtil.ReportPlayer(criminal, crimeId);
            
        }

        [Command("crimelist", Alias = "cl", Group = "Cop Commands")]
        public void CrimeList(Client sender)
        {
            int count = 0;
            string accumulator = "";

            API.sendChatMessageToPlayer(sender, "_____CRIMES_____");

            foreach (
                var crime in
                    WantedLevelDataProvider.Crimes.Select(pair => string.Format("{0}: {1}", pair.Key, pair.Value.Name)))
            {
                accumulator += crime + ", ";

                if (++count%6 == 0)
                {
                    API.sendChatMessageToPlayer(sender, accumulator);
                    accumulator = "";
                }
            }
        }

        public void TicketPlayer(Client sender, Client criminal)
        {
            if (API.getEntityData(sender, "IS_COP") != true)
            {
                API.sendChatMessageToPlayer(sender, "~r~ERROR: You're not a cop!");
                return;
            }

            if (criminal == sender)
            {
                API.sendChatMessageToPlayer(sender, "~r~You cant ticket yourself!");
                return;
            }

            if (API.getEntityData(criminal, "WantedLevel") == 0 ||
                API.getEntityData(criminal, "WantedLevel") > 2)
            {
                API.sendChatMessageToPlayer(sender, "~r~Arrest Them!");
                return;
            }

            if (API.getEntityPosition(sender).DistanceToSquared(API.getEntityPosition(criminal)) > 25f)
            {
                API.sendChatMessageToPlayer(sender, "~r~You're too far!");
                return;
            }

            List<int> crimes = API.getEntityData(criminal, "Crimes");
            int totalPrice = 0;

            foreach (var crime in crimes)
            {
                var crimeData = WantedLevelDataProvider.Crimes.Get(crime);

                totalPrice += crimeData.TicketCost;
            }

            if (API.getEntityData(criminal, "Money") >= totalPrice)
            {
                API.sendChatMessageToPlayer(criminal, "~b~" + sender.name + "~w~ has fined you for $" + totalPrice + ". Type /acceptfine to pay the fine.");
                API.sendChatMessageToPlayer(sender, "You offered " + criminal.name + " to pay his fine.");

                API.setEntityData(criminal, "FINE_OFFERED", true);
                API.setEntityData(criminal, "FINE_OFFERED_BY", sender);
            }
            else
            {
                API.sendChatMessageToPlayer(sender, "~r~The player can't pay their fine!");
            }
        }

        [Command("faction", Alias = "f,d", Group = "Cop Commands", GreedyArg = true)]
        public void BroadcastToOtherCops(Client sender, string text)
        {
            if (API.getEntityData(sender, "IS_COP") != true)
            {
                API.sendChatMessageToPlayer(sender, "~r~ERROR: You're not a cop!");
                return;
            }

            CopUtil.BroadcastToCops("~b~[RADIO] ~h~" + sender.name + "~h~~w~: " + text);
        }

        [Command("jaillist", Group = "Cop Commands")]
        public void GetAllPlayersInJail(Client sender)
        {
            if (API.getEntityData(sender, "IS_COP") != true)
            {
                API.sendChatMessageToPlayer(sender, "~r~ERROR: You're not a cop!");
                return;
            }

            var players = API.getAllPlayers();

            int count = 0;
            string accumulator = "";


            API.sendChatMessageToPlayer(sender, "_________JAIL________");

            foreach (var player in players)
            {
                if (API.getEntityData(player, "LOGGED_IN") != true || API.getEntityData(player, "JAILED") != true) continue;

                accumulator += string.Format("{0} ({1}s), ", player.name, API.TickCount - JailController.JailTimes.Get(player));

                if (++count % 6 == 0)
                {
                    API.sendChatMessageToPlayer(sender, accumulator);
                    accumulator = "";
                }
            }
        }


    }
}