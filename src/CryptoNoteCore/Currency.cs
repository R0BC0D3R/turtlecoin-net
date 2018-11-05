//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define inline __inline
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define IDENT32(x) ((uint32_t) (x))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define IDENT64(x) ((uint64_t) (x))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32(x) ((((uint32_t) (x) & 0x000000ff) << 24) | (((uint32_t) (x) & 0x0000ff00) << 8) | (((uint32_t) (x) & 0x00ff0000) >> 8) | (((uint32_t) (x) & 0xff000000) >> 24))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64(x) ((((uint64_t) (x) & 0x00000000000000ff) << 56) | (((uint64_t) (x) & 0x000000000000ff00) << 40) | (((uint64_t) (x) & 0x0000000000ff0000) << 24) | (((uint64_t) (x) & 0x00000000ff000000) << 8) | (((uint64_t) (x) & 0x000000ff00000000) >> 8) | (((uint64_t) (x) & 0x0000ff0000000000) >> 24) | (((uint64_t) (x) & 0x00ff000000000000) >> 40) | (((uint64_t) (x) & 0xff00000000000000) >> 56))
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32LE IDENT32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32BE SWAP32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap32le ident32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap32be swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap32le mem_inplace_ident
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap32be mem_inplace_swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap32le memcpy_ident32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap32be memcpy_swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64LE IDENT64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64BE SWAP64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap64le ident64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap64be swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap64le mem_inplace_ident
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap64be mem_inplace_swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap64le memcpy_ident64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap64be memcpy_swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32BE IDENT32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP32LE SWAP32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap32be ident32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap32le swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap32be mem_inplace_ident
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap32le mem_inplace_swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap32be memcpy_ident32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap32le memcpy_swap32
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64BE IDENT64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SWAP64LE SWAP64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap64be ident64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define swap64le swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap64be mem_inplace_ident
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define mem_inplace_swap64le mem_inplace_swap64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap64be memcpy_ident64
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define memcpy_swap64le memcpy_swap64

//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define KV_MEMBER(member) s(member, #member);


using Logging;
using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
// 
// Please see the included LICENSE file for more information.

// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
// Copyright (c) 2014-2018, The Monero Project
// Copyright (c) 2018, The TurtleCoin Developers
// 
// Please see the included LICENSE file for more information.


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
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ENDL std::endl

