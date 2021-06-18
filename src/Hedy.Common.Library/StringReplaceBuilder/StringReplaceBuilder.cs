namespace Hedy.Core.Infrastructure.String
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class StringReplaceBuilder
    {
        private string tagFirst = "#{";
        private string tagLast = "}#";
        private Dictionary<string, object> tagValues = new Dictionary<string, object>();
        private string template = string.Empty;
        private CultureInfo culture = CultureInfo.GetCultureInfo("pt-BR");

        public StringReplaceBuilder()
        {
        }

        public StringReplaceBuilder(string template)
        {
            this.template = template;
        }

        public StringReplaceBuilder AddHashTag(string tag, object value)
        {
            this.tagValues.Add(tag, value);

            return this;
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

        public StringReplaceBuilder AddTemplate(string template)
        {
            this.template = template;

            return this;
        }

        public StringReplaceBuilder AddCulture(string culture = "pt-BR")
        {
            this.culture = new CultureInfo(culture);

            return this;
        }

        public StringReplaceBuilder ClearHashTags()
        {
            this.tagValues = new Dictionary<string, object>();

            return this;
        }

        public string GetReplace()
        {
            if (string.IsNullOrWhiteSpace(template))
                throw new ArgumentException("Parameter cannot be null or empty", template);

            string tmp = template;

            foreach (KeyValuePair<string, object> entry in tagValues)
            {
                var tag = string.Concat(tagFirst, entry.Key.Trim(), tagLast);

                tmp = tmp.Replace(tag, GetFormatValue(entry.Value));
            }

            return tmp;
        }

        private string GetFormatValue(object value)
        {
            string valueFomrat = string.Empty;

            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.Decimal:
                    valueFomrat = string.Format(culture, "{0:C2}", value);
                    break;

                case TypeCode.Int32:
                    valueFomrat = value.ToString();
                    break;

                case TypeCode.String:
                    valueFomrat = value.ToString().Trim();
                    break;

                case TypeCode.DateTime:
                    valueFomrat = string.Format(culture, "{0:d}", value);
                    break;

                default:
                    valueFomrat = value.ToString().Trim();
                    break;
            }

            return valueFomrat;
        }
    }
}