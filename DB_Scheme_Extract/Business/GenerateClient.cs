using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB_Scheme_Extract.Persistence;

namespace DB_Scheme_Extract.Business
{
    class GenerateClient
    {
        public void generateScripts(ConnectionProperty prop, string rootPath)
        {
            GenerateTemplate template = new ProcedureTemplate();
            template.initialConnProp(prop);
            // prepare Procedure to disk
            List<string> objNameList = template.getObjNameList(ProcedureTemplate.TEM_TYPE);
            string scriptStr = template.generateObjList(objNameList, ProcedureTemplate.TEM_TYPE);
            template.writeStringToDisk(rootPath, ProcedureTemplate.TEM_TYPE, scriptStr);
            // end of Procedure
        }
    }
}
