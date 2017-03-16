using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SurveyDemo.Models;
using Newtonsoft.Json;//JsonConvert... need NuGet package

namespace SurveyDemo.Controllers.StellaDemo
{
    public class StellaApiController : ApiController
    {


        // POST api/stella
        public IHttpActionResult Post(Customer customer)
        {

            string g = Guid.NewGuid().ToString(); //.ToString().Substring(0, 8);


            List<Object> x = new List<Object>();
            x.Add(new { uuid = g });
            x.Add(customer);
            string y = JsonConvert.SerializeObject(x);
            return Ok(y);
        }


        //=============================================http://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c
        //string uuid = RandomString(3);
        //public static string RandomString(int length)
        //{
        //    Random random = new Random();
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //    return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        //}
        //============================http://stackoverflow.com/questions/1344221/how-can-i-generate-random-alphanumeric-strings-in-c ... https://msdn.microsoft.com/en-us/library/system.guid.newguid(v=vs.110).aspx
        //Guid g;
        //string g = Guid.NewGuid().ToString().Substring(0, 8);

    }
}
