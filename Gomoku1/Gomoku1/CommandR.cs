using System;
using System.Collections.Generic;
using System.Text;

namespace Gomoku1
{
    interface CommandR
    {
        public bool finish();
        public int[] moveCom();
    }
}
