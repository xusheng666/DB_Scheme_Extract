using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB_Scheme_Extract.Utility;

namespace DB_Scheme_Extract.Business
{
    class TemplateFactory
    {
        public static GenerateTemplate getInstance(string type)
        {
            GenerateTemplate template = null;
            switch (type)
            {
                case Constants.TYPE_PROCEDURE:
                case Constants.TYPE_FUNCTION:
                case Constants.TYPE_TYPE:
                case Constants.TYPE_PACKAGE:
                    template = new ProcedureTemplate();
                    break;
                case Constants.TYPE_TABLE:
                    template = new TableTemplate();
                    break;
                case Constants.TYPE_TRIGGER:
                    template = new TriggerTemplate();
                    break;
                case Constants.TYPE_VIEW:
                    template = new ViewTemplate();
                    break;
                default:
                    throw new NotImplementedException("no such type template defined with name:"+ type);
            }

            return template;
        }
    }
}
