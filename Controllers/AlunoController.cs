using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StrongFit.Models;

namespace StrongFit.Controllers
{
    [Authorize(Roles = "Aluno")] // Restringe o acesso a Alunos
    public class AlunoController : Controller
    {
        private readonly Context _context;

        public AlunoController(Context context)
        {
            _context = context;
        }

        // Action para visualizar os treinos do aluno
        public async Task<IActionResult> MeusTreinos()
        {
            var alunoId = User.Identity.Name; // Identifica o aluno logado
            var aluno = await _context.Alunos
                .Include(a => a.Treinos)
                .ThenInclude(t => t.TreinoExercicios)
                .ThenInclude(te => te.Exercicio)
                .FirstOrDefaultAsync(a => a.AlunoID == int.Parse(alunoId));

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno.Treinos);
        }
    }
}
