﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using DB_Scheme_Extract.Utility;

namespace DB_Scheme_Extract.Persistence
{
    class OracleClient
    {
        public static List<string> queryObjNameList(OracleConnection conn, string objType)
        {
            List<string> objNameList = new List<string>();
            try
            {
                string sqlText = DBSQL.QUERYOBJECTSLISTSQL;
                if (!String.IsNullOrEmpty(sqlText))
                {
                    conn.Open();
                    sqlText = sqlText.Replace("?", objType);

                    DataSet ds = OracleClient.getResultTable(conn, sqlText);
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            objNameList.Add(ds.Tables[0].Rows[i][0].ToString());
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception)
            {
                throw;
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
