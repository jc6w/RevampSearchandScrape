using System;
using System.Collections.Generic;

namespace RevampSearchandScrape
{
    public static class ErrrorExtensions
    {
        public static IEnumerable<Exception> FlattenHierarchy(this Exception ex)
        {
            if (ex == null) { yield break; }
            else
            {
                var innerException = ex;
                do
                {
                    yield return innerException;
                    innerException = innerException.InnerException;
                }
                while (innerException != null);
            }

        }
    }
}
