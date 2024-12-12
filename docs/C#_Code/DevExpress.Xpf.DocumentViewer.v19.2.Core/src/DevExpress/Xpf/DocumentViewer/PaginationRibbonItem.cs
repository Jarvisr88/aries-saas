namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.DocumentViewer.Themes;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class PaginationRibbonItem : DocumentViewerBarButtonItem
    {
        static PaginationRibbonItem()
        {
            BarItemLinkCreator.Default.RegisterObject(typeof(PaginationRibbonItem), typeof(PaginationRibbonItemLink), (CreateObjectMethod<BarItemLink>) (o => new PaginationRibbonItemLink()));
            BarItemLinkControlCreator.Default.RegisterObject(typeof(PaginationRibbonItemLink), typeof(PaginationRibbonItemLinkControl), (CreateObjectMethod<BarItemLinkControlBase>) (o => new PaginationRibbonItemLinkControl()));
            BarItemLinkControlStrategyCreator.Default.RegisterObject<PaginationRibbonItemLink, PaginationRibbonItemLinkControl, PaginationRibbonItemLinkControlStrategy>(x => new PaginationRibbonItemLinkControlStrategy(x));
        }

        protected override void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
        {
            base.OnCommandChanged(oldCommand, newCommand);
            CommandBase base2 = newCommand as CommandBase;
            if (base2 == null)
            {
                base.ClearValue(FrameworkContentElement.StyleProperty);
            }
            else
            {
                base.DataContext = base2;
                DocumentViewerThemeKeyExtension resourceKey = new DocumentViewerThemeKeyExtension();
                resourceKey.ResourceKey = DocumentViewerThemeKeys.RibbonPaginationItemStyle;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(this);
                base.Style = (Style) base.FindResource(resourceKey);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PaginationRibbonItem.<>c <>9 = new PaginationRibbonItem.<>c();

            internal BarItemLink <.cctor>b__0_0(object o) => 
                new PaginationRibbonItemLink();

            internal BarItemLinkControlBase <.cctor>b__0_1(object o) => 
                new PaginationRibbonItemLinkControl();

            internal PaginationRibbonItemLinkControlStrategy <.cctor>b__0_2(IBarItemLinkControl x) => 
                new PaginationRibbonItemLinkControlStrategy(x);
        }
    }
}

