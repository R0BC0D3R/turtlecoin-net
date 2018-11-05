// Copyright (c) 2018, The TurtleCoin Developers
// 
// Please see the included LICENSE file for more information.





public class WalletInfo
{
	public WalletInfo(string walletFileName, string walletPass, string walletAddress, bool viewWallet, CryptoNote.WalletGreen wallet)
	{
		this.walletFileName = walletFileName;
		this.walletPass = walletPass;
		this.walletAddress = walletAddress;
		this.viewWallet = viewWallet;
		this.wallet = wallet;
	}

	/* How many transactions do we know about */
	public uint knownTransactionCount = 0;

	/* The wallet file name */
	public string walletFileName;

	/* The wallet password */
	public string walletPass;

	/* The wallets primary address */
	public string walletAddress;

	/* Is the wallet a view only wallet */
	public bool viewWallet;

	/* The walletgreen wallet container */
	public CryptoNote.WalletGreen wallet;
}

public class Config
{
	/* Was the wallet file specified on CLI */
	public bool walletGiven = false;

	/* Was the wallet pass specified on CLI */
	public bool passGiven = false;

	/* Should we log walletd logs to a file */
	public bool debug = false;

	/* The daemon host */
	public string host = "127.0.0.1";

	/* The daemon port */
	public int port = CryptoNote.RPC_DEFAULT_PORT;

	/* The wallet file path */
	public string walletFile = "";

	/* The wallet password */
	public string walletPass = "";
}

public class AddressBookEntry
{
	public AddressBookEntry()
	{
	}

	/* Used for quick comparison with strings */
	public AddressBookEntry(string friendlyName)
	{
		this.friendlyName = friendlyName;
	}

	public AddressBookEntry(string friendlyName, string address, string paymentID, bool integratedAddress)
	{
		this.friendlyName = friendlyName;
		this.address = address;
		this.paymentID = paymentID;
		this.integratedAddress = integratedAddress;
	}

	/* Friendly name for this address book entry */
	public string friendlyName;

	/* The wallet address of this entry */
	public string address;

	/* The payment ID associated with this address */
	public string paymentID;

	/* Did the user enter this as an integrated address? (We need this to
	   display back the address as either an integrated address, or an
	   address + payment ID pair */
	public bool integratedAddress;

	public void serialize(CryptoNote.ISerializer s)
	{
		KV_MEMBER(friendlyName) KV_MEMBER(address) KV_MEMBER(paymentID) KV_MEMBER(integratedAddress)
	}

	/* Only compare via name as we don't really care about the contents */
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool operator ==(const AddressBookEntry &rhs) const
	public static bool operator == (AddressBookEntry ImpliedObject, AddressBookEntry rhs)
	{
		return rhs.friendlyName == ImpliedObject.friendlyName;
	}
}

/* An address book is a vector of address book entries */

/* This borrows from haskell, and is a nicer boost::optional class. We either
   have Just a value, or Nothing.

   Example usage follows.
   The below code will print:

   ```
   100
   Nothing
   ```

   Maybe<int> parseAmount(std::string input)
   {
        if (input.length() == 0)
        {
            return Nothing<int>();
        }

        try
        {
            return Just<int>(std::stoi(input)
        }
        catch (const std::invalid_argument &)
        {
            return Nothing<int>();
        }
   }

   int main()
   {
       auto foo = parseAmount("100");

       if (foo.isJust)
       {
           std::cout << foo.x << std::endl;
       }
       else
       {
           std::cout << "Nothing" << std::endl;
       }

       auto bar = parseAmount("garbage");

       if (bar.isJust)
       {
           std::cout << bar.x << std::endl;
       }
       else
       {
           std::cout << "Nothing" << std::endl;
       }
   }

*/

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <class X>
public class Maybe <X>
{
	public X x = new X();
	public bool isJust;

	public Maybe(X x)
	{
		this.x = x;
		this.isJust = true;
	}
	public Maybe()
	{
		this.isJust = false;
	}
}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <class X>