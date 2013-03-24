using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB_Scheme_Extract.Utility;
using System.Data;
using DB_Scheme_Extract.Persistence;
using Oracle.DataAccess.Client;
using System.Windows.Forms;

namespace DB_Scheme_Extract.Business.exportData
{
    class GeneratePreloadTable
    {
        private const string RETCHAR = "\r\n";

        public List<string> generateTableScript(List<string> tableList, string appPath, string ownerName, ProgressBar fileBar)
        {
            List<string> outputList = new List<string>();
            OracleConnection connOri = ConnectionManager.getConnection();

            if (tableList != null && tableList.Count > 0 && connOri != null)
            {
                // Step 1: set write out path
                string rootPath = appPath + Constants.seperator + "preload";

                for (int i = 0; i < tableList.Count; i++)
                {
                    string tableName = tableList[i];
                    string script = prepareScriptOfTable(connOri, tableName, ownerName, fileBar);

                    string outputPath = writeStringToDisk(rootPath, tableName, script);
                    outputList.Add(outputPath);
                }
            }

            connOri.Close();

            return outputList;
        }

        /*************************************
         * get table columns and columns values
         *************************************/
        private string prepareScriptOfTable(OracleConnection conn, string tableName, string ownerName, ProgressBar fileBar)
        {
            StringBuilder scriptCol = new StringBuilder();
            StringBuilder scriptFinal = new StringBuilder();
            scriptFinal.Append("SET DEFINE OFF; " + RETCHAR);
            scriptFinal.Append("DELETE FROM " + ownerName + "." + tableName + ";" + RETCHAR);

            // Step 1: get all columns name of the table
            string sqlText = DBSQL.QUERY_TABLE_COLUMNS;
            sqlText = sqlText.Replace("?", tableName);

            DataSet ds = OracleClient.getResultTable(conn, sqlText);
            List<string> colList = new List<string>();
            if (ds.Tables.Count > 0)
            {
                scriptCol.Append("INSERT INTO " + ownerName + "." + tableName + "(");
                // prepare one row with the column type
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    colList.Add(ds.Tables[0].Rows[i][1].ToString());
                    if (i != ds.Tables[0].Rows.Count - 1)
                    {
                        scriptCol.Append(ds.Tables[0].Rows[i][0] + ",");
                    }
                    else
                    {
                        scriptCol.Append(ds.Tables[0].Rows[i][0] + ")");
                    }
                }
                scriptCol.Append(RETCHAR);
                scriptCol.Append(" VALUES( ");
            }

            // Step 2: get all values and combine to the insert script
            string sqlValueText = DBSQL.QUERY_COLUMNS_VALUES;
            sqlValueText = sqlValueText.Replace("?", tableName);


            DataSet valueDS = OracleClient.getResultTable(conn, sqlValueText);
            if (valueDS.Tables.Count > 0)
            {
                for (int i = 0; i < valueDS.Tables[0].Rows.Count; i++)
                {
                    // first append the column script
                    scriptFinal.Append(scriptCol);

                    string columnValue = "";
                    for (int j = 0; j < colList.Count; j++)
                    {
                        columnValue = getDataTypeScript(valueDS.Tables[0].Rows[i][j].ToString(), colList[j]);

                        if (j != colList.Count - 1)
                        {
                            scriptFinal.Append(columnValue + ",");
                        }
                        else
                        {
                            scriptFinal.Append(columnValue);
                        }
                        // update progress bar
                        int incre = 100 / (i + 1) / (j + 1);
                        fileBar.Increment(incre);
                    }
                    scriptFinal.Append(");" + RETCHAR);
                }
            }
            scriptFinal.Append("COMMIT;");

            return scriptFinal.ToString();
        }

        private string getDataTypeScript(string strValue, string columnType)
        {
            string dataValueScript = "";
            string formatStr = strValue.Replace("'", "''");
            switch (columnType)
            {
                case Constants.DT_CLOB:
                    dataValueScript = "'" + formatStr + "'";
                    break;
                case Constants.DT_DATE:
                case Constants.DT_TIME:
                    dataValueScript = "TO_DATE('" + formatStr + "', 'DD/MM/YYYY HH:MI:SS AM')";
                    break;
                default:
                    dataValueScript = "'" + formatStr + "'";
                    break;
            }
            return dataValueScript;
        }

        /*************************************
        * write the script into disk
        *************************************/
        private string writeStringToDisk(string rootPath, string tableName, string scriptStr)
        {
            FileUtil fu = new FileUtil();
            string fileName = tableName + "_preload.sql";
            fu.setRootPath(rootPath);

            string outputFileName = fu.writeScript2CorrectFile(scriptStr, fileName);
            return outputFileName;
        }
    }
}
