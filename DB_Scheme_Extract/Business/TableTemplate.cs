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
    class TableTemplate : GenerateTemplate
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
                    if (!item.Contains("DJA9") && !item.Contains("_A") && !item.Contains("_X"))
                    {
                        string spScript = genScript(connOri, item, objType);
                        // prepare script write to file
                        spFinally += spScript;
                    }
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

                    string sqlText = DBSQL.QUERY_TABLE_COLS;
                    sqlText = sqlText.Replace("?", objNameUpper);
                    sqlText += " ORDER BY COLUMN_ID ASC ";

                    DataSet ds = OracleClient.getResultTable(conn, sqlText);

                    if (ds.Tables.Count > 0)
                    {
                        StringBuilder beforeSB = new StringBuilder();
                        // prepare the alter and drop script
                        beforeSB.Append("ALTER TABLE MS9DJA." + objNameUpper + " DROP PRIMARY KEY CASCADE;" + RETCHAR);
                        beforeSB.Append("DROP TABLE MS9DJA." + objNameUpper + " CASCADE CONSTRAINTS PURGE;" + RETCHAR + RETCHAR);
                        sb.Append(beforeSB.ToString());
                        // generate first line
                        sb.Append("CREATE " + objType + " MS9DJA." + objNameUpper + RETCHAR);
                        sb.Append("(" + RETCHAR);
                        // generate the columns
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            // append the column name
                            sb.Append("".PadLeft(4));
                            sb.Append((ds.Tables[0].Rows[i][0].ToString().TrimEnd()).PadRight(36));

                            string columnDataType = getDataTypeScript(ds.Tables[0].Rows[i]);
                            sb.Append(columnDataType.PadRight(25));

                            if (i == ds.Tables[0].Rows.Count - 1)
                            {// if last no need the ","
                                sb.Append((ds.Tables[0].Rows[i][5].ToString() == "N" ? "NOT NULL" : "") + RETCHAR);
                            }
                            else
                            {
                                sb.Append((ds.Tables[0].Rows[i][5].ToString() == "N" ? "NOT NULL," : ",") + RETCHAR);
                            }
                        }
                        sb.Append(")" + RETCHAR);

                        // gen the script of pk 
                        string sqlPKColsText = DBSQL.QUERY_CONST_COLS;
                        sqlPKColsText = sqlPKColsText.Replace("?", objNameUpper);
                        sqlPKColsText += " ORDER BY B.POSITION ASC ";

                        DataSet pkds = OracleClient.getResultTable(conn, sqlPKColsText);
                        string pkName = "";
                        StringBuilder constScript = new StringBuilder();
                        if (pkds.Tables.Count>0)
                        {
                            for (int i = 0; i < pkds.Tables[0].Rows.Count; i++)
                            {
                                if (i != pkds.Tables[0].Rows.Count-1)
                                {
                                    constScript.Append(pkds.Tables[0].Rows[i][1].ToString() + ",");
                                }
                                else
                                {
                                    constScript.Append(pkds.Tables[0].Rows[i][1].ToString());
                                    pkName = pkds.Tables[0].Rows[i][0].ToString();
                                }
                                 
                            }
                        }
                        sb.Append("ALTER TABLE MS9DJA." + objNameUpper + " ADD (CONSTRAINT " + pkName + " PRIMARY KEY (" + constScript + "));" + RETCHAR);
                        // below are the synonym and grant
                        StringBuilder afterSB = new StringBuilder();
                        string synonym = objNameUpper.Substring(objNameUpper.IndexOf("_") + 1);
                        // afterSB.Append("ALTER TABLE MS9DJA." + objName.ToUpper() + " DROP PRIMARY KEY CASCADE;" + RETCHAR);
                        afterSB.Append("CREATE OR REPLACE SYNONYM MS9DJA.T_EXT_" + synonym + " FOR MS9DJA." + objNameUpper + ";" + RETCHAR);
                        //afterSB.Append("GRANT SELECT, INSERT, UPDATE, DELETE ON MS9DJA." + objNameUpper + " TO MS_DJA_EDITOR_ROLE;" + RETCHAR + RETCHAR);
                        afterSB.Append(DBSQL.TABLE_GRANT.Replace("?", objNameUpper) + RETCHAR + RETCHAR);

                        sb.Append(afterSB.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("genScript got error:" + e);
            }

            return sb.ToString();
        }

        private string getDataTypeScript(DataRow row)
        {
            string dataTypeScript = "";

            switch (row[1].ToString())
            {
                case Constants.DT_VCHAR2:
                    string byteType = row[9].ToString() == "B" ? "BYTE" : "CHAR";
                    dataTypeScript = row[1].ToString() + "(" + row[2].ToString() + " " + byteType + ")";
                    break;
                case Constants.DT_NVCHAR2:
                    dataTypeScript = row[1].ToString() + "(" + row[2].ToString() + ")";
                    break;
                default:
                    dataTypeScript = row[1].ToString();
                    break;
            }
            return dataTypeScript;
        }
    }
}
