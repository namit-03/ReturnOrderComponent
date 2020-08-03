using ComponentProcessingMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessingMicroservice.Processing
{
    public class AccessoryWorkflow : IProcessing
    {
        public int ProcessingCharge(bool request)
        {
            return 300;
        }
    }
}
