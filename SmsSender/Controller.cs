using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmsSender
{
    class Controller : IDisposable
    {
        public Robot RobotInstance;
        public HttpClient httpClient;
        public IDisposable webApp;
        public bool isRunning;
        private bool disposedValue;

        public Controller() {
            isRunning = false;
            httpClient = new HttpClient();
        }
        ~Controller()
        {
            File.AppendAllText("WriteLines.txt", "Closing\n");
            httpClient.Dispose();
            if (isRunning)
            {
                Stop();
            }
        }
        public void Start()
        {
            RobotInstance = Robot.Instance;
            RobotInstance.RequestListEvent += StackChanged;
            httpClient = new HttpClient();
            webApp = WebApp.Start<Startup>("http://localhost:12345");
            isRunning = true;
        }
        public void StackChanged(object sender, Robot.EventArgs e)
        {
            if (e.Code == 1)
            {
                System.Threading.Thread.Sleep(1000);
                File.AppendAllText("WriteLines.txt",  DateTime.Now.ToString("HH:mm:ss.ffffff") + " - " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()+" : Stack Changed function called \n");

                SmsRequest request = RobotInstance.PopRequest();
                SmsCallback callback= RobotInstance.Send(request);
                SendCallback(callback).Wait();
            }
        }
        public async Task<HttpResponseMessage> SendCallback(SmsCallback callback)
        {
            Uri address = new Uri(callback.callbackurl);
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("id", callback.id),
                new KeyValuePair<string, string>("code", callback.code.ToString()),
                new KeyValuePair<string, string>("message", callback.message),
                new KeyValuePair<string, string>("timestamp", callback.timestamp)
            });
            var response = await httpClient.PostAsync(address, formContent);
            return response;
        }
        public void Stop()
        {
            webApp.Dispose();
            RobotInstance.Clear();
            isRunning = false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: yönetilen durumu (yönetilen nesneleri) atın
                }

                // TODO: yönetilmeyen kaynakları (yönetilmeyen nesneleri) serbest bırakın ve sonlandırıcıyı geçersiz kılın
                // TODO: büyük alanları null olarak ayarlayın
                disposedValue = true;
            }
        }

        // // TODO: sonlandırıcıyı yalnızca 'Dispose(bool disposing)' içinde yönetilmeyen kaynakları serbest bırakacak kod varsa geçersiz kılın
        // ~Controller()
        // {
        //     // Bu kodu değiştirmeyin. Temizleme kodunu 'Dispose(bool disposing)' metodunun içine yerleştirin.
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Bu kodu değiştirmeyin. Temizleme kodunu 'Dispose(bool disposing)' metodunun içine yerleştirin.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
