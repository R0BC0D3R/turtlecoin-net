// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2016-2018, The Karbowanec developers
// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using System.Diagnostics;

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

namespace Crypto
{

public class EllipticCurvePoint
{
  public ushort[] data = Arrays.InitializeWithDefaultInstances<ushort>(32);
}

public class EllipticCurveScalar
{
  public ushort[] data = Arrays.InitializeWithDefaultInstances<ushort>(32);
}

  public class crypto_ops : System.IDisposable
  {
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	crypto_ops();
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	crypto_ops(crypto_ops UnnamedParameter);
//C++ TO C# CONVERTER NOTE: This 'CopyFrom' method was converted from the original copy assignment operator:
//ORIGINAL LINE: void operator =(const crypto_ops &);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void CopyFrom(crypto_ops UnnamedParameter);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	public void Dispose();

	private static void generate_keys(PublicKey pub, SecretKey sec)
	{
	  lock_guard<mutex> @lock = new lock_guard<mutex>(GlobalMembers.random_lock);
	  ge_p3 point = new ge_p3();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  Crypto.GlobalMembers.random_scalar(reinterpret_cast<EllipticCurveScalar&>(sec));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_scalarmult_base(point, reinterpret_cast<byte>(sec));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_p3_tobytes(reinterpret_cast<byte>(pub), point);
	}

	/* Generate a new key pair
	 */
	private void generate_keys(PublicKey pub, SecretKey sec)
	{
	  this.generate_keys(pub, sec);
	}
	private static void generate_deterministic_keys(PublicKey pub, ref SecretKey sec, SecretKey second)
	{
	  lock_guard<mutex> @lock = new lock_guard<mutex>(GlobalMembers.random_lock);
	  ge_p3 point = new ge_p3();
	  sec = second;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  sc_reduce32(reinterpret_cast<byte>(sec)); // reduce in case second round of keys (sendkeys)
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_scalarmult_base(point, reinterpret_cast<byte>(sec));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_p3_tobytes(reinterpret_cast<byte>(pub), point);
	}
	private void generate_deterministic_keys(PublicKey pub, SecretKey sec, SecretKey second)
	{
	  this.generate_deterministic_keys(pub, sec, second);
	}
	private static SecretKey generate_m_keys(PublicKey pub, ref SecretKey sec, SecretKey recovery_key = SecretKey(), bool recover = false)
	{
	  lock_guard<mutex> @lock = new lock_guard<mutex>(GlobalMembers.random_lock);
	  ge_p3 point = new ge_p3();
	  SecretKey rng = new SecretKey();
	  if (recover)
	  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: rng = recovery_key;
		rng.CopyFrom(recovery_key);
	  }
	  else
	  {
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		Crypto.GlobalMembers.random_scalar(reinterpret_cast<EllipticCurveScalar&>(rng));
	  }
	  sec = rng;
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  sc_reduce32(reinterpret_cast<byte>(sec)); // reduce in case second round of keys (sendkeys)
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_scalarmult_base(point, reinterpret_cast<byte>(sec));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_p3_tobytes(reinterpret_cast<byte>(pub), point);

	  return rng;
	}
	private SecretKey generate_m_keys(PublicKey pub, SecretKey sec, SecretKey recovery_key = SecretKey(), bool recover = false)
	{
	  return this.generate_m_keys(pub, sec, recovery_key, recover);
	}
	private static bool check_key(PublicKey key)
	{
	  ge_p3 point = new ge_p3();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  return ge_frombytes_vartime(point, reinterpret_cast<const byte>(key)) == 0;
	}

	/* Check a public key. Returns true if it is valid, false otherwise.
	 */
	private bool check_key(PublicKey key)
	{
	  return this.check_key(key);
	}
	private static bool secret_key_to_public_key(SecretKey sec, PublicKey pub)
	{
	  ge_p3 point = new ge_p3();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  if (sc_check(reinterpret_cast<const byte>(sec)) != 0)
	  {
		return false;
	  }
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_scalarmult_base(point, reinterpret_cast<const byte>(sec));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_p3_tobytes(reinterpret_cast<byte>(pub), point);
	  return true;
	}

