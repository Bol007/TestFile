using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestFile1;

namespace TestAPI.Areas.V1.Controllers
{
    public class HomesController : ApiController
    {
        // GET api/<controller>

        [HttpGet()]
        [Route("api/Homes/{id}", Name = "GetAll")]
        public IEnumerable<CBS_LN_APP> GetAll(int id)
        {
            var model = new Model1();
            //var idata = model.Database.SqlQuery<CBS_LN_APP>("select top 100000 * from CBS_LN_APP").ToArray();
            var idata = model.CBS_LN_APP.AsNoTracking().OrderBy(q => q.CBS_APP_NO).Skip(0).Take(id).ToList();

            return idata;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}