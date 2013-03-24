using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DB_Scheme_Extract.Model;
using System.Windows.Forms;

namespace DB_Scheme_Extract.Utility
{
    class SerializeUtil
    {
        public void Serialize(object serClassName)
        {
            //ConfigurationObject c = new ConfigurationObject();
            string dirPath = getFileDirPath();
            //object obj = Activator.CreateInstance(serClassName.GetType());
            FileStream fileStream = FileUtil.createFileByPath(dirPath, getFileName(serClassName));
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(fileStream, serClassName);
            fileStream.Close();
        }
        public object DeSerialize(object serClassName)
        {
            string dirPath = getFileDirPath();
            string path = dirPath + getFileName(serClassName);
            // if file not exist for the first time then will create an empty object file
            if (!File.Exists(path))
            {
                Serialize(getDefaultConf());
            }
            FileStream fileStream = FileUtil.readFile(path);
            BinaryFormatter b = new BinaryFormatter();
            object c = b.Deserialize(fileStream);
            fileStream.Close();

            return c;
        }

        /*****************************
         * private methods
         *****************************/
        private string getFileDirPath()
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);

            return appPath + SerialConstants.SERIAL_OBJ_PATH + Constants.seperator;
        }

        private string getFileName(object className)
        {
            return className.ToString() + SerialConstants.SERIAL_OBJ_PATH_EXT;
        }

        // set the default conf when no file exist
        private ConfigurationObject getDefaultConf()
        {
            ConfigurationObject defaultConf = new ConfigurationObject();
            defaultConf.DataSource = "localhost/extdev";
            defaultConf.UserID = "MS9DJA";
            defaultConf.Password = "password";
            defaultConf.ExportFilePath = "";

            List<string> preloadTableList = new List<string>();
            preloadTableList.Add("DJA0513_MTN_WEB_TEMPLATE_L");
            preloadTableList.Add("DJA0514_MTN_FILETYPE_L");
            preloadTableList.Add("DJA0515_BTA_FILETYPE_L");
            preloadTableList.Add("DJA0516_MTN_NOTIFY_TMPL_L");
            preloadTableList.Add("DJA0521_SD_FILETYPE_L");
            preloadTableList.Add("T_IC_CODE");
            
            defaultConf.PreloadTableNameList = preloadTableList;

            return defaultConf;
        }
    }
}
