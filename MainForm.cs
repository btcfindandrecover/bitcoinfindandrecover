using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using KFS.Disks;
using KFS.FileSystems;
using KFS.DataStream;
using System.Data;
using System.Management;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Configuration;
using System.Security.Cryptography;

namespace CryptoFinder
{
    /// <summary>
    /// Summary description for MainForm.
    /// </summary>
    public class MainForm : System.Windows.Forms.Form
    {
        private ZinoLib.Windows.Controls.DriveComboBox _cbDrives;
        private System.Windows.Forms.Button RestoreWalletFilesButton;
        private Label DriveToScan;
        private CheckBox deletedFilesCB;
        private CheckBox existingFilesCB;
        private FolderBrowserDialog restoreFolderDialog;
        private Button recoveredFolderButton;
        private Label SelectCryptoCurrencies;
        private CheckBox ArmoryCB;
        private CheckBox BitcoinQTCB;
        private CheckBox CopayCB;
        private CheckBox ElectrumCB;
        private CheckBox MultibitHDCB;
        private CheckBox MultibitCB;
        private IContainer components;
        private CheckBox mSIGNACB;
        private CheckBox BitherCB;
        private TabControl tabControl1;
        private TabPage WalletFinderTab;
        private Label SelectScanLocations;
        private TabPage WalletPasswordRecoveryTab;
        private Button TokenFileButton;
        private OpenFileDialog openFileDialog;
        private Button EnterPasswordListButton;
        private Button PasswordFileButton;
        private Button EnterTokenListButton;
        private Label PasswordModificationsOptions;
        private CheckBox EnableTyposSearchCB;
        private CheckBox leetSpeakCB;
        private Label label2;
        private ToolTip toolTip1;
        private NumericUpDown MaxVariationsNumeric;
        private Button StartWalletPasswordRecovery;
        public BackgroundWorker backgroundWorkerRecover;
        private Button DeleteFilesWorkFolderButton;
        private ProgressBar progressBar1;
        private Button CancelWalletPasswordRecovery;
        private Button CancelRestoreWalletFilesButton;
        private ProgressBar progressBar2;
        public BackgroundWorker backgroundWorkerFinder;
        private Label ProgressLabel;
        private Button recoveredFolderButton2;
        private Label label5;
        private Button RecoverFileButton;
        private Label label4;
        private Label ETALabel;
        private System.Windows.Forms.Timer timer1;
        private long MaxFileSize = 786432;
        private Label CurrentFileLabel;
        private GroupBox groupBox1;
        private Label StatusFilesLabel;
        private Label label6;
        private Label label7;
        private Label label8;
        public FileSystemWatcher watcherRestore;
        private RadioButton radioButtonTokens;
        private RadioButton radioButtonPasswords;
        private Panel panel1;
        private Label label1;
        private Label label10;
        private PictureBox pictureBox1;
        private Label label11;
        private Label label12;
        private Label ProjectsPageLabel;
        private Label GeneralSupportIssue;
        private Label HelpCreatingListsLabel;
        public FileSystemWatcher watcherTMP;
        private Label label9;
        private Label PasswordsCounter;
        private CheckBox DisabledDedupCB;
        private Label label13;
        private Label StatusLabel;
        private Label label14;
        private CheckBox IgnoreMaximumETACB;

        public Config config;
        private Button button1;
        private Label label15;
        private Button ShowResultsButton;
        private Label label16;
        private Label label3;

