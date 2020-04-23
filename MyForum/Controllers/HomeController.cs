using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyForum.Data;
using MyForum.Models;

namespace MyForum.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _db;
        private static int _tourTypePhotoId;

        public HomeController(ApplicationDbContext context)
        {
            _db = context;
        }

        public IActionResult Index() => View(_db.Topics.ToList());

        public IActionResult Rules()
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
