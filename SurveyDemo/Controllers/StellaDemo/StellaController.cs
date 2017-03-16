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
            string g = Guid.NewGuid().ToString();
            Random ran = new Random(); int ranInt = ran.Next(1, 6);
            string[] rewards = {"Low Rating", "Thumbs Up", "Coffee", "Lunch", "Day Off"};
           
            SurveyReturned feedback = new SurveyReturned { uuid=g, rating=ranInt.ToString(), reward=rewards[ranInt-1]};
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