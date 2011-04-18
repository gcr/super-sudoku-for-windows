using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace SuperSudoku
{
    class File
    {
        /// <summary>
        /// Takes as input a 9x9 integer grid array and a file name,
        /// and returns true if successful in reading the named file.
        /// </summary>
        public bool ReadFile(Grid grid, string fileName)
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    String line;
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    for (int i=0;i<9;i++)
                    {
                        line= sr.ReadLine();
                        for (int j = 1; j < 10; j++)
                        {
                            if (line[2 * (j - 1) + 1] == '-')
                            {
                                grid.Set((0-(line[2 * (j - 1)] - 48)), false, i, j-1);
                            }
                            else {
                                grid.Set(line[2 * (j - 1)] - 48, true, i, j-1);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                MessageBox.Show("The file could not be read: '"+e.Message+"'");
                return false;
            }
        }

        /// <summary>
        /// Takes as input a 9x9 integer grid array and a file name,
        /// and returns true if successful in writing the named file.
        /// </summary>
        public bool WriteFile(Grid grid, string fileName)
        {
            try
            {
                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
                for (int i = 1; i < 10; i++)
                {
                    string line = "";
                    for (int j = 1; j < 10; j++)
                    {
                        line += Math.Abs(grid.Get(i-1, j-1));
                        if (grid.IsEditable(i - 1, j - 1))
                        {
                            line += '+';
                        }
                        else{
                            line += '-';
                        }
                    }
                    file.WriteLine(line);
                }
                file.Close();
                return true;
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                MessageBox.Show("The file could not be written: '"+e.Message+"'");
                return false;
            }
        }

    }
}
