namespace DevExpress.Xpf.Core.FilteringUI
{
    using System.Windows;

    public sealed class PredefinedFilterCollection : FreezableCollection<PredefinedFilter>
    {
        protected override Freezable CreateInstanceCore() => 
            new PredefinedFilterCollection();
    }
}

