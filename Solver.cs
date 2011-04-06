using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSudoku
{
    class Solver
    {

        /// <summary>
        /// Solves the grid in-place.
        /// </summary>
        /// <param name="grid">
        /// The grid to solve. This grid will be modified to contain the solution to
        /// the sudoku puzzle, if it exists.
        /// </param>
        /// <returns>
        /// If the puzzle can be unambiguously solved, Solve() will return true.
        /// If the puzzle cannot be solved or is 
        /// </returns>
        public bool Solve(Grid grid)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Get the list of valid moves for the given cell in the given grid.
        /// Used by the Hints bar.
        /// </summary>
        /// <returns>
        /// A list of valid move choices.
        /// </returns>
        public int[] GetHintsFor(Grid grid, int row, int col)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Returns true if the puzzle isn't obviously broken
        /// </summary>
        public bool IsValidPuzzle(Grid grid)
        {
            // if (only one copy of each number in each row, column, square) {
            //    return true;
            // }
            throw new NotImplementedException();
        }

    }
}
