namespace SuperSudoku
{
    partial class DifficultyForm
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
            this.easyButton = new System.Windows.Forms.Button();
            this.n = new System.Windows.Forms.Button();
            this.hardButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // easyButton
            // 
            this.easyButton.Location = new System.Drawing.Point(12, 12);
            this.easyButton.Name = "easyButton";
            this.easyButton.Size = new System.Drawing.Size(150, 35);
            this.easyButton.TabIndex = 0;
            this.easyButton.Text = "&Easy";
            this.easyButton.UseVisualStyleBackColor = true;
            this.easyButton.Click += new System.EventHandler(this.easyButton_Click);
            // 
            // n
            // 
            this.n.Location = new System.Drawing.Point(168, 12);
            this.n.Name = "n";
            this.n.Size = new System.Drawing.Size(150, 35);
            this.n.TabIndex = 1;
            this.n.Text = "&Medium";
            this.n.UseVisualStyleBackColor = true;
            this.n.Click += new System.EventHandler(this.n_Click);
            // 
            // hardButton
            // 
            this.hardButton.Location = new System.Drawing.Point(324, 12);
            this.hardButton.Name = "hardButton";
            this.hardButton.Size = new System.Drawing.Size(150, 35);
            this.hardButton.TabIndex = 2;
            this.hardButton.Text = "&Hard";
            this.hardButton.UseVisualStyleBackColor = true;
            this.hardButton.Click += new System.EventHandler(this.hardButton_Click);
            // 
            // DifficultyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 146);
            this.Controls.Add(this.hardButton);
            this.Controls.Add(this.n);
            this.Controls.Add(this.easyButton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(492, 190);
            this.MinimumSize = new System.Drawing.Size(492, 190);
            this.Name = "DifficultyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose difficulty";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button easyButton;
        private System.Windows.Forms.Button n;
        private System.Windows.Forms.Button hardButton;
    }
}