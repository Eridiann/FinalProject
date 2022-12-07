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
	public partial class EditPage : ContentPage
	{
		private readonly HttpClient httpClient;
		private Equipment equipment;
		public EditPage(HttpClient httpClient, Equipment equipment)
		{
			InitializeComponent();
			this.httpClient = httpClient;
			this.equipment = equipment;
			btnUpdate.Clicked += BtnUpdate_Clicked;
			
			name.Text = equipment.Name;
			description.Text = equipment.Description;
			load.Text = equipment.Load.ToString();
			value.Text = equipment.Value.ToString();
			range.Text = equipment.Range;
			maxWear.Text = equipment.MaxWear.ToString();
			wear.Text = equipment.Wear.ToString();
			harmType.Text = equipment.HarmType;
			harmValue.Text = equipment.HarmValue.ToString();
		}

		private async void BtnUpdate_Clicked(object sender, EventArgs e)
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

			Equipment update = new Equipment()
			{
				id = equipment.id,
				Name = name.Text,
				Description = description.Text,
				Load = long.Parse(load.Text),
				Value = long.Parse(value.Text),
				Range = range.Text,
				MaxWear = long.Parse(maxWear.Text),
				Wear = long.Parse(wear.Text),
				HarmType = harmType.Text,
				HarmValue = long.Parse(harmValue.Text)
			};
			string url = "https://saludd.azurewebsites.net/api/Equipment";
			string json = update.ToJson();
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await httpClient.PutAsync(url, content);

			if (response.IsSuccessStatusCode)
			{
				await DisplayAlert("Correct", "Equipment updated", "OK");
				await Navigation.PopAsync();
				return;
			}
			else
			{
				await DisplayAlert("Error", "Failed to update equipment", "OK");
				return;
			}
		}
	}
}