using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using Xamarin.Essentials;
using System.Diagnostics;
using Newtonsoft.Json;
using Plugin.Messaging;
using System.Threading;
using Matcha.BackgroundService;

using SMS_Sender2.Models;

namespace SMS_Sender2
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //Флаг отправки СМС
        //public bool sendSMS = false;
        public SendSMS_Flag SendSMS { get; set; }

        //Время повтора операции
        public const int TimeToSleep = 5; //5 секунд

        /*
        private void BeginSendClicked(object sender, System.EventArgs e)
        {
            BeginSendButton.Text = "Идёт рассылка СМС";
            BeginSendButton.BackgroundColor = Color.Red;

            EndSendButton.Text = "Остановить рассылку СМС";
            EndSendButton.BackgroundColor = Color.AliceBlue;

            SendSMS = new SendSMS_Flag()
            {
                CanSend = true
            };
            //sendSMS = true;
            
        }
        private void EndSendClicked(object sender, System.EventArgs e)
        {
            SendSMS = new SendSMS_Flag()
            {
                CanSend = false
            };
            //sendSMS = false;

            EndSendButton.Text = "Рассылка СМС остановлена";
            EndSendButton.BackgroundColor = Color.Red;

            BeginSendButton.Text = "Начать рассылку СМС";
            BeginSendButton.BackgroundColor = Color.AliceBlue;
        }
        */
    }
}
