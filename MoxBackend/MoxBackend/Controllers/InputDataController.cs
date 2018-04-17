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
    public class InputDataController : ApiController
    {
        // GET: api/InputData
        public ArrayList Get()
        {
            InputDataPersistence ip = new InputDataPersistence();
            return ip.getAllInputData();
        }       

        public InputData Get(DateTime Id, String DeviceId)
        {
            InputDataPersistence ip = new InputDataPersistence();
            InputData inputData = ip.getInputData(Id, DeviceId);
            return inputData;
        }

        // POST: api/InputData
        public HttpResponseMessage Post([FromBody]InputData inputDataValue)
        {
            InputDataPersistence ip = new InputDataPersistence();
            String date;
            date = ip.saveInputData(inputDataValue);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("/{0}", date));
            return response;
        }

        // PUT: api/InputData/5
        public HttpResponseMessage Put(DateTime Id, [FromBody]InputData inputDataValue, String DeviceId)
        {
            InputDataPersistence ip = new InputDataPersistence();
            bool recordExisted = false;
            recordExisted = ip.updateInputData(Id, inputDataValue, DeviceId);
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

        // DELETE: api/InputData/5
        public HttpResponseMessage Delete(DateTime Id)
        {
            InputDataPersistence ip = new InputDataPersistence();
            bool recordExisted = false;
            recordExisted = ip.deleteInputData(Id);
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
