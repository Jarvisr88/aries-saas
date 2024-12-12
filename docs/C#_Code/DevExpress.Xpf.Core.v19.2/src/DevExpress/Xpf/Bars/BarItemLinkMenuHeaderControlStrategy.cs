namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class BarItemLinkMenuHeaderControlStrategy : BarItemLinkControlStrategy
    {
        public BarItemLinkMenuHeaderControlStrategy(IBarItemLinkControl instance);
        private void CheckMenuHeaders();
        private void ClearMenuHeader();
        public override INavigationOwner GetBoundOwner();
        protected override DataTemplate GetContentTemplate();
        public override void OnLinkInfoChanged(BarItemLinkInfo oldValue, BarItemLinkInfo newValue);
        protected override void SelectRoot();
        protected override bool ShouldHighlightItem();
        protected override void UpdateActualPropertiesOverride();

        protected BarItemLinkMenuHeaderControl MenuHeaderInstance { get; }

        protected bool IsItemFake { get; private set; }

        public BarItemLinkMenuHeader MenuHeaderLink { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemLinkMenuHeaderControlStrategy.<>c <>9;
            public static Action<BarItemLinkBase> <>9__13_0;

            static <>c();
            internal void <ClearMenuHeader>b__13_0(BarItemLinkBase link);
        }
    }
}

