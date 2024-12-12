namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal static class Extenstions
    {
        internal static Queue<T> AsQueue<T>(this IEnumerable<T> collecetion) => 
            new Queue<T>(collecetion);
    }
}

