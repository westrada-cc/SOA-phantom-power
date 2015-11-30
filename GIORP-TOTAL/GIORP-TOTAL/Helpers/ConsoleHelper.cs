using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIORP_TOTAL.Helpers
{
    public static class ConsoleHelper
    {
        /// <summary>
        /// Reads a key from console until it is Y or N is entered buy user. 
        /// Returns true if Y is pressed and false if N is pressed.
        /// </summary>
        /// <returns></returns>
        public static bool ReadConfirm()
        {
            ConsoleKey? input = null;
            while(true)
            {
                input = Console.ReadKey().Key;
                if (input == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    return true;
                }
                if (input == ConsoleKey.N)
                {
                    Console.WriteLine();
                    return false;
                }
            }
        }
    }
}
