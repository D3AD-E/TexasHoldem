namespace TexasHoldem.Client.Forms
{
    partial class CreateRoomForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateRoomForm));
            this.RoomNameTextB = new System.Windows.Forms.TextBox();
            this.CreateFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.CancelFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MaxPlayersUD = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.topBorder1 = new TexasHoldem.Client.Utils.UserControls.TopBorder();
            ((System.ComponentModel.ISupportInitialize)(this.MaxPlayersUD)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RoomNameTextB
            // 
            this.RoomNameTextB.Location = new System.Drawing.Point(32, 84);
            this.RoomNameTextB.MaxLength = 50;
            this.RoomNameTextB.Name = "RoomNameTextB";
            this.RoomNameTextB.Size = new System.Drawing.Size(287, 24);
            this.RoomNameTextB.TabIndex = 0;
            // 
            // CreateFButton
            // 
            this.CreateFButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(167)))), ((int)(((byte)(119)))));
            this.CreateFButton.FlatAppearance.BorderSize = 0;
            this.CreateFButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.CreateFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateFButton.Location = new System.Drawing.Point(32, 177);
            this.CreateFButton.Name = "CreateFButton";
            this.CreateFButton.Size = new System.Drawing.Size(124, 42);
            this.CreateFButton.TabIndex = 2;
            this.CreateFButton.Text = "Create";
            this.CreateFButton.UseVisualStyleBackColor = true;
            this.CreateFButton.Click += new System.EventHandler(this.CreateFButton_Click);
            // 
            // CancelFButton
            // 
            this.CancelFButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(96)))), ((int)(((byte)(54)))));
            this.CancelFButton.FlatAppearance.BorderSize = 0;
            this.CancelFButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.CancelFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelFButton.Location = new System.Drawing.Point(195, 177);
            this.CancelFButton.Name = "CancelFButton";
            this.CancelFButton.Size = new System.Drawing.Size(124, 42);
            this.CancelFButton.TabIndex = 3;
            this.CancelFButton.Text = "Cancel";
            this.CancelFButton.UseVisualStyleBackColor = true;
            this.CancelFButton.Click += new System.EventHandler(this.CancelFButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Max players:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Room name:";
            // 
            // MaxPlayersUD
            // 
            this.MaxPlayersUD.Location = new System.Drawing.Point(34, 131);
            this.MaxPlayersUD.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.MaxPlayersUD.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.MaxPlayersUD.Name = "MaxPlayersUD";
            this.MaxPlayersUD.Size = new System.Drawing.Size(285, 24);
            this.MaxPlayersUD.TabIndex = 6;
            this.MaxPlayersUD.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.topBorder1);
            this.panel1.Controls.Add(this.MaxPlayersUD);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.CancelFButton);
            this.panel1.Controls.Add(this.CreateFButton);
            this.panel1.Controls.Add(this.RoomNameTextB);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(351, 242);
            this.panel1.TabIndex = 7;
            // 
            // topBorder1
            // 
            this.topBorder1.AutoSize = true;
            this.topBorder1.BackColor = System.Drawing.Color.Transparent;
            this.topBorder1.HasMinimize = true;
            this.topBorder1.Location = new System.Drawing.Point(0, 0);
            this.topBorder1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.topBorder1.Name = "topBorder1";
            this.topBorder1.Size = new System.Drawing.Size(351, 31);
            this.topBorder1.TabIndex = 7;
            // 
            // CreateRoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(361, 252);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "CreateRoomForm";
            this.Text = "CreateRoomForm";
            ((System.ComponentModel.ISupportInitialize)(this.MaxPlayersUD)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox RoomNameTextB;
        private Utils.UserControls.FlatButton CreateFButton;
        private Utils.UserControls.FlatButton CancelFButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown MaxPlayersUD;
        private System.Windows.Forms.Panel panel1;
        private Utils.UserControls.TopBorder topBorder1;
    }
}