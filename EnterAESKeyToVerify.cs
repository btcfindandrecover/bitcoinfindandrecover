using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace CryptoFinder
{
    public partial class EnterAESKeyToVerify : Form
    {
        public EnterAESKeyToVerify()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string passwordVerificationString = Crypto.Decrypt<AesManaged>(Program.mainForm.PasswordVerificationString, textBox1.Text, Program.mainForm.Salt);

            if (passwordVerificationString == "Password OK")
            {
                Program.mainForm.AESKey = textBox1.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("The password you have entered is not correct, please try again");
            }
        }
    }
}