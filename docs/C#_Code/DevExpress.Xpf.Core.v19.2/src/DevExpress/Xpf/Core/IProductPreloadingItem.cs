namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;

    public interface IProductPreloadingItem
    {
        string AssemblyFullName { get; }

        IEnumerable<FrameworkElement> Controls { get; }
    }
}

