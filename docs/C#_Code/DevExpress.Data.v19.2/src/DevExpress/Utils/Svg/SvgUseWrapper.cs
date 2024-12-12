namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class SvgUseWrapper : SvgElementWrapper
    {
        private SvgElementWrapper referencedElementCore;

        public SvgUseWrapper(SvgElement element) : base(element)
        {
        }

        protected override GraphicsPath GetPathCore(double scale) => 
            this.ReferencedElement.Get<SvgElementWrapper, GraphicsPath>(x => x.GetPath(scale), base.GetPathCore(scale));

        protected internal override Matrix GetTransform(Matrix m, double scale, bool clone = false)
        {
            m = base.GetTransform(m, scale, clone);
            Matrix matrix = new Matrix();
            Func<SvgUnit, double> get = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<SvgUnit, double> local1 = <>c.<>9__8_0;
                get = <>c.<>9__8_0 = x => x.Value;
            }
            matrix.Translate((float) (this.SvgUse.X.Get<SvgUnit, double>(get, 0.0) * scale), (float) (this.SvgUse.Y.Get<SvgUnit, double>((<>c.<>9__8_1 ??= x => x.Value), 0.0) * scale));
            Matrix matrix2 = m.Clone();
            matrix2.Multiply(matrix);
            return matrix2;
        }

        protected override void RenderCore(ISvgGraphics g, double scale)
        {
            if (this.ReferencedElement != null)
            {
                Func<SvgElementWrapper, SvgElement> get = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<SvgElementWrapper, SvgElement> local1 = <>c.<>9__7_0;
                    get = <>c.<>9__7_0 = x => x.Element;
                }
                Func<SvgElement, SvgElement> func2 = <>c.<>9__7_1;
                if (<>c.<>9__7_1 == null)
                {
                    Func<SvgElement, SvgElement> local2 = <>c.<>9__7_1;
                    func2 = <>c.<>9__7_1 = x => x.Parent;
                }
                SvgElement parent = this.ReferencedElement.Get<SvgElementWrapper, SvgElement>(get, null).Get<SvgElement, SvgElement>(func2, null);
                this.ReferencedElement.Element.SetParent(base.Element);
                Func<SvgElementWrapper, SvgElement> func3 = <>c.<>9__7_2;
                if (<>c.<>9__7_2 == null)
                {
                    Func<SvgElementWrapper, SvgElement> local3 = <>c.<>9__7_2;
                    func3 = <>c.<>9__7_2 = x => x.Element;
                }
                this.ReferencedElement.Get<SvgElementWrapper, SvgElement>(func3, null).Do<SvgElement>(x => this.ReferencedElement.Render(g, this.PaletteProvider, scale));
                Func<SvgElementWrapper, SvgElement> func4 = <>c.<>9__7_4;
                if (<>c.<>9__7_4 == null)
                {
                    Func<SvgElementWrapper, SvgElement> local4 = <>c.<>9__7_4;
                    func4 = <>c.<>9__7_4 = x => x.Element;
                }
                this.ReferencedElement.Get<SvgElementWrapper, SvgElement>(func4, null).Do<SvgElement>(x => x.SetParent(parent));
            }
        }

        private DevExpress.Utils.Svg.SvgUse SvgUse =>
            base.Element as DevExpress.Utils.Svg.SvgUse;

        internal SvgElementWrapper ReferencedElement
        {
            get
            {
                if (this.referencedElementCore == null)
                {
                    this.referencedElementCore = base.Owner.GetElementWrapperById(this.SvgUse.ReferencedElement.ToString());
                    this.referencedElementCore.Do<SvgElementWrapper>(delegate (SvgElementWrapper x) {
                        x.Scale = base.Scale;
                    });
                }
                return this.referencedElementCore;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgUseWrapper.<>c <>9 = new SvgUseWrapper.<>c();
            public static Func<SvgElementWrapper, SvgElement> <>9__7_0;
            public static Func<SvgElement, SvgElement> <>9__7_1;
            public static Func<SvgElementWrapper, SvgElement> <>9__7_2;
            public static Func<SvgElementWrapper, SvgElement> <>9__7_4;
            public static Func<SvgUnit, double> <>9__8_0;
            public static Func<SvgUnit, double> <>9__8_1;

            internal double <GetTransform>b__8_0(SvgUnit x) => 
                x.Value;

            internal double <GetTransform>b__8_1(SvgUnit x) => 
                x.Value;

            internal SvgElement <RenderCore>b__7_0(SvgElementWrapper x) => 
                x.Element;

            internal SvgElement <RenderCore>b__7_1(SvgElement x) => 
                x.Parent;

            internal SvgElement <RenderCore>b__7_2(SvgElementWrapper x) => 
                x.Element;

            internal SvgElement <RenderCore>b__7_4(SvgElementWrapper x) => 
                x.Element;
        }
    }
}

