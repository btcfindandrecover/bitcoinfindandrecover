using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;

namespace CryptoFinder
{
    public class IDCryptoFiles
    {
        public bool Armory { get; set; }
        public bool BitcoinQT { get; set; }
        public bool Bither { get; set; }
        public bool Copay { get; set; }
        public bool DeletedFiles { get; set; }
        public bool DeepScan { get; set; }
        public bool Electrum { get; set; }
        public long MaxFileSize { get; set; }
        public bool mSIGNA { get; set; }
        public bool Multibit { get; set; }
        public bool MultibitHD { get; set; }
        public int progress { get; set; }
        public string RestoreFolder { get; set; }
        public bool WalletFilesLister(string path)
        {
            List<DirectoryInfo> RecursiveRemovalsDirsList = new List<DirectoryInfo>() { new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)), new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Windows)) };
            List<string> RecursiveRemovalsStringsList = new List<string>() { ".iso", ".jpg", ".mp3", ".cab", ".avi", ".gif", ".png", ".bmp", ".mp4", ".aac", ".flac", ".wav", ".divx", ".mov", ".mpg", ".mpeg", ".vmw", ".msi", ".exe", ".dll" };
            List<string> RecursiveSizeMaxExceptions = new List<string>() { "wallet", @"copay\Local Storage", "mbhd", "bither", ".vault" };
            List<string> ContentStringListArmory = new List<string>() { "WALLET\0" };
            List<string> ContentStringListBitcoinQT = new List<string>() { "keymeta!" };
            List<string> ContentStringListBither = new List<string>() { "hd_account_addresses" };
            List<string> ContentStringListElectrum = new List<string>() { "master_private_keys" };
            List<string> ContentStringListmSIGNA = new List<string>() { "privkey_ciphertext" };
            List<string> ContentStringListMultibit = new List<string>() { "org.bitcoin.production" };
            //List<string> FileNameBitcoinQT = new List<string>() { "wallet.dat" };
            //List<string> FileNameListElectrum = new List<string>() { "_wallet" };
            //List<string> FileNameListMultibit = new List<string>() { ".wallet" };
            List<string> FullPathRegexListMultibitHD = new List<string>() { "(mbhd-)([0-9a-fA-F]{8}(-)){4}[0-9a-fA-F]{8}" };
            List<string> FullPathStringListCopay = new List<string>() { @"copay\Local Storage" };
            List<string> recursiveFileList = new List<string>();
            recursiveFileList = GetFiles(path, "*.*");
            recursiveFileList = RemovalFromList(recursiveFileList, RecursiveRemovalsDirsList, RecursiveRemovalsStringsList);
            recursiveFileList = RestrictFileSize(recursiveFileList, MaxFileSize, RecursiveSizeMaxExceptions);
            int filesCount = recursiveFileList.Count;
            BackgroundWorkerProgress("Searching " + filesCount + " files for Armory wallets...");
            bool canceled;
            canceled = CancelRequested();
            if (
                canceled == true)
            {
                return false;
            }
            if (Armory == true)
            {
                List<string> list = new List<string>();
                list = StringContentList(recursiveFileList, ContentStringListArmory);
                List<string> listNoDuplicated = list.Distinct().ToList();
                if (DeletedFiles == true)
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopier(listNoDuplicated, RestoreFolder + @"Armory\Restored\");
                    }
                }
                else
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopierSubDirs(listNoDuplicated, RestoreFolder + @"Armory\");
                    }
                }
            }
            BackgroundWorkerProgress("Searching " + filesCount + " files for BitcoinQT wallets...");
            canceled = CancelRequested();
            if (canceled == true)
            { return false; }
            if (BitcoinQT == true)
            {
                List<string> list = new List<string>();
                list = StringContentList(recursiveFileList, ContentStringListBitcoinQT);
                List<string> listNoDuplicated = list.Distinct().ToList();
                if (DeletedFiles == true)
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopier(listNoDuplicated, RestoreFolder + @"BitcoinQT\Restored\");
                    }
                }
                else
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopierSubDirs(listNoDuplicated, RestoreFolder + @"BitcoinQT\");
                    }
                }
            }
            BackgroundWorkerProgress("Searching " + filesCount + " files for Bither wallets...");
            canceled = CancelRequested();
            if (canceled == true)
            { return false; }
            if (Bither == true)
            {
                List<string> list = new List<string>();
                list = StringContentList(recursiveFileList, ContentStringListBither);
                List<string> listNoDuplicated = list.Distinct().ToList();
                if (DeletedFiles == true)
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopier(listNoDuplicated, RestoreFolder + @"Bither\Restored\");
                    }
                }
                else
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopierSubDirs(listNoDuplicated, RestoreFolder + @"Bither\");
                    }
                }
            }
            BackgroundWorkerProgress("Searching " + filesCount + " files for Copay wallets...");
            canceled = CancelRequested();
            if (canceled == true)
            { return false; }
            if (Copay == true)
            {
                List<string> list = new List<string>();
                list = PathStringList(recursiveFileList, FullPathStringListCopay);
                List<string> listNoDuplicated = list.Distinct().ToList();
                if (DeletedFiles == true)
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopier(listNoDuplicated, RestoreFolder + @"Copay\Restored\");
                    }
                }
                else
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopierSubDirs(listNoDuplicated, RestoreFolder + @"Copay\");
                    }
                }
            }
            BackgroundWorkerProgress("Searching " + filesCount + " files for Electrum wallets...");
            canceled = CancelRequested();
            if (canceled == true)
            { return false; }
            if (Electrum == true)
            {
                List<string> list = new List<string>();
                list = StringContentList(recursiveFileList, ContentStringListElectrum);
                List<string> listNoDuplicated = list.Distinct().ToList();
                if (DeletedFiles == true)
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopier(listNoDuplicated, RestoreFolder + @"Electrum\Restored\");
                    }
                }
                else
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopierSubDirs(listNoDuplicated, RestoreFolder + @"Electrum\");
                    }
                }
            }
            BackgroundWorkerProgress("Searching " + filesCount + " files for mSIGNA wallets...");
            canceled = CancelRequested();
            if (canceled == true)
            { return false; }
            if (mSIGNA == true)
            {
                List<string> list = new List<string>();
                list = StringContentList(recursiveFileList, ContentStringListmSIGNA);
                List<string> listNoDuplicated = list.Distinct().ToList();
                if (DeletedFiles == true)
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopier(listNoDuplicated, RestoreFolder + @"mSGINA\Restored\");
                    }
                }
                else
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopierSubDirs(listNoDuplicated, RestoreFolder + @"mSGINA\");
                    }
                }
            }
            BackgroundWorkerProgress("Searching " + filesCount + " files for Multibit wallets...");
            canceled = CancelRequested();
            if (canceled == true)
            { return false; }
            if (Multibit == true)
            {
                List<string> list = new List<string>();
                list = StringContentList(recursiveFileList, ContentStringListMultibit);
                List<string> listNoDuplicated = list.Distinct().ToList();
                if (DeletedFiles == true)
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopier(listNoDuplicated, RestoreFolder + @"Multibit\Restored\");
                    }
                }
                else
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopierSubDirs(listNoDuplicated, RestoreFolder + @"Multibit\");
                    }
                }
            }
            BackgroundWorkerProgress("Searching " + filesCount + " files for MultibitHD wallets...");
            canceled = CancelRequested();
            if (canceled == true)
            { return false; }
            if (MultibitHD == true)
            {
                List<string> list = new List<string>();
                list = RegexNameList(recursiveFileList, FullPathRegexListMultibitHD);
                List<string> listNoDuplicated = list.Distinct().ToList();
                if (DeletedFiles == true)
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopierMBHD(listNoDuplicated, RestoreFolder + @"MultibitHD\Restored\");
                    }
                }
                else
                {
                    if (listNoDuplicated.Count != 0)
                    {
                        ListCopierSubDirs(listNoDuplicated, RestoreFolder + @"MultibitHD\");
                    }
                }
            }
            BackgroundWorkerProgress("Search finished");
            return true;
        }
        static private List<string> GetFiles(string path, string pattern)
        {
            var files = new List<string>();
            try
            {
                files.AddRange(Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));
                foreach (var directory in Directory.GetDirectories(path))
                {
                    files.AddRange(GetFiles(directory, pattern));
                }
            }
            catch
            {
            }
            return files;
        }
        public List<string> RemovalFromList(List<string> recursiveFileList, List<DirectoryInfo> RecursiveRemovalsDirsList, List<string> RecursiveRemovalsStringsList)
        {
            List<string> list = new List<string>();
            foreach (string s in recursiveFileList)
            {
                bool match = false;
                foreach (DirectoryInfo r in RecursiveRemovalsDirsList)
                {
                    if (s.Contains(r.FullName) == true)
                    {
                        match = true;
                        break;
                    }
                }
                foreach (string t in RecursiveRemovalsStringsList)
                {
                    if (s.Contains(t))
                    {
                        match = true;
                        break;
                    }
                }
                if (match == false)
                {
                    list.Add(s);
                }
            }
            return list;
        }
        public List<string> RestrictFileSize(List<string> list, long MaxFileSize, List<string> RecursiveSizeMaxExceptions)
        {
            foreach (string l in list)
            {
                bool match = false;
                foreach (string s in RecursiveSizeMaxExceptions)
                {
                    if (l.Contains(s) == true)
                    {
                        match = true;
                        break;
                    }
                    if (match == true)
                    {
                        try
                        {
                            FileInfo f = new FileInfo(l);
                            long size = f.Length;
                            if (size > MaxFileSize)
                            {
                                list.Remove(l);
                            }
                        }
                        catch { }
                    }

                }
            }
            return list;
        }
        private string RandomHex(int lenght)
        {
            string allowedChars = "abcdef123456789";
            char[] chars = null;
            chars = new char[lenght]; Random rd = new Random();

            for (int i = 0; i < lenght; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
        public List<string> FileStringList(List<string> recursiveFileList, List<string> inputList)
        {
            List<string> list;
            list = new List<string>();
            int loopCounter = 0;
            foreach (string s in recursiveFileList)
            {
                FileInfo f = new FileInfo(s);
                foreach (string i in inputList)
                {
                    if (f.Name.Contains(i))
                    {
                        list.Add(f.FullName);
                    }

                }
                loopCounter++;
                if (loopCounter > 500)
                {
                    bool canceled = CancelRequested();
                    if (canceled == true)
                    {
                        return list;
                    }
                    else
                    {
                        loopCounter = 0;
                    }
                }
            }
            return list;
        }
        public List<string> PathStringList(List<string> recursiveFileList, List<string> inputList)
        {
            List<string> list;
            list = new List<string>();
            int loopCounter = 0;
            foreach (string s in recursiveFileList)
            {
                FileInfo f = new FileInfo(s);
                foreach (string i in inputList)
                {
                    if (f.FullName.Contains(i))
                    {
                        List<string> FullFolderScanList = new List<string>();
                        FullFolderScanList = GetFiles(f.DirectoryName, "*.*");
                        foreach (string ff in FullFolderScanList)
                        {
                            list.Add(ff);
                        }
                    }
                }
                loopCounter++;
                if (loopCounter > 500)
                {
                    bool canceled = CancelRequested();
                    if (canceled == true)
                    {
                        return list;
                    }
                    else
                    {
                        loopCounter = 0;
                    }
                }
            }
            return list;
        }
        public List<string> RegexNameList(List<string> recursiveFileList, List<string> inputList)
        {
            List<string> list;
            list = new List<string>();
            int loopCounter = 0;
            foreach (string s in recursiveFileList)
            {
                try
                {
                    FileInfo f = new FileInfo(s);
                    foreach (string i in inputList)
                    {
                        try
                        {
                            Regex regex = new Regex(i);
                            Match match = regex.Match(f.FullName);
                            if (match.Success)
                            {
                                List<string> FullFolderScanList = new List<string>();
                                FullFolderScanList = GetFiles(f.DirectoryName, "*.*");
                                foreach (string ff in FullFolderScanList)
                                {
                                    list.Add(ff);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                loopCounter++;
                if (loopCounter > 500)
                {
                    bool canceled = CancelRequested();
                    if (canceled == true)
                    {
                        return list;
                    }
                    else
                    {
                        loopCounter = 0;
                    }
                }
            }
            return list;
        }
        public List<string> StringContentList(List<string> recursiveFileList, List<string> inputList)
        {
            List<string> list;
            list = new List<string>();
            int loopCounter = 0;
            foreach (string s in recursiveFileList)
            {
                try
                {
                    FileInfo f = new FileInfo(s);
                    foreach (string i in inputList)
                    {
                        try
                        {
                            string fileContent = null;
                            fileContent = File.ReadAllText(f.FullName);
                            if (fileContent.Contains(i))
                            {
                                list.Add(f.FullName);
                            }
                        }
                        catch { }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                loopCounter++;
                if (loopCounter > 500)
                {
                    bool canceled = CancelRequested();
                    if (canceled == true)
                    {
                        return list;
                    }
                    else
                    {
                        loopCounter = 0;
                    }
                }
            }
            return list;
        }
        public List<string> RegexContentList(List<string> recursiveFileList, List<string> inputList)
        {
            List<string> list;
            list = new List<string>();
            int loopCounter = 0;
            foreach (string s in recursiveFileList)
            {
                try
                {
                    FileInfo f = new FileInfo(s);
                    foreach (string i in inputList)
                    {
                        try
                        {
                            string fileContent = null;
                            fileContent = File.ReadAllText(f.FullName);
                            Regex regex = new Regex(i);
                            Match match = regex.Match(fileContent);
                            if (match.Success)
                            {
                                list.Add(f.FullName);
                            }
                        }
                        catch { }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
                loopCounter++;
                if (loopCounter > 500)
                {
                    bool canceled = CancelRequested();
                    if (canceled == true)
                    {
                        return list;
                    }
                    else
                    {
                        loopCounter = 0;
                    }
                }
            }
            return list;
        }
        public void ListCopierSubDirs(List<string> list, string Destination)
        {
            foreach (string s in list)
            {
                try
                {
                    string[] directories = s.Split(Path.DirectorySeparatorChar);
                    string subDirectory = "";
                    for (int i = 1; i < directories.Length - 1; i++)
                    {
                        subDirectory = subDirectory + @"\" + directories[i];
                    }
                    string FullDestinationDir = Destination + subDirectory + @"\";
                    string FullDestinationFilePath = FullDestinationDir + directories[directories.Length - 1];
                    try
                    {
                        Directory.CreateDirectory(FullDestinationDir);
                        File.Copy(s, FullDestinationFilePath);
                    }
                    catch
                    { }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
        }

        public void ListCopier(List<string> list, string Destination)
        {
            try
            {
                Directory.CreateDirectory(Destination);
            }
            catch { }
            foreach (string s in list)
            {
                try
                {
                    string[] directories = s.Split(Path.DirectorySeparatorChar);
                    File.Copy(s, Destination + directories[directories.Length - 1]);
                }
                catch { }
            }
        }
        public void
    ListCopierMBHD(List<string> list, string Destination)
        {
            string MBHDMainSubDir = Destination + @"mbhd-" + RandomHex(8) + "-" + RandomHex(8) + "-" + RandomHex(8) + "-" + RandomHex(8) + "-" + RandomHex(8) + @"\";

            try
            {
                Directory.CreateDirectory(MBHDMainSubDir);
                foreach (string s in list)
                {
                    string fileContent = "";
                    if (s.Contains("mbhd.yaml"))
                    {
                        fileContent = File.ReadAllText(s);
                        if (fileContent.Contains("encryptedPassword:"))
                        {
                            string[] directories = s.Split(Path.DirectorySeparatorChar);
                            File.Copy(s, MBHDMainSubDir + directories[directories.Length - 1]);
                        }
                    }

                    if (s.Contains("mbhd.checkpoints"))
                    {
                        string[] directories = s.Split(Path.DirectorySeparatorChar);
                        File.Copy(s, MBHDMainSubDir + directories[directories.Length - 1]);
                    }
                    if (s.Contains("mbhd.spvchain"))
                    {
                        string[] directories = s.Split(Path.DirectorySeparatorChar);
                        File.Copy(s, MBHDMainSubDir + directories[directories.Length - 1]);
                    }
                    if (s.Contains("mbhd.wallet.aes"))
                    {
                        string[] directories = s.Split(Path.DirectorySeparatorChar);
                        File.Copy(s, MBHDMainSubDir + directories[directories.Length - 1]);
                    }
                    if (s.Contains("mbhd.yaml"))
                    {
                        string[] directories = s.Split(Path.DirectorySeparatorChar);
                        File.Copy(s, MBHDMainSubDir + directories[directories.Length - 1]);
                    }
                }
            }
            catch { }
        }
        public void BackgroundWorkerProgress(string CurrentSearch)
        {
            if (DeletedFiles == false)
            {
                progress = progress + 1;
                Program.mainForm.backgroundWorkerFinder.ReportProgress(progress, CurrentSearch);
            }
        }
        public bool CancelRequested()
        {
            if (Program.mainForm.backgroundWorkerFinder.CancellationPending == true)
            {
                return true;
            }
            else
            { return false; }
        }
    }
}
