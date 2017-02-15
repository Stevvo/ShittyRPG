using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTANetworkServer;
using GTANetworkShared;
using System.Timers;

namespace IcaroRPG
{
    public class Chat : Script
    {

        public Chat()
        {
            API.onChatMessage += OnPlayerChat;
        }
        
        public void sendCloseMessage(Client player, float radius, string sender, string msg)
        {
            List<Client> nearPlayers = API.getPlayersInRadiusOfPlayer(radius, player);
           nearPlayers.Remove(player);
            foreach (Client target in nearPlayers)
            {
                API.sendChatMessageToPlayer(player, sender, msg);
            }
        }
        public void OnPlayerChat(Client player, string message, CancelEventArgs e)
        {
            sendCloseMessage(player, 15.0f, "~#ffffff~",player.name + " says: " + message);
            e.Cancel = true;
            return;
        }
        

        [Command("me", GreedyArg = true)]
        public void Command_me(Client sender, string message)
        {
            TextLabel txt = API.createTextLabel(sender.name + " " + message + ".", sender.position, 15, 0.7f);
            API.setTextLabelColor(txt, 220, 220, 220, 200);
            API.attachEntityToEntity(txt, sender, "SKEL_HEAD", new Vector3(0, 0, 0.7), new Vector3(0, 0, 0));
            API.delay(6000, true, () => { txt.delete(); });
        }
        [Command("do", GreedyArg = true)]
        public void Command_do(Client sender, string message)
        {
            TextLabel txt = API.createTextLabel("( " + message + ". ) ", sender.position, 15, 0.7f);
            API.setTextLabelColor(txt, 220, 220, 220, 200);
            API.attachEntityToEntity(txt, sender, "SKEL_HEAD", new Vector3(0, 0, 0.7), new Vector3(0, 0, 0));
            API.setEntityData(txt, "Showtxt", true);
            API.delay(6000, true, () => { txt.delete(); });
        }

        [Command("o", GreedyArg = true)]
        public void Command_o(Client sender, string message)
        {
            API.sendChatMessageToAll(sender.name, sender.name + ": ((" + message + "))");
        }


    }

}
