namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class SvgRootWrapper : SvgElementWrapper
    {
        public SvgRootWrapper(SvgElement element) : base(element)
        {
        }

        protected virtual void ApplyViewBoxTransform(ISvgGraphics g, float imageScale)
        {
            SvgUnit unit = ((this.Root.Width == null) || (this.Root.Width.UnitType == SvgUnitType.Percentage)) ? null : this.Root.Width;
            SvgUnit unit2 = ((this.Root.Height == null) || (this.Root.Height.UnitType == SvgUnitType.Percentage)) ? null : this.Root.Height;
            if ((this.Root.ViewBox != null) && ((unit == null) || (unit2 == null)))
            {
                g.TranslateTransform(-((float) this.Root.ViewBox.MinX) * imageScale, -((float) this.Root.ViewBox.MinY) * imageScale, MatrixOrder.Prepend);
            }
            if ((this.Root.ViewBox != null) && ((unit != null) && (unit2 != null)))
            {
                float sx = (float) Math.Min((double) (unit.Value / this.Root.ViewBox.Width), (double) (unit2.Value / this.Root.ViewBox.Height));
                float num2 = (-((float) this.Root.ViewBox.MinX) * sx) + ((((float) unit.Value) / 2f) - (((float) (this.Root.ViewBox.Width / 2.0)) * sx));
                float num3 = (-((float) this.Root.ViewBox.MinY) * sx) + ((((float) unit2.Value) / 2f) - (((float) (this.Root.ViewBox.Height / 2.0)) * sx));
                Func<SvgRoot, SvgUnit> get = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<SvgRoot, SvgUnit> local1 = <>c.<>9__4_0;
                    get = <>c.<>9__4_0 = x => x.X;
                }
                Func<SvgUnit, float> func2 = <>c.<>9__4_1;
                if (<>c.<>9__4_1 == null)
                {
                    Func<SvgUnit, float> local2 = <>c.<>9__4_1;
                    func2 = <>c.<>9__4_1 = x => (float) x.Value;
                }
                float dx = this.Root.Get<SvgRoot, SvgUnit>(get, null).Get<SvgUnit, float>(func2, 0f);
                Func<SvgRoot, SvgUnit> func3 = <>c.<>9__4_2;
                if (<>c.<>9__4_2 == null)
                {
                    Func<SvgRoot, SvgUnit> local3 = <>c.<>9__4_2;
                    func3 = <>c.<>9__4_2 = x => x.Y;
                }
                Func<SvgUnit, float> func4 = <>c.<>9__4_3;
                if (<>c.<>9__4_3 == null)
                {
                    Func<SvgUnit, float> local4 = <>c.<>9__4_3;
                    func4 = <>c.<>9__4_3 = y => (float) y.Value;
                }
                g.TranslateTransform(dx, this.Root.Get<SvgRoot, SvgUnit>(func3, null).Get<SvgUnit, float>(func4, 0f), MatrixOrder.Prepend);
                g.TranslateTransform(num2 * imageScale, num3 * imageScale, MatrixOrder.Prepend);
                g.ScaleTransform(sx, sx, MatrixOrder.Prepend);
            }
        }

        public override void Render(ISvgGraphics g, ISvgPaletteProvider paletteProvider, double scale)
        {
            if (base.IsDisplay)
            {
                object graphicsState = g.Save();
                Func<SvgElementWrapper, bool> predicate = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Func<SvgElementWrapper, bool> local1 = <>c.<>9__3_0;
                    predicate = <>c.<>9__3_0 = x => x is SvgTransformGroupWrapper;
                }
                (base.Childs.FirstOrDefault<SvgElementWrapper>(predicate) as SvgTransformGroupWrapper).Do<SvgTransformGroupWrapper>(x => g.Transform = x.GetTransform(g.Transform, scale, false));
                this.ApplyViewBoxTransform(g, (float) scale);
                this.SetClip(g, scale);
                this.RenderChild(g, paletteProvider, scale, this);
                g.Restore(graphicsState);
            }
        }

        private SvgRoot Root =>
            base.Element as SvgRoot;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgRootWrapper.<>c <>9 = new SvgRootWrapper.<>c();
            public static Func<SvgElementWrapper, bool> <>9__3_0;
            public static Func<SvgRoot, SvgUnit> <>9__4_0;
            public static Func<SvgUnit, float> <>9__4_1;
            public static Func<SvgRoot, SvgUnit> <>9__4_2;
            public static Func<SvgUnit, float> <>9__4_3;

            internal SvgUnit <ApplyViewBoxTransform>b__4_0(SvgRoot x) => 
                x.X;

            internal float <ApplyViewBoxTransform>b__4_1(SvgUnit x) => 
                (float) x.Value;

            internal SvgUnit <ApplyViewBoxTransform>b__4_2(SvgRoot x) => 
                x.Y;

            internal float <ApplyViewBoxTransform>b__4_3(SvgUnit y) => 
                (float) y.Value;

            internal bool <Render>b__3_0(SvgElementWrapper x) => 
                x is SvgTransformGroupWrapper;
        }
    }
}

