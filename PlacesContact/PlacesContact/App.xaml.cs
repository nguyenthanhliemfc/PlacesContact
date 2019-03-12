using PlacesContact.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Matcha.BackgroundService;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PlacesContact
{
    public partial class App : Application
    {        
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            BackgroundAggregatorService.StopBackgroundService();
            BackgroundAggregatorService.Add(() => new BackgroundTaskJob(10)); //Task run every 10s
            BackgroundAggregatorService.StartBackgroundService();
        }

        protected override void OnSleep()
        {
            
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
