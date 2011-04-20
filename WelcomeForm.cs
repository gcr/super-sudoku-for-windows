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
    /// The form that the users see first.
    /// </summary>
    public partial class WelcomeForm : Form
    {

        public WelcomeForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When the "generate puzzle" button is clicked
        /// </summary>
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

        /// <summary>
        /// When the user clicks "Load"
        /// </summary>
        private void loadButton_Click(object sender, EventArgs e)
        {
            GameManager.LoadGame(this);
        }
    }
}
