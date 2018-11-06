// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { inline bool operator==(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) == 0; } inline bool operator!=(const type &_v1, const type &_v2) { return std::memcmp(&_v1, &_v2, sizeof(type)) != 0; } }
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CRYPTO_MAKE_HASHABLE(type) CRYPTO_MAKE_COMPARABLE(type) namespace Crypto { static_assert(sizeof(uint) <= sizeof(type), "Size of " #type " must be at least that of uint"); inline uint hash_value(const type &_v) { return reinterpret_cast<const uint &>(_v); } } namespace std { template<> struct hash<Crypto::type> { uint operator()(const Crypto::type &_v) const { return reinterpret_cast<const uint &>(_v); } }; }
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER (CN_SOFT_SHELL_MEMORY / 2)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_PAD_MULTIPLIER (CN_SOFT_SHELL_WINDOW / CN_SOFT_SHELL_MULTIPLIER)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define CN_SOFT_SHELL_ITER_MULTIPLIER (CN_SOFT_SHELL_PAD_MULTIPLIER / 2)

namespace CryptoNote
{

public class CryptoNoteConnectionContext
{
  public byte version;
  public boost::uuids.uuid m_connection_id = new boost::uuids.uuid();
  public uint m_remote_ip = 0;
  public uint m_remote_port = 0;
  public bool m_is_income = false;
  public DateTime m_started = 0;

  public enum state
  {
	state_befor_handshake = 0, //default state
	state_synchronizing,
	state_idle,
	state_normal,
	state_sync_required,
	state_pool_sync_required,
	state_shutdown
  }

  public state m_state = state.state_befor_handshake;
  public LinkedList<Crypto.Hash> m_needed_objects = new LinkedList<Crypto.Hash>();
  public HashSet<Crypto.Hash> m_requested_objects = new HashSet<Crypto.Hash>();
  public uint m_remote_blockchain_height = 0;
  public uint m_last_response_height = 0;
}

}

