using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameLobbyServer
{
    class Server
    {
        public Server()
        {
            m_SocketList = new List<Socket>();
            m_ServerPort = 9999;
        }

        ~Server()
        {
            m_Server = null;
        }

        public void StartServer()
        {
            try
            {
                m_Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                m_Server.Bind(new IPEndPoint(IPAddress.Any, m_ServerPort));
                m_Server.Listen(5);

                m_ThreadAccept = new Thread(new ThreadStart(AcceptConnection));
                m_ThreadAccept.Start();

                listening = true;
                terminating = false;
                accept = true;
            }
            catch
            {
                    // TODO: Error: Cannot listen to PORT
            }
        }

        public void StopServer()
        {
            if(PlayerManager.GetPlayerList() != null)
            { 
                foreach (Player p in PlayerManager.GetPlayerList())
                {
                    Message msg = new Message(203, p.GetName(), null);
                    Server.Send(p.GetSocket(), msg);
                    PlayerManager.DisconnectPlayer(p.GetName());
                    }
            }

            

            m_Server = null;
            listening = false;
            terminating = true;
            accept = false;
        }

        public void SetPort(int port)
        {
            m_ServerPort = port;
        }

        private void AcceptConnection()
        {
            while (accept)
            {
                try
                {
                    m_SocketList.Add(m_Server.Accept());
                    Console.Write("New client connected...\n");
                    Thread ThreadReceive;
                    ThreadReceive = new Thread(new ThreadStart(Receive));
                    ThreadReceive.Start();
                }
                catch
                {
                    if (terminating)
                        accept = false;
                    else
                        Console.Write("Listening socket has stopped working...\n");
                }
            }
        }

        private void Receive()
        {
            bool connected = true;
            Socket n = m_SocketList[m_SocketList.Count - 1];
           

            while (connected)
            {
                try
                {
                    Byte[] buffer = new byte[1024];
                    int rec = n.Receive(buffer);

                    if (rec <= 0)
                    {
                        throw new SocketException();
                    }

                    string newmessage = Encoding.Default.GetString(buffer);
                    newmessage = newmessage.Substring(0, newmessage.IndexOf("\0"));

                    

                    MessageHandler.MessageRouter(n, newmessage);
                }
                catch
                {
                    if (!terminating)
                        Console.Write("Client has disconnected...\n");
                    n.Close();
                    m_SocketList.Remove(n);
                    connected = false;
                }
            }

        }

        void BroadCastMessage(string message)
        {
            byte[] buffer = Encoding.Default.GetBytes(message);

            //broadcast the message to all clients
            foreach (Player p in PlayerManager.GetPlayerList())
            {
                p.GetSocket().Send(buffer);
            }
            Console.Write("Your message has been broadcasted.\n");
        }
        
        public static void Send(Socket s, string message)
        {
            if (s.Connected == true)
            {
                byte[] buffer = Encoding.Default.GetBytes(message);
                s.Send(buffer);
            }
        }

        public static void Send(Socket s, Message msg)
        {
            if (s.Connected == true)
            {
                string message = JsonConvert.SerializeObject(msg);
                Send(s, message);
            }
        }

        private Socket m_Server;
        static List<Socket> m_SocketList;
        private int m_ServerPort;
        private Thread m_ThreadAccept;

        private bool listening = false;
        private bool terminating = false;
        private bool accept = true;
    }
}
