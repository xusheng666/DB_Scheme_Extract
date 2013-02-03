using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB_Scheme_Extract.Business
{
    class ConnectionProperty
    {
        public string dataSource { get; set; }
        public string userID { get; set; }
        public string password { get; set; }

        public string getConnString()
        {
            //"Data Source=mas-proddb/udevext; User ID=MS9DJA; Password=password;"
            return "Data Source="+dataSource+"; User ID="+userID+"; Password="+password+";";
        }
    }
}
