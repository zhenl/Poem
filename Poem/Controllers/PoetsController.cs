using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poem.Data;
using Poem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Poem.Controllers
{
    [Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    //[Authorize]
    public class PoetsController : Controller
    {
        private PoemDbContext _dbContext;
        public PoetsController(PoemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/Poets
        [HttpGet]
        public PagedPoets Get(int pagesize, int index, string filter)
        {

            var poets =from p in _dbContext.Poets select p;
            if (!string.IsNullOrEmpty(filter))
            {
                poets = from p in poets where p.Name.Contains(filter) select p;
            }
            
            var ps = pagesize;
            if (ps <= 0)
            {
                ps = 50;
            }

            var res = new PagedPoets();
            res.CurrentPage = index;
            res.PageSize = ps;
            res.Total = poets.Count();

            res.Poets = poets.Skip(index * ps).Take(ps);
            return res;
        }

        // GET api/Poets/5
        [HttpGet("{id}")]
        public PoetInfo Get(int id)
        {
            return  _dbContext.Poets.Find(id);
        }

        // GET api/Poets/GetByName/李白
        [HttpGet("{name}")]
        [ActionName("GetByName")]
        public PoetInfo GetByName(string name)
        {
            var poet= _dbContext.Poets.FirstOrDefault(p => p.Name == name);
            //_dbContext.Entry(poet).Collection(p=>p.Poems).Load();
            //foreach (var poetPoem in poet.Poems)
            //{
            //    poetPoem.Poet = null;
            //}
            return poet;
        }

        // GET api/Poets/GetPoemsByAuthor/李白
        [HttpGet("{name}")]
        [ActionName("GetPoemsByAuthor")]
        public IEnumerable<PoemInfo> GetPoemsByAuthor(string name)
        {
            var poet = _dbContext.Poets.FirstOrDefault(p => p.Name == name);
            var Poems = from p in _dbContext.Poems where p.PoetId == poet.PoetId select p;
            
            return Poems;
        }

        [HttpGet("{name}")]
        [ActionName("GetPoemsByAuthorCondition")]
        public PagedPoems GetPoemsByAuthor(string name,int pagesize,int index,string filter)
        {
            var poet = _dbContext.Poets.FirstOrDefault(p => p.Name == name);
            var Poems = from p in _dbContext.Poems where p.PoetId == poet.PoetId select p;
            if (!string.IsNullOrEmpty(filter))
            {
                Poems=from p in Poems where p.Title.Contains(filter) select p;
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

        // GET api/Poets/GetPoemsByAuthor/李白
        [HttpGet("{name}/{title}")]
        [ActionName("GetPoemsByAuthorTitle")]
        public IEnumerable<PoemInfo> GetPoemsByAuthor(string name,string title)
        {
            var poet = _dbContext.Poets.FirstOrDefault(p => p.Name == name);
            var Poems = from p in _dbContext.Poems where p.PoetId == poet.PoetId && p.Title.Contains(title) select p;
            // _dbContext.Entry(poet).Collection(p => p.Poems).Load();
            return Poems;
        }
    }
}
