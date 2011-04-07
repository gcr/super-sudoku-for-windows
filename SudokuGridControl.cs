﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SuperSudoku
{
    public partial class SudokuGridControl : UserControl
    {
        private Grid grid;
        private TextBox[][] boxes;
        
        public Grid BackingGrid
        {
            get { return grid; }
        }
        public SudokuGridControl(Grid bgrid)
        {
            this.Resize += new EventHandler(FixFonts);
            this.ResizeRedraw = false;
            InitializeComponent();

            this.grid = bgrid;

            ResetTextboxen();
            UpdateGridView();

            FixFonts(new Object(), new EventArgs());

        }

        private void ForEachTextBox(Action<TextBox, int, int> fun)
        {
            for (int i=0; i<boxes.Length; i++) {
                for (int j=0; j<boxes[i].Length; j++) {
                    fun(boxes[i][j], i, j);
                }
            }
        }

        private void ResetTextboxen()
        {
            // ADD EIGHTY ONE TEXT BOXEN
            table.Dock = DockStyle.Fill;
            table.Margin = new Padding(0);
            table.Padding = new Padding(0);
            table.RowStyles.Clear();
            table.ColumnStyles.Clear();
            table.ColumnCount = 9;
            table.RowCount = 9;
            table.Controls.Clear();

            boxes = new TextBox[9][];
            for (int row = 0; row < 9; row++)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)(100.0 / 9)));
                table.RowStyles.Add(new RowStyle(SizeType.Percent, (float)(100.0 / 9)));
                boxes[row] = new TextBox[9];
                for (int col = 0; col < 9; col++)
                {
                    TextBox newBox = makeTextBox(row, col);
                    boxes[row][col] = newBox;
                    table.Controls.Add(newBox, col, row);
                }
            }
        }

        private TextBox makeTextBox(int row, int col)
        {
            // Make a new textbox
            TextBox newBox = new TextBox();
            newBox.Dock = DockStyle.Fill;
            newBox.Text = ""+row;
            newBox.Multiline = true; // Required to change height
            newBox.TextAlign = HorizontalAlignment.Center;
            newBox.MaxLength = 1;
            newBox.Margin = new Padding(0);
            return newBox;
        }

        private void FixFonts(object sender, EventArgs e)
        {
            if (boxes != null)
            {
                Font f = findSuitableFont(this.Width/9, this.Height / 9);
                ForEachTextBox((tbox, row, col) =>
                {
                    tbox.Font = f;
                });
            }
        }

        /// <summary>
        /// Find a good font that fits inside the bounding box
        /// </summary>
        private Font findSuitableFont(int targetWidth, int targetHeight)
        {
            Graphics measurer = CreateGraphics();

            Font bestFont = new Font(this.Font.FontFamily, 8);
            float bestDistance = float.MaxValue;
            for (int i = 8; i < 72; i++)
            {
                // only considering height
                SizeF size = measurer.MeasureString("0123456789", new Font(this.Font.FontFamily, i));
                if (size.Height < targetHeight && bestDistance > (targetHeight - size.Height))
                {
                    bestDistance = targetHeight - size.Height;
                    bestFont = new Font(this.Font.FontFamily, i);
                }
            }

            return bestFont;
        }

        private void UpdateGridView()
        {
            if (boxes != null)
            {
                ForEachTextBox((tbox, row, col) =>
                {
                    int boxVal = grid.Get(row, col);
                    tbox.Text = (boxVal != 0) ? ""+boxVal : "";
                    if (grid.IsEditable(row, col)) {
                        makeCellEditable(row, col);
                    } else {
                        makeCellIneditable(row, col);
                    }
                });
            }
        }

        private void makeCellIneditable(int row, int col)
        {
            TextBox tbox = boxes[row][col];
            tbox.BackColor = Color.LightGray;
            tbox.ForeColor = Color.DarkGray;
            tbox.ReadOnly = true;
        }

        private void makeCellEditable(int row, int col)
        {
            TextBox tbox = boxes[row][col];
            tbox.BackColor = Color.White;
            tbox.ForeColor = Color.Black;
            tbox.ReadOnly = false;
        }

    }
}