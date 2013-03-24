using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB_Scheme_Extract.Persistence;
using Oracle.DataAccess.Client;
using DB_Scheme_Extract.Utility;
using System.Data;

namespace DB_Scheme_Extract.Business
{
    abstract class GenerateTemplate
    {
        public const string RETCHAR = "\r\n";
        // Step 1: get all object name list for the object type
        public List<string> getObjNameList(string objType)
        {
            List<string> objNameList = OracleClient.queryObjNameList(objType);
            return objNameList;
        }

        // Step 2: gen the script by object type, defer to sub class implemented.
        abstract public string generateObjList(List<string> objNameList, string objType);

        // Step 3: write the generated script into rootpath
        public string writeStringToDisk(string rootPath, string objType, string scriptStr, string userID)
        {
            FileUtil fu = new FileUtil();
            string fileName = userID.ToLower() + "-db-ddl-" + Constants.exeSeqDict[objType];
            fu.setRootPath(rootPath);

            string outputFileName = fu.writeScript2CorrectFile(scriptStr, fileName);
            return outputFileName;
        }
    }
}
