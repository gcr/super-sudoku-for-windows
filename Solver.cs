using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSudoku
{
    /*
     * Depth-first search works great for puzzles that look like puzzles
     * but performance is quite inhibitive for mostly empty grids where
     * the first solution can be half the search space away.
     * 
     * To avoid an infuriatingly slow user experience, certain decisions
     * must be made. I am going to use a different, more efficient algoricthm
     * in slight violation of the design document.
     * 
     * Donld Knuth developed "Algorithm X," which solves sudoku puzzles by
     * representing the state of a puzzle as a massive array that encapsulates
     * both the state of the puzzle and the constraints placed upon the sudoku
     * grid. Each row specifies a numeric value for one cell. One row could
     * represent putting a '3' in the top-right corner, for example. Another
     * row could represent putting a 5 in dead center.
     * 
     * The problem then becomes: Pick a subset of these rows that generates
     * a valid solution.
     * 
     * Sudoku puzzles have four "constraints":
     *      - Every cell must have one number
     *      - Every row must have one of each number
     *      - Every column must have one of each number
     *      - Every grid "box" must have one of each number
     * 
     * Adapted from http://code.google.com/p/narorumo/wiki/SudokuDLX
     */

    public class Solver
    {
        private List<int> Encode(int major, int minor)
        {
            List<int> result = Enumerable.Repeat(0, 81).ToList();
            result[major * 9 + minor] = 1;
            return result;
        }

        private void Decode(List<int> row, out int major, out int minor)
        {
            major = 0;
            minor = 0;
            for (int i = 0; i < row.Count; i++)
            {
                if (row[i] == 1)
                {
                    // found one!
                    major = i / 9;
                    minor = i % 9;
                }
            }
        }

        private List<int> EncodeCell(int val, int row, int col)
        {
            // The first 81 columns represent the constraint
            // that the certain cell is filled.
            return Encode(row, col).Concat(
                // The next 81 columns represent the constraint
                // that each row has this number.
                   Encode(val, row).Concat(
                // The next 81 columns represent the constraint
                // that each column has this number.
                   Encode(val, col).Concat(
                // The final 81 columnss represent the constraint
                // that each box has this number.
                   Encode(val, (row - (row % 3)) + (col / 3))
            ))).ToList();
        }

        private void DecodeCell(List<int> constraint, out int row, out int col, out int val)
        {
            // grab the row and column out of the first 81 columns of the constraint
            Decode(constraint.GetRange(0, 81).ToArray(), out row, out col);
            // grab the value out of the next 81
            Decode(constraint.GetRange(81, 81).ToArray(), out val, out row);
        }

        private List<List<int>> InitialBoard()
        {
            List<List<int>> result = new List<List<int>>();
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    for (int val = 0; val < 9; val++)
                    {
                        result.Add(EncodeCell(val, row, col));
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// Solves the grid in-place.
        /// </summary>
        /// <param name="grid">
        /// The grid to solve. This grid will be modified to contain the solution to
        /// the sudoku puzzle, if it exists.
        /// </param>
        /// <returns>
        /// If the puzzle can be unambiguously solved, Solve() will return true.
        /// If the puzzle cannot be solved or is invalid, this will return false.
        /// </returns>
        public bool Solve(Grid grid)
        {
            List<int[]> gridConstraints = InitialBoard();
            DancingLinks();
            return false;
        }

        private List<List<int>> solutions = new List<int>;
        public int[] DancingLinks(Dictionary<int, List<int>> grid)//, List<int> initialSolutions)
        {
            if (grid.Count == 0)
            {
                // empty
                return new int[0];
            }
            else
            {
                // colValues maps columns to [column, numberOfColumns]
                // ordered by the number of 1s in each column
                List<int> colValues = grid.Keys.Select((col,idx) => {
                    return new int[2] { col, new List<int>(colValues[col]).Sum() };
                }).OrderBy((cell) => cell[1]);

                if (colValues[0][1] == 0)
                {
                    // cannot execute this
                    return new int[0];
                }
                foreach (int[] colChoice in colValues)
                {
                    int col = colChoice[0];
                    foreach (int rownum in grid[col].Select((v,r)=>r).Where((r)=>grid[col][r]==1);
                    {
                        // Include this row in the partial solution
                        List<int> chosenColumns = grid.Keys.Where((col) => grid[col][rownum]==1);
                        solutions.Add(chosenColumns);
                        // For each column in chosenColumns:
                        // For each row in the chosenColumn:
                        // delete the row
                        // then delete the column

                    }
                }
            }

            return new int[5];
        }

        private int[] colmap(List<int[]> grid)
        {
            int[] columns = new int[9];
            for (int col = 0; col < 9; col++)
            {
                columns[col] = grid.Select((row) => row[col]).Sum();
            }
            return columns;
        }

        /// <summary>
        /// Get the list of valid moves for the given cell in the given grid.
        /// Used by the Hints bar.
        /// </summary>
        /// <returns>
        /// A list of valid move choices.
        /// </returns>
        public List<int> GetHintsFor(Grid grid, int row, int col)
        {
            var occupiedSquares = grid.GetColumn(col).Concat(grid.GetRow(row)).Concat(grid.GetSquareAbout(row, col));
            List<int> results = new List<int>();
            for (int i = 1; i <= 9; i++)
            {
                if (!occupiedSquares.Contains(i))
                {
                    results.Add(i);
                }
            }
            return results;
        }


        /// <summary>
        /// Finds errors in the grid. Returns a list of cells where:
        ///     - there is another cell with the same value in the row
        ///     - there is another cell with the same value in the column
        ///     - there is another cell with the same value in the 3x3 square
        ///       where the cell resides
        /// </summary>
        public List<List<int>> FindErrors(Grid grid)
        {
            List<List<int>> errors = new List<List<int>>();
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    int val = grid.Get(row, col);
                    // The same cell in the row?
                    bool valueInRow = grid.GetRow(row).Count((eachCell) => eachCell == val) > 1;
                    // The same cell in the column?
                    bool valueInCol = grid.GetColumn(col).Count((eachCell) => eachCell == val) > 1;
                    // The same cell in the 3x3 square?
                    bool valueInSquare = grid.GetSquareAbout(row, col).Count((eachCell) => eachCell == val) > 1;
                    if (val != 0 && (valueInRow || valueInCol || valueInSquare))
                    {
                        errors.Add(new List<int>(new int[] { row, col }));
                    }
                }
            }
            return errors;
        }
    }
}
