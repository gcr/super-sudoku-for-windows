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

    /// <summary>
    /// This form is the "Generate New Puzzle" screen.
    /// It allows the user to select between easy,
    /// medium, and difficult puzzles to play.
    /// </summary>
    public partial class DifficultyForm : Form
    {
        private Grid result;
        public Grid Result
        {
            get { return result; }
        }

        private bool hasResult = false;
        public bool HasResult
        {
            get { return hasResult; }
        }

        private Grid easy;
        private Grid med;
        private Grid hard;

        Generator gen = new Generator();

        public DifficultyForm()
        {
            InitializeComponent();

            makeNewGrids();
        }

        /// <summary>
        /// This method creates three new sudoku grids.
        /// </summary>
        private void makeNewGrids()
        {
            easy = gen.Generate(DifficultyLevel.Easy);
            easyPanel.Controls.Clear();
            SudokuGridControl easyGcontrol = new SudokuGridControl(easy);
            easyPanel.Controls.Add(easyGcontrol);
            easyGcontrol.Dock = DockStyle.Fill;
            easyGcontrol.IsEditable = false;

            med = gen.Generate(DifficultyLevel.Medium);
            medPanel.Controls.Clear();
            SudokuGridControl medGcontrol = new SudokuGridControl(med);
            medPanel.Controls.Add(medGcontrol);
            medGcontrol.Dock = DockStyle.Fill;
            medGcontrol.IsEditable = false;

            hard = gen.Generate(DifficultyLevel.Hard);
            hardPanel.Controls.Clear();
            SudokuGridControl hardGcontrol = new SudokuGridControl(hard);
            hardPanel.Controls.Add(hardGcontrol);
            hardGcontrol.Dock = DockStyle.Fill;
            hardGcontrol.IsEditable = false;
        }

        private void easyButton_Click(object sender, EventArgs e)
        {
            result = easy;
            hasResult = true;
            this.Close();
        }

        private void medButtonClick(object sender, EventArgs e)
        {
            result = med;
            hasResult = true;
            this.Close();
        }

        private void hardButton_Click(object sender, EventArgs e)
        {
            result = hard;
            hasResult = true;
            this.Close();
        }
    }
}
