using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTANetworkServer;
using GTANetworkShared;

namespace IcaroRPG
{
   public class Money : Script
    {
        public const int ATM_ROOT = 18;
        public const int ATM_TRANSFER = 19;

        public Money()
        {
            PopulateCashpoints();
            API.onEntityEnterColShape += ATMHit;
            API.onResourceStart += onStart;
            API.onClientEventTrigger += ScriptEvent;
        }

        public void ScriptEvent(Client sender, string eventName, object[] args)
        {
            if (eventName == "menu_handler_select_item")
            {
                var callbackId = (int)args[0];
                var index = (int)args[1];
                if (callbackId == ATM_ROOT)
                {
                    if (index == 0)
                    {
                        Balance(sender);
                    }
                    else if (index == 1)
                    {
                        var icallback = 3;
                        var showtext = "";
                        var maxlen = 10;
                        API.shared.triggerClientEvent(sender, "get_user_input", icallback, showtext, maxlen, null);
                    }
                    else if (index == 2)
                    {
                        var icallback = 4;
                        var showtext = "";
                        var maxlen = 10;
                        API.shared.triggerClientEvent(sender, "get_user_input", icallback, showtext, maxlen, null);
                    }
                    else if (index == 3)
                    {
                        var list = API.getAllPlayers();
                        API.setEntityData(sender, "list", list);
                        list.Remove(sender);
                        var itemsLen = list.Count;
                        object[] argumentList = new object[5 + itemsLen + itemsLen];
                        argumentList[0] = ATM_TRANSFER;
                        argumentList[1] = "Transfer";
                        argumentList[2] = "Select ~b~~h~payee:";
                        argumentList[3] = false;
                        argumentList[4] = itemsLen;
                        var i = 0;
                        foreach (Client player in list)
                        {
                            argumentList[5 + i] = player.name;
                            argumentList[5 + itemsLen + i] = "";
                            i++;
                        }
                        API.shared.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
                    }
                }
                if (callbackId == ATM_TRANSFER)
                {
                    var icallback = 5;
                    var showtext = "";
                    var maxlen = 10;
                    API.shared.triggerClientEvent(sender, "get_user_input", icallback, showtext, maxlen, index);
                }
            }
            else if (eventName == "menu_handler_user_input")
            {
                if (args[1] != null)
                {
                    var icallback = (int)args[0];
                    var msg = (string)args[1];
                    if (icallback == 3)
                    {
                        int n;
                        bool isNumeric = int.TryParse(msg, out n);
                        if (isNumeric)
                        {
                            if (TakeBankMoney(sender, n))
                            {
                                GiveMoney(sender, n);
                                var bal = API.getEntityData(sender, "BankMoney");
                                API.sendPictureNotificationToPlayer(sender, "           $" + bal.ToString(), "CHAR_BANK_MAZE", 0, 0, "~g~WITHDRAWAL COMPLETE", "New Balance:");
                                return;
                            }
                            API.sendPictureNotificationToPlayer(sender, "Insufficient Funds", "CHAR_BANK_MAZE", 0, 0, "~r~DECLINED", " ");
                            return;
                        }
                        API.sendPictureNotificationToPlayer(sender, "Invalid Operation", "CHAR_BANK_MAZE", 0, 0, "~r~DECLINED", " ");
                    }
                    if (icallback == 4)
                    {
                        int n;
                        bool isNumeric = int.TryParse(msg, out n);
                        if (isNumeric)
                        {
                            if (TakeMoney(sender, n))
                            {
                                GiveBankMoney(sender, n);
                                var bal = API.getEntityData(sender, "BankMoney");
                                API.sendPictureNotificationToPlayer(sender, "           $" + bal.ToString(), "CHAR_BANK_MAZE", 0, 0, "~g~DEPOSIT COMPLETE", "New Balance:");
                                return;
                            }
                            API.sendNotificationToPlayer(sender, "You don't have enough cash");
                            return;
                        }
                        API.sendPictureNotificationToPlayer(sender, "Invalid Operation", "CHAR_BANK_MAZE", 0, 0, "~r~DECLINED", "");
                    }
                    if (icallback == 5)
                    {
                        int n;
                        bool isNumeric = int.TryParse(msg, out n);
                        if (isNumeric)
                        {
                            if (TakeBankMoney(sender, n))
                            {
                                List<Client> list = API.getEntityData(sender, "list");
                                int index = (int)args[2];
                                API.sendPictureNotificationToPlayer(list[index],"           $" + msg, "CHAR_BANK_MAZE", 0, 0, "~g~TRANSFER RECIEVED", "From: " + sender.name);
                                GiveBankMoney(list[index], n);
                                return;
                            }
                            API.sendPictureNotificationToPlayer(sender, "Insufficient Funds", "CHAR_BANK_MAZE", 0, 0, "~r~DECLINED", " ");
                            return;
                        }
                        API.sendPictureNotificationToPlayer(sender, "Invalid Operation", "CHAR_BANK_MAZE", 0, 0, "~r~DECLINED", " ");
                    }
                }
            }
        }

