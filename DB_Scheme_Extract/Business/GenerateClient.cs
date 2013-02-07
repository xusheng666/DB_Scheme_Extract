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
        public List<string> generateScripts(List<string> typeList, ConnectionProperty prop, string rootPath, ProgressBar fileBar)
        {
            List<string> pathList = new List<string>();
            ConnectionManager.initialDBConn(prop);
            // loop every type 
            generateObjByType(typeList, prop, rootPath, pathList, fileBar);
            // end of loop
            return pathList;
        }

        private void generateObjByType(List<string> typeList, ConnectionProperty prop, string rootPath, List<string> pathList, ProgressBar fileBar)
        {
            int i = 0;
            foreach (var type in typeList)
            {
                i++;
                GenerateTemplate template = TemplateFactory.getInstance(type);
                List<string> objNameList = template.getObjNameList(type);
                
                string scriptStr = template.generateObjList(objNameList, type);
                string path = template.writeStringToDisk(rootPath, type, scriptStr, prop.userID);

                pathList.Add(path);
                for (int j = 1; j < objNameList.Count()+1; j++)
                {
                    int incre = 100 / i / j;
                    fileBar.Increment(incre);
                }
               
            }
        }
    }
}
