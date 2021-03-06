﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSudoku
{
    public enum DifficultyLevel {Easy, Medium, Hard};

    /// <summary>
    /// This class generates new puzzles.
    /// </summary>
    class Generator
    {
        private Random rand = new Random();
        private Solver solver = new Solver();

        /// <summary>
        /// Helper function: count the blank squares in the given grid.
        /// </summary>
        /// <returns>the number of blank squares</returns>
        private int CountBlank(Grid grid)
        {
            int nBlanks = 0;
            grid.ForEachSquare((row, col, val) =>
            {
                if (val == 0)
                {
                    nBlanks+=1;
                }
            });
            return nBlanks;
        }

        /// <summary>
        /// Blank two squares in a symmetric fashion, or not.
        /// 
        /// We always use rotational symmetry because it's prettiest.
        /// </summary>
        private void MaybeRandomBlank(Grid grid)
        {
            int row1 = rand.Next(9);
            int col1 = rand.Next(9);

            int row2 = (8 - row1);
            int col2 = (8 - col1);

            if (grid.Get(row1, col1) != 0 && grid.Get(row2, col2) != 0)
            {
                grid.Set(0, true, row1, col1);
                grid.Set(0, true, row2, col2);
            }
        }

        /// <summary>
        /// Generates a grid with the given difficulty level.
        /// </summary>
        public Grid Generate(DifficultyLevel difficulty)
        {
            // Generates stuff
            // 1. Randomnly fill in the top and left part of the grid.
            // 2. Try to solve it
            // 3. Until we have enough blanks:
            //      remove some blanks
            //      if it isn't uniquely solvable, add those two blanks again.
            Grid grid = GenerateBlankGrid();

            Grid result;

            // Top row
            List<int> row = Enumerable.Range(1, 9).OrderBy((i) => rand.Next()).ToList();
            for (int i = 0; i < 9; i++)
            {
                grid.Set(row[i], false, i, 0);
            }

            // Top column
            row = Enumerable.Range(1, 9).OrderBy((i) => rand.Next()).ToList();
            row.Remove(grid.Get(0, 0));
            for (int i = 0; i < 8; i++)
            {
                grid.Set(row[i], false, 0, i+1);
            }

            if (solver.FindErrors(grid).Count > 0)
            {
                // This is not enough to guarantee correctness.
                // If we get an invalid grid, try try again!
                result = Generate(difficulty);
            }
            else
            {
                solver.Solve(grid);

                // How many blanks do we need?
                int targetBlanks = 0;
                switch (difficulty)
                {
                    case DifficultyLevel.Easy:
                        targetBlanks = 30;
                        break;
                    case DifficultyLevel.Medium:
                        targetBlanks = 45;
                        break;
                    case DifficultyLevel.Hard:
                        targetBlanks = 50;
                        break;
                }

                // Remove squares until we have the right number of blanks.
                int tries = 0;
                while (tries < 100 && CountBlank(grid) < targetBlanks)
                {
                    Grid saveCopy = grid.Copy();
                    // Solving is expensive. Blanking squares is easy!
                    // When the grid is mostly full, you can blank squares
                    // in relative safety without generating a non-unique
                    // puzzle. When the puzzle gets more sparse, you
                    // need to be more careful.
                    // That's what we're doing here. Quick optimization.
                    for (int i = 0; i < (targetBlanks - CountBlank(grid))/2 + 1; i++)
                    {
                        MaybeRandomBlank(grid);
                    }
                    if (!solver.Solve(grid.Copy()))
                    {
                        // it failed
                        grid = saveCopy;
                    }
                    tries++;
                }
                //Console.WriteLine("Generated puzzle in " + tries + " tries with "+CountBlank(grid)+" blanks");

                // Finally, set every square to be not editable
                grid.ForEachSquare((r, c, val) =>
                {
                    if (val != 0)
                    {
                        grid.SetEditable(false, r, c);
                    }
                });

                result = grid;
            }
            return result;
        }

        /// <summary>
        /// In the event that a blank grid is needed, call this method!
        /// (it's only here to satisfy the SDS requirements)
        /// </summary>
        public Grid GenerateBlankGrid()
        {
            return new Grid();
        }

    }
}
