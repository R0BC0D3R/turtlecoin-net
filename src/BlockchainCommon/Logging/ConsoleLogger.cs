// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using Color = System.ConsoleColor;
using System;
using System.Collections.Generic;

namespace Logging
{

    public class ConsoleLogger : CommonLogger
    {
        private object mutex = new object();

        public ConsoleLogger(Level level) : base(level)
        { }

        //C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
        private Dictionary<string, Color> doLogString_colorMapping = new Dictionary<string, Color>()
        {
          {GlobalMembers.BLUE, Color.DarkBlue},
          {GlobalMembers.GREEN, Color.DarkGreen},
          {GlobalMembers.RED, Color.DarkRed},
          {GlobalMembers.YELLOW, Color.DarkYellow},
          {GlobalMembers.WHITE, Color.White},
          {GlobalMembers.CYAN, Color.DarkCyan},
          {GlobalMembers.MAGENTA, Color.DarkMagenta},
          {GlobalMembers.BRIGHT_BLUE, Color.Blue},
          {GlobalMembers.BRIGHT_GREEN, Color.Green},
          {GlobalMembers.BRIGHT_RED, Color.Red},
          {GlobalMembers.BRIGHT_YELLOW, Color.Yellow},
          {GlobalMembers.BRIGHT_WHITE, Color.White},
          {GlobalMembers.BRIGHT_CYAN, Color.Cyan},
          {GlobalMembers.BRIGHT_MAGENTA, Color.Magenta},
          {GlobalMembers.DEFAULT, Color.Gray}
        };

        protected override void doLogString(string message)
        {
            bool readingText = false;
            lock (mutex)
            {
                readingText = true;
            }
            bool changedColor = false;
            string color = "";

            //C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
            //	static ClassicUnorderedMap<string, Color> colorMapping = { { BLUE, Color::Blue }, { GREEN, Color::Green }, { RED, Color::Red }, { YELLOW, Color::Yellow }, { WHITE, Color::White }, { CYAN, Color::Cyan }, { MAGENTA, Color::Magenta }, { BRIGHT_BLUE, Color::BrightBlue }, { BRIGHT_GREEN, Color::BrightGreen }, { BRIGHT_RED, Color::BrightRed }, { BRIGHT_YELLOW, Color::BrightYellow }, { BRIGHT_WHITE, Color::BrightWhite }, { BRIGHT_CYAN, Color::BrightCyan }, { BRIGHT_MAGENTA, Color::BrightMagenta }, { DEFAULT, Color::Default } };

            for (int charPos = 0; charPos < message.Length; ++charPos)
            {
                if (message[charPos] == COLOR_DELIMETER)
                {
                    readingText = !readingText;
                    color += message[charPos];
                    if (readingText)
                    {
                        Color it = doLogString_colorMapping[color];
                        //C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
                        Console.ForegroundColor = it;
                        changedColor = true;
                        color = "";
                    }
                }
                else if (readingText)
                {
                    Console.Write(message[charPos]);
                }
                else
                {
                    color += message[charPos];
                }
            }

            if (changedColor)
            {
                Console.ForegroundColor = doLogString_colorMapping[GlobalMembers.DEFAULT];
            }
        }
    }
}