	/* Checks a private key and computes the corresponding public key.
	 */
	private bool secret_key_to_public_key(SecretKey sec, PublicKey pub)
	{
	  return this.secret_key_to_public_key(sec, pub);
	}
	private static bool generate_key_derivation(PublicKey key1, SecretKey key2, KeyDerivation derivation)
	{
	  ge_p3 point = new ge_p3();
	  ge_p2 point2 = new ge_p2();
	  ge_p1p1 point3 = new ge_p1p1();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  Debug.Assert(sc_check(reinterpret_cast<const byte>(key2)) == 0);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  if (ge_frombytes_vartime(point, reinterpret_cast<const byte>(key1)) != 0)
	  {
		return false;
	  }
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_scalarmult(point2, reinterpret_cast<const byte>(key2), point);
	  ge_mul8(point3, point2);
	  ge_p1p1_to_p2(point2, point3);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_tobytes(reinterpret_cast<byte>(derivation), point2);
	  return true;
	}

	/* To generate an ephemeral key used to send money to:
	 * * The sender generates a new key pair, which becomes the transaction key. The public transaction key is included in "extra" field.
	 * * Both the sender and the receiver generate key derivation from the transaction key and the receivers' "view" key.
	 * * The sender uses key derivation, the output index, and the receivers' "spend" key to derive an ephemeral public key.
	 * * The receiver can either derive the public key (to check that the transaction is addressed to him) or the private key (to spend the money).
	 */
	private bool generate_key_derivation(PublicKey key1, SecretKey key2, KeyDerivation derivation)
	{
	  return this.generate_key_derivation(key1, key2, derivation);
	}
	private static bool derive_public_key(KeyDerivation derivation, size_t output_index, PublicKey @base, PublicKey derived_key)
	{
	  EllipticCurveScalar scalar = new EllipticCurveScalar();
	  ge_p3 point1 = new ge_p3();
	  ge_p3 point2 = new ge_p3();
	  ge_cached point3 = new ge_cached();
	  ge_p1p1 point4 = new ge_p1p1();
	  ge_p2 point5 = new ge_p2();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  if (ge_frombytes_vartime(point1, reinterpret_cast<const byte>(@base)) != 0)
	  {
		return false;
	  }
	  Crypto.GlobalMembers.derivation_to_scalar(derivation, new size_t(output_index), scalar);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_scalarmult_base(point2, reinterpret_cast<byte>(scalar));
	  ge_p3_to_cached(point3, point2);
	  ge_add(point4, point1, point3);
	  ge_p1p1_to_p2(point5, point4);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_tobytes(reinterpret_cast<byte>(derived_key), point5);
	  return true;
	}
	private bool derive_public_key(KeyDerivation derivation, size_t output_index, PublicKey @base, PublicKey derived_key)
	{
	  return this.derive_public_key(derivation, new size_t(output_index), @base, derived_key);
	}
	private bool derive_public_key(KeyDerivation derivation, size_t output_index, PublicKey @base, ushort prefix, size_t prefixLength, PublicKey derived_key)
	{
	  return this.derive_public_key(derivation, new size_t(output_index), @base, prefix, new size_t(prefixLength), derived_key);
	}
	private static bool derive_public_key(KeyDerivation derivation, size_t output_index, PublicKey @base, ushort suffix, size_t suffixLength, PublicKey derived_key)
	{
	  EllipticCurveScalar scalar = new EllipticCurveScalar();
	  ge_p3 point1 = new ge_p3();
	  ge_p3 point2 = new ge_p3();
	  ge_cached point3 = new ge_cached();
	  ge_p1p1 point4 = new ge_p1p1();
	  ge_p2 point5 = new ge_p2();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  if (ge_frombytes_vartime(point1, reinterpret_cast<const byte>(@base)) != 0)
	  {
		return false;
	  }
	  Crypto.GlobalMembers.derivation_to_scalar(derivation, new size_t(output_index), suffix, new size_t(suffixLength), scalar);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_scalarmult_base(point2, reinterpret_cast<byte>(scalar));
	  ge_p3_to_cached(point3, point2);
	  ge_add(point4, point1, point3);
	  ge_p1p1_to_p2(point5, point4);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_tobytes(reinterpret_cast<byte>(derived_key), point5);
	  return true;
	}
	//hack for pg
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	static bool underive_public_key_and_get_scalar(KeyDerivation UnnamedParameter, uint UnnamedParameter2, PublicKey UnnamedParameter3, PublicKey UnnamedParameter4, EllipticCurveScalar UnnamedParameter5);
	private bool underive_public_key_and_get_scalar(KeyDerivation derivation, uint output_index, PublicKey derived_key, PublicKey @base, EllipticCurveScalar hashed_derivation)
	{
	  return this.underive_public_key_and_get_scalar(derivation, output_index, derived_key, @base, hashed_derivation);
	}
	private static void generate_incomplete_key_image(PublicKey pub, EllipticCurvePoint incomplete_key_image)
	{
	  ge_p3 point = new ge_p3();
	  Crypto.GlobalMembers.hash_to_ec(pub, point);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_p3_tobytes(reinterpret_cast<byte>(incomplete_key_image), point);
	}
//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
//ORIGINAL LINE: friend void generate_incomplete_key_image(const PublicKey &, EllipticCurvePoint &);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void generate_incomplete_key_image(PublicKey UnnamedParameter, EllipticCurvePoint UnnamedParameter2);
	//
	private static void derive_secret_key(KeyDerivation derivation, size_t output_index, SecretKey @base, SecretKey derived_key)
	{
	  EllipticCurveScalar scalar = new EllipticCurveScalar();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  Debug.Assert(sc_check(reinterpret_cast<const byte>(@base)) == 0);
	  Crypto.GlobalMembers.derivation_to_scalar(derivation, new size_t(output_index), scalar);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  sc_add(reinterpret_cast<byte>(derived_key), reinterpret_cast<const byte>(@base), reinterpret_cast<byte>(scalar));
	}
//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
//ORIGINAL LINE: friend void derive_secret_key(const KeyDerivation &, size_t, const SecretKey &, SecretKey &);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void derive_secret_key(KeyDerivation UnnamedParameter, size_t UnnamedParameter2, SecretKey UnnamedParameter3, SecretKey UnnamedParameter4);
	private static void derive_secret_key(KeyDerivation derivation, size_t output_index, SecretKey @base, ushort suffix, size_t suffixLength, SecretKey derived_key)
	{
	  EllipticCurveScalar scalar = new EllipticCurveScalar();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  Debug.Assert(sc_check(reinterpret_cast<const byte>(@base)) == 0);
	  Crypto.GlobalMembers.derivation_to_scalar(derivation, new size_t(output_index), suffix, new size_t(suffixLength), scalar);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  sc_add(reinterpret_cast<byte>(derived_key), reinterpret_cast<const byte>(@base), reinterpret_cast<byte>(scalar));
	}
//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
//ORIGINAL LINE: friend void derive_secret_key(const KeyDerivation &, size_t, const SecretKey &, const ushort*, size_t, SecretKey &);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void derive_secret_key(KeyDerivation UnnamedParameter, size_t UnnamedParameter2, SecretKey UnnamedParameter3, ushort UnnamedParameter4, size_t UnnamedParameter5, SecretKey UnnamedParameter6);
	private static bool underive_public_key(KeyDerivation derivation, size_t output_index, PublicKey derived_key, PublicKey @base)
	{
	  EllipticCurveScalar scalar = new EllipticCurveScalar();
	  ge_p3 point1 = new ge_p3();
	  ge_p3 point2 = new ge_p3();
	  ge_cached point3 = new ge_cached();
	  ge_p1p1 point4 = new ge_p1p1();
	  ge_p2 point5 = new ge_p2();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  if (ge_frombytes_vartime(point1, reinterpret_cast<const byte>(derived_key)) != 0)
	  {
		return false;
	  }
	  Crypto.GlobalMembers.derivation_to_scalar(derivation, new size_t(output_index), scalar);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_scalarmult_base(point2, reinterpret_cast<byte>(scalar));
	  ge_p3_to_cached(point3, point2);
	  ge_sub(point4, point1, point3);
	  ge_p1p1_to_p2(point5, point4);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_tobytes(reinterpret_cast<byte>(@base), point5);
	  return true;
	}
	private bool underive_public_key(KeyDerivation derivation, size_t output_index, PublicKey derived_key, PublicKey @base)
	{
	  return this.underive_public_key(derivation, new size_t(output_index), derived_key, @base);
	}
	private static bool underive_public_key(KeyDerivation derivation, size_t output_index, PublicKey derived_key, ushort suffix, size_t suffixLength, PublicKey @base)
	{
	  EllipticCurveScalar scalar = new EllipticCurveScalar();
	  ge_p3 point1 = new ge_p3();
	  ge_p3 point2 = new ge_p3();
	  ge_cached point3 = new ge_cached();
	  ge_p1p1 point4 = new ge_p1p1();
	  ge_p2 point5 = new ge_p2();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  if (ge_frombytes_vartime(point1, reinterpret_cast<const byte>(derived_key)) != 0)
	  {
		return false;
	  }

	  Crypto.GlobalMembers.derivation_to_scalar(derivation, new size_t(output_index), suffix, new size_t(suffixLength), scalar);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_scalarmult_base(point2, reinterpret_cast<byte>(scalar));
	  ge_p3_to_cached(point3, point2);
	  ge_sub(point4, point1, point3);
	  ge_p1p1_to_p2(point5, point4);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_tobytes(reinterpret_cast<byte>(@base), point5);
	  return true;
	}

