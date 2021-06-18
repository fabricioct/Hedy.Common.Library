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
            var template = "Ol� senhor #{nome}#, na data de #{data}# o senhor tem o valor de #{valor}# para restituir.";

            var builder = new StringReplaceBuilder(template);

            var resultado = builder.AddHashTag("nome", "Fabr�cio C. Taglialegna")
                .AddHashTag("data", DateTime.Now)
                .AddHashTag("valor", 10523.25m)
                .GetReplace();
        }
    }
}