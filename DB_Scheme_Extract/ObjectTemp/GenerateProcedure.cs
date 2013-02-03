using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB_Scheme_Extract.ObjectTemp
{
    class GenerateProcedure : GenerateFactory
    {
        string genScript(string objName)
        {
            string objScript = "";

            return objScript;
        }

        public static string generateScriptFile2Disk(string range, List<ALL_OBJECTS> objectList, string envType, string extType, string pathOption, string objType)
        {
            FileUtil fu = new FileUtil();
            OracleConnection connOri = ConnectionManager.getOriginalDBConn(envType, extType);
            connOri.Open();
            // query sp script and updated to target db
            try
            {
                string spFinally = "";
                string grantText = "";
                string wholeScript = "";
                string modelName = objectList[0].module.ToLower();
                foreach (var item in objectList)
                {
                    string spScript = HomeDAService.getSPScript(connOri, item.object_name, objType);
                    if (objType.Equals(HomeDAService.PROCEDURE) || objType.Equals(HomeDAService.FUNCTION))
                    {
                        string grantScript = "";
                        grantScript = DBSQL.SP_GRANT.Replace("?", item.object_name);
                        grantText += grantScript;
                        grantText += "\r\n";
                    }
                    //break;
                    // prepare script write to file
                    spFinally += spScript + "/\r\n";

                    // for Package or Type there will be one more object called Package Body and Type Body
                    string bodyScript = null;
                    if (HomeDAService.PACKAGE.Equals(objType) || HomeDAService.TYPE.Equals(objType))
                    {
                        bodyScript = HomeDAService.getSPScript(connOri, item.object_name, objType + " BODY");
                        if (!String.IsNullOrEmpty(bodyScript))
                        {
                            spFinally += bodyScript + "/\r\n";
                        }
                    }
                }
                connOri.Close();

                wholeScript = spFinally + grantText;
                // write out the script into hard disk
                string fileName = fu.getCorrectFileName(extType.ToLower() + "db", modelName, range, objType.ToLower());
                return fu.writeScript2CorrectFile(wholeScript, pathOption, fileName, range);
            }
            catch (Exception innere)
            {
                connOri.Close();
                throw innere;
            }
        }
    }
}  