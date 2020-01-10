namespace CryptoFinder
{
    partial class EnterToken
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterToken));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.EnterTokenLB1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.LocatedEnd1CB = new System.Windows.Forms.CheckBox();
            this.Position1Text = new System.Windows.Forms.TextBox();
            this.LocatedStart1CB = new System.Windows.Forms.CheckBox();
            this.MustBeIncluded1CB = new System.Windows.Forms.CheckBox();
            this.Token1Text = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.UpperCaseButton = new System.Windows.Forms.Button();
            this.LowerCaseButton = new System.Windows.Forms.Button();
            this.NumbersButton = new System.Windows.Forms.Button();
            this.SpaceButton = new System.Windows.Forms.Button();
            this.closeNoSave = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvValues = new System.Windows.Forms.DataGridView();
            this.SaveLineButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 50;
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 50;
            this.toolTip1.ReshowDelay = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(980, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Position";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(807, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Located at the end";
            this.toolTip1.SetToolTip(this.label3, "Will only be placed at the end of the password");
            // 
            // EnterTokenLB1
            // 
            this.EnterTokenLB1.AutoSize = true;
            this.EnterTokenLB1.Location = new System.Drawing.Point(4, 1);
            this.EnterTokenLB1.Name = "EnterTokenLB1";
            this.EnterTokenLB1.Size = new System.Drawing.Size(61, 20);
            this.EnterTokenLB1.TabIndex = 0;
            this.EnterTokenLB1.Text = "Tokens";
            this.toolTip1.SetToolTip(this.EnterTokenLB1, "You may include multiple alternatives separated by a space. For example, if your " +
        "password is \"CorrectHorseBatteryStaple\", you would enter, one per line\": ");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(461, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Must be included";
            this.toolTip1.SetToolTip(this.label1, "If you are certain it\'s included in the password, check this box");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(634, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Located at the start";
            this.toolTip1.SetToolTip(this.label2, "Will only be placed at the beginning of the password");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 536);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(442, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Add variable characters to cursor position of \"password parts\"";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 652);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 50);
            this.button1.TabIndex = 6;
            this.button1.Text = "Accept";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LocatedEnd1CB
            // 
            this.LocatedEnd1CB.AutoSize = true;
            this.LocatedEnd1CB.Location = new System.Drawing.Point(807, 65);
            this.LocatedEnd1CB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LocatedEnd1CB.Name = "LocatedEnd1CB";
            this.LocatedEnd1CB.Size = new System.Drawing.Size(22, 21);
            this.LocatedEnd1CB.TabIndex = 8;
            this.LocatedEnd1CB.UseVisualStyleBackColor = true;
            // 
            // Position1Text
            // 
            this.Position1Text.Location = new System.Drawing.Point(980, 65);
            this.Position1Text.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Position1Text.Name = "Position1Text";
            this.Position1Text.Size = new System.Drawing.Size(112, 26);
            this.Position1Text.TabIndex = 0;
            // 
            // LocatedStart1CB
            // 
            this.LocatedStart1CB.AutoSize = true;
            this.LocatedStart1CB.Location = new System.Drawing.Point(634, 65);
            this.LocatedStart1CB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LocatedStart1CB.Name = "LocatedStart1CB";
            this.LocatedStart1CB.Size = new System.Drawing.Size(22, 21);
            this.LocatedStart1CB.TabIndex = 8;
            this.LocatedStart1CB.UseVisualStyleBackColor = true;
            // 
            // MustBeIncluded1CB
            // 
            this.MustBeIncluded1CB.AutoSize = true;
            this.MustBeIncluded1CB.Location = new System.Drawing.Point(461, 65);
            this.MustBeIncluded1CB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MustBeIncluded1CB.Name = "MustBeIncluded1CB";
            this.MustBeIncluded1CB.Size = new System.Drawing.Size(22, 21);
            this.MustBeIncluded1CB.TabIndex = 8;
            this.MustBeIncluded1CB.UseVisualStyleBackColor = true;
            // 
            // Token1Text
            // 
            this.Token1Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Token1Text.Location = new System.Drawing.Point(4, 65);
            this.Token1Text.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Token1Text.Name = "Token1Text";
            this.Token1Text.Size = new System.Drawing.Size(448, 35);
            this.Token1Text.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.75904F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.06024F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.06024F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.06024F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.06024F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.EnterTokenLB1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.MustBeIncluded1CB, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.LocatedStart1CB, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.Position1Text, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.LocatedEnd1CB, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.Token1Text, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 39);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1154, 121);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // UpperCaseButton
            // 
            this.UpperCaseButton.Location = new System.Drawing.Point(0, 571);
            this.UpperCaseButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UpperCaseButton.Name = "UpperCaseButton";
            this.UpperCaseButton.Size = new System.Drawing.Size(200, 50);
            this.UpperCaseButton.TabIndex = 9;
            this.UpperCaseButton.Text = "Upper case letters";
            this.UpperCaseButton.UseVisualStyleBackColor = true;
            this.UpperCaseButton.Click += new System.EventHandler(this.UpperCaseButton_Click);
            // 
            // LowerCaseButton
            // 
            this.LowerCaseButton.Location = new System.Drawing.Point(206, 571);
            this.LowerCaseButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LowerCaseButton.Name = "LowerCaseButton";
            this.LowerCaseButton.Size = new System.Drawing.Size(200, 50);
            this.LowerCaseButton.TabIndex = 10;
            this.LowerCaseButton.Text = "Lower case letters";
            this.LowerCaseButton.UseVisualStyleBackColor = true;
            this.LowerCaseButton.Click += new System.EventHandler(this.LowerCaseButton_Click);
            // 
            // NumbersButton
            // 
            this.NumbersButton.Location = new System.Drawing.Point(412, 571);
            this.NumbersButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NumbersButton.Name = "NumbersButton";
            this.NumbersButton.Size = new System.Drawing.Size(200, 50);
            this.NumbersButton.TabIndex = 11;
            this.NumbersButton.Text = "Numbers";
            this.NumbersButton.UseVisualStyleBackColor = true;
            this.NumbersButton.Click += new System.EventHandler(this.NumbersButton_Click);
            // 
            // SpaceButton
            // 
            this.SpaceButton.Location = new System.Drawing.Point(618, 571);
            this.SpaceButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SpaceButton.Name = "SpaceButton";
            this.SpaceButton.Size = new System.Drawing.Size(200, 50);
            this.SpaceButton.TabIndex = 12;
            this.SpaceButton.Text = "Space";
            this.SpaceButton.UseVisualStyleBackColor = true;
            this.SpaceButton.Click += new System.EventHandler(this.SpaceButton_Click);
            // 
            // closeNoSave
            // 
            this.closeNoSave.Location = new System.Drawing.Point(206, 652);
            this.closeNoSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.closeNoSave.Name = "closeNoSave";
            this.closeNoSave.Size = new System.Drawing.Size(200, 50);
            this.closeNoSave.TabIndex = 13;
            this.closeNoSave.Text = "Cancel";
            this.closeNoSave.UseVisualStyleBackColor = true;
            this.closeNoSave.Click += new System.EventHandler(this.closeNoSave_Click);
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Location = new System.Drawing.Point(0, 520);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1366, 2);
            this.label11.TabIndex = 43;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(0, 635);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1366, 2);
            this.label6.TabIndex = 45;
            // 
            // dgvValues
            // 
            this.dgvValues.AllowUserToAddRows = false;
            this.dgvValues.AllowUserToDeleteRows = false;
            this.dgvValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvValues.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvValues.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvValues.Location = new System.Drawing.Point(14, 276);
            this.dgvValues.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvValues.MaximumSize = new System.Drawing.Size(1153, 231);
            this.dgvValues.MinimumSize = new System.Drawing.Size(1153, 231);
            this.dgvValues.Name = "dgvValues";
            this.dgvValues.RowHeadersWidth = 50;
            this.dgvValues.Size = new System.Drawing.Size(1153, 231);
            this.dgvValues.TabIndex = 46;
            this.dgvValues.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvValues_CellEndEdit);
            // 
            // SaveLineButton
            // 
            this.SaveLineButton.Location = new System.Drawing.Point(16, 216);
            this.SaveLineButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SaveLineButton.Name = "SaveLineButton";
            this.SaveLineButton.Size = new System.Drawing.Size(200, 50);
            this.SaveLineButton.TabIndex = 47;
            this.SaveLineButton.Text = "Save line";
            this.SaveLineButton.UseVisualStyleBackColor = true;
            this.SaveLineButton.Click += new System.EventHandler(this.SaveLineButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(745, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(421, 29);
            this.label7.TabIndex = 49;
            this.label7.Text = "Need help creating tokens? Click here";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // EnterToken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 680);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.SaveLineButton);
            this.Controls.Add(this.dgvValues);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.closeNoSave);
            this.Controls.Add(this.SpaceButton);
            this.Controls.Add(this.NumbersButton);
            this.Controls.Add(this.LowerCaseButton);
            this.Controls.Add(this.UpperCaseButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(1362, 744);
            this.MinimumSize = new System.Drawing.Size(1362, 728);
            this.Name = "EnterToken";
            this.Text = "Enter tokens";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.EnterToken_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValues)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox LocatedEnd1CB;
        private System.Windows.Forms.TextBox Position1Text;
        private System.Windows.Forms.CheckBox LocatedStart1CB;
        private System.Windows.Forms.CheckBox MustBeIncluded1CB;
        private System.Windows.Forms.TextBox Token1Text;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label EnterTokenLB1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button UpperCaseButton;
        private System.Windows.Forms.Button LowerCaseButton;
        private System.Windows.Forms.Button NumbersButton;
        private System.Windows.Forms.Button SpaceButton;
        private System.Windows.Forms.Button closeNoSave;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvValues;
        private System.Windows.Forms.Button SaveLineButton;
        private System.Windows.Forms.Label label7;
    }
}