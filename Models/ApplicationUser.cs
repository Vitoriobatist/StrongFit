using Microsoft.AspNetCore.Identity;

namespace Exemplo_Identity_Autorizacao.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }

        // "Aluno" ou "Personal"
        public string TipoUsuario { get; set; }
    }
}
