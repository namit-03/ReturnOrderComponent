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
            int res = 300;
            if (request == true)
            {
                res += 200;
            }

            return res;
        }
    }
}
