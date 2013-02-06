using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB_Scheme_Extract.Persistence;
using System.Data;
using Oracle.DataAccess.Client;
using DB_Scheme_Extract.Utility;

namespace DB_Scheme_Extract.Business
{
    class TriggerTemplate : GenerateTemplate
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
                    string spScript = genScript(connOri, null, objType);
                    // prepare script write to file
                    spFinally += spScript + "/\r\n";

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
                    string sqlText = DBSQL.QUERY_SP_DETAILSQL;
                    sqlText = sqlText.Replace("?", "'" + objName + "'");
                    sqlText += " AND TYPE ='" + objType + "'";
                    sqlText += " ORDER BY LINE ASC ";

                    DataSet ds = OracleClient.getResultTable(conn, sqlText);
                    List<string> noDataInSysTableList = new List<string>();
                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                string firstLine = ds.Tables[0].Rows[i][4].ToString();

                                int startInt = firstLine.ToUpper().IndexOf(objName.ToUpper());
                                if (startInt == -1)
                                {
                                    noDataInSysTableList.Add(objName);
                                    continue;
                                }
                                string trimSpaceLine = firstLine.Substring(startInt);
                                //string updFirstLine = firstLine.Replace(objName, " MS9DJA." + objName).Replace(objName.ToLower(), " MS9DJA." + objName);
                                sb.Append("CREATE OR REPLACE " + objType + " MS9DJA." + trimSpaceLine);
                            }
                            else
                            {
                                if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i][4].ToString()) &&
                                    !String.IsNullOrEmpty(ds.Tables[0].Rows[i][4].ToString().Trim()) &&
                                    !"\n".Equals(ds.Tables[0].Rows[i][4].ToString().Trim()))// not include the "\n" line
                                {
                                    sb.Append(ds.Tables[0].Rows[i][4].ToString().TrimEnd() + "\r\n");
                                }
                            }
                        }
                        //lastLineText = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][4].ToString().TrimEnd();
                    }
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
