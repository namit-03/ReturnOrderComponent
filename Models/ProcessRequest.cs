using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComponentProcessingMicroservice.Models;

namespace ComponentProcessingMicroservice.Models
{
    public class ProcessRequest
    {
         public string Name{ get; set; }

        public string ContactNumber{ get; set; }

        public int CreditCardNumber{ get; set; }

        public string ComponentType { get; set; }

        public string ComponentName { get; set; }

        public int Quantity { get; set; }



        public  Boolean IsPriorityRequest{ get; set; }

    }
}
