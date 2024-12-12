namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;

    public static class ListHelper
    {
        public static bool AreEqual<T>(IList<T> list1, IList<T> list2);
        public static T Find<T>(IList<T> list, Predicate<T> match);
    }
}

