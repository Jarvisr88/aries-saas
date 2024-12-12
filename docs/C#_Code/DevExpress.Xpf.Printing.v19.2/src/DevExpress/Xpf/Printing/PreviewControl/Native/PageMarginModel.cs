namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class PageMarginModel
    {
        protected PageMarginModel(MarginSide side)
        {
            this.Side = side;
            this.Zoom = 1.0;
        }

        public static PageMarginModel Create(MarginSide side)
        {
            <>c__DisplayClass0_0 class_;
            Expression[] expressionArray1 = new Expression[] { Expression.Field(Expression.Constant(class_, typeof(<>c__DisplayClass0_0)), fieldof(<>c__DisplayClass0_0.side)) };
            return ViewModelSource.Create<PageMarginModel>(Expression.Lambda<Func<PageMarginModel>>(Expression.New((ConstructorInfo) methodof(PageMarginModel..ctor), (IEnumerable<Expression>) expressionArray1), new ParameterExpression[0]));
        }

        protected void OnMaxSizeFChanged()
        {
            this.MaxSize = ((float) this.MaxSizeF).DocToDip() * this.Zoom;
        }

        protected void OnSizeFChanged()
        {
            double num = (((float) this.SizeF).DocToDip() * this.Zoom) - 1.0;
            this.Size = (num < 0.0) ? 0.0 : num;
        }

        protected void OnZoomChanged()
        {
            double num = (((float) this.SizeF).DocToDip() * this.Zoom) - 1.0;
            this.Size = (num < 0.0) ? 0.0 : num;
            this.MaxSize = ((float) this.MaxSizeF).DocToDip() * this.Zoom;
        }

        public void SetDefinitions(double sizeF, double maxSizeF)
        {
            this.SizeF = sizeF;
            this.MaxSizeF = maxSizeF;
        }

        public MarginSide Side { get; private set; }

        public virtual double SizeF { get; protected set; }

        public virtual double MaxSizeF { get; protected set; }

        public virtual double Size { get; protected set; }

        public virtual double MaxSize { get; protected set; }

        public virtual double Zoom { get; set; }
    }
}

