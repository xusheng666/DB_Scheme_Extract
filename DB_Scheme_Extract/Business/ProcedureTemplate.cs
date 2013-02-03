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
    class ProcedureTemplate : GenerateTemplate
    {
        public const string TEM_TYPE = Constants.PATH_PROCEDURE;

        override public string generateObjList(List<string> objectList, string objType)
        {
            OracleConnection connOri = getConnection();
            connOri.Open();
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

                    if (ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                string firstLine = ds.Tables[0].Rows[i][4].ToString();

                                int startInt = firstLine.IndexOf(objName.ToLower());
                                if (startInt == -1)
                                {
                                    startInt = firstLine.IndexOf(objName);
                                }
                                string trimSpaceLine = firstLine.Substring(startInt);
                                //string updFirstLine = firstLine.Replace(objName, " MS9DJA." + objName).Replace(objName.ToLower(), " MS9DJA." + objName);
                                sb.Append("CREATE OR REPLACE " + objType + " MS9DJA." + trimSpaceLine);
                            }
                            else
                            {
                                if (!"\n".Equals(ds.Tables[0].Rows[i][4].ToString()) &&
                                    !String.IsNullOrEmpty(ds.Tables[0].Rows[i][4].ToString()))// not include the "\n" line
                                {
                                    sb.Append(ds.Tables[0].Rows[i][4].ToString().TrimEnd() + "\r\n");
                                }
                            }
                        }
                        //lastLineText = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1][4].ToString().TrimEnd();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return sb.ToString();
        }
    }
}
