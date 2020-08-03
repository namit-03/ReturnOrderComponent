using ComponentProcessingMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessingMicroservice.Processing
{
    public class IntegralWorkflow : IProcessing
    {
    

        public int ProcessingCharge( ProcessRequest ob1)
        {
            int res = 0; 
            res = 500;
            if (ob1.IsPriorityRequest == true)
            {
                
                res = 500 + 200;
            }
            else
            {
                res = 500;
                
            }
            return res;
        }
    }
}
