using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsSender
{
    public class SmsRequest
    {
        public string phonenumber { get; set; }
        public string message { get; set; }
        public string id { get; set; }
        public string callbackurl { get; set; }
    }
    public class SmsResponse
    {
        public string id { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public string timestamp { get; set; }
    }
    public class SmsCallback
    {
        public string id { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public string callbackurl { get; set; }
        public string timestamp { get; set; }
    }
   
}
