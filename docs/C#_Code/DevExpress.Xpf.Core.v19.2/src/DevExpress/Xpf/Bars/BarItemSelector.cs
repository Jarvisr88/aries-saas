namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BarItemSelector : BarLinkContainerItem
    {
        public static readonly DependencyProperty SelectedItemProperty;
        public static readonly DependencyProperty AllowEmptySelectionProperty;
        public static readonly DependencyProperty ClearSelectionWhenItemsChangedProperty;
        private readonly int groupIndex;
        private readonly Locker initializationLocker;
        private readonly Locker selectionLocker;

        static BarItemSelector();
        public BarItemSelector();
        public override void BeginInit();
        protected bool CanSelectItem(BarItem item);
        protected virtual void CheckEmptySelection();
        private void ClearItem(BarItem item);
        protected override CommonBarItemCollection CreateCommonBarItemCollection();
        public override void EndInit();
        protected virtual BarItem GetBarItem(IBarItem source);
        protected virtual bool? GetIsSelected(BarItem item);
        protected virtual BarItem GetItemBySource(object source);
        protected virtual object GetSourceByItem(IBarItem element);
        protected virtual void OnAllowEmptySelectionChanged(bool oldValue);
        protected virtual void OnInitializationLockerUnlocked(object sender, EventArgs e);
        protected virtual void OnItemCheckedChanged(object sender, ItemClickEventArgs e);
        protected internal override void OnItemLinksSourceChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected internal override void OnItemTemplateChanged(DependencyPropertyChangedEventArgs e);
        protected internal override void OnItemTemplateSelectorChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnSelectedItemChanged(object oldValue);
        protected virtual void SetIsSelected(BarItem item, bool value);
        private void UpdateItem(BarItem item);

        public object SelectedItem { get; set; }

        public bool AllowEmptySelection { get; set; }

        public bool ClearSelectionWhenItemsChanged { get; set; }

        protected int GroupIndex { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemSelector.<>c <>9;
            public static Func<BarItem, object> <>9__19_0;
            public static Func<bool?, bool> <>9__19_1;
            public static Func<bool> <>9__19_2;
            public static Func<bool?, bool> <>9__28_0;
            public static Func<bool> <>9__28_1;
            public static Func<bool?, bool> <>9__28_2;
            public static Func<bool> <>9__28_3;
            public static Func<BarItemLink, BarItem> <>9__34_0;

            static <>c();
            internal void <.cctor>b__5_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__5_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal BarItem <GetBarItem>b__34_0(BarItemLink x);
            internal bool <OnItemCheckedChanged>b__28_0(bool? x);
            internal bool <OnItemCheckedChanged>b__28_1();
            internal bool <OnItemCheckedChanged>b__28_2(bool? x);
            internal bool <OnItemCheckedChanged>b__28_3();
            internal object <OnItemsCollectionChanged>b__19_0(BarItem x);
            internal bool <OnItemsCollectionChanged>b__19_1(bool? x);
            internal bool <OnItemsCollectionChanged>b__19_2();
        }
    }
}

