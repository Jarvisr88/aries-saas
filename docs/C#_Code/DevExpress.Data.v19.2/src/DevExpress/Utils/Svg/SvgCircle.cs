namespace DevExpress.Utils.Svg
{
    using System;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("circle")]
    public class SvgCircle : SvgElement
    {
        public static SvgCircle Create(SvgElementProperties properties, double centerX = 0.0, double centerY = 0.0, double radius = 0.0)
        {
            SvgCircle circle1 = new SvgCircle();
            circle1.CenterX = centerX;
            circle1.CenterY = centerY;
            circle1.Radius = radius;
            SvgCircle circle = circle1;
            circle.Assign(properties);
            return circle;
        }

        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgCircle>(updateStyle);

        [SvgPropertyNameAlias("cx")]
        public double CenterX
        {
            get => 
                this.GetValueCore<double>("CenterX", false);
            private set => 
                this.SetValueCore<double>("CenterX", value);
        }

        [SvgPropertyNameAlias("cy")]
        public double CenterY
        {
            get => 
                this.GetValueCore<double>("CenterY", false);
            private set => 
                this.SetValueCore<double>("CenterY", value);
        }

        [SvgPropertyNameAlias("r")]
        public double Radius
        {
            get => 
                this.GetValueCore<double>("Radius", false);
            private set => 
                this.SetValueCore<double>("Radius", value);
        }
    }
}

