namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq;

    public class ContentHost : ContentHostBase
    {
        internal const string ContentHostName = "ContentHost";

        protected override ContentHostPresenterBase FindContentHostPresenter() => 
            LayoutTreeHelper.GetVisualParents(this, null).OfType<ContentHostPresenter>().FirstOrDefault<ContentHostPresenter>();

        protected override string GetHostName() => 
            "ContentHost";
    }
}

