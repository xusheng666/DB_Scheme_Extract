using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB_Scheme_Extract.Utility
{
    public class DBSQL
    {
        /**********************************/
        /* below for HOME controller query
        /**********************************/ 
        public static string QUERYOBJECTSLISTSQL = "SELECT * FROM ALL_OBJECTS WHERE OWNER = 'MS9DJA' AND OBJECT_TYPE = ? ";
        
        public static string QUERY_SP_DETAILSQL = "SELECT * FROM ALL_SOURCE WHERE OWNER = 'MS9DJA' AND NAME = ? ";

        public static string SP_GRANT = "GRANT EXECUTE ON MS9DJA.? TO MS_DJA_EDITOR_ROLE; ";
        
        /*****************************/
        /* below for deploy query of Generate controller
        /*****************************/
        public static string D_QUERYOBJECTS = "SELECT OBJECT_TYPE, COUNT(1) FROM ALL_OBJECTS WHERE OWNER = 'MS9DJA' AND OBJECT_TYPE NOT IN ('LOB','INDEX','SYNONYM','TRIGGER','TYPE') GROUP BY OBJECT_TYPE ORDER BY OBJECT_TYPE ";
         
        /*****************************/
        /* below for table view controller query
        /*****************************/
        public static string QUERY_TABLE_COLS = "SELECT OWNER,TABLE_NAME,COLUMN_NAME,DATA_TYPE,DATA_LENGTH,DATA_PRECISION,DATA_SCALE,NULLABLE,DEFAULT_LENGTH,DATA_DEFAULT,LAST_ANALYZED,CHAR_USED,COLUMN_ID FROM ALL_TAB_COLUMNS WHERE OWNER = 'MS9DJA' AND TABLE_NAME = '?' ";
        public static string TABLE_GRANT = "GRANT SELECT, INSERT, UPDATE, DELETE ON MS9DJA.? TO MS_DJA_EDITOR_ROLE; ";
    }
}