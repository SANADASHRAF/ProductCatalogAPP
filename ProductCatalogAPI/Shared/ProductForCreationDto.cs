using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductForCreationDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Duration { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CreatedByUserId { get; set; }
    }
}
