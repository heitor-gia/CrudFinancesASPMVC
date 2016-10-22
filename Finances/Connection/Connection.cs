using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finances.Connection
{
    public class ConnectionClass
    {
        MySqlConnection conn;
        MySqlCommand cmm;
        MySqlDataReader dr;
        private string strConnection
        {
            get
            {
                return "Server=localhost;" +
                        "Port=3306;" +
                        "Database=financesdb;" +
                        "Uid=heitor;" +
                        "Pwd = ;";
            }
        }

        public ConnectionClass()
        {
            conn = new MySqlConnection(strConnection);
        }

        private void Connect()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
        }

        private void Close()
        {
            if (conn.State != System.Data.ConnectionState.Closed)
                conn.Close();
        }

        public void ExecuteCommand(MySqlCommand pCmd)
        {
            Connect();
            pCmd.Connection = conn;
            pCmd.ExecuteNonQuery();
            Close();

        }

        public MySqlDataReader getDataReader(MySqlCommand cmm)
        {
            Connect();
            cmm.Connection = conn;
            dr = cmm.ExecuteReader();

            return dr;
        }
    }
}