using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB_Scheme_Extract.Model
{
    /***************************************
     * serialize the configuration object
     ***************************************/
    [Serializable]
    class ConfigurationObject
    {
        public string DataSource { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string ExportFilePath { get; set; }

        public List<string> PreloadTableNameList { get; set; }
    }
}
