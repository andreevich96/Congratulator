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

        private static string _relation;
        private static string _name;
        private static int _page;
        private static SortState _sortOrder = SortState.DateOfBirthAsc;

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
        public async Task<IActionResult> Index(string relation,
            string name,
            int page = 1,
            SortState? sortOrder = null)
        {
            _relation = string.IsNullOrWhiteSpace(relation) || relation.Contains("Все")
                ? _relation
                : relation;
            _name = name;

            _page = page == 1 && page == 0
                ? _page
                : page;

            _sortOrder = !sortOrder.HasValue
                ? _sortOrder
                : sortOrder.Value;

            int pageSize = 10;

            IQueryable<Birthday> birthdays = _db.Birthdays;


            if (birthdays != null && !string.IsNullOrWhiteSpace(_relation))
            {
                birthdays = birthdays.Where(p => p.Relationship == _relation);
            }
            if (!String.IsNullOrEmpty(_name))
            {
                birthdays = birthdays.Where(p => p.Name.Contains(_name));
            }

            switch (_sortOrder)
            {
                case SortState.DateOfBirthDesc:
                    birthdays = birthdays.OrderByDescending(s => s.DateOfBirth.Month).ThenByDescending(s => s.DateOfBirth.Day);
                    break;
                case SortState.NameAsc:
                    birthdays = birthdays.OrderBy(s => s.Name);
                    break;
                case SortState.NameDesc:
                    birthdays = birthdays.OrderByDescending(s => s.Name);
                    break;
                case SortState.RelationshipAsc:
                    birthdays = birthdays.OrderBy(s => s.Relationship);
                    break;
                case SortState.RelationshipDesc:
                    birthdays = birthdays.OrderByDescending(s => s.Relationship);
                    break;
                default:
                    birthdays = birthdays.OrderBy(x => x.DateOfBirth.Month).ThenBy(x => x.DateOfBirth.Day);
                    break;
            }


            var count = await birthdays.CountAsync();
            birthdays = birthdays.Skip((_page - 1) * pageSize).Take(pageSize);

            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, _page, pageSize),
                SortViewModel = new SortViewModel(_sortOrder),
                FilterViewModel = new FilterViewModel(_db.Birthdays.ToList(), _relation, _name),
                Birthdays = await birthdays.ToListAsync()
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
