using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StrongFit.Models;

namespace StrongFit.Controllers
{
    [Authorize(Roles = "Personal")] // Restringe o acesso a Personal
    public class PersonalController : Controller
    {
        private readonly Context _context;

        public PersonalController(Context context)
        {
            _context = context;
        }

        // Action para ver a lista de alunos do personal
        public IActionResult MeusAlunos()
        {
            var personalId = User.Identity.Name; // Identifica o personal logado
            var alunos = _context.Alunos
                .Where(a => a.PersonalID == int.Parse(personalId))
                .ToList();

            return View(alunos);
        }

        // Action para criar treino para um aluno
        public IActionResult CriarTreino(int alunoId)
        {
            ViewBag.Exercicios = new SelectList(_context.Exercicios, "ExercicioID", "Nome");
            ViewBag.AlunoID = alunoId;
            return View();
        }

        // POST: Criar treino para o aluno
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CriarTreino(Treino treino)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treino);
                await _context.SaveChangesAsync();
                return RedirectToAction("MeusAlunos");
            }
            return View(treino);
        }
    }
}