	/* Inverse function of derive_public_key. It can be used by the receiver to find which "spend" key was used to generate a transaction. This may be useful if the receiver used multiple addresses which only differ in "spend" key.
	 */
	private bool underive_public_key(KeyDerivation derivation, size_t output_index, PublicKey derived_key, ushort prefix, size_t prefixLength, PublicKey @base)
	{
	  return this.underive_public_key(derivation, new size_t(output_index), derived_key, prefix, new size_t(prefixLength), @base);
	}
	private static void generate_signature(Hash prefix_hash, PublicKey pub, SecretKey sec, Signature sig)
	{
	  lock_guard<mutex> @lock = new lock_guard<mutex>(GlobalMembers.random_lock);
	  ge_p3 tmp3 = new ge_p3();
	  EllipticCurveScalar k = new EllipticCurveScalar();
	  s_comm buf = new s_comm();
#if !NDEBUG
	  {
		ge_p3 t = new ge_p3();
		PublicKey t2 = new PublicKey();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		Debug.Assert(sc_check(reinterpret_cast<const byte>(sec)) == 0);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		ge_scalarmult_base(t, reinterpret_cast<const byte>(sec));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		ge_p3_tobytes(reinterpret_cast<byte>(t2), t);
		Debug.Assert(pub == t2);
	  }
#endif
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: buf.h = prefix_hash;
	  buf.h.CopyFrom(prefix_hash);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  buf.key = reinterpret_cast<const EllipticCurvePoint&>(pub);
	  Crypto.GlobalMembers.random_scalar(k);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_scalarmult_base(tmp3, reinterpret_cast<byte>(k));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_p3_tobytes(reinterpret_cast<byte>(buf.comm), tmp3);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  Crypto.GlobalMembers.hash_to_scalar(buf, sizeof(s_comm), reinterpret_cast<EllipticCurveScalar&>(sig));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  sc_mulsub(reinterpret_cast<byte>(sig) + 32, reinterpret_cast<byte>(sig), reinterpret_cast<const byte>(sec), reinterpret_cast<byte>(k));
	}

