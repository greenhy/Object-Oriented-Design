using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.IO;
using System.Linq;

namespace Gomoku1
{
    class Player
    {
        public int PlayerType { get; set; }
        public int Colour { get; set; }
        public string PlayerName { get; set; }
        public int Difficulty { get; set; }
        public int GameStatus { get; set; }
        public int GameType { get; set; }

        //to select the previous game
        public int selectGameStatus()
        {
            //Read File which has game history
            const char DELIM = ',';
            const string FILENAME = "GameHistory.txt";
            FileStream inFile = new FileStream(FILENAME, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            int lineCount = File.ReadAllLines(FILENAME).Count();

            string recordIn;
            string[] fields;

            

            //prompt to player to select the game
            WriteLine("Select the game in the below list if you want to restart previous game\ninput the input number \nif you want to start a new game please enter 0 :");
            WriteLine("{0,-20}{1,-5}{2,8}{3,18}", "Input Number", "Date", "Order", "Player Name");
            
            int cnt = 0;
            recordIn = reader.ReadLine();
            if (recordIn != null) {
                GameData.Instance.data = new string[lineCount, 10];
                while (recordIn != null)
                {
                    //split the data and display
                    fields = recordIn.Split(DELIM);
                    if (fields.Length == 1 || fields.Length == 0)
                    {
                        WriteLine("New Game");
                        GameData.Instance.data[cnt, 0] = Convert.ToString("1");
                        cnt++;
                    }
                    else
                    {
                        WriteLine("{0,-10}{1,-10}{2,5}{3,10}", fields[0], fields[1], fields[2], fields[3]);

                        //to save the game history which is selected to mark positions and save the players information and save the game data
                        GameData.Instance.data[cnt, 1] = Convert.ToString(fields[0]);//inputnumber
                        GameData.Instance.data[cnt, 2] = Convert.ToString(fields[1]);//date
                        GameData.Instance.data[cnt, 3] = Convert.ToString(fields[2]);//order
                        GameData.Instance.data[cnt, 4] = Convert.ToString(fields[3]);//player name
                        GameData.Instance.data[cnt, 5] = Convert.ToString(fields[4]);//player type
                        GameData.Instance.data[cnt, 6] = Convert.ToString(fields[5]);//player colour
                        GameData.Instance.data[cnt, 7] = Convert.ToString(fields[6]);//x position
                        GameData.Instance.data[cnt, 8] = Convert.ToString(fields[7]);//y position
                        GameData.Instance.data[cnt, 9] = Convert.ToString(fields[8]);//mark
                        ++cnt;
                        
                    }
                    recordIn = reader.ReadLine();
                }
                reader.Close();
                inFile.Close();               

                GameStatus = Convert.ToInt32(ReadLine());

                // check the value is vaild
                while (GameStatus > cnt || GameStatus < 0)
                {
                    WriteLine("invaild input.");
                    WriteLine("Select the game in the below list if you want to restart previous game\ninput the input number \nif you want to start a new game please enter 0 :");
                    GameStatus = Convert.ToInt32(ReadLine());
                }
                WriteLine("You select {0}.", GameStatus);

                //display the selected game date
                if (GameStatus != 0)
                {
                    WriteLine(GameData.Instance.data[GameStatus, 2]);
                }
            }
            // if there is no record just start a new game
            else
            {
                WriteLine("There are no record.");
                GameStatus = 0;
                reader.Close();
                inFile.Close();
            }         
            
           //save the game status
            PlayerData.Instance.gameStatus = GameStatus;
            
            return GameStatus;
        }

        //to select the player type
        public int selectPlayerType()
        {

            WriteLine("Select your partner (1.Human 2.Computer) please write 1 or 2:");
            PlayerType = Convert.ToInt32(ReadLine());

            //check the vlaue is valid
            while ((PlayerType != 1) && (PlayerType != 2))
            {
                WriteLine("{0} is invaild value.\nSelect your partner (1.Human 2.Computer) please write 1 or 2:", PlayerType);
                PlayerType = Convert.ToInt32(ReadLine());
            }
            WriteLine("You select {0}.", PlayerType);
            return PlayerType;
        }

        //to select stone colour
        public int selectColour()
        {

            WriteLine("Select your colour (1.Black 2.White) please write 1 or 2:");
            Colour = Convert.ToInt32(ReadLine());

            //check the value is vaild
            while ((Colour != 1) && (Colour != 2))
            {
                WriteLine("{0} is invaild value.\nSelect your colour (1.Black 2.White) please write 1 or 2:", Colour);
                Colour = Convert.ToInt32(ReadLine());
            }
            WriteLine("You select {0}.", Colour);

            return Colour;
        }

        // to select player name
        public string selectPlayerName()
        {

            WriteLine("Select your name:");
            PlayerName = ReadLine();
            WriteLine("Your name is {0}.", PlayerName);

            return PlayerName;
        }

        // to select difficulty of the game when play with a computer
        public int selectDifficulty()
        {
            WriteLine("Select the difficualty (1.Easy 2.Normal 3.Hard) please write 1 or 2 or 3:");
            Difficulty = Convert.ToInt32(ReadLine());

            //check the value is vaild
            while ((Difficulty != 1) && (Difficulty != 2) && (Difficulty != 3))
            {
                WriteLine("{0} is invaild value.\nSelect the difficualty (1.Easy 2.Normal 3.Hard) please write 1 or 2 or 3:", Difficulty);
                Difficulty = Convert.ToInt32(ReadLine());
            }
            WriteLine("You select {0}.", Difficulty);

            return Difficulty;
        }

        //to select game type in this program game type is always 1(gomoku) so don't need to use this method
        //just avoid error put the game type directly in the main
      /*  public int selectGameType()
        {
            WriteLine("Select the game type (1.gomoku 2.othelo 3.mills) please write 1 or 2 or 3:");
            GameType = Convert.ToInt32(ReadLine());

            //check the value is vaild
            while ((GameType != 1) && (GameType != 2) && (GameType != 3))
            {
                WriteLine("{0} is invaild value.\nSSelect the game type (1.gomoku 2.othelo 3.mils) please write 1 or 2 or 3:", GameType);
                GameType = Convert.ToInt32(ReadLine());
            }
            WriteLine("You select {0}.", GameType);
            PlayerData.Instance.gameType=GameType;
            return GameType;
        }*/
    }
}
