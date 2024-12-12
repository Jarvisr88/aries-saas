namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    internal static class DataControlOriginationElementHelper
    {
        public static void EnumerateDependentElements<T>(DataControlBase sourceControl, Func<DataControlBase, T> getTarget, Action<T> targetInOpenDetailHandler, Action<T> targetInClosedDetailHandler = null)
        {
            EnumerateDependentElementsCore<T>(sourceControl, getTarget, targetInOpenDetailHandler, targetInClosedDetailHandler, false, false);
        }

        private static void EnumerateDependentElementsCore<T>(DataControlBase sourceControl, Func<DataControlBase, T> getTarget, Action<T> targetInOpenDetailHandler, Action<T> targetInClosedDetailHandler, bool includeSourceControl, bool skipOriginationControl)
        {
            DataControlBase originationDataControl = sourceControl.GetOriginationDataControl();
            T local = getTarget(sourceControl);
            if (!skipOriginationControl && (includeSourceControl || !ReferenceEquals(sourceControl, originationDataControl)))
            {
                targetInOpenDetailHandler(getTarget(originationDataControl));
            }
            foreach (DataControlBase base3 in originationDataControl.DetailClones)
            {
                if (includeSourceControl || !ReferenceEquals(base3, sourceControl))
                {
                    if (((RowDetailInfoBase) base3.DataControlParent).IsExpanded)
                    {
                        targetInOpenDetailHandler(getTarget(base3));
                        continue;
                    }
                    if (targetInClosedDetailHandler != null)
                    {
                        targetInClosedDetailHandler(getTarget(base3));
                    }
                }
            }
        }

        public static void EnumerateDependentElementsIncludingSource<T>(DataControlBase sourceControl, Func<DataControlBase, T> getTarget, Action<T> targetInOpenDetailHandler, Action<T> targetInClosedDetailHandler = null)
        {
            EnumerateDependentElementsCore<T>(sourceControl, getTarget, targetInOpenDetailHandler, targetInClosedDetailHandler, true, false);
        }

        public static void EnumerateDependentElementsSkipOriginationControl<T>(DataControlBase sourceControl, Func<DataControlBase, T> getTarget, Action<T> targetInOpenDetailHandler, Action<T> targetInClosedDetailHandler = null)
        {
            EnumerateDependentElementsCore<T>(sourceControl, getTarget, targetInOpenDetailHandler, targetInClosedDetailHandler, false, true);
        }

        public static void UpdateActualTemplateSelector(DependencyObject target, DependencyObject originationObject, DependencyPropertyKey propertyKey, DataTemplateSelector selector, DataTemplate template, Func<DataTemplateSelector, DataTemplate, DataTemplateSelector> createWrapper = null)
        {
            createWrapper ??= (<>c.<>9__4_0 ??= (s, t) => new ActualTemplateSelectorWrapper(s, t));
            target.SetValue(propertyKey, (originationObject != null) ? originationObject.GetValue(propertyKey.DependencyProperty) : createWrapper(selector, template));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataControlOriginationElementHelper.<>c <>9 = new DataControlOriginationElementHelper.<>c();
            public static Func<DataTemplateSelector, DataTemplate, DataTemplateSelector> <>9__4_0;

            internal DataTemplateSelector <UpdateActualTemplateSelector>b__4_0(DataTemplateSelector s, DataTemplate t) => 
                new ActualTemplateSelectorWrapper(s, t);
        }
    }
}

