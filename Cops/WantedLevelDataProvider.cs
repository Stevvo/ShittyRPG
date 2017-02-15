using System.Collections.Generic;
using GTANetworkServer;
using GTANetworkShared;

namespace IcaroRPG.Cops
{
    public static class WantedLevelDataProvider
    {
        public static Dictionary<int, CrimeData> Crimes = new Dictionary<int, CrimeData>
        {
            {
                0,
                new CrimeData()
                {
                    Name = "Murder",
                    TicketCost = 0,
                    WantedLevel = 4,
                }
            },
            {
                1,
                new CrimeData()
                {
                    Name = "Traffic Violation",
                    TicketCost = 600,
                    WantedLevel = 1,
                }
            },
            {
                2,
                new CrimeData()
                {
                    Name = "Murder of a LEO", 
                    TicketCost = 0,
                    WantedLevel = 5,
                }
            },
            {
                3,
                new CrimeData()
                {
                    Name = "Grand Theft Auto",
                    TicketCost = 0,
                    WantedLevel = 3,
                }
            },
            {
                4,
                new CrimeData()
                {
                    Name = "Obstruction of Justice",
                    TicketCost = 800,
                    WantedLevel = 1,
                }
            },
            {
                5,
                new CrimeData()
                {
                    Name = "Assault",
                    TicketCost = 800,
                    WantedLevel = 1,
                }
            },
            {
                6,
                new CrimeData()
                {
                    Name = "Kidnapping",
                    TicketCost = 0,
                    WantedLevel = 4,
                }
            },
            {
                7,
                new CrimeData()
                {
                    Name = "Weapons violation.",
                    TicketCost = 1000,
                    WantedLevel = 1,
                }
            },
            {
                8,
                new CrimeData()
                {
                    Name = "Robbery",
                    TicketCost = 0,
                    WantedLevel = 2,
                }
            },
            {
                9,
                new CrimeData()
                {
                    Name = "Public order violation",
                    TicketCost = 500,
                    WantedLevel = 1,
                }
            },
            {
                10,
                new CrimeData()
                {
                    Name = "Drug possession",
                    TicketCost = 500,
                    WantedLevel = 1,
                }
            },
            {
                11,
                new CrimeData()
                {
                    Name = "Drug dealing",
                    TicketCost = 0,
                    WantedLevel = 2,
                }
            },
            {
                12,
                new CrimeData()
                {
                    Name = "Civil Offence",
                    TicketCost = 300,
                    WantedLevel = 1,
                }
            },
        };
        

       public static int GetTimeFromWantedLevel(int wantedLevel)
        {
            switch (wantedLevel)
            {
                
                case 3:
                    return 60;
                case 4:
                    return 120;
                case 5:
                    return 180;
                case 6:
                    return 240;
                case 7:
                    return 300;
                case 8:
                    return 360;
                case 9:
                    return 420;
                case 10:
                    return 600;
                case 11:
                    return 700;
                case 12:
                    return 800;
                default:
                    return 60;
            }
        }



    }

    public struct CrimeData
    {
        public string Name;
        public int WantedLevel;
        public int TicketCost;
    }
}