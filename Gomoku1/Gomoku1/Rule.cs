using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku1
{
    class Rule
    {
        private CommandR command;

        public void setCommand(CommandR command)
        {
            this.command = command;
        }

        public bool checkFinishCondition()
        {
            return command.finish();
        }
        public int[] computerMove()
        {
            return command.moveCom();
        }
    }

}
