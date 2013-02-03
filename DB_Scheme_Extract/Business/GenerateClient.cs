using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB_Scheme_Extract.Persistence;
using DB_Scheme_Extract.Utility;

namespace DB_Scheme_Extract.Business
{
    class GenerateClient
    {
        public string generateScripts(ConnectionProperty prop, string rootPath)
        {
            GenerateTemplate template = new ProcedureTemplate();
            ConnectionManager.initialDBConn(prop);
            // prepare Procedure to disk
            List<string> objNameList = template.getObjNameList(Constants.TYPE_PROCEDURE);
            string scriptStr = template.generateObjList(objNameList, Constants.TYPE_PROCEDURE);
            string outputPath = template.writeStringToDisk(rootPath, Constants.TYPE_PROCEDURE, scriptStr, prop.userID);
            // end of Procedure
            return outputPath;
        }
    }
}
