using System;
using Hedy.Core.Infrastructure.String;
using Xunit;

namespace Hedy.Common.Library.Test
{
    public class StringReplaceBuilderTest
    {
        [Fact]
        public void Test1()
        {
            var template = "Olá senhor #{nome}#, na data de #{data}# o senhor tem o valor de #{Valor}# para restituir. Data test #{teste}# CPF: #{cpf}#  ";

            var builder = new ReplaceBuilder();

            var resultado = builder.AddHashTag("nome", "Fabrício C. Taglialegna")
                .AddHashTag("data", DateTime.Now)
                .AddHashTag("valor", 10523.25m)
                .AddHashTag("teste", DateTime.Now,"yyyy-MM-dd")
                .AddHashTag("cpf", "03619652651", "CPF").AddTemplate(template);

            var html = resultado.RawText;
        }
    }
}