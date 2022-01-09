using CoreDemo.Models;
using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryDAL _categories;
        

        public HomeController(ILogger<HomeController> logger, ICategoryDAL categories)
        {
            _logger = logger;
            _categories = categories;
        }

        public IActionResult Index()
        {
            //_categories.topla(21, 22);
            //_categories.AddCategory();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
