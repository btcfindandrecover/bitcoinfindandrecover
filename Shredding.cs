/*
	Author: Earl Peter Gangoso
	Copyright (C) 2015
	Class Name: Shredding
	[Part of Secure Shredder Console (Open Source) and used by File Hider/Unhider Plus]
	Website: http://earlpeter.com/

	Based on the research "Secure Data Shredder" - Proceedings of the Global Engineering, Science and Technology Conference 2012, 28-29 December 2012, Dhaka, Bangladesh
	By Vikram Bahl, David Leong, Guo Jiayan, Jonathan Siang and Tay Mei Lan
	Source: http://www.phoebekwok.com/publications/GJY_SecureDataShredder.pdf

	===Algorithms used===
	Zero Data 1 Pass (0x00) [f]
	British HMG IS5-Baseline 1 Pass (0x00) [t]
	Russian GOST P50739-95 2 Passes (0x00, random) [f]
	British HMG IS5-Enhanced 3 Passes (0x00, 0xFF, random) [f,f,t]
	US DoD 5220.22-M / Canadian RCMP DSX 3 Passes (0x00, 0x01, random) [t]
	German VSITR 7 Passes (0x00,0x01, 0x00, 0x01, 0x00,0x01, random) [f]
	Bruce Schneier's 7 Passes (0x01, 0x00, random, random, random, random, random) [f]
	Canadian RCMP TSSIT OPS-II 7 Passes (0x00,0x01, 0x00,0x01, 0x00,0x01, random) [f...,t]
	Peter Gutmann 35 Passes
*/
using System;
using System.IO;
using System.Security.Cryptography;

namespace CryptoFinder
{
    public static class Shredding
    {
        #region "Private Functions"
        /// <summary>
        /// Generates a random 8-bit pattern
        /// </summary>
        /// <returns>Byte</returns>
        private static byte randomPattern()
        {
            Random myRandom = new Random();
            return (byte)myRandom.Next(0, 255);
        }

        /// <summary>
        /// Shred file using custom algorithm based on given pattern
        /// </summary>
        /// <param name="fileName">Path to file.</param>
        /// <param name="Patterns">Patterns to use.</param>
        /// <param name="verify">Use 0 to not verify pattern. 1 to verify.</param>
        public static void shredFile(string fileName, byte[] Patterns, short[] verify)
        {
            var MyFileInfo = new FileInfo(fileName);
            if (!MyFileInfo.Exists || MyFileInfo.Length == 0)
            {
                File.Delete(fileName);
                return;
            }

            // clear file attributes to get full control over file
            File.SetAttributes(fileName, FileAttributes.Normal);

            Stream s = new FileStream(MyFileInfo.FullName, FileMode.Open, FileAccess.ReadWrite, FileShare.None, (int)MyFileInfo.Length);
            if (!s.CanSeek || !s.CanWrite)
                throw new Exception("Can't open the file.");

            for (int i = 0; i < Patterns.Length; i++)
            {
                s.Seek(0, SeekOrigin.Begin);
                while (s.Position < s.Length)
                    s.WriteByte(Patterns[i]);

                s.Flush();
                // verify pattern if necessary
                if ((i < verify.Length && verify[i] == 1) || (verify[verify.Length - 1] == 1))
                {
                    s.Seek(0, SeekOrigin.Begin);
                    while (s.Position < s.Length)
                        if ((byte)s.ReadByte() != Patterns[i])
                            throw (new Exception("The file shredding failed in the pattern verification."));
                }
            }

            s.Close();
            File.Delete(fileName);
        }
        #endregion

