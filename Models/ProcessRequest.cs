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

        public int ContactNumber{ get; set; }

        public int CreditCradNumber{ get; set; }

        public DetailDefectiveComponent DefectiveComponentDetail { get; set; }

        public string isPriorityRequest { get; set; }

    }
}
