using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Gomoku1
{
    class Board
    {
        int borderSize = Piece.Instance.boardSize;

        //create board (first time)
        public void createBoard()
        {
          
                //set current player
                Piece.Instance.currentPlayer = 1;
                WriteLine("currentPlayer in Board::" + Piece.Instance.currentPlayer);

                //first row
                Write("┌─");
                for (int i = 0; i < borderSize - 1; ++i)
                {
                    Write("─┬─");
                }
                WriteLine("─┐");

                //second~ 14th row
                for (int i = 0; i < borderSize - 1; ++i)
                {
                    Write("├─");
                    for (int j = 0; j < borderSize - 1; ++j)
                    {
                        Write("─┼─");
                    }
                    WriteLine("─┤");
                }

                //last row
                Write("└─");
                for (int i = 0; i < borderSize - 1; ++i)
                {
                    Write("─┴─");
                }
                WriteLine("─┘");
            }

        //mark position
        public void markPosition()
        {
                 borderSize = 15;
                //first row
                Write("┌─");
                for (int i = 0; i < borderSize - 1; ++i)
                {
                    Write("─┬─");
                }
                WriteLine("─┐");

                //second~ 14th row
                for (int i = 0; i < borderSize - 1; ++i)
                {
                    Write("├─");
                    for (int j = 0; j < borderSize - 1; ++j)
                    {
                        //emtpy position
                        if (Piece.Instance.position[i, j] == 0) Write("─┼─");
                        // marked by player
                        else if (Piece.Instance.position[i, j] == 1)
                        {
                            //select the stone colour
                            //in the board it looks like opposite however, that's because of backgroud colour.
                            if (PlayerData.Instance.playerColour == 1)
                            {
                                Write("-●");
                            }
                            else
                            {
                                Write("-○");
                            }
                        }
                        //marked by partner
                        else if (Piece.Instance.position[i, j] == 2)
                        {
                            //select the stone colour
                            if (PlayerData.Instance.partnerColour == 2)
                            {
                            Write("-○");
                        }
                            else
                            {
                            Write("-●");
                            }
                        }
                    }

                    WriteLine("─┤");
                }



                //last row
                Write("└─");
                for (int i = 0; i < borderSize - 1; ++i)
                {
                    Write("─┴─");
                }
                WriteLine("─┘");

            }
    }
}
