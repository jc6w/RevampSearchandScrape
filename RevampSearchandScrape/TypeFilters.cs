using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RevampSearchandScrape
{
    public static class TypeFilters
    {
        public static IEnumerable<Type> WithMatchingInterface(this IEnumerable<Type> types)
        {
            return types.Where(type =>
                type.GetTypeInfo().GetInterface("I" + type.Name) != null);
        }
    }
}
