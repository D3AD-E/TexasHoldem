namespace TexasHoldem.Client
{
    partial class PlayerDisplay
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerDisplay));
            this.MoneyLabel = new System.Windows.Forms.Label();
            this.ActionLabel = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.CardImg1 = new System.Windows.Forms.PictureBox();
            this.CardImg0 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AfkProgressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.CardImg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardImg0)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MoneyLabel
            // 
            this.MoneyLabel.AutoSize = true;
            this.MoneyLabel.ForeColor = System.Drawing.Color.Green;
            this.MoneyLabel.Location = new System.Drawing.Point(25, 22);
            this.MoneyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MoneyLabel.Name = "MoneyLabel";
            this.MoneyLabel.Size = new System.Drawing.Size(53, 18);
            this.MoneyLabel.TabIndex = 14;
            this.MoneyLabel.Text = "Money";
            // 
            // ActionLabel
            // 
            this.ActionLabel.AutoSize = true;
            this.ActionLabel.Location = new System.Drawing.Point(4, 43);
            this.ActionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ActionLabel.Name = "ActionLabel";
            this.ActionLabel.Size = new System.Drawing.Size(49, 18);
            this.ActionLabel.TabIndex = 13;
            this.ActionLabel.Text = "Action";
            this.ActionLabel.Visible = false;
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(25, 5);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(82, 18);
            this.UsernameLabel.TabIndex = 12;
            this.UsernameLabel.Text = "Empty seat";
            // 
            // CardImg1
            // 
            this.CardImg1.BackColor = System.Drawing.Color.Transparent;
            this.CardImg1.Image = ((System.Drawing.Image)(resources.GetObject("CardImg1.Image")));
            this.CardImg1.InitialImage = null;
            this.CardImg1.Location = new System.Drawing.Point(61, 1);
            this.CardImg1.Name = "CardImg1";
            this.CardImg1.Size = new System.Drawing.Size(56, 75);
            this.CardImg1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CardImg1.TabIndex = 8;
            this.CardImg1.TabStop = false;
            // 
            // CardImg0
            // 
            this.CardImg0.Image = ((System.Drawing.Image)(resources.GetObject("CardImg0.Image")));
            this.CardImg0.InitialImage = null;
            this.CardImg0.Location = new System.Drawing.Point(1, 1);
            this.CardImg0.Name = "CardImg0";
            this.CardImg0.Size = new System.Drawing.Size(56, 75);
            this.CardImg0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CardImg0.TabIndex = 7;
            this.CardImg0.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.GhostWhite;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.AfkProgressBar);
            this.panel1.Controls.Add(this.UsernameLabel);
            this.panel1.Controls.Add(this.ActionLabel);
            this.panel1.Controls.Add(this.MoneyLabel);
            this.panel1.Location = new System.Drawing.Point(1, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(159, 64);
            this.panel1.TabIndex = 15;
            // 
            // AfkProgressBar
            // 
            this.AfkProgressBar.Location = new System.Drawing.Point(-1, 43);
            this.AfkProgressBar.Name = "AfkProgressBar";
            this.AfkProgressBar.Size = new System.Drawing.Size(159, 18);
            this.AfkProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.AfkProgressBar.TabIndex = 15;
            this.AfkProgressBar.Visible = false;
            // 
            // PlayerDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CardImg0);
            this.Controls.Add(this.CardImg1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "PlayerDisplay";
            this.Size = new System.Drawing.Size(163, 112);
            this.Load += new System.EventHandler(this.PlayerDisplay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CardImg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CardImg0)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label MoneyLabel;
        public System.Windows.Forms.Label ActionLabel;
        public System.Windows.Forms.Label UsernameLabel;
        public System.Windows.Forms.PictureBox CardImg1;
        public System.Windows.Forms.PictureBox CardImg0;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar AfkProgressBar;
    }
}
