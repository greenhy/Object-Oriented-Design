using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Gomoku1
{
    class OnlineHelp
    {
       
        public void displayeGameRule()
        {
            WriteLine("The example of game");
            WriteLine("Reference:https://en.wikipedia.org/wiki/Gomoku");
            WriteLine("This game on the 15×15 board is adapted from the paper \"Go-Moku and Threat-Space Search\".[7]\nThe opening moves show clearly black's advantage. \nAn open row of three (one that is not blocked by an opponent's stone at either end) has to be blocked immediately, or countered with a threat elsewhere on the board. \nIf not blocked or countered, the open row of three will be extended to an open row of four, which threatens to win in two ways.\nWhite has to block open rows of three at moves 10, 14, 16 and 20, but black only has to do so at move 9. \nMove 20 is a blunder for white(it should have been played next to black 19). \nBlack can now force a win against any defence by white, starting with move 21.\nSecond game(continuation from first game)\nThere are two forcing sequences for black, depending on whether white 22 is played next to black 15 or black 21. \nThe diagram on the right shows the first sequence. \nAll the moves for white are forced.Such long forcing sequences are typical in gomoku, \nand expert players can read out forcing sequences of 20 to 40 moves rapidly and accurately.\nOther second game\nThe diagram on the right shows the second forcing sequence. \nThis diagram shows why white 20 was a blunder; \nif it had been next to black 19(at the position of move 32 in this diagram) then black 31 would not be a threat and so the forcing sequence would fail.");
        }
        
        public void displayFAQ()
        {
            WriteLine("Please visit our website: https://www.abcd.com");
            WriteLine("Q1: question");
            WriteLine("A1: answer");
        }

    }
}
