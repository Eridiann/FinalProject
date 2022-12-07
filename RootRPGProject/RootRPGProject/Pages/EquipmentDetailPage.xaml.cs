using RootRPGProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RootRPGProject.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EquipmentDetailPage : ContentPage
	{
		HttpClient httpClient;
		Equipment equipment;
		public EquipmentDetailPage(long id, HttpClient httpClient)
		{
			this.httpClient = httpClient;
			InitializeComponent();
			GetData(id);
		}

		public async void GetData(long id)
		{
			string token = await Xamarin.Essentials.SecureStorage.GetAsync("Token");
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			string url = "https://saludd.azurewebsites.net/api/equipment/" + id.ToString();

			HttpResponseMessage response = await httpClient.GetAsync(url);
			if (response.IsSuccessStatusCode)
			{
				equipment = Equipment.FromJson(await response.Content.ReadAsStringAsync());
				name.Text = equipment.Name;
				description.Text = equipment.Description;
				load.Text = "load: " + equipment.Load.ToString();
				wear.Text = MakeWearString(equipment);
				harm.Text = equipment.HarmType + ": " + equipment.HarmValue;
				if (equipment.Tags == null) tags.Text = "No Tags";
				else tags.Text = string.Join("\n", equipment.Tags.Select(tag => tag.Name + "\n" + tag.Effect));
				if (equipment.WeaponSkills == null) weaponSkills.Text = "No weapon skills";
				else weaponSkills.Text = string.Join("\n", equipment.WeaponSkills);
				Delete.Clicked += DeleteEquipment;
				Update.Clicked += Update_Clicked;
			}
			else
			{
				description.Text = "Coundn't find equipment data";
				tags.Text = response.StatusCode.ToString();
				weaponSkills.Text = await response.Content.ReadAsStringAsync();
			}
		}

		private async void Update_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new EditPage(httpClient, equipment));
		}

		private async void DeleteEquipment(object sender, EventArgs e)
		{
			if(await DisplayAlert("Warning!", "Are you sure you want to delete this object?", "Yes", "No"))
			{
				string url = "https://saludd.azurewebsites.net/api/equipment/" + equipment.id.ToString();
				await httpClient.DeleteAsync(url);
				await Navigation.PopAsync();
			}
		}

		private string MakeWearString(Equipment equipment)
		{
			string result = "wear:";
			for (int i = 0; i < equipment.MaxWear; i++)
			{
				if (i < equipment.Wear)
				{
					result += " |x|";
				}
				else
				{
					result += " |-|";
				}
			}
			return result;
		}
	}
}