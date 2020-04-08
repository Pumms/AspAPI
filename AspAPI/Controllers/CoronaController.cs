using AspAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace AspAPI.Controllers
{
    public class CoronaController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://corona.lmao.ninja/")
        };

        // GET: Corona
        public ActionResult Index()
        {
            return View(LoadCorona());
        }

        public JsonResult LoadCorona()
        {
            IEnumerable<CoronaVM> coronas = null;
            var responseTask = client.GetAsync("countries");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<CoronaVM>>();
                readTask.Wait();
                coronas = readTask.Result;
            }
            else
            {
                coronas = Enumerable.Empty<CoronaVM>();
                ModelState.AddModelError(string.Empty, "Sorry Server Error, Try Again");
            }
            return new JsonResult { Data = coronas, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}