﻿using System;
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
        public void ReadFile(Grid grid, string fileName)
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
                    while ((line = sr.ReadLine()) != null)
                    {
                        MessageBox.Show(line);
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                MessageBox.Show("The file could not be read.");
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Takes as input a 9x9 integer grid array and a file name,
        /// and returns true if successful in writing the named file.
        /// </summary>
        public void WriteFile(Grid grid, string fileName)
        {
            try
            {
                // Write the string to a file.
                System.IO.StreamWriter file = new System.IO.StreamWriter(fileName);
                file.WriteLine("0+1+2+3+4-5+6+7-8+");

                file.Close();
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                MessageBox.Show("The file could not be written.");
                MessageBox.Show(e.Message);
            }
        }

    }
}