namespace CryptoNote
{

//C++ TO C# CONVERTER NOTE: C# has no need of forward class declarations:
//class AccountBase;

public class Currency
{
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t maxBlockHeight() const
  public uint32_t maxBlockHeight()
  {
	  return m_maxBlockHeight;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t maxBlockBlobSize() const
  public size_t maxBlockBlobSize()
  {
	  return m_maxBlockBlobSize;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t maxTxSize() const
  public size_t maxTxSize()
  {
	  return m_maxTxSize;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t publicAddressBase58Prefix() const
  public uint64_t publicAddressBase58Prefix()
  {
	  return m_publicAddressBase58Prefix;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t minedMoneyUnlockWindow() const
  public uint32_t minedMoneyUnlockWindow()
  {
	  return m_minedMoneyUnlockWindow;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t timestampCheckWindow(uint32_t blockHeight) const
  public size_t timestampCheckWindow(uint32_t blockHeight)
  {
	  if (blockHeight >= CryptoNote.parameters.LWMA_2_DIFFICULTY_BLOCK_INDEX_V3)
	  {
		  return CryptoNote.parameters.BLOCKCHAIN_TIMESTAMP_CHECK_WINDOW_V3;
	  }
	  else
	  {
		  return m_timestampCheckWindow;
	  }
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t blockFutureTimeLimit(uint32_t blockHeight) const
  public uint64_t blockFutureTimeLimit(uint32_t blockHeight)
  {
	  if (blockHeight >= CryptoNote.parameters.LWMA_2_DIFFICULTY_BLOCK_INDEX_V2)
	  {
		  return CryptoNote.parameters.CRYPTONOTE_BLOCK_FUTURE_TIME_LIMIT_V4;
	  }
	  else if (blockHeight >= CryptoNote.parameters.LWMA_2_DIFFICULTY_BLOCK_INDEX)
	  {
		  return CryptoNote.parameters.CRYPTONOTE_BLOCK_FUTURE_TIME_LIMIT_V3;
	  }
	  else
	  {
		  return m_blockFutureTimeLimit;
	  }
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t moneySupply() const
  public uint64_t moneySupply()
  {
	  return m_moneySupply;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint emissionSpeedFactor() const
  public uint emissionSpeedFactor()
  {
	  return m_emissionSpeedFactor;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t genesisBlockReward() const
  public uint64_t genesisBlockReward()
  {
	  return m_genesisBlockReward;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t rewardBlocksWindow() const
  public size_t rewardBlocksWindow()
  {
	  return m_rewardBlocksWindow;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t zawyDifficultyBlockIndex() const
  public uint32_t zawyDifficultyBlockIndex()
  {
	  return m_zawyDifficultyBlockIndex;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t zawyDifficultyV2() const
  public size_t zawyDifficultyV2()
  {
	  return m_zawyDifficultyV2;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint8_t zawyDifficultyBlockVersion() const
  public uint8_t zawyDifficultyBlockVersion()
  {
	  return m_zawyDifficultyBlockVersion;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t blockGrantedFullRewardZone() const
  public size_t blockGrantedFullRewardZone()
  {
	  return m_blockGrantedFullRewardZone;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t blockGrantedFullRewardZoneByBlockVersion(uint8_t blockMajorVersion) const
  public size_t blockGrantedFullRewardZoneByBlockVersion(uint8_t blockMajorVersion)
  {
	if (blockMajorVersion >= BLOCK_MAJOR_VERSION_3)
	{
	  return m_blockGrantedFullRewardZone;
	}
	else if (blockMajorVersion == BLOCK_MAJOR_VERSION_2)
	{
	  return CryptoNote.parameters.CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE_V2;
	}
	else
	{
	  return CryptoNote.parameters.CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE_V1;
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t minerTxBlobReservedSize() const
  public size_t minerTxBlobReservedSize()
  {
	  return m_minerTxBlobReservedSize;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t numberOfDecimalPlaces() const
  public size_t numberOfDecimalPlaces()
  {
	  return m_numberOfDecimalPlaces;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t coin() const
  public uint64_t coin()
  {
	  return m_coin;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t minimumFee() const
  public uint64_t minimumFee()
  {
	  return m_mininumFee;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t defaultDustThreshold(uint32_t height) const
  public uint64_t defaultDustThreshold(uint32_t height)
  {
	  if (height >= CryptoNote.parameters.DUST_THRESHOLD_V2_HEIGHT)
	  {
		  return CryptoNote.parameters.DEFAULT_DUST_THRESHOLD_V2;
	  }

	  return m_defaultDustThreshold;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t defaultFusionDustThreshold(uint32_t height) const
  public uint64_t defaultFusionDustThreshold(uint32_t height)
  {
	  if (height >= CryptoNote.parameters.FUSION_DUST_THRESHOLD_HEIGHT_V2)
	  {
		  return CryptoNote.parameters.DEFAULT_DUST_THRESHOLD_V2;
	  }

	  return m_defaultDustThreshold;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t difficultyTarget() const
  public uint64_t difficultyTarget()
  {
	  return m_difficultyTarget;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t difficultyWindow() const
  public size_t difficultyWindow()
  {
	  return m_difficultyWindow;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t difficultyWindowByBlockVersion(uint8_t blockMajorVersion) const
public size_t difficultyWindowByBlockVersion(uint8_t blockMajorVersion)
{
  if (blockMajorVersion >= BLOCK_MAJOR_VERSION_3)
  {
	return m_difficultyWindow;
  }
  else if (blockMajorVersion == BLOCK_MAJOR_VERSION_2)
  {
	return CryptoNote.parameters.DIFFICULTY_WINDOW_V2;
  }
  else
  {
	return CryptoNote.parameters.DIFFICULTY_WINDOW_V1;
  }
}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t difficultyLag() const
  public size_t difficultyLag()
  {
	  return m_difficultyLag;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t difficultyLagByBlockVersion(uint8_t blockMajorVersion) const
public size_t difficultyLagByBlockVersion(uint8_t blockMajorVersion)
{
  if (blockMajorVersion >= BLOCK_MAJOR_VERSION_3)
  {
	return m_difficultyLag;
  }
  else if (blockMajorVersion == BLOCK_MAJOR_VERSION_2)
  {
	return CryptoNote.parameters.DIFFICULTY_LAG_V2;
  }
  else
  {
	return CryptoNote.parameters.DIFFICULTY_LAG_V1;
  }
}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t difficultyCut() const
  public size_t difficultyCut()
  {
	  return m_difficultyCut;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t difficultyCutByBlockVersion(uint8_t blockMajorVersion) const
public size_t difficultyCutByBlockVersion(uint8_t blockMajorVersion)
{
  if (blockMajorVersion >= BLOCK_MAJOR_VERSION_3)
  {
	return m_difficultyCut;
  }
  else if (blockMajorVersion == BLOCK_MAJOR_VERSION_2)
  {
	return CryptoNote.parameters.DIFFICULTY_CUT_V2;
  }
  else
  {
	return CryptoNote.parameters.DIFFICULTY_CUT_V1;
  }
}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t difficultyBlocksCount() const
  public size_t difficultyBlocksCount()
  {
	  return m_difficultyWindow + m_difficultyLag;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t difficultyBlocksCountByBlockVersion(uint8_t blockMajorVersion, uint32_t height) const
public size_t difficultyBlocksCountByBlockVersion(uint8_t blockMajorVersion, uint32_t height)
{
	if (height >= CryptoNote.parameters.LWMA_2_DIFFICULTY_BLOCK_INDEX)
	{
		return CryptoNote.parameters.DIFFICULTY_BLOCKS_COUNT_V3;
	}

	return difficultyWindowByBlockVersion(new uint8_t(blockMajorVersion)) + difficultyLagByBlockVersion(new uint8_t(blockMajorVersion));
}

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t maxBlockSizeInitial() const
  public size_t maxBlockSizeInitial()
  {
	  return m_maxBlockSizeInitial;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t maxBlockSizeGrowthSpeedNumerator() const
  public uint64_t maxBlockSizeGrowthSpeedNumerator()
  {
	  return m_maxBlockSizeGrowthSpeedNumerator;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t maxBlockSizeGrowthSpeedDenominator() const
  public uint64_t maxBlockSizeGrowthSpeedDenominator()
  {
	  return m_maxBlockSizeGrowthSpeedDenominator;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t lockedTxAllowedDeltaSeconds() const
  public uint64_t lockedTxAllowedDeltaSeconds()
  {
	  return m_lockedTxAllowedDeltaSeconds;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t lockedTxAllowedDeltaBlocks() const
  public size_t lockedTxAllowedDeltaBlocks()
  {
	  return m_lockedTxAllowedDeltaBlocks;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t mempoolTxLiveTime() const
  public uint64_t mempoolTxLiveTime()
  {
	  return m_mempoolTxLiveTime;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t mempoolTxFromAltBlockLiveTime() const
  public uint64_t mempoolTxFromAltBlockLiveTime()
  {
	  return m_mempoolTxFromAltBlockLiveTime;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t numberOfPeriodsToForgetTxDeletedFromPool() const
  public uint64_t numberOfPeriodsToForgetTxDeletedFromPool()
  {
	  return m_numberOfPeriodsToForgetTxDeletedFromPool;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t fusionTxMaxSize() const
  public size_t fusionTxMaxSize()
  {
	  return m_fusionTxMaxSize;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t fusionTxMinInputCount() const
  public size_t fusionTxMinInputCount()
  {
	  return m_fusionTxMinInputCount;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t fusionTxMinInOutCountRatio() const
  public size_t fusionTxMinInOutCountRatio()
  {
	  return m_fusionTxMinInOutCountRatio;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t upgradeHeight(uint8_t majorVersion) const
  public uint32_t upgradeHeight(uint8_t majorVersion)
  {
	if (majorVersion == BLOCK_MAJOR_VERSION_2)
	{
	  return m_upgradeHeightV2;
	}
	else if (majorVersion == BLOCK_MAJOR_VERSION_3)
	{
	  return m_upgradeHeightV3;
	}
	else if (majorVersion == BLOCK_MAJOR_VERSION_4)
	{
	  return m_upgradeHeightV4;
	}
	else
	{
	  return (uint32_t) - 1;
	}
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint upgradeVotingThreshold() const
  public uint upgradeVotingThreshold()
  {
	  return m_upgradeVotingThreshold;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t upgradeVotingWindow() const
  public uint32_t upgradeVotingWindow()
  {
	  return m_upgradeVotingWindow;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t upgradeWindow() const
  public uint32_t upgradeWindow()
  {
	  return m_upgradeWindow;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t minNumberVotingBlocks() const
  public uint32_t minNumberVotingBlocks()
  {
	  return (m_upgradeVotingWindow * m_upgradeVotingThreshold + 99) / 100;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t maxUpgradeDistance() const
  public uint32_t maxUpgradeDistance()
  {
	  return 7 * m_upgradeWindow;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint32_t calculateUpgradeHeight(uint32_t voteCompleteHeight) const
  public uint32_t calculateUpgradeHeight(uint32_t voteCompleteHeight)
  {
	  return voteCompleteHeight + m_upgradeWindow;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const string& blocksFileName() const
  public string blocksFileName()
  {
	  return m_blocksFileName;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const string& blockIndexesFileName() const
  public string blockIndexesFileName()
  {
	  return m_blockIndexesFileName;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const string& txPoolFileName() const
  public string txPoolFileName()
  {
	  return m_txPoolFileName;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isBlockexplorer() const
  public bool isBlockexplorer()
  {
	  return m_isBlockexplorer;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isTestnet() const
  public bool isTestnet()
  {
	  return m_testnet;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const BlockTemplate& genesisBlock() const
  public BlockTemplate genesisBlock()
  {
	  return cachedGenesisBlock.getBlock();
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const Crypto::Hash& genesisBlockHash() const
  public Crypto.Hash genesisBlockHash()
  {
	  return cachedGenesisBlock.getBlockHash();
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool getBlockReward(uint8_t blockMajorVersion, size_t medianSize, size_t currentBlockSize, uint64_t alreadyGeneratedCoins, uint64_t fee, uint64_t& reward, int64_t& emissionChange) const
  public bool getBlockReward(uint8_t blockMajorVersion, size_t medianSize, size_t currentBlockSize, uint64_t alreadyGeneratedCoins, uint64_t fee, ref uint64_t reward, ref int64_t emissionChange)
  {
	Debug.Assert(alreadyGeneratedCoins <= m_moneySupply);
	Debug.Assert(m_emissionSpeedFactor > 0 && m_emissionSpeedFactor <= 8 * sizeof(uint64_t));

	uint64_t baseReward = (m_moneySupply - alreadyGeneratedCoins) >> (int)m_emissionSpeedFactor;
	if (alreadyGeneratedCoins == 0 && m_genesisBlockReward != 0)
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: baseReward = m_genesisBlockReward;
	  baseReward.CopyFrom(m_genesisBlockReward);
	  Console.Write("Genesis block reward: ");
	  Console.Write(baseReward);
	  Console.Write("\n");
	}

	size_t blockGrantedFullRewardZone = blockGrantedFullRewardZoneByBlockVersion(new uint8_t(blockMajorVersion));
	medianSize = Math.Max(medianSize, blockGrantedFullRewardZone);
	if (currentBlockSize > UINT64_C(2) * medianSize)
	{
	  logger.functorMethod(TRACE) << "Block cumulative size is too big: " << currentBlockSize << ", expected less than " << 2 * medianSize;
	  return false;
	}

	uint64_t penalizedBaseReward = getPenalizedAmount(baseReward, medianSize, currentBlockSize);
	uint64_t penalizedFee = blockMajorVersion >= BLOCK_MAJOR_VERSION_2 ? getPenalizedAmount(fee, medianSize, currentBlockSize) : fee;

	emissionChange = penalizedBaseReward - (fee - penalizedFee);
	reward = penalizedBaseReward + penalizedFee;

	return true;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t maxBlockCumulativeSize(uint64_t height) const
  public size_t maxBlockCumulativeSize(uint64_t height)
  {
	Debug.Assert(height <= uint64_t.MaxValue / m_maxBlockSizeGrowthSpeedNumerator);
	size_t maxSize = (size_t)(m_maxBlockSizeInitial + (height * m_maxBlockSizeGrowthSpeedNumerator) / m_maxBlockSizeGrowthSpeedDenominator);
	Debug.Assert(maxSize >= m_maxBlockSizeInitial);
	return maxSize;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool constructMinerTx(uint8_t blockMajorVersion, uint32_t height, size_t medianSize, uint64_t alreadyGeneratedCoins, size_t currentBlockSize, uint64_t fee, const AccountPublicAddress& minerAddress, Transaction& tx, const BinaryArray& extraNonce = BinaryArray(), size_t maxOuts = 1) const
  public bool constructMinerTx(uint8_t blockMajorVersion, uint32_t height, size_t medianSize, uint64_t alreadyGeneratedCoins, size_t currentBlockSize, uint64_t fee, AccountPublicAddress minerAddress, Transaction tx, BinaryArray extraNonce = BinaryArray(), size_t maxOuts = 1)
  {

	tx.inputs.clear();
	tx.outputs.clear();
	tx.extra.clear();

	KeyPair txkey = generateKeyPair();
	addTransactionPublicKeyToExtra(tx.extra, txkey.publicKey);
	if (!extraNonce.empty())
	{
	  if (!addExtraNonceToTransactionExtra(tx.extra, extraNonce))
	  {
		return false;
	  }
	}

	BaseInput in = new BaseInput();
	in.blockIndex = height;

	uint64_t blockReward = new uint64_t();
	int64_t emissionChange = new int64_t();
	if (!getBlockReward(new uint8_t(blockMajorVersion), new size_t(medianSize), new size_t(currentBlockSize), new uint64_t(alreadyGeneratedCoins), new uint64_t(fee), ref blockReward, ref emissionChange))
	{
	  logger.functorMethod(INFO) << "Block is too big";
	  return false;
	}

	List<uint64_t> outAmounts = new List<uint64_t>();
	CryptoNote.GlobalMembers.decompose_amount_into_digits(new uint64_t(blockReward), defaultDustThreshold(new uint32_t(height)), (uint64_t a_chunk) =>
	{
		outAmounts.Add(a_chunk);
	}, (uint64_t a_dust) =>
	{
		outAmounts.Add(a_dust);
	});

	if (!(1 <= maxOuts))
	{
		logger.functorMethod(ERROR, BRIGHT_RED) << "max_out must be non-zero";
		return false;
	}
	while (maxOuts < outAmounts.Count)
	{
	  outAmounts[outAmounts.Count - 2] += outAmounts[outAmounts.Count - 1];
	  outAmounts.Resize(outAmounts.Count - 1);
	}

	uint64_t summaryAmounts = 0;
	for (size_t no = 0; no < outAmounts.Count; no++)
	{
	  Crypto.KeyDerivation derivation = boost::value_initialized<Crypto.KeyDerivation>();
	  Crypto.PublicKey outEphemeralPubKey = boost::value_initialized<Crypto.PublicKey>();

	  bool r = Crypto.generate_key_derivation(minerAddress.viewPublicKey, txkey.secretKey, derivation);

	  if (!(r))
	  {
		logger.functorMethod(ERROR, BRIGHT_RED) << "while creating outs: failed to generate_key_derivation(" << minerAddress.viewPublicKey << ", " << txkey.secretKey << ")";
		return false;
	  }

	  r = Crypto.derive_public_key(derivation, no, minerAddress.spendPublicKey, outEphemeralPubKey);

	  if (!(r))
	  {
		logger.functorMethod(ERROR, BRIGHT_RED) << "while creating outs: failed to derive_public_key(" << derivation << ", " << no << ", " << minerAddress.spendPublicKey << ")";
		return false;
	  }

	  KeyOutput tk = new KeyOutput();
	  tk.key = outEphemeralPubKey;

	  TransactionOutput @out = new TransactionOutput();
	  summaryAmounts += @out.amount = outAmounts[no];
	  @out.target = tk;
	  tx.outputs.push_back(@out);
	}

	if (!(summaryAmounts == blockReward))
	{
	  logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to construct miner tx, summaryAmounts = " << summaryAmounts << " not equal blockReward = " << blockReward;
	  return false;
	}

	tx.version = CURRENT_TRANSACTION_VERSION;
	//lock
	tx.unlockTime = height + m_minedMoneyUnlockWindow;
	tx.inputs.push_back(in);
	return true;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isFusionTransaction(const Transaction& transaction, uint32_t height) const
  public bool isFusionTransaction(Transaction transaction, uint32_t height)
  {
	return isFusionTransaction(transaction, CryptoNote.GlobalMembers.getObjectBinarySize(transaction), new uint32_t(height));
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isFusionTransaction(const Transaction& transaction, size_t size, uint32_t height) const
  public bool isFusionTransaction(Transaction transaction, size_t size, uint32_t height)
  {
	Debug.Assert(CryptoNote.GlobalMembers.getObjectBinarySize(transaction) == size);

	List<uint64_t> outputsAmounts = new List<uint64_t>();
	outputsAmounts.Capacity = transaction.outputs.size();
	foreach (TransactionOutput output in transaction.outputs)
	{
	  outputsAmounts.Add(output.amount);
	}

	return isFusionTransaction(getInputsAmounts(transaction), outputsAmounts, new size_t(size), new uint32_t(height));
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isFusionTransaction(const ClassicVector<uint64_t>& inputsAmounts, const ClassicVector<uint64_t>& outputsAmounts, size_t size, uint32_t height) const
  public bool isFusionTransaction(List<uint64_t> inputsAmounts, List<uint64_t> outputsAmounts, size_t size, uint32_t height)
  {
	if (size > fusionTxMaxSize())
	{
	  return false;
	}

	if (inputsAmounts.Count < fusionTxMinInputCount())
	{
	  return false;
	}

	if (inputsAmounts.Count < outputsAmounts.Count * fusionTxMinInOutCountRatio())
	{
	  return false;
	}

	uint64_t inputAmount = 0;
	foreach (var amount in inputsAmounts)
	{
	  if (amount < defaultFusionDustThreshold(new uint32_t(height)))
	  {
		return false;
	  }

	  inputAmount += amount;
	}

	List<uint64_t> expectedOutputsAmounts = new List<uint64_t>();
	expectedOutputsAmounts.Capacity = outputsAmounts.Count;
	decomposeAmount(inputAmount, defaultFusionDustThreshold(new uint32_t(height)), expectedOutputsAmounts);
	expectedOutputsAmounts.Sort();

	return expectedOutputsAmounts == outputsAmounts;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isAmountApplicableInFusionTransactionInput(uint64_t amount, uint64_t threshold, uint32_t height) const
  public bool isAmountApplicableInFusionTransactionInput(uint64_t amount, uint64_t threshold, uint32_t height)
  {
	uint8_t ignore = new uint8_t();
	return isAmountApplicableInFusionTransactionInput(new uint64_t(amount), new uint64_t(threshold), ref ignore, new uint32_t(height));
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isAmountApplicableInFusionTransactionInput(uint64_t amount, uint64_t threshold, uint8_t& amountPowerOfTen, uint32_t height) const
  public bool isAmountApplicableInFusionTransactionInput(uint64_t amount, uint64_t threshold, ref uint8_t amountPowerOfTen, uint32_t height)
  {
	if (amount >= threshold)
	{
	  return false;
	}

	if (amount < defaultFusionDustThreshold(new uint32_t(height)))
	{
	  return false;
	}

	var it = std::lower_bound(GlobalMembers.PRETTY_AMOUNTS.GetEnumerator(), GlobalMembers.PRETTY_AMOUNTS.end(), amount);
	if (it == GlobalMembers.PRETTY_AMOUNTS.end() || amount != *it)
	{
	  return false;
	}

	amountPowerOfTen = (uint8_t)(std::distance(GlobalMembers.PRETTY_AMOUNTS.GetEnumerator(), it) / 9);
	return true;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string accountAddressAsString(const AccountBase& account) const
  public string accountAddressAsString(AccountBase account)
  {
	return getAccountAddressAsStr(m_publicAddressBase58Prefix, account.getAccountKeys().address);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string accountAddressAsString(const AccountPublicAddress& accountPublicAddress) const
  public string accountAddressAsString(AccountPublicAddress accountPublicAddress)
  {
	return getAccountAddressAsStr(m_publicAddressBase58Prefix, accountPublicAddress);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool parseAccountAddressString(const string& str, AccountPublicAddress& addr) const
  public bool parseAccountAddressString(string str, AccountPublicAddress addr)
  {
	uint64_t prefix = new uint64_t();
	if (!CryptoNote.parseAccountAddressString(prefix, addr, str))
	{
	  return false;
	}

	if (prefix != m_publicAddressBase58Prefix)
	{
	  logger.functorMethod(DEBUGGING) << "Wrong address prefix: " << prefix << ", expected " << m_publicAddressBase58Prefix;
	  return false;
	}

	return true;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string formatAmount(uint64_t amount) const
  public string formatAmount(uint64_t amount)
  {
	string s = Convert.ToString(amount);
	if (s.Length < m_numberOfDecimalPlaces + 1)
	{
	  s = s.insert(0, m_numberOfDecimalPlaces + 1 - s.Length, '0');
	}
	s = s.Insert(s.Length - m_numberOfDecimalPlaces, ".");
	return s;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: string formatAmount(int64_t amount) const
  public string formatAmount(int64_t amount)
  {
	string s = formatAmount((uint64_t)Math.Abs(amount));

	if (amount < 0)
	{
	  s = s.Insert(0, "-");
	}

	return s;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool parseAmount(const string& str, uint64_t& amount) const
  public bool parseAmount(string str, uint64_t amount)
  {
	string strAmount = str;
	boost::algorithm.trim(strAmount);

	size_t pointIndex = strAmount.IndexOfAny((Convert.ToString('.')).ToCharArray());
	size_t fractionSize = new size_t();
	if (-1 != pointIndex)
	{
	  fractionSize = strAmount.Length - pointIndex - 1;
	  while (m_numberOfDecimalPlaces < fractionSize && '0' == strAmount.back())
	  {
		strAmount = strAmount.Remove(strAmount.Length - 1, 1);
		--fractionSize;
	  }
	  if (m_numberOfDecimalPlaces < fractionSize)
	  {
		return false;
	  }
	  strAmount = strAmount.Remove(pointIndex, 1);
	}
	else
	{
	  fractionSize = 0;
	}

	if (string.IsNullOrEmpty(strAmount))
	{
	  return false;
	}

	if (!std::all_of(strAmount.GetEnumerator(), strAmount.end(), global::isdigit))
	{
	  return false;
	}

	if (fractionSize < m_numberOfDecimalPlaces)
	{
	  strAmount.append(m_numberOfDecimalPlaces - fractionSize, '0');
	}

	return Common.GlobalMembers.fromString(strAmount, amount);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t getNextDifficulty(uint8_t version, uint32_t blockIndex, ClassicVector<uint64_t> timestamps, ClassicVector<uint64_t> cumulativeDifficulties) const
  public uint64_t getNextDifficulty(uint8_t version, uint32_t blockIndex, List<uint64_t> timestamps, List<uint64_t> cumulativeDifficulties)
  {
	  /* nextDifficultyV3 and above are defined in src/CryptoNoteCore/Difficulty.cpp */
	  if (blockIndex >= CryptoNote.parameters.LWMA_3_DIFFICULTY_BLOCK_INDEX)
	  {
		  return nextDifficultyV6(timestamps, cumulativeDifficulties);
	  }
	  else if (blockIndex >= CryptoNote.parameters.LWMA_2_DIFFICULTY_BLOCK_INDEX_V3)
	  {
		  return nextDifficultyV5(timestamps, cumulativeDifficulties);
	  }
	  else if (blockIndex >= CryptoNote.parameters.LWMA_2_DIFFICULTY_BLOCK_INDEX_V2)
	  {
		  return nextDifficultyV4(timestamps, cumulativeDifficulties);
	  }
	  else if (blockIndex >= CryptoNote.parameters.LWMA_2_DIFFICULTY_BLOCK_INDEX)
	  {
		  return nextDifficultyV3(timestamps, cumulativeDifficulties);
	  }
	  else
	  {
		  return nextDifficulty(new uint8_t(version), new uint32_t(blockIndex), new List<uint64_t>(timestamps), new List<uint64_t>(cumulativeDifficulties));
	  }
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint64_t nextDifficulty(uint8_t version, uint32_t blockIndex, ClassicVector<uint64_t> timestamps, ClassicVector<uint64_t> cumulativeDifficulties) const
  public uint64_t nextDifficulty(uint8_t version, uint32_t blockIndex, List<uint64_t> timestamps, List<uint64_t> cumulativeDifficulties)
  {

  List<uint64_t> timestamps_o = new List<uint64_t>(timestamps);
  List<uint64_t> cumulativeDifficulties_o = new List<uint64_t>(cumulativeDifficulties);
	size_t c_difficultyWindow = difficultyWindowByBlockVersion(new uint8_t(version));
	size_t c_difficultyCut = difficultyCutByBlockVersion(new uint8_t(version));

	Debug.Assert(c_difficultyWindow >= 2);

	if (timestamps.Count > c_difficultyWindow)
	{
	  timestamps.Resize(c_difficultyWindow);
	  cumulativeDifficulties.Resize(c_difficultyWindow);
	}

	size_t length = timestamps.Count;
	Debug.Assert(length == cumulativeDifficulties.Count);
	Debug.Assert(length <= c_difficultyWindow);
	if (length <= 1)
	{
	  return 1;
	}

	timestamps.Sort();

	size_t cutBegin = new size_t();
	size_t cutEnd = new size_t();
	Debug.Assert(2 * c_difficultyCut <= c_difficultyWindow - 2);
	if (length <= c_difficultyWindow - 2 * c_difficultyCut)
	{
	  cutBegin = 0;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: cutEnd = length;
	  cutEnd.CopyFrom(length);
	}
	else
	{
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: cutBegin = (length - (c_difficultyWindow - 2 * c_difficultyCut) + 1) / 2;
	  cutBegin.CopyFrom((length - (c_difficultyWindow - 2 * c_difficultyCut) + 1) / 2);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: cutEnd = cutBegin + (c_difficultyWindow - 2 * c_difficultyCut);
	  cutEnd.CopyFrom(cutBegin + (c_difficultyWindow - 2 * c_difficultyCut));
	}
	Debug.Assert(cutBegin + 2 <= cutEnd != null && cutEnd <= length);
	uint64_t timeSpan = timestamps[cutEnd - 1] - timestamps[cutBegin];
	if (timeSpan == 0)
	{
	  timeSpan = 1;
	}

	uint64_t totalWork = cumulativeDifficulties[cutEnd - 1] - cumulativeDifficulties[cutBegin];
	Debug.Assert(totalWork > 0);

	uint64_t low = new uint64_t();
	uint64_t high = new uint64_t();
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: low = mul128(totalWork, m_difficultyTarget, &high);
	low.CopyFrom(GlobalMembers.mul128(new uint64_t(totalWork), new uint64_t(m_difficultyTarget), high));
	if (high != 0 || uint64_t.MaxValue - low < (timeSpan - 1))
	{
	  return 0;
	}

	uint8_t c_zawyDifficultyBlockVersion = new uint8_t(m_zawyDifficultyBlockVersion);
	if (m_zawyDifficultyV2 != null)
	{
	  c_zawyDifficultyBlockVersion = 2;
	}
	if (version >= c_zawyDifficultyBlockVersion != null && c_zawyDifficultyBlockVersion != null)
	{
	  if (high != 0)
	  {
		return 0;
	  }
	  uint64_t nextDiffZ = low / timeSpan;

	  return nextDiffZ;
	}

	if (m_zawyDifficultyBlockIndex != null && m_zawyDifficultyBlockIndex <= blockIndex)
	{
	  if (high != 0)
	  {
		return 0;
	  }

  /*
    Recalculating 'low' and 'timespan' with hardcoded values:
    DIFFICULTY_CUT=0
    DIFFICULTY_LAG=0
    DIFFICULTY_WINDOW=17
  */
	  c_difficultyWindow = 17;
	  c_difficultyCut = 0;

	  Debug.Assert(c_difficultyWindow >= 2);

	  size_t t_difficultyWindow = new size_t(c_difficultyWindow);
	  if (c_difficultyWindow > timestamps.Count)
	  {
		t_difficultyWindow = timestamps.Count;
	  }
	  List<uint64_t> timestamps_tmp = new List<uint64_t>(timestamps_o.end() - t_difficultyWindow, timestamps_o.end());
	  List<uint64_t> cumulativeDifficulties_tmp = new List<uint64_t>(cumulativeDifficulties_o.end() - t_difficultyWindow, cumulativeDifficulties_o.end());

	  length = timestamps_tmp.Count;
	  Debug.Assert(length == cumulativeDifficulties_tmp.Count);
	  Debug.Assert(length <= c_difficultyWindow);
	  if (length <= 1)
	  {
		return 1;
	  }

	  timestamps_tmp.Sort();

	  Debug.Assert(2 * c_difficultyCut <= c_difficultyWindow - 2);
	  if (length <= c_difficultyWindow - 2 * c_difficultyCut)
	  {
		cutBegin = 0;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: cutEnd = length;
		cutEnd.CopyFrom(length);
	  }
	  else
	  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: cutBegin = (length - (c_difficultyWindow - 2 * c_difficultyCut) + 1) / 2;
		cutBegin.CopyFrom((length - (c_difficultyWindow - 2 * c_difficultyCut) + 1) / 2);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: cutEnd = cutBegin + (c_difficultyWindow - 2 * c_difficultyCut);
		cutEnd.CopyFrom(cutBegin + (c_difficultyWindow - 2 * c_difficultyCut));
	  }
	  Debug.Assert(cutBegin + 2 <= cutEnd != null && cutEnd <= length);
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: timeSpan = timestamps_tmp[cutEnd - 1] - timestamps_tmp[cutBegin];
	  timeSpan.CopyFrom(timestamps_tmp[cutEnd - 1] - timestamps_tmp[cutBegin]);
	  if (timeSpan == 0)
	  {
		timeSpan = 1;
	  }

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: totalWork = cumulativeDifficulties_tmp[cutEnd - 1] - cumulativeDifficulties_tmp[cutBegin];
	  totalWork.CopyFrom(cumulativeDifficulties_tmp[cutEnd - 1] - cumulativeDifficulties_tmp[cutBegin]);
	  Debug.Assert(totalWork > 0);

//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: low = mul128(totalWork, m_difficultyTarget, &high);
	  low.CopyFrom(GlobalMembers.mul128(new uint64_t(totalWork), new uint64_t(m_difficultyTarget), high));
	  if (high != 0 || uint64_t.MaxValue - low < (timeSpan - 1))
	  {
		return 0;
	  }
	  uint64_t nextDiffZ = low / timeSpan;
	  if (nextDiffZ <= 100)
	  {
		nextDiffZ = 100;
	  }
	  return nextDiffZ;
	}

	return (low + timeSpan - 1) / timeSpan; // with version
  }


//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool checkProofOfWorkV1(const CachedBlock& block, uint64_t currentDifficulty) const
  public bool checkProofOfWorkV1(CachedBlock block, uint64_t currentDifficulty)
  {
	if (BLOCK_MAJOR_VERSION_1 != block.getBlock().majorVersion)
	{
	  return false;
	}

	return check_hash(block.getBlockLongHash(), currentDifficulty);
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool checkProofOfWorkV2(const CachedBlock& cachedBlock, uint64_t currentDifficulty) const
  public bool checkProofOfWorkV2(CachedBlock cachedBlock, uint64_t currentDifficulty)
  {
	auto block = cachedBlock.getBlock();
	if (block.majorVersion < BLOCK_MAJOR_VERSION_2)
	{
	  return false;
	}

	if (!check_hash(cachedBlock.getBlockLongHash(), currentDifficulty))
	{
	  return false;
	}

	TransactionExtraMergeMiningTag mmTag = new TransactionExtraMergeMiningTag();
	if (!getMergeMiningTagFromExtra(block.parentBlock.baseTransaction.extra, mmTag))
	{
	  logger.functorMethod(ERROR) << "merge mining tag wasn't found in extra of the parent block miner transaction";
	  return false;
	}

	if (8 * sizeof(cachedGenesisBlock.getBlockHash()) < block.parentBlock.blockchainBranch.size())
	{
	  return false;
	}

	Crypto.Hash auxBlocksMerkleRoot = new Crypto.Hash();
	Crypto.GlobalMembers.tree_hash_from_branch(block.parentBlock.blockchainBranch.data(), block.parentBlock.blockchainBranch.size(), cachedBlock.getAuxiliaryBlockHeaderHash(), cachedGenesisBlock.getBlockHash(), auxBlocksMerkleRoot);

	if (auxBlocksMerkleRoot != mmTag.merkleRoot)
	{
	  logger.functorMethod(ERROR, BRIGHT_YELLOW) << "Aux block hash wasn't found in merkle tree";
	  return false;
	}

	return true;
  }
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool checkProofOfWork(const CachedBlock& block, uint64_t currentDiffic) const
  public bool checkProofOfWork(CachedBlock block, uint64_t currentDiffic)
  {
	switch (block.getBlock().majorVersion)
	{
	case BLOCK_MAJOR_VERSION_1:
	  return checkProofOfWorkV1(block, new uint64_t(currentDiffic));

	case BLOCK_MAJOR_VERSION_2:
	case BLOCK_MAJOR_VERSION_3:
	case BLOCK_MAJOR_VERSION_4:
	  return checkProofOfWorkV2(block, new uint64_t(currentDiffic));
	}

	logger.functorMethod(ERROR, BRIGHT_RED) << "Unknown block major version: " << block.getBlock().majorVersion << "." << block.getBlock().minorVersion;
	return false;
  }

//C++ TO C# CONVERTER TODO TASK: 'rvalue references' have no equivalent in C#:
  public Currency(Currency && currency)
  {
	  this.m_maxBlockHeight = currency.m_maxBlockHeight;
	  this.m_maxBlockBlobSize = currency.m_maxBlockBlobSize;
	  this.m_maxTxSize = currency.m_maxTxSize;
	  this.m_publicAddressBase58Prefix = currency.m_publicAddressBase58Prefix;
	  this.m_minedMoneyUnlockWindow = currency.m_minedMoneyUnlockWindow;
	  this.m_timestampCheckWindow = currency.m_timestampCheckWindow;
	  this.m_blockFutureTimeLimit = currency.m_blockFutureTimeLimit;
	  this.m_moneySupply = currency.m_moneySupply;
	  this.m_emissionSpeedFactor = currency.m_emissionSpeedFactor;
	  this.m_rewardBlocksWindow = currency.m_rewardBlocksWindow;
	  this.m_blockGrantedFullRewardZone = currency.m_blockGrantedFullRewardZone;
	  this.m_isBlockexplorer = currency.m_isBlockexplorer;
	  this.m_minerTxBlobReservedSize = currency.m_minerTxBlobReservedSize;
	  this.m_numberOfDecimalPlaces = currency.m_numberOfDecimalPlaces;
	  this.m_coin = currency.m_coin;
	  this.m_mininumFee = currency.m_mininumFee;
	  this.m_defaultDustThreshold = currency.m_defaultDustThreshold;
	  this.m_difficultyTarget = currency.m_difficultyTarget;
	  this.m_difficultyWindow = currency.m_difficultyWindow;
	  this.m_difficultyLag = currency.m_difficultyLag;
	  this.m_difficultyCut = currency.m_difficultyCut;
	  this.m_maxBlockSizeInitial = currency.m_maxBlockSizeInitial;
	  this.m_maxBlockSizeGrowthSpeedNumerator = currency.m_maxBlockSizeGrowthSpeedNumerator;
	  this.m_maxBlockSizeGrowthSpeedDenominator = currency.m_maxBlockSizeGrowthSpeedDenominator;
	  this.m_lockedTxAllowedDeltaSeconds = currency.m_lockedTxAllowedDeltaSeconds;
	  this.m_lockedTxAllowedDeltaBlocks = currency.m_lockedTxAllowedDeltaBlocks;
	  this.m_mempoolTxLiveTime = currency.m_mempoolTxLiveTime;
	  this.m_numberOfPeriodsToForgetTxDeletedFromPool = currency.m_numberOfPeriodsToForgetTxDeletedFromPool;
	  this.m_fusionTxMaxSize = currency.m_fusionTxMaxSize;
	  this.m_fusionTxMinInputCount = currency.m_fusionTxMinInputCount;
	  this.m_fusionTxMinInOutCountRatio = currency.m_fusionTxMinInOutCountRatio;
	  this.m_upgradeHeightV2 = currency.m_upgradeHeightV2;
	  this.m_upgradeHeightV3 = currency.m_upgradeHeightV3;
	  this.m_upgradeHeightV4 = currency.m_upgradeHeightV4;
	  this.m_upgradeVotingThreshold = currency.m_upgradeVotingThreshold;
	  this.m_upgradeVotingWindow = currency.m_upgradeVotingWindow;
	  this.m_upgradeWindow = currency.m_upgradeWindow;
	  this.m_blocksFileName = currency.m_blocksFileName;
	  this.m_blockIndexesFileName = currency.m_blockIndexesFileName;
	  this.m_txPoolFileName = currency.m_txPoolFileName;
	  this.m_genesisBlockReward = currency.m_genesisBlockReward;
	  this.m_zawyDifficultyBlockIndex = currency.m_zawyDifficultyBlockIndex;
	  this.m_zawyDifficultyV2 = currency.m_zawyDifficultyV2;
	  this.m_zawyDifficultyBlockVersion = currency.m_zawyDifficultyBlockVersion;
	  this.m_testnet = currency.m_testnet;
	  this.genesisBlockTemplate = std::move(currency.genesisBlockTemplate);
	  this.cachedGenesisBlock = new CachedBlock(genesisBlockTemplate);
	  this.logger = new Logging.LoggerRef(currency.logger);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t getApproximateMaximumInputCount(size_t transactionSize, size_t outputCount, size_t mixinCount) const
  public size_t getApproximateMaximumInputCount(size_t transactionSize, size_t outputCount, size_t mixinCount)
  {
	size_t KEY_IMAGE_SIZE = sizeof(Crypto.KeyImage);
	size_t OUTPUT_KEY_SIZE = sizeof(decltype(KeyOutput.key));
	size_t AMOUNT_SIZE = sizeof(uint64_t) + 2; //varint
	size_t GLOBAL_INDEXES_VECTOR_SIZE_SIZE = sizeof(uint8_t); //varint
	size_t GLOBAL_INDEXES_INITIAL_VALUE_SIZE = sizeof(uint32_t); //varint
	size_t GLOBAL_INDEXES_DIFFERENCE_SIZE = sizeof(uint32_t); //varint
	size_t SIGNATURE_SIZE = sizeof(Crypto.Signature);
	size_t EXTRA_TAG_SIZE = sizeof(uint8_t);
	size_t INPUT_TAG_SIZE = sizeof(uint8_t);
	size_t OUTPUT_TAG_SIZE = sizeof(uint8_t);
	size_t PUBLIC_KEY_SIZE = sizeof(Crypto.PublicKey);
	size_t TRANSACTION_VERSION_SIZE = sizeof(uint8_t);
	size_t TRANSACTION_UNLOCK_TIME_SIZE = sizeof(uint64_t);

	size_t outputsSize = outputCount * (OUTPUT_TAG_SIZE + OUTPUT_KEY_SIZE + AMOUNT_SIZE);
	size_t headerSize = TRANSACTION_VERSION_SIZE + TRANSACTION_UNLOCK_TIME_SIZE + EXTRA_TAG_SIZE + PUBLIC_KEY_SIZE;
	size_t inputSize = INPUT_TAG_SIZE + AMOUNT_SIZE + KEY_IMAGE_SIZE + SIGNATURE_SIZE + GLOBAL_INDEXES_VECTOR_SIZE_SIZE + GLOBAL_INDEXES_INITIAL_VALUE_SIZE + mixinCount * (GLOBAL_INDEXES_DIFFERENCE_SIZE + SIGNATURE_SIZE);

	return (transactionSize - headerSize - outputsSize) / inputSize;
  }

  private Currency(Logging.ILogger log)
  {
	  this.logger = new Logging.LoggerRef(log, "currency");
  }

  private bool init()
  {
	if (!generateGenesisBlock())
	{
	  logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to generate genesis block";
	  return false;
	}

	try
	{
	  cachedGenesisBlock.getBlockHash();
	}
	catch (System.Exception e)
	{
	  logger.functorMethod(ERROR, BRIGHT_RED) << "Failed to get genesis block hash: " << e.Message;
	  return false;
	}

	if (isTestnet())
	{
	  m_upgradeHeightV2 = 0;
//C++ TO C# CONVERTER TODO TASK: The following line was determined to be a copy assignment (rather than a reference assignment) - this should be verified and a 'CopyFrom' method should be created:
//ORIGINAL LINE: m_upgradeHeightV3 = static_cast<uint32_t>(-1);
	  m_upgradeHeightV3.CopyFrom((uint32_t) - 1);
	  m_blocksFileName = "testnet_" + m_blocksFileName;
	  m_blockIndexesFileName = "testnet_" + m_blockIndexesFileName;
	  m_txPoolFileName = "testnet_" + m_txPoolFileName;
	}

	return true;
  }

  private bool generateGenesisBlock()
  {
	genesisBlockTemplate = boost::value_initialized<BlockTemplate>();

	string genesisCoinbaseTxHex = CryptoNote.parameters.GENESIS_COINBASE_TX_HEX;
	BinaryArray minerTxBlob = new BinaryArray();

	bool r = fromHex(genesisCoinbaseTxHex, minerTxBlob) && CryptoNote.GlobalMembers.fromBinaryArray(ref genesisBlockTemplate.baseTransaction, minerTxBlob);

	if (!r)
	{
	  logger.functorMethod(ERROR, BRIGHT_RED) << "failed to parse coinbase tx from hard coded blob";
	  return false;
	}

	genesisBlockTemplate.majorVersion = BLOCK_MAJOR_VERSION_1;
	genesisBlockTemplate.minorVersion = BLOCK_MINOR_VERSION_0;
	genesisBlockTemplate.timestamp = 0;
	genesisBlockTemplate.nonce = 70;
	if (m_testnet)
	{
	  ++genesisBlockTemplate.nonce;
	}
	//miner::find_nonce_for_given_block(bl, 1, 0);
	cachedGenesisBlock.reset(new CachedBlock(genesisBlockTemplate));
	return true;
  }

  private uint32_t m_maxBlockHeight = new uint32_t();
  private size_t m_maxBlockBlobSize = new size_t();
  private size_t m_maxTxSize = new size_t();
  private uint64_t m_publicAddressBase58Prefix = new uint64_t();
  private uint32_t m_minedMoneyUnlockWindow = new uint32_t();

  private size_t m_timestampCheckWindow = new size_t();
  private uint64_t m_blockFutureTimeLimit = new uint64_t();

  private uint64_t m_moneySupply = new uint64_t();
  private uint m_emissionSpeedFactor;
  private uint64_t m_genesisBlockReward = new uint64_t();

  private size_t m_rewardBlocksWindow = new size_t();
  private uint32_t m_zawyDifficultyBlockIndex = new uint32_t();
  private size_t m_zawyDifficultyV2 = new size_t();
  private uint8_t m_zawyDifficultyBlockVersion = new uint8_t();
  private size_t m_blockGrantedFullRewardZone = new size_t();
  private size_t m_minerTxBlobReservedSize = new size_t();

  private size_t m_numberOfDecimalPlaces = new size_t();
  private uint64_t m_coin = new uint64_t();

  private uint64_t m_mininumFee = new uint64_t();
  private uint64_t m_defaultDustThreshold = new uint64_t();

  private uint64_t m_difficultyTarget = new uint64_t();
  private size_t m_difficultyWindow = new size_t();
  private size_t m_difficultyLag = new size_t();
  private size_t m_difficultyCut = new size_t();

  private size_t m_maxBlockSizeInitial = new size_t();
  private uint64_t m_maxBlockSizeGrowthSpeedNumerator = new uint64_t();
  private uint64_t m_maxBlockSizeGrowthSpeedDenominator = new uint64_t();

  private uint64_t m_lockedTxAllowedDeltaSeconds = new uint64_t();
  private size_t m_lockedTxAllowedDeltaBlocks = new size_t();

  private uint64_t m_mempoolTxLiveTime = new uint64_t();
  private uint64_t m_mempoolTxFromAltBlockLiveTime = new uint64_t();
  private uint64_t m_numberOfPeriodsToForgetTxDeletedFromPool = new uint64_t();

  private size_t m_fusionTxMaxSize = new size_t();
  private size_t m_fusionTxMinInputCount = new size_t();
  private size_t m_fusionTxMinInOutCountRatio = new size_t();

  private uint32_t m_upgradeHeightV2 = new uint32_t();
  private uint32_t m_upgradeHeightV3 = new uint32_t();
  private uint32_t m_upgradeHeightV4 = new uint32_t();
  private uint m_upgradeVotingThreshold;
  private uint32_t m_upgradeVotingWindow = new uint32_t();
  private uint32_t m_upgradeWindow = new uint32_t();

  private string m_blocksFileName;
  private string m_blockIndexesFileName;
  private string m_txPoolFileName;

  private readonly List<uint64_t> PRETTY_AMOUNTS = new List<uint64_t>();

  private bool m_testnet;
  private bool m_isBlockexplorer;

  private BlockTemplate genesisBlockTemplate = new BlockTemplate();
  private std::unique_ptr<CachedBlock> cachedGenesisBlock = new std::unique_ptr<CachedBlock>();

  private Logging.LoggerRef logger = new Logging.LoggerRef();

//C++ TO C# CONVERTER TODO TASK: C# has no concept of a 'friend' class:
//  friend class CurrencyBuilder;
}

public class CurrencyBuilder : boost::noncopyable
{
  public CurrencyBuilder(Logging.ILogger log)
  {
//C++ TO C# CONVERTER TODO TASK: The following line was determined to contain a copy constructor call - this should be verified and a copy constructor should be created:
//ORIGINAL LINE: this.m_currency = new CryptoNote.Currency(log);
	  this.m_currency = new CryptoNote.Currency(new Logging.ILogger(log));
	maxBlockNumber(parameters.CRYPTONOTE_MAX_BLOCK_NUMBER);
	maxBlockBlobSize(parameters.CRYPTONOTE_MAX_BLOCK_BLOB_SIZE);
	maxTxSize(parameters.CRYPTONOTE_MAX_TX_SIZE);
	publicAddressBase58Prefix(parameters.CRYPTONOTE_PUBLIC_ADDRESS_BASE58_PREFIX);
	minedMoneyUnlockWindow(parameters.CRYPTONOTE_MINED_MONEY_UNLOCK_WINDOW);

	timestampCheckWindow(parameters.BLOCKCHAIN_TIMESTAMP_CHECK_WINDOW);
	blockFutureTimeLimit(parameters.CRYPTONOTE_BLOCK_FUTURE_TIME_LIMIT);

	moneySupply(parameters.MONEY_SUPPLY);
	emissionSpeedFactor(parameters.EMISSION_SPEED_FACTOR);
  genesisBlockReward(parameters.GENESIS_BLOCK_REWARD);

	rewardBlocksWindow(parameters.CRYPTONOTE_REWARD_BLOCKS_WINDOW);
  zawyDifficultyBlockIndex(parameters.ZAWY_DIFFICULTY_BLOCK_INDEX);
  zawyDifficultyV2(parameters.ZAWY_DIFFICULTY_V2);
  zawyDifficultyBlockVersion(parameters.ZAWY_DIFFICULTY_DIFFICULTY_BLOCK_VERSION);
	blockGrantedFullRewardZone(parameters.CRYPTONOTE_BLOCK_GRANTED_FULL_REWARD_ZONE);
	minerTxBlobReservedSize(parameters.CRYPTONOTE_COINBASE_BLOB_RESERVED_SIZE);

	numberOfDecimalPlaces(parameters.CRYPTONOTE_DISPLAY_DECIMAL_POINT);

	mininumFee(parameters.MINIMUM_FEE);
	defaultDustThreshold(parameters.DEFAULT_DUST_THRESHOLD);

	difficultyTarget(parameters.DIFFICULTY_TARGET);
	difficultyWindow(parameters.DIFFICULTY_WINDOW);
	difficultyLag(parameters.DIFFICULTY_LAG);
	difficultyCut(parameters.DIFFICULTY_CUT);

	maxBlockSizeInitial(parameters.MAX_BLOCK_SIZE_INITIAL);
	maxBlockSizeGrowthSpeedNumerator(parameters.MAX_BLOCK_SIZE_GROWTH_SPEED_NUMERATOR);
	maxBlockSizeGrowthSpeedDenominator(parameters.MAX_BLOCK_SIZE_GROWTH_SPEED_DENOMINATOR);

	lockedTxAllowedDeltaSeconds(parameters.CRYPTONOTE_LOCKED_TX_ALLOWED_DELTA_SECONDS);
	lockedTxAllowedDeltaBlocks(parameters.CRYPTONOTE_LOCKED_TX_ALLOWED_DELTA_BLOCKS);

	mempoolTxLiveTime(parameters.CRYPTONOTE_MEMPOOL_TX_LIVETIME);
	mempoolTxFromAltBlockLiveTime(parameters.CRYPTONOTE_MEMPOOL_TX_FROM_ALT_BLOCK_LIVETIME);
	numberOfPeriodsToForgetTxDeletedFromPool(parameters.CRYPTONOTE_NUMBER_OF_PERIODS_TO_FORGET_TX_DELETED_FROM_POOL);

	fusionTxMaxSize(parameters.FUSION_TX_MAX_SIZE);
	fusionTxMinInputCount(parameters.FUSION_TX_MIN_INPUT_COUNT);
	fusionTxMinInOutCountRatio(parameters.FUSION_TX_MIN_IN_OUT_COUNT_RATIO);

	upgradeHeightV2(parameters.UPGRADE_HEIGHT_V2);
	upgradeHeightV3(parameters.UPGRADE_HEIGHT_V3);
	upgradeHeightV4(parameters.UPGRADE_HEIGHT_V4);
	upgradeVotingThreshold(parameters.UPGRADE_VOTING_THRESHOLD);
	upgradeVotingWindow(parameters.UPGRADE_VOTING_WINDOW);
	upgradeWindow(parameters.UPGRADE_WINDOW);

	blocksFileName(parameters.CRYPTONOTE_BLOCKS_FILENAME);
	blockIndexesFileName(parameters.CRYPTONOTE_BLOCKINDEXES_FILENAME);
	txPoolFileName(parameters.CRYPTONOTE_POOLDATA_FILENAME);

	  isBlockexplorer(false);
	testnet(false);
  }

  public Currency currency()
  {
	if (!m_currency.init())
	{
	  throw new System.Exception("Failed to initialize currency object");
	}

	return std::move(m_currency);
  }

  public Transaction generateGenesisTransaction()
  {
	CryptoNote.Transaction tx = new CryptoNote.Transaction();
	CryptoNote.AccountPublicAddress ac = boost::value_initialized<CryptoNote.AccountPublicAddress>();
	m_currency.constructMinerTx(1, 0, 0, 0, 0, 0, ac, tx); // zero fee in genesis
	return tx;
  }
  public Transaction generateGenesisTransaction(List<AccountPublicAddress> targets)
  {
	 Debug.Assert(targets.Count > 0);

	 CryptoNote.Transaction tx = new CryptoNote.Transaction();
	 tx.inputs.clear();
	 tx.outputs.clear();
	 tx.extra.clear();
	 tx.version = CURRENT_TRANSACTION_VERSION;
	 tx.unlockTime = m_currency.m_minedMoneyUnlockWindow;
	 KeyPair txkey = generateKeyPair();
	 addTransactionPublicKeyToExtra(tx.extra, txkey.publicKey);
	 BaseInput in = new BaseInput();
	 in.blockIndex = 0;
	 tx.inputs.push_back(in);
	 uint64_t block_reward = m_currency.m_genesisBlockReward;
	 uint64_t target_amount = block_reward / targets.Count;
	 uint64_t first_target_amount = target_amount + block_reward % targets.Count;
	 for (size_t i = 0; i < targets.Count; ++i)
	 {
	   Crypto.KeyDerivation derivation = boost::value_initialized<Crypto.KeyDerivation>();
	   Crypto.PublicKey outEphemeralPubKey = boost::value_initialized<Crypto.PublicKey>();
	   bool r = Crypto.generate_key_derivation(targets[i].viewPublicKey, txkey.secretKey, derivation);
	   if (r)
	   {
	   }
	   Debug.Assert(r == true);
 //      CHECK_AND_ASSERT_MES(r, false, "while creating outs: failed to generate_key_derivation(" << targets[i].viewPublicKey << ", " << txkey.sec << ")");
	   r = Crypto.derive_public_key(derivation, i, targets[i].spendPublicKey, outEphemeralPubKey);
	   Debug.Assert(r == true);
  //     CHECK_AND_ASSERT_MES(r, false, "while creating outs: failed to derive_public_key(" << derivation << ", " << i << ", " << targets[i].spendPublicKey << ")");
	   KeyOutput tk = new KeyOutput();
	   tk.key = outEphemeralPubKey;
	   TransactionOutput @out = new TransactionOutput();
	   @out.amount = (i == 0) ? first_target_amount : target_amount;
	   Console.Write("outs: ");
	   Console.Write(Convert.ToString(@out.amount));
	   Console.Write("\n");
	   @out.target = tk;
	   tx.outputs.push_back(@out);
	 }
	 return tx;
  }
  public CurrencyBuilder maxBlockNumber(uint32_t val)
  {
	  m_currency.m_maxBlockHeight = val;
	  return this;
  }
  public CurrencyBuilder maxBlockBlobSize(size_t val)
  {
	  m_currency.m_maxBlockBlobSize = val;
	  return this;
  }
  public CurrencyBuilder maxTxSize(size_t val)
  {
	  m_currency.m_maxTxSize = val;
	  return this;
  }
  public CurrencyBuilder publicAddressBase58Prefix(uint64_t val)
  {
	  m_currency.m_publicAddressBase58Prefix = val;
	  return this;
  }
  public CurrencyBuilder minedMoneyUnlockWindow(uint32_t val)
  {
	  m_currency.m_minedMoneyUnlockWindow = val;
	  return this;
  }

  public CurrencyBuilder timestampCheckWindow(size_t val)
  {
	  m_currency.m_timestampCheckWindow = val;
	  return this;
  }
  public CurrencyBuilder blockFutureTimeLimit(uint64_t val)
  {
	  m_currency.m_blockFutureTimeLimit = val;
	  return this;
  }

  public CurrencyBuilder moneySupply(uint64_t val)
  {
	  m_currency.m_moneySupply = val;
	  return this;
  }
  public CurrencyBuilder emissionSpeedFactor(uint val)
  {
	if (val <= 0 || val > 8 * sizeof(uint64_t))
	{
	  throw new System.ArgumentException("val at emissionSpeedFactor()");
	}

	m_currency.m_emissionSpeedFactor = val;
	return this;
  }
  public CurrencyBuilder genesisBlockReward(uint64_t val)
  {
	  m_currency.m_genesisBlockReward = val;
	  return this;
  }

  public CurrencyBuilder rewardBlocksWindow(size_t val)
  {
	  m_currency.m_rewardBlocksWindow = val;
	  return this;
  }
  public CurrencyBuilder zawyDifficultyBlockIndex(uint32_t val)
  {
	  m_currency.m_zawyDifficultyBlockIndex = val;
	  return this;
  }
  public CurrencyBuilder zawyDifficultyV2(size_t val)
  {
	  m_currency.m_zawyDifficultyV2 = val;
	  return this;
  }
  public CurrencyBuilder zawyDifficultyBlockVersion(uint8_t val)
  {
	  m_currency.m_zawyDifficultyBlockVersion = val;
	  return this;
  }
  public CurrencyBuilder blockGrantedFullRewardZone(size_t val)
  {
	  m_currency.m_blockGrantedFullRewardZone = val;
	  return this;
  }
  public CurrencyBuilder minerTxBlobReservedSize(size_t val)
  {
	  m_currency.m_minerTxBlobReservedSize = val;
	  return this;
  }

  public CurrencyBuilder numberOfDecimalPlaces(size_t val)
  {
	m_currency.m_numberOfDecimalPlaces = val;
	m_currency.m_coin = 1;
	for (size_t i = 0; i < m_currency.m_numberOfDecimalPlaces; ++i)
	{
	  m_currency.m_coin *= 10;
	}

	return this;
  }

  public CurrencyBuilder mininumFee(uint64_t val)
  {
	  m_currency.m_mininumFee = val;
	  return this;
  }
  public CurrencyBuilder defaultDustThreshold(uint64_t val)
  {
	  m_currency.m_defaultDustThreshold = val;
	  return this;
  }

  public CurrencyBuilder difficultyTarget(uint64_t val)
  {
	  m_currency.m_difficultyTarget = val;
	  return this;
  }
  public CurrencyBuilder difficultyWindow(size_t val)
  {
	if (val < 2)
	{
	  throw new System.ArgumentException("val at difficultyWindow()");
	}
	m_currency.m_difficultyWindow = val;
	return this;
  }
  public CurrencyBuilder difficultyLag(size_t val)
  {
	  m_currency.m_difficultyLag = val;
	  return this;
  }
  public CurrencyBuilder difficultyCut(size_t val)
  {
	  m_currency.m_difficultyCut = val;
	  return this;
  }

  public CurrencyBuilder maxBlockSizeInitial(size_t val)
  {
	  m_currency.m_maxBlockSizeInitial = val;
	  return this;
  }
  public CurrencyBuilder maxBlockSizeGrowthSpeedNumerator(uint64_t val)
  {
	  m_currency.m_maxBlockSizeGrowthSpeedNumerator = val;
	  return this;
  }
  public CurrencyBuilder maxBlockSizeGrowthSpeedDenominator(uint64_t val)
  {
	  m_currency.m_maxBlockSizeGrowthSpeedDenominator = val;
	  return this;
  }

  public CurrencyBuilder lockedTxAllowedDeltaSeconds(uint64_t val)
  {
	  m_currency.m_lockedTxAllowedDeltaSeconds = val;
	  return this;
  }
  public CurrencyBuilder lockedTxAllowedDeltaBlocks(size_t val)
  {
	  m_currency.m_lockedTxAllowedDeltaBlocks = val;
	  return this;
  }

  public CurrencyBuilder mempoolTxLiveTime(uint64_t val)
  {
	  m_currency.m_mempoolTxLiveTime = val;
	  return this;
  }
  public CurrencyBuilder mempoolTxFromAltBlockLiveTime(uint64_t val)
  {
	  m_currency.m_mempoolTxFromAltBlockLiveTime = val;
	  return this;
  }
  public CurrencyBuilder numberOfPeriodsToForgetTxDeletedFromPool(uint64_t val)
  {
	  m_currency.m_numberOfPeriodsToForgetTxDeletedFromPool = val;
	  return this;
  }

  public CurrencyBuilder fusionTxMaxSize(size_t val)
  {
	  m_currency.m_fusionTxMaxSize = val;
	  return this;
  }
  public CurrencyBuilder fusionTxMinInputCount(size_t val)
  {
	  m_currency.m_fusionTxMinInputCount = val;
	  return this;
  }
  public CurrencyBuilder fusionTxMinInOutCountRatio(size_t val)
  {
	  m_currency.m_fusionTxMinInOutCountRatio = val;
	  return this;
  }

  public CurrencyBuilder upgradeHeightV2(uint32_t val)
  {
	  m_currency.m_upgradeHeightV2 = val;
	  return this;
  }
  public CurrencyBuilder upgradeHeightV3(uint32_t val)
  {
	  m_currency.m_upgradeHeightV3 = val;
	  return this;
  }
  public CurrencyBuilder upgradeHeightV4(uint32_t val)
  {
	  m_currency.m_upgradeHeightV4 = val;
	  return this;
  }
  public CurrencyBuilder upgradeVotingThreshold(uint val)
  {
	if (val <= 0 || val > 100)
	{
	  throw new System.ArgumentException("val at upgradeVotingThreshold()");
	}

	m_currency.m_upgradeVotingThreshold = val;
	return this;
  }
  public CurrencyBuilder upgradeVotingWindow(uint32_t val)
  {
	  m_currency.m_upgradeVotingWindow = val;
	  return this;
  }
  public CurrencyBuilder upgradeWindow(uint32_t val)
  {
	if (val <= 0)
	{
	  throw new System.ArgumentException("val at upgradeWindow()");
	}

	m_currency.m_upgradeWindow = val;
	return this;
  }

  public CurrencyBuilder blocksFileName(string val)
  {
	  m_currency.m_blocksFileName = val;
	  return this;
  }
  public CurrencyBuilder blockIndexesFileName(string val)
  {
	  m_currency.m_blockIndexesFileName = val;
	  return this;
  }
  public CurrencyBuilder txPoolFileName(string val)
  {
	  m_currency.m_txPoolFileName = val;
	  return this;
  }

  public CurrencyBuilder isBlockexplorer(bool val)
  {
	  m_currency.m_isBlockexplorer = val;
	  return this;
  }
  public CurrencyBuilder testnet(bool val)
  {
	  m_currency.m_testnet = val;
	  return this;
  }

  private Currency m_currency = new Currency();
}

}

