// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// This file is part of Bytecoin.
//
// Bytecoin is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Bytecoin is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// This file is part of Bytecoin.
//
// Bytecoin is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Bytecoin is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with Bytecoin.  If not, see <http://www.gnu.org/licenses/>.


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl

namespace Logging
{

public class StreamLogger : CommonLogger
{
  public StreamLogger(Level level = DEBUGGING) : base.functorMethod(level)
  {
	  this.stream = null;
  }
  public StreamLogger(std::ostream stream, Level level = DEBUGGING) : base.functorMethod(level)
  {
	  this.stream = stream;
  }
  public void attachToStream(std::ostream stream)
  {
	this.stream = stream;
  }

  protected override void doLogString(string message)
  {
	if (stream != null && stream.good())
	{
	  lock (mutex)
	  {
		  bool readingText = true;
	  }
	  for (size_t charPos = 0; charPos < message.Length; ++charPos)
	  {
		if (message[charPos] == base.COLOR_DELIMETER)
		{
		  readingText = !readingText;
		}
		else if (readingText)
		{
		  stream << message[charPos];
		}
	  }

	  stream << std::flush;
	}
  }

  protected std::ostream stream;

  private object mutex = new object();
}

}


