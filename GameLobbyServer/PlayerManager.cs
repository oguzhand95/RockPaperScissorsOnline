using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameLobbyServer
{

    class PlayerManager
    {
        private static PlayerManager m_Instance = null;
        private static readonly object m_Padlock = new object();

        private static LinkedList<Player> m_PlayerList;

        private PlayerManager()
        {
            m_PlayerList = new LinkedList<Player>();
        }


        public static LinkedList<Player> GetPlayerList()
        {
            return m_PlayerList;
        }

        public static void AddPlayer(Player p)
        {
            if (m_Instance == null)
            {
                m_Instance = new PlayerManager();
            }

            m_PlayerList.AddLast(p);
        }


        public static LinkedListNode<Player> Find(string name)
        {
            if (m_Instance == null)
            {
                m_Instance = new PlayerManager();
            }
            LinkedListNode<Player> temp = m_PlayerList.First;
            while (temp != null)
            {
                if (temp.Value.GetName() == name) return temp;
                temp = temp.Next;
            }
            return null; // Traversed the entire list. Not found.  
        }
        public static void DisconnectPlayer(string name)
        {
            if (m_Instance == null)
            {
                m_Instance = new PlayerManager();
            }

            LinkedListNode<Player> player = Find(name);

            if (player == null)
            {
                return;
            }
            player.Value.GetSocket().Close();
            m_PlayerList.Remove(player);
        }

        public static PlayerManager Instance
        {
            get
            {
                lock (m_Padlock)
                {
                    if (m_Instance == null)
                    {
                        m_Instance = new PlayerManager();
                    }
                    return m_Instance;
                }
            }
        }
    }
}
