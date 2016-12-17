using System.Collections.Generic;
using System.Web.Http;
using System.Configuration;

namespace UniversityIot.VitocontrolApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new[]
            {
                ConfigurationManager.AppSettings["gatewayServiceUrl"],
                ConfigurationManager.AppSettings["userServiceUrl"]
            };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}