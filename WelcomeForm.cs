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
    public partial class WelcomeForm : Form
    {

        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            GameManager.GeneratePuzzle(this);
        }

        /// <summary>
        /// When the user clicks the Edit New Game button, run the game with that form
        /// </summary>
        private void editButton_Click(object sender, EventArgs e)
        {
            GameManager.EnterNewPuzzle(this);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            GameManager.LoadGame(this);
        }
    }
}
