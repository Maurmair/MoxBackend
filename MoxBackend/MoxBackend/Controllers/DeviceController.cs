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
    public class DeviceController : ApiController
    {
        // GET: api/Device
        public ArrayList Get()
        {
            DevicePersistence dp = new DevicePersistence();
            return dp.getDevices();
        }

        // GET: api/Device/5
        public Device Get(String Id)
        {
            DevicePersistence dp = new DevicePersistence();
            Device device = dp.getDevice(Id);
            return device;
        }

        // POST: api/Device
        public HttpResponseMessage Post([FromBody]Device DeviceValue)
        {
            DevicePersistence dp = new DevicePersistence();
            String deviceId;
            deviceId = dp.saveDevice(DeviceValue);
            DeviceValue.DeviceId = deviceId;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("/{0}", deviceId));
            return response;
        }

        // PUT: api/Device/5
        public HttpResponseMessage Put(String Id, [FromBody]Device DeviceValue)
        {
            DevicePersistence dp = new DevicePersistence();
            bool recordExisted = false;
            recordExisted = dp.updateDevice(Id, DeviceValue);
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

        // DELETE: api/Device/5
        public HttpResponseMessage Delete(String Id)
        {
            DevicePersistence dp = new DevicePersistence();
            bool recordExisted = false;
            recordExisted = dp.deleteDevice(Id);
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