        public void ATMHit(ColShape colshape, NetHandle entity)
        {
            if (colshape.getData("ATM") == true)
            {
                var sender = API.getPlayerFromHandle(entity);
                if (sender == null) return;

                object[] argumentList = new object[13];
                argumentList[0] = ATM_ROOT;
                argumentList[1] = "ATM";
                argumentList[2] = "Select option:";
                argumentList[3] = false;
                argumentList[4] = 4;
                argumentList[5] = "Balance";
                argumentList[6] = "Withdraw";
                argumentList[7] = "Deposit";
                argumentList[8] = "Transfer";
                for (var i = 0; i < 4; i++)
                {
                    argumentList[9 + i] = "";
                }
                API.triggerClientEvent(sender, "menu_handler_create_menu", argumentList);
            }
        }

        public void onStart()
        {
            API.delay(1800000, false, () =>
            {
               foreach(Client player in API.getAllPlayers())
                {
                    var level = API.getEntityData(player, "Level");
                    API.setEntityData(player, "Level", level + 1);
                    int amount = level * 70;
                    var bankrmoney = API.shared.getEntityData(player, "BankMoney");
                    API.shared.setEntityData(player, "BankMoney", bankrmoney + amount);
                    API.sendPictureNotificationToPlayer(player, "From: Department of Social Security \n Amount: $" + amount + "- \n Balance: $" + bankrmoney.ToString(), "CHAR_BANK_MAZE", 1, 1, "Maze Bank Co.", "TRANSFER RECIEVED");
                }

            });

            API.delay(450000, true, () =>
            {
                foreach (Client player in API.getAllPlayers())
                {
                    var level = API.getEntityData(player, "Level");
                    API.setEntityData(player, "Level", level + 1);
                    int amount = level * 70;
                    var bankrmoney = API.shared.getEntityData(player, "BankMoney");
                    API.shared.setEntityData(player, "BankMoney", bankrmoney + amount);
                    API.sendPictureNotificationToPlayer(player, "From: Department of Social Security \n Amount: $" + amount + "- \n Balance: $" + bankrmoney.ToString(), "CHAR_BANK_MAZE", 1, 1, "Maze Bank Co.", "TRANSFER RECIEVED");
                }

            });
        }

        public static bool TakeMoney(Client sender, int amount)
        {
            var playermoney = API.shared.getEntityData(sender, "Money");
            if ((playermoney > amount) &&(amount > 0))
            {
                
                API.shared.setEntityData(sender, "Money", playermoney - amount);
                API.shared.triggerClientEvent(sender, "update_money_display", API.shared.getEntityData(sender, "Money"));
                return true;
            }
            return false;
        }

        public static void GiveMoney(Client sender, int amount)
        {
            if (amount > 0)
            {
                var playermoney = API.shared.getEntityData(sender, "Money");
                API.shared.setEntityData(sender, "Money", playermoney + amount);
                API.shared.triggerClientEvent(sender, "update_money_display", API.shared.getEntityData(sender, "Money"));
            }
        }

