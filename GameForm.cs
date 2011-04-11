﻿using System;
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

        private bool showErrors = false;

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
                RecalculateErrors();
                RecalculateHints(row, col);
            };

            gcontrol.CellChange += (int row, int col) =>
            {
                Console.WriteLine("Set grid " + row + "," + col + " to " + grid.Get(row, col));
                RecalculateErrors();
                RecalculateHints(row, col);
            };

            gcontrol.CellFocused += (int row, int col) =>
            {
                Console.WriteLine("Selected grid " + row + "," + col);
                RecalculateHints(row, col);
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
            showErrorsToolStripMenuItem.Checked = !showErrorsToolStripMenuItem.Checked;
            showErrors = showErrorsToolStripMenuItem.Checked;
            RecalculateErrors();
        }

        private void HelpRulesClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HelpAboutClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Recalculates the errors
        /// </summary>
        private void RecalculateErrors()
        {
            gcontrol.ClearErrors();
            if (showErrors)
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
        /// Recalculate the hints bar
        /// </summary>
        private void RecalculateHints(int row, int col)
        {
            List<int> hints = solver.GetHintsFor(grid, row, col);
            hintBarText.Text = "Hints: " + String.Join(",", hints.Select((i)=>""+i).ToArray());
        }
    }
}
