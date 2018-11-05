// Copyright (c) 2018, The TurtleCoin Developers
//
// Please see the included LICENSE.txt file for more information.


using System.Collections.Generic;

public class Peerlist
{
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		Peerlist(ClassicVector<PeerlistEntry> peers, uint maxSize);

		/* Gets the size of the peer list */
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: uint count() const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		uint count();

		/* Gets a peer list entry, indexed by time */
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool get(PeerlistEntry &entry, uint index) const;
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		bool get(PeerlistEntry entry, uint index);

		/* Trim the peer list, removing the oldest ones */
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		void trim();

		private List<PeerlistEntry> m_peers;

		private readonly uint m_maxSize;
}
