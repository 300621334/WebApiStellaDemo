using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveyDemo.Models;
using System.Net.Http;//for HttpClient
using Newtonsoft.Json;//JsonConvert

/*https://msdn.microsoft.com/en-us/library/jj574232(v=vs.113).aspx
 * Lazy-Loading=when a navigation-prop is called eg. Blogs.Posts, it auto-loads ALL the Post-objs/entities.
 * Eager-Loading=by using .Include("Posts") or .Include(b=>b.Posts), it loads ALL Posts under a Blog. But we cannot filter Poats, ALL of them will be loaded.
 * Explicit-Loading=To be able to filter Posts, we need "explicit Loading" eg. can db.Posts.Reference("Blog")/(p=>p.Blogs) if nav-prop Blogs inside Posts ref to just one obj/entity, or if nav-prop refs to a collection(>1 objs) then ctx.Blogs.Collection("Posts")/(b=>b.Posts).Query().Where(p=>p.Tags.Contains("searchWord")).Load() ... to just count rel rows eout loading .Query().Count(); .Query gives us the underlying SQL so we can apply filters to that.
 To include all objs/entities related to ctx.Object(s), use .Include(). But a related-"navigation"-prop must be there in definition of class for obj we are querying */

namespace SurveyDemo.Controllers
{
    public class HomeController : Controller
    {
        #region Replace this with DB
        //List<Interaction> interactions = new List<Interaction> { 
        //     new Interaction(){contactID=1, channel="phone", country="Canada", custID=1, empID=1, surveySent=0}
        //    ,new Interaction(){contactID=2, channel="phone", country="Canada", custID=2, empID=2, surveySent=1}
        //    ,new Interaction(){contactID=3, channel="phone", country="Canada", custID=3, empID=3, surveySent=0}
        //};
        //List<Customer> customers = new List<Customer> { 
        //     new Customer(){ custID=1, name="Mani", email="abc@xyz.com"}
        //    ,new Customer(){ custID=2, name="John", email="def@xyz.com"}
        //    ,new Customer(){ custID=3, name="Paul", email="ghi@xyz.com"}
        //};
        //List<Employee> employees = new List<Employee> { 
        //     new Employee(){ empID=1, name="aaa", email="123@xyz.com"}
        //    ,new Employee(){ empID=2, name="bbb", email="456@xyz.com"}
        //    ,new Employee(){ empID=3, name="ccc", email="789@xyz.com"}
        //};
        #endregion


        public ActionResult Index()
        {//make an HttpClient call to Api GET() in ValuesController & let that do same job as ToBeSent() is doing. Then .ReasAsAsync<> and return .Result;
            return View();
        }

        public ActionResult ToBeSent() //browser repeatedly made some requests; to stop add  this to webConfig "<add key="vs:EnableBrowserLink" value="false" />" : http://stackoverflow.com/questions/19917595/net-localhost-website-consistently-making-get-arterysignalr-polltransport-long
        {
            IList<InteractView> toBeSent = null;
            using (SurveyEntities ctx = new SurveyEntities())
            {
                
                //toBeSent = ctx.Interacts.Where(s => s.uuid == null).ToList<Interact>(); //"EntityCommandExecutionException" when the underlying storage provider could not execute the specified command
                toBeSent = ctx.Interacts.AsNoTracking().Where(i => i.uuid == null).Select(i => new InteractView() //.AsNoTracking() just so the ctx will not track(cache) the entities returned, performance boast for read-only entities. It does that for lifetime of ctx ie. using(){} block
                {
                Customer = ctx.Customers.Where(c=>c.custId == i.Customer_custId).Select(c=>c.Name).FirstOrDefault(),
                Agent = ctx.Employees.Where(e=>e.empId==i.Employee_empId).Select(e=>e.Name).FirstOrDefault(),
                 InteractId = i.interactId,
                  surveySent = i.uuid==null?"No":"Yes",
                   uuid = i.uuid==null?"N/A":i.uuid.ToString()
                }).ToList<InteractView>(); //"EntityCommandExecutionException" when the underlying storage provider could not execute the specified command
                
                
                return View(toBeSent);
            }   
            //var toBeSent = interactions.Where(s => s.surveySent == 0);
            //return View(toBeSent);
        }

