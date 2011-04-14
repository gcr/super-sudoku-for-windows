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
            this.medButton = new System.Windows.Forms.Button();
            this.hardButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.easyPanel = new System.Windows.Forms.Panel();
            this.medPanel = new System.Windows.Forms.Panel();
            this.hardPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // easyButton
            // 
            this.easyButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.easyButton.Location = new System.Drawing.Point(73, 268);
            this.easyButton.Name = "easyButton";
            this.easyButton.Size = new System.Drawing.Size(150, 35);
            this.easyButton.TabIndex = 0;
            this.easyButton.Text = "&Easy";
            this.easyButton.UseVisualStyleBackColor = true;
            this.easyButton.Click += new System.EventHandler(this.easyButton_Click);
            // 
            // medButton
            // 
            this.medButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.medButton.Location = new System.Drawing.Point(370, 268);
            this.medButton.Name = "medButton";
            this.medButton.Size = new System.Drawing.Size(150, 35);
            this.medButton.TabIndex = 1;
            this.medButton.Text = "&Medium";
            this.medButton.UseVisualStyleBackColor = true;
            this.medButton.Click += new System.EventHandler(this.medButtonClick);
            // 
            // hardButton
            // 
            this.hardButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.hardButton.Location = new System.Drawing.Point(668, 268);
            this.hardButton.Name = "hardButton";
            this.hardButton.Size = new System.Drawing.Size(150, 35);
            this.hardButton.TabIndex = 2;
            this.hardButton.Text = "&Hard";
            this.hardButton.UseVisualStyleBackColor = true;
            this.hardButton.Click += new System.EventHandler(this.hardButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.easyPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.easyButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.hardButton, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.medButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.medPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.hardPanel, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(893, 306);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // easyPanel
            // 
            this.easyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.easyPanel.Location = new System.Drawing.Point(5, 5);
            this.easyPanel.Margin = new System.Windows.Forms.Padding(5);
            this.easyPanel.Name = "easyPanel";
            this.easyPanel.Size = new System.Drawing.Size(287, 255);
            this.easyPanel.TabIndex = 5;
            // 
            // medPanel
            // 
            this.medPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.medPanel.Location = new System.Drawing.Point(302, 5);
            this.medPanel.Margin = new System.Windows.Forms.Padding(5);
            this.medPanel.Name = "medPanel";
            this.medPanel.Size = new System.Drawing.Size(287, 255);
            this.medPanel.TabIndex = 3;
            // 
            // hardPanel
            // 
            this.hardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hardPanel.Location = new System.Drawing.Point(599, 5);
            this.hardPanel.Margin = new System.Windows.Forms.Padding(5);
            this.hardPanel.Name = "hardPanel";
            this.hardPanel.Size = new System.Drawing.Size(289, 255);
            this.hardPanel.TabIndex = 4;
            // 
            // DifficultyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 306);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.Name = "DifficultyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Choose difficulty";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button easyButton;
        private System.Windows.Forms.Button medButton;
        private System.Windows.Forms.Button hardButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel medPanel;
        private System.Windows.Forms.Panel hardPanel;
        private System.Windows.Forms.Panel easyPanel;
    }
}