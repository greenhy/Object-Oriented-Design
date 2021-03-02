using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku1
{
    class Game
    {
        private CommandG command;

        public void setCommand(CommandG command)
        {
            this.command = command;
        }

        public void start()
        {
            command.excute();
        }
        public void restart()
        {
            command.restart();
        }
    }

}
