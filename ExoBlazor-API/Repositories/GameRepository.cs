using Dapper;
using ExoBlazor_API.Models;
using Microsoft.Data.SqlClient;

namespace ExoBlazor_API.Repositories
{
    public class GameRepository
    {
        private readonly SqlConnection _connection;

        public GameRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool Insert(Game game)
        {
            string sql = "INSERT INTO Games (Title, Synopsis, ReleaseYear) " +
                "VALUES (@title, @synopsis, @releaseYear)";

            return _connection.Execute(sql, new { title = game.Title, synopsis = game.Synopsis, releaseYear = game.ReleaseYear }) > 0;
        }

        public List<Game> GetAllGames()
        {
            string sql = "SELECT * FROM Games";

            return _connection.Query<Game>(sql).ToList();
        }

        public Game? GetGameById(int gameId)
        {
            string sql = "SELECT * FROM Games WHERE GameId = @gameId";

            return _connection.QuerySingleOrDefault<Game>(sql, new { gameId });
        }

        public bool UpdateGame(int gameId, Game game)
        {
            string sql = "UPDATE Games SET " +
                "Title = @title, " +
                "Synopsis = @synopsis, " +
                "ReleaseYear = @releaseYear " +
                "WHERE GameId = @gameId";

            return _connection.Execute(sql, new { gameId, title = game.Title, synopsis = game.Synopsis }) > 0;
        }

        public bool DeleteGame(int gameId)
        {
            string sql = "DELETE FROM Games WHERE GameId = @gameId";

            return _connection.Execute(sql, new { gameId }) > 0;
        }
    }
}
