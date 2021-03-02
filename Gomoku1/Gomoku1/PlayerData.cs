using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku1
{
    //Singleton
    class PlayerData
    {
        private static PlayerData instance = null;
        public static PlayerData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerData();
                }

                return instance;
            }
        }

        private PlayerData()
        {

        }

        public int playerType;
        public int playerColour;
        public string playerName;
        public int difficulty;
        public int gameStatus;
        public int partnerType;
        public int partnerColour;
        public string partnerName;
        public int gameType;
    }
}
