namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    [ContentProperty("Items")]
    public class BarItemMenuHeader : BarItem, ILinksHolder, IMultipleElementRegistratorSupport, IBarNameScopeSupport, IInputElement, ILogicalChildrenContainer
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty ItemsAttachedBehaviorProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        public static readonly DependencyProperty ItemStyleProperty;
        public static readonly DependencyProperty ItemStyleSelectorProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty MinColCountProperty;
        public static readonly DependencyProperty ItemsOrientationProperty;
        internal bool fake;
        private BarItemLinkCollection itemLinks;
        private CommonBarItemCollection itemsCore;
        private readonly ImmediateActionsManager immediateActionsManager;
        private MergedLinksHolderCollection mergedChildren;
        private CustomizedBarItemLinkCollection customizedItems;
        private readonly List<object> logicalChildrenContainerItems;

        event ValueChangedEventHandler<BarItemLinkCollection> ILinksHolder.ActualLinksChanged;

        static BarItemMenuHeader();
        public BarItemMenuHeader();
        protected virtual BarItemLinkCollection CreateItemLinksCollection();
        GlyphSize ILinksHolder.GetDefaultItemsGlyphSize(LinkContainerType linkContainerType);
        IEnumerator ILinksHolder.GetLogicalChildrenEnumerator();
        void ILinksHolder.Merge(ILinksHolder holder);
        void ILinksHolder.OnLinkAdded(BarItemLinkBase link);
        void ILinksHolder.OnLinkRemoved(BarItemLinkBase link);
        void ILinksHolder.UnMerge();
        void ILinksHolder.UnMerge(ILinksHolder holder);
        void ILogicalChildrenContainer.AddLogicalChild(object child);
        void ILogicalChildrenContainer.RemoveLogicalChild(object child);
        protected override IEnumerable<object> GetRegistratorKeys();
        protected override object GetRegistratorName(object registratorKey);
        protected static void OnItemsOrientationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnItemsSourceChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected static void OnItemStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected static void OnItemStyleSelectorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected static void OnItemTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected static void OnItemTemplateSelectorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private void OnLayoutUpdated(object sender, EventArgs e);
        protected static void OnMinColCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void UpdateItemsAttachedBehavior(DependencyPropertyChangedEventArgs e);
        protected internal virtual void UpdateLinkControlsIsEmpty(bool isAddOperation);

        [Description("Provides access to a collection of BarItemLinks, hosted in this BarItemMenuHeader. This is a dependency property.")]
        public BarItemLinkCollection ItemLinks { get; }

        public HeaderOrientation ItemsOrientation { get; set; }

        public int MinColCount { get; set; }

        protected override IEnumerator LogicalChildren { get; }

        [Description("Gets or sets the template applied to all BarItems hosted in this BarItemMenuHeader.")]
        public DataTemplate ItemTemplate { get; set; }

        [Description("Gets or sets the style applied to all BarItems hosted within this BarItemMenuHeader.")]
        public Style ItemStyle { get; set; }

        [Description("Gets or sets an object that chooses a style applied to items in this BarItemMenuHeader.This is a dependency property.")]
        public StyleSelector ItemStyleSelector { get; set; }

        [Description("Gets or sets the object that uses custom logic to choose a template applied to items in this BarItemMenuHeader.")]
        public DataTemplateSelector ItemTemplateSelector { get; set; }

        public CommonBarItemCollection Items { get; }

        [Description("Gets or sets the source of items for this BarItemMenuHeader.")]
        public IEnumerable ItemsSource { get; set; }

        bool ILinksHolder.ShowDescription { get; }

        BarItemLinkCollection ILinksHolder.Links { get; }

        ImmediateActionsManager ILinksHolder.ImmediateActionsManager { get; }

        MergedLinksHolderCollection ILinksHolder.MergedChildren { get; }

        CustomizedBarItemLinkCollection ILinksHolder.CustomizedItems { get; }

        BarItemLinkCollection ILinksHolder.ActualLinks { get; }

        ILinksHolder ILinksHolder.MergedParent { get; set; }

        GlyphSize ILinksHolder.ItemsGlyphSize { get; }

        Size ILinksHolder.CustomItemsGlyphSize { get; }

        LinksHolderType ILinksHolder.HolderType { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemMenuHeader.<>c <>9;
            public static Func<BarItemMenuHeader, IList> <>9__50_0;
            public static Func<BarItemMenuHeader, BarItem> <>9__50_1;
            public static LinkControlAction<IBarItemLinkControl> <>9__52_0;
            public static LinkControlAction<IBarItemLinkControl> <>9__53_0;

            static <>c();
            internal BarItemLink <.cctor>b__11_0(object <arg>);
            internal BarItemLinkControlBase <.cctor>b__11_1(object <arg>);
            internal void <OnItemsOrientationPropertyChanged>b__53_0(IBarItemLinkControl lControl);
            internal IList <OnItemsSourceChanged>b__50_0(BarItemMenuHeader bar);
            internal BarItem <OnItemsSourceChanged>b__50_1(BarItemMenuHeader bar);
            internal void <OnMinColCountPropertyChanged>b__52_0(IBarItemLinkControl lControl);
        }
    }
}

