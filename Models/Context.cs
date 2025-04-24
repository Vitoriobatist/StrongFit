using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StrongFit.Models;

public class Context : IdentityDbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    // DbSets para as entidades do seu projeto
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Exercicio> Exercicios { get; set; }
    public DbSet<Personal> Personais { get; set; }
    public DbSet<Treino> Treinos { get; set; }
    public DbSet<TreinoExercicio> TreinoExercicios { get; set; }
}
