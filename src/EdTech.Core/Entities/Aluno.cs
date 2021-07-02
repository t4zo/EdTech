using System.ComponentModel.DataAnnotations;

namespace EdTech.Core.Entities
{
    public class Aluno
    {
        public string RA { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public Cpf Cpf { get; set; }

        public void UpdateNome(string newNome)
        {
            Nome = newNome;
        }

        public bool UpdateEmail(string newEmail)
        {
            var isValid = new EmailAddressAttribute().IsValid(newEmail);
            if (isValid)
            {
                Email = newEmail;
            };

            return isValid;
        }
    }
}