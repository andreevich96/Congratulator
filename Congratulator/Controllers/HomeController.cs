using Congratulator.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Network.Core.Services;
using Network.Core.Shared.Entities;
using Network.Core.ViewModels;
using Network.Infrastructure;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Congratulator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BirthdaysService _birthdaysService;
        private ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger
            , BirthdaysService birthdaysService
            , ApplicationDbContext context
            , IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _birthdaysService = birthdaysService;
            _db = context;
            _hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index(SortState sortOrder = SortState.DateOfBirthAsc, int page = 1 )
        {
            int pageSize = 10;
            IQueryable<Birthday> birthdays = _db.Birthdays;
            var count = await birthdays.CountAsync();
            var items = await birthdays.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Birthdays = items
            };

            //IQueryable<Birthday> birthdays = _db.Birthdays;
            ViewData["DateOfBirthSort"] = sortOrder == SortState.DateOfBirthAsc ? SortState.DateOfBirthDesc : SortState.DateOfBirthAsc;
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["RelationshipSort"] = sortOrder == SortState.RelationshipAsc ? SortState.RelationshipDesc : SortState.RelationshipAsc;

            viewModel.Birthdays = sortOrder switch
            {
                SortState.DateOfBirthDesc => viewModel.Birthdays.OrderByDescending(s => s.DateOfBirth),
                SortState.NameAsc => viewModel.Birthdays.OrderBy(s => s.Name),
                SortState.NameDesc => viewModel.Birthdays.OrderByDescending(s => s.Name),
                SortState.RelationshipAsc => viewModel.Birthdays.OrderBy(s => s.Relationship),
                SortState.RelationshipDesc => viewModel.Birthdays.OrderByDescending(s => s.Relationship),
                _ => viewModel.Birthdays.OrderBy(s => s.DateOfBirth),
            };
            return View(viewModel); 
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Birthday birthday)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (birthday.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(birthday.ImageFile.FileName);
                    string extansion = Path.GetExtension(birthday.ImageFile.FileName);
                    birthday.Photo = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extansion;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await birthday.ImageFile.CopyToAsync(filestream);
                    };
                }
                _db.Birthdays.Add(birthday);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Birthday birthday = await _db.Birthdays.FirstOrDefaultAsync(a => a.Id == id);
                if (birthday != null)
                    return View(birthday);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Birthday birthday)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (birthday.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(birthday.ImageFile.FileName);
                    string extansion = Path.GetExtension(birthday.ImageFile.FileName);
                    birthday.Photo = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extansion;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await birthday.ImageFile.CopyToAsync(filestream);
                    };
                }
                _db.Birthdays.Update(birthday);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Birthday birthday = await _db.Birthdays.FirstOrDefaultAsync(p => p.Id == id);
                if (birthday != null)
                    return View(birthday);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Birthday birthday = await _db.Birthdays.FirstOrDefaultAsync(p => p.Id == id);
                if (birthday != null)
                {
                    _db.Birthdays.Remove(birthday);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
