using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLobbyClient
{
    class UIUpdate
    {
        public static FormClient form;

        public static FormGame CreateGame(string localName, string opponentName)
        {
            return form.CreateGameUI(localName, opponentName);
        }

        public static void SendMessageToFormGame(Message msg)
        {
            form.SendMessageToFormGameUI(msg);
        }

        public static void DisplayOnClientScreen(string s)
        {
            form.UpdateClientScreen(s);
        }

        public static void ConnectionEstablished()
        {
            form.ConnectionEstablishedUI();
        }

        public static void ConnectionTerminated()
        {
            form.ConnectionTerminatedUI();
        }

        public static void AddPlayersToList(List<string> players)
        {
            form.AddPlayersToListUI(players);
        }

        public static void ClearPlayerList()
        {
            form.ClearPlayerListUI();
        }
    }
}
