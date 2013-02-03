using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB_Scheme_Extract.Utility;

namespace DB_Scheme_Extract.ObjectTemp
{
    abstract class GenerateFactory
    {
        abstract string genScript(string objName);
        public static GenerateFactory getObjectInstanceByType(string objType)
        {
            switch (objType)
            {
                case Constants.TYPE_FUNCTION:
                    return new GenerateFunction();
                case Constants.TYPE_TABLE:
                    return new GenerateTable();
                case Constants.TYPE_PROCEDURE:
                    return new GenerateProcedure();
                default:
                    throw new NotImplementedException("Oracle DB didn't have such object type: "+objType);
            }

        }
    }
}
