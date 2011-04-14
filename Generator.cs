using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSudoku
{
    public enum DifficultyLevel {Easy, Medium, Hard};

    class Generator
    {

        /// <summary>
        /// Generates a grid with the given difficulty level
        /// </summary>
        public Grid Generate(DifficultyLevel difficulty)
        {
            // Generates stuff!!!!!!111
            // 1. Randomnly fill in the top and left part of the grid.
            // 2. Try to solve it
            // Until we have enough blanks:
            //    remove ~two blanks symmetrically
            //    if it isn't uniquely solvable, add those two blanks again.
            Grid grid = GenerateBlankGrid();
            Random rand = new Random();

            // Top row
            List<int> row = new List<int>();
            while (row.Count < 9)
            {
                int num = rand.Next(1, 10);
                if (!row.Contains(num))
                {
                    row.Add(num);
                }
            }
            for (int i = 0; i < 9; i++)
            {
                grid.Set(row[i], false, i, 0);
            }

            // Top column
            row.RemoveRange(1, 8);
            while (row.Count < 9)
            {
                int num = rand.Next(1, 10);
                if (!row.Contains(num))
                {
                    row.Add(num);
                }
            }
            for (int i = 0; i < 9; i++)
            {
                grid.Set(row[i], false, 0, i);
            }


            return grid;
        }

        /// <summary>
        /// In the event that a blank grid is needed, this method can be called.
        /// </summary>
        public Grid GenerateBlankGrid()
        {
            return new Grid();
        }

    }
}
