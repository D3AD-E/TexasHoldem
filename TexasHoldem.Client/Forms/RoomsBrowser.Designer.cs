namespace TexasHoldem.Client.Forms
{
    partial class RoomsBrowserForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomsBrowserForm));
            this.RoomsListView = new System.Windows.Forms.ListView();
            this.RoomName = new System.Windows.Forms.ColumnHeader();
            this.BetSize = new System.Windows.Forms.ColumnHeader();
            this.AvailablePlaces = new System.Windows.Forms.ColumnHeader();
            this.LoggedInNameLabel = new System.Windows.Forms.Label();
            this.AvailableMoneyLabel = new System.Windows.Forms.Label();
            this.LogOutFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.JoinRoomFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.CreateRoomFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.SettingsFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.AddMoneyFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.topBorder1 = new TexasHoldem.Client.Utils.UserControls.TopBorder();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RoomsListView
            // 
            this.RoomsListView.AutoArrange = false;
            this.RoomsListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.RoomsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.RoomName,
            this.BetSize,
            this.AvailablePlaces});
            this.RoomsListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.RoomsListView.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RoomsListView.FullRowSelect = true;
            this.RoomsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.RoomsListView.HideSelection = false;
            this.RoomsListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.RoomsListView.LabelWrap = false;
            this.RoomsListView.Location = new System.Drawing.Point(35, 49);
            this.RoomsListView.MultiSelect = false;
            this.RoomsListView.Name = "RoomsListView";
            this.RoomsListView.Size = new System.Drawing.Size(513, 297);
            this.RoomsListView.TabIndex = 1;
            this.RoomsListView.TabStop = false;
            this.RoomsListView.UseCompatibleStateImageBehavior = false;
            this.RoomsListView.View = System.Windows.Forms.View.Details;
            this.RoomsListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.RoomsListView_ColumnWidthChanging);
            this.RoomsListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.RoomsListView_MouseDoubleClick);
            // 
            // RoomName
            // 
            this.RoomName.Text = "Room";
            this.RoomName.Width = 288;
            // 
            // BetSize
            // 
            this.BetSize.Text = "BB/SB";
            this.BetSize.Width = 80;
            // 
            // AvailablePlaces
            // 
            this.AvailablePlaces.Text = "Available places";
            this.AvailablePlaces.Width = 140;
            // 
            // LoggedInNameLabel
            // 
            this.LoggedInNameLabel.AutoSize = true;
            this.LoggedInNameLabel.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LoggedInNameLabel.Location = new System.Drawing.Point(35, 29);
            this.LoggedInNameLabel.Name = "LoggedInNameLabel";
            this.LoggedInNameLabel.Size = new System.Drawing.Size(102, 17);
            this.LoggedInNameLabel.TabIndex = 2;
            this.LoggedInNameLabel.Text = "Logged in as:";
            // 
            // AvailableMoneyLabel
            // 
            this.AvailableMoneyLabel.AutoSize = true;
            this.AvailableMoneyLabel.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AvailableMoneyLabel.Location = new System.Drawing.Point(409, 29);
            this.AvailableMoneyLabel.Name = "AvailableMoneyLabel";
            this.AvailableMoneyLabel.Size = new System.Drawing.Size(68, 17);
            this.AvailableMoneyLabel.TabIndex = 3;
            this.AvailableMoneyLabel.Text = "Balance:";
            // 
            // LogOutFButton
            // 
            this.LogOutFButton.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.LogOutFButton.FlatAppearance.BorderSize = 0;
            this.LogOutFButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.LogOutFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LogOutFButton.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LogOutFButton.Location = new System.Drawing.Point(554, 49);
            this.LogOutFButton.Name = "LogOutFButton";
            this.LogOutFButton.Size = new System.Drawing.Size(120, 40);
            this.LogOutFButton.TabIndex = 4;
            this.LogOutFButton.Text = "Log out";
            this.LogOutFButton.UseVisualStyleBackColor = true;
            // 
            // JoinRoomFButton
            // 
            this.JoinRoomFButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(167)))), ((int)(((byte)(119)))));
            this.JoinRoomFButton.FlatAppearance.BorderSize = 0;
            this.JoinRoomFButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.JoinRoomFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.JoinRoomFButton.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.JoinRoomFButton.Location = new System.Drawing.Point(554, 306);
            this.JoinRoomFButton.Name = "JoinRoomFButton";
            this.JoinRoomFButton.Size = new System.Drawing.Size(120, 40);
            this.JoinRoomFButton.TabIndex = 4;
            this.JoinRoomFButton.Text = "Join";
            this.JoinRoomFButton.UseVisualStyleBackColor = true;
            this.JoinRoomFButton.Click += new System.EventHandler(this.JoinRoomFButton_Click);
            // 
            // CreateRoomFButton
            // 
            this.CreateRoomFButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(96)))), ((int)(((byte)(54)))));
            this.CreateRoomFButton.FlatAppearance.BorderSize = 0;
            this.CreateRoomFButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.CreateRoomFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateRoomFButton.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CreateRoomFButton.Location = new System.Drawing.Point(554, 260);
            this.CreateRoomFButton.Name = "CreateRoomFButton";
            this.CreateRoomFButton.Size = new System.Drawing.Size(120, 40);
            this.CreateRoomFButton.TabIndex = 4;
            this.CreateRoomFButton.Text = "Create";
            this.CreateRoomFButton.UseVisualStyleBackColor = true;
            this.CreateRoomFButton.Click += new System.EventHandler(this.CreateRoomFButton_Click);
            // 
            // SettingsFButton
            // 
            this.SettingsFButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(96)))), ((int)(((byte)(54)))));
            this.SettingsFButton.FlatAppearance.BorderSize = 0;
            this.SettingsFButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.SettingsFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsFButton.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SettingsFButton.Location = new System.Drawing.Point(554, 214);
            this.SettingsFButton.Name = "SettingsFButton";
            this.SettingsFButton.Size = new System.Drawing.Size(120, 40);
            this.SettingsFButton.TabIndex = 4;
            this.SettingsFButton.Text = "Settings";
            this.SettingsFButton.UseVisualStyleBackColor = true;
            // 
            // AddMoneyFButton
            // 
            this.AddMoneyFButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(96)))), ((int)(((byte)(54)))));
            this.AddMoneyFButton.FlatAppearance.BorderSize = 0;
            this.AddMoneyFButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.AddMoneyFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddMoneyFButton.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AddMoneyFButton.Location = new System.Drawing.Point(554, 168);
            this.AddMoneyFButton.Name = "AddMoneyFButton";
            this.AddMoneyFButton.Size = new System.Drawing.Size(120, 40);
            this.AddMoneyFButton.TabIndex = 4;
            this.AddMoneyFButton.Text = "Add money";
            this.AddMoneyFButton.UseVisualStyleBackColor = true;
            this.AddMoneyFButton.Click += new System.EventHandler(this.AddMoneyFButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.AddMoneyFButton);
            this.panel1.Controls.Add(this.SettingsFButton);
            this.panel1.Controls.Add(this.CreateRoomFButton);
            this.panel1.Controls.Add(this.JoinRoomFButton);
            this.panel1.Controls.Add(this.LogOutFButton);
            this.panel1.Controls.Add(this.AvailableMoneyLabel);
            this.panel1.Controls.Add(this.LoggedInNameLabel);
            this.panel1.Controls.Add(this.RoomsListView);
            this.panel1.Location = new System.Drawing.Point(32, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(696, 388);
            this.panel1.TabIndex = 5;
            // 
            // topBorder1
            // 
            this.topBorder1.AutoSize = true;
            this.topBorder1.BackColor = System.Drawing.Color.Transparent;
            this.topBorder1.HasMinimize = true;
            this.topBorder1.Location = new System.Drawing.Point(0, 0);
            this.topBorder1.Name = "topBorder1";
            this.topBorder1.Size = new System.Drawing.Size(760, 31);
            this.topBorder1.TabIndex = 6;
            // 
            // RoomsBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(760, 452);
            this.Controls.Add(this.topBorder1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "RoomsBrowserForm";
            this.Text = "RoomsBrowser";
            this.Load += new System.EventHandler(this.RoomsBrowserForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView RoomsListView;
        private System.Windows.Forms.ColumnHeader RoomName;
        private System.Windows.Forms.ColumnHeader BetSize;
        private System.Windows.Forms.ColumnHeader AvailablePlaces;
        private System.Windows.Forms.Label LoggedInNameLabel;
        private System.Windows.Forms.Label AvailableMoneyLabel;
        private Utils.UserControls.FlatButton LogOutFButton;
        private Utils.UserControls.FlatButton CreateRoomFButton;
        private Utils.UserControls.FlatButton SettingsFButton;
        private Utils.UserControls.FlatButton AddMoneyFButton;
        private Utils.UserControls.FlatButton JoinRoomFButton;
        private System.Windows.Forms.Panel panel1;
        private Utils.UserControls.TopBorder topBorder1;
    }
}