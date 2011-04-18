using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SuperSudoku
{
    /// <summary>
    /// This class implements UI state transitinos all in one place; both the
    /// welcome screen's buttons and the game screen's File menu hook in here.
    /// </summary>
    public class GameManager
    {
        /// <summary>
        /// Shows a dialog for saving the grid you pass.
        /// </summary>
        /// <param name="grid">The grid to be saved</param>
        /// <returns>Whether the save succeeded. False if the user canceled.</returns>
        public static bool SaveGame(Grid grid)
        {
            bool result = false;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Sudoku Game|*.sud";
            dialog.Title = "Save Game";
            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                new File().WriteFile(grid, dialog.FileName);
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Loads a game from disk (showing load dialog) and then shows the game form.
        /// </summary>
        /// <param name="form">This form will be hidden.</param>
        public static bool LoadGame(Form form)
        {
            bool result = false;
            File fileOpener = new File();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Sudoku Game|*.sud";
            dialog.Title = "Load Game";
            dialog.ShowDialog();

            if (dialog.FileName != "")
            {
                form.Hide();
                Grid newGrid = new Grid();
                fileOpener.ReadFile(newGrid, dialog.FileName);
                GameForm gform = new GameForm(newGrid, true);
                gform.ShowDialog();
                form.Close();
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Shows the dialog for creating a puzzle. May also show the new game screen.
        /// </summary>
        /// <param name="form">This form will be hidden.</param>
        public static void GeneratePuzzle(Form form)
        {
            DifficultyForm dform = new DifficultyForm();
            dform.ShowDialog();
            if (dform.HasResult)
            {
                form.Hide();
                GameForm gform = new GameForm(dform.Result, true);
                gform.ShowDialog();
                form.Close();
            }
        }

        /// <summary>
        /// Shows a blank game screen.
        /// </summary>
        /// <param name="form">This form will be hidden.</param>
        public static void EnterNewPuzzle(Form form)
        {
            form.Hide();
            GameForm gform = new GameForm(new Grid(), false);
            gform.ShowDialog();
            form.Close();
        }

        /// <summary>
        /// When the player transitions from "edit grid" mode to "play this game" mode
        /// </summary>
        internal static void PlayThisPuzzle(Grid oldGrid, Form form)
        {
            Grid grid = oldGrid.Copy();
            grid.ForEachSquare((row, col, val) =>
            {
                if (val != 0)
                {
                    grid.SetEditable(false, row, col);
                }
            });
            Grid copyGrid = grid.Copy();
            bool result = (new Solver().Solve(copyGrid));
            if (result)
            {
                form.Hide();
                GameForm gform = new GameForm(grid, true);
                gform.ShowDialog();
                form.Close();
            }
            else
            {
                if (MessageBox.Show(copyGrid.IsFull()? "This puzzle can be solved in more than one way. Play anyway?" : "This puzzle cannot be solved. Play anyway?", "Unsolvable Puzzle", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    form.Hide();
                    GameForm gform = new GameForm(grid, true);
                    gform.ShowDialog();
                    form.Close();
                }
            }
        }
    }
}
