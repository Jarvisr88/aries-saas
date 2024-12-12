namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class TextPart
    {
        private IList<GraphicsPath> attachedPathCore;
        private GraphicsPath pathCore;
        private GraphicsPath resultPathCore;

        public TextPart()
        {
            this.attachedPathCore = new List<GraphicsPath>();
        }

        public TextPart(SvgTextWrapper element)
        {
            this.attachedPathCore = new List<GraphicsPath>();
            this.Element = element;
            this.Offset = PointF.Empty;
        }

        public TextPart(TextPart parent, SvgTextWrapper element)
        {
            this.attachedPathCore = new List<GraphicsPath>();
            this.Element = element;
            this.Offset = parent.Offset;
            this.Parent = parent;
        }

        public void AddStringToPath(string value, ISvgGraphics g)
        {
            Func<SvgTextWrapper, IEnumerable<SvgUnit>> listGetter = <>c.<>9__35_0;
            if (<>c.<>9__35_0 == null)
            {
                Func<SvgTextWrapper, IEnumerable<SvgUnit>> local1 = <>c.<>9__35_0;
                listGetter = <>c.<>9__35_0 = e => e.Text.X;
            }
            IList<double> valueList = this.GetValueList(value.Length, listGetter);
            Func<SvgTextWrapper, IEnumerable<SvgUnit>> func3 = <>c.<>9__35_1;
            if (<>c.<>9__35_1 == null)
            {
                Func<SvgTextWrapper, IEnumerable<SvgUnit>> local2 = <>c.<>9__35_1;
                func3 = <>c.<>9__35_1 = e => e.Text.Y;
            }
            IList<double> source = this.GetValueList(value.Length, func3);
            using (GdiFontWrapper wrapper = this.Element.GetFont())
            {
                float fontBaselineHeight = wrapper.CalcFontHeight();
                Func<SvgTextWrapper, IEnumerable<SvgUnit>> func1 = <>c.<>9__35_2;
                if (<>c.<>9__35_2 == null)
                {
                    Func<SvgTextWrapper, IEnumerable<SvgUnit>> local3 = <>c.<>9__35_2;
                    func1 = <>c.<>9__35_2 = e => e.Text.Dy;
                }
                IList<double> list3 = this.GetValueList(value.Length, func1);
                double y = this.Offset.Y;
                double x = this.Offset.X;
                if (valueList.Any<double>())
                {
                    this.FlushPath();
                    x = valueList.Last<double>();
                    this.XAnchor = x;
                }
                this.EnsurePath();
                y = (source.Any<double>() ? source.Last<double>() : y) + (list3.Any<double>() ? list3.Last<double>() : 0.0);
                this.AddStringToPath(value, wrapper, new PointF((float) x, (float) y), fontBaselineHeight);
                SizeF ef = wrapper.MeasureString(g, value);
                x += ef.Width;
                this.CharCount += value.Length;
                this.Offset = new PointF((float) x, (float) y);
            }
        }

        private void AddStringToPath(string value, GdiFontWrapper font, PointF location, float fontBaselineHeight)
        {
            GraphicsPath pathCore = this.pathCore;
            font.AddStringToPath(pathCore, value, new PointF(location.X, location.Y - fontBaselineHeight));
        }

        private void EnsurePath()
        {
            if (this.pathCore == null)
            {
                this.pathCore = new GraphicsPath();
                this.pathCore.StartFigure();
                TextPart parent = this;
                while (true)
                {
                    if ((parent == null) || (parent.XAnchor > -3.4028234663852886E+38))
                    {
                        parent.attachedPathCore.Add(this.pathCore);
                        break;
                    }
                    parent = parent.Parent;
                }
            }
        }

        private void FlushPath()
        {
            if (this.pathCore != null)
            {
                this.pathCore.CloseFigure();
                if (this.pathCore.PointCount < 1)
                {
                    this.attachedPathCore.Clear();
                    this.XAnchor = -3.4028234663852886E+38;
                    this.pathCore = null;
                }
                else
                {
                    if (this.XAnchor > -3.4028234663852886E+38)
                    {
                        float maxValue = float.MaxValue;
                        float minValue = float.MinValue;
                        foreach (GraphicsPath path in this.attachedPathCore)
                        {
                            RectangleF bounds = path.GetBounds();
                            if (bounds.Left < maxValue)
                            {
                                maxValue = bounds.Left;
                            }
                            if (bounds.Right > minValue)
                            {
                                minValue = bounds.Right;
                            }
                        }
                        float offsetX = 0f;
                        SvgTextAnchor textAnchor = this.Element.Text.TextAnchor;
                        if (textAnchor == SvgTextAnchor.Middle)
                        {
                            offsetX -= (minValue - maxValue) / 2f;
                        }
                        else if (textAnchor == SvgTextAnchor.End)
                        {
                            offsetX -= minValue - maxValue;
                        }
                        if (offsetX != 0f)
                        {
                            using (Matrix matrix = new Matrix())
                            {
                                matrix.Translate(offsetX, 0f);
                                foreach (GraphicsPath path2 in this.attachedPathCore)
                                {
                                    path2.Transform(matrix);
                                }
                            }
                        }
                        this.attachedPathCore.Clear();
                        this.XAnchor = -3.4028234663852886E+38;
                    }
                    if (this.resultPathCore == null)
                    {
                        this.resultPathCore = this.pathCore;
                    }
                    else
                    {
                        this.resultPathCore.AddPath(this.pathCore, false);
                    }
                    this.pathCore = null;
                }
            }
        }

        public GraphicsPath GetPath()
        {
            this.FlushPath();
            return this.resultPathCore;
        }

        private IList<double> GetValueList(int maxCount, Func<SvgTextWrapper, IEnumerable<SvgUnit>> listGetter)
        {
            List<double> list = new List<double>();
            TextPart parent = this;
            int count = 0;
            int num2 = 0;
            while (parent != null)
            {
                count += parent.CharCount;
                Func<SvgUnit, double> selector = <>c.<>9__39_0;
                if (<>c.<>9__39_0 == null)
                {
                    Func<SvgUnit, double> local1 = <>c.<>9__39_0;
                    selector = <>c.<>9__39_0 = p => p.Value;
                }
                list.AddRange(listGetter(parent.Element).Skip<SvgUnit>(count).Take<SvgUnit>(maxCount).Select<SvgUnit, double>(selector));
                if (list.Count > num2)
                {
                    maxCount -= list.Count - num2;
                    count += list.Count - num2;
                    num2 = list.Count;
                }
                if (maxCount < 1)
                {
                    return list;
                }
                parent = parent.Parent;
            }
            return list;
        }

        public PointF Offset { get; set; }

        public SvgTextWrapper Element { get; set; }

        public float LetterSpacingAdjust { get; set; }

        public int CharCount { get; set; }

        public TextPart Parent { get; set; }

        public float StartOffsetAdjust { get; set; }

        public double XAnchor { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TextPart.<>c <>9 = new TextPart.<>c();
            public static Func<SvgTextWrapper, IEnumerable<SvgUnit>> <>9__35_0;
            public static Func<SvgTextWrapper, IEnumerable<SvgUnit>> <>9__35_1;
            public static Func<SvgTextWrapper, IEnumerable<SvgUnit>> <>9__35_2;
            public static Func<SvgUnit, double> <>9__39_0;

            internal IEnumerable<SvgUnit> <AddStringToPath>b__35_0(SvgTextWrapper e) => 
                e.Text.X;

            internal IEnumerable<SvgUnit> <AddStringToPath>b__35_1(SvgTextWrapper e) => 
                e.Text.Y;

            internal IEnumerable<SvgUnit> <AddStringToPath>b__35_2(SvgTextWrapper e) => 
                e.Text.Dy;

            internal double <GetValueList>b__39_0(SvgUnit p) => 
                p.Value;
        }
    }
}

