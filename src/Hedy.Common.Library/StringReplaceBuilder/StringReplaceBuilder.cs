namespace Hedy.Core.Infrastructure.String
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class ReplaceBuilder
    {
        private string preFix = "#{";
        private string postFix = "}#";
        private readonly Dictionary<string, HasTag> tagValues = new Dictionary<string, HasTag>();
        private CultureInfo culture = CultureInfo.GetCultureInfo("pt-BR");

        private class HasTag
        {
            public string Tag { get; set; }

            public object Parameters { get; set; }

            public string Format { get; set; }
        }

        public ReplaceBuilder AddHashTag(string hashTag, object value)
        {
            this.tagValues.Add(hashTag, new HasTag() { Tag = hashTag, Parameters = value });

            return this;
        }

        public ReplaceBuilder AddHashTag(string hashTag, object value, string format)
        {
            this.tagValues.Add(hashTag, new HasTag() { Tag = hashTag, Parameters = value, Format = format });

            return this;
        }

        public ReplaceBuilder AddPrefFix(string tagFirst)
        {
            this.preFix = tagFirst;

            return this;
        }

        public ReplaceBuilder AddPostFix(string tagLast)
        {
            this.postFix = tagLast;

            return this;
        }

        public ReplaceBuilder AddCulture(string culture = "en-US")
        {
            this.culture = new CultureInfo(culture);

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

                    tmp = Regex.Replace(tmp, tag, GetFormatValue(entry.Value), RegexOptions.IgnoreCase);
                }

                return tmp;
            }

            private string GetFormatValue(HasTag value)
            {
                string valueFomrat;
                string stFormat = null;
                string[] customMask = { "CNPJ", "CPF", "CEP" };

                if (customMask.Any(x => (value.Format != null) && x.Contains(value.Format)))
                    return CustomMask(value.Format, value.Parameters);

                if (!string.IsNullOrWhiteSpace(value.Format))
                    stFormat = string.Concat("{0:", value.Format.Trim(), "}");

                switch (Type.GetTypeCode(value.Parameters.GetType()))
                {
                    case TypeCode.Decimal:
                        valueFomrat = string.Format(builder.culture, string.IsNullOrWhiteSpace(stFormat) ? "{0:C2}" : stFormat, value.Parameters);
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

            public static string CustomMask(string mask, object value)
            {
                mask = mask.Trim().ToUpper();
                var valueRetorno = Regex.Replace(value.ToString(), @"[^\d]", string.Empty);

                switch (mask)
                {
                    case "CPF":
                        valueRetorno = Regex.Replace(valueRetorno, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");
                        break;

                    case "CNPJ":
                        valueRetorno = Regex.Replace(valueRetorno, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", "$1.$2.$3/$4-$5");
                        break;

                    case "CEP":
                        valueRetorno = Regex.Replace(valueRetorno, @"(\d{2})(\d{3})(\d{3})", "$1.$2-$3");
                        break;

                    default:
                        break;
                }

                return valueRetorno;
            }
        }

        public Template AddTemplate(string template) => new Template(this, template);
    }
}