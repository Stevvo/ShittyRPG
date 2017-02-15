using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;


namespace IcaroRPG
{
    public class LoginManager : Script
    {
        public LoginManager()
        {
            Database.Init();

            API.onResourceStop += onResourceStop;
        }

        [Command("skin2")]
        public void Skin(Client sender)
        {
            API.call("SpawnManager", "CreateSkinSelection", sender);
        }

        [Command("login")]
        public void login(Client sender, string msg)
        {
            if (!Database.IsPlayerLoggedIn(sender))
            {
                if (!Database.TryLoginPlayer(sender, msg))
                {
                    if (Database.DoesAccountExist(sender.socialClubName))
                    {
                        API.triggerClientEvent(sender, "showLogin");
                        API.triggerClientEvent(sender, "display_subtitle", "~r~ERROR:~w~ Wrong password.", 3000);
                        API.sendChatMessageToPlayer(sender, "~r~ERROR:~w~ Wrong password.");
                        return;
                    }
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
    }

        [Command]
        public void Register(Client sender, string password)
        {
            if (Database.IsPlayerLoggedIn(sender))
            {
                API.sendChatMessageToPlayer(sender, "~r~ERROR: ~w~You're already logged in!");
                return;
            }

            if (Database.DoesAccountExist(sender.socialClubName))
            {
                API.sendChatMessageToPlayer(sender, "~r~ERROR: ~w~An account linked to this Social Club handle already exists!");
                return;
            }

            Database.CreatePlayerAccount(sender, password);
            Database.LoadPlayerAccount(sender);
            API.sendChatMessageToPlayer(sender, "~g~Logged in successfully!");
           // API.call("SpawnManager", "SpawnCitizen", sender);
            int money = API.getEntityData(sender, "Money");
            API.triggerClientEvent(sender, "update_money_display", money);
            API.call("CharacterCreator", "startCreator", sender);
        }

        public void onResourceStop()
        {
            foreach (var client in API.getAllPlayers())
            {
                if (Database.IsPlayerLoggedIn(client))
                {
                    ConnectionManager.Leave(client);
                   // Database.SavePlayerAccount(client);
                }
                foreach (var data in API.getAllEntityData(client))
                {
                   API.resetEntityData(client, data);
                }
            }
        }
    }
}