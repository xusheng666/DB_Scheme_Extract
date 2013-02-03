using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Oracle.DataAccess.Client;

namespace DB_Scheme_Extract.Utility
{
    class PoolManager
    {
        private class PoolItem
        {
            public bool inUse = false;
            public Object item;
            public PoolItem(Object item) { this.item = item; }
        }
        private ArrayList items = new ArrayList();
        public void add(Object item)
        {
            items.Add(new PoolItem(item));
        }
        class EmptyPoolException : Exception { }
        public Object get()
        {
            for (int i = 0; i < items.Count; i++)
            {
                PoolItem pitem = (PoolItem)items[i];
                if (pitem.inUse == false)
                {
                    pitem.inUse = true;
                    return pitem.item;
                }
            }
            // Fail early:
            throw new EmptyPoolException();
            // return null; // Delayed failure
        }
        public void release(Object item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                PoolItem pitem = (PoolItem)items[i];
                if (item == pitem.item)
                {
                    pitem.inUse = false;
                    item = null;
                    return;
                }
            }
            throw new Exception(item + " not found");
        }
    }
    public class ConnectionPool
    {
        private static PoolManager pool = new PoolManager();
        
        public static void addConnections(int number, string connString)
        {
            for (int i = 0; i < number; i++)
            {
                pool.add(new OracleConnection(connString));
            }
        }
        public static OracleConnection getConnection()
        {
            return (OracleConnection)pool.get();
        }
        public static void releaseConnection(OracleConnection c)
        {
            pool.release(c);
        }
    }
}
