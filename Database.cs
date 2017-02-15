using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using GTANetworkServer;
using GTANetworkShared;

namespace IcaroRPG
{
    public static partial class Database
    {
        public const string ACCOUNT_FOLDER = "cnc_accounts";
        public const string INVENTORY_FOLDER = "cnc_inventories";

        public static void Init()
        {
            if (!Directory.Exists(ACCOUNT_FOLDER))
                Directory.CreateDirectory(ACCOUNT_FOLDER);

            API.shared.consoleOutput("Database initialized!");
        }

        public static bool DoesAccountExist(string name)
        {
            var path = Path.Combine(ACCOUNT_FOLDER, name);
            return File.Exists(path);
        }

        public static bool IsPlayerLoggedIn(Client player)
        {
            return API.shared.getEntityData(player, "LOGGED_IN") == true;
        }

        public static void CreatePlayerAccount(Client player, string password)
        {
            var path = Path.Combine(ACCOUNT_FOLDER, player.socialClubName);

            //if (!path.StartsWith(Directory.GetCurrentDirectory())) return;
            var data = new PlayerData()
            {
                SocialClubName = player.socialClubName,
                Skin = player.model,
                PX = (int)-851.62,
                PY = (int)-124,
                PZ = (int)38,
                Money = 1500,
                BankMoney = 5000,
                Level = 1,
                Password = API.shared.getHashSHA256(password),
                Health = 200,
                Dimension = 1,
                HomeDimension = Directory.GetFiles(ACCOUNT_FOLDER).Length,
                HomeID = 1
            };

            var ser = API.shared.toJson(data);

            File.WriteAllText(path, ser);
        }

        public static bool TryLoginPlayer(Client player, string password)
        {
            var path = Path.Combine(ACCOUNT_FOLDER, player.socialClubName);

            if(!File.Exists(path))
            {
                return false;
            }

            var txt = File.ReadAllText(path);

            PlayerData playerObj = API.shared.fromJson(txt).ToObject<PlayerData>();

            return API.shared.getHashSHA256(password) == playerObj.Password;
        }

        public static void LoadPlayerAccount(Client player)
        {
            var path = Path.Combine(ACCOUNT_FOLDER, player.socialClubName);

            var txt = File.ReadAllText(path);
            

            PlayerData playerObj = API.shared.fromJson(txt).ToObject<PlayerData>();

            API.shared.setEntityData(player, "LOGGED_IN", true);

            foreach (var property in typeof(PlayerData).GetProperties())
            {
                if (property.GetCustomAttributes(typeof (XmlIgnoreAttribute), false).Length > 0) continue;

                API.shared.setEntityData(player, property.Name, property.GetValue(playerObj, null));
            }
        }

        public static void SavePlayerAccount(Client player)
        {
            var path = Path.Combine(ACCOUNT_FOLDER, player.socialClubName);

            //if (!path.StartsWith(Directory.GetCurrentDirectory())) return;

            if (!File.Exists(path)) return;

            var old = API.shared.fromJson(File.ReadAllText(path));

            var data = new PlayerData()
            {
                SocialClubName = player.socialClubName,
                Password = old.Password,
            };

            foreach (var property in typeof(PlayerData).GetProperties())
            {
                if (property.GetCustomAttributes(typeof(XmlIgnoreAttribute), false).Length > 0) continue;

                if (API.shared.hasEntityData(player, property.Name))
                {
                    if (API.shared.getEntityData(player, "Skin") != 0)
                    {
                        property.SetValue(data, API.shared.getEntityData(player, property.Name), null);
                    }
                }
                if (API.shared.hasEntitySyncedData(player, property.Name))
                {
                        property.SetValue(data, API.shared.getEntityData(player, property.Name), null);
                }
            }

            var ser = API.shared.toJson(data);

            File.WriteAllText(path, ser);
        }
    }

    public class PlayerData
    {
        [XmlIgnore]
        public string SocialClubName { get; set; }
        [XmlIgnore]
        public string Password { get; set; }
        public int Level { get; set; }
        public int WantedLevel { get; set; }
        public int Money { get; set; }
        public int BankMoney { get; set; }
        public List<int> Crimes { get; set; } 
        public bool Jailed { get; set; }
        public uint JailTime { get; set; }
        public int Health { get; set; }
        public int Armor { get; set; }
        public int PX { get; set; }
        public int PY { get; set; }
        public int PZ { get; set; }
        public int Dimension { get; set; }
        public bool Nametagvisible { get; set; }
        public bool IS_COP { get; set; }
        public int Skin { get; set;  }
        public double eX { get; set; }
        public double eY { get; set; }
        public double eZ { get; set; }
        public int draw0 { get; set; }
        public int draw1 { get; set; }
        public int draw2 { get; set; }
        public int draw3 { get; set; }
        public int draw4 { get; set; }
        public int draw5 { get; set; }
        public int draw6 { get; set; }
        public int draw7 { get; set; }
        public int draw8 { get; set; }
        public int draw9 { get; set; }
        public int draw10 { get; set; }
        public int draw11 { get; set; }
        public int tx0 { get; set; }
        public int tx1 { get; set; }
        public int tx2 { get; set; }
        public int tx3 { get; set; }
        public int tx4 { get; set; }
        public int tx5 { get; set; }
        public int tx6 { get; set; }
        public int tx7 { get; set; }
        public int tx8 { get; set; }
        public int tx9 { get; set; }
        public int tx10 { get; set; }
        public int tx11 { get; set; }
        public int propdraw0 { get; set; }
        public int propdraw1 { get; set; }
        public int propdraw2 { get; set; }
        public int propdraw3 { get; set; }
        public int propdraw4 { get; set; }
        public int propdraw5 { get; set; }
        public int propdraw6 { get; set; }
        public int propdraw7 { get; set; }
        public int propdraw8 { get; set; }
        public int propdraw9 { get; set; }
        public int proptx0 { get; set; }
        public int proptx1 { get; set; }
        public int proptx2 { get; set; }
        public int proptx3 { get; set; }
        public int proptx4 { get; set; }
        public int proptx5 { get; set; }
        public int proptx6 { get; set; }
        public int proptx7 { get; set; }
        public int proptx8 { get; set; }
        public int proptx9 { get; set; }
        public bool GTAO_HAS_CHARACTER_DATA { get; set; }
        public int GTAO_SHAPE_FIRST_ID { get; set; }
        public int GTAO_SHAPE_SECOND_ID { get; set; }
        public int GTAO_SKIN_FIRST_ID { get; set; }
        public int GTAO_SKIN_SECOND_ID { get; set; }
        public double GTAO_SHAPE_MIX { get; set; }
        public double GTAO_SKIN_MIX { get; set; }
        public int GTAO_HAIR_COLOR { get; set; }
        public int GTAO_HAIR_HIGHLIGHT_COLOR { get; set; }
        public int GTAO_EYE_COLOR { get; set; }
        public int GTAO_EYEBROWS { get; set; }
        public int GTAO_EYEBROWS_COLOR { get; set; }
        public int GTAO_MAKEUP_COLOR { get; set; }
        public int GTAO_LIPSTICK_COLOR { get; set; }
        public int GTAO_EYEBROWS_COLOR2 { get; set; }
        public int GTAO_MAKEUP_COLOR2 { get; set; }
        public int GTAO_LIPSTICK_COLOR2 { get; set; }
        public int GTAO_MAKEUP { get; set; }
        public int GTAO_BEARD_COLOR { get; set; }
        public int GTAO_BEARD { get; set; }
        public int HomeDimension { get; set; }
        public int HomeID { get; set; }
        public int SpawnChoice { get; set; }
    }
}
