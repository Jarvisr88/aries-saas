namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.DocumentViewer.Themes;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class PaginationBarItem : BarStaticItem
    {
        static PaginationBarItem()
        {
            BarStaticItem.ShowBorderProperty.OverrideMetadata(typeof(PaginationBarItem), new FrameworkPropertyMetadata(false));
            BarItemLinkCreator.Default.RegisterObject(typeof(PaginationBarItem), typeof(PaginationBarItemLink), (CreateObjectMethod<BarItemLink>) (o => new PaginationBarItemLink()));
            BarItemLinkControlCreator.Default.RegisterObject(typeof(PaginationBarItemLink), typeof(PaginationBarItemLinkControl), (CreateObjectMethod<BarItemLinkControlBase>) (o => new PaginationBarItemLinkControl()));
            BarItemLinkControlStrategyCreator.Default.RegisterObject<PaginationBarItemLink, PaginationBarItemLinkControl, PaginationBarItemLinkControlStrategy>(x => new PaginationBarItemLinkControlStrategy(x));
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
                resourceKey.ResourceKey = DocumentViewerThemeKeys.BarPaginationItemStyle;
                resourceKey.ThemeName = ThemeHelper.GetEditorThemeName(this);
                base.Style = (Style) base.FindResource(resourceKey);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PaginationBarItem.<>c <>9 = new PaginationBarItem.<>c();

            internal BarItemLink <.cctor>b__0_0(object o) => 
                new PaginationBarItemLink();

            internal BarItemLinkControlBase <.cctor>b__0_1(object o) => 
                new PaginationBarItemLinkControl();

            internal PaginationBarItemLinkControlStrategy <.cctor>b__0_2(IBarItemLinkControl x) => 
                new PaginationBarItemLinkControlStrategy(x);
        }
    }
}