        public ActionResult Create(int contactID)//create an Edit form so agent can verify detail of customer//when "Send Survey" btn clicked, a GET request is send. (bcoz it's just a hyperling, NOT a submit btn on any FORM)
        {
            //var interact = interactions.Where(s => s.contactID == contactID).FirstOrDefault();
            //var customer = customers.Where(s => s.custID == interact.custID).FirstOrDefault(); //replace e EF context.DbSetName.Where(...)
            CustView c = null;
            using (SurveyEntities ctx = new SurveyEntities())
            {
                int x  = ctx.Interacts.Where(s=>s.interactId==contactID).Select(s=>s.Customer_custId).FirstOrDefault();
                c= ctx.Customers.Where(s => s.custId == x).Select(s => new CustView
                {
                    interactId = contactID,//from param
                    custID = s.custId,
                    name = s.Name,
                    email = s.Email
                }).FirstOrDefault();
            }
            return View(c);
        }

        [HttpPost] //when "Submit/Send" btn clicked on EDIT-view. Since it is a FROM, a POST rqst sent along e values from ALL controls/input-fields. That data binds to Customer object
        public string Create(CustView customer)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55911/api/StellaApi");  //this 2nd POST doesn't come from browser, rather internally ein webServer. So browser does NOT show it. Only later RedirectToAction("ToBeSent"); is displayed as a POST call to http://localhost:55911 e HTML in response body

                //var postTask = client.PostAsJsonAsync<Customer>("values", customer);
                var postTask = client.PostAsJsonAsync<CustView>("StellaApi", customer);// empty str also works ("", customer);
                postTask.Wait();

                var response = postTask.Result;
                if(response.IsSuccessStatusCode)
                {
                    //var x = response.Content.ReadAsAsync<CustView>();//this does NOT return a string, rather a Task
                    var x = response.Content.ReadAsAsync<List<Object>>();
                    x.Wait();//wait for TASK to complete
                    //CustView y = x.Result;//Task returns a string
                    List<Object> y = x.Result;

                    UuidObj uuid = JsonConvert.DeserializeObject<UuidObj>(y.ElementAt(0).ToString());
                    string z = JsonConvert.SerializeObject(y);//I kept trying to deserial obj e err. Actually obj needs to be SERIAlised NOT other way around!!
                    //List<Object> list = JsonConvert.DeserializeObject<List<Object>>(y);
                    //UuidObj uuid = new UuidObj { uuid = list.ElementAt(0).ToString() };
                    
                    //string[] z = y.Split(',');
                    //UuidObj uuid = JsonConvert.DeserializeObject<UuidObj>(z[0]);
                    
                    
                    
                    
                    //string z = JsonConvert.SerializeObject(y);//can serial a List of multiple objs too //http://www.newtonsoft.com/json/help/html/SerializingCollections.htm
                    updateInteractDb(customer.interactId, uuid);
                    
                    return z;//in real life, save uuid to database here via EF context & DbSet

                    //return RedirectToAction("ToBeSent");//here a POST call made to ToBeSent() shown in browser as "http://localhost:55911" only 
                    //this "return" will exit Create() so remaining code never exe
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            return View().ToString();
        }

        private void updateInteractDb(int interactId, UuidObj uuid) //http://stackoverflow.com/questions/3642371/how-to-update-only-one-field-using-entity-framework
        {
            using(SurveyEntities db = new SurveyEntities())
            {
                var interaction = db.Interacts.FirstOrDefault(s=>s.interactId == interactId);
                interaction.uuid = uuid.uuid;//state=modified is tracked auto. Don't have to explicitly set the state.
                db.SaveChanges();
            }
        }

     

        public ActionResult AllSent()
        {
            IList<InteractView> allSent = null;
            using (SurveyEntities ctx = new SurveyEntities())
            {
                //toBeSent = ctx.Interacts.Where(s => s.uuid == null).ToList<Interact>(); //"EntityCommandExecutionException" when the underlying storage provider could not execute the specified command
                allSent = ctx.Interacts.Where(i => i.uuid != null).Select(i => new InteractView()
                {
                    Customer = ctx.Customers.Where(c => c.custId == i.Customer_custId).Select(c => c.Name).FirstOrDefault(),
                    Agent = ctx.Employees.Where(e => e.empId == i.Employee_empId).Select(e => e.Name).FirstOrDefault(),
                    InteractId = i.interactId,
                    surveySent = i.uuid == null ? "No" : "Yes",
                    uuid = i.uuid == null ? "N/A" : i.uuid.ToString()
                }).ToList<InteractView>(); //"EntityCommandExecutionException" when the underlying storage provider could not execute the specified command


                return View("ToBeSent", allSent);
            }  
        }
    }
}
