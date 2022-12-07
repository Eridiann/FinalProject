using RootRPGProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RootRPGProject.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEquipmentPage : ContentPage
	{
		HttpClient httpClient;
		public AddEquipmentPage(HttpClient httpClient)
		{
			InitializeComponent();
			this.httpClient = httpClient;
			btnAdd.Clicked += BtnAdd_Clicked;
		}

		private async void BtnAdd_Clicked(object sender, EventArgs e)
		{
			if (name.Text == null || name.Text == "" ||
				description.Text == null || description.Text == "" ||
				load.Text == null || load.Text == "" ||
				value.Text == null || value.Text == "" ||
				range.Text == null || range.Text == "" ||
				maxWear.Text == null || maxWear.Text == "" ||
				harmType.Text == null || harmType.Text == "" ||
				harmValue.Text == null || harmValue.Text == "")
			{
				await DisplayAlert("Error", "Missing input fields", "ok");
				return;
			}

			Equipment equipment = new Equipment()
			{
				id = 0,
				Name = name.Text,
				Description = description.Text,
				Load = long.Parse(load.Text),
				Value = long.Parse(value.Text),
				Range = range.Text,
				MaxWear = long.Parse(maxWear.Text),
				Wear = 0,
				HarmType = harmType.Text,
				HarmValue = long.Parse(harmValue.Text)
			};
			string url = "https://saludd.azurewebsites.net/api/Equipment";
			string json = equipment.ToJson();
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PutAsync(url, content);

			if(response.IsSuccessStatusCode)
			{
				await DisplayAlert("Correct", "Equipment created", "OK");
				await Navigation.PopAsync();
				return;
			}
			else
			{
				await DisplayAlert("Error", "Failed to create equipment", "OK");
				return;
			}
		}
	}
}