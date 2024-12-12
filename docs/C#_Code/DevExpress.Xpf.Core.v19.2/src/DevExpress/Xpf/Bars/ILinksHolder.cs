namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public interface ILinksHolder : IMultipleElementRegistratorSupport, IBarNameScopeSupport, IInputElement, ILogicalChildrenContainer
    {
        event ValueChangedEventHandler<BarItemLinkCollection> ActualLinksChanged;

        GlyphSize GetDefaultItemsGlyphSize(LinkContainerType linkContainerType);
        IEnumerator GetLogicalChildrenEnumerator();
        void Merge(ILinksHolder holder);
        void OnLinkAdded(BarItemLinkBase link);
        void OnLinkRemoved(BarItemLinkBase link);
        void UnMerge();
        void UnMerge(ILinksHolder holder);

        CustomizedBarItemLinkCollection CustomizedItems { get; }

        BarItemLinkCollection Links { get; }

        CommonBarItemCollection Items { get; }

        IEnumerable ItemsSource { get; }

        BarItemLinkCollection ActualLinks { get; }

        ILinksHolder MergedParent { get; set; }

        GlyphSize ItemsGlyphSize { get; }

        Size CustomItemsGlyphSize { get; }

        bool ShowDescription { get; }

        LinksHolderType HolderType { get; }

        DevExpress.Xpf.Editors.ImmediateActionsManager ImmediateActionsManager { get; }

        MergedLinksHolderCollection MergedChildren { get; }
    }
}

