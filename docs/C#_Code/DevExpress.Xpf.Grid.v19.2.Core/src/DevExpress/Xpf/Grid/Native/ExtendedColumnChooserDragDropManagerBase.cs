namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class ExtendedColumnChooserDragDropManagerBase
    {
        protected readonly DataControlBase DataControl;

        public ExtendedColumnChooserDragDropManagerBase(DataControlBase dataControl)
        {
            this.DataControl = dataControl;
        }

        protected abstract bool CanDrop(ParsedEventArgs parsedArgs);
        private static DropPosition GetPosition(BaseColumn target, double dropPositionRelativeCoefficient)
        {
            if (target is ColumnBase)
            {
                return ((dropPositionRelativeCoefficient > 0.5) ? DropPosition.After : DropPosition.Before);
            }
            DropPosition inside = DropPosition.Inside;
            if (dropPositionRelativeCoefficient <= 0.2)
            {
                inside = DropPosition.Before;
            }
            if (dropPositionRelativeCoefficient >= 0.8)
            {
                inside = DropPosition.After;
            }
            return inside;
        }

        public DropPosition? ProcessDrag(BaseColumn source, BaseColumn target, double dropPositionRelativeCoefficient, HeaderPresenterType headerPresenterType)
        {
            ParsedEventArgs args;
            if (this.TryParseEventArgs(source, target, GetPosition(target, dropPositionRelativeCoefficient), dropPositionRelativeCoefficient, headerPresenterType, out args) && this.CanDrop(args))
            {
                return new DropPosition?(args.Position);
            }
            return null;
        }

        public void ProcessDrop(BaseColumn source, BaseColumn target, double dropPositionRelativeCoefficient, HeaderPresenterType headerPresenterType)
        {
            ParsedEventArgs args;
            if (this.TryParseEventArgs(source, target, GetPosition(target, dropPositionRelativeCoefficient), dropPositionRelativeCoefficient, headerPresenterType, out args) && this.CanDrop(args))
            {
                this.ProcessDropCore(args);
            }
        }

        protected abstract void ProcessDropCore(ParsedEventArgs parsedArgs);
        private bool TryParseEventArgs(BaseColumn source, BaseColumn target, DropPosition position, double dropPositionRelativeCoefficient, HeaderPresenterType headerPresenterType, out ParsedEventArgs parsedEventArgs)
        {
            parsedEventArgs = null;
            if ((source == null) || ReferenceEquals(source, target))
            {
                return false;
            }
            if (this.DataControl.viewCore.ViewBehavior.GetActualColumnFixed(source) != this.DataControl.viewCore.ViewBehavior.GetActualColumnFixed(target))
            {
                return false;
            }
            ParsedEventArgs args = new ParsedEventArgs {
                Source = source,
                Target = target,
                Position = position,
                HeaderPresenterType = headerPresenterType
            };
            parsedEventArgs = args;
            return true;
        }

        protected bool AllowChangeColumnParent =>
            (this.DataControl.DataView is ITableView) ? ((ITableView) this.DataControl.DataView).AllowChangeColumnParent : false;

        protected bool AllowChangeBandParent =>
            (this.DataControl.DataView is ITableView) ? ((ITableView) this.DataControl.DataView).AllowChangeBandParent : false;

        protected bool UseLegacyColumnVisibleIndexes =>
            (this.DataControl.DataView != null) ? this.DataControl.DataView.UseLegacyColumnVisibleIndexes : false;

        public class ParsedEventArgs
        {
            public BaseColumn Source { get; set; }

            public BaseColumn Target { get; set; }

            public DropPosition Position { get; set; }

            public DevExpress.Xpf.Grid.HeaderPresenterType HeaderPresenterType { get; set; }
        }
    }
}

