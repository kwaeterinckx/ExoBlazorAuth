using ExoBlazor_FE.Models;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ExoBlazor_FE.Pages
{
    public partial class Users
    {
        public List<UserModel> AllUsers { get; set; } = new List<UserModel>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        async Task LoadData()
        {
            string token = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            AllUsers = await HttpClient.GetFromJsonAsync<List<UserModel>>("User");
        }
    }
}
