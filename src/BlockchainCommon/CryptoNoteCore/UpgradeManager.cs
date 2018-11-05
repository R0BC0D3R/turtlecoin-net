using System.Collections.Generic;
using System.Diagnostics;

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



