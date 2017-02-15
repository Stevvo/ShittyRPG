using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTANetworkServer;
using GTANetworkShared;

namespace IcaroRPG
{
    public class Damage : Script
    {
        public Damage()
        {
            API.onClientEventTrigger += ClientTrigger;
        }
        public void ClientTrigger(Client sender, string eventName, object[] args)
        {
            if (eventName == "dmg")
            {
                API.setEntityData(sender, "Health", sender.health);
                API.setEntityData(sender, "Armor", sender.armor);
            }
        }
    }
}
