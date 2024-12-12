namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    [ContentProperty("Items")]
    public class BarItemLinkHolderBase : SLBarItemLinkHolderBase, ILinksHolder, IMultipleElementRegistratorSupport, IBarNameScopeSupport, IInputElement, ILogicalChildrenContainer
    {
        public static readonly DependencyProperty VisibleProperty;
        public static readonly DependencyProperty ItemLinksSourceProperty;
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty ItemTemplateSelectorProperty;
        public static readonly DependencyProperty ItemStyleProperty;
        public static readonly DependencyProperty ItemStyleSelectorProperty;
        public static readonly DependencyProperty CustomItemsGlyphSizeProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty BarItemsAttachedBehaviorProperty;
        public static readonly DependencyProperty ItemLinksSourceElementGeneratesUniqueBarItemProperty;
        private BarItemLinkCollection links;
        private BarManager manager;
        private CommonBarItemCollection itemsCore;
        private readonly ImmediateActionsManager immediateActionsManager;
        private MergedLinksHolderCollection mergedChildren;
        private CustomizedBarItemLinkCollection customizedItems;
        private ILinksHolder mergedParent;
        private BarItemGeneratorHelper<BarItemLinkHolderBase> itemGeneratorHelper;
        private readonly List<object> logicalChildrenContainerItems;

        event ValueChangedEventHandler<BarItemLinkCollection> ILinksHolder.ActualLinksChanged;

        static BarItemLinkHolderBase();
        public BarItemLinkHolderBase();
        protected virtual bool CoerceVisible(bool value);
        protected virtual BarItemLinkCollection CreateItemLinksCollection();
        protected virtual MergedLinksHolderCollection CreateMergedLinksCollection();
        GlyphSize ILinksHolder.GetDefaultItemsGlyphSize(LinkContainerType linkContainerType);
        IEnumerator ILinksHolder.GetLogicalChildrenEnumerator();
        void ILinksHolder.Merge(ILinksHolder holder);
        void ILinksHolder.OnLinkAdded(BarItemLinkBase link);
        void ILinksHolder.OnLinkRemoved(BarItemLinkBase link);
        void ILinksHolder.UnMerge();
        void ILinksHolder.UnMerge(ILinksHolder holder);
        void ILogicalChildrenContainer.AddLogicalChild(object child);
        void ILogicalChildrenContainer.RemoveLogicalChild(object child);
        object IMultipleElementRegistratorSupport.GetName(object registratorKey);
        protected virtual IEnumerable<object> GetRegistratorKeys();
        protected virtual object GetRegistratorName(object registratorKey);
        protected virtual void MergeCore(ILinksHolder holder);
        protected internal virtual void OnCustomItemsGlyphSizeChanged(Size oldValue, Size newValue);
        protected override void OnInitialized(EventArgs e);
        protected virtual void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e);
        protected virtual void OnItemLinksSourceChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void OnItemLinksSourceElementGeneratesUniqueBarItemChanged(bool oldValue);
        protected static void OnItemLinksSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnItemLinksTemplateChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnItemLinksTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnItemLinksTemplateSelectorChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnItemLinksTemplateSelectorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        private void OnLayoutUpdated(object sender, EventArgs e);
        protected virtual void OnLinksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected virtual void OnManagerChanged(BarManager newManager);
        protected virtual void OnManagerChanging(BarManager oldManager);
        protected virtual void OnMergedLinksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected virtual void OnNameChanged(string oldValue, string newValue);
        protected static void OnNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        protected virtual void OnVisibleChanged(DependencyPropertyChangedEventArgs e);
        protected static void OnVisiblePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e);
        protected void UnMergeCore();
        protected virtual void UnMergeCore(ILinksHolder holder);
        protected virtual void UpdateLinksControl();

        public Size CustomItemsGlyphSize { get; set; }

        [Description("Gets or sets the current BarManager.This is a dependency property."), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BarManager Manager { get; set; }

        protected override IEnumerator LogicalChildren { get; }

        [Description("Gets or sets whether the current container is visible.This is a dependency property.")]
        public bool Visible { get; set; }

        public CommonBarItemCollection Items { get; }

        [Description("Provides access to the collection of links owned by the current container."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public BarItemLinkCollection ItemLinks { get; }

        public object ItemLinksSource { get; set; }

        public DataTemplate ItemTemplate { get; set; }

        public DataTemplateSelector ItemTemplateSelector { get; set; }

        public Style ItemStyle { get; set; }

        public StyleSelector ItemStyleSelector { get; set; }

        protected BarItemGeneratorHelper<BarItemLinkHolderBase> ItemGeneratorHelper { get; }

        BarItemLinkCollection ILinksHolder.Links { get; }

        IEnumerable ILinksHolder.ItemsSource { get; }

        bool ILinksHolder.ShowDescription { get; }

        ImmediateActionsManager ILinksHolder.ImmediateActionsManager { get; }

        MergedLinksHolderCollection ILinksHolder.MergedChildren { get; }

        BarItemLinkCollection ILinksHolder.ActualLinks { get; }

        CustomizedBarItemLinkCollection ILinksHolder.CustomizedItems { get; }

        ILinksHolder ILinksHolder.MergedParent { get; set; }

        GlyphSize ILinksHolder.ItemsGlyphSize { get; }

        LinksHolderType ILinksHolder.HolderType { get; }

        [Description("Gets or sets whether each reference to a data object in an BarItemLinkHolderBase.ItemLinksSource for this BarItemLinkHolderBase should generate a unique BarItem, whether this data object was previously referenced. This is a dependency property.")]
        public bool ItemLinksSourceElementGeneratesUniqueBarItem { get; set; }

        IEnumerable<object> IMultipleElementRegistratorSupport.RegistratorKeys { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarItemLinkHolderBase.<>c <>9;

            static <>c();
            internal object <.cctor>b__13_0(DependencyObject d, object v);
            internal void <.cctor>b__13_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__13_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

