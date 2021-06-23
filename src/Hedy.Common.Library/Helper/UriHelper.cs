using System;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Hedy.Common.Library.Helper
{
    public static class UriHelper
    {
        public static UriBuilder GetBuilder(Uri uri, params string[] resource)
        {
            return GetBuilder(uri, null, resource);
        }

        public static UriBuilder GetBuilder(Uri uri, object Parameter, params string[] resource)
        {
            var builder = new UriBuilder(uri)
            {
                Path = resource.Aggregate(string.Empty, (a, b) => $"{a}/{b.TrimStart('/', '\\').TrimEnd('/', '\\')}")
            };

            var query = HttpUtility.ParseQueryString(builder.Query);

            if (Parameter == null)
                return builder;

            foreach (var propertyInfo in Parameter.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                query.Add(propertyInfo.Name, propertyInfo.GetValue(Parameter).ToString());

            builder.Query = query.ToString();

            return builder;
        }
    }
}