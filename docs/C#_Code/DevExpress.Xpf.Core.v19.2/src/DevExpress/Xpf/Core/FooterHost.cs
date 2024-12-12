namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq;

    public class FooterHost : ContentHostBase
    {
        internal const string FooterHostName = "FooterHost";

        protected override ContentHostPresenterBase FindContentHostPresenter() => 
            LayoutTreeHelper.GetVisualParents(this, null).OfType<ContentAndFooterHostPresenter>().FirstOrDefault<ContentAndFooterHostPresenter>();

        protected override string GetHostName() => 
            "FooterHost";
    }
}

