using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLobbyClient
{
    class MessageHandler
    {
        private static MessageHandler m_Instance = null;
        private static readonly object m_Padlock = new object();
        private static FormGame m_FormGame = null;
        private static bool isPlaying = false;
        public enum MessageType
        {
            POKE =100,
            REQ_SET_NAME = 101,
            REQ_USER_LIST = 102,
            REQ_DISCONNECT = 103,
            REQ_INVITATION = 104,
            REQ_GAME = 105,
            REQ_SCORE=106,
            RES_SET_NAME = 201,
            RES_USER_LIST = 202,
            RES_DISCONNECT = 203,
            RES_GAME = 205,
            RES_SCORE = 206,
            ERR_NAME_EXISTS = 501,
            ERR_INVITATION = 504,
            ERR_GAMEDECLINED = 505,
            ERR_GAME_ALREADYPLAYING=506,
            GAME_SELECTION = 601,
            GAME_ANNOUNCE_WINNER = 602,
            GAME_ENABLE_SEND_BUTTON = 603
        }

        public static MessageHandler Instance
        {
            get
            {
                lock (m_Padlock)
                {
                    if (m_Instance == null)
                    {
                        m_Instance = new MessageHandler();
                    }
                    return m_Instance;
                }
            }
        }

        public static void Print(Message msg)
        {
            Console.WriteLine("Name: " + msg.Name);
            Console.WriteLine("Type: " + msg.Type);

            Console.WriteLine("Data: [");

            foreach (Dictionary<string, string> entry in msg.Data)
            {

                foreach (KeyValuePair<string, string> kvp in entry)
                {
                    Console.WriteLine(kvp.Key + " : " + kvp.Value);
                }
            }

            Console.WriteLine("]");
        }

        public static void UIEventRouter(Socket s, string name, MessageType type)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }
            Message response;
            switch ((int)type)
            {
                case 100:
                    response = Create100(name);
                    break;
                case 101:
                    response = Create101(name);
                    break;
                case 102:
                    response = Create102(name);
                    break;
                case 103:
                    response = Create103(name);
                    break;
                case 104:
                    response = Create104(name);
                    break;
                case 106:
                    response = Create106(name);
                    break;
                default:
                    response = null;
                    break;
            }

            Client.Send(response);
        }

        public static void MessageRouter(Socket s, string msg)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }

            UIUpdate.DisplayOnClientScreen(msg);

            Message DeserializedMessage = JsonConvert.DeserializeObject<Message>(msg);

            switch (DeserializedMessage.Type)
            {
                case 104:
                    Process104(s, DeserializedMessage);
                break;
                case 201:
                    Process201(s, DeserializedMessage);
                    break;
                case 202:
                    Process202(s, DeserializedMessage);
                    break;
                case 203:
                    Process203(s, DeserializedMessage);
                    break;
                case 206:
                    Process206(s, DeserializedMessage);
                    break;
                case 205:
                    Process205(s, DeserializedMessage);
                    break;
                case 501:
                    Process501(s, DeserializedMessage);
                    break;
                case 506:
                    Process506(s, DeserializedMessage);
                    break;
                case 602:
                    Process602(s,DeserializedMessage);
                    break;
                case 603:
                    Process603(s, DeserializedMessage);
                    break;
                default:
                    UIUpdate.DisplayOnClientScreen("ERROR: Undefined message type!");
                    break;
            }
        }

        ///<summary>
        /// REQ_INVITATION
        ///</summary>
        private static void Process104(Socket s, Message msg)
        {
            if (isPlaying == false)
            {
                DialogResult dialogResult = MessageBox.Show("Game invitation from " + msg.Name + ". Do you want to play?", "Game Invitation", MessageBoxButtons.YesNo);

                List<KeyValuePair<string, string>> l = new List<KeyValuePair<string, string>>();
                foreach (Dictionary<string, string> entry in msg.Data)
                {
                    foreach (KeyValuePair<string, string> kvp in entry)
                    {
                        l.Add(kvp);
                    }
                }
                string me = l[0].Value;

                Dictionary<string, string> dic1 = new Dictionary<string, string>();
                List<Dictionary<string, string>> data1 = new List<Dictionary<string, string>>();
                dic1.Add("To", msg.Name);

                if (dialogResult == DialogResult.Yes)
                {
                    dic1.Add("StartGame", "1");
                    isPlaying = true;
                }
                else if (dialogResult == DialogResult.No)
                {
                    dic1.Add("StartGame", "0");
                }
                data1.Add(dic1);

                Message sendMsg = new Message(105, me, data1);

                Client.Send(sendMsg);
            }
            else
            {
                List<KeyValuePair<string, string>> l = new List<KeyValuePair<string, string>>();
                foreach (Dictionary<string, string> entry in msg.Data)
                {
                    foreach (KeyValuePair<string, string> kvp in entry)
                    {
                        l.Add(kvp);
                    }
                }
                string me = l[0].Value;


                Dictionary<string, string> dic1 = new Dictionary<string, string>();
                List<Dictionary<string, string>> data1 = new List<Dictionary<string, string>>();
                dic1.Add("To", msg.Name);
                data1.Add(dic1);




                Message sendMsg = new Message(506, me, data1);

                Client.Send(sendMsg);
            }
            
        }

        ///<summary>
        /// RES_SET_NAME
        ///</summary>
        private static void Process201(Socket s, Message msg)
        {
            UIUpdate.ConnectionEstablished();
        }

        ///<summary>
        /// RES_USER_LIST
        ///</summary>
        private static void Process202(Socket s, Message msg)
        {
            List<string> l = new List<string>();
            UIUpdate.ClearPlayerList();
            foreach (Dictionary<string, string> entry in msg.Data)
            {
                foreach (KeyValuePair<string, string> kvp in entry)
                {
                    l.Add(kvp.Value);
                }
            }
            UIUpdate.AddPlayersToList(l);
        }

        ///<summary>
        /// RES_DISCONNECT
        ///</summary>
        private static void Process203(Socket s, Message msg)
        {
            UIUpdate.ConnectionTerminated();
        }

        ///<summary>
        /// RES_GAME
        ///</summary>
        private static void Process205(Socket s, Message msg)
        {
            //DialogResult dialogResult = MessageBox.Show("Game started! or supposed to be started", "Bahadir'a gelsin!");

            List<KeyValuePair<string, string>> l = new List<KeyValuePair<string, string>>();
            foreach (Dictionary<string, string> entry in msg.Data)
            {
                foreach (KeyValuePair<string, string> kvp in entry)
                {
                    l.Add(kvp);
                }
            }
            string opp= l[0].Value;

            m_FormGame = UIUpdate.CreateGame(l[0].Value, msg.Name);
            isPlaying = true;
        }

        private static void Process206(Socket s, Message msg)
        {
            List<KeyValuePair<string, string>> l = new List<KeyValuePair<string, string>>();
            foreach (Dictionary<string, string> entry in msg.Data)
            {
                foreach (KeyValuePair<string, string> kvp in entry)
                {
                    l.Add(kvp);
                }
            }
            string score = l[0].Value;

            MessageBox.Show("Score of player named " + msg.Name + " is " + score);
        }

        ///<summary>
        /// ERR_NAME_EXISTS
        ///</summary>
        private static void Process501(Socket s, Message msg)
        {
            UIUpdate.DisplayOnClientScreen("ERROR 501: There is a player with similar name, please try again!");
            UIUpdate.ConnectionTerminated();
        }


        ///<summary>
        /// ERR_GAME_ALREADYPLAYING
        ///</summary>
        private static void Process506(Socket s, Message msg)
        {
            UIUpdate.DisplayOnClientScreen("ERROR 506: Invited player " + msg.Name + " is already playing with another player!");
        }
        private static void Process602(Socket s, Message msg)
        {
            UIUpdate.SendMessageToFormGame(msg);
            isPlaying = false;
        }

        private static void Process603(Socket s, Message msg)
        {
            UIUpdate.SendMessageToFormGame(msg);
        }


        // RESPONSE CREATING FUNCTIONS

        ///<summary>
        /// POKE
        ///</summary>
        private static Message Create100(string name)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }
            return new Message(100, name, null);
        }

        ///<summary>
        /// REQ_SET_NAME
        ///</summary>
        private static Message Create101(string name)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }
            return new Message(101, name, null);
        }

        ///<summary>
        /// REQ_USER_LIST
        ///</summary>
        private static Message Create102(string name)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }

            return new Message(102, name, null);
        }

        ///<summary>
        /// REQ_DISCONNECT
        ///</summary>
        private static Message Create103(string name)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }

            return new Message(103, name, null);
        }

        private static Message Create104(string name)
        {
            string[] nameArray = name.Split('&');

            Dictionary<string, string> dic1 = new Dictionary<string, string>();
            List<Dictionary<string, string>> data1 = new List<Dictionary<string, string>>();
            int index = nameArray[1].Length - 1;
            if (  nameArray[1][index] == '\n' )
            {
                nameArray[1] = nameArray[1].Substring(0, nameArray[1].Length - 1);
            }
            dic1.Add("To", nameArray[1]);
            data1.Add(dic1);

            return new Message(104, nameArray[0], data1);
        }
        private static Message Create106(string name)
        {
            return new Message(106, name, null);
        }

        ///<summary>
        /// REQ_GAME
        ///</summary>
        private static Message Create105(Message msg)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }

            return msg;
        }
    }
}
