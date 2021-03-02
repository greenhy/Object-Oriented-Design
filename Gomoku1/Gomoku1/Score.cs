using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku1
{
    class Score
    {
        int scoreP1=0;
        int scoreP2=0;

        public void saveScore()
        {
            //save the score in the GameData
            GameData.Instance.score[0, 0] = scoreP1;
            GameData.Instance.score[1, 0] = scoreP2;
        }

        public void calculateScore()
        {
            //increse the score when player win the game
            if (Piece.Instance.winner==1) {
                scoreP1 += 1;
            }
            else
            {
                scoreP2 += 1;

            }
            saveScore();
        }
    }
}
