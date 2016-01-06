namespace GameLobbyClient
{
    partial class FormGame
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
            this.btnScissors = new System.Windows.Forms.Button();
            this.btnRock = new System.Windows.Forms.Button();
            this.btnPaper = new System.Windows.Forms.Button();
            this.lblSelectionInfo = new System.Windows.Forms.Label();
            this.lblSelection = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblLocalPlayer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnScissors
            // 
            this.btnScissors.BackgroundImage = global::GameLobbyClient.Properties.Resources.Scissors;
            this.btnScissors.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnScissors.Location = new System.Drawing.Point(324, 12);
            this.btnScissors.Name = "btnScissors";
            this.btnScissors.Size = new System.Drawing.Size(150, 150);
            this.btnScissors.TabIndex = 2;
            this.btnScissors.UseVisualStyleBackColor = true;
            this.btnScissors.Click += new System.EventHandler(this.btnScissors_Click);
            // 
            // btnRock
            // 
            this.btnRock.BackgroundImage = global::GameLobbyClient.Properties.Resources.Rock;
            this.btnRock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRock.Location = new System.Drawing.Point(168, 12);
            this.btnRock.Name = "btnRock";
            this.btnRock.Size = new System.Drawing.Size(150, 150);
            this.btnRock.TabIndex = 1;
            this.btnRock.UseVisualStyleBackColor = true;
            this.btnRock.Click += new System.EventHandler(this.btnRock_Click);
            // 
            // btnPaper
            // 
            this.btnPaper.BackgroundImage = global::GameLobbyClient.Properties.Resources.Paper;
            this.btnPaper.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPaper.Location = new System.Drawing.Point(12, 12);
            this.btnPaper.Name = "btnPaper";
            this.btnPaper.Size = new System.Drawing.Size(150, 150);
            this.btnPaper.TabIndex = 0;
            this.btnPaper.UseVisualStyleBackColor = true;
            this.btnPaper.Click += new System.EventHandler(this.btnPaper_Click);
            // 
            // lblSelectionInfo
            // 
            this.lblSelectionInfo.AutoSize = true;
            this.lblSelectionInfo.Location = new System.Drawing.Point(13, 169);
            this.lblSelectionInfo.Name = "lblSelectionInfo";
            this.lblSelectionInfo.Size = new System.Drawing.Size(97, 13);
            this.lblSelectionInfo.TabIndex = 3;
            this.lblSelectionInfo.Text = "Current Selection : ";
            // 
            // lblSelection
            // 
            this.lblSelection.AutoSize = true;
            this.lblSelection.Location = new System.Drawing.Point(116, 169);
            this.lblSelection.Name = "lblSelection";
            this.lblSelection.Size = new System.Drawing.Size(35, 13);
            this.lblSelection.TabIndex = 4;
            this.lblSelection.Text = "Paper";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(324, 164);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(150, 23);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send Selection";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lblLocalPlayer
            // 
            this.lblLocalPlayer.AutoSize = true;
            this.lblLocalPlayer.Location = new System.Drawing.Point(206, 169);
            this.lblLocalPlayer.Name = "lblLocalPlayer";
            this.lblLocalPlayer.Size = new System.Drawing.Size(41, 13);
            this.lblLocalPlayer.TabIndex = 6;
            this.lblLocalPlayer.Text = "LOCAL";
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 190);
            this.Controls.Add(this.lblLocalPlayer);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lblSelection);
            this.Controls.Add(this.lblSelectionInfo);
            this.Controls.Add(this.btnScissors);
            this.Controls.Add(this.btnRock);
            this.Controls.Add(this.btnPaper);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormGame";
            this.Text = "Game";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPaper;
        private System.Windows.Forms.Button btnRock;
        private System.Windows.Forms.Button btnScissors;
        private System.Windows.Forms.Label lblSelectionInfo;
        private System.Windows.Forms.Label lblSelection;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblLocalPlayer;
    }
}