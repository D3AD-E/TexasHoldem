namespace TexasHoldem.Client
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.RaiseAmountUD = new System.Windows.Forms.NumericUpDown();
            this.BoardCard1Img = new System.Windows.Forms.PictureBox();
            this.BoardCard2Img = new System.Windows.Forms.PictureBox();
            this.BoardCard3Img = new System.Windows.Forms.PictureBox();
            this.BoardCard4Img = new System.Windows.Forms.PictureBox();
            this.BoardCard5Img = new System.Windows.Forms.PictureBox();
            this.PotSizeLabel = new System.Windows.Forms.Label();
            this.playerDisplay1 = new TexasHoldem.Client.PlayerDisplay();
            this.HoldingLabel = new System.Windows.Forms.Label();
            this.playerDisplay2 = new TexasHoldem.Client.PlayerDisplay();
            this.playerDisplay4 = new TexasHoldem.Client.PlayerDisplay();
            this.playerDisplay8 = new TexasHoldem.Client.PlayerDisplay();
            this.playerDisplay5 = new TexasHoldem.Client.PlayerDisplay();
            this.playerDisplay3 = new TexasHoldem.Client.PlayerDisplay();
            this.playerDisplay6 = new TexasHoldem.Client.PlayerDisplay();
            this.playerDisplay7 = new TexasHoldem.Client.PlayerDisplay();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FoldFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.CallFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.RaiseFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.topBorder1 = new TexasHoldem.Client.Utils.UserControls.TopBorder();
            ((System.ComponentModel.ISupportInitialize)(this.RaiseAmountUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoardCard1Img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoardCard2Img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoardCard3Img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoardCard4Img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoardCard5Img)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(35, 506);
            this.UsernameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(77, 18);
            this.UsernameLabel.TabIndex = 2;
            this.UsernameLabel.Text = "Username";
            // 
            // RaiseAmountUD
            // 
            this.RaiseAmountUD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.RaiseAmountUD.DecimalPlaces = 1;
            this.RaiseAmountUD.Location = new System.Drawing.Point(654, 499);
            this.RaiseAmountUD.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RaiseAmountUD.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.RaiseAmountUD.Name = "RaiseAmountUD";
            this.RaiseAmountUD.Size = new System.Drawing.Size(156, 24);
            this.RaiseAmountUD.TabIndex = 10;
            // 
            // BoardCard1Img
            // 
            this.BoardCard1Img.BackColor = System.Drawing.Color.Transparent;
            this.BoardCard1Img.Location = new System.Drawing.Point(244, 199);
            this.BoardCard1Img.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BoardCard1Img.Name = "BoardCard1Img";
            this.BoardCard1Img.Size = new System.Drawing.Size(67, 94);
            this.BoardCard1Img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BoardCard1Img.TabIndex = 7;
            this.BoardCard1Img.TabStop = false;
            // 
            // BoardCard2Img
            // 
            this.BoardCard2Img.BackColor = System.Drawing.Color.Transparent;
            this.BoardCard2Img.Location = new System.Drawing.Point(319, 199);
            this.BoardCard2Img.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BoardCard2Img.Name = "BoardCard2Img";
            this.BoardCard2Img.Size = new System.Drawing.Size(67, 94);
            this.BoardCard2Img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BoardCard2Img.TabIndex = 8;
            this.BoardCard2Img.TabStop = false;
            // 
            // BoardCard3Img
            // 
            this.BoardCard3Img.BackColor = System.Drawing.Color.Transparent;
            this.BoardCard3Img.Location = new System.Drawing.Point(394, 199);
            this.BoardCard3Img.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BoardCard3Img.Name = "BoardCard3Img";
            this.BoardCard3Img.Size = new System.Drawing.Size(67, 94);
            this.BoardCard3Img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BoardCard3Img.TabIndex = 7;
            this.BoardCard3Img.TabStop = false;
            // 
            // BoardCard4Img
            // 
            this.BoardCard4Img.BackColor = System.Drawing.Color.Transparent;
            this.BoardCard4Img.Location = new System.Drawing.Point(469, 199);
            this.BoardCard4Img.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BoardCard4Img.Name = "BoardCard4Img";
            this.BoardCard4Img.Size = new System.Drawing.Size(67, 94);
            this.BoardCard4Img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BoardCard4Img.TabIndex = 8;
            this.BoardCard4Img.TabStop = false;
            // 
            // BoardCard5Img
            // 
            this.BoardCard5Img.BackColor = System.Drawing.Color.Transparent;
            this.BoardCard5Img.Location = new System.Drawing.Point(544, 199);
            this.BoardCard5Img.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BoardCard5Img.Name = "BoardCard5Img";
            this.BoardCard5Img.Size = new System.Drawing.Size(67, 94);
            this.BoardCard5Img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BoardCard5Img.TabIndex = 8;
            this.BoardCard5Img.TabStop = false;
            // 
            // PotSizeLabel
            // 
            this.PotSizeLabel.AutoSize = true;
            this.PotSizeLabel.BackColor = System.Drawing.Color.Transparent;
            this.PotSizeLabel.Location = new System.Drawing.Point(403, 338);
            this.PotSizeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PotSizeLabel.Name = "PotSizeLabel";
            this.PotSizeLabel.Size = new System.Drawing.Size(39, 18);
            this.PotSizeLabel.TabIndex = 11;
            this.PotSizeLabel.Text = "Pot: ";
            // 
            // playerDisplay1
            // 
            this.playerDisplay1.BackColor = System.Drawing.Color.Transparent;
            this.playerDisplay1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playerDisplay1.Location = new System.Drawing.Point(451, 365);
            this.playerDisplay1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.playerDisplay1.Name = "playerDisplay1";
            this.playerDisplay1.Size = new System.Drawing.Size(160, 112);
            this.playerDisplay1.TabIndex = 12;
            // 
            // HoldingLabel
            // 
            this.HoldingLabel.AutoSize = true;
            this.HoldingLabel.Location = new System.Drawing.Point(325, 506);
            this.HoldingLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.HoldingLabel.Name = "HoldingLabel";
            this.HoldingLabel.Size = new System.Drawing.Size(58, 18);
            this.HoldingLabel.TabIndex = 13;
            this.HoldingLabel.Text = "Holding";
            // 
            // playerDisplay2
            // 
            this.playerDisplay2.BackColor = System.Drawing.Color.Transparent;
            this.playerDisplay2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playerDisplay2.Location = new System.Drawing.Point(649, 254);
            this.playerDisplay2.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.playerDisplay2.Name = "playerDisplay2";
            this.playerDisplay2.Size = new System.Drawing.Size(160, 112);
            this.playerDisplay2.TabIndex = 12;
            // 
            // playerDisplay4
            // 
            this.playerDisplay4.BackColor = System.Drawing.Color.Transparent;
            this.playerDisplay4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playerDisplay4.Location = new System.Drawing.Point(451, 27);
            this.playerDisplay4.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.playerDisplay4.Name = "playerDisplay4";
            this.playerDisplay4.Size = new System.Drawing.Size(160, 112);
            this.playerDisplay4.TabIndex = 12;
            // 
            // playerDisplay8
            // 
            this.playerDisplay8.BackColor = System.Drawing.Color.Transparent;
            this.playerDisplay8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playerDisplay8.Location = new System.Drawing.Point(226, 365);
            this.playerDisplay8.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.playerDisplay8.Name = "playerDisplay8";
            this.playerDisplay8.Size = new System.Drawing.Size(160, 112);
            this.playerDisplay8.TabIndex = 12;
            // 
            // playerDisplay5
            // 
            this.playerDisplay5.BackColor = System.Drawing.Color.Transparent;
            this.playerDisplay5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playerDisplay5.Location = new System.Drawing.Point(226, 27);
            this.playerDisplay5.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.playerDisplay5.Name = "playerDisplay5";
            this.playerDisplay5.Size = new System.Drawing.Size(160, 112);
            this.playerDisplay5.TabIndex = 12;
            // 
            // playerDisplay3
            // 
            this.playerDisplay3.BackColor = System.Drawing.Color.Transparent;
            this.playerDisplay3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playerDisplay3.Location = new System.Drawing.Point(649, 95);
            this.playerDisplay3.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.playerDisplay3.Name = "playerDisplay3";
            this.playerDisplay3.Size = new System.Drawing.Size(160, 112);
            this.playerDisplay3.TabIndex = 12;
            // 
            // playerDisplay6
            // 
            this.playerDisplay6.BackColor = System.Drawing.Color.Transparent;
            this.playerDisplay6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playerDisplay6.Location = new System.Drawing.Point(36, 95);
            this.playerDisplay6.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.playerDisplay6.Name = "playerDisplay6";
            this.playerDisplay6.Size = new System.Drawing.Size(160, 112);
            this.playerDisplay6.TabIndex = 12;
            // 
            // playerDisplay7
            // 
            this.playerDisplay7.BackColor = System.Drawing.Color.Transparent;
            this.playerDisplay7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.playerDisplay7.Location = new System.Drawing.Point(36, 254);
            this.playerDisplay7.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.playerDisplay7.Name = "playerDisplay7";
            this.playerDisplay7.Size = new System.Drawing.Size(160, 112);
            this.playerDisplay7.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.FoldFButton);
            this.panel1.Controls.Add(this.CallFButton);
            this.panel1.Controls.Add(this.RaiseFButton);
            this.panel1.Controls.Add(this.playerDisplay7);
            this.panel1.Controls.Add(this.playerDisplay6);
            this.panel1.Controls.Add(this.playerDisplay3);
            this.panel1.Controls.Add(this.playerDisplay5);
            this.panel1.Controls.Add(this.playerDisplay8);
            this.panel1.Controls.Add(this.playerDisplay4);
            this.panel1.Controls.Add(this.playerDisplay2);
            this.panel1.Controls.Add(this.HoldingLabel);
            this.panel1.Controls.Add(this.playerDisplay1);
            this.panel1.Controls.Add(this.PotSizeLabel);
            this.panel1.Controls.Add(this.BoardCard5Img);
            this.panel1.Controls.Add(this.BoardCard4Img);
            this.panel1.Controls.Add(this.BoardCard3Img);
            this.panel1.Controls.Add(this.BoardCard2Img);
            this.panel1.Controls.Add(this.BoardCard1Img);
            this.panel1.Controls.Add(this.RaiseAmountUD);
            this.panel1.Controls.Add(this.UsernameLabel);
            this.panel1.Location = new System.Drawing.Point(32, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(840, 606);
            this.panel1.TabIndex = 14;
            // 
            // FoldFButton
            // 
            this.FoldFButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.FoldFButton.FlatAppearance.BorderSize = 0;
            this.FoldFButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.FoldFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FoldFButton.Location = new System.Drawing.Point(330, 526);
            this.FoldFButton.Name = "FoldFButton";
            this.FoldFButton.Size = new System.Drawing.Size(156, 45);
            this.FoldFButton.TabIndex = 14;
            this.FoldFButton.Text = "Fold";
            this.FoldFButton.UseVisualStyleBackColor = true;
            this.FoldFButton.Click += new System.EventHandler(this.FoldFButon_Click);
            // 
            // CallFButton
            // 
            this.CallFButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(96)))), ((int)(((byte)(54)))));
            this.CallFButton.FlatAppearance.BorderSize = 0;
            this.CallFButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.CallFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CallFButton.Location = new System.Drawing.Point(492, 529);
            this.CallFButton.Name = "CallFButton";
            this.CallFButton.Size = new System.Drawing.Size(156, 45);
            this.CallFButton.TabIndex = 14;
            this.CallFButton.Text = "Call";
            this.CallFButton.UseVisualStyleBackColor = true;
            this.CallFButton.Click += new System.EventHandler(this.CallFButton_Click);
            // 
            // RaiseFButton
            // 
            this.RaiseFButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(96)))), ((int)(((byte)(54)))));
            this.RaiseFButton.FlatAppearance.BorderSize = 0;
            this.RaiseFButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.RaiseFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RaiseFButton.Location = new System.Drawing.Point(654, 529);
            this.RaiseFButton.Name = "RaiseFButton";
            this.RaiseFButton.Size = new System.Drawing.Size(156, 45);
            this.RaiseFButton.TabIndex = 14;
            this.RaiseFButton.Text = "Raise";
            this.RaiseFButton.UseVisualStyleBackColor = true;
            this.RaiseFButton.Click += new System.EventHandler(this.RaiseFButton_Click);
            // 
            // topBorder1
            // 
            this.topBorder1.BackColor = System.Drawing.Color.Transparent;
            this.topBorder1.HasMinimize = true;
            this.topBorder1.Location = new System.Drawing.Point(0, 0);
            this.topBorder1.Margin = new System.Windows.Forms.Padding(2);
            this.topBorder1.Name = "topBorder1";
            this.topBorder1.Size = new System.Drawing.Size(904, 31);
            this.topBorder1.TabIndex = 15;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(904, 670);
            this.Controls.Add(this.topBorder1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
            this.Load += new System.EventHandler(this.GameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RaiseAmountUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoardCard1Img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoardCard2Img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoardCard3Img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoardCard4Img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoardCard5Img)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.NumericUpDown RaiseAmountUD;
        private System.Windows.Forms.PictureBox BoardCard1Img;
        private System.Windows.Forms.PictureBox BoardCard2Img;
        private System.Windows.Forms.PictureBox BoardCard3Img;
        private System.Windows.Forms.PictureBox BoardCard4Img;
        private System.Windows.Forms.PictureBox BoardCard5Img;
        private System.Windows.Forms.Label PotSizeLabel;
        private PlayerDisplay playerDisplay1;
        private System.Windows.Forms.Label HoldingLabel;
        private PlayerDisplay playerDisplay2;
        private PlayerDisplay playerDisplay4;
        private PlayerDisplay playerDisplay8;
        private PlayerDisplay playerDisplay5;
        private PlayerDisplay playerDisplay3;
        private PlayerDisplay playerDisplay6;
        private PlayerDisplay playerDisplay7;
        private System.Windows.Forms.Panel panel1;
        private Utils.UserControls.FlatButton FoldFButton;
        private Utils.UserControls.FlatButton CallFButton;
        private Utils.UserControls.FlatButton RaiseFButton;
        private Utils.UserControls.TopBorder topBorder1;
    }
}

