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

        public GameForm(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            this.gcontrol = new SudokuGridControl(grid);
            this.gridPanel.Controls.Add(gcontrol);
            gcontrol.Dock = DockStyle.Fill;

            gcontrol.CellClear += (int row, int col) =>
            {
                Console.WriteLine("Cleared grid row " + row + " col " + col);
            };

            gcontrol.CellChange += (int row, int col) =>
            {
                Console.WriteLine("Set grid " + row + "," + col + " to " + grid.Get(row, col));
            };

            gcontrol.CellFocused += (int row, int col) =>
            {
                Console.WriteLine("Selected grid " + row + "," + col);
            };
        }

        private void FileNewGameClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FileEnterPuzzleClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FileSaveGameClick(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Sudoku Game|*.sud";
            dialog.Title = "Save Game";
            dialog.ShowDialog();

            fileWriter.WriteFile(grid, dialog.FileName);
        }

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

            fileWriter.WriteFile(unsolvedGrid, dialog.FileName);
        }

        private void FileLoadClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FileQuitClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OptionsShowHintsClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OptionsShowErrorsClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HelpRulesClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HelpAboutClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
