using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Poem.Data;

namespace Poem.Models
{
    [Table(name: "Peom")]
    public class PoemInfo
    {
        [Key]
        public int PoemId { get; set; }

        [Column("PoetID")]
        public int PoetId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Volumn { get; set; }

        public string Num { get; set; }

        
        [ForeignKey("PoetId")]
        public virtual PoetInfo Poet { get; set; }
    }
}
