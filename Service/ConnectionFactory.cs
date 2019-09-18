using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace IMS_Einleitung.Service
{
    class ConnectionFactory
    {
        public static SqlConnection GetConnection()
        {
            var connectionString =
                "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Example;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            return new SqlConnection(connectionString);
        }
    }
}