	/* Generation and checking of a standard signature.
	 */
	private void generate_signature(Hash prefix_hash, PublicKey pub, SecretKey sec, Signature sig)
	{
	  this.generate_signature(prefix_hash, pub, sec, sig);
	}
	private static bool check_signature(Hash prefix_hash, PublicKey pub, Signature sig)
	{
	  ge_p2 tmp2 = new ge_p2();
	  ge_p3 tmp3 = new ge_p3();
	  EllipticCurveScalar c = new EllipticCurveScalar();
	  s_comm buf = new s_comm();
	  Debug.Assert(check_key(pub));
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: buf.h = prefix_hash;
	  buf.h.CopyFrom(prefix_hash);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  buf.key = reinterpret_cast<const EllipticCurvePoint&>(pub);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  if (ge_frombytes_vartime(tmp3, reinterpret_cast<const byte>(pub)) != 0)
	  {
		abort();
	  }
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  if (sc_check(reinterpret_cast<const byte>(sig)) != 0 || sc_check(reinterpret_cast<const byte>(sig) + 32) != 0)
	  {
		return false;
	  }
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_double_scalarmult_base_vartime(tmp2, reinterpret_cast<const byte>(sig), tmp3, reinterpret_cast<const byte>(sig) + 32);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_tobytes(reinterpret_cast<byte>(buf.comm), tmp2);
	  Crypto.GlobalMembers.hash_to_scalar(buf, sizeof(s_comm), c);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  sc_sub(reinterpret_cast<byte>(c), reinterpret_cast<byte>(c), reinterpret_cast<const byte>(sig));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  return sc_isnonzero(reinterpret_cast<byte>(c)) == 0;
	}
	private bool check_signature(Hash prefix_hash, PublicKey pub, Signature sig)
	{
	  return this.check_signature(prefix_hash, pub, sig);
	}
	private static void generate_key_image(PublicKey pub, SecretKey sec, KeyImage image)
	{
	  ge_p3 point = new ge_p3();
	  ge_p2 point2 = new ge_p2();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  Debug.Assert(sc_check(reinterpret_cast<const byte>(sec)) == 0);
	  Crypto.GlobalMembers.hash_to_ec(pub, point);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_scalarmult(point2, reinterpret_cast<const byte>(sec), point);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_tobytes(reinterpret_cast<byte>(image), point2);
	}

