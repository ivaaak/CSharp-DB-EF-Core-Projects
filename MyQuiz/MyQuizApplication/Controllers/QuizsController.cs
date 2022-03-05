using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyQuizApplication.Core.Contracts;
using MyQuizApplication.Core.Models;
using MyQuizApplication.Infrastructure.Data;
using MyQuizApplication.Infrastructure.Data.Models;

namespace MyQuizApplication.Controllers
{
    public class QuizsController : Controller
    {
        private readonly IQuizService quizService;

        public QuizsController(IQuizService _quizService)
        {
            quizService = _quizService;
        }

        // GET: Quizs
        public async Task<IActionResult> Index()
        {
            return View(await quizService.GetQuizzes());
        }

        // GET: Quizs/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var quiz = await _context.Quizzes
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (quiz == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(quiz);
        //}

        // GET: Quizs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quizs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] QuizModel quiz)
        {
            if (ModelState.IsValid)
            {
                await quizService.CreateQuiz(quiz);

                return RedirectToAction(nameof(Index));
            }

            return View(quiz);
        }

        // GET: Quizs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await quizService.GetById(id ?? 0);

            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // POST: Quizs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] QuizModel quiz)
        {
            if (id != quiz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if ((await quizService.Edit(quiz)) == false)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            return View(quiz);
        }

        // GET: Quizs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await quizService.GetById(id ?? 0);

            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // POST: Quizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await quizService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        //private bool QuizExists(int id)
        //{
        //    return _context.Quizzes.Any(e => e.Id == id);
        //}
    }
}
