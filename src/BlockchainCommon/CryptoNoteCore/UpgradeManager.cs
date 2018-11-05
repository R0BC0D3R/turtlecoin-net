// Copyright (c) 2012-2017, The CryptoNote developers, The Bytecoin developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;
using System.Diagnostics;

namespace CryptoNote
{

//Simple upgrade manager version. It doesn't support voting for now.
public class UpgradeManager: IUpgradeManager
{
  public UpgradeManager()
  {
  }
  public override void Dispose()
  {
	  base.Dispose();
  }

  public override void addMajorBlockVersion(uint8_t targetVersion, uint32_t upgradeHeight)
  {
	Debug.Assert(m_upgradeDetectors.Count == 0 || m_upgradeDetectors[m_upgradeDetectors.Count - 1].targetVersion() < targetVersion);
	m_upgradeDetectors.emplace_back(makeUpgradeDetector(targetVersion, upgradeHeight));
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual uint8_t getBlockMajorVersion(uint32_t blockIndex) const override
  public override uint8_t getBlockMajorVersion(uint32_t blockIndex)
  {
	for (var it = m_upgradeDetectors.rbegin(); it != m_upgradeDetectors.rend(); ++it)
	{
	  if (it.get().upgradeIndex() < blockIndex)
	  {
		return it.get().targetVersion();
	  }
	}

	return BLOCK_MAJOR_VERSION_1;
  }

  private List<std::unique_ptr<IUpgradeDetector>> m_upgradeDetectors = new List<std::unique_ptr<IUpgradeDetector>>();
}

} //namespace CryptoNote



