namespace GameLobbyClient
{
    partial class FormClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.textIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.listPlayer = new System.Windows.Forms.ListView();
            this.ClientScreen = new System.Windows.Forms.RichTextBox();
            this.textName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblRefresh = new System.Windows.Forms.Label();
            this.btnPoke = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.inviteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showScoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(294, 22);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 25);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(294, 62);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 25);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // textIP
            // 
            this.textIP.Location = new System.Drawing.Point(188, 45);
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(100, 20);
            this.textIP.TabIndex = 2;
            this.textIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textIP_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(147, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(188, 71);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(100, 20);
            this.textPort.TabIndex = 3;
            this.textPort.Text = "9000";
            this.textPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textPort_KeyPress);
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(147, 74);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(35, 13);
            this.lblPort.TabIndex = 5;
            this.lblPort.Text = "Port : ";
            // 
            // listPlayer
            // 
            this.listPlayer.Location = new System.Drawing.Point(12, 22);
            this.listPlayer.MultiSelect = false;
            this.listPlayer.Name = "listPlayer";
            this.listPlayer.Size = new System.Drawing.Size(129, 262);
            this.listPlayer.TabIndex = 6;
            this.listPlayer.TabStop = false;
            this.listPlayer.UseCompatibleStateImageBehavior = false;
            this.listPlayer.View = System.Windows.Forms.View.List;
            this.listPlayer.SelectedIndexChanged += new System.EventHandler(this.listPlayer_SelectedIndexChanged);
            this.listPlayer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listPlayer_MouseDown);
            // 
            // ClientScreen
            // 
            this.ClientScreen.Location = new System.Drawing.Point(147, 97);
            this.ClientScreen.Name = "ClientScreen";
            this.ClientScreen.ReadOnly = true;
            this.ClientScreen.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ClientScreen.Size = new System.Drawing.Size(308, 213);
            this.ClientScreen.TabIndex = 7;
            this.ClientScreen.TabStop = false;
            this.ClientScreen.Text = "";
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(188, 19);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(100, 20);
            this.textName.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(147, 22);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(41, 13);
            this.lblName.TabIndex = 9;
            this.lblName.Text = "Name :";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(12, 287);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(129, 23);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.Text = "Refresh List";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblRefresh
            // 
            this.lblRefresh.AutoSize = true;
            this.lblRefresh.Location = new System.Drawing.Point(12, 6);
            this.lblRefresh.Name = "lblRefresh";
            this.lblRefresh.Size = new System.Drawing.Size(61, 13);
            this.lblRefresh.TabIndex = 11;
            this.lblRefresh.Text = "Player List :";
            // 
            // btnPoke
            // 
            this.btnPoke.Enabled = false;
            this.btnPoke.Location = new System.Drawing.Point(375, 22);
            this.btnPoke.Name = "btnPoke";
            this.btnPoke.Size = new System.Drawing.Size(75, 25);
            this.btnPoke.TabIndex = 6;
            this.btnPoke.TabStop = false;
            this.btnPoke.Text = "Poke";
            this.btnPoke.UseVisualStyleBackColor = true;
            this.btnPoke.Click += new System.EventHandler(this.btnPoke_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inviteToolStripMenuItem,
            this.showScoreToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(136, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // inviteToolStripMenuItem
            // 
            this.inviteToolStripMenuItem.Name = "inviteToolStripMenuItem";
            this.inviteToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.inviteToolStripMenuItem.Text = "Invite";
            this.inviteToolStripMenuItem.Click += new System.EventHandler(this.ınviteToolStripMenuItem_Click);
            // 
            // showScoreToolStripMenuItem
            // 
            this.showScoreToolStripMenuItem.Name = "showScoreToolStripMenuItem";
            this.showScoreToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.showScoreToolStripMenuItem.Text = "Show Score";
            this.showScoreToolStripMenuItem.Click += new System.EventHandler(this.showScoreToolStripMenuItem_Click);
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 322);
            this.Controls.Add(this.btnPoke);
            this.Controls.Add(this.lblRefresh);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.ClientScreen);
            this.Controls.Add(this.listPlayer);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textIP);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormClient";
            this.Text = "GameLobby Client";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormClient_FormClosed);
            this.Load += new System.EventHandler(this.FormClient_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.ListView listPlayer;
        private System.Windows.Forms.RichTextBox ClientScreen;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblRefresh;
        private System.Windows.Forms.Button btnPoke;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem inviteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showScoreToolStripMenuItem;
    }
}

