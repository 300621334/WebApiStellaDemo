using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SurveyDemo.Controllers.StellaDemo
{
    public class StellaApiController : ApiController
    {


        // POST api/stella
        public IHttpActionResult Post([FromBody]string value)
        {
            return Ok(value);
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
        string g = Guid.NewGuid().ToString().Substring(0, 8);

    }
}
