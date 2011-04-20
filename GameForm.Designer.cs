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
            this.components = new System.ComponentModel.Container();
            this.hintBarText = new System.Windows.Forms.Label();
            this.solveButton = new System.Windows.Forms.Button();
            this.gridPanel = new System.Windows.Forms.Panel();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.generateNewPuzzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.timerLabel = new System.Windows.Forms.Label();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hintBarText
            // 
            this.hintBarText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.hintBarText.AutoSize = true;
            this.hintBarText.Location = new System.Drawing.Point(12, 456);
            this.hintBarText.Name = "hintBarText";
            this.hintBarText.Size = new System.Drawing.Size(50, 13);
            this.hintBarText.TabIndex = 3;
            this.hintBarText.Text = "Hints Bar";
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
            this.gridPanel.Location = new System.Drawing.Point(12, 79);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.Size = new System.Drawing.Size(442, 360);
            this.gridPanel.TabIndex = 5;
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateNewPuzzleToolStripMenuItem,
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
            // generateNewPuzzleToolStripMenuItem
            // 
            this.generateNewPuzzleToolStripMenuItem.Name = "generateNewPuzzleToolStripMenuItem";
            this.generateNewPuzzleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.generateNewPuzzleToolStripMenuItem.Size = new System.Drawing.Size(478, 34);
            this.generateNewPuzzleToolStripMenuItem.Text = "&Generate New Puzzle";
            this.generateNewPuzzleToolStripMenuItem.Click += new System.EventHandler(this.FileGenerateNewPuzzleClick);
            // 
            // enterPuzzleToolStripMenuItem
            // 
            this.enterPuzzleToolStripMenuItem.Name = "enterPuzzleToolStripMenuItem";
            this.enterPuzzleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.N)));
            this.enterPuzzleToolStripMenuItem.Size = new System.Drawing.Size(478, 34);
            this.enterPuzzleToolStripMenuItem.Text = "Enter &New Puzzle";
            this.enterPuzzleToolStripMenuItem.Click += new System.EventHandler(this.FileEnterPuzzleClick);
            // 
            // saveGameToolStripMenuItem
            // 
            this.saveGameToolStripMenuItem.Name = "saveGameToolStripMenuItem";
            this.saveGameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveGameToolStripMenuItem.Size = new System.Drawing.Size(478, 34);
            this.saveGameToolStripMenuItem.Text = "&Save Game";
            this.saveGameToolStripMenuItem.Click += new System.EventHandler(this.FileSaveGameClick);
            // 
            // saveGameUnsolvedToolStripMenuItem
            // 
            this.saveGameUnsolvedToolStripMenuItem.Name = "saveGameUnsolvedToolStripMenuItem";
            this.saveGameUnsolvedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.saveGameUnsolvedToolStripMenuItem.Size = new System.Drawing.Size(478, 34);
            this.saveGameUnsolvedToolStripMenuItem.Text = "Save Game &Unsolved";
            this.saveGameUnsolvedToolStripMenuItem.Click += new System.EventHandler(this.FileSaveGameUnsolvedClick);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(478, 34);
            this.loadToolStripMenuItem.Text = "&Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.FileLoadClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(475, 6);
            // 
            // quitGameToolStripMenuItem
            // 
            this.quitGameToolStripMenuItem.Name = "quitGameToolStripMenuItem";
            this.quitGameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quitGameToolStripMenuItem.Size = new System.Drawing.Size(478, 34);
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
            this.alwaysShowHintsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.alwaysShowHintsToolStripMenuItem.Size = new System.Drawing.Size(391, 34);
            this.alwaysShowHintsToolStripMenuItem.Text = "&Always Show Hints";
            this.alwaysShowHintsToolStripMenuItem.Click += new System.EventHandler(this.OptionsShowHintsClick);
            // 
            // showErrorsToolStripMenuItem
            // 
            this.showErrorsToolStripMenuItem.Checked = true;
            this.showErrorsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showErrorsToolStripMenuItem.Name = "showErrorsToolStripMenuItem";
            this.showErrorsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.showErrorsToolStripMenuItem.Size = new System.Drawing.Size(391, 34);
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
            this.gameRulesToolStripMenuItem.Size = new System.Drawing.Size(311, 34);
            this.gameRulesToolStripMenuItem.Text = "Online Game &Rules";
            this.gameRulesToolStripMenuItem.Click += new System.EventHandler(this.HelpRulesClick);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(311, 34);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.HelpAboutClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(466, 37);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // timerLabel
            // 
            this.timerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.timerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timerLabel.Location = new System.Drawing.Point(12, 37);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(442, 39);
            this.timerLabel.TabIndex = 6;
            this.timerLabel.Text = "0:00";
            this.timerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 1000;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimerTick);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 490);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.gridPanel);
            this.Controls.Add(this.solveButton);
            this.Controls.Add(this.hintBarText);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameForm";
            this.Text = "Super Sudoku";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameFormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label hintBarText;
        private System.Windows.Forms.Button solveButton;
        private System.Windows.Forms.Panel gridPanel;
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem generateNewPuzzleToolStripMenuItem;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.Timer gameTimer;
    }
}
