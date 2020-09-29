namespace TexasHoldem.Client.Forms
{
    partial class SignUpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignUpForm));
            this.UsernameTextB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Password1TextB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Password2TextB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.CancelFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.SignUpFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.topBorder1 = new TexasHoldem.Client.Utils.UserControls.TopBorder();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UsernameTextB
            // 
            this.UsernameTextB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.UsernameTextB.Font = new System.Drawing.Font("SF UI Display Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UsernameTextB.Location = new System.Drawing.Point(32, 72);
            this.UsernameTextB.MaxLength = 20;
            this.UsernameTextB.Name = "UsernameTextB";
            this.UsernameTextB.Size = new System.Drawing.Size(257, 22);
            this.UsernameTextB.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gilroy Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(32, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Gilroy Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(32, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // Password1TextB
            // 
            this.Password1TextB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.Password1TextB.Font = new System.Drawing.Font("SF UI Display Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Password1TextB.Location = new System.Drawing.Point(32, 118);
            this.Password1TextB.MaxLength = 50;
            this.Password1TextB.Name = "Password1TextB";
            this.Password1TextB.PasswordChar = '*';
            this.Password1TextB.Size = new System.Drawing.Size(257, 22);
            this.Password1TextB.TabIndex = 2;
            this.Password1TextB.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Gilroy Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(32, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Repeat password";
            // 
            // Password2TextB
            // 
            this.Password2TextB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.Password2TextB.Font = new System.Drawing.Font("SF UI Display Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Password2TextB.Location = new System.Drawing.Point(32, 164);
            this.Password2TextB.MaxLength = 50;
            this.Password2TextB.Name = "Password2TextB";
            this.Password2TextB.PasswordChar = '*';
            this.Password2TextB.Size = new System.Drawing.Size(257, 22);
            this.Password2TextB.TabIndex = 2;
            this.Password2TextB.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Gilroy Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(114, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "SIGN UP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Gilroy Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.Color.Indigo;
            this.label5.Location = new System.Drawing.Point(41, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(242, 112);
            this.label5.TabIndex = 4;
            this.label5.Text = "Sign up and get:\r\n• 1000 coins added to your account\r\n• Access to poker rooms\r\n• " +
    "Great experience!\r\n\r\nYou can always get more coins in\r\n ADD COINS menu";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // CancelFButton
            // 
            this.CancelFButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(96)))), ((int)(((byte)(54)))));
            this.CancelFButton.FlatAppearance.BorderSize = 0;
            this.CancelFButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.CancelFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelFButton.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CancelFButton.Location = new System.Drawing.Point(179, 328);
            this.CancelFButton.Name = "CancelFButton";
            this.CancelFButton.Size = new System.Drawing.Size(110, 37);
            this.CancelFButton.TabIndex = 6;
            this.CancelFButton.Text = "Cancel";
            this.CancelFButton.UseVisualStyleBackColor = true;
            this.CancelFButton.Click += new System.EventHandler(this.CancelFButton_Click);
            // 
            // SignUpFButton
            // 
            this.SignUpFButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(96)))), ((int)(((byte)(54)))));
            this.SignUpFButton.FlatAppearance.BorderSize = 0;
            this.SignUpFButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.SignUpFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SignUpFButton.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SignUpFButton.Location = new System.Drawing.Point(32, 328);
            this.SignUpFButton.Name = "SignUpFButton";
            this.SignUpFButton.Size = new System.Drawing.Size(110, 37);
            this.SignUpFButton.TabIndex = 6;
            this.SignUpFButton.Text = "Sign up";
            this.SignUpFButton.UseVisualStyleBackColor = true;
            this.SignUpFButton.Click += new System.EventHandler(this.SignUpFButton_Click);
            // 
            // topBorder1
            // 
            this.topBorder1.AutoSize = true;
            this.topBorder1.BackColor = System.Drawing.Color.Transparent;
            this.topBorder1.HasMinimize = true;
            this.topBorder1.Location = new System.Drawing.Point(0, 0);
            this.topBorder1.Name = "topBorder1";
            this.topBorder1.Size = new System.Drawing.Size(380, 31);
            this.topBorder1.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SignUpFButton);
            this.panel1.Controls.Add(this.CancelFButton);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Password2TextB);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Password1TextB);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.UsernameTextB);
            this.panel1.Location = new System.Drawing.Point(32, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 382);
            this.panel1.TabIndex = 8;
            // 
            // SignUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(380, 446);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topBorder1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "SignUpForm";
            this.Text = "SignUpForm";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UsernameTextB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Password1TextB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Password2TextB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private Utils.UserControls.FlatButton SignUpFButton;
        private Utils.UserControls.FlatButton CancelFButton;
        private Utils.UserControls.TopBorder topBorder1;
        private System.Windows.Forms.Panel panel1;
    }
}