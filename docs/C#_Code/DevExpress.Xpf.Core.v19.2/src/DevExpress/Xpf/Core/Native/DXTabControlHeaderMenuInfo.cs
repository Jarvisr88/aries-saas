namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class DXTabControlHeaderMenuInfo : BindableBase
    {
        private bool isMenuOpen;
        private ObservableCollection<DependencyObject> items;
        private bool hasItems;
        protected readonly DXTabControl Owner;

        public DXTabControlHeaderMenuInfo(DXTabControl owner);
        private void AddCustomItems();
        private void Clear();
        protected virtual void GenerateItems();
        protected virtual IEnumerable<DependencyObject> GetItems();
        private IEnumerable<DXTabItem> GetTabItems();
        protected void OnIsMenuOpenChanged();
        public void SelectTab(DXTabItem tabItem);
        public void UpdateHasItems();

        public ICommand SelectTabCommand { get; private set; }

        public virtual bool IsMenuOpen { get; set; }

        public virtual ObservableCollection<DependencyObject> Items { get; protected set; }

        public virtual bool HasItems { get; protected set; }

        protected bool ShowHeaderMenu { get; }

        protected bool ShowVisibleTabItemsInHeaderMenu { get; }

        protected bool ShowHiddenItemsInHeaderMenu { get; }

        protected bool ShowDisabledTabItemsInHeaderMenu { get; }

        protected TabControlViewBase View { get; }

        protected IEnumerable<DXTabItem> TabItems { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXTabControlHeaderMenuInfo.<>c <>9;
            public static Func<DXTabItem, bool> <>9__33_1;
            public static Func<FrameworkContentElement, bool> <>9__36_0;
            public static Func<DXTabItem, bool> <>9__37_1;

            static <>c();
            internal bool <Clear>b__36_0(FrameworkContentElement x);
            internal bool <GetItems>b__33_1(DXTabItem tabItem);
            internal bool <GetTabItems>b__37_1(DXTabItem item);
        }
    }
}

