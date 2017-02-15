using System;
using System.Collections.Generic;
using GTANetworkServer;
using GTANetworkShared;

namespace IcaroRPG.Cops
{
    public static class CopUtil
    {

        public static bool ReportPlayer(Client player, int crimeId)
        {
            var crimeWL = WantedLevelDataProvider.Crimes.Get(crimeId);

            if (API.shared.getEntityData(player, "WantedLevel") >= crimeWL.WantedLevel)
            {
                return false;
            }

            API.shared.setEntityData(player, "WantedLevel", crimeWL.WantedLevel);

            API.shared.sendChatMessageToPlayer(player, "~y~You have been reported for " + WantedLevelDataProvider.Crimes.Get(crimeId).Name);

            if (crimeWL.WantedLevel <= 2)
            {
                BroadcastToCops("~b~TICKET ISSUED FOR ~w~" + player.name + " ~b~FOR~w~ " + WantedLevelDataProvider.Crimes.Get(crimeId).Name);
            }
            else
            {
                BroadcastToCops("~b~ARREST WARRANT ISSUED FOR ~w~" + player.name + " ~b~FOR~w~ " + WantedLevelDataProvider.Crimes.Get(crimeId).Name);
            }

            List<int> playerCrimes;

            if ((playerCrimes = API.shared.getEntityData(player, "Crimes")) == null)
            {
                playerCrimes = new List<int>();
            }

            playerCrimes.Add(crimeId);

            API.shared.setEntityData(player, "Crimes", playerCrimes);
            if (crimeWL.WantedLevel > 2)
                API.shared.setPlayerNametagColor(player, 232, 44, 44);
            else
                API.shared.setPlayerNametagColor(player, 240, 160, 55);
         //   API.shared.setPlayerWantedLevel(player, (int)Math.Ceiling(crimeWL.WantedLevel / 2f));

            return true;
        }

        public static void Cuff(Client Suspect)
        {
            API.shared.playPlayerAnimation(Suspect, 49, "mp_arresting", "idle");
            API.shared.removeAllPlayerWeapons(Suspect);
            var cuffs = API.shared.createObject(-1281059971, Suspect.position, new Quaternion(0f, 0f, 0f, 0f));
            API.shared.attachEntityToEntity(cuffs, Suspect, "SKEL_ROOT", new Vector3(-0.043, -0.225, 0.086), new Vector3(0, 0, 179.99));
            API.shared.setEntityData(Suspect, "Cuffs", cuffs);
            API.shared.triggerClientEvent(Suspect, "CUFFED", true);
            API.shared.delay(120000, true, () => { UnCuff(Suspect); API.shared.sendNotificationToPlayer(Suspect, "Your handcuffs expired"); });
        }
        public static void UnCuff(Client Suspect)
        {
            API.shared.triggerClientEvent(Suspect, "CUFFED", false);
            API.shared.stopPlayerAnimation(Suspect);
            NetHandle cuffs = API.shared.getEntityData(Suspect, "Cuffs");
            API.shared.deleteEntity(cuffs);
            API.shared.resetEntityData(Suspect, "Cuffs");
        }

        public static void BroadcastToCops(string message)
        {
            var players = API.shared.getAllPlayers();

            foreach (var player in players)
            {
                if (API.shared.getEntityData(player, "IS_COP") == true)
                {
                    API.shared.sendChatMessageToPlayer(player, message);
                }
            }
        }

        public static int CalculatePlayerFine(Client player)
        {
            List<int> crimes = API.shared.getEntityData(player, "Crimes");
            int totalPrice = 0;

            if (crimes != null)
            foreach (var crime in crimes)
            {
                var crimeData = WantedLevelDataProvider.Crimes.Get(crime);

                totalPrice += crimeData.TicketCost;
            }

            return totalPrice;
        }
    }
}