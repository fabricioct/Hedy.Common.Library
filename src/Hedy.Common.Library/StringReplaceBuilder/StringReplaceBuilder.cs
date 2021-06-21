namespace Hedy.Core.Infrastructure.String
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class ReplaceBuilder
    {
        private string preFix = "#{";
        private string postFix = "}#";
        private Dictionary<string, HasTag> tagValues = new Dictionary<string, HasTag>();
        private CultureInfo culture = CultureInfo.GetCultureInfo("pt-BR");

        private class HasTag
        {
            public string Tag { get; set; }

            public object Parameters { get; set; }

            public string Format { get; set; }
        }

        public ReplaceBuilder AddHashTag(string tag, object value)
        {
            this.tagValues.Add(tag, new HasTag() { Tag = tag, Parameters = value });

            return this;
        }

        public ReplaceBuilder AddHashTag(string tag, object value, string format)
        {
            this.tagValues.Add(tag, new HasTag() { Tag = tag, Parameters = value, Format = format });

            return this;
        }

        public ReplaceBuilder AddTagFirst(string tagFirst)
        {
            this.preFix = tagFirst;

            return this;
        }

        public ReplaceBuilder AddTagLast(string tagLast)
        {
            this.postFix = tagLast;

            return this;
        }

        public ReplaceBuilder AddCulture(string culture = "pt-BR")
        {
            this.culture = new CultureInfo(culture);

            return this;
        }

        public ReplaceBuilder ClearHashTags()
        {
            this.tagValues = new  Dictionary<string, HasTag>();

            return this;
        }

        public class Template
        {
            private readonly ReplaceBuilder builder;
            private readonly string template;

            public string RawText
            {
                get { return ResolveHashTags(); }
            }

            public Template(ReplaceBuilder builder, string template)
            {
                this.builder = builder;
                this.template = template;
            }

            private string ResolveHashTags()
            {
                if (string.IsNullOrWhiteSpace(template))
                    throw new ArgumentException("Parameter cannot be null or empty", template);

                string tmp = template;

                foreach (KeyValuePair<string, HasTag> entry in builder.tagValues)
                {
                    var tag = string.Concat(builder.preFix, entry.Key.Trim(), builder.postFix);

                    tmp = tmp.Replace(tag, GetFormatValue(entry.Value));
                }

                return tmp;
            }

            private string GetFormatValue(HasTag value)
            {
                string valueFomrat;
                string stFormat = null;
                
                if(!string.IsNullOrWhiteSpace(value.Format))
                    stFormat = string.Concat("{0:", value.Format, "}"); 

                switch (Type.GetTypeCode(value.Parameters.GetType()))
                {
                    case TypeCode.Decimal:
                        valueFomrat = string.Format(builder.culture, string.IsNullOrWhiteSpace(stFormat) ? "{0:C2}": stFormat, value.Parameters);
                        break;

                    case TypeCode.Int32:
                        valueFomrat = value.Parameters.ToString();
                        break;

                    case TypeCode.String:
                        valueFomrat = value.Parameters.ToString().Trim();
                        break;

                    case TypeCode.DateTime:
                        valueFomrat = string.Format(builder.culture, string.IsNullOrWhiteSpace(stFormat) ? "{0:d}" : stFormat, value.Parameters);
                        break;

                    default:
                        valueFomrat = value.ToString().Trim();
                        break;
                }

                return valueFomrat;
            }
        }

        public Template AddTemplate(string template) => new Template(this, template);
    }
}