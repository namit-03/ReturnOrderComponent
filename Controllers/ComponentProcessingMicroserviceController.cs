using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ComponentProcessingMicroservice.Models;

namespace ComponentProcessingMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentProcessingMicroserviceController : ControllerBase
    {
        // GET: api/ComponentProcessingMicroservice
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ComponentProcessingMicroservice/5
        [HttpGet("ProcessDetail")]
        public ProcessResponse Get(ProcessRequest)
        {
            
        }

        // POST: api/ComponentProcessingMicroservice
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/ComponentProcessingMicroservice/5
        [HttpPut("CompleteProcessing")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
