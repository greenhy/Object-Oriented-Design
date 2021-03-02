using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku1
{
    class HumanPartner : Player
    {
        public new int selectPlayerType()
        {
            PlayerType = 1;

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

            PlayerName = "Human";


            return PlayerName;
        }
    }
    
}
