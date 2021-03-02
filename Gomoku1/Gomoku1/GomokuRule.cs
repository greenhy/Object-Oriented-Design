using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static System.Console;

namespace Gomoku1
{
    class GomokuRule
    {
        //check the rule to finish the game
        public bool defindFinishRule(int x, int y)
        {

            int cnt = 1;

            // check the right
            for (int i = x + 1; i < Piece.Instance.boardSize; i++)
            {
                //if there is a stone, increase the conunt
                if (Piece.Instance.position[i, y] == Piece.Instance.position[x, y])
                    cnt++;
                else
                    break;
            }
            // check the left
            for (int i = x - 1; i >= 0; i--)
            {
                //if there is a stone, increase the conunt
                if (Piece.Instance.position[i, y] == Piece.Instance.position[x, y])
                    cnt++;
                else
                    break;
            }
            //if there is a five or more stone return true->finish the game
            if (cnt >= 5)
            {
                return true;
            }

            //initialize the count to check other directions
            cnt = 1;

            // check the below
            for (int i = y + 1; i < Piece.Instance.boardSize; i++)
            {
                //if there is a stone, increase the conunt
                if (Piece.Instance.position[x, i] == Piece.Instance.position[x, y])
                    cnt++;
                else
                    break;
            }

            // check the below
            for (int i = y - 1; i >= 0; i--)
            {
                //if there is a stone, increase the conunt
                if (Piece.Instance.position[x, i] == Piece.Instance.position[x, y])
                    cnt++;
                else
                    break;
            }

            //if there is a five or more stone return true->finish the game
            if (cnt >= 5)
            {
                return true;
            }

            //initialize the count to check other directions
            cnt = 1;

            // diagonal right upper
            for (int i = x + 1, j = y - 1; i < Piece.Instance.boardSize && j >= 0; i++, j--)
            {
                //if there is a stone, increase the conunt
                if (Piece.Instance.position[i, j] == Piece.Instance.position[x, y])
                    cnt++;
                else
                    break;
            }

            // diagonal left lower
            for (int i = x - 1, j = y + 1; i >= 0 && j < Piece.Instance.boardSize; i--, j++)
            {
                //if there is a stone, increase the conunt
                if (Piece.Instance.position[i, j] == Piece.Instance.position[x, y])
                    cnt++;
                else
                    break;
            }

            //if there is a five or more stone return true->finish the game
            if (cnt >= 5)
            {
                return true;
            }

            //initialize the count to check other directions
            cnt = 1;

            // diagonal left upper
            for (int i = x - 1, j = y - 1; i >= 0 && j >= 0; i--, j--)
            {
                //if there is a stone, increase the conunt
                if (Piece.Instance.position[i, j] == Piece.Instance.position[x, y])
                    cnt++;
                else
                    break;
            }
            // diagonal right lower
            for (int i = x + 1, j = y + 1; i < Piece.Instance.boardSize && j < Piece.Instance.boardSize; i++, j++)
            {
                //if there is a stone, increase the conunt
                if (Piece.Instance.position[i, j] == Piece.Instance.position[x, y])
                    cnt++;
                else
                    break;
            }
            //if there is a five or more stone return true->finish the game
            if (cnt >= 5)
            {
                return true;
            }

            //if not matched return false to continue the game
            return false;
        }

