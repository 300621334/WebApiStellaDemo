using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using SurveyDemo.Models;

namespace SurveyDemo.Controllers.StellaDemo
{
    public class StellaController : Controller
    {
        // GET: /stella/index
        public ActionResult Index()
        {
            //string g = Guid.NewGuid().ToString();
           
            SurveyReturned feedback = new SurveyReturned { uuid=1234, rating=5, reward="coffee"};
            return View(feedback);
        }

        public string DataReturn(SurveyReturned surveyReturned)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55911/api/values");
                var postTask = client.PostAsJsonAsync<SurveyReturned>("", surveyReturned);
                postTask.Wait();
                var response = postTask.Result;
                if(response.IsSuccessStatusCode)
                {
                    var x = response.Content.ReadAsAsync<string>();
                    x.Wait();
                    return x.Result;
                }

            }
            return "Data Return to RBC was unsuccessful";
        }
    }
}