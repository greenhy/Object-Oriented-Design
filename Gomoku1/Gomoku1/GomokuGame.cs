using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.IO;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;

namespace Gomoku1
{
    class GomokuGame
    {
        private int cnt = 0;
        private int option = 0;
        const int REFRESH= 999;
        const int STOP = 888;
        const int UNDO = 777;
        const int REDO = 555;
        const int RULE = 333;
        const int FAQ = 222;

        //to start the game
        public void startGame()
        {
            Board b = new Board();
            b.createBoard();
            process(b);
        }

        // the main process of the game
        public void process(Board b){
            OnlineHelp on = new OnlineHelp();
            
            //display the game options and player can select an option
            while (true)
            {
                WriteLine("select option 999: refresh the game, 888: stop the game, 777: undo move, 555: redo move, 0: continue, 333: Rule, 222: FAQ>>");

                option = Convert.ToInt32(ReadLine());

                // check the input is vaild
                while (option != REFRESH && option != STOP && option != UNDO && option != REDO && option != 0 && option != 333 && option != 222)
                {
                    WriteLine("invalid value.");
                    WriteLine("select option 999: refresh the game, 888: stop the game, 777: undo move, 555: redo move, 0: continue, 333: Rule, 222: FAQ>>");
                    option = Convert.ToInt32(ReadLine());
                }

                if (option == REFRESH) refresh();
                else if (option == STOP) break;
                else if (option == UNDO) undo(b);
                else if (option == REDO) redo(b);
                else if (option == RULE) on.displayeGameRule();
                else if (option == FAQ) on.displayFAQ();

                int[] savePosition = new int[2];
                //move position and save the position for history
                savePosition = movePosition();
                //record the movement
                saveHistory(Piece.Instance.inputNubmer, cnt, savePosition[0], savePosition[1]);
                cnt++;
                Piece.Instance.inputNubmer++;

                //set the x, y
                GameData.Instance.x = savePosition[0];
                GameData.Instance.y = savePosition[1];

                //if game is not finished continue the game
                if (!finishGame(savePosition[0], savePosition[1]))
                {
                    //display the current player and previous position
                    WriteLine("currentPlayer::" + Piece.Instance.currentPlayer);
                    WriteLine("previous position(x,y)::("+ GameData.Instance.x+","+ GameData.Instance.y+")");
                    //show the board with previous movement
                    b.markPosition();
                }
                else break;

                //change the turn
                if (Piece.Instance.currentPlayer == 1)
                {
                    Piece.Instance.currentPlayer = 2;
                }
                else
                {
                    Piece.Instance.currentPlayer = 1;
                }
            }
        }

        //move position
        public int[] movePosition()
        {
            int x, y;
            int[] position = new int[2];
            GomokuRule r = new GomokuRule();
            CommandR ruleOnCommand = new GomokuRuleOnCommand(r);
            Rule rule = new Rule();
            rule.setCommand(ruleOnCommand);

            // if play with human player(object diagram and sequence diagram part2)
            if (PlayerData.Instance.playerType == 1)
            {
                while (true)
                {
                    //input x position
                    WriteLine("Enter the position x>>");
                    x = Convert.ToInt32(ReadLine()) - 1;
                    //check the value
                    while (x > Piece.Instance.boardSize || x < 0)
                    {
                        WriteLine("invalid position");
                        WriteLine("Enter the position x>>");
                        x = Convert.ToInt32(ReadLine()) - 1;
                    }
                    //input y position
                    WriteLine("Enter the position y>>");
                    y = Convert.ToInt32(ReadLine()) - 1;
                    //check the value
                    while (y > Piece.Instance.boardSize || y < 0)
                    {
                        WriteLine("invalid position");
                        WriteLine("Enter the position y>>");
                        y = Convert.ToInt32(ReadLine()) - 1;
                    }
                    // check the position. if it already used player should be select another position
                    if (checkPosition(x, y))
                    {
                        break;
                    }

                }

                // store the current position information
                if (Piece.Instance.currentPlayer == 1)
                {
                    Piece.Instance.position[x, y] = 1;
                }

                else if (Piece.Instance.currentPlayer == 2)
                {
                    Piece.Instance.position[x, y] = 2;
                }

                position[0] = x;
                position[1] = y;

                //save the current player, poistion x, y and mark for redo and undo
                GameData.Instance.redo[0, 0] = Piece.Instance.currentPlayer;
                GameData.Instance.redo[0, 1] = x;
                GameData.Instance.redo[0, 2] = y;
                GameData.Instance.redo[0, 3] = Piece.Instance.position[x, y];
            }
            //if play with computer player
            else
            {
                while (true)
                {
                    //palyer select the position
                    if(Piece.Instance.currentPlayer == 1)
                    {
                        WriteLine("Enter the position x>>");
                        x = Convert.ToInt32(ReadLine()) - 1;

                        while (x > Piece.Instance.boardSize || x < 0)
                        {
                            WriteLine("invalid position");
                            WriteLine("Enter the position x>>");
                            x = Convert.ToInt32(ReadLine()) - 1;
                        }

                        WriteLine("Enter the position y>>");
                        y = Convert.ToInt32(ReadLine()) - 1;
                        while (y > Piece.Instance.boardSize || y < 0)
                        {
                            WriteLine("invalid position");
                            WriteLine("Enter the position y>>");
                            y = Convert.ToInt32(ReadLine()) - 1;
                        }

                        if (checkPosition(x, y))
                        {
                            break;
                        }
                    }
                    // if computer's turn
                    else
                    {
                        //get position from rule
                        int[] point = rule.computerMove();
                        x = point[0];
                        y = point[1];
                        if (checkPosition(x, y))
                        {
                            break;
                        }
                    }
                   
                }
            }

           //save the position
            if (Piece.Instance.currentPlayer == 1)
            {
                Piece.Instance.position[x, y] = 1;
            }

            else if (Piece.Instance.currentPlayer == 2)
            {
                Piece.Instance.position[x, y] = 2;
            }

            //save the game data for redo and undo
            GameData.Instance.redo[0, 0] = Piece.Instance.currentPlayer;
            GameData.Instance.redo[0, 1] = x;
            GameData.Instance.redo[0, 2] = y;
            GameData.Instance.redo[0, 3] = Piece.Instance.position[x, y];

            position[0] = x;
            position[1] = y;

            GameData.Instance.x = position[0]+1;
            GameData.Instance.y = position[1]+1;

            return position;
        }

