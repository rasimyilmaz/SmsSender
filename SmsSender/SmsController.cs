using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmsSender
{
    public class SmsController : ApiController
    {

        // GET api/sms
        public IEnumerable<string> Get()
        {
            return new string[] { "Hello", "World" };
        }

        // GET api/sms/1
        public int Get(int id)
        {
            return id;
        }

        
        // POST api/sms
        public void Post([FromBody] SmsRequest request)
        {
            Console.WriteLine("Post Message");
            Program.Send(request);
        }

        /*
        Sms[] smss = new Sms[]
        {
            new Sms{Id=1,PhoneNumber="05541461471",Message="Hello"},
            new Sms{Id=2,PhoneNumber="05541461471",Message="How are you ?"}
        };
        public IEnumerable<Sms> GetAllSmss()
        {
            return smss;
        }
        public Sms GetSmsById(int id)
        {
            var sms = smss.FirstOrDefault((p) => p.Id == id);
            if (sms == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return sms;
        }
        public IEnumerable<Sms> GetSmssByPhoneNumber(string phonenumber)
        {
            return smss.Where(p => string.Equals(p.PhoneNumber, phonenumber,
                    StringComparison.OrdinalIgnoreCase));
        }*/
    }
}
