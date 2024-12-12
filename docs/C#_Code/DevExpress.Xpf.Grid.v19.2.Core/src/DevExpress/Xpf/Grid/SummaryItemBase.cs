namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Data.Summary;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class SummaryItemBase : DependencyObject, ICaptionSupport, IAlignmentItem, ISummaryItem
    {
        public static readonly DependencyProperty TagProperty;
        public static readonly DependencyProperty SummaryTypeProperty;
        public static readonly DependencyProperty FieldNameProperty;
        public static readonly DependencyProperty DisplayFormatProperty;
        public static readonly DependencyProperty ShowInColumnProperty;
        public static readonly DependencyProperty VisibleProperty;
        public static readonly DependencyProperty AlignmentProperty;
        public static readonly DependencyProperty IsLastProperty;
        public static readonly DependencyProperty FixedTotalSummaryElementStyleProperty;
        public static readonly DependencyProperty TotalSummaryElementStyleProperty;
        public static readonly DependencyProperty CalculationModeProperty;
        private WeakReference collectionReference;

        static SummaryItemBase()
        {
            Type ownerType = typeof(DevExpress.Xpf.Grid.SummaryItemBase);
            TagProperty = DependencyPropertyManager.Register("Tag", typeof(object), ownerType, new PropertyMetadata(null, (d, e) => ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e)));
            SummaryTypeProperty = DependencyPropertyManager.Register("SummaryType", typeof(SummaryItemType), ownerType, new PropertyMetadata(SummaryItemType.None, (d, e) => ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e)));
            FieldNameProperty = DependencyPropertyManager.Register("FieldName", typeof(string), ownerType, new PropertyMetadata(string.Empty, (d, e) => ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e), (CoerceValueCallback) ((d, baseValue) => ((baseValue == null) ? string.Empty : baseValue))));
            DisplayFormatProperty = DependencyPropertyManager.Register("DisplayFormat", typeof(string), ownerType, new PropertyMetadata(string.Empty, (d, e) => ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e), (CoerceValueCallback) ((d, baseValue) => ((baseValue == null) ? string.Empty : baseValue))));
            ShowInColumnProperty = DependencyPropertyManager.Register("ShowInColumn", typeof(string), ownerType, new PropertyMetadata(string.Empty, (d, e) => ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e), (CoerceValueCallback) ((d, baseValue) => ((baseValue == null) ? string.Empty : baseValue))));
            VisibleProperty = DependencyPropertyManager.Register("Visible", typeof(bool), ownerType, new PropertyMetadata(true, (d, e) => ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e)));
            AlignmentProperty = DependencyPropertyManager.Register("Alignment", typeof(GridSummaryItemAlignment), ownerType, new PropertyMetadata(GridSummaryItemAlignment.Default, (d, e) => ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e)));
            IsLastProperty = DependencyPropertyManager.Register("IsLast", typeof(bool), ownerType, new PropertyMetadata(false));
            FixedTotalSummaryElementStyleProperty = DependencyPropertyManager.Register("FixedTotalSummaryElementStyle", typeof(Style), typeof(DevExpress.Xpf.Grid.SummaryItemBase), new PropertyMetadata(null, (d, e) => ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e)));
            TotalSummaryElementStyleProperty = DependencyProperty.Register("TotalSummaryElementStyle", typeof(Style), typeof(DevExpress.Xpf.Grid.SummaryItemBase), new PropertyMetadata(null, (d, e) => ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e)));
            CalculationModeProperty = DependencyProperty.Register("CalculationMode", typeof(GridSummaryCalculationMode?), typeof(DevExpress.Xpf.Grid.SummaryItemBase), new PropertyMetadata(null, (d, e) => ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e)));
        }

        protected SummaryItemBase()
        {
        }

        private bool AllowColumnDisplayFormat(SummaryItemType summaryType) => 
            (summaryType != SummaryItemType.Count) && ((summaryType != SummaryItemType.None) && (summaryType != SummaryItemType.Custom));

        private SummaryFormatFactory CreateDefaultGroupColumnSummaryFormatFactory()
        {
            SummaryFormatFactory factory1 = new SummaryFormatFactory();
            factory1.Average = 0x1d;
            factory1.Min = 0x1b;
            factory1.Max = 0x1c;
            factory1.Count = 0x1a;
            factory1.Sum = 30;
            return factory1;
        }

        private SummaryFormatFactory CreateDefaultGroupColumnSummaryFormatSameColumnFactory()
        {
            SummaryFormatFactory factory1 = new SummaryFormatFactory();
            factory1.Average = 0x18;
            factory1.Min = 0x16;
            factory1.Max = 0x17;
            factory1.Count = 0x15;
            factory1.Sum = 0x19;
            return factory1;
        }

        private SummaryFormatFactory CreateDefaultGroupSummaryFormatFactory()
        {
            SummaryFormatFactory factory1 = new SummaryFormatFactory();
            factory1.Average = 9;
            factory1.Min = 7;
            factory1.Max = 8;
            factory1.Count = 6;
            factory1.Sum = 10;
            return factory1;
        }

        private SummaryFormatFactory CreateDefaultTotalSummaryFormatFactory()
        {
            SummaryFormatFactory factory1 = new SummaryFormatFactory();
            factory1.Average = 0x13;
            factory1.Min = 0x11;
            factory1.Max = 0x12;
            factory1.Count = 0x10;
            factory1.Sum = 20;
            return factory1;
        }

        private SummaryFormatFactory CreateDefaultTotalSummaryFormatSameColumnFactory()
        {
            SummaryFormatFactory factory1 = new SummaryFormatFactory();
            factory1.Average = 14;
            factory1.Min = 12;
            factory1.Max = 13;
            factory1.Count = 11;
            factory1.Sum = 15;
            return factory1;
        }

        private SummaryFormatFactory CreateFooterFormatFactory(ColumnSummaryType columnSummaryType) => 
            (columnSummaryType == ColumnSummaryType.Total) ? this.CreateDefaultTotalSummaryFormatFactory() : this.CreateDefaultGroupColumnSummaryFormatFactory();

        private SummaryFormatFactory CreateFooterSameColumnFormatFactory(ColumnSummaryType columnSummaryType) => 
            (columnSummaryType == ColumnSummaryType.Total) ? this.CreateDefaultTotalSummaryFormatSameColumnFactory() : this.CreateDefaultGroupColumnSummaryFormatSameColumnFactory();

        internal bool EqualsToControllerSummaryItem(SummaryItem controllerSummaryItem) => 
            (this.FieldName == controllerSummaryItem.FieldName) && (this.SummaryType == controllerSummaryItem.SummaryType);

        private string GetDisplayTextCore(IFormatProvider language, string format, params object[] args) => 
            DisplayFormatHelper.GetDisplayTextFromDisplayFormat(language, format, args);

        internal string GetFooterDisplayFormat(ColumnSummaryType columnSummaryType) => 
            this.GetFooterDisplayFormat(this.CreateFooterFormatFactory(columnSummaryType), string.Empty, true, true);

        protected internal virtual string GetFooterDisplayFormat(string columnDisplayFormat, ColumnSummaryType columnSummaryType) => 
            this.GetFooterDisplayFormat(this.CreateFooterFormatFactory(columnSummaryType), columnDisplayFormat, true, false);

        private string GetFooterDisplayFormat(SummaryFormatFactory formatFactory, string columnDisplayFormat, bool columnIsSet, bool forseUseColumnDisplayFormat)
        {
            string displayFormat = FormatStringConverter.GetDisplayFormat(forseUseColumnDisplayFormat ? columnDisplayFormat : this.DisplayFormat);
            if (displayFormat == string.Empty)
            {
                SummaryItemType summaryType = this.SummaryType;
                displayFormat = formatFactory.CreateFormat(summaryType);
                if ((columnDisplayFormat != string.Empty) && this.AllowColumnDisplayFormat(summaryType))
                {
                    string[] textArray3;
                    if (!columnIsSet && (DisplayFormatHelper.GetLastParameterIndex(displayFormat) != 1))
                    {
                        textArray3 = new string[] { columnDisplayFormat };
                    }
                    else
                    {
                        textArray3 = new string[] { columnDisplayFormat, "{1}" };
                    }
                    displayFormat = string.Format(displayFormat, (object[]) textArray3);
                }
            }
            return displayFormat;
        }

        internal string GetFooterDisplayFormatSameColumn(ColumnSummaryType columnSummaryType) => 
            this.GetFooterDisplayFormatSameColumn(string.Empty, columnSummaryType, true);

        protected internal virtual string GetFooterDisplayFormatSameColumn(string columnDisplayFormat, ColumnSummaryType columnSummaryType) => 
            this.GetFooterDisplayFormatSameColumn(columnDisplayFormat, columnSummaryType, false);

        protected string GetFooterDisplayFormatSameColumn(string columnDisplayFormat, ColumnSummaryType columnSummaryType, bool forseUseColumnDisplayFormat) => 
            this.GetFooterDisplayFormat(this.CreateFooterSameColumnFormatFactory(columnSummaryType), columnDisplayFormat, false, forseUseColumnDisplayFormat);

        public string GetFooterDisplayText(IFormatProvider language, string columnCaption, object value, string columnDisplayFormat) => 
            this.GetFooterDisplayText(language, columnCaption, value, columnDisplayFormat, false);

        internal string GetFooterDisplayText(IFormatProvider language, string columnCaption, object value, string columnDisplayFormat, bool forceColumnDisplayFormat) => 
            this.GetFooterDisplayTextCore(language, columnCaption, value, columnDisplayFormat, ColumnSummaryType.Total, forceColumnDisplayFormat);

        private string GetFooterDisplayTextCore(IFormatProvider language, string columnCaption, object value, string columnDisplayFormat, ColumnSummaryType columnSummaryType) => 
            this.GetFooterDisplayTextCore(language, columnCaption, value, columnDisplayFormat, columnSummaryType, false);

        private string GetFooterDisplayTextCore(IFormatProvider language, string columnCaption, object value, string columnDisplayFormat, ColumnSummaryType columnSummaryType, bool forceColumnDisplayFormat)
        {
            if (!string.IsNullOrEmpty(this.ShowInColumn) && (this.FieldName != this.ShowInColumn))
            {
                return this.GetFooterDisplayTextWithColumnNameCore(language, columnCaption, value, columnDisplayFormat, columnSummaryType);
            }
            object[] args = new object[] { value, columnCaption };
            return this.GetDisplayTextCore(language, this.GetFooterDisplayFormatSameColumn(columnDisplayFormat, columnSummaryType, forceColumnDisplayFormat), args);
        }

        internal string GetFooterDisplayTextWithColumnName(IFormatProvider language, string columnCaption, object value, string columnDisplayFormat) => 
            this.GetFooterDisplayTextWithColumnNameCore(language, columnCaption, value, columnDisplayFormat, ColumnSummaryType.Total);

        private string GetFooterDisplayTextWithColumnNameCore(IFormatProvider language, string columnCaption, object value, string columnDisplayFormat, ColumnSummaryType columnSummaryType)
        {
            object[] args = new object[] { value, columnCaption };
            return this.GetDisplayTextCore(language, this.GetFooterDisplayFormat(columnDisplayFormat, columnSummaryType), args);
        }

        protected internal virtual Style GetFooterElementStyle() => 
            null;

        public string GetGroupColumnDisplayText(IFormatProvider language, string columnCaption, object value, string columnDisplayFormat) => 
            this.GetFooterDisplayTextCore(language, columnCaption, value, columnDisplayFormat, ColumnSummaryType.Group);

        internal string GetGroupDisplayFormat() => 
            this.GetFooterDisplayFormat(this.CreateDefaultGroupSummaryFormatFactory(), string.Empty, true, true);

        protected virtual string GetGroupDisplayFormat(string columnDisplayFormat) => 
            this.GetFooterDisplayFormat(this.CreateDefaultGroupSummaryFormatFactory(), columnDisplayFormat, true, false);

        public string GetGroupDisplayText(IFormatProvider language, string columnCaption, object value, string columnDisplayFormat)
        {
            object[] args = new object[] { value, columnCaption };
            return this.GetDisplayTextCore(language, this.GetGroupDisplayFormat(columnDisplayFormat), args);
        }

        internal bool IsInDataController(SummaryItemCollection controllerSummaries) => 
            controllerSummaries.GetSummaryItemByTag(this) != null;

        protected void OnSummaryChanged(DependencyPropertyChangedEventArgs e)
        {
            GridSummaryCalculationMode? calculationModeCore = this.CalculationModeCore;
            GridSummaryCalculationMode? calculationMode = this.CalculationMode;
            if ((calculationModeCore.GetValueOrDefault() == calculationMode.GetValueOrDefault()) ? ((calculationModeCore != null) != (calculationMode != null)) : true)
            {
                this.CalculationModeChanged = true;
                this.CalculationModeCore = this.CalculationMode;
            }
            try
            {
                if (this.Collection != null)
                {
                    this.Collection.OnSummaryChanged(this, e);
                }
            }
            finally
            {
                this.CalculationModeChanged = false;
            }
        }

        internal ISummaryItemOwner Collection
        {
            get => 
                (this.collectionReference == null) ? null : ((ISummaryItemOwner) this.collectionReference.Target);
            set => 
                this.collectionReference = new WeakReference(value);
        }

        [Description("Gets or sets the aggregate function type. This is a dependency property."), XtraSerializableProperty]
        public SummaryItemType SummaryType
        {
            get => 
                (SummaryItemType) base.GetValue(SummaryTypeProperty);
            set => 
                base.SetValue(SummaryTypeProperty, value);
        }

        [Description("Gets or sets custom data associated with the summary item. This is a dependency property."), TypeConverter(typeof(ObjectConverter)), Category("Data")]
        public object Tag
        {
            get => 
                base.GetValue(TagProperty);
            set => 
                base.SetValue(TagProperty, value);
        }

        [Description("Gets or sets the name of a data source field whose values are used for summary calculation. This is a dependency property."), XtraSerializableProperty]
        public string FieldName
        {
            get => 
                (string) base.GetValue(FieldNameProperty);
            set => 
                base.SetValue(FieldNameProperty, value);
        }

        [Description("Gets or sets the pattern used to format the summary value. This is a dependency property."), XtraSerializableProperty]
        public string DisplayFormat
        {
            get => 
                (string) base.GetValue(DisplayFormatProperty);
            set => 
                base.SetValue(DisplayFormatProperty, value);
        }

        [Description("Gets or sets a value that specifies a column in whose footer or group rows the summary is displayed. This is a dependency property."), XtraSerializableProperty]
        public string ShowInColumn
        {
            get => 
                (string) base.GetValue(ShowInColumnProperty);
            set => 
                base.SetValue(ShowInColumnProperty, value);
        }

        [Description("Gets or sets whether the summary item is displayed within a View. This is a dependency property."), XtraSerializableProperty]
        public bool Visible
        {
            get => 
                (bool) base.GetValue(VisibleProperty);
            set => 
                base.SetValue(VisibleProperty, value);
        }

        [Description("Gets or sets the horizontal alignment within the Fixed Summary Panel. This is a dependency property."), XtraSerializableProperty]
        public GridSummaryItemAlignment Alignment
        {
            get => 
                (GridSummaryItemAlignment) base.GetValue(AlignmentProperty);
            set => 
                base.SetValue(AlignmentProperty, value);
        }

        public bool IsLast
        {
            get => 
                (bool) base.GetValue(IsLastProperty);
            set => 
                base.SetValue(IsLastProperty, value);
        }

        public Style FixedTotalSummaryElementStyle
        {
            get => 
                (Style) base.GetValue(FixedTotalSummaryElementStyleProperty);
            set => 
                base.SetValue(FixedTotalSummaryElementStyleProperty, value);
        }

        public Style TotalSummaryElementStyle
        {
            get => 
                (Style) base.GetValue(TotalSummaryElementStyleProperty);
            set => 
                base.SetValue(TotalSummaryElementStyleProperty, value);
        }

        [Description("Gets or sets a mode that specifies by which rows the summary value is calculated."), XtraSerializableProperty]
        public GridSummaryCalculationMode? CalculationMode
        {
            get => 
                (GridSummaryCalculationMode?) base.GetValue(CalculationModeProperty);
            set => 
                base.SetValue(CalculationModeProperty, value);
        }

        internal GridSummaryCalculationMode? CalculationModeCore { get; private set; }

        internal virtual string ActualShowInColumn =>
            string.IsNullOrEmpty(this.ShowInColumn) ? this.FieldName : this.ShowInColumn;

        internal bool CalculationModeChanged { get; private set; }

        string ICaptionSupport.Caption
        {
            get
            {
                if (this.SummaryType == SummaryItemType.None)
                {
                    return string.Empty;
                }
                string str = this.SummaryType.ToString();
                if (!string.IsNullOrEmpty(this.FieldName))
                {
                    str = this.FieldName + " (" + str + ")";
                }
                return str;
            }
        }

        internal virtual bool? IgnoreNullValues =>
            null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.Grid.SummaryItemBase.<>c <>9 = new DevExpress.Xpf.Grid.SummaryItemBase.<>c();

            internal void <.cctor>b__11_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e);
            }

            internal void <.cctor>b__11_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e);
            }

            internal void <.cctor>b__11_10(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e);
            }

            internal void <.cctor>b__11_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e);
            }

            internal void <.cctor>b__11_12(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e);
            }

            internal void <.cctor>b__11_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e);
            }

            internal object <.cctor>b__11_3(DependencyObject d, object baseValue) => 
                (baseValue == null) ? string.Empty : baseValue;

            internal void <.cctor>b__11_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e);
            }

            internal object <.cctor>b__11_5(DependencyObject d, object baseValue) => 
                (baseValue == null) ? string.Empty : baseValue;

            internal void <.cctor>b__11_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e);
            }

            internal object <.cctor>b__11_7(DependencyObject d, object baseValue) => 
                (baseValue == null) ? string.Empty : baseValue;

            internal void <.cctor>b__11_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e);
            }

            internal void <.cctor>b__11_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DevExpress.Xpf.Grid.SummaryItemBase) d).OnSummaryChanged(e);
            }
        }

        public enum ColumnSummaryType
        {
            Total,
            Group
        }
    }
}

