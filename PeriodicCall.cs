using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Matcha.BackgroundService;
using System.Diagnostics;
using System.Net.Http;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Plugin.Messaging;
using System.Threading;

using SMS_Sender2.Models;

namespace SMS_Sender2
{
    public abstract class PeriodicCall : IPeriodicTask
    {
        //Время повтора операции 5 секунд
        protected PeriodicCall(int seconds)
        {
            Interval = TimeSpan.FromSeconds(seconds);
        }
        public TimeSpan Interval { get; set; }

        public async Task<bool> StartJob()
        {
            // YOUR CODE HERE
            // THIS CODE WILL BE EXECUTE EVERY INTERVAL
            try
            {
                //Запуск рассылки СМС, Повтор каждые Interval
                await ExecuteLoadItemsCommand();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            //if (SendSMS.CanSend == true) return true;

            return true; //return false when you want to stop or trigger only once
        }

        HttpClient client = new HttpClient();
        Response response { get; set; }
        body Body { get; set; }
        private async Task<bool> ExecuteLoadItemsCommand()
        {
            //Получаем данные из БД
            body _body = await GetItemsAsync();
            if (_body.response == null) return false;

            try
            {
                foreach (Response row in _body?.response)
                {
                    if (row != null)
                    {
                        //Debug.WriteLine("row.IsSend = ");
                        //Debug.WriteLine(row.IsSend);
                        //Тип 3 - отправка СМС, ещё не отправлено
                        if ((row.Type == 3) && ((row.IsSend == "false") || (row.IsSend == null)))
                        {
                            try
                            {
                                string recipient = row.TelefonAddress;
                                string messageText = row.Text;

                                //Отправляем СМС
                                var smsMessenger = CrossMessaging.Current.SmsMessenger;
                                if (smsMessenger.CanSendSmsInBackground)
                                    smsMessenger.SendSmsInBackground(recipient, messageText);

                                //Отмечаем что СМС отправлено
                                Request Request = new Request()
                                {
                                    type = "doc",
                                    docID = 9,
                                    id = row.id
                                };
                                List<docColumns> docColumns = new List<docColumns>();
                                docColumns.Add(new docColumns
                                {
                                    Name = "IsSend",
                                    DataTypes = "BOOLEAN",
                                    Value = "1"
                                });
                                Request.docColumns = docColumns;
                                //Отправляем в БД признак отправки СМС 
                                await AddItemAsync(Request);
                            }
                            catch (FeatureNotSupportedException ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return true;
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        // Получение данных с сервера БД
        public async Task<body> GetItemsAsync()
        {
            Body = new body();
            if (!IsConnected)
            {
                Debug.WriteLine("Нет соединения с интернетом");
                return Body;
            }
            try
            {
                if (client.BaseAddress == null)
                {
                    //адрес сервера
                    string url = "http://192.168.0.105:3000";
                    //Debug.WriteLine("GetAddress");
                    //Debug.WriteLine(url);
                    client.BaseAddress = new Uri(url);
                }

                //Тип запроса
                string _type = "type=query";
                //Номер таблицы для чтения данных
                string _docID = "&docID=9";
                //Поле по которому фильтруем
                string _docColumn = "&docColumn=IsSend";
                //Значение поля фильтрации
                string _columnValue = "&columnValue=1";
                //Оператор сравнения
                string _op = "&op=not";
                //Собираем вместе
                string _url = "api/?" + _type + _docID + _docColumn + _columnValue + _op;

                //Для SQLlite
                //string _url = "api/?type=query&docID=9";
                //string _url = "api/?type=query&docID=9&docColumn=IsSend&columnValue=1&op=not";
                //Debug.WriteLine("_url === ");
                //Debug.WriteLine(_url);

                //Посылаем запрос                
                var json = await client.GetStringAsync($"/" + _url);

                Debug.WriteLine("json");
                Debug.WriteLine(json);

                if (json != "{\"response\":[]}")
                {
                    //Читаем тело ответа
                    var _Body = await Task.Run(() => JsonConvert.DeserializeObject<body>(json));
                    //Выводим на экран номер телефона отправляемого СМС
                    //LabelSend.Text = _Body.response[0]?.TelefonAddress;
                    Body = _Body;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return Body;
        }

        // Добавление или изменение данных на сервере
        public async Task AddItemAsync(Request Request)
        {
            if (!IsConnected)
            {
                Debug.WriteLine("Нет соединения с интернетом");
            }
            try
            {
                var serializedItem = JsonConvert.SerializeObject(Request);
                //Debug.WriteLine("!!! Data to add:");
                //Debug.WriteLine(serializedItem);

                HttpResponseMessage HttpResponse = await client.PostAsync($"api/", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                HttpContent responseContent = HttpResponse.Content;
                var json = await responseContent.ReadAsStringAsync();
                //Debug.WriteLine("!!! await responseContent.ReadAsStringAsync()=");
                //Debug.WriteLine(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
