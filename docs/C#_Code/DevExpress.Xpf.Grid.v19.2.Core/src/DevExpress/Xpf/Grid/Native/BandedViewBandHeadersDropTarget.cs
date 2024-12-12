namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public class BandedViewBandHeadersDropTarget : BandedViewColumnHeadersDropTarget
    {
        public BandedViewBandHeadersDropTarget(Panel panel, FixedStyle fixedStyle) : base(panel, fixedStyle)
        {
        }

        private bool AreSameBandsOwners(IBandsOwner source, IBandsOwner target)
        {
            IBandsOwner objA = source;
            if (source.DataControl.IsOriginationDataControl())
            {
                objA = source.FindClone(base.Grid);
            }
            return ReferenceEquals(objA, target);
        }

        protected override bool DenyDropToAnotherParent(BaseColumn column, BaseColumn target, HeaderPresenterType moveFrom) => 
            !this.IsSameRow(column, target, moveFrom) && !DesignerHelper.GetValue(column, column.AllowChangeParent, true);

        protected override bool DropToTopBottomEdges(BaseColumn sourceColumn, BaseGridHeader target, Point point, HeaderPresenterType moveFrom, out BandedViewDropPlace dropPlace)
        {
            if (!ReferenceEquals(sourceColumn.ParentBandInternal, sourceColumn))
            {
                if ((((BandBase) target.BaseColumn).VisibleBands.Count == 0) && base.Grid.BandsLayoutCore.AllowBandMultiRow)
                {
                    dropPlace = (((BandBase) target.BaseColumn).ActualRows.Count == 0) ? BandedViewDropPlace.Left : BandedViewDropPlace.Bottom;
                }
                else
                {
                    dropPlace = BandedViewDropPlace.None;
                }
                return true;
            }
            dropPlace = BandedViewDropPlace.None;
            if (point.Y <= (target.ActualHeight - 4.0))
            {
                return false;
            }
            BandsLayoutBase bandsLayoutCore = this.GridView.DataControl.BandsLayoutCore;
            if ((DesignerHelper.GetValue(bandsLayoutCore, bandsLayoutCore.AllowChangeBandParent, true) && !ReferenceEquals(sourceColumn, target.BaseColumn)) && base.Grid.BandsLayoutCore.AllowBandMultiRow)
            {
                dropPlace = BandedViewDropPlace.Bottom;
            }
            return true;
        }

        protected override Point GetAdornerLocationOffset(BaseColumn sourceColumn, BaseGridHeader header, BandedViewDropPlace dropPlace) => 
            ((dropPlace != BandedViewDropPlace.Left) || sourceColumn.IsBand) ? base.GetAdornerLocationOffset(sourceColumn, header, dropPlace) : new Point(0.0, header.ActualHeight);

        protected override bool IsSameRow(BaseColumn column, BaseColumn target, HeaderPresenterType moveFrom) => 
            !(column is BandBase) ? ((!base.Grid.DataView.IsColumnVisibleInHeaders(column) || (!column.Visible || (moveFrom == HeaderPresenterType.GroupPanel))) ? base.AreSameColumns(column.ParentBandInternal, target.ParentBandInternal) : false) : this.AreSameBandsOwners(((BandBase) column).Owner, ((BandBase) target).Owner);

        protected override void MoveBandColumn(BaseColumn sourceColumn, BaseGridHeader targetHeader, Point point, HeaderPresenterType moveFrom, bool useLegacyColumnVisibleIndexes)
        {
            BandedViewDropPlace dropPlace = this.GetDropPlace(sourceColumn, targetHeader, base.GetLocation(targetHeader, point), moveFrom);
            BandBase source = sourceColumn as BandBase;
            if (source != null)
            {
                this.GridView.DataControl.BandsLayoutCore.MoveBandTo(source, (BandBase) targetHeader.BaseColumn, dropPlace, moveFrom, useLegacyColumnVisibleIndexes);
            }
            else
            {
                this.GridView.DataControl.BandsLayoutCore.MoveColumnTo(sourceColumn, (BandBase) targetHeader.BaseColumn, dropPlace, moveFrom, useLegacyColumnVisibleIndexes);
            }
        }

        protected override bool ShouldCalcOffsetForOtherColumns =>
            false;
    }
}

