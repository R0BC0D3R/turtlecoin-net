// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { inline bool operator==(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) == 0; } inline bool operator!=(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) != 0; } }
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_HASHABLE(type) CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { static_assert(sizeof(size_t) <= sizeof(type), "Size of " #type " must be at least that of size_t"); inline size_t hash_value(const type &_v) { return reinterpret_cast<const size_t &>(_v); } } namespace std { template<> struct hash<Crypto::type> { size_t operator()(const Crypto::type &_v) const { return reinterpret_cast<const size_t &>(_v); } }; }
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER (CN_SOFT_SHELL_MEMORY / 2)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_PAD_MULTIPLIER (CN_SOFT_SHELL_WINDOW / CN_SOFT_SHELL_MULTIPLIER)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER_MULTIPLIER (CN_SOFT_SHELL_PAD_MULTIPLIER / 2)

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//  class ISerializer;

  /************************************************************************/
  /*                                                                      */
  /************************************************************************/
  public class AccountBase
  {
	//-----------------------------------------------------------------
	public AccountBase()
	{
	  setNull();
	}
	//-----------------------------------------------------------------
	public void generate()
	{

	  Crypto.generate_keys(m_keys.address.spendPublicKey, m_keys.spendSecretKey);

	  /* We derive the view secret key by taking our spend secret key, hashing
	     with keccak-256, and then using this as the seed to generate a new set
	     of keys - the public and private view keys. See generate_deterministic_keys */

	  generateViewFromSpend(m_keys.spendSecretKey, m_keys.viewSecretKey, m_keys.address.viewPublicKey);
	  m_creation_timestamp = time(null);

	}
	public static void generateViewFromSpend(Crypto.SecretKey spend, Crypto.SecretKey viewSecret, Crypto.PublicKey viewPublic)
	{
	  Crypto.SecretKey viewKeySeed = new Crypto.SecretKey();

	  GlobalMembers.keccak((ushort) spend, sizeof(Crypto.SecretKey), (ushort) viewKeySeed, sizeof(Crypto.SecretKey));

	  Crypto.generate_deterministic_keys(viewPublic, viewSecret, viewKeySeed);
	}
	public static void generateViewFromSpend(Crypto.SecretKey spend, Crypto.SecretKey viewSecret)
	{
	  /* If we don't need the pub key */
	  Crypto.PublicKey unused_dummy_variable = new Crypto.PublicKey();
	  generateViewFromSpend(spend, viewSecret, unused_dummy_variable);
	}

	//-----------------------------------------------------------------
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const AccountKeys &getAccountKeys() const
	public AccountKeys getAccountKeys()
	{
	  return m_keys;
	}
	public void setAccountKeys(AccountKeys keys)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_keys = keys;
	  m_keys.CopyFrom(keys);
	}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: ulong get_createtime() const
	public ulong get_createtime()
	{
		return m_creation_timestamp;
	}
	public void set_createtime(ulong val)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_creation_timestamp = val;
		m_creation_timestamp.CopyFrom(val);
	}
	//-----------------------------------------------------------------

	public void serialize(ISerializer s)
	{
	  s.functorMethod(m_keys, "m_keys");
	  s.functorMethod(m_creation_timestamp, "m_creation_timestamp");
	}

//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template <class t_archive>
	public void serialize<t_archive>(t_archive a, uint UnnamedParameter)
	{
	  a m_keys;
	  a m_creation_timestamp;
	}

	//-----------------------------------------------------------------
	private void setNull()
	{
	  m_keys = new AccountKeys();
	}
	private AccountKeys m_keys = new AccountKeys();
	private ulong m_creation_timestamp = new ulong();
  }
}

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ROTL64(x, y) (((x) << (y)) | ((x) >> (64 - (y))))

