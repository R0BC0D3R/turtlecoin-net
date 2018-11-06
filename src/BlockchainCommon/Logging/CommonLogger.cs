// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System;
using System.Collections.Generic;

namespace Logging
{
    public class CommonLogger : ILogger
    {
        public char COLOR_DELIMETER { get; set; }
        public List<string> LEVEL_NAMES { get; set; }

        protected SortedSet<string> disabledCategories = new SortedSet<string>();
        protected Level logLevel;
        protected string pattern;

        //void operator ()(string category, Level level, DateTime time, string body);


        protected CommonLogger(Level level)
        {
            this.logLevel = level;
            this.pattern = "%D %T %L [%C] ";
        }


        public void FunctorMethod(string category, Level level, DateTime time, string body)
        {
            if ((level <= logLevel) && disabledCategories.Count == 0)
            {
                string body2 = body;
                if (!string.IsNullOrEmpty(pattern))
                {
                    int insertPos = 0;
                    if (!string.IsNullOrEmpty(body2) && body2[0] == COLOR_DELIMETER)
                    {
                        int delimPos = body2.IndexOf(COLOR_DELIMETER, 1);
                        if (delimPos != -1)
                        {
                            insertPos = delimPos + 1;
                        }
                    }

                    body2 = body2.Insert(insertPos, GlobalMembers.FormatPattern(pattern, category, level, time));
                }

                doLogString(body2);
            }
        }
        public virtual void EnableCategory(string category)
        {
            disabledCategories.Remove(category);
        }
        public virtual void DisableCategory(string category)
        {
            disabledCategories.Add(category);
        }
        public virtual void SetMaxLevel(Level level)
        {
            logLevel = level;
        }

        public void SetPattern(string pattern)
        {
            this.pattern = pattern;
        }


        protected virtual void doLogString(string message)
        {
        }
    }
}