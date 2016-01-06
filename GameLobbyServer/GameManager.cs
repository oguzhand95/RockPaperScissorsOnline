using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLobbyServer
{
    class GameManager
    {
        private static GameManager m_Instance = null;
        private static readonly object m_Padlock = new object();

        private static LinkedList<Game> m_GameList;

        private GameManager()
        {
            m_GameList = new LinkedList<Game>();
        }


        public static LinkedList<Game> GetGameList()
        {
            return m_GameList;
        }

        public static void AddGame(Game g)
        {
            if (m_Instance == null)
            {
                m_Instance = new GameManager();
            }

            m_GameList.AddLast(g);
        }

        public static void AddGame(Player p1, Player p2)
        {
            Game g = new Game(p1, p2);
            if (m_Instance == null)
            {
                m_Instance = new GameManager();
            }

            m_GameList.AddLast(g);
        }

        public static bool CheckEvaluatable(string playerName1, string playerName2)
        {
            LinkedListNode<Game> game = Find(playerName1, playerName2);
            if (game == null)
                return false;

            return game.Value.CanBeEvaluated();
        }
        public static string EvaluateGame(string playerName1, string playerName2)
        {
            // We are assuming, we used CheckEvaluatable to check!
            LinkedListNode<Game> game = Find(playerName1, playerName2);
            if (game == null)
                return null;

            string eval = game.Value.GetWinner();

            return eval;
        }

        public static bool CheckCount(string playerName1, string playerName2)
        {
            // We are assuming, we used CheckEvaluatable to check!
            LinkedListNode<Game> game = Find(playerName1, playerName2);
            if (game == null)
                return false;

            return game.Value.CheckCount();
        }


        public static void SetSelection(string playerName1, string playerName2, bool playerBin, string selection)
        {
            LinkedListNode<Game> game = Find(playerName1, playerName2);

            if (game == null)
                return;

            if (playerBin == false)
            {
                game.Value.SetSelection(playerName1, selection);
            }
            else
            {
                game.Value.SetSelection(playerName2, selection);
            }
        }
        public static LinkedListNode<Game> Find(string name1, string name2)
        {
            if (m_Instance == null)
            {
                m_Instance = new GameManager();
            }
            LinkedListNode<Game> temp = m_GameList.First;
            while (temp != null)
            {
                if (temp.Value.GetName(false).Equals(name1))
                {
                    if (temp.Value.GetName(true).Equals(name2))
                    {
                        return temp;
                    }
                }
                else if (temp.Value.GetName(false).Equals(name2))
                {
                    if (temp.Value.GetName(true).Equals(name1))
                    {
                        return temp;
                    }
                }
                
                temp = temp.Next;
            }
            return null; // Traversed the entire list. Not found.  
        }
        public static void DeleteGame(string name1, string name2)
        {
            if (m_Instance == null)
            {
                m_Instance = new GameManager();
            }

            LinkedListNode<Game> game = Find(name1, name2);

            if (game == null)
            {
                return;
            }
            m_GameList.Remove(game);
        }

        public static GameManager Instance
        {
            get
            {
                lock (m_Padlock)
                {
                    if (m_Instance == null)
                    {
                        m_Instance = new GameManager();
                    }
                    return m_Instance;
                }
            }
        }
    }
}
