using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    /// <summary>
    /// A class declared with the "static" attribute can only contain
    /// static methods and properties.  No instance of this class will ever exist.
    ///
    /// Essentially, this is a collection of logically connected global functions and variables
    /// It is a very NOT Object Oriented way of writing code which is still very useful.
    ///
    /// Important lesson: Not everything needs to be an Object.  In this case, the TextFormatter
    /// class is just a container for a bunch of related functions.
    /// </summary>
    public static class TextFormatter
    {

        #region Color Definitions
        // Color Definitions use the inline conditional to use good colors
        // regardless of whether you use Light or Dark mode for your terminal.
        // They might not work well if you use a custom background color for your terminal...

        /// <summary>
        /// Warning messages in Yellow
        /// </summary>
        public static ConsoleColor Warning = Console.BackgroundColor == ConsoleColor.White ? ConsoleColor.DarkYellow : ConsoleColor.Yellow;

        /// <summary>
        /// Danger messages in Red
        /// </summary>
        public static ConsoleColor Danger = Console.BackgroundColor == ConsoleColor.White ? ConsoleColor.DarkRed : ConsoleColor.Red;

        /// <summary>
        /// Positive messages in Green
        /// </summary>
        public static ConsoleColor Positive = Console.BackgroundColor == ConsoleColor.White ? ConsoleColor.DarkGreen : ConsoleColor.Green;

        /// <summary>
        /// Neutral messages in the default text color for your theme (Black on White or White on Black)
        /// </summary>
        public static ConsoleColor Neutral = Console.BackgroundColor == ConsoleColor.White ? ConsoleColor.Black : ConsoleColor.White;

        /// <summary>
        /// Special messages in Blue
        /// </summary>
        public static ConsoleColor Special = Console.BackgroundColor == ConsoleColor.White ? ConsoleColor.DarkCyan : ConsoleColor.Cyan;

        #endregion // Color Definitions

        /// <summary>
        /// print a message in a particular color.
        /// </summary>
        /// <param name="color">Any color you want!</param>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintLine(ConsoleColor color, string message, params object[] stuff)
        {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message, stuff);
            Console.ForegroundColor = prev;
        }

        /// <summary>
        /// Print a message in the predefined Warning color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintLineWarning(string message, params object[] stuff) =>
            PrintLine(Warning, message, stuff);

        /// <summary>
        /// Print a message in the predefined Danger color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintLineDanger(string message, params object[] stuff) =>
            PrintLine(Danger, message, stuff);

        /// <summary>
        /// Print a message in the predefined Positive color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintLinePositive(string message, params object[] stuff) =>
            PrintLine(Positive, message, stuff);

        /// <summary>
        /// Print a message in the predefined Special color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintLineSpecial(string message, params object[] stuff) =>
            PrintLine(Special, message, stuff);


        /// <summary>
        /// print a message in a particular color.
        /// </summary>
        /// <param name="color">Any color you want!</param>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void Print(ConsoleColor color, string message, params object[] stuff)
        {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(message, stuff);
            Console.ForegroundColor = prev;
        }

        /// <summary>
        /// Print a message in the predefined Warning color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintWarning(string message, params object[] stuff) =>
            Print(Warning, message, stuff);

        /// <summary>
        /// Print a message in the predefined Danger color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintDanger(string message, params object[] stuff) =>
            Print(Danger, message, stuff);

        /// <summary>
        /// Print a message in the predefined Positive color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintPositive(string message, params object[] stuff) =>
            Print(Positive, message, stuff);

        /// <summary>
        /// Print a message in the predefined Special color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stuff"></param>
        public static void PrintSpecial(string message, params object[] stuff) =>
            Print(Special, message, stuff);

    }
}
