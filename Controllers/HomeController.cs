using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication3.DAL;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var members=_context.TeamSliders.ToList();
            return View(members);
        }
    }
}
