﻿using System;
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
    public class StopIterationException : Exception { }

    public class Solver
    {
        private Random rand = new Random();

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
            int numSolutions = 0;
            if (FindErrors(grid).Count > 0)
            {
                // fail fast!
                return false;
            }

            try {
                DepthFirstSearch(grid, () => {
                    numSolutions++;
                    if (numSolutions > 1) {
                        throw new StopIterationException();
                    }
                });
            }
            catch (StopIterationException e) { }

            return numSolutions == 1;
        }

        public void DepthFirstSearch(Grid grid, Action eachSolutionAction)
        {
            CellConsideration cell = Consider(grid);
            if (grid.IsFull())// || !valid)
            {
                eachSolutionAction();
            }
            else if (cell != null)
            {
                foreach (int hint in cell.PossibleValues)
                {
                    grid.Set(hint, true, cell.Row, cell.Col);
                    DepthFirstSearch(grid, eachSolutionAction);
                    grid.Set(0, true, cell.Row, cell.Col);
                }
            }
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
            List<int> stuff = grid.GetColumn(col).Concat(grid.GetRow(row)).Concat(grid.GetSquareAbout(row, col)).ToList();
            //int[] gCols = grid.GetColumn(col);
            //int[] gRows = grid.GetRow(row);
            //int[] gSq = grid.GetSquareAbout(row, col);
            List<int> results = new List<int>();
            for (int i = 1; i <= 9; i++)
            {
                if (!stuff.Contains(i))//(gCols.Contains(i) || gRows.Contains(i) || gSq.Contains(i)))
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
        private CellConsideration Consider(Grid grid)
        {
            int smallestNOfHints = int.MaxValue;
            CellConsideration smallestConsideration = null;
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (grid.Get(row, col) == 0)
                    {
                        List<int> hints = GetHintsFor(grid, row, col);
                        /*if (hints.Count == 1) {
                            grid.Set(hints[0], true, row, col);
                        } else*/
                        if (hints.Count == 0)
                        {
                            // We found a square that has no solution
                            // This means the current state of the puzzle is bogus
                            // so stop.
                            return null;
                        }
                        if (hints.Count < smallestNOfHints)
                        {
                            smallestConsideration = new CellConsideration(row, col, hints);
                            smallestNOfHints = hints.Count;
                        }
                    }
                }
            }
            return smallestConsideration;
        }

        /*/// <summary>
        /// Fills in cells that only have one possible value
        /// </summary>
        /// <returns>whether I touched the grid in any way</returns>
        private void FillSingletons(Grid grid)
        {
            CellConsideration considerations = Consider(grid);
            if (considerations.Count > 0 && considerations[0].PossibleValues.Count == 1)
            {
                grid.Set(considerations[0].PossibleValues[0], true, considerations[0].Row, considerations[0].Col);
                FillSingletons(grid);
            }
        }*/


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
