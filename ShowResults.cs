using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CryptoFinder
{
    public partial class ShowResults : Form
    {
        public ShowResults()
        {
            InitializeComponent();
        }

        // Load a CSV file into an array of rows and columns.
        // Assume there may be blank lines but every line has
        // the same number of fields.

        private void ShowResults_Shown(object sender, EventArgs e)
        { 
            string[,] values = Helpers.LoadCsv(Program.mainForm.Results, true);
            int num_rows = values.GetUpperBound(0) + 1;
            int num_cols = values.GetUpperBound(1) + 1;
            
            dgvValues.Columns.Clear();
            dgvValues.Columns.Add("File path", "File path");
            dgvValues.Columns.Add("Results", "Results");

            for (int r = 0; r < num_rows; r++)
            {
                dgvValues.Rows.Add();
                for (int c = 0; c < num_cols; c++)
                {
                    dgvValues.Rows[r].Cells[c].Value = values[r, c];
                }
            }
        }
    }
}
