using System;

namespace EdTech.Core.Entities
{
    public class Cpf : ValueObject
    {
        public string Codigo { get; set; }

        public Cpf()
        {
        }

        public Cpf(string codigo)
        {
            Codigo = codigo;
        }

        public bool IsValid()
        {
            var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var cpfNormalized = Codigo.Trim().Replace(".", "").Replace("-", "");
            if (cpfNormalized.Length != 11) return false;

            for (var j = 0; j < 10; j++)
            {
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpfNormalized)
                {
                    return false;
                }
            }

            var tempCpf = cpfNormalized.Substring(0, 9);
            var soma = 0;

            for (var i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            var resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            var digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito += resto;

            return cpfNormalized.EndsWith(digito);
        }

        private static Cpf Parse(string codigo)
        {
            if (TryParse(codigo, out var result))
            {
                return result;
            }

            throw new ArgumentException("CPF Inválido");
        }

        private static bool TryParse(string codigo, out Cpf cpf)
        {
            var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = new Cpf(codigo);

            var codigoNormalized = codigo.Trim().Replace(".", "").Replace("-", "");
            if (codigoNormalized.Length != 11) return false;

            for (var j = 0; j < 10; j++)
            {
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == codigoNormalized)
                {
                    return false;
                }
            }

            var tempCpf = codigoNormalized.Substring(0, 9);
            var soma = 0;

            for (var i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            var resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            var digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }

            resto = soma % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito += resto;

            if (codigoNormalized.EndsWith(digito))
            {
                cpf = new Cpf(codigo);
                return true;
            }

            cpf = new Cpf(codigo);
            return false;
        }

        public void Normalize()
        {
            Codigo = Codigo.Trim().Replace(".", string.Empty).Replace("-", string.Empty);
        }

        public override string ToString()
        {
            return Codigo;
        }

        public static implicit operator Cpf(string codigo)
        {
            return Parse(codigo);
        }
    }
}
