namespace DevExpress.Utils.Svg
{
    using System;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("rect")]
    public class SvgRectangle : SvgElement
    {
        public static SvgRectangle Create(SvgElementProperties properties, double x = 0.0, double y = 0.0, double width = 0.0, double height = 0.0)
        {
            SvgRectangle rectangle1 = new SvgRectangle();
            rectangle1.X = x;
            rectangle1.Y = y;
            rectangle1.Width = width;
            rectangle1.Height = height;
            SvgRectangle rectangle = rectangle1;
            rectangle.Assign(properties);
            return rectangle;
        }

        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgRectangle>(updateStyle);

        [SvgPropertyNameAlias("x")]
        public double X
        {
            get => 
                this.GetValueCore<double>("X", false);
            private set => 
                this.SetValueCore<double>("X", value);
        }

        [SvgPropertyNameAlias("y")]
        public double Y
        {
            get => 
                this.GetValueCore<double>("Y", false);
            private set => 
                this.SetValueCore<double>("Y", value);
        }

        [SvgPropertyNameAlias("width")]
        public double Width
        {
            get => 
                this.GetValueCore<double>("Width", false);
            private set => 
                this.SetValueCore<double>("Width", value);
        }

        [SvgPropertyNameAlias("height")]
        public double Height
        {
            get => 
                this.GetValueCore<double>("Height", false);
            private set => 
                this.SetValueCore<double>("Height", value);
        }

        [SvgPropertyNameAlias("rx")]
        public double CornerRadiusX
        {
            get
            {
                double valueCore = this.GetValueCore<double>("CornerRadiusX", false);
                return ((valueCore != 0.0) ? valueCore : this.GetValueCore<double>("CornerRadiusY", false));
            }
            private set => 
                this.SetValueCore<double>("CornerRadiusX", value);
        }

        [SvgPropertyNameAlias("ry")]
        public double CornerRadiusY
        {
            get
            {
                double valueCore = this.GetValueCore<double>("CornerRadiusY", false);
                return ((valueCore != 0.0) ? valueCore : this.GetValueCore<double>("CornerRadiusX", false));
            }
            private set => 
                this.SetValueCore<double>("CornerRadiusY", value);
        }
    }
}

