namespace DevExpress.Xpf.Grid
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ColumnChooserDropTarget : IDropTarget
    {
        public void Drop(UIElement source, Point pt)
        {
            this.DropColumn(source);
        }

        internal void DropColumn(UIElement source)
        {
            DropColumnCore(source);
        }

        internal static void DropColumnCore(UIElement source)
        {
            if (!(source is ColumnChooserControlBase) && !IsForbiddenUngroupGesture(source))
            {
                BaseColumn column = GetColumn(source);
                if (column != null)
                {
                    HeaderPresenterType headerPresenterTypeFromGridColumnHeader = BaseGridColumnHeader.GetHeaderPresenterTypeFromGridColumnHeader(source);
                    if ((headerPresenterTypeFromGridColumnHeader == HeaderPresenterType.GroupPanel) || column.CanStartDragSingleColumn)
                    {
                        column.View.BeforeMoveColumnToChooser(column, headerPresenterTypeFromGridColumnHeader);
                        if (!column.View.IsLastVisibleColumn(column) || (column.View.DataControl.BandsLayoutCore != null))
                        {
                            column.Visible = false;
                        }
                    }
                }
            }
        }

        private static BaseColumn GetColumn(UIElement source)
        {
            BaseColumn columnFromDragSource = DropTargetHelper.GetColumnFromDragSource((FrameworkElement) source);
            return ((DataControlBase.FindCurrentView(source) != null) ? columnFromDragSource : null);
        }

        private static HeaderPresenterType GetHeaderPresenterType(UIElement source) => 
            BaseGridColumnHeader.GetHeaderPresenterTypeFromGridColumnHeader(source);

        internal static bool IsForbiddenUngroupGesture(UIElement source)
        {
            Func<bool> fallback = <>c.<>9__1_1;
            if (<>c.<>9__1_1 == null)
            {
                Func<bool> local1 = <>c.<>9__1_1;
                fallback = <>c.<>9__1_1 = () => false;
            }
            return (GetColumn(source) as ColumnBase).Return<ColumnBase, bool>(c => (!c.CanBeGroupedByDataControlOwner() && (GetHeaderPresenterType(source) == HeaderPresenterType.GroupPanel)), fallback);
        }

        public void OnDragLeave()
        {
        }

        public void OnDragOver(UIElement source, Point pt)
        {
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ColumnChooserDropTarget.<>c <>9 = new ColumnChooserDropTarget.<>c();
            public static Func<bool> <>9__1_1;

            internal bool <IsForbiddenUngroupGesture>b__1_1() => 
                false;
        }
    }
}

