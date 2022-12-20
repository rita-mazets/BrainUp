using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrainUp.Data;
using BrainUp.Models;
using BrainUp.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using BrainUp.StoredProcedure;
using Stripe;
using Price = BrainUp.Models.Price;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace BrainUp.Controllers
{
    public class CourcesController : Controller
    {
        private readonly BrainUpBdContext _context;
        private readonly IWebHostEnvironment _environment;

        public CourcesController(BrainUpBdContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        
        public async Task<IActionResult> Index(int? categoryId)
        {
            ViewData["Categories"] = _context.Categories.Where(c => string.IsNullOrEmpty(c.Name) == false);
            ViewData["CurrentCategory"] = categoryId ?? 0;
            IQueryable<Cource> brainUpBdContext;
            if (categoryId == null)
            {
                brainUpBdContext = _context.Cources.Include(c => c.Category).Include(c => c.Language).Include(c => c.Level).Include(c => c.Teacher).Include(c => c.Price).Include(c => c.Ranks);
            }
            else 
            {
                brainUpBdContext = _context.Cources.Include(c => c.Category).Include(c => c.Language).Include(c => c.Level).Include(c => c.Teacher).Include(c => c.Price).Where(c => c.CategoryId == categoryId).Include(c => c.Ranks);
            }

            Dictionary<int, int> starsDict = new();
            foreach (var course in brainUpBdContext)
            {
                if (course.Ranks.Any())
                {
                    starsDict[course.Id] = (int)course.Ranks.Average(c => c.Value);
                }
                else 
                {
                    starsDict[course.Id] = 0;
                }
            }

            ViewData["starsDict"] = starsDict;


            
            return View(await brainUpBdContext.ToListAsync());
        }

        


        public async Task<IActionResult> Details(int? id, string? activity)
        {
            if (id == null || _context.Cources == null)
            {
                return NotFound();
            }

            var cource = await _context.Cources
                .Include(c => c.Category)
                .Include(c => c.Language)
                .Include(c => c.Level)
                .Include(c => c.Teacher)
                .Include(c => c.Price)
                .Include(c => c.Students)
                .Include(c => c.Feedbacks)
                .Include(c => c.Ranks)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (cource.Price.Price1 > 0.01m || cource.Price.IsPaid == false)
            {

                var currency = _context.Currencies.FirstOrDefault(c => c.Symbol == cource.Price.CurrencySymbol);

                var priceInUsd = (decimal)currency.Usdequivalent * cource.Price.Price1;
                long amount = (long)(priceInUsd * 100);
                var priceString = String.Format("{0:.##}", priceInUsd);

                ViewData["amount"] = amount;
                ViewData["priceInUsd"] = priceString;
            }

            ViewData["activity"] = activity;
            ViewData["IsRegister"] = false;

            if (cource.Ranks.Any())
            {
                var star = (int)cource.Ranks.Average(c => c.Value);
                ViewData["Star"] = star;
            }

            if (cource.Students.Any(s => s.Email == User.Identity.Name))
            {
                ViewData["IsRegister"] = true;
            }

            if (cource == null)
            {
                return NotFound();
            }

            return View(cource);
        }

        public async Task<IActionResult> Study(int? id, int? menuId, int? submenuId, string? number, int? taskId, int? contentId, bool? isTrue, string? trueAnswer, int? myAnswer)
        {
            if (id == null || _context.Cources == null)
            {
                return NotFound();
            }

            var cource = await _context.Cources
                .Include(c => c.Category)
                .Include(c => c.Language)
                .Include(c => c.Level)
                .Include(c => c.Teacher)
                .Include(c => c.Price)
                .Include(c => c.Students)
                .Include(c => c.Menus)
                .FirstOrDefaultAsync(m => m.Id == id);

            double point = -1;

            if (taskId != null)
            {
                var progress = await _context.UserProgresses.FirstOrDefaultAsync(p => p.TaskId == (int)taskId);
                if (progress != null)
                {
                    point = (double)progress.Point;
                }            }

            ViewData["taskId"] = taskId;
            ViewData["contentId"] = contentId;
            ViewData["isTrue"] = isTrue ?? false;
            ViewData["trueAnswer"] = trueAnswer;
            ViewData["myAnswer"] = myAnswer ?? 0;
            ViewData["point"] = point;


            if (cource == null)
            {
                return NotFound();
            }

            return View(cource);
        }

        [HttpGet]
        public async Task<IActionResult> Fill(int id, int? menuId, int? submenuId, string? number, int? taskId, int? contentId)
        {
            if (id == null || _context.Cources == null)
            {
                return NotFound();
            }

            var cource = await _context.Cources.Include(c => c.Menus).FirstOrDefaultAsync(c => c.Id == id);

            ViewData["menu"] = menuId;
            ViewData["submenu"] = submenuId;
            ViewData["number"] = number;
            ViewData["task"] = taskId ?? 0;
            ViewData["content"] = contentId ?? 0;


            if (cource == null)
            {
                return NotFound();
            }

            return View(cource);
        }

        public IActionResult Create()
        {
            ViewData["CategoryName"] = new SelectList(_context.Categories, "Name", "Name");
            ViewData["LanguageName"] = new SelectList(_context.Languages, "Symbol", "Symbol");
            ViewData["LevelName"] = new SelectList(_context.Levels, "Name", "Name");
            ViewData["Currency"] = new SelectList(_context.Currencies, "Symbol", "Symbol");
            return View();
        }

        // POST: Cources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourceModel model)
        {
            var cource = new Cource();
            if (ModelState.IsValid)
            {
                if (model.StartDate > model.EndDate)
                {
                    ModelState.AddModelError("EndDate", "End date must be more or equal start date.");
                    return View(cource);
                }


                if (model.Image != null)
                {
                    var fileName = $"{model.Name.Replace(" ", "")}" + $"_{model.Teacher}"+ Path.GetExtension(model.Image.FileName);
                    cource.Image = fileName;
                    var path = Path.Combine(_environment.WebRootPath, "img", fileName);
                    using (var fStream = new FileStream(path, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fStream);
                    }
                    await _context.SaveChangesAsync();
                }


                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == model.Category);
                var level    = await _context.Levels.FirstOrDefaultAsync(c => c.Name == model.Level);
                var language = await _context.Languages.FirstOrDefaultAsync(c => c.Symbol == model.Language);
                var teacher  = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);

                cource.Name = model.Name;
                cource.CategoryId = category.Id;
                cource.StartDate = model.StartDate;
                cource.EndDate = model.EndDate;
                cource.ShotDiscription = model.ShotDiscription;
                cource.Discription = model.Discription;
                cource.StorageLink = model.StorageLink;
                cource.TeacherId = teacher.Id;
                cource.LevelId =  level.Id;
                cource.LanguageId = language.Id;

                var price = new Price();
                price.IsPaid = model.Price.Price != 0;
                price.Price1 = model.Price.Price;
                price.CurrencySymbol = model.Price.Currency;

                _context.Prices.Add(price);
                await _context.SaveChangesAsync();

                price = await _context.Prices.FirstOrDefaultAsync(p => p.IsPaid ==price.IsPaid && p.Price1 == price.Price1 && p.CurrencySymbol == p.CurrencySymbol);
                cource.PriceId = price.Id;
                _context.Cources.Add(cource);


                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(cource);
        }

        [HttpGet]
        public async Task<IActionResult> Register(int? id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            var cource = await _context.Cources.FirstOrDefaultAsync(u => u.Id == id);


            //user.Cources.Add(cource);
            cource.Students.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction("Study", new { id = id});
            
        }

       


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRank(Rank rank)
        {
            if (rank == null || rank.CourceId == null)
            {
                return NotFound();
            }

            await _context.Ranks.AddAsync(rank);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = rank.CourceId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFeedback(Feedback feedback)
        {
            if (feedback == null || feedback.CourceId == null)
            {
                return NotFound();
            }

            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = feedback.CourceId });
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cources == null)
            {
                return NotFound();
            }

            var cource = await _context.Cources.Include(c => c.Category)
                .Include(c => c.Language)
                .Include(c => c.Level)
                .Include(c => c.Teacher)
                .Include(c => c.Price)
                .FirstOrDefaultAsync(m => m.Id == id);
            var model = new CourceModel();
            model.Copy(cource);

            
            if (cource == null)
            {
                return NotFound();
            }



            ViewData["CategoryName"] = new SelectList(_context.Categories, "Name", "Name", cource.Category);
            ViewData["LanguageName"] = new SelectList(_context.Languages, "Symbol", "Symbol", cource.Language);
            ViewData["LevelName"] = new SelectList(_context.Levels, "Name", "Name", cource.Level);
            ViewData["Currency"] = new SelectList(_context.Currencies, "Symbol", "Symbol");
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  CourceModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            
            if (ModelState.IsValid)
            {
                if (model.StartDate > model.EndDate)
                {
                    ModelState.AddModelError("EndDate", "End date must be more or equal start date.");
                    return View(model);
                }

                var cource = await model.CopyToCource(_environment);

                try
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == model.Category);
                    var level = await _context.Levels.FirstOrDefaultAsync(c => c.Name == model.Level);
                    var language = await _context.Languages.FirstOrDefaultAsync(c => c.Symbol == model.Language);
                    var price = await _context.Prices.FirstOrDefaultAsync(c => c.Id == model.PriceId);

                    cource.CategoryId = category.Id;
                    cource.LevelId = level.Id;
                    cource.LanguageId = language.Id;
                    cource.PriceId = price.Id;

                    price.Price1 = model.Price.Price;
                    price.CurrencySymbol = model.Price.Currency;
                    price.IsPaid = model.Price.Price > 0.0001m;

                    _context.Prices.Update(price);

                    _context.Update(cource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourceExists(cource.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryName"] = new SelectList(_context.Categories, "Name", "Name");
            ViewData["LanguageName"] = new SelectList(_context.Languages, "Symbol", "Symbol");
            ViewData["LevelName"] = new SelectList(_context.Levels, "Name", "Name");
            ViewData["Currency"] = new SelectList(_context.Currencies, "Symbol", "Symbol");
            return View(model);
        }

        // GET: Cources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cources == null)
            {
                return NotFound();
            }

            var cource = await _context.Cources
                .Include(c => c.Category)
                .Include(c => c.Language)
                .Include(c => c.Level)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cource == null)
            {
                return NotFound();
            }

            return View(cource);
        }

        // POST: Cources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cources == null)
            {
                return Problem("Entity set 'BrainUpBdContext.Cources'  is null.");
            }
            var cource = await _context.Cources.FindAsync(id);
            if (cource != null)
            {
                _context.Cources.Remove(cource);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourceExists(int id)
        {
          return _context.Cources.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> TeacherCourses()
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity.Name);
            var courses = _context.Cources.Where(x => x.TeacherId == user.Id);
            ViewData["Teacher"] = true;

            return View("Index", await courses.ToListAsync());
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> StudentCourses()
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity.Name);
            var cources =  _context.Cources.Where(x => x.Students.Contains(user));

            return View( cources);
        }

        public async Task<IActionResult> Charge(string stripeEmail, string stripeToken, long price, int courceId)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = price,
                Description = "Test Payment",
                Currency = "usd",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail
            });

            if (charge.Status == "succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                return await Register(courceId);

            }
            else
            {
                return Content("Error with payment");
            }
        }
    }
}
