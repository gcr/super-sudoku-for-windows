using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSudoku
{
    /// <summary>
    /// This is simply a helper class that holds one cell to consider in the
    /// recursive search.
    /// </summary>
    public class CellConsideration
    {
        public int Row;
        public int Col;
        public List<int> PossibleValues;
        public CellConsideration(int row, int col, List<int> possibleValues)
        {
            this.Row = row;
            this.Col = col;
            this.PossibleValues = possibleValues;
        }
    }

    public class Solver
    {
        private Random rand = new Random();

        private int tries = 0;

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
            tries++;
            //FillSingletons(grid);
            bool valid = FindErrors(grid).Count == 0;
            if (grid.IsFull() || !valid)
            {
                if (!valid)
                {
                    throw new Exception("WTF");
                }
                return valid;
            }
            List<CellConsideration> considerations = Consider(grid);
            foreach (CellConsideration cell in considerations) {
                foreach (int hint in cell.PossibleValues.OrderBy((v) => rand.Next())) {
                    grid.Set(hint, true, cell.Row, cell.Col);
                    if (Solve(grid)) {
                        return true;
                    }
                    grid.Set(0, true, cell.Row, cell.Col);
                }
            }
            return false;
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
        /// Considers each cell of the grid.
        /// </summary>
        /// <returns>
        /// Returns is a list of CellConsiderations. Traverse this list in order.
        /// </returns>
        private List<CellConsideration> Consider(Grid grid)
        {
            List<CellConsideration> cells = new List<CellConsideration>();
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    List<int> hints = GetHintsFor(grid, row, col);
                    if (grid.Get(row, col) == 0 && hints.Count > 0)
                    {
                        cells.Add(new CellConsideration(row, col, hints));
                    }
                }
            }
            return cells.OrderBy((CellConsideration a) => a.PossibleValues.Count).ToList();
        }

        /// <summary>
        /// Fills in cells that only have one possible value
        /// </summary>
        /// <returns>whether I touched the grid in any way</returns>
        private void FillSingletons(Grid grid)
        {
            List<CellConsideration> considerations = Consider(grid);
            if (considerations.Count > 0 && considerations[0].PossibleValues.Count == 1)
            {
                grid.Set(considerations[0].PossibleValues[0], true, considerations[0].Row, considerations[0].Col);
                FillSingletons(grid);
            }
        }


        /// <summary>
        /// Returns true if the puzzle isn't obviously broken.
        /// </summary>
        public bool IsValidPuzzle(Grid grid)
        {
            return FindErrors(grid).Count == 0;
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
