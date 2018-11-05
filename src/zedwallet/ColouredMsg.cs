// Copyright (c) 2018, The TurtleCoin Developers
// 
// Please see the included LICENSE file for more information.






public class ColouredMsg
{
		public ColouredMsg(string msg, Common.Console.Color colour)
		{
			this.msg = msg;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.colour = colour;
			this.colour.CopyFrom(colour);
		}

		public ColouredMsg(string msg, int padding, Common.Console.Color colour)
		{
			this.msg = msg;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: this.colour = colour;
			this.colour.CopyFrom(colour);
			this.padding = padding;
			this.pad = true;
		}


		/* Set the text colour, write the message, then reset. We use a class
		   as it seems the only way to have a valid << operator. We need this
		   so we can nicely do something like:

		   std::cout << "Hello " << GreenMsg("user") << std::endl;

		   Without having to write:

		   std::cout << "Hello ";
		   GreenMsg("user");
		   std::cout << std::endl; */

//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
//ORIGINAL LINE: friend std::ostream& operator <<(std::ostream& os, const ColouredMsg &m)
		public static std::ostream operator << (std::ostream os, ColouredMsg m)
		{
			Common.Console.setTextColor(m.colour);

			if (m.pad)
			{
				os << std::left << std::setw(m.padding) << m.msg;
			}
			else
			{
				os << m.msg;
			}

			Common.Console.setTextColor(Common.Console.Color.Default);
			return os;
		}

		protected string msg;
		protected readonly Common.Console.Color colour = new Common.Console.Color();
		protected readonly int padding = 0;
		protected readonly bool pad = false;
}

public class SuccessMsg : ColouredMsg
{
		public SuccessMsg(string msg) : base(msg, Common.Console.Color.Green)
		{
		}

		public SuccessMsg(string msg, int padding) : base(msg, padding, Common.Console.Color.Green)
		{
		}
}

public class InformationMsg : ColouredMsg
{
		public InformationMsg(string msg) : base(msg, Common.Console.Color.BrightYellow)
		{
		}

		public InformationMsg(string msg, int padding) : base(msg, padding, Common.Console.Color.BrightYellow)
		{
		}
}

public class SuggestionMsg : ColouredMsg
{
		public SuggestionMsg(string msg) : base(msg, Common.Console.Color.BrightYellow)
		{
		}

		public SuggestionMsg(string msg, int padding) : base(msg, padding, Common.Console.Color.BrightYellow)
		{
		}
}

public class WarningMsg : ColouredMsg
{
		public WarningMsg(string msg) : base(msg, Common.Console.Color.BrightRed)
		{
		}

		public WarningMsg(string msg, int padding) : base(msg, padding, Common.Console.Color.BrightRed)
		{
		}
}
