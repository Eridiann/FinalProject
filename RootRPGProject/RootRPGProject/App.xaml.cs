using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
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
			AppCenter.Start("android={06bb0e7d-60a8-4e28-8715-f976cd770f14};" +
				  "uwp={Your UWP App secret here};" +
				  "ios={Your iOS App secret here};" +
				  "macos={Your macOS App secret here};",
				  typeof(Analytics), typeof(Crashes));
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
