﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Globalization;

namespace DB_Scheme_Extract.Utility
{
    public class FileUtil
    {
        public static string DEFAULT_ROOT_PATH = "F://DB_PATCH//";

        // write the deploy script to correct path
        public string writeScript2CorrectFile(string wholeScript, string fileName)
        {
            writeText2NewFile(DEFAULT_ROOT_PATH + suffixFolder(), fileName + "//" + getTodayDateStr() + ".sql", wholeScript);
            return (DEFAULT_ROOT_PATH + suffixFolder() + "//" + fileName + "//" + getTodayDateStr() + ".sql").Replace("//", "\\");
        }

        public void setRootPath(string path)
        {
            DEFAULT_ROOT_PATH = path + "//" + getTodayDateStr();
        }

        /*****************************************
         * below are private methods
         *****************************************/
        private string suffixFolder()
        {
            string dailyFolder = "//" + getTodayDateStr();
            return dailyFolder;
        }

        private void writeText2NewFile(string filePath, string fileName, string text)
        {
            if (!File.Exists(filePath))
            {
                // create root folder first
                string newFilePath = createRootFolder(filePath, fileName);
                File.WriteAllText(newFilePath, text);
            }
        }

        /**
         * create the file with folder when the folder not created
         **/
        private string createRootFolder(string folderPath, string fileName)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string newFilePath = System.IO.Path.Combine(folderPath, fileName);
            return newFilePath;
        }

        private string getTodayDateStr()
        {
            DateTime time = DateTime.Now;
            string format = "yyyyMMdd";
            string todayStr = time.ToString(format);
            return todayStr;
        }
    }
}