using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB_Scheme_Extract.Utility;
using Oracle.DataAccess.Client;
using DB_Scheme_Extract.Persistence;
using System.Data;

namespace DB_Scheme_Extract.Business
{
    class ViewTemplate : GenerateTemplate
    {
        override public string generateObjList(List<string> objectList, string objType)
        {
            OracleConnection connOri = ConnectionManager.getConnection();
            //connOri.Open();
            // query sp script and updated to target db
            try
            {
                string spFinally = "";
                foreach (var item in objectList)
                {
                    string spScript = genScript(connOri, item, objType);
                    // prepare script write to file
                    spFinally += spScript;

                }
                connOri.Close();
                // write out the script into hard disk
                return spFinally;
            }
            catch (Exception innere)
            {
                connOri.Close();
                throw innere;
            }
        }
        /***********************
        * private methods
        * ********************/
        private string genScript(OracleConnection conn, string objName, string objType)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                if (!String.IsNullOrEmpty(objName))
                {
                    // uppercase the name
                    string objNameUpper = objName.ToUpper();
                    sb.Append("DROP VIEW MS9DJA." + objNameUpper + ";" + RETCHAR);

                    sb.Append("CREATE OR REPLACE VIEW MS9DJA." + objNameUpper + RETCHAR);
                    sb.Append("(" + RETCHAR);

                    string viewColsText = DBSQL.QUERY_VIEW_COLS;
                    viewColsText = viewColsText.Replace("?", objNameUpper);
                    viewColsText += " ORDER BY COLUMN_ID ASC ";
                    DataSet colsds = OracleClient.getResultTable(conn, viewColsText);

                    for (int i = 0; i < colsds.Tables[0].Rows.Count; i++)
                    {
                        // append the column name
                        sb.Append("".PadLeft(4));

                        if (i == colsds.Tables[0].Rows.Count - 1)
                        {// if last no need the ","
                            sb.Append((colsds.Tables[0].Rows[i][0].ToString()) + RETCHAR);
                        }
                        else
                        {
                            sb.Append((colsds.Tables[0].Rows[i][0].ToString()) + "," + RETCHAR);
                        }
                    }
                    sb.Append(")" + RETCHAR);
                    sb.Append("AS" + RETCHAR);

                    string sqlText = DBSQL.QUERY_VIEW_TEXT;
                    sqlText = sqlText.Replace("?", objNameUpper);
                    DataSet ds = OracleClient.getResultTable(conn, sqlText);

                    sb.Append(ds.Tables[0].Rows[0][0].ToString());
                    sb.Append(";" + RETCHAR + RETCHAR);
                }
            }
            catch (Exception e)
            {
                throw new Exception("genScript got error:" + e);
            }

            return sb.ToString();
        }
    }
}
