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
        private List<List<int>> Initial;

        public Solver()
        {
            Initial = InitialBoard();
        }

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
                   Encode(val-1, row).Concat(
                // The next 81 columns represent the constraint
                // that each column has this number.
                   Encode(val-1, col).Concat(
                // The final 81 columnss represent the constraint
                // that each box has this number.
                   Encode(val-1, (row - (row % 3)) + (col / 3))
            ))).ToList();
        }

        private void DecodeCell(List<int> constraint, out int val, out int row, out int col)
        {
            // grab the row and column out of the first 81 columns of the constraint
            Decode(constraint.GetRange(0, 81).ToList(), out row, out col);
            // grab the value out of the next 81
            Decode(constraint.GetRange(81, 81).ToList(), out val, out row);
            val = val + 1;
            if (val == 0)
            {
                throw new Exception("WTF");
            }
        }

        private List<List<int>> InitialBoard()
        {
            List<List<int>> result = new List<List<int>>();
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    for (int val = 1; val < 10; val++)
                    {
                        result.Add(EncodeCell(val, row, col));
                    }
                }
            }
            return result;
        }


        List<Node> solutions = new List<Node>();

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

            List<List<int>> gridConstraints = Initial.Select((row) => row.Select((c) => c).ToList()).ToList();
            /*foreach (List<int> row in gridConstraints.Where((row) => row[81] == 1))
            {
                int r, v, c;
                DecodeCell(row, out v, out r, out c);
                Console.WriteLine("Constraint: val " + v + " at " + r + "," + c);
                Console.WriteLine("this column is " + (r * 9 + c));
            }*/
            solutions = new List<Node>();
            try
            {
                ColumnNode header = gridToColumnnodes(gridConstraints);
                grid.ForEachSquare((row, col, val) =>
                {
                    // first, we remove every constraint that's already
                    // satisfied in the (current) grid
                    if (val != 0) {
                        List<int> removeColumns = EncodeCell(val, row, col);
                        removeColumns = removeColumns.Select((r, i) => i).Where((i) => removeColumns[i] == 1).ToList();
                        
                        // now remove the rows w/ this constraint
                        for (ColumnNode seekCol = (ColumnNode)header.Right; seekCol != header; seekCol = (ColumnNode)seekCol.Right)
                        {
                            if (removeColumns.Contains(seekCol.Col))
                            {
                                //Console.WriteLine("Removing col " + seekCol.Col);
                                cover(seekCol);
                            }
                        }
                    }
                });

                // now that we've cleaned up the original, run DLX!
                DancingLinks(header);


            } catch (Exception e) {
                foreach (Node row in solutions) {
                    List<int> constraint = Enumerable.Repeat(0, 324).ToList();
                    constraint[((ColumnNode)row.GetColumn()).Col] = 1;
                    for (Node right = row.Right; right != row; right = right.Right)
                    {
                        constraint[((ColumnNode)right.GetColumn()).Col] = 1;
                    }

                    int r;
                    int c;
                    int v;
                    DecodeCell(constraint, out v, out r, out c);
                    grid.Set(v, true, r, c);
                }
                return true;
            }
            return false;
        }

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

        public void DancingLinks(ColumnNode header)
        {
            //Console.WriteLine("DLX with " + solutions.Count + " solutions");
            if (header.Right == header)
            {
                // No columns left
                throw new Exception("HUZZAH");
            }
            else
            {
                if (!HasZero(header))
                {
                    ColumnNode col = (ColumnNode)header.Right;
                    cover(col);
                    for (Node row = col.Down; row != col; row = row.Down)
                    {
                        solutions.Add(row);
                        for (Node right = row.Right; right != row; right = right.Right)
                        {
                            cover(right);
                        }
                        DancingLinks(header);
                        solutions.Remove(row);
                        for (Node left = row.Left; left != row; left = left.Left)
                        {
                            uncover(left);
                        }
                    }
                    uncover(col);
                }
            }
        }

        private void cover(Node node)
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

        private ColumnNode gridToColumnnodes(List<List<int>> grid)
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
            // go row-wise and add each node.
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
        public Node GetColumn()
        {
            Node findColumn = this;
            while (! (findColumn is ColumnNode)) {
                findColumn = findColumn.Up;
            }
            return findColumn;
        }
    }

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
}
