using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AspAPI.Controllers
{
    public class DepartmentController : Controller
    {
        readonly HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:44366//api/")
        };

        public JsonResult LoadDepartment()
        {
            IEnumerable<Department> datadept = null;
            var responseTask = client.GetAsync("Department");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Department>>();
                readTask.Wait();
                datadept = readTask.Result;
            }
            else
            {
                datadept = Enumerable.Empty<Department>();
                ModelState.AddModelError(string.Empty, "Sorry Server Error, Try Again");
            }

            return new JsonResult { Data = datadept, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult InsertorUpdate(Department department)
        {
            var myContent = JsonConvert.SerializeObject(department);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            if(department.Id == 0)
            {
                var result = client.PostAsync("Department", byteContent).Result;
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; //Return if Success
            }
            else
            {
                var result = client.PutAsync("Department/" + department.Id, byteContent).Result;
                return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public async Task<JsonResult> GetById(int Id)
        {
            HttpResponseMessage response = await client.GetAsync("Department");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsAsync<IList<Department>>();
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
            var Result = client.DeleteAsync("Department/" + Id).Result;
            return new JsonResult { Data = Result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Department
        public ActionResult Index()
        {
            return View(LoadDepartment());
        }
    }
}