namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("linearGradient")]
    public class SvgLinearGradient : SvgGradient
    {
        public SvgLinearGradient()
        {
            this.SetDefaultValueCore<SvgUnit>("StartX", new SvgUnit(SvgUnitType.Percentage, 0.0));
            this.SetDefaultValueCore<SvgUnit>("StartY", new SvgUnit(SvgUnitType.Percentage, 0.0));
            this.SetDefaultValueCore<SvgUnit>("EndX", new SvgUnit(SvgUnitType.Percentage, 100.0));
            this.SetDefaultValueCore<SvgUnit>("EndY", new SvgUnit(SvgUnitType.Percentage, 0.0));
        }

        public static SvgLinearGradient Create(SvgCoordinateUnits gradientUnits, SvgGradientSpreadMethod spreadMethod, SvgUnit startX = null, SvgUnit startY = null, SvgUnit endX = null, SvgUnit endY = null)
        {
            SvgLinearGradient gradient1 = new SvgLinearGradient();
            gradient1.GradientUnits = gradientUnits;
            gradient1.SpreadMethod = spreadMethod;
            SvgLinearGradient gradient = gradient1;
            if (startX != null)
            {
                gradient.StartX = startX;
            }
            if (startY != null)
            {
                gradient.StartY = startY;
            }
            if (endX != null)
            {
                gradient.EndX = endX;
            }
            if (endY != null)
            {
                gradient.EndY = endY;
            }
            return gradient;
        }

        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgLinearGradient>(updateStyle);

        public override int GetHashCode()
        {
            int[] array = new int[] { this.StartX.ToString().GetHashCode(), this.StartY.ToString().GetHashCode(), this.EndX.ToString().GetHashCode(), this.EndY.ToString().GetHashCode() };
            return (base.GetHashCode() ^ HashCodeHelper.CalcHashCode2(array));
        }

        [SvgPropertyNameAlias("x1"), TypeConverter(typeof(SvgUnitConverter))]
        public SvgUnit StartX
        {
            get => 
                this.GetValueCore<SvgUnit>("StartX", false);
            private set => 
                this.SetValueCore<SvgUnit>("StartX", value);
        }

        [SvgPropertyNameAlias("y1"), TypeConverter(typeof(SvgUnitConverter))]
        public SvgUnit StartY
        {
            get => 
                this.GetValueCore<SvgUnit>("StartY", false);
            private set => 
                this.SetValueCore<SvgUnit>("StartY", value);
        }

        [SvgPropertyNameAlias("x2"), TypeConverter(typeof(SvgUnitConverter))]
        public SvgUnit EndX
        {
            get => 
                this.GetValueCore<SvgUnit>("EndX", false);
            private set => 
                this.SetValueCore<SvgUnit>("EndX", value);
        }

        [SvgPropertyNameAlias("y2"), TypeConverter(typeof(SvgUnitConverter))]
        public SvgUnit EndY
        {
            get => 
                this.GetValueCore<SvgUnit>("EndY", false);
            private set => 
                this.SetValueCore<SvgUnit>("EndY", value);
        }
    }
}

