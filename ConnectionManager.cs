using GTANetworkServer;
using GTANetworkShared;

namespace IcaroRPG
{
    public class ConnectionManager : Script
    {
        private readonly Vector3 _skinSelectorPos = new Vector3(228.135f, -995.350f, -99.000f);
        private readonly Vector3 _skinSelectorCamPos = new Vector3(226.935f, -994.550f, -98.00f);

        public ConnectionManager()
        {
            API.onPlayerConnected += onPlayerConnected;
            API.onPlayerFinishedDownload += onPlayerDownloaded;
            API.onPlayerDisconnected += onPlayerLeave;
            API.onChatCommand += onPlayerCommand;
            API.onClientEventTrigger += ScriptEvent;
        }

        public void onPlayerCommand(Client player, string arg, CancelEventArgs ce)
        {
            if (API.getEntityData(player, "DOWNLOAD_FINISHED") != true)
            {
                ce.Cancel = true;
            }
        }

        public void onPlayerConnected(Client player)
        {
        }

        public void ScriptEvent(Client sender, string eventName, object[] args)
        {
            /*
            if (eventName == "menu_handler_user_input")
            {
                if (args[1] != null)
                {
                    var icallback = (int)args[0];
                    var msg = (string)args[1];
                    if (icallback == 6)
                    {
                        if (!Database.TryLoginPlayer(sender, msg))
                        {
                            API.shared.triggerClientEvent(sender, "get_user_input", 6, "<password>", 20, null);
                            API.triggerClientEvent(sender, "display_subtitle", "~r~ERROR:~w~ Wrong password.", 3000);
                            API.sendChatMessageToPlayer(sender, "~r~ERROR:~w~ Wrong password.");
                        }
                        else
                        {
                            Database.LoadPlayerAccount(sender);
                            API.sendChatMessageToPlayer(sender, "~g~Logged in successfully!");

                            API.call("SpawnManager", "SpawnCitizen", sender);

                            int money = API.getEntityData(sender, "Money");
                            API.triggerClientEvent(sender, "update_money_display", money);
                        }
                    }
                }
            }
            */
            if (eventName == "CefPassword")
            {
                if (args.Length != 0)
                {
                    var msg = (string)args[0];
                    if (!Database.TryLoginPlayer(sender, msg))
                    {
                        if (Database.DoesAccountExist(sender.socialClubName))
                        {
                            API.triggerClientEvent(sender, "showLogin");
                            API.triggerClientEvent(sender, "display_subtitle", "~r~ERROR:~w~ Wrong password.", 3000);
                            API.sendChatMessageToPlayer(sender, "~r~ERROR:~w~ Wrong password.");
                            return;
                        }
                        API.sendNativeToPlayer(sender, Hash.DO_SCREEN_FADE_IN, 4000);
                        Database.CreatePlayerAccount(sender, msg);
                        API.sendChatMessageToPlayer(sender, "~g~Account created!");
                        Database.LoadPlayerAccount(sender);
                        API.sendChatMessageToPlayer(sender, "~g~Logged in successfully!");
                        API.call("SpawnManager", "SpawnCitizen", sender);
                        int money = API.getEntityData(sender, "Money");
                        API.triggerClientEvent(sender, "update_money_display", money);
                        API.call("CharacterCreator", "startCreator", sender);
                    }
                    else
                    {
                        API.sendNativeToPlayer(sender, Hash.DO_SCREEN_FADE_IN, 4000);
                        Database.LoadPlayerAccount(sender);
                        API.sendChatMessageToPlayer(sender, "~g~Logged in successfully!");
                        API.call("SpawnManager", "SpawnCitizen", sender);
                        int money = API.getEntityData(sender, "Money");
                        API.triggerClientEvent(sender, "update_money_display", money);
                    }
                }
                else
                {
                    API.triggerClientEvent(sender, "showLogin");
                }
            }
        }

        public void onPlayerDownloaded(Client player)
        {
            API.setEntityData(player, "DOWNLOAD_FINISHED", true);
            API.setEntityPosition(player, new Vector3(228.135f, -995.350f, -99.000f));
            API.sendChatMessageToPlayer(player, "~r~COMMANDS:~g~ /register <password> and /login <password>");
            if (!Database.DoesAccountExist(player.socialClubName))
            {
                API.triggerClientEvent(player, "display_subtitle", "~g~use /register password then /login password", 10000);
            }
            else
            {
                /*
                var icallback = 6;
                var showtext = "<password>";
                var maxlen = 20;
                API.shared.triggerClientEvent(player, "get_user_input", icallback, showtext, maxlen, null);
                */
            }
          //  API.triggerClientEvent(player, "createCamera", _skinSelectorCamPos, _skinSelectorPos);
        }

        public static void Leave(Client player)
        {
            if (Database.IsPlayerLoggedIn(player))
            {

                API.shared.setEntityData(player, "Skin", player.model);
                API.shared.setEntityData(player, "Health", player.health);
                API.shared.setEntityData(player, "Armor", player.armor);
                Vector3 pos = API.shared.getEntityPosition(player);
                API.shared.setEntityData(player, "PX", (int)pos.X);
                API.shared.setEntityData(player, "PY", (int)pos.Y);
                API.shared.setEntityData(player, "PZ", (int)pos.Z);
                Items.save(player);
                Database.SavePlayerAccount(player);
            }
        }

        public void onPlayerLeave(Client player, string reason)
        {
            //  API.call("LoginManager", "LogOut", player);

            Leave(player);
        }
    }
}