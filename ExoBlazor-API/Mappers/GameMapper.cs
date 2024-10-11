using ExoBlazor_API.Models;
using ExoBlazor_API.Models.DTOs;

namespace ExoBlazor_API.Mappers
{
    internal static class GameMapper
    {
        public static Game ToModel(this GameDTO game)
        {
            if (game is null) throw new ArgumentNullException(nameof(game));

            return new Game()
            {
                Title = game.Title,
                Synopsis = game.Synopsis,
                ReleaseYear = game.ReleaseYear
            };
        }
    }
}
