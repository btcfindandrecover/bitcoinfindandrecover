using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.Management;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace CryptoFinder
{
    class Helpers
    {

        [DllImport("kernel32", EntryPoint = "GetShortPathName", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetShortPathName(string longPath, StringBuilder shortPath, int bufSize);
        public static class Algorithms
        {
            public static readonly HashAlgorithm MD5 = new MD5CryptoServiceProvider();
            public static readonly HashAlgorithm SHA1 = new SHA1Managed();
            public static readonly HashAlgorithm SHA256 = new SHA256Managed();
            public static readonly HashAlgorithm SHA384 = new SHA384Managed();
            public static readonly HashAlgorithm SHA512 = new SHA512Managed();
            public static readonly HashAlgorithm RIPEMD160 = new RIPEMD160Managed();
        }

        public static string GetChecksum(string filePath, HashAlgorithm algorithm)
        {
            using (var stream = new BufferedStream(System.IO.File.OpenRead(filePath), 100000))
            {
                byte[] hash = algorithm.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }
        public static bool TokensModified(string CurrentTokens)
        {
            bool result = false;
            bool delete = false;
            IEnumerable<string> IEnumerableList;
            IEnumerableList = Directory.EnumerateFiles(Program.mainForm.RestoreFolder, "*progress.tmp", SearchOption.AllDirectories);
            List<string> filesList = IEnumerableList.Cast<string>().ToList();

            if (filesList.Count > 0 && CurrentTokens != Program.mainForm.Tokens)
            {
                DialogResult dialogResult = MessageBox.Show("You have previously started a job with "
    + "a different set of passwords to test. If you continue, progress will be lost.\n" +
    "Are you sure you want to continue?", "Warning!", MessageBoxButtons.YesNo,
    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult.ToString() == "Yes")
                {
                    delete = true;
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            if (delete == true)
            {
                for (int i = filesList.Count - 1; i >= 0; i--)
                {
                    Shredding.ShredGutmann(filesList[i]);
                }
            }

            return result;
        }

        public static bool CommandModified(string CurrentCommand)
        {
            bool result = false;
            bool delete = false;
            IEnumerable<string> IEnumerableList;
            IEnumerableList = Directory.EnumerateFiles(Program.mainForm.RestoreFolder, "*progress.tmp", SearchOption.AllDirectories);
            List<string> filesList = IEnumerableList.Cast<string>().ToList();

            if (filesList.Count > 0 && (CurrentCommand != Program.mainForm.Command))
            {
                DialogResult dialogResult = MessageBox.Show("You have previously started a job with "
    + "different options. If you continue, progress will be lost.\n" +
    "Are you sure you want to continue?", "Warning!", MessageBoxButtons.YesNo,
    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (dialogResult.ToString() == "Yes")
                {
                    delete = true;
                }
                else
                {
                    result = true;
                }

                if (delete == true)
                {
                    for (int i = filesList.Count - 1; i >= 0; i--)
                    {
                        Shredding.ShredGutmann(filesList[i]);
                    }
                }
            }
            return result;
        }

        public static DataTable GetRunningProcesses()
        {
            //One way of constructing a query
            string wmiClass = "Win32_Process";
            string condition = "";
            string[] queryProperties = new string[] { "Name", "ProcessId", "Caption", "ExecutablePath", "CommandLine" };
            SelectQuery wmiQuery = new SelectQuery(wmiClass, condition, queryProperties);
            ManagementScope scope = new System.Management.ManagementScope(@"\\.\root\CIMV2");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, wmiQuery);
            ManagementObjectCollection runningProcesses = searcher.Get();

            DataTable queryResults = new DataTable();
            queryResults.Columns.Add("Name", Type.GetType("System.String"));
            queryResults.Columns.Add("ProcessId", Type.GetType("System.Int32"));
            queryResults.Columns.Add("Caption", Type.GetType("System.String"));
            queryResults.Columns.Add("Path", Type.GetType("System.String"));
            queryResults.Columns.Add("CommandLine", Type.GetType("System.String"));

            foreach (ManagementObject obj in runningProcesses)
            {
                DataRow row = queryResults.NewRow();
                row["Name"] = obj["Name"].ToString();
                row["ProcessId"] = Convert.ToInt32(obj["ProcessId"]);
                if (obj["Caption"] != null)
                    row["Caption"] = obj["Caption"].ToString();
                if (obj["ExecutablePath"] != null)
                    row["Path"] = obj["ExecutablePath"].ToString();
                if (obj["CommandLine"] != null)
                    row["CommandLine"] = obj["CommandLine"].ToString();
                queryResults.Rows.Add(row);
            }
            return queryResults;
        }
        public static string GetShortName(string sLongFileName)
        {
            var buffer = new StringBuilder(259);
            int len = GetShortPathName(sLongFileName, buffer, buffer.Capacity);
            if (len == 0) return sLongFileName;
            return buffer.ToString();
        }
        public static bool IsDirectoryAccessible(string path)
        {
            bool result;
            try
            {
                string[] dirs = System.IO.Directory.GetDirectories(path); string[] files = System.IO.Directory.GetFiles(path);
                result = true;
            }
            catch
            { result = false; }
            return result;
        }
        public static bool IsDirectoryEmpty(string path)
        {
            string[] dirs = System.IO.Directory.GetDirectories(path); string[] files = System.IO.Directory.GetFiles(path);
            return dirs.Length == 0 && files.Length == 0;
        }
        public static int CountLinesInFile(string f)
        {
            int count = 0;
            using (StreamReader r = new StreamReader(f))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    count++;
                }
            }
            return count;
        }
        public static int CountLinesInString(string s)
        {
            int n = 0;
            foreach (var c in s)
            {
                if (c == '\n') n++;
            }
            return n + 1;
        }
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        public static string InsertStringAtInterval(string source, string insert, int interval)
        {
            StringBuilder result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + interval < source.Length)
            {
                result.Append(source.Substring(currentPosition, interval)).Append(insert);
                currentPosition += interval;
            }
            if (currentPosition < source.Length)
            {
                result.Append(source.Substring(currentPosition));
            }
            return result.ToString();
        }
        public static bool CheckForEscapeCharacters(string Original)
        {
            bool result = false;
            string notifications = Program.mainForm.config.GetValue("//appSettings//add[@key='StopTokenNotifications']");
            if (notifications != "true")
            {
                if (Original.Contains("%") || Original.Contains("^") || Original.Contains("$"))
                {
                    DialogResult resultDialog = MessageBox.Show("You have entered a line with a character \"%\", \"^\" or \"$\".\n "
                        + "If it is part of your password and not an escape character to represent a wildcard character, such as \"%d\", you will need to replace it so that the software will process it properly.\n The replacement are as follows, respectively: \"%%\", \"%^\" and \"%S\".\nWould you like to stop being notified?", "Warning!",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
                    if (resultDialog.ToString() == "Yes")
                    {
                        Program.mainForm.config.SetValue("//appSettings//add[@key='" + "StopTokenNotifications" + "']", "true");

                    }
                    else
                    {
                    }
                }
            }
            return result;
        }
        //public static void PrepareDgv(string csvPath)
        //{
        //    string[,] values = Helpers.LoadCsv(csvPath, true);
        //    int num_rows = values.GetUpperBound(0) + 1;
        //    int num_cols = values.GetUpperBound(1) + 1;
        //
        //    // Display the data to show we have it.
        //
        //    // Make column headers.
        //    // For this example, we assume the first row
        //    // contains the column names.
        //    dgvValues.Columns.Clear();
        //    for (int c = 0; c < num_cols; c++)
        //        dgvValues.Columns.Add(values[0, c], values[0, c]);
        //
        //    // Add the data.
        //    for (int r = 1; r < num_rows; r++)
        //    {
        //        dgvValues.Rows.Add();
        //        for (int c = 0; c < num_cols; c++)
        //        {
        //            dgvValues.Rows[r - 1].Cells[c].Value = values[r, c];
        //        }
        //    }
        //
        //    //// Make the columns autosize.
        //    //foreach (DataGridViewColumn col in dgvValues.Columns)
        //    //    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        //}
        public static string[,] LoadCsv(string filename, bool split)
        {
            // Get the file's text.
            string whole_file = filename;

            // Split into lines.
            whole_file = whole_file.Replace('\n', '\r');
            string[] lines = whole_file.Split(new char[] { '\r' },
                StringSplitOptions.RemoveEmptyEntries);

            // See how many rows and columns there are.
            int num_rows = lines.Length;
            int num_cols = 1;
            if (split == true)
            {
                num_cols = lines[0].Split(',').Length;
            }

            // Allocate the data array.
            string[,] values = new string[num_rows, num_cols];

            // Load the array.
            for (int r = 0; r < num_rows; r++)
            {
                if (split == true)
                {
                    string[] line_r = lines[r].Split(',');
                    for (int c = 0; c < num_cols; c++)
                    {
                        values[r, c] = line_r[c];
                    }
                }
                else
                {
                    string line_r = lines[r];
                    for (int c = 0; c < num_cols; c++)
                    {
                        values[r, c] = line_r;
                    }
                }
            }

            // Return the values.
            return values;
        }
        public static string GetPythonPath()
        {
            var localmachineKey = Registry.LocalMachine;
            // Check whether we are on a 64-bit OS by checking for the Wow6432Node key (32-bit version of the Software registry key)
            var softwareKey = localmachineKey.OpenSubKey(@"SOFTWARE\Wow6432Node\Python\PythonCore\2.7\InstallPath"); // This is the correct key for 64-bit OS's
            if (softwareKey == null)
            {
                softwareKey = localmachineKey.OpenSubKey(@"SOFTWARE\Python\PythonCore\2.7\InstallPath"); // This is the correct key for 32-bit OS's

            }
            string pathName = (string)softwareKey.GetValue("");
            return pathName;
        }
    }
}