        //Rules for computer by difficulty
        public int[] defineMovementRuleforCom(int difficulty)
        {
            int[] position = new int[2];

             //easy version
                if (difficulty == 1)
                {
                    WriteLine("Computer");
                    //Use random position
                    Random r = new Random();

                    position[0] = r.Next(0, 15);
                    position[1] = r.Next(0, 15);
                    return position;
                }
                //normal version : just check rows and cols
                // when the danger is more than 3, computer try to find the poistion which can block player's stone.
                // For example player put (4,5) (4,6) (4,7) in this case computer search the right side if there is no stone, put the stone in the (4,8)
                // if (4,8) already used by computer then search the left side and put the stone in the (4,4). in difficulty 2 and 3 they have same rule but diffirent direction
                else if (difficulty == 2)
                {
                    int[,] targetP = Piece.Instance.position;
                    //search the left and right side to put the stone for attack
                    for (int a = 0; a < Piece.Instance.boardSize; ++a)
                    {
                        for (int b = 0; b < Piece.Instance.boardSize; ++b)
                        {
                            int fx = b;
                            //if there is a computer's stone
                            if (targetP[a, b] == 2)
                            {
                                //check the right side
                                for (int attack = 0; fx < b + 5; ++fx)
                                {
                                    if (targetP[a, fx] == 2)
                                    {
                                        ++attack;
                                    }
                                    //if computer can attack
                                    if (attack == 4)
                                    {
                                        for (int i = b; i < fx; ++i)
                                        {
                                            if (targetP[a, i] == 0)
                                            {
                                                //put the stone
                                                position[0] = a;
                                                position[1] = i;
                                                return position;
                                            }
                                        }

                                        //check the left side
                                        if (fx - 6 > 0 && targetP[a, fx - 6] == 0)
                                        {
                                            position[0] = a;
                                            position[1] = fx - 4;
                                            return position;
                                        }
                                        else continue;
                                    }
                                }
                            }
                        }
                    }
                    //search the left and right side to put the stone for defence
                    for (int a = 0; a < Piece.Instance.boardSize; ++a)
                    {
                        for (int b = 0; b < Piece.Instance.boardSize; ++b)
                        {
                            int fx = b;
                            int fy = a;
                            //if there is a player's stone
                            if (targetP[a, b] == 1)
                            {
                                //check the right and left side if there is a another player's stone, danger is increased if there is a computer's stone danger is decreased
                                for (int danger = 0; fx < b + 5; ++fx)
                                {
                                    if (targetP[a, fx] == 1)
                                    {
                                        ++danger;
                                    }
                                    else if (targetP[a, fx] == 2)
                                    {
                                        --danger;
                                    }

                                    //if danger is more than three it means computer should be defence
                                    if (danger >= 3)
                                    {
                                        //search the defense position(right)
                                        for (int i = b; i < fx; ++i)
                                        {
                                            if (targetP[a, i] == 0)
                                            {
                                                //set the position
                                                position[0] = a;
                                                position[1] = i;
                                                return position;

                                            }
                                        }
                                        //check the defense position(left)
                                        if (fx - 5 > 0 && targetP[a, fx - 5] == 0)
                                        {
                                            position[0] = a;
                                            position[1] = fx - danger;
                                            //if there is a stone already find another position
                                            if (targetP[position[0], position[1]] != 0) position[1] = fx + danger - 1;
                                            return position;

                                        }
                                        else continue;
                                    }
                                }
                                //check the upper and lower side if there is a another player's stone, danger is increased if there is a computer's stone danger is decreased
                                for (int danger = 0; fy < a + 5; ++fy)
                                {
                                    if (targetP[fy, b] == 1)
                                    {
                                        ++danger;
                                    }
                                    else if (targetP[fy, b] == 2)
                                    {
                                        --danger;
                                    }
                                    //if danger is more than three it means computer should be defence
                                    if (danger >= 3)
                                    {
                                        for (int i = b; i < fy; ++i)
                                        {
                                            //search the defense position
                                            if (targetP[b, i] == 0)
                                            {
                                                //set the position
                                                position[0] = i;
                                                position[1] = b;
                                                //if there is a stone already find another position
                                                if (targetP[position[0], position[1]] != 0) position[0] = i + danger - 1;
                                                return position;

                                            }
                                            else break;
                                        }
                                        //search the defense position
                                        if (fy - 5 > 0 && targetP[fy - 5, b] == 0)
                                        {
                                            //set the position
                                            position[0] = fy - danger;
                                            position[1] = b;
                                            //if there is a stone already find another position
                                            if (targetP[position[0], position[1]] != 0) position[0] = fy + danger - 1;
                                            return position;

                                        }
                                        else continue;
                                    }
                                }
                            }
                            //when computer try to put the stone in the [0,0] even it is already used
                            if (position[0] == 0 && position[1] == 0 && (targetP[0, 0] == 1 || targetP[0, 0] == 2))
                            {
                                //search the board to find empty space
                                for (int c = 1; c < Piece.Instance.boardSize; ++c)
                                {
                                    for (int d = 1; d < Piece.Instance.boardSize; ++d)
                                    {
                                        if (Piece.Instance.position[c, d] == 0)
                                        {
                                            position[0] = c;
                                            position[1] = d;
                                            break;

                                        }

                                    }
                                    break;
                                }
                            }
                        }
                    }

                    return position;
                }
                //hard version :: check all direction
                else
                {
                    int[,] targetP = Piece.Instance.position;
                    //same as difficulty 2
                    for (int a = 0; a < Piece.Instance.boardSize; ++a)
                    {
                        for (int b = 0; b < Piece.Instance.boardSize; ++b)
                        {
                            int fx = b;
                            if (targetP[a, b] == 2)
                            {
                                for (int attack = 0; fx < b + 5; ++fx)
                                {
                                    if (targetP[a, fx] == 2)
                                    {
                                        ++attack;
                                    }
                                    if (attack == 4)
                                    {
                                        for (int i = b; i < fx; ++i)
                                        {
                                            if (targetP[a, i] == 0)
                                            {
                                                position[0] = a;
                                                position[1] = i;
                                                return position;
                                            }
                                        }

                                        if (fx - 6 > 0 && targetP[a, fx - 6] == 0)
                                        {
                                            position[0] = a;
                                            position[1] = fx - 4;
                                            return position;
                                        }
                                        else continue;
                                    }
                                }
                            }
                        }
                    }
                    for (int a = 0; a < Piece.Instance.boardSize; ++a)
                    {
                        for (int b = 0; b < Piece.Instance.boardSize; ++b)
                        {
                            if (targetP[a, b] == 1)
                            {
                                //same as line 207~
                                for (int danger = 0, fx = b; fx < b + 5; ++fx)
                                {
                                    if (targetP[a, fx] == 1)
                                    {
                                        ++danger;
                                    }
                                    else if (targetP[a, fx] == 2)
                                    {
                                        --danger;
                                    }

                                    if (danger >= 3)
                                    {
                                        for (int i = b; i < fx; ++i)
                                        {
                                            if (targetP[a, i] == 0)
                                            {
                                                position[0] = a;
                                                position[1] = i;
                                                return position;

                                            }
                                        }
                                        if (fx - 5 > 0 && targetP[a, fx - 5] == 0)
                                        {
                                            position[0] = a;
                                            position[1] = fx - danger;
                                            if (targetP[position[0], position[1]] != 0) position[1] = fx + danger - 1;
                                            return position;

                                        }
                                        else continue;
                                    }
                                }
                                //same as line 247~
                                for (int danger = 0, fy = a; fy < a + 5; ++fy)
                                {
                                    if (targetP[fy, b] == 1)
                                    {
                                        ++danger;
                                    }
                                    else if (targetP[fy, b] == 2)
                                    {
                                        --danger;
                                    }

                                    if (danger >= 3)
                                    {
                                        for (int i = b; i < fy; ++i)
                                        {
                                            WriteLine("fi>>" + b + "," + i);
                                            if (targetP[b, i] == 0)
                                            {
                                                position[0] = i;
                                                position[1] = b;
                                                if (targetP[position[0], position[1]] != 0) position[0] = i + danger - 1;
                                                // WriteLine("position2>>" + position[0] + "," + position[1]);
                                                return position;

                                            }
                                            else break;
                                        }
                                        if (fy - 5 > 0 && targetP[fy - 5, b] == 0)
                                        {
                                            position[0] = fy - danger;
                                            position[1] = b;
                                            if (targetP[position[0], position[1]] != 0) position[0] = fy + danger - 1;
                                            // WriteLine("position3>>" + +position[0] + "," + position[1]);
                                            return position;

                                        }
                                        else continue;
                                    }
                                }
                                //// diagonal right upper
                                for (int danger = 0, fx = b, fy = a; fx < a + 5 && fy > 0; --fy, ++fx)
                                {
                                    if (targetP[fx, fy] == 1)
                                    {
                                        ++danger;
                                    }
                                    else if (targetP[fx, fy] == 2)
                                    {
                                        --danger;
                                    }
                                    //WriteLine("danger>>"+danger);

                                    if (danger >= 3)
                                    {
                                        for (int i = b, j = a; i < fx + 5 && j > 0; ++i, --j)
                                        {
                                            if (targetP[i, j] == 0)
                                            {
                                                position[0] = fx + 1;
                                                position[1] = fy - 1;
                                                if (targetP[position[0], position[1]] != 0)
                                                {
                                                    position[0] = fy - danger - 1; position[1] = fx + danger + 1;
                                                };
                                                return position;

                                            }
                                        }
                                    }
                                }
                                // diagonal left lower
                                for (int danger = 0, fx = b, fy = a; fy < a + 5 && fx > 0; --fx, ++fy)
                                {
                                    if (targetP[fx, fy] == 1)
                                    {
                                        ++danger;
                                    }
                                    else if (targetP[fx, fy] == 2)
                                    {
                                        --danger;
                                    }
                                    //WriteLine("danger>>"+danger);

                                    if (danger >= 3)
                                    {
                                        for (int i = a, j = b; i < fy + 5 && j > 0; ++i, --j)
                                        {
                                            if (targetP[i, j] == 0)
                                            {
                                                position[0] = fx - 1;
                                                position[1] = fy + 1;
                                                if (targetP[position[0], position[1]] != 0)
                                                {
                                                    position[0] = fx + danger + 1; position[1] = fy - danger - 1;
                                                    // WriteLine("position1>>" + +position[0] + "," + position[1]);
                                                };
                                                return position;
                                            }
                                        }
                                    }
                                }
                                //// diagonal left upper
                                for (int danger = 0, fx = b, fy = a; fy > 0 && fx > 0; --fx, --fy)
                                {
                                    if (targetP[fx, fy] == 1)
                                    {
                                        ++danger;
                                    }
                                    else if (targetP[fx, fy] == 2)
                                    {
                                        --danger;
                                    }

                                    if (danger >= 3)
                                    {
                                        for (int i = a, j = b; i > 0 && j > 0; --i, --j)
                                        {
                                            if (targetP[i, j] == 0)
                                            {
                                                position[0] = fx - 1;
                                                position[1] = fy - 1;
                                                if (targetP[position[0], position[1]] != 0)
                                                {
                                                    position[0] = b + danger - 1; position[1] = a + danger - 1;
                                                };
                                                return position;
                                            }
                                        }
                                    }
                                }
                                //diagonal right lower
                                for (int danger = 0, fx = b, fy = a; (fy < a + 5) && (fx < b + 5); ++fx, ++fy)
                                {
                                    if (targetP[fx, fy] == 1)
                                    {
                                        ++danger;
                                    }
                                    else if (targetP[fx, fy] == 2)
                                    {
                                        --danger;
                                    }

                                    if (danger >= 3)
                                    {
                                        for (int i = a, j = b; (i < fx + 5) && (j < fy + 5); ++i, ++j)
                                        {
                                            if (targetP[i, j] == 0)
                                            {
                                                position[0] = fx + 1;
                                                position[1] = fy + 1;
                                                if (targetP[position[0], position[1]] != 0)
                                                {
                                                    position[0] = fy - danger - 1; position[1] = fx - danger - 1;
                                                };
                                                return position;
                                            }
                                        }
                                    }
                                }
                            }
                            //when computer try to put the stone in the [0,0] even it is already used
                            if (position[0] == 0 && position[1] == 0 && (targetP[0, 0] == 1 || targetP[0, 0] == 2))
                            {
                                for (int c = 1; c < Piece.Instance.boardSize; ++c)
                                {
                                    for (int d = 1; d < Piece.Instance.boardSize; ++d)
                                    {
                                        if (Piece.Instance.position[c, d] == 0)
                                        {
                                            position[0] = c;
                                            position[1] = d;
                                            break;

                                        }

                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            return position;
        }
        

    }
}


