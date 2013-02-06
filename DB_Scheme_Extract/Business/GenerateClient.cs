using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB_Scheme_Extract.Persistence;
using DB_Scheme_Extract.Utility;
using System.Windows.Forms;

namespace DB_Scheme_Extract.Business
{
    class GenerateClient
    {
        public List<string> generateScripts(ConnectionProperty prop, string rootPath, ProgressBar fileBar)
        {
            List<string> pathList = new List<string>();
            ConnectionManager.initialDBConn(prop);
            // loop every type 
            generateObjByType(prop, rootPath, pathList, fileBar);
            // end of loop
            return pathList;
        }

        private void generateObjByType(ConnectionProperty prop, string rootPath, List<string> pathList, ProgressBar fileBar)
        {
            int i = 1;
            foreach (var type in Constants.typeList)
            {
                int cnt = 0; i++;
                GenerateTemplate template = TemplateFactory.getInstance(type);
                List<string> objNameList = template.getObjNameList(type);
                cnt = objNameList.Count();
                string scriptStr = template.generateObjList(objNameList, type);
                string path = template.writeStringToDisk(rootPath, type, scriptStr, prop.userID);

                pathList.Add(path);
                int incre = (cnt == 0) ? 100 / 7 : 100 / 7 / cnt;
                fileBar.Increment(incre*i);
            }
        }
    }
}
