using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLobbyServer
{
    public partial class FormServer : Form
    {
        private Server m_Server;

        public FormServer()
        {
            InitializeComponent();
            m_Server = new Server();
        }

        public void UpdateServerScreen(string msg)
        {
            MethodInvoker method = delegate
            {
                ServerScreen.AppendText(msg + "\n");
            };

            if (InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }

        private void lblPort_Click(object sender, EventArgs e)
        {
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (textPort.TextLength < 1)
            {
                UpdateServerScreen("Invalid Port! Try again!");
                return;
            }

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            m_Server.SetPort( Int32.Parse( textPort.Text ) );
            textPort.Enabled = false;
            m_Server.StartServer();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;
            btnStart.Enabled = true;
            textPort.Enabled = true;

            m_Server.StopServer();
            // Functional code here..
        }

        private void ServerScreen_TextChanged(object sender, EventArgs e)
        {

        }

        private void textPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void FormServer_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(1);
        }
    }
}
