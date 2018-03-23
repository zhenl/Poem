using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poem.Models
{
    public class PagedPoets
    {
        public IEnumerable<PoetInfo> Poets { get; set; }

        public int Total { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }
    }
}
