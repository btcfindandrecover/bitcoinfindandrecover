using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CryptoFinder
{
    static class btcrecoverInterface
    {
        //static public string ExecuteCommandSync(object command, string resultFound, string //resultNotFound)
        //{
        //    string returnString = "error";
        //    try
        //    {
        //        System.Diagnostics.ProcessStartInfo procStartInfo =
        //new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);
        //        procStartInfo.RedirectStandardOutput = true;
        //        procStartInfo.UseShellExecute = false;
        //        procStartInfo.CreateNoWindow = true;
        //        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        //        proc.StartInfo = procStartInfo;
        //        proc.Start();
        //
        //        string result = proc.StandardOutput.ReadToEnd();
        //        int indexResultFound = result.IndexOf(resultFound);
        //        int indexResultNotFound = result.IndexOf(resultNotFound);
        //        if (indexResultFound > -1)
        //        {
        //            int indexStart = result.IndexOf("Password found: '") + 17;
        //            int indexEnd = result.IndexOf("'", indexStart);
        //            string Password = "Password found: " + result.Substring(indexStart, indexEnd - //indexStart);
        //            returnString = Password;
        //        }
        //        if (indexResultNotFound > -1)
        //        {
        //            returnString = "resultNotFound";
        //        }
        //        if (indexResultFound == -1 && indexResultNotFound == -1)
        //        {
        //            returnString = "error";
        //        }
        //        //proc.Kill();
        //    }
        //    catch (Exception objException)
        //    {
        //    }
        //    return returnString;
        //}
        public static void Run(object cmdPath)
        {
            var proc = new Process();
            proc.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
            proc.StartInfo.Arguments = @" /c " + cmdPath.ToString();
            //System.IO.File.WriteAllText("command.txt", proc.StartInfo.FileName + proc.StartInfo.Arguments);

            // set up output redirection
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.EnableRaisingEvents = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            // see below for output handler
            proc.ErrorDataReceived += proc_DataReceived;
            proc.OutputDataReceived += proc_DataReceived;

            proc.Start();

            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            proc.WaitForExit();
        }
        public static void proc_DataReceived(object sender, DataReceivedEventArgs e)
        {
            //MessageBox.Show(e.Data);
            if (e.Data != null)
            {
                int indexResultFound = e.Data.IndexOf("Password found: ");
                int indexResultNotFound = e.Data.IndexOf("Password search exhausted");
                int indexNotEncrypted = e.Data.IndexOf("is not encrypted");
                int indexOutOfMemory = e.Data.IndexOf("error: out of memory");
                int indexOverMaxETA = e.Data.IndexOf("--max-eta option");
                int indexCounting = e.Data.IndexOf("Counting passwords");
                if (indexResultFound > -1 || indexResultNotFound > -1 || indexNotEncrypted > -1 || indexOutOfMemory > -1 || indexOverMaxETA > -1 || indexCounting > -1)
                {
                    Program.mainForm.backgroundWorkerRecover.ReportProgress(1000, e.Data);
                }
            }
        }
    }

}
