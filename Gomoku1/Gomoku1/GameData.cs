using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Gomoku1
{
    //singletone
    class GameData
    {
        private static GameData instance = null;
        public static GameData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameData();
                }

                return instance;
            }
        }

        private GameData()
        {

        }

        public string[,] data;
        public int[,] redo = new int[1,4];
        public int[,] score = new int[2, 1];
        public int x;
        public int y;
    }
}
