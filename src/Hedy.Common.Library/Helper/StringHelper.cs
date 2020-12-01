using System;
using System.Linq;
using System.Text.RegularExpressions;
using Hedy.Common.Library.Extension;

namespace Hedy.Common.Library.Helper
{
    public static class StringHelper
    {
        public static string MaskCpf(string cpf)
        {
            return Regex.Replace(cpf, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");
        }

        public static string MaskCnpj(string cnpj)
        {
            return Regex.Replace(cnpj, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", "$1.$2.$3/$4-$5");
        }

        public static bool ValidateCpfOrCnpj(string cpfCnpj)
        {
            return ValidateCnpj(cpfCnpj) || ValidateCpf(cpfCnpj);
        }

        public static bool ValidateCpf(string cpf)
        {
            cpf = cpf.ExtractNumber();

            if (cpf.IsNullOrWhiteSpace() || cpf.Length != 11)
                return false;

            var cpfInvalido = new string[10] { "00000000000", "11111111111", "22222222222", "33333333333", "44444444444",
                                               "55555555555", "66666666666", "77777777777", "88888888888", "99999999999" };

            if (cpfInvalido.Contains(cpf))
                return false;

            int soma = 0;
            int resto;
            int[] mt1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mt2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * mt1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * mt2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto;

            return cpf.EndsWith(digito.ToString());
        }

        public static bool ValidateCnpj(string cnpj)
        {
            cnpj = cnpj.ExtractNumber();

            if (cnpj.IsNullOrWhiteSpace() || cnpj.Length != 14)
                return false;

            var cnpjInvalido = new string[10] { "00000000000000", "11111111111111", "22222222222222", "33333333333333", "44444444444444",
                                               "55555555555555", "66666666666666", "77777777777777", "88888888888888", "99999999999999" };

            if (cnpjInvalido.Contains(cnpj))
                return false;

            int soma = 0;
            int resto;
            int[] mt1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mt2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            for (int i = 0; i < 12; i++)
                soma += int.Parse(cnpj[i].ToString()) * mt1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(cnpj[i].ToString()) * mt2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto;

            return cnpj.EndsWith(digito);
        }

        private static readonly Random random = new Random();

        public static string RandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}