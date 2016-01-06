using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLobbyClient
{
    public partial class FormClient : Form
    {
        private Client m_Client;
        private FormGame m_FormGame;
        public FormClient()
        {
            InitializeComponent();
            m_Client = new Client();
        }

        public FormGame CreateGameUI(string localName, string opponentName)
        {
            MethodInvoker method = delegate
            {
                m_FormGame.SetAttr(localName, opponentName, this);
                m_FormGame.Show();
            };

            if (InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();

            
            return m_FormGame;
        }

        public void SendMessageToFormGameUI(Message msg)
        {
            m_FormGame.Callback(msg);
        }

        public void ClearPlayerListUI()
        {
            MethodInvoker method = delegate
            {
                listPlayer.Clear();
            };
            if (InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }

        public void AddPlayersToListUI(List<string> players)
        {
            MethodInvoker method = delegate
            {
                foreach (string s in players)
                {
                    listPlayer.Items.Add(s+"\n");
                }
            };
            if (InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }

        public void ConnectionEstablishedUI()
        {
            MethodInvoker method = delegate
            {
                btnPoke.Enabled = true;
                btnDisconnect.Enabled = true;
                btnRefresh.Enabled = true;
                UpdateClientScreen("Connection established with the server!");
            };
            if (InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }
        public void ConnectionTerminatedUI()
        {
            MethodInvoker method = delegate
            {
                m_Client.StopClient();
                btnDisconnect.Enabled = false;
                btnConnect.Enabled = true;
                btnRefresh.Enabled = false;
                textIP.Enabled = true;
                textPort.Enabled = true;
                textName.Enabled = true;
            };

            if (InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }

        public void UpdateClientScreen(string msg)
        {
            MethodInvoker method = delegate
            {
                ClientScreen.AppendText(msg + "\n");
            };

            if (InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if ( textName.TextLength <= 4 || textIP.TextLength <= 7 || textPort.TextLength < 1)
            {
                UpdateClientScreen("Name must be longer 4 characters, IP cannot be less than 7 characters and Port cannot be less than 1 character!");
                return;
            }
            btnPoke.Enabled = false;
            btnConnect.Enabled = false;
            textName.Enabled = false;
            textIP.Enabled = false;
            textPort.Enabled = false;
            btnDisconnect.Enabled = false;
            btnRefresh.Enabled = false;

            m_Client.StartClient(textIP.Text, textPort.Text);

            MessageHandler.UIEventRouter(m_Client.GetSocket(), textName.Text, (MessageHandler.MessageType.REQ_SET_NAME));
        }

        private void textPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            textName.Enabled = true;
            textPort.Enabled = true;
            textIP.Enabled = true;
            btnRefresh.Enabled = false;
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            btnPoke.Enabled = false;
            MessageHandler.UIEventRouter(m_Client.GetSocket(), textName.Text, MessageHandler.MessageType.REQ_DISCONNECT);
        }

        private void FormClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageHandler.UIEventRouter(m_Client.GetSocket(), textName.Text, MessageHandler.MessageType.REQ_DISCONNECT);
            m_Client.StopClient();
            System.Environment.Exit(1);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (m_Client.GetSocket() != null && !m_Client.IsTerminated())
            {
                MessageHandler.UIEventRouter(m_Client.GetSocket(), textName.Text, MessageHandler.MessageType.REQ_USER_LIST);
            }
            else
                MessageBox.Show("You are not connected!");
        }

        private void btnPoke_Click(object sender, EventArgs e)
        {
            if (m_Client.GetSocket() != null && !m_Client.IsTerminated())
            {
                MessageHandler.UIEventRouter(m_Client.GetSocket(), textName.Text, MessageHandler.MessageType.POKE);
            }
            else
                MessageBox.Show("You are not connected!");
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            m_FormGame = new FormGame();
        }

        private void listPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listPlayer_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                if (listPlayer.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
               
            }
        }

        private void pokeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void ınviteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String name = listPlayer.SelectedItems[0].Text;

            if (name.Substring(0, name.Length-1).Equals(textName.Text))
            {
                MessageBox.Show("Dude! You can't invite yourself!");
            }
            else
            {
                MessageHandler.UIEventRouter(m_Client.GetSocket(), textName.Text + "&" + name, MessageHandler.MessageType.REQ_INVITATION);
            }    
        }

        private void showScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String name = listPlayer.SelectedItems[0].Text;
            name = name.Substring(0, name.Length - 1);

            MessageHandler.UIEventRouter(m_Client.GetSocket(), name, MessageHandler.MessageType.REQ_SCORE);

        }
    }
}
