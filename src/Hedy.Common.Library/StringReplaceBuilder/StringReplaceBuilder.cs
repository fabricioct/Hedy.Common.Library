using System;
using System.Net.Http;

namespace Hedy.Core.Infrastructure.Rest
{
    public class StringReplaceBuilder
    {
        private string tagFirst = "#{";
        private string tagLast = "}#";
        private string template = string.Empty;


        public StringReplaceBuilder()
        {
        }

        public StringReplaceBuilder AddTagFirst(string tagFirst)
        {
            this.tagFirst = tagFirst;
            return this;
        }

        public StringReplaceBuilder AddTagLast(string tagLast)
        {
            this.tagLast = tagLast;
            return this;
        }

        public StringReplaceBuilder AddTamplate(string template)
        {
            this.template = template;
            return this;
        }

    }
}