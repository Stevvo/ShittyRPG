using System.Collections.Generic;
using System.Linq;
using System.Media;
using GTANetworkServer;
using GTANetworkShared;

namespace IcaroRPG.Cops
{
    public class JailController : Script
    {
        public JailController()
        {
            API.onUpdate += onUpdate;
            API.onResourceStart += start;
        }

        public static readonly Vector3 JailCenter = new Vector3(1707.69f, 2546.69f, 45.56f);
        public static Dictionary<Client, long> JailTimes = new Dictionary<Client, long>();

        public void start()
        {
         /*   var shape = API.createSphereColShape(JailCenter, 30f);

            shape.onEntityExitColShape += (colShape, entity) =>
            {
                if (API.getEntityData(entity, "Jailed") == true)
                {
                    API.setEntityPosition(entity, JailCenter);
                }
            };
            */
        }

        public void jailPlayer(Client player, int seconds)
        {
            API.setEntityData(player, "Jailed", true);
            API.resetPlayerNametagColor(player);
            API.setEntityPosition(player, JailCenter);
            API.setEntityData(player, "WantedLevel", 0);
            API.setPlayerWantedLevel(player, 0);
            API.resetEntityData(player, "Crimes");
            API.setEntityData(player, "JailTime", seconds);
            API.removeAllPlayerWeapons(player);
            CopUtil.UnCuff(player);
            if (player.model == (int)PedHash.FreemodeMale01)
            {
                CharacterCreator.setSpecialOutfit(player, "MPrisoner.xml");
            }
            if (player.model == (int)PedHash.FreemodeFemale01)
            {
                CharacterCreator.setSpecialOutfit(player, "FPrisoner.xml");
            }

            lock (JailTimes) JailTimes.Set(player, API.TickCount + seconds*1000);
        }

        public void freePlayer(Client player)
        {
            API.resetEntityData(player, "Jailed");
            API.resetEntityData(player, "JailTime");
            API.sendChatMessageToPlayer(player, "~g~You have served your sentence! You're free to go.");
            lock (JailTimes) JailTimes.Remove(player);
            API.setEntityPosition(player, new Vector3(1850.909, 2585.61, 45.67202));
            for (var i = 3; i <= 11; i++)
            {
                var drawable = player.getData("draw" + i);
                var texture = player.getData("tx" + i);
                if (drawable != -1)
                {
                    API.setPlayerClothes(player, i, drawable, texture);
                }
                else
                {
                    API.setPlayerClothes(player, i, 0, 0);
                }
            }
            for (var i = 0; i <= 9; i++)
            {
                API.clearPlayerAccessory(player, i);
            }
        }
        
        public void onUpdate()
        {
            lock (JailTimes)
            {
                for (int i = JailTimes.Count - 1; i >= 0; i--)
                {
                    var pair = JailTimes.ElementAt(i);

                    if (API.TickCount - pair.Value > 0)
                    {
                        freePlayer(pair.Key);
                    }
                }
            }
        }

    }
}