        public bool DeletedFiles { get; set; }
        public string RestoreFolderDrive { get; set; }
        public string SelectedDrive { get; set; }
        public string WalletFileToRecover { get; set; }
        public string CurrentFileRecovery { get; set; }
        public int CurrentFileNumber { get; set; }
        public string AESKey { get; set; }
        public string Tokens
        {
            get
            {
                string cipherText = config.GetValue("//appSettings//add[@key='Tokens']");
                if (cipherText != "")
                {
                    return Crypto.Decrypt<AesManaged>(cipherText, AESKey, Salt);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                string cipherText = Crypto.Encrypt<AesManaged>(value, AESKey, Salt);
                config.SetValue("//appSettings//add[@key='" + "Tokens" + "']", cipherText);
            }
        }
        public string RestoreFolder
        {
            get
            {
                return config.GetValue("//appSettings//add[@key='RestoreFolder']");
            }
            set
            {
                config.SetValue("//appSettings//add[@key='" + "RestoreFolder" + "']", value);
            }
        }
        public string Passwords
        {
            get
            {
                string cipherText = config.GetValue("//appSettings//add[@key='Passwords']");
                if (cipherText != "")
                {
                    return Crypto.Decrypt<AesManaged>(cipherText, AESKey, Salt);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                string cipherText = Crypto.Encrypt<AesManaged>(value, AESKey, Salt);
                config.SetValue("//appSettings//add[@key='" + "Passwords" + "']", cipherText);
            }
        }
        public string Results
        {
            get
            {
                string cipherText = config.GetValue("//appSettings//add[@key='Results']");
                if (cipherText != "")
                {
                    return Crypto.Decrypt<AesManaged>(cipherText, AESKey, Salt);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                string cipherText = Crypto.Encrypt<AesManaged>(value, AESKey, Salt);
                config.SetValue("//appSettings//add[@key='" + "Results" + "']", cipherText);
            }
        }
        public string PasswordVerificationString
        {
            get
            {
                return config.GetValue("//appSettings//add[@key='PasswordVerificationString']");
            }
            set
            {
                string cipherText = Crypto.Encrypt<AesManaged>(value, AESKey, Salt);
                config.SetValue("//appSettings//add[@key='" + "PasswordVerificationString" + "']", cipherText);
            }
        }
        public string PasswordsOrTokens { get; set; }
        public string ResultsBuilder { get; set; }
        public string ApplicationDirectory { get; set; }
        public int ETA { get; set; }
        public int CurrentPasswordNumber { get; set; }
        public int TotalPasswords { get; set; }
        public int TotalFileNumber { get; set; }
        public string Command
        {
            get
            {
                string cipherText = config.GetValue("//appSettings//add[@key='Command']");
                if (cipherText != "")
                {
                    return Crypto.Decrypt<AesManaged>(cipherText, AESKey, Salt);
                }
                else
                {
                    return "";
                }
            }
            set
            {
                string cipherText = Crypto.Encrypt<AesManaged>(value, AESKey, Salt);
                config.SetValue("//appSettings//add[@key='" + "Command" + "']", cipherText);
            }
        }

        public string UpdatingTotal { get; set; }
        public string Salt { get; set; }
        //public int PasswordsPerSecond { get; set; }
        public string TempDir
        {
            get
            {
                return System.IO.Path.GetTempPath();
            }
        }
        public string RestoreFolderTemp
        {
            get
            {
                try
                {
                    Directory.CreateDirectory(RestoreFolder + @"RestoredTemp\");
                }
                catch
                { }
                return Helpers.GetShortName(RestoreFolder + @"RestoredTemp\");
            }
        }
        //public string WorkFolder
        //{
        //    get
        //    {
        //        return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BitcoinF&R";
        //    }
        //}

        public static bool scan_finished = false;
        public static void ScanFinished(object ob, EventArgs e)
        {
            scan_finished = true;
        }
        public MainForm()
        {
            //
            // Required for Windows Form Designer support
            // 
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.RestoreWalletFilesButton = new System.Windows.Forms.Button();
            this.DriveToScan = new System.Windows.Forms.Label();
            this.deletedFilesCB = new System.Windows.Forms.CheckBox();
            this.existingFilesCB = new System.Windows.Forms.CheckBox();
            this.restoreFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.recoveredFolderButton = new System.Windows.Forms.Button();
            this.SelectCryptoCurrencies = new System.Windows.Forms.Label();
            this.ElectrumCB = new System.Windows.Forms.CheckBox();
            this.MultibitHDCB = new System.Windows.Forms.CheckBox();
            this.MultibitCB = new System.Windows.Forms.CheckBox();
            this.CopayCB = new System.Windows.Forms.CheckBox();
            this.BitcoinQTCB = new System.Windows.Forms.CheckBox();
            this.ArmoryCB = new System.Windows.Forms.CheckBox();
            this.mSIGNACB = new System.Windows.Forms.CheckBox();
            this.BitherCB = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.WalletFinderTab = new System.Windows.Forms.TabPage();
            this.label16 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.HelpCreatingListsLabel = new System.Windows.Forms.Label();
            this.GeneralSupportIssue = new System.Windows.Forms.Label();
            this.ProjectsPageLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.CancelRestoreWalletFilesButton = new System.Windows.Forms.Button();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.DeleteFilesWorkFolderButton = new System.Windows.Forms.Button();
            this.SelectScanLocations = new System.Windows.Forms.Label();
            this._cbDrives = new ZinoLib.Windows.Controls.DriveComboBox();
            this.WalletPasswordRecoveryTab = new System.Windows.Forms.TabPage();
            this.ShowResultsButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.IgnoreMaximumETACB = new System.Windows.Forms.CheckBox();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.DisabledDedupCB = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonPasswords = new System.Windows.Forms.RadioButton();
            this.radioButtonTokens = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PasswordsCounter = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.StatusFilesLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CurrentFileLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ETALabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.RecoverFileButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.recoveredFolderButton2 = new System.Windows.Forms.Button();
            this.CancelWalletPasswordRecovery = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.StartWalletPasswordRecovery = new System.Windows.Forms.Button();
            this.MaxVariationsNumeric = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.leetSpeakCB = new System.Windows.Forms.CheckBox();
            this.EnableTyposSearchCB = new System.Windows.Forms.CheckBox();
            this.PasswordModificationsOptions = new System.Windows.Forms.Label();
            this.EnterPasswordListButton = new System.Windows.Forms.Button();
            this.PasswordFileButton = new System.Windows.Forms.Button();
            this.EnterTokenListButton = new System.Windows.Forms.Button();
            this.TokenFileButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundWorkerRecover = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerFinder = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.watcherTMP = new System.IO.FileSystemWatcher();
            this.watcherRestore = new System.IO.FileSystemWatcher();
            this.tabControl1.SuspendLayout();
            this.WalletFinderTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.WalletPasswordRecoveryTab.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxVariationsNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.watcherTMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.watcherRestore)).BeginInit();
            this.SuspendLayout();
            // 
            // RestoreWalletFilesButton
            // 
            this.RestoreWalletFilesButton.Location = new System.Drawing.Point(2, 368);
            this.RestoreWalletFilesButton.Name = "RestoreWalletFilesButton";
            this.RestoreWalletFilesButton.Size = new System.Drawing.Size(150, 39);
            this.RestoreWalletFilesButton.TabIndex = 1;
            this.RestoreWalletFilesButton.Text = "Start";
            this.RestoreWalletFilesButton.Click += new System.EventHandler(this.RestoreWalletFilesButton_Click);
            // 
            // DriveToScan
            // 
            this.DriveToScan.AutoSize = true;
            this.DriveToScan.Location = new System.Drawing.Point(4, 287);
            this.DriveToScan.Name = "DriveToScan";
            this.DriveToScan.Size = new System.Drawing.Size(132, 17);
            this.DriveToScan.TabIndex = 2;
            this.DriveToScan.Text = "Select drive to scan";
            // 
            // deletedFilesCB
            // 
            this.deletedFilesCB.AutoSize = true;
            this.deletedFilesCB.Checked = true;
            this.deletedFilesCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deletedFilesCB.Location = new System.Drawing.Point(410, 36);
            this.deletedFilesCB.Name = "deletedFilesCB";
            this.deletedFilesCB.Size = new System.Drawing.Size(170, 21);
            this.deletedFilesCB.TabIndex = 3;
            this.deletedFilesCB.Text = "Search in deleted files";
            this.deletedFilesCB.UseVisualStyleBackColor = true;
            // 
            // existingFilesCB
            // 
            this.existingFilesCB.AutoSize = true;
            this.existingFilesCB.Checked = true;
            this.existingFilesCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.existingFilesCB.Location = new System.Drawing.Point(410, 63);
            this.existingFilesCB.Name = "existingFilesCB";
            this.existingFilesCB.Size = new System.Drawing.Size(170, 21);
            this.existingFilesCB.TabIndex = 4;
            this.existingFilesCB.Text = "Search in existing files";
            this.existingFilesCB.UseVisualStyleBackColor = true;
            // 
            // recoveredFolderButton
            // 
            this.recoveredFolderButton.Location = new System.Drawing.Point(410, 118);
            this.recoveredFolderButton.Name = "recoveredFolderButton";
            this.recoveredFolderButton.Size = new System.Drawing.Size(150, 45);
            this.recoveredFolderButton.TabIndex = 6;
            this.recoveredFolderButton.Text = "Select work folder";
            this.recoveredFolderButton.UseVisualStyleBackColor = true;
            this.recoveredFolderButton.Click += new System.EventHandler(this.recoveredFolderButton_Click);
            // 
            // SelectCryptoCurrencies
            // 
            this.SelectCryptoCurrencies.AutoSize = true;
            this.SelectCryptoCurrencies.Location = new System.Drawing.Point(1, 3);
            this.SelectCryptoCurrencies.Name = "SelectCryptoCurrencies";
            this.SelectCryptoCurrencies.Size = new System.Drawing.Size(177, 17);
            this.SelectCryptoCurrencies.TabIndex = 8;
            this.SelectCryptoCurrencies.Text = "Select wallets to search for";
            // 
            // ElectrumCB
            // 
            this.ElectrumCB.AutoSize = true;
            this.ElectrumCB.Checked = true;
            this.ElectrumCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ElectrumCB.Location = new System.Drawing.Point(6, 131);
            this.ElectrumCB.Name = "ElectrumCB";
            this.ElectrumCB.Size = new System.Drawing.Size(85, 21);
            this.ElectrumCB.TabIndex = 10;
            this.ElectrumCB.Text = "Electrum";
            this.ElectrumCB.UseVisualStyleBackColor = true;
            // 
            // MultibitHDCB
            // 
            this.MultibitHDCB.AutoSize = true;
            this.MultibitHDCB.Checked = true;
            this.MultibitHDCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MultibitHDCB.Location = new System.Drawing.Point(6, 178);
            this.MultibitHDCB.Name = "MultibitHDCB";
            this.MultibitHDCB.Size = new System.Drawing.Size(94, 21);
            this.MultibitHDCB.TabIndex = 11;
            this.MultibitHDCB.Text = "MultibitHD";
            this.MultibitHDCB.UseVisualStyleBackColor = true;
            // 
            // MultibitCB
            // 
            this.MultibitCB.AutoSize = true;
            this.MultibitCB.Checked = true;
            this.MultibitCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MultibitCB.Location = new System.Drawing.Point(6, 202);
            this.MultibitCB.Name = "MultibitCB";
            this.MultibitCB.Size = new System.Drawing.Size(74, 21);
            this.MultibitCB.TabIndex = 12;
            this.MultibitCB.Text = "Multibit";
            this.MultibitCB.UseVisualStyleBackColor = true;
            // 
            // CopayCB
            // 
            this.CopayCB.AutoSize = true;
            this.CopayCB.Checked = true;
            this.CopayCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CopayCB.Location = new System.Drawing.Point(6, 110);
            this.CopayCB.Name = "CopayCB";
            this.CopayCB.Size = new System.Drawing.Size(70, 21);
            this.CopayCB.TabIndex = 13;
            this.CopayCB.Text = "Copay";
            this.CopayCB.UseVisualStyleBackColor = true;
            // 
            // BitcoinQTCB
            // 
            this.BitcoinQTCB.AutoSize = true;
            this.BitcoinQTCB.Checked = true;
            this.BitcoinQTCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BitcoinQTCB.Location = new System.Drawing.Point(6, 63);
            this.BitcoinQTCB.Name = "BitcoinQTCB";
            this.BitcoinQTCB.Size = new System.Drawing.Size(92, 21);
            this.BitcoinQTCB.TabIndex = 14;
            this.BitcoinQTCB.Text = "BitcoinQT";
            this.BitcoinQTCB.UseVisualStyleBackColor = true;
            // 
            // ArmoryCB
            // 
            this.ArmoryCB.AutoSize = true;
            this.ArmoryCB.Checked = true;
            this.ArmoryCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ArmoryCB.Location = new System.Drawing.Point(6, 36);
            this.ArmoryCB.Name = "ArmoryCB";
            this.ArmoryCB.Size = new System.Drawing.Size(75, 21);
            this.ArmoryCB.TabIndex = 15;
            this.ArmoryCB.Text = "Armory";
            this.ArmoryCB.UseVisualStyleBackColor = true;
            // 
            // mSIGNACB
            // 
            this.mSIGNACB.AutoSize = true;
            this.mSIGNACB.Checked = true;
            this.mSIGNACB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mSIGNACB.Location = new System.Drawing.Point(6, 156);
            this.mSIGNACB.Name = "mSIGNACB";
            this.mSIGNACB.Size = new System.Drawing.Size(83, 21);
            this.mSIGNACB.TabIndex = 16;
            this.mSIGNACB.Text = "mSIGNA";
            this.mSIGNACB.UseVisualStyleBackColor = true;
            // 
            // BitherCB
            // 
            this.BitherCB.AutoSize = true;
            this.BitherCB.Checked = true;
            this.BitherCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BitherCB.Location = new System.Drawing.Point(6, 87);
            this.BitherCB.Name = "BitherCB";
            this.BitherCB.Size = new System.Drawing.Size(67, 21);
            this.BitherCB.TabIndex = 17;
            this.BitherCB.Text = "Bither";
            this.BitherCB.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.WalletFinderTab);
            this.tabControl1.Controls.Add(this.WalletPasswordRecoveryTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1070, 606);
            this.tabControl1.TabIndex = 18;
            // 
            // WalletFinderTab
            // 
            this.WalletFinderTab.Controls.Add(this.label16);
            this.WalletFinderTab.Controls.Add(this.label3);
            this.WalletFinderTab.Controls.Add(this.HelpCreatingListsLabel);
            this.WalletFinderTab.Controls.Add(this.GeneralSupportIssue);
            this.WalletFinderTab.Controls.Add(this.ProjectsPageLabel);
            this.WalletFinderTab.Controls.Add(this.label12);
            this.WalletFinderTab.Controls.Add(this.label11);
            this.WalletFinderTab.Controls.Add(this.pictureBox1);
            this.WalletFinderTab.Controls.Add(this.ProgressLabel);
            this.WalletFinderTab.Controls.Add(this.CancelRestoreWalletFilesButton);
            this.WalletFinderTab.Controls.Add(this.progressBar2);
            this.WalletFinderTab.Controls.Add(this.DeleteFilesWorkFolderButton);
            this.WalletFinderTab.Controls.Add(this.SelectScanLocations);
            this.WalletFinderTab.Controls.Add(this._cbDrives);
            this.WalletFinderTab.Controls.Add(this.DriveToScan);
            this.WalletFinderTab.Controls.Add(this.recoveredFolderButton);
            this.WalletFinderTab.Controls.Add(this.RestoreWalletFilesButton);
            this.WalletFinderTab.Controls.Add(this.SelectCryptoCurrencies);
            this.WalletFinderTab.Controls.Add(this.BitherCB);
            this.WalletFinderTab.Controls.Add(this.existingFilesCB);
            this.WalletFinderTab.Controls.Add(this.ElectrumCB);
            this.WalletFinderTab.Controls.Add(this.deletedFilesCB);
            this.WalletFinderTab.Controls.Add(this.mSIGNACB);
            this.WalletFinderTab.Controls.Add(this.MultibitHDCB);
            this.WalletFinderTab.Controls.Add(this.ArmoryCB);
            this.WalletFinderTab.Controls.Add(this.MultibitCB);
            this.WalletFinderTab.Controls.Add(this.BitcoinQTCB);
            this.WalletFinderTab.Controls.Add(this.CopayCB);
            this.WalletFinderTab.Location = new System.Drawing.Point(4, 25);
            this.WalletFinderTab.Name = "WalletFinderTab";
            this.WalletFinderTab.Padding = new System.Windows.Forms.Padding(3);
            this.WalletFinderTab.Size = new System.Drawing.Size(1062, 577);
            this.WalletFinderTab.TabIndex = 0;
            this.WalletFinderTab.Text = "Wallet file search";
            this.WalletFinderTab.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(800, 432);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(124, 17);
            this.label16.TabIndex = 51;
            this.label16.Text = "have a suggestion";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(800, 377);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 17);
            this.label3.TabIndex = 50;
            this.label3.Text = "possible passwords";
            // 
            // HelpCreatingListsLabel
            // 
            this.HelpCreatingListsLabel.AutoSize = true;
            this.HelpCreatingListsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpCreatingListsLabel.Location = new System.Drawing.Point(800, 357);
            this.HelpCreatingListsLabel.Name = "HelpCreatingListsLabel";
            this.HelpCreatingListsLabel.Size = new System.Drawing.Size(243, 17);
            this.HelpCreatingListsLabel.TabIndex = 49;
            this.HelpCreatingListsLabel.Text = "I need help creating a list to generate";
            this.HelpCreatingListsLabel.Click += new System.EventHandler(this.HelpCreatingListsLabel_Click);
            // 
            // GeneralSupportIssue
            // 
            this.GeneralSupportIssue.AutoSize = true;
            this.GeneralSupportIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GeneralSupportIssue.Location = new System.Drawing.Point(800, 415);
            this.GeneralSupportIssue.Name = "GeneralSupportIssue";
            this.GeneralSupportIssue.Size = new System.Drawing.Size(239, 17);
            this.GeneralSupportIssue.TabIndex = 48;
            this.GeneralSupportIssue.Text = "I have a problem with the software or";
            this.GeneralSupportIssue.Click += new System.EventHandler(this.GeneralSupportIssue_Click);
            // 
            // ProjectsPageLabel
            // 
            this.ProjectsPageLabel.AutoSize = true;
            this.ProjectsPageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectsPageLabel.Location = new System.Drawing.Point(800, 324);
            this.ProjectsPageLabel.Name = "ProjectsPageLabel";
            this.ProjectsPageLabel.Size = new System.Drawing.Size(98, 17);
            this.ProjectsPageLabel.TabIndex = 46;
            this.ProjectsPageLabel.Text = "Project\'s page";
            this.ProjectsPageLabel.Click += new System.EventHandler(this.ProjectsPageLabel_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(868, 274);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 31);
            this.label12.TabIndex = 43;
            this.label12.Text = "Help";
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Location = new System.Drawing.Point(0, 268);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(1070, 1);
            this.label11.TabIndex = 42;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(800, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(220, 248);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Location = new System.Drawing.Point(13, 432);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(0, 17);
            this.ProgressLabel.TabIndex = 24;
            // 
            // CancelRestoreWalletFilesButton
            // 
            this.CancelRestoreWalletFilesButton.Location = new System.Drawing.Point(157, 368);
            this.CancelRestoreWalletFilesButton.Name = "CancelRestoreWalletFilesButton";
            this.CancelRestoreWalletFilesButton.Size = new System.Drawing.Size(150, 39);
            this.CancelRestoreWalletFilesButton.TabIndex = 23;
            this.CancelRestoreWalletFilesButton.Text = "Stop";
            this.CancelRestoreWalletFilesButton.UseVisualStyleBackColor = true;
            this.CancelRestoreWalletFilesButton.Click += new System.EventHandler(this.CancelRestoreWalletFilesButton_Click);
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(9, 475);
            this.progressBar2.Maximum = 9;
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(1045, 39);
            this.progressBar2.Step = 1;
            this.progressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar2.TabIndex = 22;
            // 
            // DeleteFilesWorkFolderButton
            // 
            this.DeleteFilesWorkFolderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.DeleteFilesWorkFolderButton.Location = new System.Drawing.Point(409, 167);
            this.DeleteFilesWorkFolderButton.Name = "DeleteFilesWorkFolderButton";
            this.DeleteFilesWorkFolderButton.Size = new System.Drawing.Size(150, 45);
            this.DeleteFilesWorkFolderButton.TabIndex = 21;
            this.DeleteFilesWorkFolderButton.Text = "Delete files in work folder";
            this.DeleteFilesWorkFolderButton.UseVisualStyleBackColor = true;
            this.DeleteFilesWorkFolderButton.Click += new System.EventHandler(this.DeleteFilesWorkFolderButton_Click);
            // 
            // SelectScanLocations
            // 
            this.SelectScanLocations.AutoSize = true;
            this.SelectScanLocations.Location = new System.Drawing.Point(406, 3);
            this.SelectScanLocations.Name = "SelectScanLocations";
            this.SelectScanLocations.Size = new System.Drawing.Size(170, 17);
            this.SelectScanLocations.TabIndex = 18;
            this.SelectScanLocations.Text = "Select locations to search";
            // 
            // _cbDrives
            // 
            this._cbDrives.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._cbDrives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cbDrives.Location = new System.Drawing.Point(6, 317);
            this._cbDrives.Name = "_cbDrives";
            this._cbDrives.Size = new System.Drawing.Size(268, 23);
            this._cbDrives.TabIndex = 0;
            // 
            // WalletPasswordRecoveryTab
            // 
            this.WalletPasswordRecoveryTab.Controls.Add(this.ShowResultsButton);
            this.WalletPasswordRecoveryTab.Controls.Add(this.button1);
            this.WalletPasswordRecoveryTab.Controls.Add(this.label15);
            this.WalletPasswordRecoveryTab.Controls.Add(this.IgnoreMaximumETACB);
            this.WalletPasswordRecoveryTab.Controls.Add(this.StatusLabel);
            this.WalletPasswordRecoveryTab.Controls.Add(this.label14);
            this.WalletPasswordRecoveryTab.Controls.Add(this.DisabledDedupCB);
            this.WalletPasswordRecoveryTab.Controls.Add(this.label13);
            this.WalletPasswordRecoveryTab.Controls.Add(this.label10);
            this.WalletPasswordRecoveryTab.Controls.Add(this.label1);
            this.WalletPasswordRecoveryTab.Controls.Add(this.panel1);
            this.WalletPasswordRecoveryTab.Controls.Add(this.groupBox1);
            this.WalletPasswordRecoveryTab.Controls.Add(this.label5);
            this.WalletPasswordRecoveryTab.Controls.Add(this.RecoverFileButton);
            this.WalletPasswordRecoveryTab.Controls.Add(this.label4);
            this.WalletPasswordRecoveryTab.Controls.Add(this.recoveredFolderButton2);
            this.WalletPasswordRecoveryTab.Controls.Add(this.CancelWalletPasswordRecovery);
            this.WalletPasswordRecoveryTab.Controls.Add(this.progressBar1);
            this.WalletPasswordRecoveryTab.Controls.Add(this.StartWalletPasswordRecovery);
            this.WalletPasswordRecoveryTab.Controls.Add(this.MaxVariationsNumeric);
            this.WalletPasswordRecoveryTab.Controls.Add(this.label2);
            this.WalletPasswordRecoveryTab.Controls.Add(this.leetSpeakCB);
            this.WalletPasswordRecoveryTab.Controls.Add(this.EnableTyposSearchCB);
            this.WalletPasswordRecoveryTab.Controls.Add(this.PasswordModificationsOptions);
            this.WalletPasswordRecoveryTab.Controls.Add(this.EnterPasswordListButton);
            this.WalletPasswordRecoveryTab.Controls.Add(this.PasswordFileButton);
            this.WalletPasswordRecoveryTab.Controls.Add(this.EnterTokenListButton);
            this.WalletPasswordRecoveryTab.Controls.Add(this.TokenFileButton);
            this.WalletPasswordRecoveryTab.Location = new System.Drawing.Point(4, 25);
            this.WalletPasswordRecoveryTab.Name = "WalletPasswordRecoveryTab";
            this.WalletPasswordRecoveryTab.Padding = new System.Windows.Forms.Padding(3);
            this.WalletPasswordRecoveryTab.Size = new System.Drawing.Size(1062, 577);
            this.WalletPasswordRecoveryTab.TabIndex = 1;
            this.WalletPasswordRecoveryTab.Text = "Wallet password recovery";
            this.WalletPasswordRecoveryTab.UseVisualStyleBackColor = true;
            // 
            // ShowResultsButton
            // 
            this.ShowResultsButton.Location = new System.Drawing.Point(317, 254);
            this.ShowResultsButton.Name = "ShowResultsButton";
            this.ShowResultsButton.Size = new System.Drawing.Size(150, 40);
            this.ShowResultsButton.TabIndex = 50;
            this.ShowResultsButton.Text = "Show results";
            this.ShowResultsButton.UseVisualStyleBackColor = true;
            this.ShowResultsButton.Click += new System.EventHandler(this.ShowResultsButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(791, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 40);
            this.button1.TabIndex = 49;
            this.button1.Text = "Delete information";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(788, 2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(229, 17);
            this.label15.TabIndex = 48;
            this.label15.Text = "Delete passwords used and results";
            // 
            // IgnoreMaximumETACB
            // 
            this.IgnoreMaximumETACB.AutoSize = true;
            this.IgnoreMaximumETACB.Location = new System.Drawing.Point(602, 306);
            this.IgnoreMaximumETACB.Name = "IgnoreMaximumETACB";
            this.IgnoreMaximumETACB.Size = new System.Drawing.Size(163, 21);
            this.IgnoreMaximumETACB.TabIndex = 47;
            this.IgnoreMaximumETACB.Text = "Ignore maximum ETA";
            this.IgnoreMaximumETACB.UseVisualStyleBackColor = true;
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.StatusLabel.Location = new System.Drawing.Point(10, 331);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(56, 25);
            this.StatusLabel.TabIndex = 46;
            this.StatusLabel.Text = "none";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(10, 306);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 25);
            this.label14.TabIndex = 45;
            this.label14.Text = "Status: ";
            // 
            // DisabledDedupCB
            // 
            this.DisabledDedupCB.AutoSize = true;
            this.DisabledDedupCB.Location = new System.Drawing.Point(602, 283);
            this.DisabledDedupCB.Name = "DisabledDedupCB";
            this.DisabledDedupCB.Size = new System.Drawing.Size(258, 21);
            this.DisabledDedupCB.TabIndex = 44;
            this.DisabledDedupCB.Text = "Disable duplicated passwords check";
            this.DisabledDedupCB.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(598, 254);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(121, 17);
            this.label13.TabIndex = 43;
            this.label13.Text = "Advanced options";
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Location = new System.Drawing.Point(0, 238);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(1070, 1);
            this.label10.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(0, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1070, 1);
            this.label1.TabIndex = 41;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonPasswords);
            this.panel1.Controls.Add(this.radioButtonTokens);
            this.panel1.Location = new System.Drawing.Point(6, 118);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(975, 23);
            this.panel1.TabIndex = 40;
            // 
            // radioButtonPasswords
            // 
            this.radioButtonPasswords.AutoSize = true;
            this.radioButtonPasswords.Location = new System.Drawing.Point(3, 0);
            this.radioButtonPasswords.Name = "radioButtonPasswords";
            this.radioButtonPasswords.Size = new System.Drawing.Size(199, 21);
            this.radioButtonPasswords.TabIndex = 38;
            this.radioButtonPasswords.TabStop = true;
            this.radioButtonPasswords.Text = "List of complete passwords";
            this.radioButtonPasswords.UseVisualStyleBackColor = true;
            this.radioButtonPasswords.CheckedChanged += new System.EventHandler(this.radioButtonPasswords_CheckedChanged);
            // 
            // radioButtonTokens
            // 
            this.radioButtonTokens.AutoSize = true;
            this.radioButtonTokens.Location = new System.Drawing.Point(560, 0);
            this.radioButtonTokens.Name = "radioButtonTokens";
            this.radioButtonTokens.Size = new System.Drawing.Size(320, 21);
            this.radioButtonTokens.TabIndex = 39;
            this.radioButtonTokens.TabStop = true;
            this.radioButtonTokens.Text = "List of elements that form a password (tokens)";
            this.radioButtonTokens.UseVisualStyleBackColor = true;
            this.radioButtonTokens.CheckedChanged += new System.EventHandler(this.radioButtonPasswords_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.PasswordsCounter);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.StatusFilesLabel);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.CurrentFileLabel);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.ETALabel);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(6, 359);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1050, 87);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            // 
            // PasswordsCounter
            // 
            this.PasswordsCounter.AutoSize = true;
            this.PasswordsCounter.Location = new System.Drawing.Point(574, 64);
            this.PasswordsCounter.Name = "PasswordsCounter";
            this.PasswordsCounter.Size = new System.Drawing.Size(0, 17);
            this.PasswordsCounter.TabIndex = 40;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(574, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(185, 17);
            this.label9.TabIndex = 39;
            this.label9.Text = "Total passwords to process:";
            // 
            // StatusFilesLabel
            // 
            this.StatusFilesLabel.AutoSize = true;
            this.StatusFilesLabel.Location = new System.Drawing.Point(6, 64);
            this.StatusFilesLabel.Name = "StatusFilesLabel";
            this.StatusFilesLabel.Size = new System.Drawing.Size(0, 17);
            this.StatusFilesLabel.TabIndex = 35;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 17);
            this.label6.TabIndex = 38;
            this.label6.Text = "Files processed:";
            // 
            // CurrentFileLabel
            // 
            this.CurrentFileLabel.AutoSize = true;
            this.CurrentFileLabel.Location = new System.Drawing.Point(574, 28);
            this.CurrentFileLabel.Name = "CurrentFileLabel";
            this.CurrentFileLabel.Size = new System.Drawing.Size(0, 17);
            this.CurrentFileLabel.TabIndex = 35;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 17);
            this.label7.TabIndex = 36;
            this.label7.Text = "Current file ETA:";
            // 
            // ETALabel
            // 
            this.ETALabel.AutoSize = true;
            this.ETALabel.Location = new System.Drawing.Point(6, 28);
            this.ETALabel.Name = "ETALabel";
            this.ETALabel.Size = new System.Drawing.Size(0, 17);
            this.ETALabel.TabIndex = 34;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(574, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 17);
            this.label8.TabIndex = 37;
            this.label8.Text = "Current file:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(203, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(285, 17);
            this.label5.TabIndex = 33;
            this.label5.Text = "instead of all files in the folder, select it here";
            // 
            // RecoverFileButton
            // 
            this.RecoverFileButton.Enabled = false;
            this.RecoverFileButton.Location = new System.Drawing.Point(206, 37);
            this.RecoverFileButton.Name = "RecoverFileButton";
            this.RecoverFileButton.Size = new System.Drawing.Size(150, 40);
            this.RecoverFileButton.TabIndex = 32;
            this.RecoverFileButton.Text = "Unique file recovery";
            this.RecoverFileButton.UseVisualStyleBackColor = true;
            this.RecoverFileButton.Click += new System.EventHandler(this.SelectWalletFile);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(203, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(327, 17);
            this.label4.TabIndex = 31;
            this.label4.Text = "Optional: if you\'d like to try to recover only one file,";
            // 
            // recoveredFolderButton2
            // 
            this.recoveredFolderButton2.Location = new System.Drawing.Point(5, 37);
            this.recoveredFolderButton2.Name = "recoveredFolderButton2";
            this.recoveredFolderButton2.Size = new System.Drawing.Size(150, 40);
            this.recoveredFolderButton2.TabIndex = 30;
            this.recoveredFolderButton2.Text = "Select work folder";
            this.recoveredFolderButton2.UseVisualStyleBackColor = true;
            this.recoveredFolderButton2.Click += new System.EventHandler(this.recoveredFolderButton_Click);
            // 
            // CancelWalletPasswordRecovery
            // 
            this.CancelWalletPasswordRecovery.Location = new System.Drawing.Point(163, 254);
            this.CancelWalletPasswordRecovery.Name = "CancelWalletPasswordRecovery";
            this.CancelWalletPasswordRecovery.Size = new System.Drawing.Size(150, 40);
            this.CancelWalletPasswordRecovery.TabIndex = 28;
            this.CancelWalletPasswordRecovery.Text = "Cancel";
            this.CancelWalletPasswordRecovery.UseVisualStyleBackColor = true;
            this.CancelWalletPasswordRecovery.Click += new System.EventHandler(this.CancelWalletPasswordRecovery_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 475);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1045, 39);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 27;
            // 
            // StartWalletPasswordRecovery
            // 
            this.StartWalletPasswordRecovery.Location = new System.Drawing.Point(9, 254);
            this.StartWalletPasswordRecovery.Name = "StartWalletPasswordRecovery";
            this.StartWalletPasswordRecovery.Size = new System.Drawing.Size(150, 40);
            this.StartWalletPasswordRecovery.TabIndex = 26;
            this.StartWalletPasswordRecovery.Text = "Start";
            this.StartWalletPasswordRecovery.UseVisualStyleBackColor = true;
            this.StartWalletPasswordRecovery.Click += new System.EventHandler(this.StartWalletPasswordRecovery_Click);
            // 
            // MaxVariationsNumeric
            // 
            this.MaxVariationsNumeric.Enabled = false;
            this.MaxVariationsNumeric.Location = new System.Drawing.Point(829, 201);
            this.MaxVariationsNumeric.Name = "MaxVariationsNumeric";
            this.MaxVariationsNumeric.Size = new System.Drawing.Size(78, 22);
            this.MaxVariationsNumeric.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(560, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(266, 17);
            this.label2.TabIndex = 24;
            this.label2.Text = "Elements used to generate the password";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // leetSpeakCB
            // 
            this.leetSpeakCB.AutoSize = true;
            this.leetSpeakCB.Location = new System.Drawing.Point(559, 61);
            this.leetSpeakCB.Name = "leetSpeakCB";
            this.leetSpeakCB.Size = new System.Drawing.Size(188, 21);
            this.leetSpeakCB.TabIndex = 23;
            this.leetSpeakCB.Text = "Leet speak (L337 sp3ak)";
            this.leetSpeakCB.UseVisualStyleBackColor = true;
            // 
            // EnableTyposSearchCB
            // 
            this.EnableTyposSearchCB.AutoSize = true;
            this.EnableTyposSearchCB.Location = new System.Drawing.Point(559, 38);
            this.EnableTyposSearchCB.Name = "EnableTyposSearchCB";
            this.EnableTyposSearchCB.Size = new System.Drawing.Size(123, 21);
            this.EnableTyposSearchCB.TabIndex = 13;
            this.EnableTyposSearchCB.Text = "Common typos";
            this.EnableTyposSearchCB.UseVisualStyleBackColor = true;
            // 
            // PasswordModificationsOptions
            // 
            this.PasswordModificationsOptions.AutoSize = true;
            this.PasswordModificationsOptions.Location = new System.Drawing.Point(557, 3);
            this.PasswordModificationsOptions.Name = "PasswordModificationsOptions";
            this.PasswordModificationsOptions.Size = new System.Drawing.Size(155, 17);
            this.PasswordModificationsOptions.TabIndex = 9;
            this.PasswordModificationsOptions.Text = "Password modifications";
            // 
            // EnterPasswordListButton
            // 
            this.EnterPasswordListButton.Enabled = false;
            this.EnterPasswordListButton.Location = new System.Drawing.Point(163, 158);
            this.EnterPasswordListButton.Name = "EnterPasswordListButton";
            this.EnterPasswordListButton.Size = new System.Drawing.Size(150, 40);
            this.EnterPasswordListButton.TabIndex = 8;
            this.EnterPasswordListButton.Text = "Type them in";
            this.EnterPasswordListButton.UseVisualStyleBackColor = true;
            this.EnterPasswordListButton.Click += new System.EventHandler(this.EnterPasswordListButton_Click);
            // 
            // PasswordFileButton
            // 
            this.PasswordFileButton.Enabled = false;
            this.PasswordFileButton.Location = new System.Drawing.Point(9, 158);
            this.PasswordFileButton.Name = "PasswordFileButton";
            this.PasswordFileButton.Size = new System.Drawing.Size(150, 40);
            this.PasswordFileButton.TabIndex = 7;
            this.PasswordFileButton.Text = "Select file";
            this.PasswordFileButton.UseVisualStyleBackColor = true;
            this.PasswordFileButton.Click += new System.EventHandler(this.PasswordFileButton_Click);
            // 
            // EnterTokenListButton
            // 
            this.EnterTokenListButton.Enabled = false;
            this.EnterTokenListButton.Location = new System.Drawing.Point(757, 158);
            this.EnterTokenListButton.Name = "EnterTokenListButton";
            this.EnterTokenListButton.Size = new System.Drawing.Size(150, 40);
            this.EnterTokenListButton.TabIndex = 5;
            this.EnterTokenListButton.Text = "Type them in";
            this.EnterTokenListButton.UseVisualStyleBackColor = true;
            this.EnterTokenListButton.Click += new System.EventHandler(this.EnterTokenListButton_Click);
            // 
            // TokenFileButton
            // 
            this.TokenFileButton.Enabled = false;
            this.TokenFileButton.Location = new System.Drawing.Point(601, 158);
            this.TokenFileButton.Name = "TokenFileButton";
            this.TokenFileButton.Size = new System.Drawing.Size(150, 40);
            this.TokenFileButton.TabIndex = 4;
            this.TokenFileButton.Text = "Select file";
            this.TokenFileButton.UseVisualStyleBackColor = true;
            this.TokenFileButton.Click += new System.EventHandler(this.TokenFileButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "Select file";
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 50;
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 50;
            this.toolTip1.ReshowDelay = 10;
            // 
            // backgroundWorkerRecover
            // 
            this.backgroundWorkerRecover.WorkerReportsProgress = true;
            this.backgroundWorkerRecover.WorkerSupportsCancellation = true;
            this.backgroundWorkerRecover.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerRecover_DoWork);
            this.backgroundWorkerRecover.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerRecover_ProgressChanged);
            this.backgroundWorkerRecover.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerRecover_RunWorkerCompleted);
            // 
            // backgroundWorkerFinder
            // 
            this.backgroundWorkerFinder.WorkerReportsProgress = true;
            this.backgroundWorkerFinder.WorkerSupportsCancellation = true;
            this.backgroundWorkerFinder.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerFinder_DoWork);
            this.backgroundWorkerFinder.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerFinder_ProgressChanged);
            this.backgroundWorkerFinder.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerFinder_RunWorkerCompleted);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // watcherTMP
            // 
            this.watcherTMP.EnableRaisingEvents = true;
            this.watcherTMP.Filter = "*.tmp";
            this.watcherTMP.SynchronizingObject = this;
            this.watcherTMP.Changed += new System.IO.FileSystemEventHandler(this.OnChanged);
            this.watcherTMP.Created += new System.IO.FileSystemEventHandler(this.OnChanged);
            // 
            // watcherRestore
            // 
            this.watcherRestore.EnableRaisingEvents = true;
            this.watcherRestore.Filter = "*.tmp";
            this.watcherRestore.IncludeSubdirectories = true;
            this.watcherRestore.SynchronizingObject = this;
            this.watcherRestore.Changed += new System.IO.FileSystemEventHandler(this.watcherRestore_Changed);
            this.watcherRestore.Created += new System.IO.FileSystemEventHandler(this.watcherRestore_Changed);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1082, 561);
            this.Controls.Add(this.tabControl1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1100, 606);
            this.MinimumSize = new System.Drawing.Size(1100, 606);
            this.Name = "MainForm";
            this.Text = "Bitcoin find & recover 1.5";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CancelWalletPasswordRecovery_Click);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tabControl1.ResumeLayout(false);
            this.WalletFinderTab.ResumeLayout(false);
            this.WalletFinderTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.WalletPasswordRecoveryTab.ResumeLayout(false);
            this.WalletPasswordRecoveryTab.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxVariationsNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.watcherTMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.watcherRestore)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        #region Wallet finder background worker

        public void backgroundWorkerFinder_DoWork(object sender, DoWorkEventArgs e)
        {
            DeletedFiles = deletedFilesCB.Checked;
            IDCryptoFiles _idCryptoFiles;
            _idCryptoFiles = new IDCryptoFiles();
            _idCryptoFiles.Armory = ArmoryCB.Checked;
            _idCryptoFiles.BitcoinQT = BitcoinQTCB.Checked;
            _idCryptoFiles.Bither = BitherCB.Checked;
            _idCryptoFiles.Copay = CopayCB.Checked;
            _idCryptoFiles.DeletedFiles = DeletedFiles;
            //_idCryptoFiles.DeepScan = deepScanCB.Checked;
            _idCryptoFiles.Electrum = ElectrumCB.Checked;
            _idCryptoFiles.mSIGNA = mSIGNACB.Checked;
            _idCryptoFiles.Multibit = MultibitCB.Checked;
            _idCryptoFiles.MultibitHD = MultibitHDCB.Checked;
            _idCryptoFiles.MaxFileSize = MaxFileSize;
            _idCryptoFiles.RestoreFolder = RestoreFolder;
            if (DeletedFiles == true)
            {
                RestoreFiles(SelectedDrive, RestoreFolderTemp);
                bool result = _idCryptoFiles.WalletFilesLister(RestoreFolderTemp);
            }
            if (existingFilesCB.Checked == true)
            {
                //MessageBox.Show("1 at mainform");
                _idCryptoFiles.DeletedFiles = false;
                bool result = _idCryptoFiles.WalletFilesLister(SelectedDrive);
            }
            System.Diagnostics.Process.Start(RestoreFolder);
        }
        private void backgroundWorkerFinder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar2.Value = e.ProgressPercentage;
            progressBar2.Update();
            ProgressLabel.Text = e.UserState.ToString();
        }

        private void backgroundWorkerFinder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar2.Value = 0;
            progressBar2.Update();
            ProgressLabel.Text = "";
        }
        #endregion
        #region Wallet recover background worker
        public void backgroundWorkerRecover_DoWork(object sender, DoWorkEventArgs e)
        {
            //List<string> found = new List<string>();
            //List<string> notFound = new List<string>();
            IEnumerable<string> IEnumerableList;
            List<string> filesList;
            if (e.Argument.ToString().Contains("folder"))
            {
                IEnumerableList = Directory.EnumerateFiles(RestoreFolder, "*", SearchOption.AllDirectories);
                filesList = IEnumerableList.Cast<string>().ToList();
            }
            else
            {
                List<string> file = new List<string>() { e.Argument.ToString() };
                filesList = file;
            }

            for (int k = filesList.Count - 1; k >= 0; k--)
            {
                if (filesList[k].Contains(@"\PasswordRecovery") || filesList[k].Contains(@"-progress.tmp"))
                {
                    filesList.Remove(filesList[k]);
                }
            }

            int progressBarMax = filesList.Count();
            int i = 0;
            //StreamWriter tw1 = new StreamWriter(TempDir + "WorkFolder.tmp");
            //tw1.Write(WorkFolder);
            //tw1.Close();
            ApplicationDirectory = Helpers.GetShortName(AppDomain.CurrentDomain.BaseDirectory);
            StreamWriter tw2 = new StreamWriter(TempDir + "TotalFileNumber.tmp");
            tw2.Write(filesList.Count());
            tw2.Close();

            foreach (string file in filesList)
            {
                int percentage = (i * 100) / filesList.Count();
                StreamWriter tw4 = new StreamWriter(TempDir + "CurrentFileNumber.tmp");
                tw4.Write(i);
                tw4.Close();
                backgroundWorkerRecover.ReportProgress(percentage);
                if (backgroundWorkerRecover.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }
                CurrentFileRecovery = file;
                string fileShortName = Helpers.GetShortName(file);
                object commandI = "";
                if (Program.mainForm.Tokens != "")
                {
                    if (MaxVariationsNumeric.Value > 0)
                    {
                        commandI = commandI + " --max-tokens " + MaxVariationsNumeric.Value;
                    }
                }
                if (DisabledDedupCB.Checked == true)
                {
                    commandI = commandI + " --no-dupchecks";
                }
                if (IgnoreMaximumETACB.Checked == true)
                {
                    commandI = commandI + " --max-eta 8760000000";
                }
                if (EnableTyposSearchCB.Checked && leetSpeakCB.Checked)
                {
                    commandI = commandI + @" --typos-map " + ApplicationDirectory + @"\btcrecover\typos\us-map_leet.txt";
                }
                else
                {
                    if (EnableTyposSearchCB.Checked == true)
                    {
                        commandI = commandI + @" --typos-map " + ApplicationDirectory + @"\btcrecover\typos\us-map.txt";
                    }
                    if (leetSpeakCB.Checked == true)
                    {
                        commandI = commandI + @" --typos-map " + ApplicationDirectory + @"\btcrecover\typos\leet-map.txt";
                    }
                }


                if (PasswordsOrTokens == "Passwords")
                {
                    System.IO.File.WriteAllText(TempDir + "PasswordsOrTokens.tmp", Passwords);
                    commandI = Helpers.GetPythonPath() + @"\python.exe " + ApplicationDirectory + @"btcrecover\btcrecover.py --passwordlist " + Helpers.GetShortName(TempDir + "PasswordsOrTokens.tmp") + " --wallet " + fileShortName + commandI;
                }
                else
                {
                    System.IO.File.WriteAllText(TempDir + "PasswordsOrTokens.tmp", Tokens);
                    commandI = Helpers.GetPythonPath() + @"\python.exe " + ApplicationDirectory + @"btcrecover\btcrecover.py --tokenlist " + Helpers.GetShortName(TempDir + "PasswordsOrTokens.tmp") + " --wallet " + fileShortName + " --autosave " + fileShortName + "-progress.tmp" + commandI;
                }
                if (i == 0)
                {
                    if (Helpers.CommandModified(commandI.ToString()))
                    {
                        backgroundWorkerRecover.CancelAsync();
                        return;
                    }
                }
                else
                {
                    Command = commandI.ToString();
                    btcrecoverInterface.Run(commandI);
                }

                i++;
                Thread.Sleep(2500);
            }
        }

        private void backgroundWorkerRecover_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage != 1000)
            {
                progressBar1.Value = e.ProgressPercentage;
                progressBar1.Update();
                ETALabel.Text = "";
                PasswordsCounter.Text = "";
                CurrentFileLabel.Text = "";
                if (System.IO.File.Exists(TempDir + "CurrentFileNumber.tmp") && System.IO.File.Exists(TempDir + "TotalFileNumber.tmp"))
                {
                    StatusFilesLabel.Text = System.IO.File.ReadAllText(TempDir + "CurrentFileNumber.tmp") + "/" + System.IO.File.ReadAllText(TempDir + "TotalFileNumber.tmp");
                }
                else
                {
                    StatusFilesLabel.Text = "";
                }
                StatusLabel.Text = "preparing file";
            }
            try
            {
                string result = e.UserState.ToString();
                string output = "";
                int indexResultFound = result.IndexOf("Password found: ");
                int indexResultNotFound = result.IndexOf("Password search exhausted");
                int indexNotEncrypted = result.IndexOf("is not encrypted");
                int indexOutOfMemory = result.IndexOf("error: out of memory");
                int indexOverMaxETA = result.IndexOf("--max-eta option");
                int indexCounting = result.IndexOf("Counting passwords");
                //int indexETA = result.IndexOf("Seconds:");
                //int indexPasswords = result.IndexOf("Passwords:");
                bool write = false;
                if (indexResultFound > -1)
                {
                    int indexStart = result.IndexOf("Password found: '") + 17;
                    int indexEnd = result.IndexOf("'", indexStart);
                    string Password = "Password found: " + result.Substring(indexStart, indexEnd - indexStart);
                    output = CurrentFileRecovery + "," + Password;
                    write = true;
                }
                if (indexOutOfMemory > -1)
                {
                    BackgroundWorkerRecoverStop();
                    MessageBox.Show("The process has ran out of memory. Please enable the option \"Disable duplicated passwords check\""
                        + " and run this again. Alternatively, you may run the process in another system or lower the complexity of the passwords list. Depending on what "
                        + "passwords you are checking, you might or might not see a higher completion time with this option enabled");
                }
                if (indexResultNotFound > -1)
                {
                    output = CurrentFileRecovery + "," + "Password not found";
                    write = true;
                }
                if (indexNotEncrypted > -1)
                {
                    output = CurrentFileRecovery + "," + "Wallet is not encrypted";
                    write = true;
                }
                if (indexOverMaxETA > -1)
                {
                    BackgroundWorkerRecoverStop();
                    MessageBox.Show("The estimated time of completion is over " + ETA / 3600 + " hours.\n It is recommended " +
                       "try to generate a more precise password list. If you still would like to continue, please enable the " +
                       "\"Ignore maximum ETA\" option and start again");

                }
                if (indexCounting > -1)
                {
                    //MessageBox.Show(result);
                }
                //if (indexETA > -1)
                //{
                //    output = result;
                //    write = true;
                //}
                //if (indexPasswords > -1)
                //{
                //    output = result;
                //    write = true;
                //}
                if (write == true)
                {
                    Results = Results + "\n" + output;
                }
                //btcrecoverLabel.Text = e.UserState.ToString();
            }
            catch
            { }

        }

        private void backgroundWorkerRecover_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            watcherRestore.EnableRaisingEvents = false;
            watcherTMP.EnableRaisingEvents = false;
            timer1.Enabled = false;
            watcherRestore.EnableRaisingEvents = false;
            watcherTMP.EnableRaisingEvents = false;
            timer1.Enabled = false;
            progressBar1.Value = 100;
            ETALabel.Text = "";
            PasswordsCounter.Text = "";
            StatusFilesLabel.Text = "";
            CurrentFileLabel.Text = "";
            StatusLabel.Text = "";
            AESKey = null;
            try
            {
                Shredding.ShredGutmann(TempDir + "WorkFolder.tmp");
                Shredding.ShredGutmann(TempDir + "TotalFileNumber.tmp");
                Shredding.ShredGutmann(TempDir + "CurrentFileNumber.tmp");
                Shredding.ShredGutmann(TempDir + "ETA.tmp");
                Shredding.ShredGutmann(TempDir + "Count.tmp");
                Shredding.ShredGutmann(TempDir + "UpdatingTotal.tmp");
                Shredding.ShredGutmann(System.IO.Path.GetTempPath() + "PasswordsOrTokens.tmp");
            }
            catch
            { }
            if (e.Cancelled == false)
            {
                MessageBox.Show("Done!");
            }
        }
        public void BackgroundWorkerRecoverStop()
        {
            if (backgroundWorkerRecover.IsBusy)
            {
                backgroundWorkerRecover.CancelAsync();
                DataTable dt = Helpers.GetRunningProcesses();
                foreach (DataRow row in dt.Rows)
                {
                    string CommandLine = row["CommandLine"].ToString();
                    if (CommandLine.Contains("btcrecover") && !CommandLine.Contains("cmd"))
                    {
                        int PID = int.Parse(row["ProcessId"].ToString());
                        Process p = Process.GetProcessById(PID);
                        p.Kill();
                    }
                    progressBar1.Value = 0;
                    ETALabel.Text = "";
                    PasswordsCounter.Text = "";
                    StatusFilesLabel.Text = "";
                    CurrentFileLabel.Text = "";
                    StatusLabel.Text = "cancelling";
                }
            }
        }
        #endregion
        #region Buttons
        private void RestoreWalletFilesButton_Click(object sender, System.EventArgs e)
        {
            DeletedFiles = deletedFilesCB.Checked;
            //if (_cbDrives.SelectedIndex != -1 && RestoreFolder != null)
            if (RestoreFolder == "")
            {
                MessageBox.Show("Please select an empty folder to restore to");
                return;
            }
            if (Helpers.IsDirectoryAccessible(RestoreFolder) == false)
            {
                MessageBox.Show("Cannot access the selected folder, please select another");
                return;
            }
            if (Helpers.IsDirectoryEmpty(RestoreFolder) == false)
            {
                MessageBox.Show("Please select an empty folder to restore to");
                return;
            }
            if ((RestoreFolderDrive == _cbDrives.SelectedDrive) && DeletedFiles == true)
            {
                MessageBox.Show("Please select a different path to restore to, such as a USB drive or another partition. If you restore to the same partition, you risk overwritting deleted files.");
                return;
            }
            if (DeletedFiles == false && existingFilesCB.Checked == false)
            {
                MessageBox.Show("Please select at least one recovery mode");
                return;
            }
            SelectedDrive = _cbDrives.SelectedDrive;
            ProgressLabel.Text = "Restore in progress";
            if (!backgroundWorkerRecover.IsBusy && !backgroundWorkerFinder.IsBusy)
            {
                backgroundWorkerFinder.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("A process is already running, please wait until it's finished");
            }
        }
        private void recoveredFolderButton_Click(object sender, EventArgs e)
        {
            DialogResult result = restoreFolderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                RestoreFolder = Helpers.GetShortName(restoreFolderDialog.SelectedPath);
                if (RestoreFolder.EndsWith(@"\") == false)
                {
                    RestoreFolder = RestoreFolder + @"\";
                }
                RestoreFolderDrive = Path.GetPathRoot(RestoreFolder);
                RecoverFileButton.Enabled = true;
                recoveredFolderButton.Text = Path.GetFullPath(RestoreFolder);
                recoveredFolderButton2.Text = Path.GetFullPath(RestoreFolder);
            }
        }
        private void TokenFileButton_Click(object sender, EventArgs e)
        {
            if (Program.mainForm.RestoreFolder == "")
            {
                MessageBox.Show("Please select the work folder first");
            }
            else
            {
                DialogResult result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    bool AESKeySet = AESKeyEntry();
                    if (AESKeySet == true)
                    {
                        if (Helpers.TokensModified(System.IO.File.ReadAllText(openFileDialog.FileName)))
                        {
                            //PasswordFileButton.Text = Path.GetFullPath(openFileDialog.FileName);
                            string fileRead = System.IO.File.ReadAllText(openFileDialog.FileName);
                            Tokens = fileRead;
                            PasswordsOrTokens = "Tokens";
                            Helpers.CheckForEscapeCharacters(Tokens);
                        }
                    }
                }
            }
        }

        private void EnterTokenListButton_Click(object sender, EventArgs e)
        {
            if (Program.mainForm.RestoreFolder == "")
            {
                MessageBox.Show("Please select the work folder first");
            }
            else
            {
                bool AESKeySet = AESKeyEntry();
                if (AESKeySet == true)
                {
                    Form enterTokenForm = new EnterToken();
                    enterTokenForm.Show();
                }
            }
        }
        private void PasswordFileButton_Click(object sender, EventArgs e)
        {
            if (Program.mainForm.RestoreFolder == "")
            {
                MessageBox.Show("Please select the work folder first");
            }
            else
            {
                DialogResult result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    bool AESKeySet = AESKeyEntry();
                    if (AESKeySet == true)
                    {
                        //PasswordFileButton.Text = Path.GetFullPath(openFileDialog.FileName);
                        string fileRead = System.IO.File.ReadAllText(openFileDialog.FileName);
                        Passwords = fileRead;
                        PasswordsOrTokens = "Passwords";
                    }
                }
            }
        }
        private void EnterPasswordListButton_Click(object sender, EventArgs e)
        {
            if (Program.mainForm.RestoreFolder == "")
            {
                MessageBox.Show("Please select the work folder first");
            }
            else
            {
                Form enterPasswordsForm = new EnterPasswords();
                enterPasswordsForm.Show();
            }
        }
        private void StartWalletPasswordRecovery_Click(object sender, EventArgs e)
        {
            bool AESKeySet = AESKeyEntry();
            if (AESKeySet == true)
            {
                bool error = false;
                ETA = 0;
                TotalPasswords = 0;
                this.watcherTMP.Path = TempDir;
                this.watcherTMP.EnableRaisingEvents = true;
                this.watcherRestore.Path = RestoreFolder;
                this.watcherRestore.EnableRaisingEvents = true;
                if (PasswordsOrTokens == "" || PasswordsOrTokens == null)
                {
                    MessageBox.Show("You must select if you want to work with complete passwords or tokens");
                    error = true;
                }
                if (RestoreFolder == "")
                {
                    MessageBox.Show("Please select the work folder");
                    error = true;
                }
                if (Helpers.IsDirectoryAccessible(RestoreFolder) == false)
                {
                    MessageBox.Show("Cannot access the selected folder, please select another");
                    return;
                }
                if (PasswordsOrTokens == "Tokens")
                {
                    int tokenListLines = Helpers.CountLinesInString(Tokens);
                    if (MaxVariationsNumeric.Value > tokenListLines)
                    {
                        DialogResult result = MessageBox.Show("The number of elements in the password parts file is "
                            + tokenListLines + " and you've selected to use " + MaxVariationsNumeric.Value +
                            ". Would you like to use " + tokenListLines + " as the maximum number?", "Warning!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);
                        if (result.ToString() == "Yes")
                        {
                            MaxVariationsNumeric.Value = tokenListLines;
                        }
                        else
                        {
                            error = true;
                        }
                    }
                }
                if (error == false)
                {
                    if (!backgroundWorkerRecover.IsBusy && !backgroundWorkerFinder.IsBusy)
                    {
                        if (WalletFileToRecover == null)
                        {
                            backgroundWorkerRecover.RunWorkerAsync("folder");
                        }
                        else
                        {
                            backgroundWorkerRecover.RunWorkerAsync(WalletFileToRecover);
                        }
                    }
                    else
                    {
                        MessageBox.Show("A process is already running, please wait until it's finished");
                    }
                }
            }
        }
        private void CancelWalletPasswordRecovery_Click(object sender, EventArgs e)
        {
            BackgroundWorkerRecoverStop();
        }
        private void CancelRestoreWalletFilesButton_Click(object sender, EventArgs e)
        {

            if (backgroundWorkerFinder.IsBusy)
            {
                ProgressLabel.Text = "Please hold... waiting for background process to finish";
                backgroundWorkerFinder.CancelAsync();
            }
        }
        private void DeleteFilesWorkFolderButton_Click(object sender, EventArgs e)
        {
            if (RestoreFolder == "")
            {
                MessageBox.Show("Please select the work folder first");
                return;
            }
            DialogResult result = MessageBox.Show("This will delete all files from the work folder "
                + RestoreFolder + " and its subdirectories\nAre you sure?", "Warning!",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Warning,
    MessageBoxDefaultButton.Button2);
            if (result.ToString() == "Yes")
            {
                //System.IO.DirectoryInfo di = new DirectoryInfo(RestoreFolder);
                //foreach (FileInfo file in di.GetFiles())
                //{
                //    Shredding.ShredGutmann(file.FullName);
                //}
                //foreach (DirectoryInfo dir in di.GetDirectories())
                //{
                //    dir.Delete(true);
                //}
                IEnumerable<string> IEnumerableList;
                List<string> filesList;
                System.IO.DirectoryInfo di = new DirectoryInfo(RestoreFolder);
                IEnumerableList = Directory.EnumerateFiles(RestoreFolder, "*", SearchOption.AllDirectories);
                filesList = IEnumerableList.Cast<string>().ToList();

                foreach (string file in filesList)
                {
                    Shredding.ShredGutmann(file);
                }
                Thread.Sleep(1000);
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    try
                    {
                        dir.Delete(true);
                    }
                    catch { }
                }
            }

        }
        #endregion
        public static void RestoreFiles(string dev, string restoreFolder)
        {
            if (dev.EndsWith(@"\") == true)
            {
                dev = dev.Substring(0, dev.Length - 1);
            }
            var volumes = DiskLoader.LoadLogicalVolumes();
            var volume = volumes.FirstOrDefault(x => x.ToString().Contains(dev));
            if (volume == null)
            {
                MessageBox.Show("Disk not found: " + dev);
                return;
            }
            dev = volume.ToString();

            var fs = ((IFileSystemStore)volume).FS;
            if (fs == null)
            {
                MessageBox.Show("Disk " + dev + " contains no readable FS.");
                return;
            }

            //Console.Error.WriteLine("Deleted files on " + dev);
            //Console.Error.WriteLine("=================" + new String('=', dev.Length));
            var scanner = new KickassUndelete.Scanner(dev, fs, 786432);
            scanner.ScanFinished += new EventHandler(ScanFinished);
            scanner.StartScan();
            while (!scan_finished)
            {
                Thread.Sleep(100);
            }
            var files = scanner.GetDeletedFiles();
            foreach (var file in files)
            {
                var node = file.GetFileSystemNode();
                var data = node.GetBytes(0, node.StreamLength);
                //TextWriter output = new StreamWriter(restoreFolder + file.Name);
                using (BinaryWriter b = new BinaryWriter(
                  System.IO.File.Open(restoreFolder + file.Name, FileMode.Create)))
                {
                    b.Write(data);
                    //output.Write(data, 0, data.Length);
                }

                //TextWriter tw2 = new StreamWriter(restoreFolder + file.Name);
                //tw2.WriteLine(BitConverter.ToString(data));
                //tw2.Close();
            }
        }
        private void SelectWalletFile(object sender, EventArgs e)
        {
            if (Program.mainForm.RestoreFolder == "")
            {
                MessageBox.Show("Please select the work folder first");
            }
            else
            {
                openFileDialog.InitialDirectory = RestoreFolder;
                DialogResult result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    WalletFileToRecover = openFileDialog.FileName;
                    RecoverFileButton.Text = Path.GetFullPath(openFileDialog.FileName);
                }
            }
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            string fileText = "";
            try
            {
                fileText = System.IO.File.ReadAllText(e.FullPath);
            }
            catch { }
            try
            {
                if (e.FullPath.Contains("ETA"))
                {
                    ETA = int.Parse(fileText);
                    StatusLabel.Text = "working";
                }
                if (e.FullPath.Contains("Count"))
                {
                    TotalPasswords = int.Parse(fileText);
                }
                if (e.FullPath.Contains("UpdatingTotal"))
                {
                    UpdatingTotal = fileText;
                    UpdatingTotal = UpdatingTotal.Replace("\r", "");
                    StatusLabel.Text = "Passwords:" + UpdatingTotal;
                }
            }
            catch { }
            if (ETA != 0 && TotalPasswords != 0)
            {
                timer1.Enabled = true;
                //PasswordsPerSecond = (TotalPasswords / ETA);
            }
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            if (ETA > 0 && backgroundWorkerRecover.IsBusy)
            {
                ETA = ETA - 1;
                TimeSpan span = TimeSpan.FromSeconds(ETA);
                ETALabel.Text = span.ToString(@"hh\:mm\:ss");
            }
            if (TotalPasswords > 0)
            {
                if (CurrentPasswordNumber == 0)
                {
                    CurrentPasswordNumber = 1;
                }
                if (ETA == 0 && backgroundWorkerRecover.IsBusy == false)
                {
                    ETALabel.Text = "Processing...";
                }
                else
                {
                    if (ETA != 0)
                    {
                        //CurrentPasswordNumber = CurrentPasswordNumber + PasswordsPerSecond;
                        string formattedTotalPasswords = TotalPasswords.ToString("N0");
                        PasswordsCounter.Text = formattedTotalPasswords;
                    }
                    else
                    {
                        PasswordsCounter.Text = "";
                    }
                }
            }
        }

        private void watcherRestore_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.FullPath.Contains(@"-progress.tmp"))
            {
                StringBuilder longPath = new StringBuilder(255);
                CurrentFileLabel.Text = Path.GetFullPath(e.FullPath.Remove(e.FullPath.Length - 13));
            }
        }
        private void radioButtonPasswords_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonPasswords.Checked == true)
            {
                TokenFileButton.Enabled = false;
                EnterTokenListButton.Enabled = false;
                MaxVariationsNumeric.Enabled = false;
                PasswordFileButton.Enabled = true;
                EnterPasswordListButton.Enabled = true;
                PasswordsOrTokens = "Passwords";
            }
            if (radioButtonTokens.Checked == true)
            {
                PasswordFileButton.Enabled = false;
                EnterPasswordListButton.Enabled = false;
                MaxVariationsNumeric.Enabled = true;
                TokenFileButton.Enabled = true;
                EnterTokenListButton.Enabled = true;
                PasswordsOrTokens = "Tokens";
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            config = new Config();
            config.cfgFile = "App.config";
            Salt = config.GetValue("//appSettings//add[@key='Salt']");
            if (Salt == "")
            {
                Salt = Crypto.GenerateSimpleSalt();
                config.SetValue("//appSettings//add[@key='" + "Salt" + "']", Salt);
            }
            if (RestoreFolder != "")
            {
                recoveredFolderButton.Text = RestoreFolder;
                recoveredFolderButton2.Text = RestoreFolder;
                RestoreFolderDrive = Path.GetPathRoot(RestoreFolder);
                RecoverFileButton.Enabled = true;
            }
        }
        public bool AESKeyEntry()
        {
            if (AESKey != null)
            {
                return true;
            }
            else
            {
                string PasswordVerificationString = config.GetValue("//appSettings//add[@key='PasswordVerificationString']");
                if (PasswordVerificationString == "")
                {
                    Form enterAESKeyNew = new EnterAESKeyNew();
                    enterAESKeyNew.ShowDialog();
                }
                else
                {
                    Form enterAESKeyToVerify = new EnterAESKeyToVerify();
                    enterAESKeyToVerify.ShowDialog();
                }
                if (AESKey != null)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("This operation requires the password to be set");
                    return false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("This will delete results, passwords and other configurations. Continue?", "Warning!",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Warning,
    MessageBoxDefaultButton.Button2);
            if (result.ToString() == "Yes")
            {
                config.SetValue("//appSettings//add[@key='" + "LastCommand" + "']", "");
                config.SetValue("//appSettings//add[@key='" + "LastPasswordsOrTokens" + "']", "");
                config.SetValue("//appSettings//add[@key='" + "LastUsedCodes" + "']", "");
                config.SetValue("//appSettings//add[@key='" + "Passwords" + "']", "");
                config.SetValue("//appSettings//add[@key='" + "PasswordVerificationString" + "']", "");
                config.SetValue("//appSettings//add[@key='" + "RestoreFolder" + "']", "");
                recoveredFolderButton.Text = "Select work folder";
                recoveredFolderButton2.Text = "Select work folder";
                RestoreFolder = "";
                config.SetValue("//appSettings//add[@key='" + "Results" + "']", "");
                config.SetValue("//appSettings//add[@key='" + "Tokens" + "']", "");
                AESKey = null;
            }
        }

        private void ShowResultsButton_Click(object sender, EventArgs e)
        {
            bool AESKeySet = AESKeyEntry();
            if (AESKeySet == true)
            {
                if (Results != "")
                {
                    Form showResults = new ShowResults();
                    showResults.ShowDialog();
                }
                else
                {
                    MessageBox.Show("There are no stored results");
                }
            }
        }

        private void HelpCreatingListsLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Alex-Jaeger/BitcoinFindAndRecover/blob/master/Tokens.txt");
        }


        private void GeneralSupportIssue_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("mailto:jaeger-alexander@gmx.net");
            }
            catch
            {
                MessageBox.Show("Contact the developer: jaeger-alexander@gmx.net");
            }
        }

        private void ProjectsPageLabel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://alex-jaeger.github.io/BitcoinFindAndRecover");
        }
    }
}
