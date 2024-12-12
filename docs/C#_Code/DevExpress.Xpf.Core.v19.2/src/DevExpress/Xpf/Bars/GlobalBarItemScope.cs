namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.Collections.Generic;

    public class GlobalBarItemScope
    {
        private static GlobalBarItemScope instanceCore;
        protected Dictionary<GlobalBarItemScopeKey, WeakReference> itemScope;
        protected WeakList<BarItemLinkBase> pendingItemLinks;

        public GlobalBarItemScope();
        public static void AddBarItem(GlobalBarItemScopeKey key, BarItem item);
        public static void LinkItem(GlobalBarItemScopeKey key, BarItem item);

        protected static GlobalBarItemScope Instance { get; }
    }
}

