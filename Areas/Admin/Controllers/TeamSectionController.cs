using Microsoft.AspNetCore.Mvc;
using WebApplication3.DAL;
using WebApplication3.Models;


namespace WebApplication3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamSectionController : Controller
    {
        AppDbContext _context;
        private readonly IWebHostEnvironment _enviroment;

        public TeamSectionController(AppDbContext context,IWebHostEnvironment enviroment)
        {
            _context = context;
            _enviroment = enviroment;
        }

        public IActionResult Index()
        {
           var teamSliders=_context.TeamSliders.ToList();
            return View(teamSliders);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TeamSlider teamSlider)
        {
            if (!ModelState.IsValid) { return View(); }

            string path = _enviroment.WebRootPath + @"\admin\upload\member\";
            string filename=Guid.NewGuid()+teamSlider.ImgFile.FileName;
            
            
            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {
                teamSlider.ImgFile.CopyTo(stream);
                teamSlider.ImgUrl = filename;
            }
            _context.TeamSliders.Add(teamSlider);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id) 
        {
            if (id == 0) { return View(); }
            var user=_context.TeamSliders.FirstOrDefault(x=>x.ID==id);
            
            string path = _enviroment.WebRootPath + @"\admin\upload\member\";
            FileInfo fileinfo = new FileInfo(path+user.ImgUrl);
            if (fileinfo.Exists)
            {
                fileinfo.Delete();
            }
            _context.TeamSliders.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var member = _context.TeamSliders.FirstOrDefault(x => x.ID == id);
            return View(member);
        }
        [HttpPost]
        public IActionResult Update(TeamSlider teamSlider)
        {
            if(!ModelState.IsValid) { return View(); }
           
            var member=_context.TeamSliders.FirstOrDefault(x=>x.ID==teamSlider.ID);
            string path = _enviroment.WebRootPath + @"\admin\upload\member\";
            string filename = Guid.NewGuid() + teamSlider.ImgFile.FileName;
            FileInfo fileinfo = new FileInfo(path + member.ImgUrl);
            if (fileinfo.Exists) 
            {
                fileinfo.Delete();
            }
            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {
                teamSlider.ImgFile.CopyTo(stream);
                member.ImgUrl = filename;
            }
            member.Fullname=teamSlider.Fullname;
            member.Position = teamSlider.Position;
            _context.SaveChanges();
            return RedirectToAction("Index");   
        }
    }
    
}
