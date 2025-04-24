using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StrongFit.Models;

namespace StrongFit.Controllers
{
    [Authorize(Roles = "Personal")] // Restringe o acesso para Personal
    public class TreinoController : Controller
    {
        private readonly Context _context;

        public TreinoController(Context context)
        {
            _context = context;
        }

        // GET: Treino
        public async Task<IActionResult> Index()
        {
            var treinos = _context.Treinos.Include(t => t.Aluno).Include(t => t.Personal);
            return View(await treinos.ToListAsync());
        }

        // GET: Treino/Create
        public IActionResult Create()
        {
            ViewBag.Alunos = new SelectList(_context.Alunos, "AlunoID", "Nome");
            return View();
        }

        // POST: Treino/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TreinoID,AlunoID,PersonalID,Data,Hora")] Treino treino)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(treino);
        }

        // GET: Treino/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treino = await _context.Treinos
                .Include(t => t.Aluno)
                .Include(t => t.Personal)
                .FirstOrDefaultAsync(m => m.TreinoID == id);
            if (treino == null)
            {
                return NotFound();
            }

            return View(treino);
        }

        // GET: Treino/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treino = await _context.Treinos.FindAsync(id);
            if (treino == null)
            {
                return NotFound();
            }
            ViewBag.Alunos = new SelectList(_context.Alunos, "AlunoID", "Nome", treino.AlunoID);
            return View(treino);
        }

        // POST: Treino/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TreinoID,AlunoID,PersonalID,Data,Hora")] Treino treino)
        {
            if (id != treino.TreinoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreinoExists(treino.TreinoID))
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
            return View(treino);
        }

        // GET: Treino/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treino = await _context.Treinos
                .Include(t => t.Aluno)
                .Include(t => t.Personal)
                .FirstOrDefaultAsync(m => m.TreinoID == id);
            if (treino == null)
            {
                return NotFound();
            }

            return View(treino);
        }

        // POST: Treino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var treino = await _context.Treinos.FindAsync(id);
            _context.Treinos.Remove(treino);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreinoExists(int id)
        {
            return _context.Treinos.Any(e => e.TreinoID == id);
        }
    }
}
