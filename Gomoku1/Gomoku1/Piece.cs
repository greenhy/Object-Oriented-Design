using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku1
{
    class Piece
    {
        private static Piece instance = null;
        public static Piece Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Piece();
                }

                return instance;
            }
        }

        private Piece()
        {

        }

        public int currentPlayer;
        public int[,] position = new int[15,15];
        public int boardSize = 15;
        public int inputNubmer = 1;
        public int winner;
    }
}
