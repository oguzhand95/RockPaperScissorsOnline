using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameLobbyClient
{
    class Client
    {
        private bool terminating;
        private static Socket m_Client;
        private Thread m_ThreadReceive;


        public Client()
        {
            terminating = false;
        }

        public bool IsTerminated()
        {
            return terminating;
        }

        ~Client()
        {
            m_Client = null;
            terminating = true;
        }


        public Socket GetSocket()
        {
            return m_Client;
        }
        public void StartClient(string IP, string port)
        {
            try
            {
                m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                m_Client.Connect(IP, Int32.Parse(port));
                m_ThreadReceive = new Thread(new ThreadStart(Receive));
                m_ThreadReceive.Start();
            }
            catch
            {
                UIUpdate.DisplayOnClientScreen("Your message could not be sent.");
                UIUpdate.DisplayOnClientScreen("Connection lost...");
                terminating = true;
                m_Client.Disconnect(true);
                m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }

        }

        public void StopClient()
        {
            if (m_Client != null && terminating != true)
            {
                m_Client.Disconnect(true);
                terminating = true;
            }

            terminating = false;
            m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        }

        private void Receive()
        {
            bool connected = true;
            Console.WriteLine("Connected to the server.");

            while (connected)
            {
                try
                {
                    byte[] buffer = new byte[1024];

                    int rec = m_Client.Receive(buffer);

                    if (rec <= 0)
                    {
                        throw new SocketException();
                    }

                    string newmessage = Encoding.Default.GetString(buffer);
                    newmessage = newmessage.Substring(0, newmessage.IndexOf("\0"));
                    MessageHandler.MessageRouter(m_Client, newmessage);
                }
                catch
                {
                    if (!terminating)
                        UIUpdate.DisplayOnClientScreen("Connection has been terminated...\n");
                    connected = false;
                }
            }
        }

        public static void Send(Message msg)
        {
            string message = JsonConvert.SerializeObject(msg);

            byte[] buffer = Encoding.Default.GetBytes(message);

            //we can send a byte[] 
            m_Client.Send(buffer);
        }

    }
}