        public static void ShredOnePass(string fileName)
        {
            shredFile(fileName, new byte[] { 0x00 }, new short[] { 0 });
        }
        public static void ShredBritishHMG1(string fileName)
        {
            shredFile(fileName, new byte[] { 0x00 }, new short[] { 1 });
        }
        public static void ShredRussianGOST(string fileName)
        {
            shredFile(fileName, new byte[] { 0x00, randomPattern() }, new short[] { 0 });
        }
        public static void ShredBritishHMG3(string fileName)
        {
            shredFile(fileName, new byte[] { 0x00, 0xFF, randomPattern() }, new short[] { 0, 0, 1 });
        }
        public static void ShredUSDoD(string fileName)
        {
            shredFile(fileName, new byte[] { 0x00, 0x01, randomPattern() }, new short[] { 1 });
        }
        public static void ShredGermanVSITR(string fileName)
        {
            shredFile(fileName, new byte[] { 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, randomPattern() }, new short[] { 0 });
        }
        public static void ShredBruceSchneier(string fileName)
        {
            shredFile(fileName, new byte[] { 0x01, 0x00, randomPattern(), randomPattern(), randomPattern(), randomPattern(), randomPattern() }, new short[] { 0 });
        }
        public static void ShredCanadianRCMP(string fileName)
        {
            shredFile(fileName, new byte[] { 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, randomPattern() }, new short[] { 0, 0, 0, 0, 0, 0, 1 });
        }
        public static void ShredGutmann(string fileName)
        {
            var MyFileInfo = new FileInfo(fileName);
            if (!MyFileInfo.Exists || MyFileInfo.Length == 0)
            {
                File.Delete(fileName);
                return;
            }

            // clear file attributes to get full control over file
            File.SetAttributes(fileName, FileAttributes.Normal);

            const int GutmannPasses = 35;
            var GutmannPass = new byte[GutmannPasses][];

            for (var i = 0; i < GutmannPass.Length; i++)
            {
                if ((i == 14) || (i == 19) || (i == 25) || (i == 26) || (i == 27))
                    continue;

                GutmannPass[i] = new byte[MyFileInfo.Length];
            }

            using (var rnd = new RNGCryptoServiceProvider())
            {
                for (var i = 0L; i < 4; i++)
                {
                    rnd.GetBytes(GutmannPass[i]);
                    rnd.GetBytes(GutmannPass[31 + i]);
                }
            }

            for (var i = 0L; i < MyFileInfo.Length;)
            {
                GutmannPass[4][i] = 0x55;
                GutmannPass[5][i] = 0xAA;
                GutmannPass[6][i] = 0x92;
                GutmannPass[7][i] = 0x49;
                GutmannPass[8][i] = 0x24;
                GutmannPass[10][i] = 0x11;
                GutmannPass[11][i] = 0x22;
                GutmannPass[12][i] = 0x33;
                GutmannPass[13][i] = 0x44;
                GutmannPass[15][i] = 0x66;
                GutmannPass[16][i] = 0x77;
                GutmannPass[17][i] = 0x88;
                GutmannPass[18][i] = 0x99;
                GutmannPass[20][i] = 0xBB;
                GutmannPass[21][i] = 0xCC;
                GutmannPass[22][i] = 0xDD;
                GutmannPass[23][i] = 0xEE;
                GutmannPass[24][i] = 0xFF;
                GutmannPass[28][i] = 0x6D;
                GutmannPass[29][i] = 0xB6;
                GutmannPass[30][i++] = 0xDB;
                if (i >= MyFileInfo.Length)
                    continue;

                GutmannPass[4][i] = 0x55;
                GutmannPass[5][i] = 0xAA;
                GutmannPass[6][i] = 0x49;
                GutmannPass[7][i] = 0x24;
                GutmannPass[8][i] = 0x92;
                GutmannPass[10][i] = 0x11;
                GutmannPass[11][i] = 0x22;
                GutmannPass[12][i] = 0x33;
                GutmannPass[13][i] = 0x44;
                GutmannPass[15][i] = 0x66;
                GutmannPass[16][i] = 0x77;
                GutmannPass[17][i] = 0x88;
                GutmannPass[18][i] = 0x99;
                GutmannPass[20][i] = 0xBB;
                GutmannPass[21][i] = 0xCC;
                GutmannPass[22][i] = 0xDD;
                GutmannPass[23][i] = 0xEE;
                GutmannPass[24][i] = 0xFF;
                GutmannPass[28][i] = 0xB6;
                GutmannPass[29][i] = 0xDB;
                GutmannPass[30][i++] = 0x6D;
                if (i >= MyFileInfo.Length)
                    continue;

                GutmannPass[4][i] = 0x55;
                GutmannPass[5][i] = 0xAA;
                GutmannPass[6][i] = 0x24;
                GutmannPass[7][i] = 0x92;
                GutmannPass[8][i] = 0x49;
                GutmannPass[10][i] = 0x11;
                GutmannPass[11][i] = 0x22;
                GutmannPass[12][i] = 0x33;
                GutmannPass[13][i] = 0x44;
                GutmannPass[15][i] = 0x66;
                GutmannPass[16][i] = 0x77;
                GutmannPass[17][i] = 0x88;
                GutmannPass[18][i] = 0x99;
                GutmannPass[20][i] = 0xBB;
                GutmannPass[21][i] = 0xCC;
                GutmannPass[22][i] = 0xDD;
                GutmannPass[23][i] = 0xEE;
                GutmannPass[24][i] = 0xFF;
                GutmannPass[28][i] = 0xDB;
                GutmannPass[29][i] = 0x6D;
                GutmannPass[30][i++] = 0xB6;
            }

            GutmannPass[14] = GutmannPass[4];
            GutmannPass[19] = GutmannPass[5];
            GutmannPass[25] = GutmannPass[6];
            GutmannPass[26] = GutmannPass[7];
            GutmannPass[27] = GutmannPass[8];

            Stream s = new FileStream(MyFileInfo.FullName, FileMode.Open, FileAccess.Write, FileShare.None, (int)MyFileInfo.Length, FileOptions.DeleteOnClose | FileOptions.RandomAccess | FileOptions.WriteThrough);

            if (!s.CanSeek || !s.CanWrite)
                throw new Exception("Can't open the file.");

            for (var i = 0L; i < GutmannPass.Length; i++)
            {
                s.Seek(0, SeekOrigin.Begin);
                s.Write(GutmannPass[i], 0, GutmannPass[i].Length);
                s.Flush();
            }

            s.Close();
        }
    }
}
