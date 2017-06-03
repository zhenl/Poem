using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Poem.Models
{
    [Table(name: "Poet")]
    public class PoetInfo
    {
        public PoetInfo()
        {
            //this.Poems=new HashSet<PoemInfo>();
        }
        [Key]
        public int PoetId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        //public virtual ICollection<PoemInfo> Poems { get; set; }
    }
}
