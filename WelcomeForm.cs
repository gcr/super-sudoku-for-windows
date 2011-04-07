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
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            DifficultyForm dform = new DifficultyForm();
            Generator gen = new Generator();
            dform.ShowDialog();
            if (dform.HasResult)
            {
                this.Hide();
                gen.Generate(dform.Result);
                GameForm gform = new GameForm(gen.SolutionGrid);
                gform.ShowDialog();
                this.Close();
            }
        }

        /// <summary>
        /// When the user clicks the Edit New Game button, run the game with that form
        /// </summary>
        private void editButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            GameForm gform = new GameForm(new Grid());
            gform.ShowDialog();
            this.Close();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}