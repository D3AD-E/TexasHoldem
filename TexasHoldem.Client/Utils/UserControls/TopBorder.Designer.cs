namespace TexasHoldem.Client.Utils.UserControls
{
    partial class TopBorder
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
            this.CloseFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.MinimizeFButton = new TexasHoldem.Client.Utils.UserControls.FlatButton();
            this.SuspendLayout();
            // 
            // CloseFButton
            // 
            this.CloseFButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.CloseFButton.FlatAppearance.BorderColor = System.Drawing.Color.DarkBlue;
            this.CloseFButton.FlatAppearance.BorderSize = 0;
            this.CloseFButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.CloseFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseFButton.Font = new System.Drawing.Font("Gilroy Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CloseFButton.Location = new System.Drawing.Point(179, 0);
            this.CloseFButton.Name = "CloseFButton";
            this.CloseFButton.Size = new System.Drawing.Size(28, 31);
            this.CloseFButton.TabIndex = 2;
            this.CloseFButton.Text = "X";
            this.CloseFButton.UseVisualStyleBackColor = true;
            this.CloseFButton.Click += new System.EventHandler(this.CloseFButton_Click);
            // 
            // MinimizeFButton
            // 
            this.MinimizeFButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.MinimizeFButton.FlatAppearance.BorderColor = System.Drawing.Color.MediumBlue;
            this.MinimizeFButton.FlatAppearance.BorderSize = 0;
            this.MinimizeFButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.MinimizeFButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeFButton.Font = new System.Drawing.Font("Gilroy Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MinimizeFButton.Location = new System.Drawing.Point(151, 0);
            this.MinimizeFButton.Name = "MinimizeFButton";
            this.MinimizeFButton.Size = new System.Drawing.Size(28, 31);
            this.MinimizeFButton.TabIndex = 3;
            this.MinimizeFButton.Text = "–";
            this.MinimizeFButton.UseVisualStyleBackColor = true;
            this.MinimizeFButton.Click += new System.EventHandler(this.MinimizeFButton_Click);
            // 
            // TopBorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MinimizeFButton);
            this.Controls.Add(this.CloseFButton);
            this.Name = "TopBorder";
            this.Size = new System.Drawing.Size(207, 31);
            this.Load += new System.EventHandler(this.TopBorder_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopBorder_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private FlatButton CloseFButton;
        private FlatButton MinimizeFButton;
    }
}
