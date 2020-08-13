using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApp.Infra.Data.Dapper.DapperHelpers
{
    public interface IConnectionConfig
    {
        string GetConnection();
    }

    public class ConnectionConfig
    {
        public string ConnectionString { get; set; }
        public ConnectionConfig(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }


        public string GetConnection()
        {
            return ConnectionString;
        }
    }
}
