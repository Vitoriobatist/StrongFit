namespace StrongFit.Models
{
    public class TreinoExercicio
    {
        public int TreinoID { get; set; }
        public Treino Treino { get; set; }

        public int ExercicioID { get; set; }
        public Exercicio Exercicio { get; set; }
    }
}
