namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class NavigationPager<TNode> where TNode: class
    {
        public NavigationPager(Func<TNode> up, Func<TNode> down)
        {
            Guard.ArgumentNotNull(up, "up");
            Guard.ArgumentNotNull(down, "down");
            this.Up = up;
            this.Down = down;
        }

        public Func<TNode> Up { get; set; }

        public Func<TNode> Down { get; set; }
    }
}

