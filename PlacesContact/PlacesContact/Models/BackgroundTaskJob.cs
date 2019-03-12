using Matcha.BackgroundService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlacesContact.Models
{
    public class BackgroundTaskJob : IPeriodicTask
    {
        public TimeSpan Interval { get; set; }
        public BackgroundTaskJob(int seconds)
        {
            Interval = TimeSpan.FromSeconds(seconds);
        }        
        
        public async Task<bool> StartJob()
        {
            MessagingCenter.Send<object, string>(this, "BackgroundTask", "Background running");
            return true; //return false when you want to stop or trigger only once
        }
    }
}
