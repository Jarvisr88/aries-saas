namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.FilteringUI;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class ExcelSmartColumnFilterInfo : ColumnFilterInfoBase
    {
        private UnsubscribeAction unsubscribeFromFilteringContext;

        public ExcelSmartColumnFilterInfo(ColumnBase column) : base(column)
        {
        }

        internal override void ApplyDelayedFilter()
        {
        }

        public override bool CanShowFilterPopup()
        {
            object obj1;
            DataViewBase view = base.View;
            if (view == null)
            {
                DataViewBase local1 = view;
                obj1 = null;
            }
            else
            {
                DataControlBase dataControl = view.DataControl;
                if (dataControl != null)
                {
                    obj1 = dataControl.DataProviderBase.Columns[base.Column.FieldName];
                }
                else
                {
                    DataControlBase local2 = dataControl;
                    obj1 = null;
                }
            }
            return ((obj1 != null) && (!this.IsNoneFiltersAllowed<DevExpress.Xpf.Grid.AllowedUnaryFilters>(base.Column.AllowedUnaryFilters, DevExpress.Xpf.Grid.AllowedUnaryFilters.None) || (!this.IsNoneFiltersAllowed<DevExpress.Xpf.Grid.AllowedBinaryFilters>(base.Column.AllowedBinaryFilters, DevExpress.Xpf.Grid.AllowedBinaryFilters.None) || (!this.IsNoneFiltersAllowed<DevExpress.Xpf.Grid.AllowedBetweenFilters>(base.Column.AllowedBetweenFilters, DevExpress.Xpf.Grid.AllowedBetweenFilters.None) || (!this.IsNoneFiltersAllowed<DevExpress.Xpf.Grid.AllowedDateTimeFilters>(base.Column.AllowedDateTimeFilters, DevExpress.Xpf.Grid.AllowedDateTimeFilters.None) || (!this.IsNoneFiltersAllowed<DevExpress.Xpf.Grid.AllowedAnyOfFilters>(base.Column.AllowedAnyOfFilters, DevExpress.Xpf.Grid.AllowedAnyOfFilters.None) || !this.IsNoneFiltersAllowed<AllowedDataAnalysisFilters>(base.Column.AllowedDataAnalysisFilters, AllowedDataAnalysisFilters.None)))))));
        }

        protected override void ClearPopupData(PopupBaseEdit popup)
        {
        }

        internal override PopupBaseEdit CreateColumnFilterPopup()
        {
            if (base.View == null)
            {
                return null;
            }
            PopupBaseEdit edit1 = new PopupBaseEdit();
            edit1.ShowNullText = false;
            edit1.IsTextEditable = false;
            PopupBaseEdit popup = edit1;
            ExcelColumnFilterInfoBase.SetDefaultPopupSize(popup, base.Column);
            return popup;
        }

        private static DataTemplate CreatePopupContentTemplate() => 
            (DataTemplate) XamlReader.Parse("<DataTemplate xmlns:dxfui='http://schemas.devexpress.com/winfx/2008/xaml/core/filteringui' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml' xmlns:dxg='http://schemas.devexpress.com/winfx/2008/xaml/grid'>\r\n                <dxfui:ExcelStyleFilterElement x:Name='PART_FilterElement' />\r\n            </DataTemplate>");

        private static ControlTemplate CreatePopupTemplate() => 
            (ControlTemplate) XamlReader.Parse("<ControlTemplate xmlns:dxg='http://schemas.devexpress.com/winfx/2008/xaml/grid' xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>\r\n                <dxg:ColumnFilteringUIContentControl Content='{Binding}' ApplyFilterImmediately='{Binding ApplyFilterImmediately}' />\r\n            </ControlTemplate>");

        protected internal override CriteriaOperator GetFilterCriteria()
        {
            throw new NotImplementedException();
        }

        private UniqueValues GetUniqueValuesForValuesTab(UniqueValues uniqueValues, PopupBaseEdit popup)
        {
            Lazy<ExcelColumnFilterSettings> excelColumnFilterSettingsLazy = new Lazy<ExcelColumnFilterSettings>(delegate {
                ExcelColumnFilterSettings settings = new ExcelColumnFilterSettings();
                Func<ReadOnlyCollection<object>, IEnumerable<object>> left = <>c.<>9__10_1;
                if (<>c.<>9__10_1 == null)
                {
                    Func<ReadOnlyCollection<object>, IEnumerable<object>> local1 = <>c.<>9__10_1;
                    left = <>c.<>9__10_1 = x => x;
                }
                settings.FilterItems = new TrackChangesList<object>(uniqueValues.Value.Match<IEnumerable<object>>(left, <>c.<>9__10_2 ??= delegate (ReadOnlyCollection<Counted<object>> x) {
                    Func<Counted<object>, object> selector = <>c.<>9__10_3;
                    if (<>c.<>9__10_3 == null)
                    {
                        Func<Counted<object>, object> local1 = <>c.<>9__10_3;
                        selector = <>c.<>9__10_3 = y => y.Value;
                    }
                    return x.Select<Counted<object>, object>(selector);
                }).ToList<object>());
                return settings;
            });
            if (base.View != null)
            {
                base.View.RaiseFilterPopupEvent(base.Column, popup, excelColumnFilterSettingsLazy);
            }
            return ((!excelColumnFilterSettingsLazy.IsValueCreated || !excelColumnFilterSettingsLazy.Value.IsChanged) ? uniqueValues : UniqueValues.FromValues(excelColumnFilterSettingsLazy.Value.FilterItems.ToReadOnlyCollection<object>()));
        }

        private bool IsNoneFiltersAllowed<T>(T? allowedFilters, T noneFilters) where T: struct => 
            (allowedFilters != null) ? allowedFilters.Value.Equals(noneFilters) : this.GetIsVirtualSource();

        internal override void OnPopupClosed(PopupBaseEdit popup, bool applyFilter)
        {
            base.OnPopupClosed(popup, applyFilter);
            if (this.unsubscribeFromFilteringContext == null)
            {
                UnsubscribeAction unsubscribeFromFilteringContext = this.unsubscribeFromFilteringContext;
            }
            else
            {
                this.unsubscribeFromFilteringContext();
            }
            this.unsubscribeFromFilteringContext = null;
        }

        protected override void RaiseFilterPopupEvent(PopupBaseEdit popup)
        {
        }

        protected override void UpdatePopupData(PopupBaseEdit popup)
        {
            FilteringUIContext filteringContext = base.View.DataControl.FilteringContext;
            bool? useCommandManager = null;
            DelegateCommand clearFilterCommand = new DelegateCommand(() => filteringContext.ApplyFilter(this.Column.FieldName, null), () => filteringContext.GetFilter(this.Column.FieldName) != null, useCommandManager);
            this.unsubscribeFromFilteringContext = filteringContext.RegisterListener(new FilteringContextListener(() => clearFilterCommand.RaiseCanExecuteChanged()));
            DataTemplate template = base.Column.CustomColumnFilterPopupTemplate ?? CreatePopupContentTemplate();
            ColumnFilteringUIDataContext context = new ColumnFilteringUIDataContext(filteringContext, base.Column.FieldName, template, base.Column.ImmediateUpdateColumnFilter, base.Column.GetShowAllTableValuesInFilterPopup(), base.View.LocalizationDescriptor.GetValue(0x30.ToString()), clearFilterCommand, getValues => this.GetUniqueValuesForValuesTab(getValues, popup));
            popup.DataContext = context;
            popup.PopupContentTemplate = CreatePopupTemplate();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelSmartColumnFilterInfo.<>c <>9 = new ExcelSmartColumnFilterInfo.<>c();
            public static Func<ReadOnlyCollection<object>, IEnumerable<object>> <>9__10_1;
            public static Func<Counted<object>, object> <>9__10_3;
            public static Func<ReadOnlyCollection<Counted<object>>, IEnumerable<object>> <>9__10_2;

            internal IEnumerable<object> <GetUniqueValuesForValuesTab>b__10_1(ReadOnlyCollection<object> x) => 
                x;

            internal IEnumerable<object> <GetUniqueValuesForValuesTab>b__10_2(ReadOnlyCollection<Counted<object>> x)
            {
                Func<Counted<object>, object> selector = <>9__10_3;
                if (<>9__10_3 == null)
                {
                    Func<Counted<object>, object> local1 = <>9__10_3;
                    selector = <>9__10_3 = y => y.Value;
                }
                return x.Select<Counted<object>, object>(selector);
            }

            internal object <GetUniqueValuesForValuesTab>b__10_3(Counted<object> y) => 
                y.Value;
        }
    }
}

