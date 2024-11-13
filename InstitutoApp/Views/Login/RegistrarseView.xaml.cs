using Firebase.Auth;
using System.Net.Http.Headers;

namespace InstitutoApp.Views;

public partial class RegistrarseView : ContentPage
{
	private readonly FirebaseAuthClient _client;
	private const string FirebaseApikey = "AIzaSyAJu02w6o9rVt0aau1BjkQMsWyh_sgnRa4";
	private const string RequestUri = "https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key=" + FirebaseApikey;
	public RegistrarseView(FirebaseAuthClient firebaseAuthClient)
    {
        InitializeComponente();
		_client = firebaseAuthClient;
	}

  

	
}