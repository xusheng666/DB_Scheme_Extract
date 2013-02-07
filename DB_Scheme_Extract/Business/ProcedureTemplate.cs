using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB_Scheme_Extract.Persistence;
using System.Data;
using Oracle.DataAccess.Client;
using DB_Scheme_Extract.Utility;
using System.Text.RegularExpressions;

namespace DB_Scheme_Extract.Business
{
    class ProcedureTemplate : GenerateTemplate
    {
        override public string generateObjList(List<string> objectList, string objType)
        {
            OracleConnection connOri = ConnectionManager.getConnection();
            //connOri.Open();
            // query sp script and updated to target db
            try
            {
                string spFinally = "";
                string grantText = "";
                string wholeScript = "";
                foreach (var item in objectList)
                {
                    string spScript = genScript(connOri, item, objType);
                    if (objType.Equals(Constants.TYPE_PROCEDURE) || objType.Equals(Constants.TYPE_FUNCTION))
                    {
                        string grantScript = "";
                        grantScript = DBSQL.SP_GRANT.Replace("?", item);
                        grantText += grantScript;
                        grantText += "\r\n";
                    }
                    //break;
                    // prepare script write to file
                    spFinally += spScript + "/\r\n";

                    // for Package or Type there will be one more object called Package Body and Type Body
                    string bodyScript = null;
                    if (Constants.TYPE_PACKAGE.Equals(objType) || Constants.PATH_TYPE.Equals(objType))
                    {
                        bodyScript = genScript(connOri, item, objType + " BODY");
                        if (!String.IsNullOrEmpty(bodyScript))
                        {
                            spFinally += bodyScript + "/\r\n";
                        }
                    }
                }
                connOri.Close();

                wholeScript = spFinally + grantText;
                // write out the script into hard disk
                return wholeScript;
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

                                firstLine = firstLine.Replace("\"", "");
                                
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
                            if (ds.Tables[0].Rows.Count==1)
                            {
                                sb.Append("\r\n");
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
