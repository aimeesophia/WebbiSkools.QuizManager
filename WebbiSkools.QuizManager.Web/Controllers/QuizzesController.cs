using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebbiSkools.QuizManager.Web.Data;
using WebbiSkools.QuizManager.Web.Models;

namespace WebbiSkools.QuizManager.Web.Controllers
{
    public class QuizzesController : Controller
    {
        private readonly QuizManagerContext _context;

        public QuizzesController(QuizManagerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View("Index", await _context.Quizzes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        [Authorize(Roles = "Edit")]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public async Task<IActionResult> Create(Quiz quiz)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(quiz);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View("Create", quiz);
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("Unable to save changes.", ex, null);
            }

            return View("Create", quiz);
        }

        [Authorize(Roles = "Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }
            return View("Edit", quiz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Edit")]
        public async Task<IActionResult> Edit(int id, Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var quizToUpdate = await _context.Quizzes
                    .Include(q => q.Questions)
                    .ThenInclude(q => q.Answers)
                    .FirstOrDefaultAsync(q => q.Id == id);

                if (await TryUpdateModelAsync<Quiz>(
                    quizToUpdate,
                    ""))
                {
                    try
                    {
                        await _context.SaveChangesAsync();

                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!QuizExists(quiz.Id))
                        {
                            return NotFound();
                        }
                        
                        ModelState.AddModelError("", "Unable to save changes. Please try again.");
                    }
                }
            }

            return View("Edit", quiz);
        }

        [Authorize(Roles = "Edit")]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (quiz == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed. Please try again.";
            }

            return View("Delete", quiz);
        }

        [Authorize(Roles = "Edit")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Quizzes.Remove(quiz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChnagesError = true });
            }
        }

        [ExcludeFromCodeCoverage]
        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }
    }
}
