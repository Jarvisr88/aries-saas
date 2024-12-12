namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("radialGradient")]
    public class SvgRadialGradient : SvgGradient
    {
        public SvgRadialGradient()
        {
            this.SetDefaultValueCore<SvgUnit>("CenterX", new SvgUnit(SvgUnitType.Percentage, 50.0));
            this.SetDefaultValueCore<SvgUnit>("CenterY", new SvgUnit(SvgUnitType.Percentage, 50.0));
            this.SetDefaultValueCore<SvgUnit>("Radius", new SvgUnit(SvgUnitType.Percentage, 50.0));
        }

        public static SvgRadialGradient Create(SvgCoordinateUnits gradientUnits, SvgGradientSpreadMethod spreadMethod, SvgUnit centerX = null, SvgUnit centerY = null, SvgUnit radius = null, SvgUnit focalX = null, SvgUnit focalY = null, SvgUnit focalRadius = null)
        {
            SvgRadialGradient gradient1 = new SvgRadialGradient();
            gradient1.GradientUnits = gradientUnits;
            gradient1.SpreadMethod = spreadMethod;
            SvgRadialGradient gradient = gradient1;
            if (centerX != null)
            {
                gradient.CenterX = centerX;
            }
            if (centerY != null)
            {
                gradient.CenterY = centerY;
            }
            if (radius != null)
            {
                gradient.Radius = radius;
            }
            if (focalX != null)
            {
                gradient.FocalX = focalX;
            }
            if (focalY != null)
            {
                gradient.FocalX = focalX;
            }
            if (focalRadius != null)
            {
                gradient.FocalRadius = focalRadius;
            }
            return gradient;
        }

        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgRadialGradient>(updateStyle);

        [SvgPropertyNameAlias("cx"), TypeConverter(typeof(SvgUnitConverter))]
        public SvgUnit CenterX
        {
            get => 
                this.GetValueCore<SvgUnit>("CenterX", false);
            private set => 
                this.SetValueCore<SvgUnit>("CenterX", value);
        }

        [SvgPropertyNameAlias("cy"), TypeConverter(typeof(SvgUnitConverter))]
        public SvgUnit CenterY
        {
            get => 
                this.GetValueCore<SvgUnit>("CenterY", false);
            private set => 
                this.SetValueCore<SvgUnit>("CenterY", value);
        }

        [SvgPropertyNameAlias("r"), TypeConverter(typeof(SvgUnitConverter))]
        public SvgUnit Radius
        {
            get => 
                this.GetValueCore<SvgUnit>("Radius", false);
            private set => 
                this.SetValueCore<SvgUnit>("Radius", value);
        }

        [SvgPropertyNameAlias("fx"), TypeConverter(typeof(SvgUnitConverter))]
        public SvgUnit FocalX
        {
            get => 
                this.GetValueCore<SvgUnit>("FocalX", false);
            private set => 
                this.SetValueCore<SvgUnit>("FocalX", value);
        }

        [SvgPropertyNameAlias("fy"), TypeConverter(typeof(SvgUnitConverter))]
        public SvgUnit FocalY
        {
            get => 
                this.GetValueCore<SvgUnit>("FocalY", false);
            private set => 
                this.SetValueCore<SvgUnit>("FocalY", value);
        }

        [SvgPropertyNameAlias("fr"), TypeConverter(typeof(SvgUnitConverter))]
        public SvgUnit FocalRadius
        {
            get => 
                this.GetValueCore<SvgUnit>("FocalRadius", false);
            private set => 
                this.SetValueCore<SvgUnit>("FocalRadius", value);
        }
    }
}

