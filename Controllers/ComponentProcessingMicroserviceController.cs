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
using System.Text;
using ComponentProcessingMicroservice.Processing;

namespace ComponentProcessingMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentProcessingMicroserviceController : ControllerBase
    {
        ProcessRequest RequestObject = new ProcessRequest();

        ProcessResponse ResponseObject = new ProcessResponse();

        ComponentProcessingMicroserviceController obj = new ComponentProcessingMicroserviceController();
        // GET: api/ComponentProcessingMicroservice
        [HttpGet]
        public int GetProcessRequestObject(ProcessRequest ob)
        {
            return 0;
        }

        // GET: api/ComponentProcessingMicroservice/5
        [HttpGet("ProcessDetail")]
        public string GetRequest(string json)
        {
            string ComponentType = "";int ComponentQuantity = 0;

            int Processing = 0;
            var RequestObject = JsonConvert.DeserializeObject<ProcessRequest>(json);
       RequestObject = new ProcessRequest
            {
                Name = RequestObject.Name,
                ContactNumber = RequestObject.ContactNumber,
                CreditCardNumber = RequestObject.CreditCardNumber,
                ComponentType = RequestObject.ComponentType,
                ComponentName = RequestObject.ComponentName,
                Quantity = RequestObject.Quantity,
                IsPriorityRequest = RequestObject.IsPriorityRequest

            };
            ComponentType = RequestObject.ComponentType;
            ComponentQuantity = RequestObject.Quantity;
            Processing = ProcessId();
            DateTime date = DateTime.Now;
            ResponseObject = new ProcessResponse
            {
                RequestId = Processing,
                ProcessingCharge =ProcessingCharge(RequestObject.ComponentType),
                PackagingAndDeliveryCharge = SendResonse(ComponentType,ComponentQuantity),
                DateOfDelivery = date

            };

         string ResponseString= JsonConvert.SerializeObject(ResponseObject);
            return ResponseString;

        }

        private int SendResonse(string item, int count)
        {
            var PackigingDeliveryCharge = "";
            UriBuilder builder = new UriBuilder("");

            builder.Query = "item='Integral'&count='3'";
            HttpClient client = new HttpClient();
            HttpResponseMessage result = client.GetAsync(builder.Uri).Result;


            if (result.IsSuccessStatusCode)
            {
                PackigingDeliveryCharge = result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                PackigingDeliveryCharge = "";
            }
            int charge = int.Parse(PackigingDeliveryCharge);
            return charge;
        }



        [HttpPost("CardDetails")]
        private PaymentDetails CardDetails(CardDetails ob)
        {
            var CardDetails = "";
            PaymentDetails Response;
      

            CardDetails obj = new CardDetails
            {
                CreditCardNumber = 1234,
                CreditLimit = 500,
                ProcessingCharge = 100

            };




            var data = JsonConvert.SerializeObject(obj);
            var value = new StringContent(data, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
              var  response = client.PostAsync("", value).Result;

                if (response.IsSuccessStatusCode)
                {
                    Response = JsonConvert.DeserializeObject<PaymentDetails>(CardDetails);


                }
                else
                {
                    Response = null;
                }
                return Response;
            }
        }

        public int ProcessId()
        {
            int n = 0;
            Random _random = new Random();
            n = _random.Next(500,1000);
            return n;
        }

        public int ProcessingCharge(string Workflow)
        {
            int ProcessingCharge = 0;
            if (Workflow == "IntegralWorkflow")
            {
                IntegralWorkflow ob = new IntegralWorkflow();
                ProcessingCharge= ob.ProcessingCharge(RequestObject);
            }
            else
            {
                AccessoryWorkflow ob1 = new AccessoryWorkflow();
                ProcessingCharge=ob1.ProcessingCharge(RequestObject);
            }
            return ProcessingCharge;
        }


        /*     public int RecieveResponse()
        {

C:\Users\Namit Pundir\source\repos\ComponentProcessingMicroservice\Controllers\ComponentProcessingMicroserviceController.cs
        }  */
    }
}