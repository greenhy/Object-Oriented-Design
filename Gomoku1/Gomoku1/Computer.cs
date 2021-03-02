using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku1
{
    class Computer : Player
    {
        public new int selectPlayerType()
        {
            PlayerType = 2;

            return PlayerType;
        }

        public int selectColour(int colour)
        {

            if (colour == 1)
            {
                Colour = 2;
            }
            else
            {
                Colour = 1;
            }

            return Colour;
        }


        public new string selectPlayerName()
        {

            PlayerName = "Computer";

            return PlayerName;
        }

    }
}
