using API.Models;
using API.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AspAPI.Controllers
{
    public class DivisionController : Controller
    {
        readonly HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:44366/api/")
        };

        // GET: Division
        public ActionResult Index()
        {
            return View(LoadDivision());
        }

        public JsonResult LoadDivision()
        {
            IEnumerable<DivisionVM> datadivision = null;
            var responseTask = client.GetAsync("Division");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<DivisionVM>>();
                readTask.Wait();
                datadivision = readTask.Result;
            }
            else
            {
                datadivision = Enumerable.Empty<DivisionVM>();
                ModelState.AddModelError(string.Empty, "Sorry Server Error, Try Again");
            }

            return new JsonResult { Data = datadivision, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult InsertorUpdate(DivisionVM division)
        {
            var myContent = JsonConvert.SerializeObject(division);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            if (division.Id == 0)
            {
                var result = client.PostAsync("Division", byteContent).Result;
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; //Return if Success
            }
            else
            {
                var result = client.PutAsync("Division/" + division.Id, byteContent).Result;
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public async Task<JsonResult> GetById(int Id)
        {
            HttpResponseMessage response = await client.GetAsync("Division");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<IList<DivisionVM>>();
                var dept = data.FirstOrDefault(t => t.Id == Id);
                var json = JsonConvert.SerializeObject(dept, Formatting.None, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });
                return new JsonResult { Data = json, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return Json("Internal Server Error"); // return if Error
        }

        public JsonResult Delete(int Id)
        {
            var Result = client.DeleteAsync("Division/" + Id).Result;
            return new JsonResult { Data = Result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}