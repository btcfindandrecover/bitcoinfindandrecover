using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CryptoFinder
{
    public partial class EnterToken : Form
    {
        public EnterToken()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (OptionsOK() == false)
            {
                return;
            }
            else
            {
                dgvToTokenList();
                if (Token1Text.Text != "")
                {
                    CreateToken();
                }
                if (TokenList != "")
                {
                    if (Helpers.TokensModified(TokenList) == false)
                    {
                        bool AESKeySet = Program.mainForm.AESKeyEntry();
                        if (AESKeySet == true)
                        {
                            Program.mainForm.Tokens = TokenList;
                            Program.mainForm.PasswordsOrTokens = "Tokens";
                            Helpers.CheckForEscapeCharacters(TokenList);
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You have not entered any password parts");
                }
            }
        }
        public string TokenList { get; set; }
        public string CharactersStart1 { get; set; }
        public string CharactersEnd1 { get; set; }

        private void UpperCaseButton_Click(object sender, EventArgs e)
        {
            string addition = @"%A";
            Token1Text.Text = Token1Text.Text.Substring(0, Token1Text.SelectionStart) + addition + Token1Text.Text.Substring(Token1Text.SelectionStart);
        }

        private void LowerCaseButton_Click(object sender, EventArgs e)
        {
            string addition = @"%a";
            Token1Text.Text = Token1Text.Text.Substring(0, Token1Text.SelectionStart) + addition + Token1Text.Text.Substring(Token1Text.SelectionStart);
        }

        private void NumbersButton_Click(object sender, EventArgs e)
        {
            string addition = @"%d";
            Token1Text.Text = Token1Text.Text.Substring(0, Token1Text.SelectionStart) + addition + Token1Text.Text.Substring(Token1Text.SelectionStart);
        }

        private void SpaceButton_Click(object sender, EventArgs e)
        {
            string addition = @"%s";
            Token1Text.Text = Token1Text.Text.Substring(0, Token1Text.SelectionStart) + addition + Token1Text.Text.Substring(Token1Text.SelectionStart);
        }
        private void CreateToken()
        {
            if (Token1Text.Text != null)
            {
                string Token;
                Token = Token1Text.Text;
                int selectionstart = Token1Text.SelectionStart;
                if (LocatedStart1CB.Checked == true)
                {
                    string[] words = Token.Split(' ');
                    Token = "";
                    foreach (string word in words)
                        if (word != "")
                        {
                            Token = Token + @"^" + word + " ";
                        }
                }
                if (LocatedEnd1CB.Checked == true)
                {
                    string[] words = Token.Split(' ');
                    Token = "";
                    foreach (string word in words)
                        if (word != "")
                        {
                            Token = Token + word + @"$ ";
                        }
                }
                if (Position1Text.Text != "")
                {
                    string[] words = Token.Split(' ');
                    Token = "";
                    foreach (string word in words)
                        if (word != "")
                        {
                            Token = Token + "^" + Position1Text.Text + "^" + word + " ";
                        }
                }
                if (MustBeIncluded1CB.Checked == true)
                {
                    Token = @"+ " + Token;
                }
                if (TokenList != null)
                {
                    TokenList = TokenList + "\n" + Token;
                }
                else
                {
                    TokenList = TokenList + Token;
                }
            }
        }
        private bool OptionsOK()
        {
            bool optionsOK = true;
            if ((LocatedStart1CB.Checked == true || LocatedEnd1CB.Checked == true) && Position1Text.Text != "")
            {
                MessageBox.Show("You cannot define a part to be at the start or the end and also be at a range");
                optionsOK = false;
            }
            if (LocatedStart1CB.Checked == true && LocatedEnd1CB.Checked == true)
            {
                MessageBox.Show("You cannot define a part to be both at the start and the end");
                optionsOK = false;
            }
            return optionsOK;

        }
        private void closeNoSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void EnterToken_Shown(object sender, EventArgs e)
        {
            PrepareTable(true);
        }
        private void SaveLineButton_Click(object sender, EventArgs e)
        {
            if (OptionsOK() == false)
            {
                return;
            }
            else
            {
                if (Token1Text.Text == "")
                {
                    MessageBox.Show("You have not entered any text");
                    return;
                }
                else
                {
                    CreateToken();
                    //if (TokenList == null)
                    //{
                    //    TokenList = Token1Text.Text;
                    //}
                    //else
                    //{
                    //    TokenList = TokenList + "\n" + Token1Text.Text;
                    //}
                    Token1Text.Text = "";
                    MustBeIncluded1CB.Checked = false;
                    LocatedStart1CB.Checked = false;
                    LocatedEnd1CB.Checked = false;
                    Position1Text.Text = "";
                    PrepareTable(false);
                }
            }
        }
        private void PrepareTable(bool FirstTime)
        {
            if (Program.mainForm.Tokens != "" || TokenList != null)
            {
                if (FirstTime == true)
                {
                    TokenList = Program.mainForm.Tokens;
                }
                else
                {
                    TokenList = TokenList;
                }
                string[,] values = Helpers.LoadCsv(TokenList, false);
                int num_rows = values.GetUpperBound(0) + 1;
                int num_cols = values.GetUpperBound(1) + 1;

                // Display the data to show we have it.

                // Make column headers.
                // For this example, we assume the first row
                // contains the column names.
                dgvValues.Columns.Clear();
                //for (int c = 0; c < num_cols; c++)
                dgvValues.Columns.Add("Tokens", "Tokens");

                // Add the data.
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

        private void dgvValues_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvToTokenList();
        }
        private void dgvToTokenList()
        {
            TokenList = "";
            foreach (DataGridViewRow datarow in dgvValues.Rows)
            {
                if (datarow.Cells["Tokens"].Value != null)
                {
                    TokenList = TokenList + datarow.Cells["Tokens"].Value.ToString() + "\n";
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.mainForm.ApplicationDirectory + "Tokens.txt");
        }
    }
}
