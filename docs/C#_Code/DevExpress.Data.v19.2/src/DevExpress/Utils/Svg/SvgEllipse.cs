namespace DevExpress.Utils.Svg
{
    using System;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("ellipse")]
    public class SvgEllipse : SvgElement
    {
        public static SvgEllipse Create(SvgElementProperties properties, double centerX = 0.0, double centerY = 0.0, double radiusX = 0.0, double radiusY = 0.0)
        {
            SvgEllipse ellipse1 = new SvgEllipse();
            ellipse1.CenterX = centerX;
            ellipse1.CenterY = centerY;
            ellipse1.RadiusX = radiusX;
            ellipse1.RadiusY = radiusY;
            SvgEllipse ellipse = ellipse1;
            ellipse.Assign(properties);
            return ellipse;
        }

        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgEllipse>(updateStyle);

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

        [SvgPropertyNameAlias("rx")]
        public double RadiusX
        {
            get => 
                this.GetValueCore<double>("RadiusX", false);
            private set => 
                this.SetValueCore<double>("RadiusX", value);
        }

        [SvgPropertyNameAlias("ry")]
        public double RadiusY
        {
            get => 
                this.GetValueCore<double>("RadiusY", false);
            private set => 
                this.SetValueCore<double>("RadiusY", value);
        }
    }
}

