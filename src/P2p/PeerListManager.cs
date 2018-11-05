using System.Collections.Generic;

// Copyright (c) 2018, The TurtleCoin Developers
// 
// Please see the included LICENSE file for more information.

// Copyright (c) 2018, The TurtleCoin Developers
// 
// Please see the included LICENSE file for more information.






public class PeerlistManager
{
		public PeerlistManager()
		{
			this.m_whitePeerlist = new Peerlist(m_peers_white, CryptoNote.P2P_LOCAL_WHITE_PEERLIST_LIMIT);
			this.m_grayPeerlist = new Peerlist(m_peers_gray, CryptoNote.P2P_LOCAL_GRAY_PEERLIST_LIMIT);
		}

		public bool init(bool allow_local_ip)
		{
		  m_allow_local_ip = allow_local_ip;
		  return true;
		}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t get_white_peers_count() const
		public size_t get_white_peers_count()
		{
			return m_peers_white.Count;
		}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: size_t get_gray_peers_count() const
		public size_t get_gray_peers_count()
		{
			return m_peers_gray.Count;
		}
		public bool merge_peerlist(LinkedList<PeerlistEntry> outer_bs)
		{
		  foreach (PeerlistEntry be in outer_bs)
		  {
			append_with_peer_gray(be);
		  }

		  // delete extra elements
		  trim_gray_peerlist();
		  return true;
		}
		public bool get_peerlist_head(LinkedList<PeerlistEntry> bs_head, uint32_t depth = CryptoNote.P2P_DEFAULT_PEERS_IN_HANDSHAKE)
		{
			/* Sort the peers by last seen [Newer peers come first] */
//C++ TO C# CONVERTER TODO TASK: The following line could not be converted:
			std::sort(m_peers_white.begin(), m_peers_white.end(), [](const auto & lhs, const auto & rhs)
			{
				return lhs.last_seen > rhs.last_seen;
			}
		   );

			uint32_t i = 0;

			foreach (var peer in m_peers_white)
			{
				if (!peer.last_seen)
				{
					continue;
				}

				bs_head.AddLast(peer);

				if (i > depth)
				{
					break;
				}

				i++;
			}

			return true;
		}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool get_peerlist_full(ClassicLinkedList<PeerlistEntry>& pl_gray, ClassicLinkedList<PeerlistEntry>& pl_white) const
		public bool get_peerlist_full(LinkedList<PeerlistEntry> pl_gray, LinkedList<PeerlistEntry> pl_white)
		{
			std::copy(m_peers_gray.GetEnumerator(), m_peers_gray.end(), std::back_inserter(pl_gray));
			std::copy(m_peers_white.GetEnumerator(), m_peers_white.end(), std::back_inserter(pl_white));

			return true;
		}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool get_white_peer_by_index(PeerlistEntry& p, size_t i) const
		public bool get_white_peer_by_index(PeerlistEntry p, size_t i)
		{
		  return m_whitePeerlist.get(p, i);
		}
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool get_gray_peer_by_index(PeerlistEntry& p, size_t i) const
		public bool get_gray_peer_by_index(PeerlistEntry p, size_t i)
		{
		  return m_grayPeerlist.get(p, i);
		}
		public bool append_with_peer_white(PeerlistEntry newPeer)
		{
			try
			{
				if (!is_ip_allowed(newPeer.adr.ip))
				{
					return true;
				}

				/* See if the peer already exists */
//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
//C++ TO C# CONVERTER NOTE: 'auto' variable declarations are not supported in C#:
//ORIGINAL LINE: auto whiteListIterator = std::find_if(m_peers_white.begin(), m_peers_white.end(), [&newPeer](const auto peer)
				var whiteListIterator = std::find_if(m_peers_white.GetEnumerator(), m_peers_white.end(), (peer UnnamedParameter) =>
				{
					return peer.adr == newPeer.adr;
				});

				/* Peer doesn't exist */
				if (whiteListIterator == m_peers_white.end())
				{
					//put new record into white list
					m_peers_white.Add(newPeer);
					trim_white_peerlist();
				}
				/* It does, update it */
				else
				{
					//update record in white list
					*whiteListIterator = newPeer;
				}

				//remove from gray list, if need
//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
//C++ TO C# CONVERTER NOTE: 'auto' variable declarations are not supported in C#:
//ORIGINAL LINE: auto grayListIterator = std::find_if(m_peers_gray.begin(), m_peers_gray.end(), [&newPeer](const auto peer)
				var grayListIterator = std::find_if(m_peers_gray.GetEnumerator(), m_peers_gray.end(), (peer UnnamedParameter) =>
				{
					return peer.adr == newPeer.adr;
				});


				if (grayListIterator != m_peers_gray.end())
				{
//C++ TO C# CONVERTER TODO TASK: There is no direct equivalent to the STL vector 'erase' method in C#:
					m_peers_gray.erase(grayListIterator);
				}

				return true;
			}
			catch (System.Exception)
			{
				return false;
			}

			return false;
		}
		public bool append_with_peer_gray(PeerlistEntry newPeer)
		{
			try
			{
				if (!is_ip_allowed(newPeer.adr.ip))
				{
					return true;
				}

				//find in white list
//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
//C++ TO C# CONVERTER NOTE: 'auto' variable declarations are not supported in C#:
//ORIGINAL LINE: auto whiteListIterator = std::find_if(m_peers_white.begin(), m_peers_white.end(), [&newPeer](const auto peer)
				var whiteListIterator = std::find_if(m_peers_white.GetEnumerator(), m_peers_white.end(), (peer UnnamedParameter) =>
				{
					return peer.adr == newPeer.adr;
				});

				if (whiteListIterator != m_peers_white.end())
				{
					return true;
				}

				//update gray list
//C++ TO C# CONVERTER TODO TASK: Lambda expressions cannot be assigned to 'var':
//C++ TO C# CONVERTER NOTE: 'auto' variable declarations are not supported in C#:
//ORIGINAL LINE: auto grayListIterator = std::find_if(m_peers_gray.begin(), m_peers_gray.end(), [&newPeer](const auto peer)
				var grayListIterator = std::find_if(m_peers_gray.GetEnumerator(), m_peers_gray.end(), (peer UnnamedParameter) =>
				{
					return peer.adr == newPeer.adr;
				});

				if (grayListIterator == m_peers_gray.end())
				{
					//put new record into white list
					m_peers_gray.Add(newPeer);
					trim_gray_peerlist();
				}
				else
				{
					//update record in white list
					*grayListIterator = newPeer;
				}

				return true;
			}
			catch (System.Exception)
			{
				return false;
			}

			return false;
		}
		public bool set_peer_just_seen(uint64_t peer, uint32_t ip, uint32_t port)
		{
		  NetworkAddress addr = new NetworkAddress();
		  addr.ip = ip;
		  addr.port = port;
		  return set_peer_just_seen(new uint64_t(peer), addr);
		}
		public bool set_peer_just_seen(uint64_t peer, NetworkAddress addr)
		{
		  try
		  {
			//find in white list
			PeerlistEntry ple = new PeerlistEntry();
			ple.adr = addr;
			ple.id = peer;
			ple.last_seen = time(null);
			return append_with_peer_white(ple);
		  }
		  catch (System.Exception)
		  {
		  }

		  return false;
		}
//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//		bool set_peer_unreachable(PeerlistEntry pr);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool is_ip_allowed(uint32_t ip) const
		public bool is_ip_allowed(uint32_t ip)
		{
		  System.Ipv4Address addr = new System.Ipv4Address(networkToHost(ip));

		  //never allow loopback ip
		  if (addr.isLoopback())
		  {
			return false;
		  }

		  if (!m_allow_local_ip && addr.isPrivate())
		  {
			return false;
		  }

		  return true;
		}
		public void trim_white_peerlist()
		{
			m_whitePeerlist.trim();
		}
		public void trim_gray_peerlist()
		{
			m_grayPeerlist.trim();
		}

		public void serialize(CryptoNote.ISerializer s)
		{
		  const uint8_t currentVersion = 1;
		  uint8_t version = new uint8_t(currentVersion);

		  s.functorMethod(version, "version");

		  if (version != currentVersion)
		  {
			return;
		  }

		  s.functorMethod(m_peers_white, "whitelist");
		  s.functorMethod(m_peers_gray, "graylist");
		}

		public Peerlist getWhite()
		{
			return m_whitePeerlist;
		}
		public Peerlist getGray()
		{
			return m_grayPeerlist;
		}

		private string m_config_folder;
		private bool m_allow_local_ip;
		private List<PeerlistEntry> m_peers_gray = new List<PeerlistEntry>();
		private List<PeerlistEntry> m_peers_white = new List<PeerlistEntry>();
		private Peerlist m_whitePeerlist = new Peerlist();
		private Peerlist m_grayPeerlist = new Peerlist();
}