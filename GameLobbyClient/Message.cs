using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLobbyClient
{
    public class Message
    {
        public Message(int type, string name, List<Dictionary<string, string>> data)
        {
            Type = type;
            Name = name;
            Data = data;
        }
        public int Type;
        public string Name;
        public List<Dictionary<string, string>> Data;
    }
}
