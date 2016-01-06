using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameLobbyServer
{
    public class Player
    {
        public Player(Socket sock)
        {
            m_Name = null;
            m_Socket = sock;
            m_NameRecieved = false;
            m_Score = 0;
        }

        public Player(string name, Socket sock)
        {
            m_Name = name;
            m_Socket = sock;
            m_NameRecieved = true;
            m_Score = 0;
        }


        public void IncrementScoreByOne()
        {
            m_Score++;
        }
        
        public int GetScore()
        {
            return m_Score;
        }

        public Socket GetSocket()
        {
            return m_Socket;
        }
        public void SetSocket(Socket s)
        {
            m_Socket = s;
        }

        public string GetName()
        {
            return m_Name;
        }
        public void SetName(string name)
        {
            m_Name = name;
            m_NameRecieved = true;
        }

        public Boolean m_NameRecieved;
        private string m_Name;
        private Socket m_Socket;
        private int m_Score;
    }
}
