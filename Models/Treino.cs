namespace StrongFit.Models
{
    public class Treino
    {
        public int TreinoID { get; set; }
        public int PersonalID { get; set; }
        public int AlunoID { get; set; }
        public DateTime Data { get; set; }
        public DateTime Hora { get; set; }

        public Personal Personal { get; set; }
        public Aluno Aluno { get; set; }

        public ICollection<TreinoExercicio> TreinoExercicios { get; set; }
    }
}
