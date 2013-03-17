using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB_Scheme_Extract.Utility
{
    public class DBSQL
    {
        /**********************************/
        /* below for generate procedure
        /**********************************/
        public static string QUERYOBJECTSLISTSQL = "SELECT * FROM ALL_OBJECTS WHERE OWNER = 'MS9DJA' AND OBJECT_TYPE = ? ";
        public static string QUERY_SP_DETAILSQL = "SELECT * FROM ALL_SOURCE WHERE OWNER = 'MS9DJA' AND NAME = ? ";
        public static string SP_GRANT = "GRANT EXECUTE ON MS9DJA.? TO MS_DJA_EDITOR_ROLE; ";
         
        /*****************************/
        /* below for generate table
        /*****************************/
        public static string QUERY_TABLE_COLS = "SELECT COLUMN_NAME,DATA_TYPE,DATA_LENGTH,DATA_PRECISION,DATA_SCALE,NULLABLE,DEFAULT_LENGTH,DATA_DEFAULT,LAST_ANALYZED,CHAR_USED,COLUMN_ID FROM ALL_TAB_COLUMNS WHERE OWNER = 'MS9DJA' AND TABLE_NAME = UPPER('?') ";
        public static string QUERY_CONST_COLS = "SELECT B.CONSTRAINT_NAME, B.COLUMN_NAME FROM ALL_CONSTRAINTS A, ALL_CONS_COLUMNS B WHERE A.CONSTRAINT_NAME = B.CONSTRAINT_NAME AND A.OWNER = 'MS9DJA' AND A.TABLE_NAME = UPPER('?') AND A.CONSTRAINT_TYPE = 'P'";
        public static string TABLE_GRANT = "GRANT SELECT, INSERT, UPDATE, DELETE ON MS9DJA.? TO MS_DJA_EDITOR_ROLE; ";

        /*****************************/
        /* below for generate view
        /*****************************/
        //public static string QUERY_VIEW_TEXT = "SELECT TEXT FROM ALL_VIEWS WHERE OWNER = 'MS9DJA' AND VIEW_NAME = '?'";
        public static string QUERY_VIEW_TEXT = "SELECT GETLONG('SELECT TEXT FROM ALL_VIEWS WHERE VIEW_NAME = :X', ':X', '?' ) FROM DUAL;";
        public static string QUERY_VIEW_COLS = "SELECT COLUMN_NAME,COLUMN_ID FROM ALL_TAB_COLUMNS WHERE OWNER = 'MS9DJA' AND TABLE_NAME = '?'";
    }
}