	/* To send money to a key:
	 * * The sender generates an ephemeral key and includes it in transaction output.
	 * * To spend the money, the receiver generates a key image from it.
	 * * Then he selects a bunch of outputs, including the one he spends, and uses them to generate a ring signature.
	 * To check the signature, it is necessary to collect all the keys that were used to generate it. To detect double spends, it is necessary to check that each key image is used at most once.
	 */
	private void generate_key_image(PublicKey pub, SecretKey sec, KeyImage image)
	{
	  this.generate_key_image(pub, sec, image);
	}
	private static KeyImage scalarmultKey(KeyImage P, KeyImage a)
	{
	  ge_p3 A = new ge_p3();
	  ge_p2 R = new ge_p2();
	  // maybe use assert instead?
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_frombytes_vartime(A, reinterpret_cast<const byte>(P));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_scalarmult(R, reinterpret_cast<const byte>(a), A);
	  KeyImage aP = new KeyImage();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_tobytes(reinterpret_cast<byte>(aP), R);
	  return aP;
	}
	private KeyImage scalarmultKey(KeyImage P, KeyImage a)
	{
	  return this.scalarmultKey(P, a);
	}
	private static void hash_data_to_ec(ushort data, uint len, PublicKey key)
	{
	  Hash h = new Hash();
	  ge_p2 point = new ge_p2();
	  ge_p1p1 point2 = new ge_p1p1();
	  Crypto.GlobalMembers.cn_fast_hash(data, len, h);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_fromfe_frombytes_vartime(point, reinterpret_cast<const byte>(h));
	  ge_mul8(point2, point);
	  ge_p1p1_to_p2(point, point2);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  ge_tobytes(reinterpret_cast<byte>(key), point);
	}
	private void hash_data_to_ec(ushort data, uint len, PublicKey key)
	{
	  this.hash_data_to_ec(data, len, key);
	}
	private static void generate_ring_signature(Hash prefix_hash, KeyImage image, PublicKey[] pubs, size_t pubs_count, SecretKey sec, size_t sec_index, Signature[] sig)
	{
	  lock_guard<mutex> @lock = new lock_guard<mutex>(GlobalMembers.random_lock);
	  size_t i = new size_t();
	  ge_p3 image_unp = new ge_p3();
	  ge_dsmp image_pre = new ge_dsmp();
	  EllipticCurveScalar sum = new EllipticCurveScalar();
	  EllipticCurveScalar k = new EllipticCurveScalar();
	  EllipticCurveScalar h = new EllipticCurveScalar();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//C++ TO C# CONVERTER TODO TASK: The memory management function 'alloca' has no equivalent in C#:
	  rs_comm buf = reinterpret_cast<rs_comm>(alloca(Crypto.GlobalMembers.rs_comm_size(new size_t(pubs_count))));
	  Debug.Assert(sec_index < pubs_count);
#if !NDEBUG
	  {
		ge_p3 t = new ge_p3();
		PublicKey t2 = new PublicKey();
		KeyImage t3 = new KeyImage();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		Debug.Assert(sc_check(reinterpret_cast<const byte>(sec)) == 0);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		ge_scalarmult_base(t, reinterpret_cast<const byte>(sec));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		ge_p3_tobytes(reinterpret_cast<byte>(t2), t);
		Debug.Assert(pubs[sec_index] == t2);
		generate_key_image(pubs[sec_index], sec, t3);
		Debug.Assert(image == t3);
		for (i = 0; i < pubs_count; i++)
		{
		  Debug.Assert(check_key(pubs[i]));
		}
	  }
#endif
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  if (ge_frombytes_vartime(image_unp, reinterpret_cast<const byte>(image)) != 0)
	  {
		abort();
	  }
	  ge_dsm_precomp(image_pre, image_unp);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  sc_0(reinterpret_cast<byte>(sum));
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: buf->h = prefix_hash;
	  buf.h.CopyFrom(prefix_hash);
	  for (i = 0; i < pubs_count; i++)
	  {
		ge_p2 tmp2 = new ge_p2();
		ge_p3 tmp3 = new ge_p3();
		if (i == sec_index)
		{
		  Crypto.GlobalMembers.random_scalar(k);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  ge_scalarmult_base(tmp3, reinterpret_cast<byte>(k));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  ge_p3_tobytes(reinterpret_cast<byte>(buf.ab[i].a), tmp3);
		  Crypto.GlobalMembers.hash_to_ec(pubs[i], tmp3);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  ge_scalarmult(tmp2, reinterpret_cast<byte>(k), tmp3);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  ge_tobytes(reinterpret_cast<byte>(buf.ab[i].b), tmp2);
		}
		else
		{
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  Crypto.GlobalMembers.random_scalar(reinterpret_cast<EllipticCurveScalar&>(sig[i]));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  Crypto.GlobalMembers.random_scalar(*reinterpret_cast<EllipticCurveScalar>(reinterpret_cast<byte>(sig[i]) + 32));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  if (ge_frombytes_vartime(tmp3, reinterpret_cast<const byte>(pubs[i])) != 0)
		  {
			abort();
		  }
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  ge_double_scalarmult_base_vartime(tmp2, reinterpret_cast<byte>(sig[i]), tmp3, reinterpret_cast<byte>(sig[i]) + 32);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  ge_tobytes(reinterpret_cast<byte>(buf.ab[i].a), tmp2);
		  Crypto.GlobalMembers.hash_to_ec(pubs[i], tmp3);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  ge_double_scalarmult_precomp_vartime(tmp2, reinterpret_cast<byte>(sig[i]) + 32, tmp3, reinterpret_cast<byte>(sig[i]), image_pre);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  ge_tobytes(reinterpret_cast<byte>(buf.ab[i].b), tmp2);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		  sc_add(reinterpret_cast<byte>(sum), reinterpret_cast<byte>(sum), reinterpret_cast<byte>(sig[i]));
		}
	  }
	  Crypto.GlobalMembers.hash_to_scalar(buf, Crypto.GlobalMembers.rs_comm_size(new size_t(pubs_count)), h);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  sc_sub(reinterpret_cast<byte>(sig[sec_index]), reinterpret_cast<byte>(h), reinterpret_cast<byte>(sum));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  sc_mulsub(reinterpret_cast<byte>(sig[sec_index]) + 32, reinterpret_cast<byte>(sig[sec_index]), reinterpret_cast<const byte>(sec), reinterpret_cast<byte>(k));
	}
//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' function:
//ORIGINAL LINE: friend void generate_ring_signature(const Hash &, const KeyImage &, const PublicKey *const *, size_t, const SecretKey &, size_t, Signature *);
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//	void generate_ring_signature(Hash UnnamedParameter, KeyImage UnnamedParameter2, PublicKey[] UnnamedParameter3, size_t UnnamedParameter4, SecretKey UnnamedParameter5, size_t UnnamedParameter6, Signature UnnamedParameter7);
	private static bool check_ring_signature(Hash prefix_hash, KeyImage image, PublicKey[] pubs, size_t pubs_count, Signature[] sig, bool checkKeyImage)
	{
	  size_t i = new size_t();
	  ge_p3 image_unp = new ge_p3();
	  ge_dsmp image_pre = new ge_dsmp();
	  EllipticCurveScalar sum = new EllipticCurveScalar();
	  EllipticCurveScalar h = new EllipticCurveScalar();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
//C++ TO C# CONVERTER TODO TASK: The memory management function 'alloca' has no equivalent in C#:
	  rs_comm buf = reinterpret_cast<rs_comm>(alloca(Crypto.GlobalMembers.rs_comm_size(new size_t(pubs_count))));
#if !NDEBUG
	  for (i = 0; i < pubs_count; i++)
	  {
		Debug.Assert(check_key(pubs[i]));
	  }
#endif
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  if (ge_frombytes_vartime(image_unp, reinterpret_cast<const byte>(image)) != 0)
	  {
		return false;
	  }
	  ge_dsm_precomp(image_pre, image_unp);
	  if (checkKeyImage && ge_check_subgroup_precomp_vartime(image_pre) != 0)
	  {
		return false;
	  }
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  sc_0(reinterpret_cast<byte>(sum));
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: buf->h = prefix_hash;
	  buf.h.CopyFrom(prefix_hash);
	  for (i = 0; i < pubs_count; i++)
	  {
		ge_p2 tmp2 = new ge_p2();
		ge_p3 tmp3 = new ge_p3();
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		if (sc_check(reinterpret_cast<const byte>(sig[i])) != 0 || sc_check(reinterpret_cast<const byte>(sig[i]) + 32) != 0)
		{
		  return false;
		}
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		if (ge_frombytes_vartime(tmp3, reinterpret_cast<const byte>(pubs[i])) != 0)
		{
		  abort();
		}
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		ge_double_scalarmult_base_vartime(tmp2, reinterpret_cast<const byte>(sig[i]), tmp3, reinterpret_cast<const byte>(sig[i]) + 32);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		ge_tobytes(reinterpret_cast<byte>(buf.ab[i].a), tmp2);
		Crypto.GlobalMembers.hash_to_ec(pubs[i], tmp3);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		ge_double_scalarmult_precomp_vartime(tmp2, reinterpret_cast<const byte>(sig[i]) + 32, tmp3, reinterpret_cast<const byte>(sig[i]), image_pre);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		ge_tobytes(reinterpret_cast<byte>(buf.ab[i].b), tmp2);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
		sc_add(reinterpret_cast<byte>(sum), reinterpret_cast<byte>(sum), reinterpret_cast<const byte>(sig[i]));
	  }
	  Crypto.GlobalMembers.hash_to_scalar(buf, Crypto.GlobalMembers.rs_comm_size(new size_t(pubs_count)), h);
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  sc_sub(reinterpret_cast<byte>(h), reinterpret_cast<byte>(h), reinterpret_cast<byte>(sum));
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
	  return sc_isnonzero(reinterpret_cast<byte>(h)) == 0;
	}
	private bool check_ring_signature(Hash prefix_hash, KeyImage image, PublicKey[] pubs, size_t pubs_count, Signature sig, bool checkKeyImage)
	{
	  return this.check_ring_signature(prefix_hash, image, pubs, new size_t(pubs_count), sig, checkKeyImage);
	}
  }

  /* Generate a value filled with random bytes.
   */
