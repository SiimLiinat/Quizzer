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
    public class QuizzerAnswerController : Controller
    {
        private readonly AppDbContext _context;

        public QuizzerAnswerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: QuizzerAnswer
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.QuizzerAnswers.Include(q => q.Answer).Include(q => q.Quizzer);
            return View(await appDbContext.ToListAsync());
        }

        // GET: QuizzerAnswer/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizzerAnswer = await _context.QuizzerAnswers
                .Include(q => q.Answer)
                .Include(q => q.Quizzer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizzerAnswer == null)
            {
                return NotFound();
            }

            return View(quizzerAnswer);
        }

        // GET: QuizzerAnswer/Create
        public IActionResult Create()
        {
            ViewData["AnswerId"] = new SelectList(_context.Answers, "Id", "AnswerText");
            ViewData["QuizzerId"] = new SelectList(_context.Quizzers, "Id", "Id");
            return View();
        }

        // POST: QuizzerAnswer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnswerId,QuizzerId,Id")] QuizzerAnswer quizzerAnswer)
        {
            if (ModelState.IsValid)
            {
                quizzerAnswer.Id = Guid.NewGuid();
                _context.Add(quizzerAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnswerId"] = new SelectList(_context.Answers, "Id", "AnswerText", quizzerAnswer.AnswerId);
            ViewData["QuizzerId"] = new SelectList(_context.Quizzers, "Id", "Id", quizzerAnswer.QuizzerId);
            return View(quizzerAnswer);
        }

        // GET: QuizzerAnswer/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizzerAnswer = await _context.QuizzerAnswers.FindAsync(id);
            if (quizzerAnswer == null)
            {
                return NotFound();
            }
            ViewData["AnswerId"] = new SelectList(_context.Answers, "Id", "AnswerText", quizzerAnswer.AnswerId);
            ViewData["QuizzerId"] = new SelectList(_context.Quizzers, "Id", "Id", quizzerAnswer.QuizzerId);
            return View(quizzerAnswer);
        }

        // POST: QuizzerAnswer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AnswerId,QuizzerId,Id")] QuizzerAnswer quizzerAnswer)
        {
            if (id != quizzerAnswer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quizzerAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizzerAnswerExists(quizzerAnswer.Id))
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
            ViewData["AnswerId"] = new SelectList(_context.Answers, "Id", "AnswerText", quizzerAnswer.AnswerId);
            ViewData["QuizzerId"] = new SelectList(_context.Quizzers, "Id", "Id", quizzerAnswer.QuizzerId);
            return View(quizzerAnswer);
        }

        // GET: QuizzerAnswer/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizzerAnswer = await _context.QuizzerAnswers
                .Include(q => q.Answer)
                .Include(q => q.Quizzer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quizzerAnswer == null)
            {
                return NotFound();
            }

            return View(quizzerAnswer);
        }

        // POST: QuizzerAnswer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var quizzerAnswer = await _context.QuizzerAnswers.FindAsync(id);
            _context.QuizzerAnswers.Remove(quizzerAnswer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizzerAnswerExists(Guid id)
        {
            return _context.QuizzerAnswers.Any(e => e.Id == id);
        }
    }
}