        public static void GiveBankMoney(Client sender, int amount)
        {
            if (amount > 0)
            {
                var bankrmoney = API.shared.getEntityData(sender, "BankMoney");
                API.shared.setEntityData(sender, "BankMoney", bankrmoney + amount);
                API.shared.triggerClientEvent(sender, "update_money_display", API.shared.getEntityData(sender, "Money"));
            }
        }

        public static bool TakeBankMoney(Client sender, int amount)
        {
            var bankrmoney = API.shared.getEntityData(sender, "BankMoney");
            if ((bankrmoney > amount) && (amount > 0))
            {
                API.shared.setEntityData(sender, "BankMoney", bankrmoney - amount);
                API.shared.triggerClientEvent(sender, "update_money_display", API.shared.getEntityData(sender, "Money"));
                return true;
            }
            return false;
        }

        public class ATM
        {
            public Vector3 Position { get; set; }

            public ATM(Vector3 position)
            {
                Position = position;
                ColShape col = API.shared.createSphereColShape(Position, 3);
                col.setData("ATM", true);
                var b = API.shared.createBlip(Position);
                API.shared.setBlipSprite(b, 108);
                b.shortRange = true;

            }
        }

        public void Balance(Client sender)
        {
            var bal = API.getEntityData(sender, "BankMoney");
            API.sendPictureNotificationToPlayer(sender, "           $" + bal.ToString(), "CHAR_BANK_MAZE", 0, 0, "BALANCE", "");
        }

