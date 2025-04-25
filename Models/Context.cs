using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StrongFit.Models;

public class Context : IdentityDbContext
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Exercicio> Exercicios { get; set; }
    public DbSet<Personal> Personais { get; set; }
    public DbSet<Treino> Treinos { get; set; }
    public DbSet<TreinoExercicio> TreinoExercicios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da chave composta da tabela de junção
        modelBuilder.Entity<TreinoExercicio>()
            .HasKey(te => new { te.TreinoID, te.ExercicioID });

        // Evita múltiplos caminhos de deleção em cascata
        modelBuilder.Entity<Treino>()
            .HasOne(t => t.Aluno)
            .WithMany(a => a.Treinos)
            .HasForeignKey(t => t.AlunoID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Treino>()
            .HasOne(t => t.Personal)
            .WithMany(p => p.Treinos)
            .HasForeignKey(t => t.PersonalID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
