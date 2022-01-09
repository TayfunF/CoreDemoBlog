using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class BlogController : Controller
    {
       
        private readonly Context _context;
        BlogManager bm;
        public BlogController(Context context)
        {
            _context = context;
            bm = new BlogManager(new EfBlogRepository(_context));
        }

        public IActionResult Index()
        {
            var values = bm.GetList();
            return View(values);
        }
    }
}
