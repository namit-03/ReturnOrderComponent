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
using ReturnOrderPortal.Models;
using Microsoft.AspNetCore.Authorization;

namespace ComponentProcessingMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentProcessingMicroserviceController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ComponentProcessingMicroserviceController));

        readonly int Limit = 50000;

        public static ProcessRequest RequestObject = new ProcessRequest();

        public static ProcessResponse ResponseObject = new ProcessResponse();

         public DateTime DeliveryDate()
        {
            DateTime date = DateTime.Now;
            if (RequestObject.IsPriorityRequest == true && RequestObject.ComponentType == "Integral")
            {
                return date.AddDays(2);
            }
            else
            {
                return date.AddDays(5);
            }
        }
        public int PackagingDelivery(string Item, int Count)
        {
            var PackigingDeliveryCharge = "";
            var query = "?item=" + Item + "&count=" + Count;
            HttpClient client = new HttpClient();
            HttpResponseMessage result = client.GetAsync("https://localhost:44348/GetPackagingDeliveryCharge" + query).Result;


            if (result.IsSuccessStatusCode)
            {
                PackigingDeliveryCharge = result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                PackigingDeliveryCharge = "0";
            }
            int charge = int.Parse(PackigingDeliveryCharge);
            return charge;
        }

        public string CardDetails(CardDetails details)
        {
            PaymentDetails Response;
            var data = JsonConvert.SerializeObject(details);
            var value = new StringContent(data, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                var response = client.PostAsync("https://localhost:44360/api/ProcessPayment", value).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Response = JsonConvert.DeserializeObject<PaymentDetails>(result);
                    if (Response.Message == "Successful")
                    {
                        return "Successful";
                    }
                    else
                    {
                        return "Failed";
                    }
                }
                else
                {
                    return "Failed";
                }
            }
        }
        public int ProcessId()
        {
            int n = 0;
            Random _random = new Random();
            n = _random.Next(500, 1000);
            return n;
        }

        public int ProcessingCharge(string Workflow)
        {
            int ProcessingCharge = 0;
            Workflow = RequestObject.ComponentType;
            if (Workflow == "Integral")
            {
                IntegralWorkflow integral = new IntegralWorkflow();
                ProcessingCharge = integral.ProcessingCharge(RequestObject.IsPriorityRequest);
            }
            else
            {
                AccessoryWorkflow accessory = new AccessoryWorkflow();
                ProcessingCharge = accessory.ProcessingCharge(false);
            }
            return ProcessingCharge;
        }

        // GET: api/ComponentProcessingMicroservice/obj
        [HttpGet]
        [Authorize]
        
        public string GetRequest(string json)
        {
            
            _log4net.Info("GetRequest() called with json input");
            RequestObject = JsonConvert.DeserializeObject<ProcessRequest>(json);

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
            int Processing = ProcessId();



            ResponseObject = new ProcessResponse
            {
                RequestId = Processing,
                ProcessingCharge = ProcessingCharge(RequestObject.ComponentType),
                PackagingAndDeliveryCharge = PackagingDelivery(RequestObject.ComponentType, RequestObject.Quantity),
                DateOfDelivery = DeliveryDate()

            };

            var ResponseString = JsonConvert.SerializeObject(ResponseObject);
            return ResponseString;

        }

        // [HttpPost("{message}")]
        [HttpPost]
        public string GetUserMessage(Submission message)
        {
            _log4net.Info("GetUserMessage() called with user message as input");
            if (message.Result == "True")
            {
                CardDetails detail = new CardDetails()
                {
                    CreditCardNumber = RequestObject.CreditCardNumber,
                    CreditLimit = Limit,
                    ProcessingCharge = ResponseObject.ProcessingCharge + ResponseObject.PackagingAndDeliveryCharge
                };
                var result = CardDetails(detail);
                if (result == "Successful")
                {
                    return "Success";
                }
                else
                {
                    return "Failed";
                }
            }
            else
            {
                return "Payment not initiated";
            }
        } 
    }
}
