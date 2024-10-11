using ExoBlazor_FE.Models;
using ExoBlazor_FE.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace ExoBlazor_FE.Pages
{
    public partial class Login
    {
        public LoginForm LoginForm { get; set; } = new LoginForm();

        public async Task SubmitForm()
        {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync("User/Login", LoginForm);

            if (!response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync("Error " + response.ReasonPhrase);
            }

            string token = await response.Content.ReadAsStringAsync();
            await JSRuntime.InvokeVoidAsync("localStorage.setItem", "token", token);
            ((MyStateProvider)AuthenticationStateProvider).NotifyUserChanged();
            Nav.NavigateTo("/");
        }
    }
}
