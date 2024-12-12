namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;

    public class ExtendedColumnChooserDragDropManagerColumns : ExtendedColumnChooserDragDropManagerBase
    {
        public ExtendedColumnChooserDragDropManagerColumns(DataControlBase dataControl) : base(dataControl)
        {
        }

        protected override bool CanDrop(ExtendedColumnChooserDragDropManagerBase.ParsedEventArgs parsedArgs)
        {
            if (parsedArgs.Position == DropPosition.Inside)
            {
                return false;
            }
            ColumnBase source = parsedArgs.Source as ColumnBase;
            if (source == null)
            {
                return false;
            }
            if (!source.ActualAllowMoving)
            {
                return false;
            }
            ColumnBase target = parsedArgs.Target as ColumnBase;
            if ((target == null) || ReferenceEquals(source, target))
            {
                return false;
            }
            int targetVisibleIndex = this.GetTargetVisibleIndex(parsedArgs.Target, parsedArgs.Position);
            return ((targetVisibleIndex != source.VisibleIndex) && (this.GetCorrectedVisibleIndex(parsedArgs, targetVisibleIndex) != source.VisibleIndex));
        }

        private int GetCorrectedVisibleIndex(ExtendedColumnChooserDragDropManagerBase.ParsedEventArgs parsedArgs, int targetVisibleIndex)
        {
            if ((base.DataControl == null) || (base.DataControl.DataView == null))
            {
                return targetVisibleIndex;
            }
            Tuple<ColumnBase, BandedViewDropPlace> tuple = base.DataControl.DataView.ViewBehavior.GetColumnDropTarget((ColumnBase) parsedArgs.Source, targetVisibleIndex, HeaderPresenterType.Headers);
            return ((tuple.Item1 != null) ? ((((BandedViewDropPlace) tuple.Item2) != BandedViewDropPlace.Left) ? tuple.Item1.VisibleIndex : (tuple.Item1.VisibleIndex - 1)) : targetVisibleIndex);
        }

        private int GetTargetVisibleIndex(BaseColumn target, DropPosition position)
        {
            int visibleIndex = target.VisibleIndex;
            if (position == DropPosition.After)
            {
                visibleIndex++;
            }
            return visibleIndex;
        }

        protected override void ProcessDropCore(ExtendedColumnChooserDragDropManagerBase.ParsedEventArgs parsedArgs)
        {
            base.DataControl.viewCore.MoveColumnTo((ColumnBase) parsedArgs.Source, this.GetTargetVisibleIndex(parsedArgs.Target, parsedArgs.Position), parsedArgs.HeaderPresenterType, HeaderPresenterType.Headers, MergeGroupPosition.None);
        }
    }
}

