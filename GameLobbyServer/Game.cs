using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLobbyServer
{
    public class Game
    {
        private Player m_PlayerOne;
        private Player m_PlayerTwo;

        private List<string> m_PlayerOne_Selection;
        private List<string> m_PlayerTwo_Selection;
        

        

        public Game(Player p1, Player p2)
        {
            m_PlayerOne = p1;
            m_PlayerTwo = p2;

            m_PlayerOne_Selection = new List<string>();
            m_PlayerTwo_Selection = new List<string>();
        }

        public string GetWinner()
        {
            // if no winner returns TIE, or <2 returns NOTENOUGH
            int p1Score = 0;
            int p2Score = 0;
            if (CanBeEvaluated())
            {
                for (int i = 0; i < m_PlayerOne_Selection.Count; i++)
                {
                    string eval = EvaluateRound(i);
                    if (eval == m_PlayerOne.GetName())
                    {
                        p1Score++;
                    }
                    else if (eval == m_PlayerTwo.GetName())
                    {
                        p2Score++;
                    }
                }
            }
            else
                return "NOTENOUGH";

            if (p1Score == p2Score)
            {
                return "TIE";
            }
            else if (p1Score > p2Score)
            {
                return m_PlayerOne.GetName();
            }
            else if (p2Score > p1Score)
            {
                return m_PlayerTwo.GetName();
            }

            return "TIE";
        }

        public string EvaluateRound(int roundNumber)
        {
            if (m_PlayerOne_Selection[roundNumber].Equals(m_PlayerTwo_Selection[roundNumber]))
            {
                return ("TIE");
            }

            if (m_PlayerOne_Selection[roundNumber].Equals("Rock"))
            {
                if (m_PlayerTwo_Selection[roundNumber].Equals("Scissors"))
                {
                    return m_PlayerOne.GetName();
                }
                else
                    return m_PlayerTwo.GetName();
            }

            if (m_PlayerOne_Selection[roundNumber].Equals("Paper"))
            {
                if (m_PlayerTwo_Selection[roundNumber].Equals("Rock"))
                {
                    return m_PlayerOne.GetName();
                }
                else
                    return m_PlayerTwo.GetName();
            }

            if (m_PlayerOne_Selection[roundNumber].Equals("Scissors"))
            {
                if (m_PlayerTwo_Selection[roundNumber].Equals("Rock"))
                {
                    return m_PlayerTwo.GetName();
                }
                else
                    return m_PlayerOne.GetName();
            }

            return null;
        }

        public string GetName(bool playerBin)
        {
            if (playerBin == false)
            {
                return m_PlayerOne.GetName();
            }
            else
                return m_PlayerTwo.GetName();
        }

        public bool CheckCount()
        {
            if (m_PlayerOne_Selection.Count == m_PlayerTwo_Selection.Count)
            {
                return true;
            }
            return false;
        }

        public Player GetPlayer(bool playerBin)
        {
            if (playerBin == false)
            {
                return m_PlayerOne;
            }
            else
                return m_PlayerTwo;
        }

        public bool SetSelection(string playerName, string selection)
        {
            if (playerName.Equals(m_PlayerOne.GetName()))
            {
                m_PlayerOne_Selection.Add(selection);
                return true;
            }
            else if (playerName.Equals(m_PlayerTwo.GetName()))
            {
                m_PlayerTwo_Selection.Add(selection);
                return true;
            }
            else
                return false;
        }
        public string GetLastSelection(string playerName)
        {
            if (playerName.Equals(m_PlayerOne.GetName()))
            {
                return m_PlayerOne_Selection[m_PlayerOne_Selection.Count-1];
            }
            else if (playerName.Equals(m_PlayerTwo.GetName()))
            {
                return m_PlayerTwo_Selection[m_PlayerOne_Selection.Count - 1];
            }
            else return "";
        }

        public bool CanBeEvaluated()
        {
            if (m_PlayerOne_Selection.Count == m_PlayerTwo_Selection.Count)
            {
                if(m_PlayerOne_Selection.Count > 2 && m_PlayerTwo_Selection.Count >2)
                    return true;
            }
            return false;
        }
    }
}
