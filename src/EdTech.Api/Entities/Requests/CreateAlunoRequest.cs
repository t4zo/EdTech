namespace EdTech.Api.Entities.Requests
{
    public class CreateAlunoRequest
    {
        public string RA { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }

        public void NormalizeCpf()
        {
            Cpf = Cpf.Trim().Replace(".", string.Empty).Replace("-", string.Empty);
        }
    }
}
