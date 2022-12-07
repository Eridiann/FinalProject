using RootRPGProject.Pages;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RootRPGProject
{
	public partial class App : Application
	{
		private readonly HttpClient httpClient = new HttpClient();
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainPage(httpClient));
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
