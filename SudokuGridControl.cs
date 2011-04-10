using System;
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
            this.Resize += new EventHandler(FixFontsAndSize);
            this.ResizeRedraw = false;
            InitializeComponent();

            this.grid = bgrid;

            ResetTextboxen();
            UpdateGridView();

            FixFontsAndSize(new Object(), new EventArgs());

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
            //table.Dock = DockStyle.Fill;
            table.Padding = new Padding(2);
            table.Margin = new Padding(0);
            table.RowStyles.Clear();
            table.ColumnStyles.Clear();
            table.ColumnCount = 3;
            table.RowCount = 3;
            table.Controls.Clear();

            // This table will have 9 subtables
            // Each subtable will have 9 textboxes.
            // The only reason why I'm using subtables
            // is because C# doesn't provide me a way of setting border widths. Ew.

            // First, initialize the boxes array
            boxes = new TextBox[9][];
            for (int i = 0; i < 9; i++)
            {
                boxes[i] = new TextBox[9];
            }

            // Then, draw the subtables.
            for (int tableRow = 0; tableRow < 3; tableRow++)
            {
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)(100.0 / 3)));
                table.RowStyles.Add(new RowStyle(SizeType.Percent, (float)(100.0 / 3)));
                
                for (int tableCol = 0; tableCol < 3; tableCol++)
                {
                    // Each subtable
                    TableLayoutPanel subTable = new TableLayoutPanel();
                    table.Controls.Add(subTable, tableCol, tableRow);

                    subTable.Dock = DockStyle.Fill;
                    subTable.Padding = new Padding(0);
                    subTable.Margin = new Padding(1);
                    subTable.RowStyles.Clear();
                    subTable.ColumnStyles.Clear();
                    subTable.ColumnCount = 3;
                    subTable.RowCount = 3;
                    subTable.Controls.Clear();

                    // Draw the nine boxes inside
                    for (int row = 0; row < 3; row++)
                    {
                        subTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)(100.0 / 3)));
                        subTable.RowStyles.Add(new RowStyle(SizeType.Percent, (float)(100.0 / 3)));
                        for (int col = 0; col < 3; col++)
                        {
                            TextBox newBox = makeTextBox(tableRow*3+row, tableCol*3+col);
                            boxes[tableRow * 3 + row][tableCol * 3 + col] = newBox;
                            subTable.Controls.Add(newBox, col, row);
                        }
                    }

                }
            }
        }

        private void FocusBox(int row, int col)
        {
            int targetBox = (((row*9+col) % 81) + 81) % 81; // C# is stupid about negative modulo
            int targetRow = targetBox/9;
            int targetCol = targetBox % 9;
            boxes[targetRow][targetCol].Focus();
            boxes[targetRow][targetCol].SelectAll();
        }

        private TextBox makeTextBox(int row, int col)
        {
            // Make a new textbox
            TextBox newBox = new TabbityTextBox();
            newBox.BorderStyle = BorderStyle.None;
            newBox.Dock = DockStyle.Fill;
            newBox.Multiline = true; // Required to change height
            newBox.TextAlign = HorizontalAlignment.Center;
            newBox.MaxLength = 1;
            newBox.Margin = new Padding(1);
            // Only allow digits
            newBox.KeyPress += (object sender, KeyPressEventArgs e) =>
            {
                if (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            };
            newBox.TextChanged += (object Sender, EventArgs e) =>
            {
                if (newBox.Text != "")
                {
                    SetCell(row, col, Int16.Parse(newBox.Text));
                    newBox.SelectAll();
                }
            };
            // Change focus
            newBox.KeyDown += (object sender, KeyEventArgs e) =>
            {
                switch (e.KeyData)
                {
                    case Keys.Down:
                        FocusBox(row + 1, col);
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.Up:
                        FocusBox(row - 1, col);
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.Tab | Keys.Shift:
                    case Keys.Left:
                        FocusBox(row, col-1);
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.Tab:
                    case Keys.Enter:
                    case Keys.Right:
                        FocusBox(row, col+1);
                        e.SuppressKeyPress = true;
                        break;

                    case Keys.Back:
                    case Keys.Space:
                    case Keys.Delete:
                        ClearCell(row, col);
                        e.SuppressKeyPress = true;
                        break;
                }
            };

            return newBox;
        }

        private void ClearCell(int row, int col)
        {
            if (grid.IsEditable(row, col)) {
                boxes[row][col].Text = "";
            }
        }

        private void SetCell(int row, int col, int value)
        {
            if (grid.IsEditable(row, col)) {
                grid.Set(value, true, row, col);
            }
        }

        private void FixFontsAndSize(object sender, EventArgs e)
        {
            this.table.Size = new Size(
                    Math.Min(this.Width, this.Height),
                    Math.Min(this.Width, this.Height)
                );
            this.table.Location = new Point(
                    this.Width / 2 - this.table.Width / 2,
                    this.Height / 2 - this.table.Height / 2
                );
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
