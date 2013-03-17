using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using DB_Scheme_Extract.Utility;
using System.IO;

namespace DB_Scheme_Extract.Persistence
{
    class OracleClient
    {

        public static List<string> queryObjNameList(string objType)
        {
            List<string> objNameList = new List<string>();
            try
            {
                string sqlText = DBSQL.QUERYOBJECTSLISTSQL;
                if (!String.IsNullOrEmpty(sqlText))
                {
                    sqlText = sqlText.Replace("?", "'" + objType + "'");

                    DataSet ds = getResultTable(ConnectionManager.getConnection(), sqlText);
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            objNameList.Add(ds.Tables[0].Rows[i][1].ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return objNameList;
        }

        public static DataSet getResultTable(OracleConnection conn, string sqlText)
        {
            OracleCommand cmd = conn.CreateCommand();

            cmd.CommandText = sqlText;

            OracleDataAdapter adapter = new OracleDataAdapter(cmd);
            DataSet ds = new DataSet();
            ds.Tables.Add(new DataTable("DbSource"));
            adapter.Fill(ds, "DbSource");

            return ds;
        }
    }
}
