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
            
        }

        protected override void OnSleep()
        {
            BackgroundAggregatorService.StopBackgroundService();
            BackgroundAggregatorService.Add(() => new BackgroundJobs(3));
            BackgroundAggregatorService.StartBackgroundService();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
