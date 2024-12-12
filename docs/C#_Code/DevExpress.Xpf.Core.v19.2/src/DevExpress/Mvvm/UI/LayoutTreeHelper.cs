namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Media3D;

    public static class LayoutTreeHelper
    {
        private static IEnumerable<DependencyObject> GetChildrenCore(DependencyObject parent, bool includeStartNode, Func<DependencyObject, IEnumerable<DependencyObject>> getChildren, Func<DependencyObject, bool> skipChildren = null)
        {
            IEnumerable<DependencyObject> source = parent.Yield<DependencyObject>().Flatten<DependencyObject>(x => ((skipChildren == null) || !skipChildren(x)) ? getChildren(x) : Enumerable.Empty<DependencyObject>());
            return (includeStartNode ? source : source.Skip<DependencyObject>(1));
        }

        public static IEnumerable<DependencyObject> GetLogicalChildren(DependencyObject parent) => 
            GetLogicalChildrenCore(parent, false, null);

        internal static IEnumerable<DependencyObject> GetLogicalChildrenCore(DependencyObject parent, bool includeStartNode, Func<DependencyObject, bool> skipChildren = null)
        {
            Func<DependencyObject, IEnumerable<DependencyObject>> getChildren = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<DependencyObject, IEnumerable<DependencyObject>> local1 = <>c.<>9__6_0;
                getChildren = <>c.<>9__6_0 = x => LogicalTreeHelper.GetChildren(x).OfType<DependencyObject>();
            }
            return GetChildrenCore(parent, includeStartNode, getChildren, skipChildren);
        }

        private static DependencyObject GetParent(DependencyObject element) => 
            ((element is Visual) || (element is Visual3D)) ? VisualTreeHelper.GetParent(element) : (!(element is FrameworkContentElement) ? null : LogicalTreeHelper.GetParent(element));

        public static IEnumerable<DependencyObject> GetVisualChildren(DependencyObject parent) => 
            GetVisualChildrenCore(parent, false, null);

        internal static IEnumerable<DependencyObject> GetVisualChildrenCore(DependencyObject parent, bool includeStartNode, Func<DependencyObject, bool> skipChildren = null)
        {
            Func<DependencyObject, IEnumerable<DependencyObject>> getChildren = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Func<DependencyObject, IEnumerable<DependencyObject>> local1 = <>c.<>9__5_0;
                getChildren = <>c.<>9__5_0 = x => from index in Enumerable.Range(0, (x != null) ? VisualTreeHelper.GetChildrenCount(x) : 0) select VisualTreeHelper.GetChild(x, index);
            }
            return GetChildrenCore(parent, includeStartNode, getChildren, skipChildren);
        }

        public static IEnumerable<DependencyObject> GetVisualParents(DependencyObject child, DependencyObject stopNode = null) => 
            GetVisualParentsCore(child, stopNode, false);

        internal static IEnumerable<DependencyObject> GetVisualParentsCore(DependencyObject child, DependencyObject stopNode, bool includeStartNode)
        {
            Func<DependencyObject, bool> stop = <>c.<>9__4_1;
            if (<>c.<>9__4_1 == null)
            {
                Func<DependencyObject, bool> local1 = <>c.<>9__4_1;
                stop = <>c.<>9__4_1 = x => ReferenceEquals(x, null);
            }
            IEnumerable<DependencyObject> source = LinqExtensions.Unfold<DependencyObject>(child, x => !ReferenceEquals(x, stopNode) ? GetParent(x) : null, stop);
            return (includeStartNode ? source : source.Skip<DependencyObject>(1));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutTreeHelper.<>c <>9 = new LayoutTreeHelper.<>c();
            public static Func<DependencyObject, bool> <>9__4_1;
            public static Func<DependencyObject, IEnumerable<DependencyObject>> <>9__5_0;
            public static Func<DependencyObject, IEnumerable<DependencyObject>> <>9__6_0;

            internal IEnumerable<DependencyObject> <GetLogicalChildrenCore>b__6_0(DependencyObject x) => 
                LogicalTreeHelper.GetChildren(x).OfType<DependencyObject>();

            internal IEnumerable<DependencyObject> <GetVisualChildrenCore>b__5_0(DependencyObject x) => 
                from index in Enumerable.Range(0, (x != null) ? VisualTreeHelper.GetChildrenCount(x) : 0) select VisualTreeHelper.GetChild(x, index);

            internal bool <GetVisualParentsCore>b__4_1(DependencyObject x) => 
                ReferenceEquals(x, null);
        }
    }
}

