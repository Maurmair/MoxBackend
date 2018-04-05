using MoxBackend.Models;
using MoxBackend.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoxBackend.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TargetController : ApiController
    {
        // GET: api/Target
        public ArrayList Get()
        {
            TargetPersistence tp = new TargetPersistence();
            return tp.getTargets();
        }

        // GET: api/Target/5
        public Target Get(DateTime Id)
        {
            TargetPersistence tp = new TargetPersistence();
            Target target = tp.getTarget(Id);
            return target;
        }

        // POST: api/Target
        public HttpResponseMessage Post([FromBody]Target TargetValue)
        {
            TargetPersistence dp = new TargetPersistence();
            String date;
            date = dp.saveTarget(TargetValue);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("/{0}", date));
            return response;
        }

        // PUT: api/Target/5
        public HttpResponseMessage Put(DateTime Id, [FromBody]Target targetValue)
        {
            TargetPersistence tp = new TargetPersistence();
            bool recordExisted = false;
            recordExisted = tp.updateTarget(Id, targetValue);
            HttpResponseMessage response;
            if (recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        // DELETE: api/Target/5
        public HttpResponseMessage Delete(DateTime Id)
        {
            TargetPersistence tp = new TargetPersistence();
            bool recordExisted = false;
            recordExisted = tp.deleteTarget(Id);
            HttpResponseMessage response;
            if (recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }
}
