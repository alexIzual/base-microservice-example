using Dapper;
using Npgsql;

namespace Microservice.First.Migrations.Common
{
    public class MigrationTool
    {
        public static void UpsertDb(string connectionString, string dbname)
        {
            using var connection = new NpgsqlConnection(connectionString);

            connection.Open();
            if (!IsDbExists(connection, dbname))
            {
                CreateDb(connection, dbname);
            }
        }

        private static bool IsDbExists(NpgsqlConnection connection, string dbname)
        {
            var query = $"SELECT EXISTS(SELECT 1 FROM pg_catalog.pg_database WHERE datname = '{dbname}');";
            var result = connection.ExecuteScalar<bool>(query);
            return result;
        }

        private static void CreateDb(NpgsqlConnection connection, string dbname)
        {
            var query = $"CREATE DATABASE {dbname};";
            connection.Execute(query);
        }

        public static void CreateUserAndGrantPrivileges(string conn, string username, string dbname)
        {

        }
    }
}
