using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using System.Net.Http.Headers;




namespace InstitutoApp.ViewModels.Login
{
    public partial class RegistrarseViewModel : ObservableObject
    {
        private readonly FirebaseAuthClient _client;
        private const string FirebaseApikey = "AIzaSyAJu02w6o9rVt0aau1BjkQMsWyh_sgnRa4";
        private const string RequestUri = "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=" + FirebaseApikey;
        public IRelayCommand RegistrarseCommand { get; }

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string verifyPassword;

        public RegistrarseViewModel()
        {
            RegistrarseCommand = new RelayCommand(Registrarse);
        }

        private async void Registrarse()
        {
           
        }

        public static async Task SendVerificationEmailAsync(string idToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent("{\"requestType\":\"VERIFY_EMAIL\",\"idToken\":\"" + idToken + "\"}");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync(RequestUri, content);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
