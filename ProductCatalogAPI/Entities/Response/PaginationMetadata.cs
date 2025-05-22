using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response
{
    public class PaginationMetadata
    {
        public int CurrentPage { get; set; }
        public int LastPage { get; set; }
        public int PerPage { get; set; }
        public int Total { get; set; }
    }
}
