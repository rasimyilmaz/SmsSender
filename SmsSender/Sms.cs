using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsSender
{
    public class Sms
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
    public class SmsRequest
    {
        public string phonenumber { get; set; }
        public string message { get; set; }
    }
}
