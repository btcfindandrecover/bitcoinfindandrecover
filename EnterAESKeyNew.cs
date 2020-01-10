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
    public partial class EnterAESKeyNew : Form
    {
        public EnterAESKeyNew()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == textBox2.Text)
            {
                Program.mainForm.AESKey = textBox1.Text;
                Program.mainForm.PasswordVerificationString = "Password OK";
                this.Close();

            }
            else
            {
                MessageBox.Show("The password does not match with the verification. Please enter them again");
            }
        }
    }
}
