using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    public class QuizzerController : Controller
    {
        private readonly AppDbContext _context;

        public QuizzerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Quizzer
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Quizzers.Include(q => q.AppUser).Include(q => q.Quiz);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Quizzer/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizzer = await _context.Quizzers
                .Include(q => q.AppUser)
                .Include(q => q.Quiz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizzer == null)
            {
                return NotFound();
            }

            return View(quizzer);
        }

        // GET: Quizzer/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id");
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Title");
            return View();
        }

        // POST: Quizzer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,QuizId,StartedAt,Points,Id")] Quizzer quizzer)
        {
            if (ModelState.IsValid)
            {
                quizzer.Id = Guid.NewGuid();
                _context.Add(quizzer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id", quizzer.AppUserId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Title", quizzer.QuizId);
            return View(quizzer);
        }

        // GET: Quizzer/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizzer = await _context.Quizzers.FindAsync(id);
            if (quizzer == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id", quizzer.AppUserId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Title", quizzer.QuizId);
            return View(quizzer);
        }

        // POST: Quizzer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,QuizId,StartedAt,Points,Id")] Quizzer quizzer)
        {
            if (id != quizzer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quizzer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizzerExists(quizzer.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.AppUsers, "Id", "Id", quizzer.AppUserId);
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Title", quizzer.QuizId);
            return View(quizzer);
        }

        // GET: Quizzer/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizzer = await _context.Quizzers
                .Include(q => q.AppUser)
                .Include(q => q.Quiz)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizzer == null)
            {
                return NotFound();
            }

            return View(quizzer);
        }

        // POST: Quizzer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var quizzer = await _context.Quizzers.FindAsync(id);
            _context.Quizzers.Remove(quizzer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizzerExists(Guid id)
        {
            return _context.Quizzers.Any(e => e.Id == id);
        }
    }
}
