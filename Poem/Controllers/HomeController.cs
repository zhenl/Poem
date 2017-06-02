using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poem.Data;
using Poem.Models;

namespace Poem.Controllers
{
    public class HomeController : Controller
    {
        private PoemDbContext _dbContext;
        public HomeController(PoemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Poems()
        {
            //var lst = new List<PoemInfo>
            //{
            //    new PoemInfo {Author = "李白", Title = "静夜思"},
            //    new PoemInfo {Author = "李白", Title = "望庐山瀑布"}
            //};

            var lst=_dbContext.Poems.ToList();

            return View(lst);
        }
    }
}
