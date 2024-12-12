namespace DevExpress.Xpf.Printing.PreviewControl
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class PageViewModel : PageViewModelBase
    {
        protected PageViewModel(Page page) : base(page)
        {
            this.SetPageMarginCommand = DelegateCommandFactory.Create<PageMarginSettings>(new Action<PageMarginSettings>(this.SetPageMargin));
        }

        public static PageViewModel Create(Page page)
        {
            <>c__DisplayClass0_0 class_;
            System.Linq.Expressions.Expression[] expressionArray1 = new System.Linq.Expressions.Expression[] { System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(class_, typeof(<>c__DisplayClass0_0)), fieldof(<>c__DisplayClass0_0.page)) };
            return ViewModelSource.Create<PageViewModel>(System.Linq.Expressions.Expression.Lambda<Func<PageViewModel>>(System.Linq.Expressions.Expression.New((ConstructorInfo) methodof(PageViewModel..ctor), (IEnumerable<System.Linq.Expressions.Expression>) expressionArray1), new ParameterExpression[0]));
        }

        internal Brick GetBrick(System.Windows.Point relativePoint, double zoom)
        {
            RectangleF empty = RectangleF.Empty;
            PointF pt = PSUnitConverter.PixelToDoc((PointF) relativePoint.ToWinFormsPoint(), (float) zoom);
            BrickNavigator navigator1 = new BrickNavigator(this.Page, true, true);
            navigator1.BrickPosition = PointF.Empty;
            Tuple<Brick, RectangleF> tuple = navigator1.FindBrick(pt);
            if (tuple == null)
            {
                return null;
            }
            empty = tuple.Item2;
            return tuple.Item1;
        }

        protected override double GetScaleMultiplier() => 
            1.0;

        private void SetPageMargin(PageMarginSettings marginSettings)
        {
            IPageSettingsService service = this.Page.Document.PrintingSystem.GetService<IPageSettingsService>();
            if (service != null)
            {
                MarginSide side = marginSettings.Side;
                switch (side)
                {
                    case MarginSide.Left:
                        service.SetLeftMargin(this.Page, (float) marginSettings.Size);
                        return;

                    case MarginSide.Top:
                        service.SetTopMargin(this.Page, (float) marginSettings.Size);
                        return;

                    case (MarginSide.Top | MarginSide.Left):
                        break;

                    case MarginSide.Right:
                        service.SetRightMargin(this.Page, (float) marginSettings.Size);
                        return;

                    default:
                        if (side != MarginSide.Bottom)
                        {
                            break;
                        }
                        service.SetBottomMargin(this.Page, (float) marginSettings.Size);
                        return;
                }
                throw new InvalidOperationException();
            }
        }

        internal override void SyncPageInfo()
        {
            base.SyncPageInfo();
            this.PageMargins = new Thickness((double) this.Page.MarginsF.Left, (double) this.Page.MarginsF.Top, (double) this.Page.MarginsF.Right, (double) this.Page.MarginsF.Bottom);
        }

        [DXHelpExclude(true), EditorBrowsable(EditorBrowsableState.Never)]
        public ICommand SetPageMarginCommand { get; private set; }

        public virtual Thickness PageMargins { get; protected set; }
    }
}

