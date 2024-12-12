namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    internal static class DataControlSelectionHelper
    {
        internal static bool AllowUpdateCurrentItem(this DataControlBase dataControl) => 
            !dataControl.AllowUpdateTwoWayBoundPropertiesOnSynchronization ? (!dataControl.DataSourceChangingLocker.IsLocked || !dataControl.HasCurrentItemBinding()) : (!dataControl.UpdateCurrentItemWasLocked || ((dataControl.DataView != null) && (dataControl.DataView.IsSynchronizedWithCurrentItem && (dataControl.DataProviderBase.CollectionViewSource != null))));

        internal static bool AllowUpdateSelectedItems(this DataControlBase dataControl) => 
            !dataControl.DataSourceChangingLocker.IsLocked || (dataControl.AllowUpdateTwoWayBoundPropertiesOnSynchronization || ReferenceEquals(dataControl.GetBindingExpression(DataControlBase.SelectedItemsProperty), null));

        internal static List<DataControlBase> GetAllOriginationDataControls(this DataControlBase dataControl)
        {
            List<DataControlBase> dataControls = new List<DataControlBase>();
            dataControl.UpdateAllOriginationDataControls(delegate (DataControlBase x) {
                dataControls.Add(x);
            });
            return dataControls;
        }

        private static BindingMode? GetBindingMode(FrameworkElement element, DependencyProperty property)
        {
            BindingExpression bindingExpression = element.GetBindingExpression(property);
            if ((bindingExpression != null) && (bindingExpression.ParentBinding != null))
            {
                return new BindingMode?(bindingExpression.ParentBinding.Mode);
            }
            return null;
        }

        internal static List<DataControlBase> GetThisAndOwnerDataControls(this DataControlBase dataControl)
        {
            List<DataControlBase> dataControls = new List<DataControlBase>();
            dataControl.EnumerateThisAndOwnerDataControls(delegate (DataControlBase x) {
                dataControls.Add(x);
            });
            return dataControls;
        }

        internal static bool HasCurrentItemBinding(this DataControlBase dataControl) => 
            dataControl.HasDefaultOrTwoWayBinding(DataControlBase.CurrentItemProperty) || (dataControl.DataView.HasDefaultOrTwoWayBinding(DataViewBase.FocusedRowProperty) || ((dataControl.SelectionMode == MultiSelectMode.None) && dataControl.HasDefaultOrTwoWayBinding(DataControlBase.SelectedItemProperty)));

        private static bool HasDefaultOrTwoWayBinding(this FrameworkElement element, DependencyProperty property)
        {
            BindingMode? bindingMode = GetBindingMode(element, property);
            BindingMode? nullable2 = bindingMode;
            BindingMode twoWay = BindingMode.Default;
            if ((((BindingMode) nullable2.GetValueOrDefault()) == twoWay) ? (nullable2 != null) : false)
            {
                return true;
            }
            nullable2 = bindingMode;
            twoWay = BindingMode.TwoWay;
            return ((((BindingMode) nullable2.GetValueOrDefault()) == twoWay) ? (nullable2 != null) : false);
        }

        internal static bool HasSelectedItems(this DataControlBase dataControl) => 
            (dataControl.SelectedItems != null) && (dataControl.SelectedItems.Count != 0);

        internal static bool HasValue(this DataControlBase dataControl, DependencyProperty property)
        {
            if (dataControl.GetValue(property) != null)
            {
                return true;
            }
            Func<BindingMode?, bool> evaluator = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Func<BindingMode?, bool> local1 = <>c.<>9__4_0;
                evaluator = <>c.<>9__4_0 = delegate (BindingMode? mode) {
                    BindingMode? nullable = mode;
                    BindingMode oneWayToSource = BindingMode.OneWayToSource;
                    return (((BindingMode) nullable.GetValueOrDefault()) == oneWayToSource) ? (nullable == null) : true;
                };
            }
            return GetBindingMode(dataControl, property).Return<BindingMode, bool>(evaluator, (<>c.<>9__4_1 ??= () => false));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataControlSelectionHelper.<>c <>9 = new DataControlSelectionHelper.<>c();
            public static Func<BindingMode?, bool> <>9__4_0;
            public static Func<bool> <>9__4_1;

            internal bool <HasValue>b__4_0(BindingMode? mode)
            {
                BindingMode? nullable = mode;
                BindingMode oneWayToSource = BindingMode.OneWayToSource;
                return ((((BindingMode) nullable.GetValueOrDefault()) == oneWayToSource) ? (nullable == null) : true);
            }

            internal bool <HasValue>b__4_1() => 
                false;
        }
    }
}

