namespace StrongFit.Models
{
    public class Aluno
    {
        public int AlunoID { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string E_Mail { get; set; }
        public string Instagram { get; set; }
        public string Telefone { get; set; }
        public string Observacoes { get; set; }

        public int PersonalID { get; set; }
        public Personal Personal { get; set; }

        public ICollection<Treino> Treinos { get; set; }
    }
}
