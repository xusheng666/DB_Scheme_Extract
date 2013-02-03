using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using DB_Scheme_Extract.Persistence;

namespace DB_Scheme_Extract.Persistence
{
    public class ConnectionManager
    {
        public ConnectionManager(ConnectionProperty inputProp)
        {
            prop = inputProp;
            initialDBConn();
        }

        public const string CONN_CLOSED = "Closed";
        public const string CONN_OPEN = "Open";

        private static List<OracleConnection> connPool = new List<OracleConnection>();
        private static ConnectionProperty prop = new ConnectionProperty();

        private static void initialDBConn()
        {
            for (int i = 0; i < 5; i++)
            {
                newConnection();
            }
        }

        public static OracleConnection getConnection()
        {
            OracleConnection conn = null;

            foreach (var item in connPool)
            {
                if (item.State.ToString() == CONN_OPEN)
                {
                    conn = item;
                    break;
                }
            }
            if (conn == null)
            {
                newConnection();
            }
            return conn;
        }

        private static void newConnection()
        {
            try
            {
                string connURL = "Data Source=" + prop.dataSource + "; User ID=" + prop.userID + "; Password=" + prop.password + ";";
                OracleConnection conn = new OracleConnection(connURL);
                connPool.Add(conn);
            }
            catch (Exception)
            {
                throw;
            } 
        }
    }
}