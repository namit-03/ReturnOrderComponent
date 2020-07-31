using ComponentProcessingMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessingMicroservice.Processing
{
    public class IntegralWorkflow : IProcessing
    {
    

        public int ProcessingCharge( ProcessRequest ob1, ProcessResponse ob2)
        {
            var res = ob2.ProcessingCharge;int NumberOfDays = 0;
            res = 500;
            if (ob1.isPriorityRequest == "highPriorityRequest")
            {
                NumberOfDays = 2;
                res = 500 + 200;
            }
            else
            {
                res = 500;
                NumberOfDays = 5;
            }
            return res;
        }
    }
}
