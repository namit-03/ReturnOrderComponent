using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComponentProcessingMicroservice.Models;
namespace ComponentProcessingMicroservice.Models
{
    public class DetailDefectiveComponent
    {
        public string ComponenetType { get; set; }

        public string ComponentName { get; set; }

        public int Quantity { get; set; }
    }
}
