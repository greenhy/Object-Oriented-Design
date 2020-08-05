using System;
using static System.Console;
using System.IO;


namespace FileComparison
{
    class Program
    {
        static void Main(string[] args)
        {
            const string WORD_FILE = @"C:\test.doc";
            const string NOTEPAD_FILE = @"C:\test.txt";
            long wordSize;
            long txtSize;
            double ratio;
            FileInfo wordInfo = new FileInfo(WORD_FILE);
            FileInfo notepadInfo = new FileInfo(NOTEPAD_FILE);
            wordSize = wordInfo.Length;
            txtSize = notepadInfo.Length;
            WriteLine("The size of the Word file: " + wordSize +
            "\nThe size of the txt file: " + txtSize);
            ratio = (double) txtSize / wordSize;
            WriteLine("The txt file is {0} of the size of the Word file",
            ratio.ToString("P2"));
        }
    }
}
