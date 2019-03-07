using Matcha.BackgroundService;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PlacesContact.Models
{
    public class BackgroundJobs : IPeriodicTask
    {
        public BackgroundJobs(int seconds)
        {
            Interval = TimeSpan.FromSeconds(seconds);
        }
        public TimeSpan Interval { get; set; }

        public async Task<bool> StartJob()
        {
            var isRunning = true;
            CrossLocalNotifications.Current.Show("Place contact", "Time: " + DateTime.Now.ToShortTimeString());
            return isRunning;
        }
    }
}
