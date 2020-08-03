using ComponentProcessingMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessingMicroservice.Processing
{
    public class AccessoryWorkflow : IProcessing
    {
        public int ProcessingCharge( ProcessRequest ob1)
        {
           int NumberOfDays = 0;
            res = 300;
            if (ob1.IsPriorityRequest == true)
            {
                NumberOfDays = 2;
                res = 300 + 200;
            }
            else
            {
                res = 300;
                NumberOfDays =5;
            }
            return res;
        }
    }
}