        //check the position
        public bool checkPosition(int x, int y)
        {
            // if player's position is already used return false and go to movePosition
            if (Piece.Instance.position[x, y] != 0)
            {
                if (Piece.Instance.currentPlayer== 1)
                {
                    WriteLine("This position was used.");
                }                
                return false;
            }
            else
            {
                return true;
            }
        }

        //check the rule to finish the game
        public bool finishGame(int x, int y)
        {

            GomokuRule r = new GomokuRule();
            CommandR ruleOnCommand = new GomokuRuleOnCommand(r);
            Rule rule = new Rule();
            rule.setCommand(ruleOnCommand);
            
            //check the rule
            //can finish
            if (rule.checkFinishCondition())
            {
                //display the winner
                WriteLine("The winner is " + Piece.Instance.currentPlayer);
                Piece.Instance.winner = Piece.Instance.currentPlayer;
                //display the score
                displayScore();
                return true;
            }
            //continue the game
            else
            {
                return false;
            }
            
        }

        //save all movements
        public void saveHistory(int inputnumber, int cnt, int x, int y)
        {
            const string FILENAME = "GameHistory.txt";
            const string DELIM = ",";
            int listNum;
            string[] fields;
            
            //read file
            var lines = File.ReadAllLines(FILENAME);
            if (lines.Count() > 1)
            {
                //find the last line
                string line = lines.Last();
                fields = line.Split(DELIM);

                for (int i = 0; i < lines.Count(); ++i)
                {   // if line is new check the previous line
                    if (line.Contains("new"))
                    {
                        line = lines[i - 1];
                        fields = line.Split(DELIM);
                        break;
                    }
                }

                //extract the last inputNumber
                listNum = Convert.ToInt32(fields[0]);
                Piece.Instance.inputNubmer = listNum + 1;
            }

            StreamWriter writer = File.AppendText(FILENAME);

            //detachment - game
            if (cnt == 0)
            {
                writer.WriteLine("new");
            }
          
            //if current player is user(player)
            if (Piece.Instance.currentPlayer == 1)
            {
                //check the value
                if (PlayerData.Instance.playerName == null)
                {
                    PlayerData.Instance.playerName = "unkown";
                }
                //save the history
                writer.WriteLine(Piece.Instance.inputNubmer + ","+System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")+","+cnt + "," + PlayerData.Instance.playerName+","+ PlayerData.Instance.playerType+","+ PlayerData.Instance.playerColour+","+x+","+y+","+Piece.Instance.position[x,y]+","+Piece.Instance.currentPlayer);
            }
            //if current player is partner
            else
            {
                //check the value
                if (PlayerData.Instance.partnerName == null)
                {
                    PlayerData.Instance.partnerName = "unkown";
                }
                //save the history
                writer.WriteLine(Piece.Instance.inputNubmer + "," + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "," + cnt + "," + PlayerData.Instance.partnerName + "," + PlayerData.Instance.partnerType + "," + PlayerData.Instance.partnerColour + "," + x + "," + y + "," + Piece.Instance.position[x, y] + "," + Piece.Instance.currentPlayer);
            }
            writer.Close();
        }

        //refresh the game
        public void refresh()
        {
            //create a new baord
            Board b = new Board();
            b.createBoard();
            
            //initialize position data
            for (int i = 0; i < Piece.Instance.boardSize; ++i)
            {
                for (int j = 0; j < Piece.Instance.boardSize; ++j)
                {
                    Piece.Instance.position[i, j] = 0;
                }
            }
            Piece.Instance.currentPlayer = 1;
            cnt = 0;
        }
        
        //redo
        public void redo(Board b) {
            //get redo position
            int x = GameData.Instance.redo[0, 1];
            int y = GameData.Instance.redo[0, 2];
            int position = GameData.Instance.redo[0, 3];
            //set position
            Piece.Instance.position[x, y] = position;
            //mark position
            b.markPosition();
            //continue the game
            process(b);
        }
        //undo
        public void undo(Board b) {
            //get undo position
            int x = GameData.Instance.redo[0, 1];
            int y = GameData.Instance.redo[0, 2];
            //set position
            Piece.Instance.position[x, y] = 0;
            //mark position
            b.markPosition();
            //continue the game
            process(b);
        }

        //start the game with previous game
        public void restartGame()
        {
            //setting
            int num = PlayerData.Instance.gameStatus;
            // if player is human set Player data
            if (GameData.Instance.data[num, 4].Equals("Human")){               
                    PlayerData.Instance.playerName = GameData.Instance.data[num - 1, 4];
                    PlayerData.Instance.playerType = Convert.ToInt32(GameData.Instance.data[num - 1, 5]);
                    PlayerData.Instance.playerColour = Convert.ToInt32(GameData.Instance.data[num - 1, 6]);
                    PlayerData.Instance.partnerName = GameData.Instance.data[num, 4];
                    PlayerData.Instance.partnerColour = Convert.ToInt32(GameData.Instance.data[num, 6]);
                    PlayerData.Instance.partnerType = Convert.ToInt32(GameData.Instance.data[num, 5]);        
            }
            // if player is Computer set Player data
            else if (GameData.Instance.data[num, 3].Equals("Computer"))
            {               
                    PlayerData.Instance.playerName = GameData.Instance.data[num - 1, 4];
                    PlayerData.Instance.playerType = Convert.ToInt32(GameData.Instance.data[num - 1, 5]);
                    PlayerData.Instance.playerColour = Convert.ToInt32(GameData.Instance.data[num - 1, 6]);
                    PlayerData.Instance.partnerName = GameData.Instance.data[num, 4];
                    PlayerData.Instance.partnerColour = Convert.ToInt32(GameData.Instance.data[num, 6]);
                    PlayerData.Instance.partnerType = Convert.ToInt32(GameData.Instance.data[num, 5]);                
            }
            // if player has name set PlayerData
            else
            {
                PlayerData.Instance.playerName = GameData.Instance.data[num, 4];
                    PlayerData.Instance.playerType = Convert.ToInt32(GameData.Instance.data[num, 5]);
                    PlayerData.Instance.playerColour = Convert.ToInt32(GameData.Instance.data[num, 6]);
                    PlayerData.Instance.partnerName = GameData.Instance.data[num - 1, 4];
                    PlayerData.Instance.partnerColour = Convert.ToInt32(GameData.Instance.data[num - 1, 6]);
                    PlayerData.Instance.partnerType = Convert.ToInt32(GameData.Instance.data[num - 1, 5]);
            }

            //set the current player
            if (Convert.ToInt32(GameData.Instance.data[num, 9]) == 1)
            {
                Piece.Instance.currentPlayer = 2;
            }
            else
            {
                Piece.Instance.currentPlayer = 1;
            }

            //set the position 
            for (int i = num; i > 0; --i)
            {
                if(GameData.Instance.data[i, 0] != "1")
                {
                    int x = Convert.ToInt32(GameData.Instance.data[i, 7]);
                    int y = Convert.ToInt32(GameData.Instance.data[i, 8]);
                    int point = Convert.ToInt32(GameData.Instance.data[i, 9]);
                    Piece.Instance.position[x, y] = point;
                }
                else
                {
                    break;
                }
            }

            //create board
            Board b = new Board();
            //mark position
            b.markPosition();
            //continue the game
            process(b);
        }
        //display the score
        public void displayScore()
        {
            Score s = new Score();
            s.calculateScore();
            WriteLine("Score P1 : P2>> " + GameData.Instance.score[0, 0] + " : " + GameData.Instance.score[1, 0]);
        }

       
    }
}
