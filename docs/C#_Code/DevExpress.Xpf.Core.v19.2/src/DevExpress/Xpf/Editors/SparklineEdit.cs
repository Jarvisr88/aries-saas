namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Printing;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    [ToolboxTabName("DX.19.2: Data & Analytics"), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), DXToolboxBrowsable(DXToolboxItemKind.Free)]
    public class SparklineEdit : BaseEdit, IImageExportSettings, IExportSettings
    {
        private static readonly DependencyPropertyKey SparklineTypePropertyKey;
        public static readonly DependencyProperty SparklineTypeProperty;
        public static readonly DependencyProperty ItemsProperty;
        public static readonly DependencyProperty PointArgumentMemberProperty;
        public static readonly DependencyProperty PointValueMemberProperty;
        public static readonly DependencyProperty PointArgumentSortOrderProperty;
        public static readonly DependencyProperty FilterCriteriaProperty;
        public static readonly DependencyProperty PointArgumentRangeProperty;
        public static readonly DependencyProperty PointValueRangeProperty;
        private const SparklineSortOrder DefaultSortOrder = SparklineSortOrder.Ascending;

        static SparklineEdit()
        {
            Type ownerType = typeof(SparklineEdit);
            SparklineTypePropertyKey = DependencyPropertyManager.RegisterReadOnly("SparklineType", typeof(SparklineViewType), ownerType, new FrameworkPropertyMetadata(SparklineViewType.Line));
            SparklineTypeProperty = SparklineTypePropertyKey.DependencyProperty;
            ItemsProperty = DependencyPropertyManager.Register("Items", typeof(IEnumerable), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((SparklineEdit) o).ItemsChanged((IEnumerable) args.OldValue, (IEnumerable) args.NewValue), (o, value) => ((SparklineEdit) o).CoerceItems(value)));
            PointArgumentMemberProperty = DependencyPropertyManager.Register("PointArgumentMember", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.None, (o, args) => ((SparklineEdit) o).ArgumentMemberChanged((string) args.NewValue)));
            PointValueMemberProperty = DependencyPropertyManager.Register("PointValueMember", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.None, (o, args) => ((SparklineEdit) o).ValueMemberChanged((string) args.NewValue)));
            PointArgumentSortOrderProperty = DependencyPropertyManager.Register("PointArgumentSortOrder", typeof(SparklineSortOrder), ownerType, new FrameworkPropertyMetadata(SparklineSortOrder.Ascending, FrameworkPropertyMetadataOptions.None, (o, args) => ((SparklineEdit) o).PointArgumentSortOrderChanged((SparklineSortOrder) args.NewValue)));
            FilterCriteriaProperty = DependencyPropertyManager.Register("FilterCriteria", typeof(CriteriaOperator), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((SparklineEdit) o).FilterCriteriaChanged((CriteriaOperator) args.NewValue)));
            PointArgumentRangeProperty = DependencyProperty.Register("PointArgumentRange", typeof(DevExpress.Xpf.Editors.Range), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((SparklineEdit) o).OnPointArgumentRangeChanged((DevExpress.Xpf.Editors.Range) args.NewValue)));
            PointValueRangeProperty = DependencyProperty.Register("PointValueRange", typeof(DevExpress.Xpf.Editors.Range), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((SparklineEdit) o).OnPointValueRangeChanged((DevExpress.Xpf.Editors.Range) args.NewValue)));
        }

        public SparklineEdit()
        {
            this.SetDefaultStyleKey(typeof(SparklineEdit));
            this.ItemsProvider = this.CreateItemsProvider();
            Action<SparklineEdit, object, ItemsProviderChangedEventArgs> onEventAction = <>c.<>9__51_0;
            if (<>c.<>9__51_0 == null)
            {
                Action<SparklineEdit, object, ItemsProviderChangedEventArgs> local1 = <>c.<>9__51_0;
                onEventAction = <>c.<>9__51_0 = (owner, o, e) => owner.EditStrategy.ItemProviderChanged(e);
            }
            this.ItemsProviderChangedEventHandler = new ItemsProviderChangedEventHandler<SparklineEdit>(this, onEventAction);
        }

        protected virtual void ArgumentMemberChanged(string argument)
        {
            this.EditStrategy.ArgumentMemberChanged(argument);
        }

        protected virtual object CoerceItems(object baseValue) => 
            this.EditStrategy.CoerceItems(baseValue);

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new SparklinePropertyProvider(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new SparklineEditStrategy(this);

        protected virtual SparklineItemsProvider CreateItemsProvider() => 
            new SparklineItemsProvider();

        protected internal override BaseEditStyleSettings CreateStyleSettings() => 
            new LineSparklineStyleSettings();

        protected virtual void FilterCriteriaChanged(CriteriaOperator newCriteriaOperator)
        {
            this.EditStrategy.FilterCriteriaChanged(newCriteriaOperator);
        }

        protected internal override Brush GetPrintBorderBrush() => 
            Brushes.Transparent;

        protected virtual void ItemsChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            this.EditStrategy.ItemsChanged(oldValue, newValue);
        }

        protected virtual void OnPointArgumentRangeChanged(DevExpress.Xpf.Editors.Range range)
        {
            this.EditStrategy.PointArgumentRangeChanged(range);
            base.AddLogicalChild(range);
        }

        protected virtual void OnPointValueRangeChanged(DevExpress.Xpf.Editors.Range range)
        {
            this.EditStrategy.PointValueRangeChanged(range);
            base.AddLogicalChild(range);
        }

        protected virtual void PointArgumentSortOrderChanged(SparklineSortOrder newColumnSortOrder)
        {
            this.EditStrategy.PointArgumentSortOrderChanged(newColumnSortOrder);
        }

        protected internal virtual void SubscribeToItemsProviderChanged()
        {
            this.ItemsProvider.ItemsProviderChanged += this.ItemsProviderChangedEventHandler.Handler;
        }

        protected override void SubscribeToSettings(BaseEditSettings settings)
        {
            base.SubscribeToSettings(settings);
            if (settings != null)
            {
                this.ItemsProvider.ItemsProviderChanged += this.ItemsProviderChangedEventHandler.Handler;
            }
        }

        protected override void UnsubscribeFromSettings(BaseEditSettings settings)
        {
            base.UnsubscribeFromSettings(settings);
            if (settings != null)
            {
                this.ItemsProvider.ItemsProviderChanged -= this.ItemsProviderChangedEventHandler.Handler;
            }
        }

        protected internal virtual void UnsubscribeToItemsProviderChanged()
        {
            this.ItemsProvider.ItemsProviderChanged -= this.ItemsProviderChangedEventHandler.Handler;
        }

        protected virtual void ValueMemberChanged(string newValue)
        {
            this.EditStrategy.ValueMemberChanged(newValue);
        }

        private ItemsProviderChangedEventHandler<SparklineEdit> ItemsProviderChangedEventHandler { get; set; }

        protected internal SparklineItemsProvider ItemsProvider { get; private set; }

        public string PointArgumentMember
        {
            get => 
                (string) base.GetValue(PointArgumentMemberProperty);
            set => 
                base.SetValue(PointArgumentMemberProperty, value);
        }

        public string PointValueMember
        {
            get => 
                (string) base.GetValue(PointValueMemberProperty);
            set => 
                base.SetValue(PointValueMemberProperty, value);
        }

        public SparklineSortOrder PointArgumentSortOrder
        {
            get => 
                (SparklineSortOrder) base.GetValue(PointArgumentSortOrderProperty);
            set => 
                base.SetValue(PointArgumentSortOrderProperty, value);
        }

        public CriteriaOperator FilterCriteria
        {
            get => 
                (CriteriaOperator) base.GetValue(FilterCriteriaProperty);
            set => 
                base.SetValue(FilterCriteriaProperty, value);
        }

        public IEnumerable Items
        {
            get => 
                (IEnumerable) base.GetValue(ItemsProperty);
            set => 
                base.SetValue(ItemsProperty, value);
        }

        public SparklineViewType SparklineType
        {
            get => 
                (SparklineViewType) base.GetValue(SparklineTypeProperty);
            internal set => 
                base.SetValue(SparklineTypePropertyKey, value);
        }

        public DevExpress.Xpf.Editors.Range PointArgumentRange
        {
            get => 
                (DevExpress.Xpf.Editors.Range) base.GetValue(PointArgumentRangeProperty);
            set => 
                base.SetValue(PointArgumentRangeProperty, value);
        }

        public DevExpress.Xpf.Editors.Range PointValueRange
        {
            get => 
                (DevExpress.Xpf.Editors.Range) base.GetValue(PointValueRangeProperty);
            set => 
                base.SetValue(PointValueRangeProperty, value);
        }

        private SparklinePropertyProvider PropertyProvider =>
            base.PropertyProvider as SparklinePropertyProvider;

        protected internal override Type StyleSettingsType =>
            typeof(SparklineStyleSettings);

        private SparklineEditStrategy EditStrategy =>
            base.EditStrategy as SparklineEditStrategy;

        FrameworkElement IImageExportSettings.SourceElement =>
            this;

        ImageRenderMode IImageExportSettings.ImageRenderMode =>
            ImageRenderMode.MakeScreenshot;

        bool IImageExportSettings.ForceCenterImageMode =>
            false;

        object IImageExportSettings.ImageKey =>
            null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SparklineEdit.<>c <>9 = new SparklineEdit.<>c();
            public static Action<SparklineEdit, object, ItemsProviderChangedEventArgs> <>9__51_0;

            internal void <.cctor>b__9_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SparklineEdit) o).ItemsChanged((IEnumerable) args.OldValue, (IEnumerable) args.NewValue);
            }

            internal object <.cctor>b__9_1(DependencyObject o, object value) => 
                ((SparklineEdit) o).CoerceItems(value);

            internal void <.cctor>b__9_2(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SparklineEdit) o).ArgumentMemberChanged((string) args.NewValue);
            }

            internal void <.cctor>b__9_3(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SparklineEdit) o).ValueMemberChanged((string) args.NewValue);
            }

            internal void <.cctor>b__9_4(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SparklineEdit) o).PointArgumentSortOrderChanged((SparklineSortOrder) args.NewValue);
            }

            internal void <.cctor>b__9_5(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SparklineEdit) o).FilterCriteriaChanged((CriteriaOperator) args.NewValue);
            }

            internal void <.cctor>b__9_6(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SparklineEdit) o).OnPointArgumentRangeChanged((DevExpress.Xpf.Editors.Range) args.NewValue);
            }

            internal void <.cctor>b__9_7(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((SparklineEdit) o).OnPointValueRangeChanged((DevExpress.Xpf.Editors.Range) args.NewValue);
            }

            internal void <.ctor>b__51_0(SparklineEdit owner, object o, ItemsProviderChangedEventArgs e)
            {
                owner.EditStrategy.ItemProviderChanged(e);
            }
        }
    }
}