        public void PopulateCashpoints()
        {
            new ATM(new Vector3(-846.6537, -341.509, 37.6685));
            new ATM(new Vector3(-847.204, -340.4291, 37.6793));
            new ATM(new Vector3(-1410.736, -98.9279, 51.397));
            new ATM(new Vector3(-1410.183, -100.6454, 51.3965));
            new ATM(new Vector3(-2295.853, 357.9348, 173.6014));
            new ATM(new Vector3(-2295.069, 356.2556, 173.6014));
            new ATM(new Vector3(-2294.3, 354.6056, 173.6014));
            new ATM(new Vector3(-282.7141, 6226.43, 30.4965));
            new ATM(new Vector3(-386.4596, 6046.411, 30.474));
            new ATM(new Vector3(24.5933, -945.543, 28.333));
            new ATM(new Vector3(5.686, -919.9551, 28.4809));
            new ATM(new Vector3(296.1756, -896.2318, 28.2901));
            new ATM(new Vector3(296.8775, -894.3196, 28.2615));
            new ATM(new Vector3(-846.6537, -341.509, 37.6685));
            new ATM(new Vector3(-847.204, -340.4291, 37.6793));
            new ATM(new Vector3(-1410.736, -98.9279, 51.397));
            new ATM(new Vector3(-1410.183, -100.6454, 51.3965));
            new ATM(new Vector3(-2295.853, 357.9348, 173.6014));
            new ATM(new Vector3(-2295.069, 356.2556, 173.6014));
            new ATM(new Vector3(-2294.3, 354.6056, 173.6014));
            new ATM(new Vector3(-282.7141, 6226.43, 30.4965));
            new ATM(new Vector3(-386.4596, 6046.411, 30.474));
            new ATM(new Vector3(24.5933, -945.543, 28.333));
            new ATM(new Vector3(5.686, -919.9551, 28.4809));
            new ATM(new Vector3(296.1756, -896.2318, 28.2901));
            new ATM(new Vector3(296.8775, -894.3196, 28.2615));
            new ATM(new Vector3(-712.9357, -818.4827, 22.7407));
            new ATM(new Vector3(-710.0828, -818.4756, 22.7363));
            new ATM(new Vector3(289.53, -1256.788, 28.4406));
            new ATM(new Vector3(289.2679, -1282.32, 28.6552));
            new ATM(new Vector3(-1569.84, -547.0309, 33.9322));
            new ATM(new Vector3(-1570.765, -547.7035, 33.9322));
            new ATM(new Vector3(-1305.708, -706.6881, 24.3145));
            new ATM(new Vector3(-2071.928, -317.2862, 12.3181));
            new ATM(new Vector3(-821.8936, -1081.555, 10.1366));
            new ATM(new Vector3(-712.9357, -818.4827, 22.7407));
            new ATM(new Vector3(-710.0828, -818.4756, 22.7363));
            new ATM(new Vector3(289.53, -1256.788, 28.4406));
            new ATM(new Vector3(289.2679, -1282.32, 28.6552));
            new ATM(new Vector3(-1569.84, -547.0309, 33.9322));
            new ATM(new Vector3(-1570.765, -547.7035, 33.9322));
            new ATM(new Vector3(-1305.708, -706.6881, 24.3145));
            new ATM(new Vector3(-2071.928, -317.2862, 12.3181));
            new ATM(new Vector3(-821.8936, -1081.555, 10.1366));
            new ATM(new Vector3(-867.013, -187.9928, 36.8822));
            new ATM(new Vector3(-867.9745, -186.3419, 36.8822));
            new ATM(new Vector3(-3043.835, 594.1639, 6.7328));
            new ATM(new Vector3(-3241.455, 997.9085, 11.5484));
            new ATM(new Vector3(-204.0193, -861.0091, 29.2713));
            new ATM(new Vector3(118.6416, -883.5695, 30.1395));
            new ATM(new Vector3(-256.6386, -715.8898, 32.7883));
            new ATM(new Vector3(-259.2767, -723.2652, 32.7015));
            new ATM(new Vector3(-254.5219, -692.8869, 32.5783));
            new ATM(new Vector3(-867.013, -187.9928, 36.8822));
            new ATM(new Vector3(-867.9745, -186.3419, 36.8822));
            new ATM(new Vector3(-3043.835, 594.1639, 6.7328));
            new ATM(new Vector3(-3241.455, 997.9085, 11.5484));
            new ATM(new Vector3(-204.0193, -861.0091, 29.2713));
            new ATM(new Vector3(118.6416, -883.5695, 30.1395));
            new ATM(new Vector3(-256.6386, -715.8898, 32.7883));
            new ATM(new Vector3(-259.2767, -723.2652, 32.7015));
            new ATM(new Vector3(-254.5219, -692.8869, 32.5783));
            new ATM(new Vector3(89.8134, 2.8803, 67.3521));
            new ATM(new Vector3(-617.8035, -708.8591, 29.0432));
            new ATM(new Vector3(-617.8035, -706.8521, 29.0432));
            new ATM(new Vector3(-614.5187, -705.5981, 30.224));
            new ATM(new Vector3(-611.8581, -705.5981, 30.224));
            new ATM(new Vector3(-537.8052, -854.9357, 28.2754));
            new ATM(new Vector3(-526.7791, -1223.374, 17.4527));
            new ATM(new Vector3(-1315.416, -834.431, 15.9523));
            new ATM(new Vector3(-1314.466, -835.6913, 15.9523));
            new ATM(new Vector3(89.8134, 2.8803, 67.3521));
            new ATM(new Vector3(-617.8035, -708.8591, 29.0432));
            new ATM(new Vector3(-617.8035, -706.8521, 29.0432));
            new ATM(new Vector3(-614.5187, -705.5981, 30.224));
            new ATM(new Vector3(-611.8581, -705.5981, 30.224));
            new ATM(new Vector3(-537.8052, -854.9357, 28.2754));
            new ATM(new Vector3(-526.7791, -1223.374, 17.4527));
            new ATM(new Vector3(-1315.416, -834.431, 15.9523));
            new ATM(new Vector3(-1314.466, -835.6913, 15.9523));
            new ATM(new Vector3(-1205.378, -326.5286, 36.851));
            new ATM(new Vector3(-1206.142, -325.0316, 36.851));
            new ATM(new Vector3(147.4731, -1036.218, 28.3678));
            new ATM(new Vector3(145.8392, -1035.625, 28.3678));
            new ATM(new Vector3(-1205.378, -326.5286, 36.851));
            new ATM(new Vector3(-1206.142, -325.0316, 36.851));
            new ATM(new Vector3(147.4731, -1036.218, 28.3678));
            new ATM(new Vector3(145.8392, -1035.625, 28.3678));
        }

    }
}
