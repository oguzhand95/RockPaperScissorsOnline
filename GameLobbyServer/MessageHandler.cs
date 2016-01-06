using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameLobbyServer
{
    class MessageHandler
    {
        public static FormServer form;
        private static MessageHandler m_Instance = null;
        private static readonly object m_Padlock = new object();
        enum MessageType
        {
            POKE=100,
            REQ_SET_NAME=101,
            REQ_USER_LIST=102,
            REQ_DISCONNECT=103,
            REQ_INVITATION=104,
            REQ_GAME=105,
            REQ_SCORE = 106,
            RES_SET_NAME =201,
            RES_USER_LIST=202,
            RES_DISCONNECT=203,
            RES_GAME=205,
            RES_SCORE = 206,
            ERR_NAME_EXISTS =501,
            ERR_INVITATION=504,
            ERR_GAMEDECLINED=505,
            ERR_GAME_ALREADYPLAYING = 506,
            GAME_SELECTION =601,
            GAME_ANNOUNCE_WINNER=602,
            GAME_ENABLE_SEND_BUTTON=603
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

        public static void DisplayOnServerScreen(string s)
        {
            form.UpdateServerScreen(s);
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

        public static bool MessageRouter(Socket s, string msg)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }

            DisplayOnServerScreen(msg);

            Message DeserializedMessage = JsonConvert.DeserializeObject<Message>(msg);

            switch (DeserializedMessage.Type)
            {
                case 100:
                    Process100(s, DeserializedMessage);
                    break;
                case 101:
                    Process101(s, DeserializedMessage);
                break;
                case 102:
                    Process102(s, DeserializedMessage);
                break;
                case 103:
                    Process103(s, DeserializedMessage);
                break;
                case 104:
                    Process104(s, DeserializedMessage);
                break;
                case 105:
                    Process105(s, DeserializedMessage);
                break;
                case 106:
                    Process106(s, DeserializedMessage);
                    break;
                case 506:
                    Process506(s, DeserializedMessage);
                    break;
                case 601:
                    Process601(s, DeserializedMessage);
                break;
                default:
                    DisplayOnServerScreen("ERROR: Undefined message type!");
                break;
            }

            return true;
        }

        ///<summary>
        /// POKE
        ///</summary>
        public static void Process100(Socket s, Message msg)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }

            DisplayOnServerScreen("Player " + msg.Name + " poked the server!");
        }


        ///<summary>
        /// REQ_SET_NAME
        ///</summary>
        public static void Process101(Socket s, Message msg)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }

            if ( PlayerManager.Find(msg.Name) != null )
            {
                DisplayOnServerScreen("ERROR 501: Process101(Socket s, Message msg)");
                Server.Send( s, Create501(msg) );
            }
            else
            { 
                PlayerManager.AddPlayer(new Player(msg.Name, s));
                DisplayOnServerScreen("Player with name " + msg.Name + " is added to PlayerManager!");
                Server.Send(s, Create201(msg));
            }
        }

        ///<summary>
        /// REQ_USER_LIST
        ///</summary>
        public static void Process102(Socket s, Message msg)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }

            DisplayOnServerScreen("Player with name " + msg.Name + " requested the user list!");
            Server.Send(s, Create202(msg));
        }

        ///<summary>
        /// REQ_DISCONNECT
        ///</summary>
        public static void Process103(Socket s, Message msg)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }
            DisplayOnServerScreen("Player with name " + msg.Name + " requested disconnection!");
            PlayerManager.DisconnectPlayer(msg.Name);
            Server.Send(s, Create203(msg));
        }

        ///<summary>
        /// REQ_INVITATION
        ///</summary>
        public static void Process104(Socket s, Message msg)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }

            List<KeyValuePair<string, string>> l = new List<KeyValuePair<string, string>>();
            foreach (Dictionary<string, string> entry in msg.Data)
            {
                foreach (KeyValuePair<string, string> kvp in entry)
                {
                    l.Add(kvp);
                }
            }

            string nameToInvite = l[0].Value;

            DisplayOnServerScreen("Player with name " + msg.Name + " requested invitation to " + nameToInvite);

            bool isMatch = false;
            Player temp = null;
            foreach (Player p in PlayerManager.GetPlayerList())
            {
                if (p.GetName().Equals(nameToInvite))
                {
                    isMatch = true;
                    temp = p;
                }
            }

            if (isMatch)
            {
                Server.Send(temp.GetSocket(), msg);
            }
            else
            {
                Server.Send(s, Create504(msg));
            }
        }

        ///<summary>
        /// REQ_GAME
        ///</summary>
        public static void Process105(Socket s, Message msg)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }

            List<KeyValuePair<string, string>> l = new List<KeyValuePair<string, string>>();
            foreach (Dictionary<string, string> entry in msg.Data)
            {
                foreach (KeyValuePair<string, string> kvp in entry)
                {
                    l.Add(kvp);
                }
            }

            String To = l[0].Value;
            String startGame = l[1].Value;

            if (startGame == "1")
            {
                string clientOne = l[0].Value;
                string clientTwo = msg.Name;
                Socket clientOne_s = null;
                Socket clientTwo_s = null;

                foreach (Player p in PlayerManager.GetPlayerList())
                {
                    if (p.GetName().Equals(clientOne))
                    {
                        clientOne_s = p.GetSocket();
                    }
                }

                foreach (Player p in PlayerManager.GetPlayerList())
                {
                    if (p.GetName().Equals(clientTwo))
                    {
                        clientTwo_s = p.GetSocket();
                    }
                }

                Dictionary<string, string> dic1 = new Dictionary<string, string>();
                List<Dictionary<string, string>> data1 = new List<Dictionary<string, string>>();
                dic1.Add("To", clientTwo);
                data1.Add(dic1);

                Dictionary<string, string> dic2 = new Dictionary<string, string>();
                List<Dictionary<string, string>> data2 = new List<Dictionary<string, string>>();
                dic2.Add("To", clientOne);
                data2.Add(dic2);


                Message msgOne = new Message(205, clientTwo, data2);
                Message msgTwo = new Message(205, clientOne, data1);


                GameManager.AddGame(new Game(PlayerManager.Find(clientOne).Value, PlayerManager.Find(clientTwo).Value));

                Server.Send(clientOne_s, Create205(msgOne));
                Server.Send(clientTwo_s, Create205(msgTwo));
            }
            else
            {
                Socket clientOne_s = null;
                foreach (Player p in PlayerManager.GetPlayerList())
                {
                    string clientOne = l[0].Value;
                    if (p.GetName().Equals(clientOne))
                    {
                        clientOne_s = p.GetSocket();
                    }
                }
                Server.Send(clientOne_s, Create505(msg));
            }
        }

        ///<summary>
        /// REQ_SCORE
        ///</summary>
        public static void Process106(Socket s, Message msg)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }

            int score;

            foreach (Player p in PlayerManager.GetPlayerList())
            {
                if (p.GetName().Equals(msg.Name))
                {
                    score = p.GetScore();
                    Server.Send(s, Create206(msg, score));
                }
            }
        }


        public static void Process506(Socket s, Message msg)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }

            List<KeyValuePair<string, string>> l = new List<KeyValuePair<string, string>>();
            foreach (Dictionary<string, string> entry in msg.Data)
            {
                foreach (KeyValuePair<string, string> kvp in entry)
                {
                    l.Add(kvp);
                }
            }

            String nameTo = l[0].Value;
            Socket sockTo = null;

            foreach (Player p in PlayerManager.GetPlayerList())
            {
                if (p.GetName().Equals( nameTo ))
                {
                    sockTo = p.GetSocket();
                }
            }

            Server.Send(sockTo, msg);
        }



        ///<summary>
        /// GAME_SELECTION
        ///</summary>
        public static void Process601(Socket s, Message msg)
        {
            if (m_Instance == null)
            {
                m_Instance = new MessageHandler();
            }

            List<KeyValuePair<string, string>> l = new List<KeyValuePair<string, string>>();
            foreach (Dictionary<string, string> entry in msg.Data)
            {
                foreach (KeyValuePair<string, string> kvp in entry)
                {
                    l.Add(kvp);
                }
            }
            

            string playerOne = msg.Name;
            string playerTwo = l[1].Value;

            string playerOneSelection = l[0].Value;

            GameManager.SetSelection(playerOne, playerTwo, false, playerOneSelection);

            bool isEvaluated = false;
            string eval = "";
            if (GameManager.CheckEvaluatable(playerOne, playerTwo))
            {
                eval = GameManager.EvaluateGame(playerOne, playerTwo);

                if (eval.Equals("TIE") || eval.Equals("NOTENOUGH"))
                {
                    isEvaluated = false;
                }
                else
                {
                    isEvaluated = true;
                }
            }

            if (isEvaluated)
            {
                // Send the Winner via Message 602
                Message msg1 = Create602(msg);
                msg1.Name = playerOne;
                Message msg2 = Create602(msg);
                msg2.Name = playerTwo;

                Dictionary<string, string> dic = new Dictionary<string, string>();
                List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
                dic.Add("Winner", eval);
                data.Add(dic);

                msg1.Data = data;
                msg2.Data = data;

                Socket clientOne_s = null;
                Socket clientTwo_s = null;

                foreach (Player p in PlayerManager.GetPlayerList())
                {
                    if (p.GetName().Equals(playerOne))
                    {
                        clientOne_s = p.GetSocket();
                    }
                }
                
                foreach (Player p in PlayerManager.GetPlayerList())
                {
                    if (p.GetName().Equals(playerTwo))
                    {
                        clientTwo_s = p.GetSocket();
                    }
                }
                Server.Send(clientOne_s, msg1);
                Server.Send(clientTwo_s, msg2);
                
                GameManager.DeleteGame(playerOne, playerTwo);

                foreach (Player p in PlayerManager.GetPlayerList())
                {
                    if (p.GetName().Equals(eval))
                    {
                        p.IncrementScoreByOne();
                    }
                }

                return;
            }
            else
            {
                if (GameManager.CheckCount(playerOne, playerTwo) == true)
                {
                    Message msg1 = Create603(msg);
                    msg1.Name = playerOne;
                    Message msg2 = Create603(msg);
                    msg2.Name = playerTwo;

                    Socket clientOne_s = null;
                    foreach (Player p in PlayerManager.GetPlayerList())
                    {
                        if (p.GetName().Equals(playerOne))
                        {
                            clientOne_s = p.GetSocket();
                        }
                    }
                    Server.Send(clientOne_s, msg1);

                    Socket clientTwo_s = null;
                    foreach (Player p in PlayerManager.GetPlayerList())
                    {
                        if (p.GetName().Equals(playerTwo))
                        {
                            clientTwo_s = p.GetSocket();
                        }
                    }
                    Server.Send(clientTwo_s, msg2);
                }
            }
            
        }

        // RESPONSE CREATING FUNCTIONS

        ///<summary>
        /// RES_SET_NAME
        ///</summary>
        private static Message Create201(Message msg)
        {
            return new Message(201, msg.Name, null);
        }

        ///<summary>
        /// RES_USER_LIST
        ///</summary>
        private static Message Create202(Message msg)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            int i = 1;
            foreach (Player p in PlayerManager.GetPlayerList())
            {
                dic.Add(i.ToString(), p.GetName());
                i++;
            }

            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
            data.Add( dic );
            return new Message(202, msg.Name, data);
        }

        ///<summary>
        /// RES_DISCONNECT
        ///</summary>
        private static Message Create203(Message msg)
        {
            return new Message(203, msg.Name, null);
        }

 

        ///<summary>
        /// RES_GAME
        ///</summary>
        private static Message Create205(Message msg)
        {
            return msg;
        }

        private static Message Create206(Message msg, int score)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
            dic.Add("Score", score.ToString());
            data.Add(dic);

            return new Message(206, msg.Name, data);
        }

        ///<summary>
        /// ERR_NAME_EXISTS
        ///</summary>
        private static Message Create501(Message msg)
        {
            return new Message(501, msg.Name, null);
        }

        ///<summary>
        /// ERR_INVITATION		
        ///</summary>
        private static Message Create504(Message msg)
        {
            return new Message(504, msg.Name, null);
        }

        private static Message Create505(Message msg)
        {
            return new Message(505,msg.Name, null);
        }

        private static Message Create602(Message msg)
        {
            return new Message(602, msg.Name, null);
        }

        private static Message Create603(Message msg)
        {
            return new Message(603, msg.Name, null);
        }
    }
}
