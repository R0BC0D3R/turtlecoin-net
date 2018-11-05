// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using Color = Common.Console.Color;
using System;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl

namespace Logging
{

public class ConsoleLogger : CommonLogger
{
  public ConsoleLogger(Level level = DEBUGGING) : base.functorMethod(level)
  {
  }

//C++ TO C# CONVERTER NOTE: This was formerly a static local variable declaration (not allowed in C#):
  private Dictionary<string, Color> doLogString_colorMapping = new Dictionary<string, Color>()
  {
	  {BLUE, Color.Blue},
	  {GREEN, Color.Green},
	  {RED, Color.Red},
	  {YELLOW, Color.Yellow},
	  {WHITE, Color.White},
	  {CYAN, Color.Cyan},
	  {MAGENTA, Color.Magenta},
	  {BRIGHT_BLUE, Color.BrightBlue},
	  {BRIGHT_GREEN, Color.BrightGreen},
	  {BRIGHT_RED, Color.BrightRed},
	  {BRIGHT_YELLOW, Color.BrightYellow},
	  {BRIGHT_WHITE, Color.BrightWhite},
	  {BRIGHT_CYAN, Color.BrightCyan},
	  {BRIGHT_MAGENTA, Color.BrightMagenta},
	  {DEFAULT, Color.Default}
  };
  protected override void doLogString(string message)
  {
	lock (mutex)
	{
		bool readingText = true;
	}
	bool changedColor = false;
	string color = "";

//C++ TO C# CONVERTER NOTE: This static local variable declaration (not allowed in C#) has been moved just prior to the method:
//	static ClassicUnorderedMap<string, Color> colorMapping = { { BLUE, Color::Blue }, { GREEN, Color::Green }, { RED, Color::Red }, { YELLOW, Color::Yellow }, { WHITE, Color::White }, { CYAN, Color::Cyan }, { MAGENTA, Color::Magenta }, { BRIGHT_BLUE, Color::BrightBlue }, { BRIGHT_GREEN, Color::BrightGreen }, { BRIGHT_RED, Color::BrightRed }, { BRIGHT_YELLOW, Color::BrightYellow }, { BRIGHT_WHITE, Color::BrightWhite }, { BRIGHT_CYAN, Color::BrightCyan }, { BRIGHT_MAGENTA, Color::BrightMagenta }, { DEFAULT, Color::Default } };

	for (size_t charPos = 0; charPos < message.Length; ++charPos)
	{
	  if (message[charPos] == base.COLOR_DELIMETER)
	  {
		readingText = !readingText;
		color += message[charPos];
		if (readingText)
		{
		  var it = doLogString_colorMapping.find(color);
//C++ TO C# CONVERTER TODO TASK: Iterators are only converted within the context of 'while' and 'for' loops:
		  Common.Console.setTextColor(it == doLogString_colorMapping.end() ? Color.Default : it.second);
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
	  Common.Console.setTextColor(Color.Default);
	}
  }

  private object mutex = new object();
}

}



