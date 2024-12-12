namespace DevExpress.Utils.Svg
{
    using System;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("line")]
    public class SvgLine : SvgElement
    {
        public static SvgLine Create(SvgElementProperties properties, double startX = 0.0, double startY = 0.0, double endX = 0.0, double endY = 0.0)
        {
            SvgLine line1 = new SvgLine();
            line1.StartX = startX;
            line1.StartY = startY;
            line1.EndX = endX;
            line1.EndY = endY;
            SvgLine line = line1;
            line.Assign(properties);
            return line;
        }

        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgLine>(updateStyle);

        [SvgPropertyNameAlias("x1")]
        public double StartX
        {
            get => 
                this.GetValueCore<double>("StartX", false);
            private set => 
                this.SetValueCore<double>("StartX", value);
        }

        [SvgPropertyNameAlias("y1")]
        public double StartY
        {
            get => 
                this.GetValueCore<double>("StartY", false);
            private set => 
                this.SetValueCore<double>("StartY", value);
        }

        [SvgPropertyNameAlias("x2")]
        public double EndX
        {
            get => 
                this.GetValueCore<double>("EndX", false);
            private set => 
                this.SetValueCore<double>("EndX", value);
        }

        [SvgPropertyNameAlias("y2")]
        public double EndY
        {
            get => 
                this.GetValueCore<double>("EndY", false);
            private set => 
                this.SetValueCore<double>("EndY", value);
        }
    }
}

