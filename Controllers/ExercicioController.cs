using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StrongFit.Models;

namespace StrongFit.Controllers
{
    [Authorize(Roles = "Personal")] // Restringe o acesso para Personal
    public class ExercicioController : Controller
    {
        private readonly Context _context;

        public ExercicioController(Context context)
        {
            _context = context;
        }

        // GET: Exercicio
        public async Task<IActionResult> Index()
        {
            return View(await _context.Exercicios.ToListAsync());
        }

        // GET: Exercicio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exercicio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExercicioID,Nome,Categoria,Descricao")] Exercicio exercicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exercicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exercicio);
        }

        // GET: Exercicio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercicio = await _context.Exercicios.FindAsync(id);
            if (exercicio == null)
            {
                return NotFound();
            }
            return View(exercicio);
        }

        // POST: Exercicio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExercicioID,Nome,Categoria,Descricao")] Exercicio exercicio)
        {
            if (id != exercicio.ExercicioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExercicioExists(exercicio.ExercicioID))
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
            return View(exercicio);
        }

        // GET: Exercicio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercicio = await _context.Exercicios
                .FirstOrDefaultAsync(m => m.ExercicioID == id);
            if (exercicio == null)
            {
                return NotFound();
            }

            return View(exercicio);
        }

        // POST: Exercicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercicio = await _context.Exercicios.FindAsync(id);
            _context.Exercicios.Remove(exercicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExercicioExists(int id)
        {
            return _context.Exercicios.Any(e => e.ExercicioID == id);
        }
    }
}
