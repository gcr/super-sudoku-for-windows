using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SuperSudoku
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Grid x = new Grid((int[][]) (new string[9] {
            "1 2 3 4 5 6 7 8 9",
            "0 5 2 3 4 0 1 2 1",
            "0 0 0 0 3 0 5 9 1",

            "1 2 3 4 5 6 7 8 9",
            "1 2 3 4 5 6 7 8 9",
            "1 2 3 4 5 6 7 8 9",

            "1 2 3 4 5 6 7 8 9",
            "1 2 3 4 5 6 7 8 9",
            "1 2 3 4 5 6 7 8 9"
            
            }).Select((string row) =>
                row.Split(' ').Select((string cell) =>
                                      Int32.Parse(cell))
                              .ToArray())
              .ToArray());

            int[] col = x.GetColumn(3);
            int[] r = x.GetRow(2);
            int[] sq = x.GetSquareAbout(2, 4);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WelcomeForm());
        }
    }
}
