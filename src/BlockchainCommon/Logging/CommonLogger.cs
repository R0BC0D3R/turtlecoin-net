using System.Collections.Generic;

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

public class CommonLogger : ILogger
{

  public static void functorMethod(string category, Level level, boost::posix_time.ptime time, string body)
  {
	if (level <= ((int)logLevel) != 0 && disabledCategories.count(category) == 0)
	{
	  string body2 = body;
	  if (!string.IsNullOrEmpty(pattern))
	  {
		size_t insertPos = 0;
		if (!string.IsNullOrEmpty(body2) && body2[0] == base.COLOR_DELIMETER)
		{
		  size_t delimPos = body2.IndexOf(base.COLOR_DELIMETER, 1);
		  if (delimPos != -1)
		  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: insertPos = delimPos + 1;
			insertPos.CopyFrom(delimPos + 1);
		  }
		}

		body2 = body2.Insert(insertPos, GlobalMembers.formatPattern(pattern, category, level, new boost::posix_time.ptime(time)));
	  }

	  doLogString(body2);
	}
  }
  public virtual void enableCategory(string category)
  {
	disabledCategories.erase(category);
  }
  public virtual void disableCategory(string category)
  {
	disabledCategories.Add(category);
  }
  public virtual void setMaxLevel(Level level)
  {
	logLevel = level;
  }

  public void setPattern(string pattern)
  {
	this.pattern = pattern;
  }

  protected SortedSet<string> disabledCategories = new SortedSet<string>();
  protected Level logLevel;
  protected string pattern;

  protected CommonLogger(Level level)
  {
	  this.logLevel = new Logging.Level(level);
	  this.pattern = "%D %T %L [%C] ";
  }
  protected virtual void doLogString(string message)
  {
  }
}

}


namespace Logging
{

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//namespace


}
