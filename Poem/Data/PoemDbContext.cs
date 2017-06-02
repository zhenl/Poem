using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Poem.Models;

namespace Poem.Data
{
    public class PoemDbContext:DbContext
    {
        public PoemDbContext(DbContextOptions<PoemDbContext> options)
            : base(options)
        {
        }

        public DbSet<PoemInfo> Poems { set; get; }

        public DbSet<PoetInfo> Poets { set; get; }


    }
}
