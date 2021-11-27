using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Matcha.BackgroundService;

namespace SMS_Sender2
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
            // Register Periodic Tasks //Время повтора операции 5 секунд
            BackgroundAggregatorService.Add(() => new PeriodicReadDB(5));

            //Start the background service
            BackgroundAggregatorService.StartBackgroundService();
        }

        protected override void OnSleep()
        {
            //BackgroundAggregatorService.StopBackgroundService();
        }

        protected override void OnResume()
        {
        }
    }
}
