namespace DevExpress.Xpf.Bars
{
    using System.Windows;

    public class SubMenuClientPanel : SubMenuClientPanelBase
    {
        protected override Size MeasureOverride(Size constraint);

        public LinksControl Owner { get; }
    }
}

