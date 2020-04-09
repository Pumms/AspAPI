using API.Models;
using API.Repository;
using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace API.Controllers
{
    public class DivisionController : ApiController
    {

        DivisionRepository division = new DivisionRepository();

        [HttpGet]
        public IEnumerable<DivisionVM> Get()
        {
            return division.Get();
        }

        [HttpGet]
        [ResponseType(typeof(DivisionVM))]
        public async Task<IEnumerable<DivisionVM>> Get(int Id)
        {
            return await division.Get(Id);
        }

        public IHttpActionResult Post(DivisionVM divisions)
        {
            if (divisions.DivisionName != null && divisions.DivisionName != "")
            {
                //If condition success
                var post = division.Create(divisions); //Lakukan Input
                if (post > 0)
                {
                    return Ok("Division Added Succesfully!");
                }
            }
            return BadRequest("Failed to Add Division");
        }

        public IHttpActionResult Put(int Id, DivisionVM divisions)
        {
            if (divisions.DivisionName != null && divisions.DivisionName != "")
            {
                var put = (division.Update(Id, divisions));
                if (put > 0)
                {
                    return Ok("Division Update Succesfully!");
                }
            }
            return BadRequest("Failed to Update Division");
        }

        public IHttpActionResult Delete(int Id)
        {
            var del = division.Delete(Id);
            if (del > 0)
            {
                return Ok("Division Delete Succesfully!");
            }
            return BadRequest("Failed to Delete Division");
        }
    }
}
