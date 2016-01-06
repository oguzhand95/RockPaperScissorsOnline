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
    public partial class FormGame : Form
    {
        private FormClient m_mainForm;
        private string m_localName;
        private string m_opponentName;
        public FormGame()
        {
            InitializeComponent();
            this.Text = "Game with ";
            this.Hide();
        }

        public void Callback(Message msg)
        {
            switch (msg.Type)
            {
                case 602:
                    List<KeyValuePair<string, string>> l = new List<KeyValuePair<string, string>>();
                    foreach (Dictionary<string, string> entry in msg.Data)
                    {
                        foreach (KeyValuePair<string, string> kvp in entry)
                        {
                            l.Add(kvp);
                        }
                    }
                    string winner = l[0].Value;

                    if (winner.Equals(m_localName))
                    {
                        MessageBox.Show("Congratulations, you won this round!");
                    }
                    else
                    {
                        MessageBox.Show("There, there! You lost! Don't worry, I am sure you will win next round!");
                    }
                    btnSend.Enabled = true;
                    this.Hide();
                    m_mainForm.Show();
                    break;
                case 603:
                    btnSend.Enabled = true;
                    break;
            }
            
        }

        public void SetAttr(string localName, string opponentName, FormClient mainForm)
        {
            m_localName = localName;
            m_opponentName = opponentName;
            m_mainForm = mainForm;



            m_mainForm.Hide();
            this.Text = "Game with " + m_opponentName;
            lblLocalPlayer.Text = "|" + m_localName + "|";
        }

        private void btnRock_Click(object sender, EventArgs e)
        {
            lblSelection.Text = "Rock";
        }

        private void btnPaper_Click(object sender, EventArgs e)
        {
            lblSelection.Text = "Paper";
        }

        private void btnScissors_Click(object sender, EventArgs e)
        {
            lblSelection.Text = "Scissors";
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            btnSend.Enabled = false;

            Dictionary<string, string> dic1 = new Dictionary<string, string>();
            List<Dictionary<string, string>> data1 = new List<Dictionary<string, string>>();
            
            dic1.Add("Selection", lblSelection.Text);
            dic1.Add("Opponent", m_opponentName);
            data1.Add(dic1);

            Message msg = new Message(601, m_localName, data1);

            Client.Send(msg);           
        }
    }
}
