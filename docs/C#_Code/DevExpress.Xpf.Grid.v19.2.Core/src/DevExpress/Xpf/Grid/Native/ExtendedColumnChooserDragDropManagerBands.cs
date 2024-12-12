namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ExtendedColumnChooserDragDropManagerBands : ExtendedColumnChooserDragDropManagerBase
    {
        public event EventHandler<ExtendedColumnChooserDropInBandEventArgs> DropInEmptyBand;

        public ExtendedColumnChooserDragDropManagerBands(DataControlBase dataControl) : base(dataControl)
        {
        }

        protected override bool CanDrop(ExtendedColumnChooserDragDropManagerBase.ParsedEventArgs parsedArgs)
        {
            ColumnBase source = parsedArgs.Source as ColumnBase;
            if (source != null)
            {
                return this.CanDropColumn(source, parsedArgs.Target, parsedArgs.Position);
            }
            BandBase sourceBand = parsedArgs.Source as BandBase;
            return ((sourceBand != null) && this.CanDropBand(sourceBand, parsedArgs.Target, parsedArgs.Position));
        }

        private bool CanDropBand(BandBase sourceBand, BaseColumn target, DropPosition position)
        {
            if (!sourceBand.ActualAllowMoving)
            {
                return false;
            }
            BandBase targetBand = target as BandBase;
            if (targetBand == null)
            {
                return false;
            }
            BandedViewDropPlace place = this.UpdateTargetBand(sourceBand, ref targetBand, position);
            if (this.IsDropParentInChild(sourceBand, targetBand))
            {
                return false;
            }
            if (!base.AllowChangeBandParent && (!ReferenceEquals(sourceBand.ParentBand, targetBand.ParentBand) || (place == BandedViewDropPlace.Bottom)))
            {
                return false;
            }
            if (position != DropPosition.Inside)
            {
                if (!ReferenceEquals(sourceBand.ParentBand, targetBand.ParentBand))
                {
                    return (sourceBand.Fixed == targetBand.Fixed);
                }
                switch (position)
                {
                    case DropPosition.Before:
                        return ((sourceBand.Fixed == targetBand.Fixed) ? ((sourceBand.VisibleIndex >= targetBand.VisibleIndex) || ((targetBand.VisibleIndex - sourceBand.VisibleIndex) > 1)) : false);

                    case DropPosition.After:
                        return ((sourceBand.Fixed == targetBand.Fixed) ? ((sourceBand.VisibleIndex <= targetBand.VisibleIndex) || ((sourceBand.VisibleIndex - targetBand.VisibleIndex) > 1)) : false);
                }
            }
            return true;
        }

        private bool CanDropColumn(ColumnBase sourceColumn, BaseColumn target, DropPosition position)
        {
            if (!sourceColumn.ActualAllowMoving)
            {
                return false;
            }
            ColumnBase targetColumn = target as ColumnBase;
            if (targetColumn != null)
            {
                return this.CanDropColumnOnColumn(sourceColumn, targetColumn, position);
            }
            BandBase targetBand = target as BandBase;
            return ((targetBand != null) && this.CanDropColumnOnBand(sourceColumn, targetBand, position));
        }

        private bool CanDropColumnOnBand(ColumnBase sourceColumn, BandBase targetBand, DropPosition position) => 
            (position == DropPosition.Inside) ? ((base.AllowChangeColumnParent || ReferenceEquals(sourceColumn.ParentBand, targetBand)) ? (targetBand.BandsCore.Count == 0) : false) : false;

        private bool CanDropColumnOnColumn(ColumnBase sourceColumn, ColumnBase targetColumn, DropPosition position) => 
            (position != DropPosition.Inside) ? (base.AllowChangeColumnParent || ReferenceEquals(targetColumn.ParentBand, sourceColumn.ParentBand)) : false;

        private BandedViewDropPlace GetBandDropPlace(DropPosition position)
        {
            switch (position)
            {
                case DropPosition.Before:
                    return BandedViewDropPlace.Left;

                case DropPosition.After:
                    return BandedViewDropPlace.Right;
            }
            return BandedViewDropPlace.Bottom;
        }

        private bool IsDropParentInChild(BandBase sourceBand, BandBase targetBand) => 
            (targetBand != null) ? (!ReferenceEquals(targetBand.ParentBand, sourceBand) ? this.IsDropParentInChild(sourceBand, targetBand.ParentBand) : true) : false;

        private void ProcessDropBand(BandBase sourceBand, BandBase targetBand, DropPosition position)
        {
            BandedViewDropPlace dropPlace = this.UpdateTargetBand(sourceBand, ref targetBand, position);
            base.DataControl.BandsLayoutCore.MoveBandTo(sourceBand, targetBand, dropPlace, HeaderPresenterType.Headers, base.UseLegacyColumnVisibleIndexes);
            this.RaiseDropInBand(targetBand, sourceBand);
        }

        private void ProcessDropColumn(ColumnBase sourceColumn, ExtendedColumnChooserDragDropManagerBase.ParsedEventArgs parsedArgs)
        {
            BandBase target = parsedArgs.Target as BandBase;
            if (target != null)
            {
                this.ProcessDropColumnOnBand(sourceColumn, target, parsedArgs.Position, parsedArgs.HeaderPresenterType);
            }
            else
            {
                this.ProcessDropColumnOnColumn(sourceColumn, parsedArgs.Target as ColumnBase, parsedArgs.Position, parsedArgs.HeaderPresenterType);
            }
        }

        private void ProcessDropColumnOnBand(ColumnBase sourceColumn, BandBase target, DropPosition position, HeaderPresenterType headerPresenterType)
        {
            if ((target.ColumnsCore.Count != 0) && ((target.ActualRows.Count != 0) && (target.ActualRows[target.ActualRows.Count - 1].Columns.Count != 0)))
            {
                BaseColumn column = target.ActualRows[target.ActualRows.Count - 1].Columns.Last<ColumnBase>();
                base.DataControl.BandsLayoutCore.MoveColumnTo(sourceColumn, column, BandedViewDropPlace.Right, headerPresenterType, base.UseLegacyColumnVisibleIndexes);
            }
            else
            {
                base.DataControl.BandsLayoutCore.MoveColumnTo(sourceColumn, target, BandedViewDropPlace.Bottom, headerPresenterType, base.UseLegacyColumnVisibleIndexes);
                this.RaiseDropInBand(target, sourceColumn);
            }
        }

        private void ProcessDropColumnOnColumn(ColumnBase sourceColumn, ColumnBase targetColumn, DropPosition position, HeaderPresenterType headerPresenterType)
        {
            BandedViewDropPlace dropPlace = (position == DropPosition.Before) ? BandedViewDropPlace.Left : BandedViewDropPlace.Right;
            base.DataControl.BandsLayoutCore.MoveColumnTo(sourceColumn, targetColumn, dropPlace, headerPresenterType, base.UseLegacyColumnVisibleIndexes);
        }

        protected override void ProcessDropCore(ExtendedColumnChooserDragDropManagerBase.ParsedEventArgs parsedArgs)
        {
            ColumnBase source = parsedArgs.Source as ColumnBase;
            if (source != null)
            {
                this.ProcessDropColumn(source, parsedArgs);
            }
            BandBase sourceBand = parsedArgs.Source as BandBase;
            BandBase target = parsedArgs.Target as BandBase;
            if ((sourceBand != null) && (target != null))
            {
                this.ProcessDropBand(sourceBand, target, parsedArgs.Position);
            }
        }

        private void RaiseDropInBand(BandBase targetBand, BaseColumn source)
        {
            if (this.DropInEmptyBand != null)
            {
                this.DropInEmptyBand(this, new ExtendedColumnChooserDropInBandEventArgs(targetBand, source));
            }
        }

        private BandedViewDropPlace UpdateTargetBand(BandBase sourceBand, ref BandBase targetBand, DropPosition position)
        {
            BandedViewDropPlace bandDropPlace = this.GetBandDropPlace(position);
            if (bandDropPlace == BandedViewDropPlace.Bottom)
            {
                if (targetBand.VisibleBands.Count == 0)
                {
                    return bandDropPlace;
                }
                for (int i = targetBand.VisibleBands.Count - 1; i >= 0; i--)
                {
                    BandBase objA = ((IList) targetBand.VisibleBands)[i] as BandBase;
                    if (!ReferenceEquals(objA, sourceBand))
                    {
                        targetBand = objA;
                        return BandedViewDropPlace.Right;
                    }
                }
            }
            return bandDropPlace;
        }
    }
}

