using ExoBlazor_FE.Models;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ExoBlazor_FE.Pages
{
    public partial class Games
    {
        public List<GameModel> AllGames { get; set; } = new List<GameModel>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        async Task LoadData()
        {
            AllGames = await HttpClient.GetFromJsonAsync<List<GameModel>>("Game");
        }
    }
}
