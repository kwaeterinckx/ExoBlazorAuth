using ExoBlazor_FE.Models;
using System.Net.Http.Json;

namespace ExoBlazor_FE.Pages
{
    public partial class Register
    {
        public RegisterForm RegisterForm { get; set; } = new RegisterForm();

        public async Task SubmitForm()
        {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync("User/Register", RegisterForm);

            if (!response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync("Error " + response.ReasonPhrase);
            }

            Nav.NavigateTo("/");
        }
    }
}
