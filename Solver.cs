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
     * and http://www.ocf.berkeley.edu/~jchu/publicportal/sudoku/sudoku.paper.html (which
     * contains errors)
     */

    public class Solver
    {
        private List<List<int>> InitialCachedConstraints;

        List<Node> currentConsideredConstraints = new List<Node>();

        public Solver()
        {
            InitialCachedConstraints = InitialBoardConstraints();
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

        /// <summary>
        /// Encodes a section of a constraint.
        /// </summary>
        /// <param name="major">We can encode two numbers in a constraint. This is the "major" number.</param>
        /// <param name="minor">This is the "minor" number.</param>
        /// <returns>An 81-element array that represents the constraint.</returns>
        private List<int> EncodeConstraintSection(int major, int minor)
        {
            List<int> result = Enumerable.Repeat(0, 81).ToList();
            result[major * 9 + minor] = 1;
            return result;
        }

        /// <summary>
        /// Given a constraint section, decode the major and minor numbers (IN-PLACE!)
        /// </summary>
        /// <param name="row">The constraint</param>
        /// <param name="major">OUTPUT: The "major" number</param>
        /// <param name="minor">OUTPUT: The "minor" number</param>
        private void DecodeConstraintSection(List<int> row, out int major, out int minor)
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
        
        /// <summary>
        /// Convert the state of a cell into a 324-element array that represents
        /// the constraints for the cell.
        /// </summary>
        /// <param name="val">The value of the grid cell</param>
        /// <param name="row">The cell's row</param>
        /// <param name="col">The cell's column</param>
        /// <returns>The constraint you get when you set the cell at row,col to val</returns>
        private List<int> EncodeCellConstraint(int val, int row, int col)
        {
            // The first 81 columns represent the constraint
            // that the certain cell is filled.
            return EncodeConstraintSection(row, col).Concat(
                // The next 81 columns represent the constraint
                // that each row has this number.
                EncodeConstraintSection(val-1, row).Concat(
                // The next 81 columns represent the constraint
                // that each column has this number.
                EncodeConstraintSection(val-1, col).Concat(
                // The final 81 columnss represent the constraint
                // that each box has this number.
                EncodeConstraintSection(val-1, (row - (row % 3)) + (col / 3))
            ))).ToList();
        }

        /// <summary>
        /// When you have a constraint on the puzzle, use this method
        /// to get the row and column of the cell along with its value.
        /// </summary>
        private void DecodeCellConstraint(List<int> constraint, out int val, out int row, out int col)
        {
            // grab the row and column out of the first 81 columns of the constraint
            DecodeConstraintSection(constraint.GetRange(0, 81).ToList(), out row, out col);
            // grab the value out of the next 81
            DecodeConstraintSection(constraint.GetRange(81, 81).ToList(), out val, out row);
            val = val + 1;
            if (val == 0)
            {
                throw new Exception("WTF");
            }
        }

        /// <summary>
        /// This gets the constraints for a completely blank sudoku board.
        /// </summary>
        private List<List<int>> InitialBoardConstraints()
        {
            List<List<int>> result = new List<List<int>>();
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    for (int val = 1; val < 10; val++)
                    {
                        result.Add(EncodeCellConstraint(val, row, col));
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
            // fail fast
            if (FindErrors(grid).Count > 0)
            {
                return false;
            }

            /*
             * Several steps here:
             * 1. Copy the initial blank board constraints
             * 2. Convert this into a linked patchwork of Nodes
             * 3. We're given a partially solved grid. "Cover" all the constraints
             *    that the grid already satisfies
             *    
             * 4. Run dancing links
             * 
             * 5. Extract the rest of the cells from the maze of currentConsideredNodes
             */
            List<List<int>> gridConstraints = InitialCachedConstraints.Select((row) => row.Select((c) => c).ToList()).ToList();
            
            // reset
            currentConsideredConstraints = new List<Node>();
            int nSolutions = 0;
            // Convert initial grid constraints into links
            ColumnNode header = gridConstraintsToPatchwork(gridConstraints);
            grid.ForEachSquare((row, col, val) =>
            {
                // Remove every constraint that's already
                // satisfied in the (current) grid
                if (val != 0)
                {
                    // which columns to Cover? all of the columns in the grid cell's constraint!
                    List<int> removeColumns = EncodeCellConstraint(val, row, col);
                    removeColumns = removeColumns.Select((r, i) => i).Where((i) => removeColumns[i] == 1).ToList();

                    // Now remove all constrained columns
                    for (ColumnNode seekCol = (ColumnNode)header.Right; seekCol != header; seekCol = (ColumnNode)seekCol.Right)
                    {
                        if (removeColumns.Contains(seekCol.Col))
                        {
                            CoverColumn(seekCol);
                        }
                    }
                }
            });

            try
            {
                // Now run dancing links
                DancingLinks(header, () => {
                    // when we find something...
                    nSolutions++;
                    if (nSolutions > 1)
                    {
                        // Stop if we found more than one solution.
                        throw new StopIterationException();
                    }
                    // oh how i pine for python's generators...
                    // Decode the solution.
                    foreach (Node constraintLink in currentConsideredConstraints)
                    {
                        // Each "constraint link" here represents one grid cell.
                        // Convert it back into an "ordinary" constraint so we can work with it...
                        List<int> constraint = Enumerable.Repeat(0, 324).ToList();
                        constraint[((ColumnNode)constraintLink.GetColumn()).Col] = 1;
                        for (Node right = constraintLink.Right; right != constraintLink; right = right.Right)
                        {
                            constraint[((ColumnNode)right.GetColumn()).Col] = 1;
                        }

                        // now that we have an ordinary constraint, extract the row, col, and value and
                        // set the cell.
                        int r, c, v;
                        DecodeCellConstraint(constraint, out v, out r, out c);
                        grid.Set(v, true, r, c);
                    }
                });
            } catch (StopIterationException e) {
                // we found more than one solution; terminate early.
            }

            return nSolutions == 1;
        }

        /// <summary>
        /// If there are any 0s in any of the currently considered columns,
        /// that means that no subset of rows can satisfy all of the constraints.
        /// </summary>
        /// <param name="header">the set of headers to test</param>
        /// <returns>Returns true if there is no solution to the columns</returns>
        public bool HasZero(ColumnNode header)
        {
            for (Node right = header.Right; right != header; right = right.Right)
            {
                if (right.Up == right)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Knuth's actual "Dancing Links" algorithm.
        /// See http://www-cs-faculty.stanford.edu/~uno/papers/dancing.ps.gz
        /// </summary>
        public void DancingLinks(ColumnNode header, Action eachSolution)
        {
            if (header.Right == header)
            {
                // No columns left. This is a valid solution.
                eachSolution();
            }
            else
            {
                if (!HasZero(header))
                {
                    // pick any column
                    // (the optimization of picking the column with the smallest 1s is too slow here)
                    ColumnNode col = (ColumnNode)header.Right;
                    // remove column
                    CoverColumn(col);
                    // For every row in the selected column...
                    for (Node row = col.Down; row != col; row = row.Down)
                    {
                        // add to "partial" solution maybe
                        currentConsideredConstraints.Add(row);
                        for (Node right = row.Right; right != row; right = right.Right)
                        {
                            // cover every selected column in this row
                            CoverColumn(right);
                        }
                        // recurse
                        DancingLinks(header, eachSolution);
                        // there was no solution, so remove this row from our "partial" solutions
                        // and uncover all the columns in this row
                        currentConsideredConstraints.Remove(row);
                        for (Node left = row.Left; left != row; left = left.Left)
                        {
                            uncover(left);
                        }
                    }
                    // no solution, so undo all that.
                    uncover(col);
                }
            }
        }

        /// <summary>
        /// Removes the given column from the patchwork (but does not
        /// erase this information; it can be restored later)
        /// </summary>
        private void CoverColumn(Node node)
        {
            Node col = node.GetColumn();
            col.Right.Left = col.Left;
            col.Left.Right = col.Right;
            for (Node row = col.Down; row != col; row = row.Down)
            {
                for (Node right = row.Right; right != row; right = right.Right)
                {
                    right.Up.Down = right.Down;
                    right.Down.Up = right.Up;
                }
            }
        }

        /// <summary>
        /// Re-adds the given column into the patchwork using the
        /// information stored in the column
        /// </summary>
        private void uncover(Node node)
        {
            Node col = node.GetColumn();
            for (Node row = col.Up; row != col; row = row.Up)
            {
                for (Node left = row.Left; left != row; left = left.Left) // TODO ??
                {
                    left.Up.Down = left;
                    left.Down.Up = left;
                }
            }
            col.Right.Left = col;
            col.Left.Right = col;
        }

        /// <summary>
        /// Converts an ordinary list of grid constraints into a patchwork of links
        /// suitable for dancing links.
        /// </summary>
        private ColumnNode gridConstraintsToPatchwork(List<List<int>> grid)
        {
            ColumnNode header = new ColumnNode(-1, null, null);
            header.Left = header;
            header.Right = header;
            //List<ColumnNode> headers = Enumerable.Range(0, 9).Select((i) => new ColumnNode(i, null, null, null, null)).ToList();
            List<Node> columns = new List<Node>();
            for (int i = 0; i < grid[0].Count; i++)
            {
                ColumnNode newNode = new ColumnNode(i, header.Left, header);
                newNode.Left.Right = newNode;
                newNode.Right.Left = newNode;
                columns.Add(newNode);
            }

            // Now that we have the header all linked up, go ahead and
            // link up each row
            foreach (List<int> row in grid)
            {
                List<Node> rowNodes = new List<Node>();
                for (int col = 0; col < row.Count; col++)
                {
                    if (row[col] == 1)
                    {
                        Node newNode = new Node(null, null, columns[col], columns[col].Up);
                        columns[col].Up.Down = newNode;
                        columns[col].Up = newNode;
                        rowNodes.Add(newNode);
                    }
                }
                for (int i = 0; i < rowNodes.Count; i++)
                {
                    // then link them up left/right
                    rowNodes[i].Right = rowNodes[(i + 1) % rowNodes.Count];
                    rowNodes[i].Left = rowNodes[(i - 1 + rowNodes.Count) % rowNodes.Count];
                }
            }


            return header;
        }
    }

    // each node in the patchwork
    public class Node
    {
        public Node Left = null;
        public Node Right = null;
        public Node Down = null;
        public Node Up = null;
        public Node(Node l, Node r, Node d, Node u)
        {
            Left = l;
            Right = r;
            Down = d;
            Up = u;
        }
        public Node()
        {
        }
        /// <summary>
        /// Finds the column this node is a part of
        /// </summary>
        public Node GetColumn()
        {
            Node findColumn = this;
            while (! (findColumn is ColumnNode)) {
                findColumn = findColumn.Up;
            }
            return findColumn;
        }
    }

    // each "column" header in the patchwork
    public class ColumnNode : Node
    {
        public int Col;
        public ColumnNode(int c, Node l, Node r)
        {
            Col = c;
            Left = l;
            Right = r;
            Down = this;
            Up = this;
        }
    }

    public class StopIterationException : Exception { }
}
