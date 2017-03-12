using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SurveyDemo.Models;

namespace SurveyDemo.Controllers
{
    public class ValuesController : ApiController
    {
        // POST api/values
        public IHttpActionResult Post(SurveyReturned surveyReturned)
        {
            //save values from surveyReturned into DB usng Ef & DbSet
            return Ok("RBC successfully saved JSON");
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
