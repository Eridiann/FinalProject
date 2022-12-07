using Newtonsoft.Json;
using RootRPGProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RootRPGProject.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EquipmentPage : ContentPage
	{
		private readonly HttpClient httpClient;
		public EquipmentPage(HttpClient httpClient)
		{
			InitializeComponent();
			this.httpClient = httpClient;
			foreach (Equipment x in GetEquipment().Result)
			{
				var layout = new StackLayout() { Orientation = StackOrientation.Horizontal };
				layout.Children.Add(new Label() { Text= x.Name, VerticalOptions = LayoutOptions.Center});
				var cell = new ViewCell { View = layout };
				cell.Tapped += (sender, e) =>
				{
					Navigation.PushAsync(new EquipmentDetailPage(x.id, httpClient));
				};
				equipmentList.Add(cell);
			}
			btnAddEquipment.Clicked += (sender, e) =>
			{
				Navigation.PushAsync(new AddEquipmentPage(httpClient));
			};
		}

		private async Task<List<Equipment>> GetEquipment()
		{
			Console.WriteLine("getting equipment");
			string url = "https://saludd.azurewebsites.net/api/Equipment";
			var response = await httpClient.GetAsync(url).ConfigureAwait(false);
			Console.WriteLine("Fetched");
			if (response.IsSuccessStatusCode)
			{
				var result = response.Content.ReadAsStringAsync().Result;
				Console.WriteLine(result);
				return JsonConvert.DeserializeObject<List<Equipment>>(result);
			}
			return null;
		}

	}
}