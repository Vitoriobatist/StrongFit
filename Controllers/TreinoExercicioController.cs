using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StrongFit.Models;

namespace StrongFit.Controllers
{
    [Authorize(Roles = "Personal")] // Restringe o acesso para Personal
    public class TreinoExercicioController : Controller
    {
        private readonly Context _context;

        public TreinoExercicioController(Context context)
        {
            _context = context;
        }

        // GET: TreinoExercicio/Create
        public IActionResult Create(int treinoId)
        {
            ViewBag.Exercicios = new SelectList(_context.Exercicios, "ExercicioID", "Nome");
            ViewBag.TreinoID = treinoId; // Passa o treinoId para associar ao exercício
            return View();
        }

        // POST: TreinoExercicio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TreinoID,ExercicioID")] TreinoExercicio treinoExercicio)
        {
            if (ModelState.IsValid)
            {
                // Verifica se o treino já tem 4 exercícios, se sim, não permite adicionar mais
                var treino = await _context.Treinos.Include(t => t.TreinoExercicios).FirstOrDefaultAsync(t => t.TreinoID == treinoExercicio.TreinoID);
                if (treino.TreinoExercicios.Count >= 4)
                {
                    ModelState.AddModelError("", "Não é possível adicionar mais de 4 exercícios a este treino.");
                    ViewBag.Exercicios = new SelectList(_context.Exercicios, "ExercicioID", "Nome");
                    ViewBag.TreinoID = treinoExercicio.TreinoID;
                    return View(treinoExercicio);
                }

                _context.Add(treinoExercicio);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Treino", new { id = treinoExercicio.TreinoID });
            }
            ViewBag.Exercicios = new SelectList(_context.Exercicios, "ExercicioID", "Nome");
            return View(treinoExercicio);
        }

        // GET: TreinoExercicio/Delete/5
        public async Task<IActionResult> Delete(int? treinoId, int? exercicioId)
        {
            if (treinoId == null || exercicioId == null)
            {
                return NotFound();
            }

            var treinoExercicio = await _context.TreinoExercicios
                .Include(te => te.Exercicio)
                .Include(te => te.Treino)
                .FirstOrDefaultAsync(te => te.TreinoID == treinoId && te.ExercicioID == exercicioId);
            if (treinoExercicio == null)
            {
                return NotFound();
            }

            return View(treinoExercicio);
        }

        // POST: TreinoExercicio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int treinoId, int exercicioId)
        {
            var treinoExercicio = await _context.TreinoExercicios
                .FirstOrDefaultAsync(te => te.TreinoID == treinoId && te.ExercicioID == exercicioId);
            _context.TreinoExercicios.Remove(treinoExercicio);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Treino", new { id = treinoId });
        }
    }
}
