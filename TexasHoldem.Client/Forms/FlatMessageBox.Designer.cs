namespace TexasHoldem.Client.Forms
{
    partial class FlatMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlatMessageBox));
            this.panel1 = new System.Windows.Forms.Panel();
            this.topBorder1 = new TexasHoldem.Client.Utils.UserControls.TopBorder();
            this.BottomLabel = new System.Windows.Forms.Label();
            this.TopLabel = new System.Windows.Forms.Label();
            this.OKFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.topBorder1);
            this.panel1.Controls.Add(this.BottomLabel);
            this.panel1.Controls.Add(this.TopLabel);
            this.panel1.Controls.Add(this.OKFButton);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(412, 209);
            this.panel1.TabIndex = 0;
            // 
            // topBorder1
            // 
            this.topBorder1.AutoSize = true;
            this.topBorder1.BackColor = System.Drawing.Color.Transparent;
            this.topBorder1.HasMinimize = false;
            this.topBorder1.Location = new System.Drawing.Point(0, 0);
            this.topBorder1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.topBorder1.Name = "topBorder1";
            this.topBorder1.Size = new System.Drawing.Size(412, 31);
            this.topBorder1.TabIndex = 3;
            // 
            // BottomLabel
            // 
            this.BottomLabel.AutoSize = true;
            this.BottomLabel.Location = new System.Drawing.Point(35, 76);
            this.BottomLabel.Name = "BottomLabel";
            this.BottomLabel.Size = new System.Drawing.Size(48, 17);
            this.BottomLabel.TabIndex = 2;
            this.BottomLabel.Text = "label1";
            // 
            // TopLabel
            // 
            this.TopLabel.AutoSize = true;
            this.TopLabel.Font = new System.Drawing.Font("Gilroy Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TopLabel.Location = new System.Drawing.Point(172, 36);
            this.TopLabel.Name = "TopLabel";
            this.TopLabel.Size = new System.Drawing.Size(74, 28);
            this.TopLabel.TabIndex = 1;
            this.TopLabel.Text = "label1";
            // 
            // OKFButton
            // 
            this.OKFButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(96)))), ((int)(((byte)(54)))));
            this.OKFButton.FlatAppearance.BorderSize = 0;
            this.OKFButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.OKFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OKFButton.Location = new System.Drawing.Point(152, 145);
            this.OKFButton.Name = "OKFButton";
            this.OKFButton.Size = new System.Drawing.Size(116, 43);
            this.OKFButton.TabIndex = 0;
            this.OKFButton.Text = "OK";
            this.OKFButton.UseVisualStyleBackColor = true;
            this.OKFButton.Click += new System.EventHandler(this.OKFButton_Click);
            // 
            // FlatMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(422, 219);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Gilroy Light", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FlatMessageBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FlatMessageBox";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label BottomLabel;
        private System.Windows.Forms.Label TopLabel;
        private Utils.UserControls.FlatButton OKFButton;
        private Utils.UserControls.TopBorder topBorder1;
    }
}