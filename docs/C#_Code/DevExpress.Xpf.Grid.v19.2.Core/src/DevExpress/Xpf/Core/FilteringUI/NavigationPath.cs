namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class NavigationPath
    {
        public NavigationPath() : this(new int[0])
        {
        }

        public NavigationPath(IReadOnlyCollection<int> value)
        {
            this.<Value>k__BackingField = value;
        }

        public IReadOnlyCollection<int> Value { get; }
    }
}

