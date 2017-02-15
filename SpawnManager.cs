using System;
using GTANetworkServer;
using GTANetworkShared;
using System.Collections.Generic;

namespace IcaroRPG
{
    public class SpawnManager : Script
    {

        private readonly Vector3 _copSpawnpoint = new Vector3(447.1f, -984.21f, 30.69f);
        private readonly Vector3 _crookSpawnpoint = new Vector3(-851.622314, -124.067039, 37.6538773);

        public SpawnManager()
        {
            API.onClientEventTrigger += ClientEvent;
            API.onPlayerRespawn += SpawnEvent;
        }

        public void SpawnEvent(Client sender)
        {
            if (!Database.IsPlayerLoggedIn(sender))
            {
                API.setEntityPosition(sender, new Vector3(228.135f, -995.350f, -99.000f));
            }
        }

        public void ClientEvent(Client sender, string eventName, object[] args)
        {
            if (eventName == "skin_select_accept")
            {
                var skin = args[0];
                API.setEntityData(sender, "Skin", skin);

                DimensionManager.DismissPrivateDimension(sender);
                API.setEntityDimension(sender, 1);

                if (Database.IsPlayerLoggedIn(sender))
                {
                    SpawnCitizen(sender);
                    API.call("Interaction", "Customize", sender);
                }
            }
        }

        // Exported

        public void SpawnCop(Client target)
        {
            API.setPlayerNametagColor(target, 55, 135, 240);

            API.setEntityData(target, "IS_COP", true);
            API.setEntityData(target, "IS_CROOK", false);

            API.setEntityPosition(target, _copSpawnpoint);
            API.givePlayerWeapon(target, WeaponHash.Nightstick, 1, false, true);
            API.givePlayerWeapon(target, WeaponHash.StunGun, 500, true, true);


            API.triggerClientEvent(target, "display_subtitle", "~r~F1 Player Menu ~g~F2 Vehicle Menu ~b~F3 COP Menu", 6000);
        }

        public void SpawnCitizen(Client target)
        {
            API.resetPlayerNametagColor(target);
            var skin = (PedHash)API.getEntityData(target, "Skin");
            target.setSkin(skin);
            API.call("CharacterCreator", "LoadClothing", target);
            API.removeAllPlayerWeapons(target);
            target.health = API.getEntityData(target, "Health");
            target.armor = API.getEntityData(target, "Armor");
            API.call("Items", "load", target);
            API.setEntityData(target, "IS_COP", false);

            if (API.getEntityData(target, "Jailed") == true)
            {
                API.call("JailController", "jailPlayer", API.getEntityData(target, "JailTime"));
                API.call("Items", "load", target);
                if (target.model == (int)PedHash.FreemodeMale01)
                {
                    CharacterCreator.setSpecialOutfit(target, "MPrisoner.xml");
                }
                if (target.model == (int)PedHash.FreemodeFemale01)
                {
                    CharacterCreator.setSpecialOutfit(target, "FPrisoner.xml");
                }
            }
            else
            {

                if (target.getData("SpawnChoice") == 2)
                {

                    var PX = API.getEntityData(target, "PX");
                    var PY = API.getEntityData(target, "PY");
                    var PZ = API.getEntityData(target, "PZ");
                    target.dimension = target.getData("Dimension");
                    API.setEntityPosition(target, new Vector3(PX, PY, PZ));
                    API.triggerClientEvent(target, "display_subtitle", "~r~F1 Player Menu ~g~F2 Vehicle Menu", 6000);
                }
                else
                {

                    var homeid = target.getData("HomeID");
                    foreach (Homes.Home home in Homes.Homeslist)
                    {
                        if (homeid == home.ID)
                        {
                            home.teleportIn(target, target.getData("HomeDimension"));
                            API.triggerClientEvent(target, "display_subtitle", "~r~F1 Player Menu ~g~F2 Vehicle Menu", 6000);
                        }
                    }
                }
            }


          //  API.setPlayerWantedLevel(target, (int)Math.Ceiling((float)API.getEntityData(target, "WantedLevel") / 2f));
        }
    }
}