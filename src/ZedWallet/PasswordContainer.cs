// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using System;

namespace Tools
{
  public partial class PasswordContainer : System.IDisposable
  {
	public const uint max_password_size = 1024;

	public PasswordContainer()
	{
		this.m_empty = true;
	}
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public PasswordContainer(string && password)
	{
		this.m_empty = false;
		this.m_password = std::move(password);
	}
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public PasswordContainer(PasswordContainer && rhs)
	{
		this.m_empty = std::move(rhs.m_empty);
		this.m_password = std::move(rhs.m_password);
	}
	public void Dispose()
	{
	  clear();
	}

	public void clear()
	{
	  if (0 < m_password.capacity())
	  {
		m_password = m_password.replace(0, m_password.capacity(), m_password.capacity(), '\0');
		m_password.resize(0);
	  }
	  m_empty = true;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool empty() const
	public bool empty()
	{
		return m_empty;
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const string& password() const
	public string password()
	{
		return m_password;
	}
//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
	public void password(string && val)
	{
		m_password = std::move(val);
									   m_empty = false;
	}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	bool read_and_validate();
	public bool read_and_validate(string msg)
	{
	  string tmpPassword = m_password;

	  if (msg == "")
	  {
		  if (!read_password())
		  {
			  Console.Write(WarningMsg("Failed to read password!"));
			  Console.Write("\n");
			  return false;
		  }
	  }
	  else
	  {
		  if (!read_password(false, msg))
		  {
			  Console.Write(WarningMsg("Failed to read password!"));
			  Console.Write("\n");
			  return false;
		  }
	  }

	  bool validPass = m_password == tmpPassword;

	  m_password = tmpPassword;

	  return validPass;
	}
	public bool read_password()
	{
	  return read_password(false, "Enter password: ");
	}
	public bool read_password(bool verify, string msg)
	{
	  clear();

	  bool r;
	  if (GlobalMembers.is_cin_tty())
	  {
		Console.Write(InformationMsg(msg));

		if (verify)
		{
		  string password1;
		  string password2;
		  r = read_from_tty(password1);
		  if (r)
		  {
			Console.Write(InformationMsg("Confirm your new password: "));
			r = read_from_tty(password2);
			if (r)
			{
			  if (password1 == password2)
			  {
				m_password = std::move(password2);
				m_empty = false;
				  return true;
			  }
			  else
			  {
				Console.Write(WarningMsg("Passwords do not match, try again."));
				Console.Write("\n");
				clear();
				  return read_password(true, msg);
			  }
			}
		  }
		}
		else
		{
			r = read_from_tty(m_password);
		}
	  }
	  else
	  {
		r = read_from_file();
	  }

	  if (r)
	  {
		m_empty = false;
	  }
	  else
	  {
		clear();
	  }

	  return r;
	}

	private bool read_from_file()
	{
	  m_password.reserve(max_password_size);
	  for (uint i = 0; i < max_password_size; ++i)
	  {
		char ch = (char)Console.Read();
		if (std::cin.eof() || ch == '\n' || ch == '\r')
		{
		  break;
		}
		else if (std::cin.fail())
		{
		  return false;
		}
		else
		{
		  m_password.push_back(ch);
		}
	  }

	  return true;
	}
	private bool read_from_tty(string password)
	{
	  const char BACKSPACE = 8;

	  IntPtr h_cin = global::GetStdHandle(STD_INPUT_HANDLE);

	  uint mode_old;
	  global::GetConsoleMode(h_cin, mode_old);
	  uint mode_new = mode_old & ~(ENABLE_ECHO_INPUT | ENABLE_LINE_INPUT);
	  global::SetConsoleMode(h_cin, mode_new);

	  bool r = true;
	  password.reserve(max_password_size);
	  while (password.Length < max_password_size)
	  {
		uint read;
		char ch;
		r = (1 == global::ReadConsoleA(h_cin, ch, 1, read, null));
		r &= (1 == read);
		if (!r)
		{
		  break;
		}
		else if (ch == '\n' || ch == '\r')
		{
		  Console.Write("\n");
		  break;
		}
		else if (ch == BACKSPACE)
		{
		  if (!string.IsNullOrEmpty(password))
		  {
			password.back() = '\0';
			password.resize(password.Length - 1);
			Console.Write("\b \b");
		  }
		}
		else
		{
		  password.push_back(ch);
		  Console.Write('*');
		}
	  }

	  global::SetConsoleMode(h_cin, mode_old);

	  return r;
	}

	private bool m_empty;
	private string m_password;
  }
}

//////////////////////////////


#if _WIN32
#else
#endif


namespace Tools
{
//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//  namespace

#if _WIN32

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//  namespace

#else

//C++ TO C# CONVERTER NOTE: C# does not allow anonymous namespaces:
//  namespace

//C++ TO C# CONVERTER WARNING: The original C++ declaration of the following method implementation was not found:
//ORIGINAL LINE: bool PasswordContainer::read_from_tty(string& password)

#endif
}


namespace Tools
{
public partial class PasswordContainer
{
  public bool read_from_tty(string password)
  {
	const char BACKSPACE = 127;

	password.reserve(max_password_size);
	while (password.Length < max_password_size)
	{
	  int ch = GlobalMembers.getch();
	  if (EOF == ch)
	  {
		return false;
	  }
	  else if (ch == '\n' || ch == '\r')
	  {
		Console.Write("\n");
		break;
	  }
	  else if (ch == BACKSPACE)
	  {
		if (!string.IsNullOrEmpty(password))
		{
		  password.back() = '\0';
		  password.resize(password.Length - 1);
		  Console.Write("\b \b");
		}
	  }
	  else
	  {
		password.push_back(ch);
		Console.Write('*');
	  }
	}

	return true;
  }
}
}