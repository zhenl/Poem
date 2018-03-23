using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poem.Data;
using Poem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Poem.Controllers
{
    [Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class PoemsController : Controller
    {
        private PoemDbContext _dbContext;
        public PoemsController(PoemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/values
        [HttpGet("{page}/{pagesize}")]
        public IEnumerable<PoemInfo> Get(int page,int pagesize)
        {
           
            return _dbContext.Poems.Skip(page* pagesize).Take(pagesize);
        }

        // GET api/values/5
        [HttpGet("{name}")]
        public PoemInfo Get(string name)
        {
            return _dbContext.Poems.FirstOrDefault(p => p.Title == name);
        }

        [HttpGet("{name}")]
        [ActionName("GetByAuthor")]
        public IEnumerable<PoemInfo> GetByAuthor(string name)
        {
            var poet = _dbContext.Poets.FirstOrDefault(p => p.Name == name);
            return _dbContext.Poems.Where(p => p.PoetId == poet.PoetId);
        }

        [HttpGet("{name}")]
        [ActionName("GetPoemsByAuthorCondition")]
        public PagedPoems GetPoemsByAuthor(string name, int pagesize, int index, string filter)
        {
            var poet = _dbContext.Poets.FirstOrDefault(p => p.Name == name);
            var Poems = from p in _dbContext.Poems where p.PoetId == poet.PoetId select p;
            if (!string.IsNullOrEmpty(filter))
            {
                Poems = from p in Poems where p.Title.Contains(filter) select p;
            }

            var ps = pagesize;
            if (ps <= 0)
            {
                ps = 50;
            }

            PagedPoems res = new PagedPoems();
            res.CurrentPage = index;
            res.PageSize = ps;
            res.Total = Poems.Count();

            res.Poems = Poems.Skip(index * ps).Take(ps);
            return res;
        }
    }
}
