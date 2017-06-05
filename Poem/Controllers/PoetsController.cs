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
    [Authorize]
    public class PoetsController : Controller
    {
        private PoemDbContext _dbContext;
        public PoetsController(PoemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/Poets
        [HttpGet]
        public IEnumerable<PoetInfo> Get()
        {
            
            return _dbContext.Poets.ToList();
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

        //// GET api/Poets/GetPoemsByAuthor/李白
        //[HttpGet("{name}")]
        //[ActionName("GetPoemsByAuthor")]
        //public IEnumerable<PoemInfo> GetPoemsByAuthor(string name)
        //{
        //    var poet = _dbContext.Poets.FirstOrDefault(p => p.Name == name);
        //   // _dbContext.Entry(poet).Collection(p => p.Poems).Load();
        //    return poet.Poems;
        //}
    }
}
