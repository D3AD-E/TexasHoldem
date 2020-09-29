namespace TexasHoldem.Client.Forms
{
    partial class AddMoneyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddMoneyForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MoneyPhaseLabel = new System.Windows.Forms.Label();
            this.SubmitButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.topBorder1 = new TexasHoldem.Client.Utils.UserControls.TopBorder();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.textBox1.Location = new System.Drawing.Point(20, 124);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox1.MaxLength = 500;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ShortcutsEnabled = false;
            this.textBox1.Size = new System.Drawing.Size(340, 90);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Gilroy Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(117, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Add 1000 coins";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Type the following text:";
            // 
            // MoneyPhaseLabel
            // 
            this.MoneyPhaseLabel.AutoSize = true;
            this.MoneyPhaseLabel.Location = new System.Drawing.Point(21, 64);
            this.MoneyPhaseLabel.Name = "MoneyPhaseLabel";
            this.MoneyPhaseLabel.Size = new System.Drawing.Size(51, 17);
            this.MoneyPhaseLabel.TabIndex = 5;
            this.MoneyPhaseLabel.Text = "label3";
            // 
            // SubmitButton
            // 
            this.SubmitButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(96)))), ((int)(((byte)(54)))));
            this.SubmitButton.FlatAppearance.BorderSize = 0;
            this.SubmitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.SubmitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SubmitButton.Location = new System.Drawing.Point(117, 220);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(146, 39);
            this.SubmitButton.TabIndex = 6;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.SubmitButton);
            this.panel1.Controls.Add(this.MoneyPhaseLabel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(32, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 277);
            this.panel1.TabIndex = 7;
            // 
            // topBorder1
            // 
            this.topBorder1.BackColor = System.Drawing.Color.Transparent;
            this.topBorder1.HasMinimize = true;
            this.topBorder1.Location = new System.Drawing.Point(0, 0);
            this.topBorder1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.topBorder1.Name = "topBorder1";
            this.topBorder1.Size = new System.Drawing.Size(446, 31);
            this.topBorder1.TabIndex = 8;
            // 
            // AddMoneyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(446, 341);
            this.Controls.Add(this.topBorder1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "AddMoneyForm";
            this.Text = "AddMoneyForm";
            this.Load += new System.EventHandler(this.AddMoneyForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label MoneyPhaseLabel;
        private Utils.UserControls.FlatButton SubmitButton;
        private System.Windows.Forms.Panel panel1;
        private Utils.UserControls.TopBorder topBorder1;
    }
}