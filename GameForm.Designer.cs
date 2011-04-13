namespace SuperSudoku
{
    partial class GameForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.enterPuzzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGameUnsolvedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysShowHintsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hintBarText = new System.Windows.Forms.Label();
            this.solveButton = new System.Windows.Forms.Button();
            this.gridPanel = new System.Windows.Forms.Panel();
            this.contextMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.showHintsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solveThisSquareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.contextMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(466, 37);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enterPuzzleToolStripMenuItem,
            this.saveGameToolStripMenuItem,
            this.saveGameUnsolvedToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitGameToolStripMenuItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(61, 33);
            this.FileMenu.Text = "&File";
            // 
            // enterPuzzleToolStripMenuItem
            // 
            this.enterPuzzleToolStripMenuItem.Name = "enterPuzzleToolStripMenuItem";
            this.enterPuzzleToolStripMenuItem.Size = new System.Drawing.Size(335, 34);
            this.enterPuzzleToolStripMenuItem.Text = "Enter &New Puzzle";
            this.enterPuzzleToolStripMenuItem.Click += new System.EventHandler(this.FileEnterPuzzleClick);
            // 
            // saveGameToolStripMenuItem
            // 
            this.saveGameToolStripMenuItem.Name = "saveGameToolStripMenuItem";
            this.saveGameToolStripMenuItem.Size = new System.Drawing.Size(335, 34);
            this.saveGameToolStripMenuItem.Text = "&Save Game";
            this.saveGameToolStripMenuItem.Click += new System.EventHandler(this.FileSaveGameClick);
            // 
            // saveGameUnsolvedToolStripMenuItem
            // 
            this.saveGameUnsolvedToolStripMenuItem.Name = "saveGameUnsolvedToolStripMenuItem";
            this.saveGameUnsolvedToolStripMenuItem.Size = new System.Drawing.Size(335, 34);
            this.saveGameUnsolvedToolStripMenuItem.Text = "Save Game &Unsolved";
            this.saveGameUnsolvedToolStripMenuItem.Click += new System.EventHandler(this.FileSaveGameUnsolvedClick);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(335, 34);
            this.loadToolStripMenuItem.Text = "&Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.FileLoadClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(332, 6);
            // 
            // quitGameToolStripMenuItem
            // 
            this.quitGameToolStripMenuItem.Name = "quitGameToolStripMenuItem";
            this.quitGameToolStripMenuItem.Size = new System.Drawing.Size(335, 34);
            this.quitGameToolStripMenuItem.Text = "&Quit Game";
            this.quitGameToolStripMenuItem.Click += new System.EventHandler(this.FileQuitClick);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alwaysShowHintsToolStripMenuItem,
            this.showErrorsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(105, 33);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // alwaysShowHintsToolStripMenuItem
            // 
            this.alwaysShowHintsToolStripMenuItem.Checked = true;
            this.alwaysShowHintsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alwaysShowHintsToolStripMenuItem.Name = "alwaysShowHintsToolStripMenuItem";
            this.alwaysShowHintsToolStripMenuItem.Size = new System.Drawing.Size(309, 34);
            this.alwaysShowHintsToolStripMenuItem.Text = "&Always Show Hints";
            this.alwaysShowHintsToolStripMenuItem.Click += new System.EventHandler(this.OptionsShowHintsClick);
            // 
            // showErrorsToolStripMenuItem
            // 
            this.showErrorsToolStripMenuItem.Checked = true;
            this.showErrorsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showErrorsToolStripMenuItem.Name = "showErrorsToolStripMenuItem";
            this.showErrorsToolStripMenuItem.Size = new System.Drawing.Size(309, 34);
            this.showErrorsToolStripMenuItem.Text = "&Show Errors";
            this.showErrorsToolStripMenuItem.Click += new System.EventHandler(this.OptionsShowErrorsClick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameRulesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(72, 33);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // gameRulesToolStripMenuItem
            // 
            this.gameRulesToolStripMenuItem.Name = "gameRulesToolStripMenuItem";
            this.gameRulesToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.gameRulesToolStripMenuItem.Text = "Game &Rules";
            this.gameRulesToolStripMenuItem.Click += new System.EventHandler(this.HelpRulesClick);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(237, 34);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.HelpAboutClick);
            // 
            // hintBarText
            // 
            this.hintBarText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hintBarText.AutoSize = true;
            this.hintBarText.Location = new System.Drawing.Point(12, 456);
            this.hintBarText.Name = "hintBarText";
            this.hintBarText.Size = new System.Drawing.Size(45, 13);
            this.hintBarText.TabIndex = 3;
            this.hintBarText.Text = "Hint Bar";
            // 
            // solveButton
            // 
            this.solveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.solveButton.Location = new System.Drawing.Point(307, 445);
            this.solveButton.Name = "solveButton";
            this.solveButton.Size = new System.Drawing.Size(147, 35);
            this.solveButton.TabIndex = 4;
            this.solveButton.Text = "Solve...";
            this.solveButton.UseVisualStyleBackColor = true;
            this.solveButton.Click += new System.EventHandler(this.SolveClick);
            // 
            // gridPanel
            // 
            this.gridPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPanel.Location = new System.Drawing.Point(12, 40);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.Size = new System.Drawing.Size(442, 399);
            this.gridPanel.TabIndex = 5;
            // 
            // contextMenu
            // 
            this.contextMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHintsToolStripMenuItem,
            this.solveThisSquareToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(121, 33);
            this.contextMenu.Text = "(context)";
            this.contextMenu.Visible = false;
            // 
            // showHintsToolStripMenuItem
            // 
            this.showHintsToolStripMenuItem.Name = "showHintsToolStripMenuItem";
            this.showHintsToolStripMenuItem.Size = new System.Drawing.Size(299, 34);
            this.showHintsToolStripMenuItem.Text = "Show &Hints";
            // 
            // solveThisSquareToolStripMenuItem
            // 
            this.solveThisSquareToolStripMenuItem.Name = "solveThisSquareToolStripMenuItem";
            this.solveThisSquareToolStripMenuItem.Size = new System.Drawing.Size(299, 34);
            this.solveThisSquareToolStripMenuItem.Text = "&Solve This Square";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 490);
            this.Controls.Add(this.gridPanel);
            this.Controls.Add(this.solveButton);
            this.Controls.Add(this.hintBarText);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameForm";
            this.Text = "Super Sudoku";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem enterPuzzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveGameUnsolvedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem quitGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alwaysShowHintsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showErrorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameRulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label hintBarText;
        private System.Windows.Forms.Button solveButton;
        private System.Windows.Forms.Panel gridPanel;
        private System.Windows.Forms.ToolStripMenuItem contextMenu;
        private System.Windows.Forms.ToolStripMenuItem showHintsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solveThisSquareToolStripMenuItem;
    }
}
