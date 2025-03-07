using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.Data.SqlClient;

namespace PerfilesWebApi.Models
{
    public class DAL
    {
        private readonly string _connectionString;

        public DAL()
        {
            //_connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PERFILES"].ConnectionString;
            _connectionString = "Data Source=localhost;Initial Catalog=PerfilesDB;Integrated Security=SSPI;TrustServerCertificate=True";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public DataTable ExecuteQuery(string query)
        {
            using (SqlConnection connection = GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public int ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
