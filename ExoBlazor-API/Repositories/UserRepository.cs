using Dapper;
using ExoBlazor_API.Models;
using Microsoft.Data.SqlClient;

namespace ExoBlazor_API.Repositories
{
    public class UserRepository
    {
        private readonly SqlConnection _connection;

        public UserRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool Register(string login, string email, string password)
        {
            string sql = "INSERT INTO Users (Login, Email, Password) " +
                "VALUES (@login, @email, @password)";

            return _connection.Execute(sql, new { login, email, password }) > 0;
        }

        public User? Login(string login, string password)
        {
            string sql = "SELECT * FROM Users WHERE Login = @login AND Password = @password";

            return _connection.QuerySingleOrDefault<User>(sql, new { login, password });
        }

        public List<User> GetUsers()
        {
            string sql = "SELECT * FROM Users";

            return _connection.Query<User>(sql).ToList();
        }

        internal string? GetPassword(string login)
        {
            string sql = "SELECT Password FROM Users WHERE Login = @login";

            return _connection.QuerySingleOrDefault<string>(sql, new { login });
        }

        internal User? GetProfileByLogin(string login)
        {
            string sql = "SELECT * FROM Users WHERE Login = @login";

            return _connection.QuerySingleOrDefault<User>(sql, new { login });
        }
    }
}
