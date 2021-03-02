using System;
using static System.Console;

namespace Gomoku1
{
    class Program
    {
        static void Main(string[] args)
        {
            //set the game environment(object and sequnce diagram part1)
            Player player = new Player();
            //select the game status
            player.selectGameStatus();
            //save the game status
            PlayerData.Instance.gameStatus = player.GameStatus;
            //as i mentioned in Player class just put directly.
            PlayerData.Instance.gameType = 1;

            //if player want to play a new game
            if (player.GameStatus == 0)
            {
                //select the player type
                player.selectPlayerType();
                //select the stone colour
                int colour = player.selectColour();
                //select the player name
                player.selectPlayerName();

                //set the player type, colour, name
                PlayerData.Instance.playerType = player.PlayerType;
                PlayerData.Instance.playerColour = player.Colour;
                PlayerData.Instance.playerName = player.PlayerName;

                //if player want to play with human
                if (player.PlayerType == 1)
                {
                    //set partner's information
                    HumanPartner partner = new HumanPartner();
                    partner.selectPlayerType();
                    partner.selectColour(colour);
                    partner.selectPlayerName();

                    PlayerData.Instance.partnerType = partner.PlayerType;
                    PlayerData.Instance.partnerColour = partner.Colour;
                    PlayerData.Instance.partnerName = partner.PlayerName;

                }
                //if player want to play with computer
                else
                {
                    //set partner's information
                    Computer com = new Computer();
                    com.selectPlayerType();
                    com.selectColour(colour);
                    com.selectPlayerName();
                    player.selectDifficulty();

                    PlayerData.Instance.partnerType = com.PlayerType;
                    PlayerData.Instance.partnerColour = com.Colour;
                    PlayerData.Instance.partnerName = com.PlayerName;
                    PlayerData.Instance.difficulty = player.Difficulty;
                }
                //start the game
                if (PlayerData.Instance.gameType == 1)
                {
                    GomokuGame g = new GomokuGame();
                    CommandG gameOnCommand = new GomokuGameOnCommand(g);
                    Game game = new Game();
                    game.setCommand(gameOnCommand);
                    game.start();
                }
            }
            //if player select previous game
            else
            {
                WriteLine(GameData.Instance.data[PlayerData.Instance.gameStatus,1]);
                //restart the game
                if (PlayerData.Instance.gameType == 1)
                {
                    GomokuGame g = new GomokuGame();
                    CommandG gameOnCommand = new GomokuGameOnCommand(g);
                    Game game = new Game();
                    game.setCommand(gameOnCommand);
                    game.restart();
                }
            }

        }
    }
}
