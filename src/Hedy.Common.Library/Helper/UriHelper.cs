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

        public static UriBuilder GetBuilder(Uri uri, object parameters, params string[] resource)
        {
            var builder = new UriBuilder(uri)
            {
                Path = resource.Aggregate(string.Empty, (a, b) => $"{a}/{b.TrimStart('/', '\\').TrimEnd('/', '\\')}")
            };

            var query = HttpUtility.ParseQueryString(builder.Query);

            if (parameters == null)
                return builder;

            foreach (var propertyInfo in parameters.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                query.Add(propertyInfo.Name, propertyInfo.GetValue(parameters).ToString());

            builder.Query = query.ToString();

            return builder;
        }
    }
}