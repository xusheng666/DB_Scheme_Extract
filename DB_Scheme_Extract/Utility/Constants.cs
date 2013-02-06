using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB_Scheme_Extract.Utility
{
    class Constants
    {
        public const string seperator = "//";

        public const string TYPE_TABLE = "TABLE";
        public const string TYPE_PACKAGE = "PACKAGE";
        public const string TYPE_TYPE = "TYPE";
        public const string TYPE_FUNCTION = "FUNCTION";
        public const string TYPE_PROCEDURE = "PROCEDURE";
        public const string TYPE_VIEW = "VIEW";
        public const string TYPE_TRIGGER = "TRIGGER";

        public const string PATH_TABLE = seperator + "TABLE";
        public const string PATH_PACKAGE = seperator + "PACKAGE";
        public const string PATH_TYPE = seperator + "TYPE";
        public const string PATH_FUNCTION = seperator + "FUNCTION";
        public const string PATH_PROCEDURE = seperator + "PROCEDURE";
        public const string PATH_VIEW = seperator + "VIEW";
        public const string PATH_TRIGGER = seperator + "TRIGGER";

        public const string CONN_CLOSED = "Closed";
        public const string CONN_OPEN = "Open";

        public static Dictionary<string, string> filePathDict = new Dictionary<string, string>()
        {
	        {TYPE_TABLE, PATH_TABLE},
            {TYPE_PACKAGE, PATH_PACKAGE},
            {TYPE_TYPE, PATH_TYPE},
	        {TYPE_FUNCTION, PATH_FUNCTION},
	        {TYPE_PROCEDURE, PATH_PROCEDURE},
            {TYPE_VIEW, PATH_VIEW},
            {TYPE_TRIGGER, PATH_TRIGGER}
        };

        public static Dictionary<string, string> exeSeqDict = new Dictionary<string, string>()
        {
            {TYPE_TABLE, "001-table"},
	        {TYPE_PACKAGE, "002-package"},
	        {TYPE_TYPE, "003-type"},
	        {TYPE_FUNCTION, "004-function"},
	        {TYPE_PROCEDURE, "005-procedure"},
            {TYPE_VIEW, "006-view"},
            {TYPE_TRIGGER , "008-trigger"}
        };

        public static List<string> typeList = new List<string>() 
        { 
            TYPE_TABLE, 
            TYPE_FUNCTION,
            TYPE_PACKAGE,
            TYPE_PROCEDURE,
            TYPE_TRIGGER,
            TYPE_TYPE,
            TYPE_VIEW        
        };
    }

}
