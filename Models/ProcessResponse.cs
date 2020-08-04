using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessingMicroservice.Models
{
    public class ProcessResponse
    {
        public int RequestId { get; set; }

        public int ProcessingCharge { get; set; }

        public int PackagingAndDeliveryCharge { get; set; }

        
        public DateTime? DateOfDelivery { get; set; }



    }
}
