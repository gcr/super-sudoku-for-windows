using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperSudoku
{
    enum DifficultyLevel {Easy, Medium, Hard};

    class Generator
    {
        Grid generatedGrid;

        /// <summary>
        /// Creates a new generator
        /// </summary>
        public Generator()
        {
            generatedGrid = new Grid();
        }

        /// <summary>
        /// Generates a grid with the given difficulty level
        /// </summary>
        public void Generate(DifficultyLevel difficulty)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The grid we generated
        /// </summary>
        public Grid SolutionGrid {
            get { return generatedGrid; }
        }


    }
}
