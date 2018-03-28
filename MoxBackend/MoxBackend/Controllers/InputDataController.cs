using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoxBackend.Controllers
{
    public class InputDataController : ApiController
    {
        // GET: api/InputData
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/InputData/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/InputData
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/InputData/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/InputData/5
        public void Delete(int id)
        {
        }
    }
}
