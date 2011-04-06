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
    public partial class DifficultyForm : Form
    {
        private DifficultyLevel result;
        private bool hasResult = false;


        public DifficultyForm()
        {
            InitializeComponent();
        }

        public DifficultyLevel Result
        {
            get { return result; }
        }

        public bool HasResult
        {
            get { return hasResult; }
        }

        private void easyButton_Click(object sender, EventArgs e)
        {
            result = DifficultyLevel.Easy;
            hasResult = true;
            this.Close();
        }

        private void n_Click(object sender, EventArgs e)
        {
            result = DifficultyLevel.Medium;
            hasResult = true;
            this.Close();
        }

        private void hardButton_Click(object sender, EventArgs e)
        {
            result = DifficultyLevel.Hard;
            hasResult = true;
            this.Close();
        }
    }
}
