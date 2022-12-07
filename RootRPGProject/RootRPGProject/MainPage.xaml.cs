using Newtonsoft.Json;
using RootRPGProject.Models;
using RootRPGProject.Pages;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RootRPGProject
{
	public partial class MainPage : ContentPage
	{
		HttpClient httpClient;
		public MainPage(HttpClient httpClient)
		{
			this.httpClient = httpClient;
			InitializeComponent();
			btnLogin.Clicked += BtnLogin_Clicked;
			btnRegister.Clicked += BtnRegister_Clicked;
		}

		private async void BtnRegister_Clicked(object sender, EventArgs e)
		{
			if (
				cellUsername.Text == null ||
				cellUsername.Text == "" ||
				cellPassword.Text == null ||
				cellPassword.Text == "" ||
				cellEmail.Text == null ||
				cellEmail.Text == ""
				)
			{
				await DisplayAlert("Error", "Missing fields", "OK");
				return;
			}

			User user = new User()
			{
				Username = cellUsername.Text,
				Password = cellPassword.Text,
				Email = cellEmail.Text,
			};
			string url = "https://saludd.azurewebsites.net/api/User/register";
			string json = user.ToJson();
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			Console.WriteLine(json.ToString());
			var response = await httpClient.PostAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				await DisplayAlert("Congratulations!", "Account created.", "OK");
			}
			else
			{
				await DisplayAlert("Error!", "Failed to create account.", "OK");
			}
		}

		private async void BtnLogin_Clicked(object sender, EventArgs e)
		{
			if (
				cellUsername.Text == null ||
				cellUsername.Text == "" ||
				cellPassword.Text == null ||
				cellPassword.Text == "" ||
				cellEmail.Text == null ||
				cellEmail.Text == ""
				)
			{
				await DisplayAlert("Error", "Missing fields", "OK");
				return;
			}

			User user = new User()
			{
				Username = cellUsername.Text,
				Password = cellPassword.Text,
				Email = cellEmail.Text,
			};
			string url = "https://saludd.azurewebsites.net/api/User/login";
			string json = user.ToJson();
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PostAsync(url, content);

			if (!response.IsSuccessStatusCode)
			{
				await DisplayAlert("Error", "Login Failed", "OK");
				Console.WriteLine(response.Content.ReadAsStringAsync().Result);
				return;
			}

			var result = response.Content.ReadAsStringAsync().Result;
			Token token = JsonConvert.DeserializeObject<Token>(result);
			string tokenStr = token.TokenToken.ToString();
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenStr);
			await SecureStorage.SetAsync("Token", tokenStr);

			await Navigation.PushAsync(new EquipmentPage(httpClient));
		}
	}
}
