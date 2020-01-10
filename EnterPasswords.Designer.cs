namespace CryptoFinder
{
    partial class EnterPasswords
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterPasswords));
            this.label1 = new System.Windows.Forms.Label();
            this.EnterPasswordsText = new System.Windows.Forms.TextBox();
            this.SaveAndCloseButton = new System.Windows.Forms.Button();
            this.closeNoSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter the passwords to try";
            // 
            // EnterPasswordsText
            // 
            this.EnterPasswordsText.Location = new System.Drawing.Point(11, 34);
            this.EnterPasswordsText.Multiline = true;
            this.EnterPasswordsText.Name = "EnterPasswordsText";
            this.EnterPasswordsText.Size = new System.Drawing.Size(403, 288);
            this.EnterPasswordsText.TabIndex = 1;
            // 
            // SaveAndCloseButton
            // 
            this.SaveAndCloseButton.Location = new System.Drawing.Point(11, 327);
            this.SaveAndCloseButton.Name = "SaveAndCloseButton";
            this.SaveAndCloseButton.Size = new System.Drawing.Size(178, 40);
            this.SaveAndCloseButton.TabIndex = 2;
            this.SaveAndCloseButton.Text = "Accept";
            this.SaveAndCloseButton.UseVisualStyleBackColor = true;
            this.SaveAndCloseButton.Click += new System.EventHandler(this.SaveAndCloseButton_Click);
            // 
            // closeNoSave
            // 
            this.closeNoSave.Location = new System.Drawing.Point(236, 327);
            this.closeNoSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.closeNoSave.Name = "closeNoSave";
            this.closeNoSave.Size = new System.Drawing.Size(178, 40);
            this.closeNoSave.TabIndex = 3;
            this.closeNoSave.Text = "Cancel";
            this.closeNoSave.UseVisualStyleBackColor = true;
            this.closeNoSave.Click += new System.EventHandler(this.closeNoSave_Click);
            // 
            // EnterPasswords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 369);
            this.Controls.Add(this.closeNoSave);
            this.Controls.Add(this.SaveAndCloseButton);
            this.Controls.Add(this.EnterPasswordsText);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EnterPasswords";
            this.Text = "Enter passwords";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox EnterPasswordsText;
        private System.Windows.Forms.Button SaveAndCloseButton;
        private System.Windows.Forms.Button closeNoSave;
    }
}