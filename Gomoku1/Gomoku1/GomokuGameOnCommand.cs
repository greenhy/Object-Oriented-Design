using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku1
{
    class GomokuGameOnCommand : CommandG
    {
        private GomokuGame game;

        public GomokuGameOnCommand(GomokuGame game)
        {
            this.game = game;
        }

        public void excute()
        {
            game.startGame();
        }

        public void restart()
        {
            game.restartGame();
        }
    }
}
