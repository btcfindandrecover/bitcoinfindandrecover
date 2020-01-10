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
    public partial class EnterPasswords : Form
    {
        public EnterPasswords()
        {
            InitializeComponent();
        }

        private void SaveAndCloseButton_Click(object sender, EventArgs e)
        {
            bool result = false;
            if (EnterPasswordsText.Text == "")
            {
                MessageBox.Show("You have not entered any passwords");
                result = true;
            }
            if (result == false)
            {
                bool AESKeySet = Program.mainForm.AESKeyEntry();
                if (AESKeySet == true)
                {
                    Program.mainForm.Passwords = EnterPasswordsText.Text;
                    Program.mainForm.PasswordsOrTokens = "Passwords";
                    this.Close();
                }
            }
        }
        private void closeNoSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
