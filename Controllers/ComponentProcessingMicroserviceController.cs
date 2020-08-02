using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ComponentProcessingMicroservice.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace ComponentProcessingMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentProcessingMicroserviceController : ControllerBase
    {
        // GET: api/ComponentProcessingMicroservice
        [HttpGet]
        public int GetProcessRequestObject(ProcessRequest ob)
        {
           return 0;
        }

        // GET: api/ComponentProcessingMicroservice/5
        [HttpGet("ProcessDetail")]
        public ProcessRequest GetRequest(ProcessRequest ob)
        {

            string JsonToString = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(item: new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(requestUri: "" + ob).Result;


                if (response.IsSuccessStatusCode)
                {
                    JsonToString = response.Content.ReadAsStringAsync().Result;
                }
                else
                    JsonToString = null;

            }
            List<ProcessRequest> Response = JsonConvert.DeserializeObject<List<ProcessRequest>>(JsonToString);
            foreach (ProcessRequest item in Response)
            {
                item.Name = ob.Name;
                item.ContactNumber = ob.ContactNumber;
                item.CreditCradNumber = ob.CreditCradNumber;
                item.ComponentType = ob.ComponentType;
                item.ComponentName = ob.ComponentName;
                item.Quantity = ob.Quantity;
                item.IsPriorityRequest = ob.IsPriorityRequest;
            }
            return ob;

        }
       
      
    }
}
