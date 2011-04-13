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
            Generator gen = new Generator();
            dform.ShowDialog();
            if (dform.HasResult)
            {
                form.Hide();
                gen.Generate(dform.Result);
                GameForm gform = new GameForm(gen.SolutionGrid, true);
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
            GameForm gform = new GameForm(new Grid(), true);
            gform.ShowDialog();
            form.Close();
        }
    }
}
