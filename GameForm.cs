using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SuperSudoku
{
    public partial class GameForm : Form
    {
        private Grid grid;
        private SudokuGridControl gcontrol;
        private File fileWriter = new File();
        private Solver solver = new Solver();

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

            this.gcontrol = new SudokuGridControl(grid);
            this.gridPanel.Controls.Add(gcontrol);
            gcontrol.Dock = DockStyle.Fill;

            gcontrol.CellClear += (int row, int col) =>
            {
                Console.WriteLine("Cleared grid row " + row + " col " + col);
                RecalculateErrors();
                RecalculateHints(row, col);
                ShowOrHideHintBar();
            };

            gcontrol.CellChange += (int row, int col) =>
            {
                Console.WriteLine("Set grid " + row + "," + col + " to " + grid.Get(row, col));
                RecalculateErrors();
                RecalculateHints(row, col);
                ShowOrHideHintBar();
            };

            gcontrol.CellFocused += (int row, int col) =>
            {
                Console.WriteLine("Selected grid " + row + "," + col);
                RecalculateHints(row, col);
                ShowOrHideHintBar();
            };

            gcontrol.ForEachTextBox((TextBox tbox, int row, int col) =>
            {
                tbox.ContextMenu = new ContextMenu();
                //for (MenuItem mnu in contextMenu.
            });
        }

        /// <summary>
        /// When the File -> Enter New Puzzle menu is chosen
        /// </summary>
        private void FileEnterPuzzleClick(object sender, EventArgs e)
        {
            MessageBox.Show("TODO: check for errors"); // NotImplementedException

            this.Hide();
            GameForm gform = new GameForm(new Grid(), true);
            gform.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// When the File -> "Save Game" menu item is chosen
        /// </summary>
        private void FileSaveGameClick(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Sudoku Game|*.sud";
            dialog.Title = "Save Game";
            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                fileWriter.WriteFile(grid, dialog.FileName);
            }
        }

        /// <summary>
        /// When the File -> Save Game Unsolved menu item is chosen
        /// </summary>
        private void FileSaveGameUnsolvedClick(object sender, EventArgs e)
        {
            Grid unsolvedGrid = grid.Copy();
            unsolvedGrid.ForEachSquare((row,col,val) =>
            {
                if (grid.IsEditable(row,col)) {
                    grid.Clear(row,col);
                }
            });
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Sudoku Game|*.sud";
            dialog.Title = "Save Game";
            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                fileWriter.WriteFile(unsolvedGrid, dialog.FileName);
            }
        }

        /// <summary>
        /// When the File -> Load Game menu item is chosen, load a new game
        /// </summary>
        private void FileLoadClick(object sender, EventArgs e)
        {
            MessageBox.Show("TODO: check for errors"); // NotImplementedException
            File fileOpener = new File();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Sudoku Game|*.sud";
            dialog.Title = "Load Game";
            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                this.Hide();
                Grid newGrid = new Grid();
                fileOpener.ReadFile(grid, dialog.FileName);
                GameForm gform = new GameForm(newGrid, true);
                gform.ShowDialog();
                this.Close();
            }
        }

        /// <summary>
        /// When the File -> Quit menu item is chosen
        /// </summary>
        private void FileQuitClick(object sender, EventArgs e)
        {
            MessageBox.Show("TODO: check for errors"); // NotImplementedException
            this.Close();
        }

        /// <summary>
        /// When the Options -> Show Hints menu item is chosen
        /// </summary>
        private void OptionsShowHintsClick(object sender, EventArgs e)
        {
            alwaysShowHintsToolStripMenuItem.Checked = !alwaysShowHintsToolStripMenuItem.Checked;
            ShowOrHideHintBar();
        }

        /// <summary>
        /// When the Options -> Show ERrors menu item is chosen
        /// </summary>
        private void OptionsShowErrorsClick(object sender, EventArgs e)
        {
            showErrorsToolStripMenuItem.Checked = !showErrorsToolStripMenuItem.Checked;
            RecalculateErrors();
        }

        /// <summary>
        /// When the Help -> Sudoku Rules menu item is chosen
        /// </summary>
        private void HelpRulesClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When the Help -> About menu item is chosen
        /// </summary>
        private void HelpAboutClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When the Solve button is clicked
        /// </summary>
        private void SolveClick(object sender, EventArgs e)
        {
            solver.Solve(grid);
            gcontrol.UpdateGridView();
        }

        /// <summary>
        /// Recalculates the errors on the grid, show them as red
        /// </summary>
        private void RecalculateErrors()
        {
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
            List<int> hints = solver.GetHintsFor(grid, row, col);
            hintBarText.Text = "Hints: " + String.Join(",", hints.Select((i)=>""+i).ToArray());
        }
    }
}
