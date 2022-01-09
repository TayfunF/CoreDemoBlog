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
    public class CategoryController : Controller
    {
        private readonly Context _context;
        CategoryManager cm;
        public CategoryController(Context context)
        {
            _context = context;
            cm = new CategoryManager(new EfCategoryRepository(_context));
        }



        public IActionResult Index()
        {
            var values = cm.GetList();
            return View(values);
        }
    }
}
