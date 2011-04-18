using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSudoku
{

    /// <summary>
    /// This class is a sudoku grid.
    /// </summary>
    public class Grid
    {

        private int[][] elts;

        /// <summary>
        /// Constructor: Turn a flat list of elements into a sudoku grid.
        /// </summary>
        /// <param name="elements">Flat list of 81 elements. Each section of nine elements is a new row.</param>
        public Grid(int[] elements)
        {
            if (elements.Length != 81)
            {
                throw new InvalidOperationException("Expected a grid with 9 elements, got " + elements.Length);
            }
            int part = 0;
            this.elts = elements
                .GroupBy((int index) => part++ / 9)
                .Select((row) => row.ToArray())
                .ToArray();
        }

        /// <summary>
        /// Constructor: Turn a jagged array of elements into a grid.
        /// </summary>
        /// <param name="elements">Jagged 9x9 array of elements. Row-major.</param>
        public Grid(int[][] elements)
        {
            if (elements.Length != 9 || elements[0].Length != 9)
            {
                throw new InvalidOperationException("Expected a grid with 9 elements to each dimension");
            }
            this.elts = elements;
        }

        /// <summary>
        /// Constructor: Turn a 2D list into elements.
        /// </summary>
        /// <param name="elements">9x9 2D list of elements. Row-major.</param>
        public Grid(List<List<int>> elements)
        {
            if (elements.Count != 9 || elements[0].Count != 9)
            {
                throw new InvalidOperationException("Expected a grid with 9 elements to each dimension");
            }
            this.elts = elements.Select((elt) => elt.ToArray()).ToArray();
        }

        /// <summary>
        /// Gets the cell at given row and column. Returns a positive number.
        /// </summary>
        public int Get(int row, int col)
        {
            return Math.Abs(elts[row][col]);
        }

        /// <summary>
        /// Clears the given cell, making it editable
        /// </summary>
        public void Clear(int row, int col)
        {
            elts[row][col] = 0;
        }

        /// <summary>
        /// Sets the value of row, col to the given value
        /// </summary>
        public void Set(int val, bool isEditable, int row, int col)
        {
            elts[row][col] = (isEditable? 1 : -1) * Math.Abs(val);
        }

        /// <summary>
        /// Returns whether the element at row, col is editable
        /// </summary>
        public bool IsEditable(int row, int col)
        {
            return elts[row][col] >= 0;
        }

        /// <summary>
        /// Sets whether the element at row, col is editable or not.
        /// </summary>
        public void SetEditable(bool isEditable, int row, int col)
        {
            elts[row][col] = (isEditable? 1 : -1) * Math.Abs(elts[row][col]);
        }


        /// <summary>
        /// Return all the elements in the given column.
        /// </summary>
        public int[] GetColumn(int col)
        {
            return new int[] {
                Math.Abs(elts[0][col]),
                Math.Abs(elts[1][col]),
                Math.Abs(elts[2][col]),
                Math.Abs(elts[3][col]),
                Math.Abs(elts[4][col]),
                Math.Abs(elts[5][col]),
                Math.Abs(elts[6][col]),
                Math.Abs(elts[7][col]),
                Math.Abs(elts[8][col])
            };

            //return elts.Select((row) => Math.Abs(row[col])).ToArray();
        }

        /// <summary>
        /// Return all the elements in the given row.
        /// </summary>
        public int[] GetRow(int row)
        {
            return new int[] {
                Math.Abs(elts[row][0]),
                Math.Abs(elts[row][1]),
                Math.Abs(elts[row][2]),
                Math.Abs(elts[row][3]),
                Math.Abs(elts[row][4]),
                Math.Abs(elts[row][5]),
                Math.Abs(elts[row][6]),
                Math.Abs(elts[row][7]),
                Math.Abs(elts[row][8])
            };

            //return (int[])elts[row].Select((item) => Math.Abs(item)).ToArray();
        }

        /// <summary>
        /// Return all the elements in the 3x3 "big" square that encompasses
        /// the cell at row, col.
        /// 
        /// Given the grid that looks like
        /// 
        /// ? ? ?  ? ? ?  ? ? ?
        /// ? ? ?  ? ? ?  ? ? ?
        /// ? ? ?  ? ? ?  ? ? ?
        /// 
        /// ? ? ?  ? ? ?  2 0 3
        /// ? ? ?  ? ? ?  1 9 5
        /// ? ? ?  ? ? ?  4 8 7
        /// 
        /// ? ? ?  ? ? ?  ? ? ?
        /// ? ? ?  ? ? ?  ? ? ?
        /// ? ? ?  ? ? ?  ? ? ?
        /// 
        /// Calling GetSquareAbout(5, 6) will return {2, 0, 3, 1, 9, 5, 4, 8, 7}
        /// because cell at row 5, col 6 is in that square.
        /// </summary>
        public int[] GetSquareAbout(int row, int col)
        {
            int sqRow = ((int)row / 3) * 3;
            int sqCol = ((int)col / 3) * 3;
            // now sqRow and sqCol will each be either 0, 3, or 6.
            // This is the top left corner of the square encompassing
            // the given cell.

            int[] result = new int[9];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result[i*3+j] = Math.Abs(this.elts[i + sqRow][j + sqCol]);
                }
            }
            return result;
        }


        /// <summary>
        /// Constructor: Create a completely blank sudoku grid.
        /// </summary>
        public Grid()
        {
            this.elts = Enumerable
                        .Repeat(0, 9)
                        .Select((_) => Enumerable.Repeat(0, 9).ToArray())
                        .ToArray();
        }

        /// <summary>
        /// Return a copy of this grid.
        /// </summary>
        public Grid Copy()
        {
            return new Grid(this.elts.Select((row) => (int[])row.Clone()).ToArray());
        }

        /// <summary>
        /// Returns whether the grid is full or not.
        /// </summary>
        public bool IsFull()
        {
            bool result = true;
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (elts[row][col] == 0)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Apply the given action to each square in the grid.
        /// </summary>
        /// <param name="fun">Function that takes row, column, and current square.</param>
        public void ForEachSquare(Action<int, int, int> fun)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    fun(row, col, Get(row, col));
                }
            }
        }

        /// <summary>
        /// Overwrite this grid's elements with elements from another grid
        /// </summary>
        public void CopyFrom(Grid other)
        {
            this.elts = other.Copy().elts;
        }
    }
}
