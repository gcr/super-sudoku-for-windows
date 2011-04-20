using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SuperSudoku
{
    public partial class GameForm : Form
    {
        // The actual grid object
        private Grid grid;
        // The UI control that holds the grid object
        private SudokuGridControl gcontrol;
        // The Solver so we can solve grids
        private Solver solver = new Solver();
        // The game time in seconds
        private int gameTime = 0;
        // UI flags
        private bool nagAboutErrors = true;
        private bool nagAboutWonGame = true;
        // "True" if we're in playing mode. "False" if we're in editing mode.
        private bool isPlaying;

        /// <summary>
        /// Creates a new form for playing the game
        /// </summary>
        /// <param name="grid">The grid to start with</param>
        /// <param name="playingMode">If true: We're "playing" the game. If false: We're "editing" the game.</param>
        public GameForm(Grid grid, bool playingMode)
        {
            InitializeComponent();
            this.grid = grid;
            this.isPlaying = playingMode;

            hintBarText.Text = "";
            if (isPlaying)
            {
                solveButton.Text = "&Solve";
            }
            else
            {
                solveButton.Text = "&Enter Puzzle";
            }
                
            // make the grid control and put it somewhere
            this.gcontrol = new SudokuGridControl(grid);
            this.gridPanel.Controls.Add(gcontrol);
            gcontrol.Dock = DockStyle.Fill;

            // When the cell is cleared, mark/unmark errors and hints
            gcontrol.CellClear += (int row, int col) =>
            {
                nagAboutErrors = true;
                nagAboutWonGame = true;
                RecalculateErrors();
                RecalculateHints(row, col);
                ShowOrHideHintBar();
            };

            // When the cell's value is changed, mark/unmark errors and hints
            gcontrol.CellChange += (int row, int col) =>
            {
                MaybeTryGameOver();
                RecalculateErrors();
                RecalculateHints(row, col);
                ShowOrHideHintBar();
            };

            // When the user selects a different cell, mark/unmark errors and hints
            gcontrol.CellFocused += (int row, int col) =>
            {
                ShowOrHideHintBar();
                if (isPlaying)
                {
                    RecalculateHints(row, col);
                }
            };

            // Add a drop-down context menu to each textbox
            gcontrol.ForEachTextBox((TextBox tbox, int row, int col) =>
            {
                tbox.ContextMenu = new ContextMenu();
                if (isPlaying)
                {
                    // The context menu should only appear when in "Playing" mode
                    tbox.ContextMenu.MenuItems.Add(new MenuItem("Show &Hints", (s, e) =>
                        {
                            hintBarText.Show();
                            // the hints bar will disappear on next call to ShowOrHideHints()
                        }));
                    tbox.ContextMenu.MenuItems.Add(new MenuItem("&Solve This Square", (s, e) =>
                        {
                            // solve the grid and copy the value
                            if (grid.IsEditable(row, col))
                            {
                                Grid solvedGrid = grid.Copy();
                                solver.Solve(solvedGrid);
                                if (!solvedGrid.IsFull())
                                {
                                    MessageBox.Show("This square cannot be solved because there are errors in your puzzle. Please erase some of your work and try again.");
                                }
                                else
                                {
                                    grid.Set(solvedGrid.Get(row, col), true, row, col);
                                }
                                gcontrol.UpdateGridView();
                                RecalculateErrors();
                            }
                        }));
                }
            });

            // initial setup: start the timer and optionally show errors.
            gameTimerTick(this, new EventArgs());
            RecalculateErrors();
            ShowOrHideHintBar();
        }

        /// <summary>
        /// Check to see if the game is finished.
        /// If the grid is full, nag the user about it.
        /// </summary>
        private void MaybeTryGameOver()
        {
            if (isPlaying && grid.IsFull())
            {
                if (solver.FindErrors(grid).Count > 0)
                {
                    // The user incorrectly entered some values
                    if (nagAboutErrors && showErrorsToolStripMenuItem.Checked)
                    {
                        MessageBox.Show("There are errors.");
                    }
                    else if (nagAboutErrors && MessageBox.Show("There are errors, would you like to see them?", "Errors", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        showErrorsToolStripMenuItem.Checked = true;
                        RecalculateErrors();
                    }
                    nagAboutErrors = false;
                    // don't bother them again
                }
                else if (nagAboutWonGame)
                {
                    // The user correctly solved the grid.
                    nagAboutWonGame = false;
                    MessageBox.Show("Good job! Your time: "+formatGameTime());
                }
            }
        }


        /// <summary>
        /// When the File -> Generate New Puzzle menu item is clicked
        /// </summary>
        private void FileGenerateNewPuzzleClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save your game first?", "Save game?", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                if (GameManager.SaveGame(grid))
                {
                    GameManager.GeneratePuzzle(this);
                }
            }
            else if (result == DialogResult.No)
            {
                GameManager.GeneratePuzzle(this);
            }
        }

        /// <summary>
        /// When the File -> Enter New Puzzle menu is chosen
        /// </summary>
        private void FileEnterPuzzleClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save your game first?", "Save game?", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                if (GameManager.SaveGame(grid))
                {
                    GameManager.EnterNewPuzzle(this);
                }
            }
            else if (result == DialogResult.No)
            {
                GameManager.EnterNewPuzzle(this);
            }
        }

        /// <summary>
        /// When the File -> "Save Game" menu item is chosen
        /// </summary>
        private void FileSaveGameClick(object sender, EventArgs e)
        {
            GameManager.SaveGame(grid);
        }

        /// <summary>
        /// When the File -> Save Game Unsolved menu item is chosen
        /// </summary>
        private void FileSaveGameUnsolvedClick(object sender, EventArgs e)
        {
            Grid unsolvedGrid = grid.Copy();
            unsolvedGrid.ForEachSquare((row,col,val) =>
            {
                if (unsolvedGrid.IsEditable(row,col)) {
                    unsolvedGrid.Clear(row,col);
                }
            });
            GameManager.SaveGame(unsolvedGrid);
        }

        /// <summary>
        /// When the File -> Load Game menu item is chosen, load a new game
        /// </summary>
        private void FileLoadClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save your game first?", "Save game?", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                if (GameManager.SaveGame(grid))
                {
                    GameManager.LoadGame(this);
                }
            }
            else if (result == DialogResult.No)
            {
                GameManager.LoadGame(this);
            }
        }

        /// <summary>
        /// When the File -> Quit menu item is chosen
        /// </summary>
        private void FileQuitClick(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to save your game first?", "Save game?", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                if (GameManager.SaveGame(grid))
                {
                    this.Close();
                }
            }
            else if (result == DialogResult.No)
            {
                this.Close();
            }
        }

        /// <summary>
        /// When the Options -> Show Hints menu item is chosen
        /// </summary>
        private void OptionsShowHintsClick(object sender, EventArgs e)
        {
            // toggle checkbox
            alwaysShowHintsToolStripMenuItem.Checked = !alwaysShowHintsToolStripMenuItem.Checked;
            ShowOrHideHintBar();
        }

        /// <summary>
        /// When the Options -> Show ERrors menu item is chosen
        /// </summary>
        private void OptionsShowErrorsClick(object sender, EventArgs e)
        {
            // toggle checkbox
            showErrorsToolStripMenuItem.Checked = !showErrorsToolStripMenuItem.Checked;
            RecalculateErrors();
        }

        /// <summary>
        /// When the Help -> Sudoku Rules menu item is chosen
        /// </summary>
        private void HelpRulesClick(object sender, EventArgs e)
        {
            // Abin wrote our wonderful hand-written manual, so pop that open
            Process.Start("http://gcr.github.com/super-sudoku-for-windows/");
        }

        /// <summary>
        /// When the Help -> About menu item is chosen
        /// </summary>
        private void HelpAboutClick(object sender, EventArgs e)
        {
            // just an about dialog
            new AboutBox().ShowDialog();
        }

        /// <summary>
        /// When the Solve button is clicked
        /// </summary>
        private void SolveClick(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                if (MessageBox.Show("Are you sure you want the computer to solve the puzzle?", "Solve now?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    bool canSolve = solver.Solve(grid);
                    if (!grid.IsFull())
                    {
                        MessageBox.Show("This puzzle has errors. Erase some of your work and try again.");
                    }
                    else
                    {
                        nagAboutWonGame = false;
                    }
                    gcontrol.UpdateGridView();
                    RecalculateErrors();
                }
            }
            else
            {
                GameManager.PlayThisPuzzle(grid, this);
            }
        }

        /// <summary>
        /// Recalculates the errors on the grid, show them as red
        /// </summary>
        private void RecalculateErrors()
        {
            // the grid control can show squares in red. Clear them and then
            // do it again.
            gcontrol.ClearErrors();
            if (showErrorsToolStripMenuItem.Checked)
            {
                foreach (List<int> sq in solver.FindErrors(grid))
                {
                    int row = sq[0];
                    int col = sq[1];
                    gcontrol.MarkError(row, col);
                }
            }
        }

        /// <summary>
        /// Shows or hides the hints bar
        /// </summary>
        private void ShowOrHideHintBar()
        {
            hintBarText.Visible = alwaysShowHintsToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Recalculate the hints in the hints bar
        /// </summary>
        private void RecalculateHints(int row, int col)
        {
            if (isPlaying)
            {
                List<int> hints = solver.GetHintsFor(grid, row, col);
                hintBarText.Text = "Hints: " + String.Join(",", hints.Select((i) => "" + i).ToArray());
            }
            else
            {
                Grid copy = grid.Copy();
                if (solver.Solve(copy))
                {
                    hintBarText.Text = "";
                }
                else
                {
                    if (copy.IsFull())
                    {
                        hintBarText.Text = "This puzzle has more than one solution.";
                    }
                    else {
                        hintBarText.Text = "This puzzle cannot be solved.";
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if the game is successfully completed.
        /// </summary>
        private bool IsGameFinished()
        {
            return grid.IsFull() && solver.FindErrors(grid).Count == 0;
        }

        /// <summary>
        /// Formats a time.
        /// Ex: If gameTime is 75, returns "1:15"
        /// </summary>
        private string formatGameTime()
        {
            return gameTime / 60 + ":" + ((gameTime % 60) < 10 ? "0" : "") + gameTime%60;
        }

        /// <summary>
        /// Each second, update timer
        /// </summary>
        private void gameTimerTick(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                timerLabel.Text = formatGameTime();
                gameTime = gameTime + 1;
            }
            else
            {
                timerLabel.Text = "Editing puzzle";
            }
        }

        /// <summary>
        /// When the user closes the form, prompt for save
        /// </summary>
        private void GameFormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Visible)
            {
                DialogResult result = MessageBox.Show("Do you want to save your game first?", "Save game?", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    GameManager.SaveGame(grid);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