//C++ TO C# CONVERTER TODO TASK: The original C++ template specifier was replaced with a C# generic specifier, which may not produce the same behavior:
//ORIGINAL LINE: template<typename T>
  public class random_engine <T>
  {

	public static T min()
	{
	  return (numeric_limits<T>.min)();
	}

	public static T max()
	{
	  return (numeric_limits<T>.max)();
	}

	public static std::enable_if<std::is_unsigned<T>.value, T>.type functorMethod()
	{
	  return Crypto.GlobalMembers.rand<T>();
	}
  }

}

namespace std
{
//C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//ORIGINAL LINE: struct hash<Crypto::PublicKey>
	public partial class hash
	{
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t operator ()(const Crypto::PublicKey &_v) const
		public static size_t functorMethod(Crypto.PublicKey _v)
		{
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
			return reinterpret_cast<const size_t &>(_v);
		}
	}
}
namespace std
{
//C++ TO C# CONVERTER TODO TASK: C++ template specialization was removed by C++ to C# Converter:
//ORIGINAL LINE: struct hash<Crypto::KeyImage>
	public partial class hash
	{
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t operator ()(const Crypto::KeyImage &_v) const
		public static size_t functorMethod(Crypto.KeyImage _v)
		{
//C++ TO C# CONVERTER TODO TASK: There is no equivalent to 'reinterpret_cast' in C#:
			return reinterpret_cast<const size_t &>(_v);
		}
	}
}


namespace Crypto
{



  public class s_comm
  {
	public Hash h = new Hash();
	public EllipticCurvePoint key = new EllipticCurvePoint();
	public EllipticCurvePoint comm = new EllipticCurvePoint();
  }

  public class rs_comm
  {
	public Hash h = new Hash();
//C++ TO C# CONVERTER NOTE: Classes must be named in C#, so the following class has been named AnonymousClass:
	public class AnonymousClass
	{
	  public EllipticCurvePoint a = new EllipticCurvePoint();
	  public EllipticCurvePoint b = new EllipticCurvePoint();
	}
	public AnonymousClass[] ab;
  }
}
