namespace DevExpress.Printing.ExportHelpers.Helpers
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class EnumerableExtensions
    {
        public static int Count(this IEnumerable source)
        {
            ICollection is2 = source as ICollection;
            return ((is2 == null) ? source.Cast<object>().Count() : is2.Count);
        }
    }
}

