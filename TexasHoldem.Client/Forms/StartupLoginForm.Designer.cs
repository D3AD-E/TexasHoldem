namespace TexasHoldem.Client
{
    partial class StartupLoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartupLoginForm));
            this.label1 = new System.Windows.Forms.Label();
            this.UsernameTextB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PasswordTextB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RememberMeCheckB = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.SignupFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.LoginFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.topBorder1 = new TexasHoldem.Client.Utils.UserControls.TopBorder();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gilroy Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(32, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username";
            // 
            // UsernameTextB
            // 
            this.UsernameTextB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.UsernameTextB.Font = new System.Drawing.Font("SF UI Display Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UsernameTextB.Location = new System.Drawing.Point(32, 93);
            this.UsernameTextB.Name = "UsernameTextB";
            this.UsernameTextB.Size = new System.Drawing.Size(278, 22);
            this.UsernameTextB.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Gilroy Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(32, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // PasswordTextB
            // 
            this.PasswordTextB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.PasswordTextB.Location = new System.Drawing.Point(32, 140);
            this.PasswordTextB.Name = "PasswordTextB";
            this.PasswordTextB.PasswordChar = '*';
            this.PasswordTextB.Size = new System.Drawing.Size(278, 24);
            this.PasswordTextB.TabIndex = 2;
            this.PasswordTextB.UseSystemPasswordChar = true;
            this.PasswordTextB.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Gilroy Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(159, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "OR";
            // 
            // RememberMeCheckB
            // 
            this.RememberMeCheckB.AutoSize = true;
            this.RememberMeCheckB.Font = new System.Drawing.Font("Gilroy Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RememberMeCheckB.Location = new System.Drawing.Point(32, 169);
            this.RememberMeCheckB.Name = "RememberMeCheckB";
            this.RememberMeCheckB.Size = new System.Drawing.Size(143, 18);
            this.RememberMeCheckB.TabIndex = 3;
            this.RememberMeCheckB.Text = "Remember password";
            this.RememberMeCheckB.UseVisualStyleBackColor = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.SignupFButton);
            this.panel1.Controls.Add(this.LoginFButton);
            this.panel1.Controls.Add(this.RememberMeCheckB);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.PasswordTextB);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.UsernameTextB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.panel1.Location = new System.Drawing.Point(32, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(343, 242);
            this.panel1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Gilroy Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(77, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 31);
            this.label4.TabIndex = 6;
            this.label4.Text = "Texas Holdem";
            // 
            // SignupFButton
            // 
            this.SignupFButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(96)))), ((int)(((byte)(54)))));
            this.SignupFButton.FlatAppearance.BorderSize = 0;
            this.SignupFButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.SignupFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SignupFButton.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SignupFButton.Location = new System.Drawing.Point(189, 193);
            this.SignupFButton.Name = "SignupFButton";
            this.SignupFButton.Size = new System.Drawing.Size(144, 34);
            this.SignupFButton.TabIndex = 5;
            this.SignupFButton.Text = "Sign up";
            this.SignupFButton.UseVisualStyleBackColor = true;
            this.SignupFButton.Click += new System.EventHandler(this.SignupFButton_Click);
            // 
            // LoginFButton
            // 
            this.LoginFButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(96)))), ((int)(((byte)(54)))));
            this.LoginFButton.FlatAppearance.BorderSize = 0;
            this.LoginFButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.LoginFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LoginFButton.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LoginFButton.Location = new System.Drawing.Point(9, 193);
            this.LoginFButton.Name = "LoginFButton";
            this.LoginFButton.Size = new System.Drawing.Size(144, 34);
            this.LoginFButton.TabIndex = 5;
            this.LoginFButton.Text = "Log in";
            this.LoginFButton.UseVisualStyleBackColor = true;
            this.LoginFButton.Click += new System.EventHandler(this.LoginFButton_ClickAsync);
            // 
            // topBorder1
            // 
            this.topBorder1.AutoSize = true;
            this.topBorder1.BackColor = System.Drawing.Color.Transparent;
            this.topBorder1.HasMinimize = true;
            this.topBorder1.Location = new System.Drawing.Point(0, 0);
            this.topBorder1.Name = "topBorder1";
            this.topBorder1.Size = new System.Drawing.Size(407, 31);
            this.topBorder1.TabIndex = 5;
            // 
            // StartupLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(407, 306);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topBorder1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "StartupLoginForm";
            this.Text = "StartupLoginForm";
            this.Load += new System.EventHandler(this.StartupLoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UsernameTextB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PasswordTextB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox RememberMeCheckB;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Panel panel1;
        private Utils.UserControls.FlatButton LoginFButton;
        private Utils.UserControls.FlatButton SignupFButton;
        private Utils.UserControls.TopBorder topBorder1;
        private System.Windows.Forms.Label label4;
    }
}