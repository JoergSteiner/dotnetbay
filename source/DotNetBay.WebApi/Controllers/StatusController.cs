using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DotNetBay.WebApi.Controllers
{
    public class StatusController : ApiController
    {
        [HttpGet]
        [Route("api/status")]
        public IHttpActionResult GetStatus()
        {
            return this.Ok("Yes");
        }

        public string Get()
        {
            return "hi";
        }
    }
}
