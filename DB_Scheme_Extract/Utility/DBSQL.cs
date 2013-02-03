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
        public static string D_QUERYOBJECTSSQL_TODAY = "SELECT DISTINCT OBJECT_NAME, OBJECT_TYPE, MODULE_NAME, DB_TYPE FROM MS9DJA.SYS_OBJECT_UPD_LOG WHERE OBJECT_TYPE = ? AND TO_DATE(OPERAION_TIME, 'YYYY-MM-DD') = TO_DATE(SYSDATE,'YYYY-MM-DD') ";

        public static string D_QUERYOBJECTSSQL_YESTERDAY = "SELECT DISTINCT OBJECT_NAME, OBJECT_TYPE, MODULE_NAME, DB_TYPE FROM MS9DJA.SYS_OBJECT_UPD_LOG WHERE OBJECT_TYPE = ? AND OPERAION_TIME BETWEEN TO_DATE ('2013/01/14', 'yyyy/mm/dd') AND TO_DATE ('2013/01/19', 'yyyy/mm/dd') ";
        
        public static string D_QUERYOBJECTSSQL_THISWEEK = "SELECT DISTINCT OBJECT_NAME, OBJECT_TYPE, MODULE_NAME, DB_TYPE FROM MS9DJA.SYS_OBJECT_UPD_LOG WHERE OBJECT_TYPE = ? AND TO_CHAR(OPERAION_TIME, 'YYYY')=TO_CHAR(SYSDATE, 'YYYY') AND TO_CHAR(OPERAION_TIME, 'MM')=TO_CHAR(SYSDATE, 'MM') AND TO_CHAR(OPERAION_TIME, 'W')=TO_CHAR(SYSDATE, 'W') ";

        public static string D_QUERYOBJECTS = "SELECT OBJECT_TYPE, COUNT(1) FROM ALL_OBJECTS WHERE OWNER = 'MS9DJA' AND OBJECT_TYPE NOT IN ('LOB','INDEX','SYNONYM','TRIGGER','TYPE') GROUP BY OBJECT_TYPE ORDER BY OBJECT_TYPE ";
        /*****************************/
        /* below for alert controller query
        /*****************************/
        public static string QUERY_INVALID_OBJECTS = "SELECT A.OBJECT_NAME, A.OBJECT_TYPE, A.CREATED, A.LAST_DDL_TIME, A.STATUS, B.LINE, B.TEXT FROM ALL_OBJECTS A LEFT JOIN ALL_ERRORS B ON B.NAME = A.OBJECT_NAME AND B.OWNER = A.OWNER AND B.TYPE = A.OBJECT_TYPE WHERE A.OWNER = 'MS9DJA' AND A.STATUS != 'VALID' ";
        
        /*****************************/
        /* below for table view controller query
        /*****************************/
        public static string QUERY_TABLE_COLS = "SELECT OWNER,TABLE_NAME,COLUMN_NAME,DATA_TYPE,DATA_LENGTH,DATA_PRECISION,DATA_SCALE,NULLABLE,DEFAULT_LENGTH,DATA_DEFAULT,LAST_ANALYZED,CHAR_USED,COLUMN_ID FROM ALL_TAB_COLUMNS WHERE OWNER = 'MS9DJA' AND TABLE_NAME = '?' ";
        public static string TABLE_GRANT = "GRANT SELECT, INSERT, UPDATE, DELETE ON MS9DJA.? TO MS_DJA_EDITOR_ROLE; ";
    }
}