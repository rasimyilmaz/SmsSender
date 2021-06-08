using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmsSender
{
    public class SmsController : ApiController
    {
        public Robot RobotInstance = Robot.Instance;

        public SmsResponse Post([FromBody] SmsRequest request)
        {

            RobotInstance.PushRequest(request);
            File.AppendAllText("WriteLines.txt", DateTime.Now.ToString("HH:mm:ss.ffffff") + " - " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString() + " : Controller.Post\n");

            return new SmsResponse { code = 100, id = request.id, message = "İşlem sıraya alındı.",timestamp=DateTime.Now.ToString() };

        }
    }
}
