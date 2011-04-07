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

        public GameForm(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            this.gcontrol = new SudokuGridControl(grid);
            this.gridPanel.Controls.Add(gcontrol);
            gcontrol.Dock = DockStyle.Fill;
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
            throw new NotImplementedException();
        }

        private void FileSaveGameUnsolvedClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
