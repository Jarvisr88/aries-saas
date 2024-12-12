namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("stop")]
    public class SvgGradientStop : SvgElement
    {
        public static SvgGradientStop Create(SvgUnit offset, string stopColor, double? stopOpacity)
        {
            SvgGradientStop stop1 = new SvgGradientStop();
            stop1.Offset = offset;
            stop1.StopColor = stopColor;
            stop1.StopOpacity = stopOpacity;
            return stop1;
        }

        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgGradientStop>(updateStyle);

        [SvgPropertyNameAlias("offset"), TypeConverter(typeof(SvgUnitOffsetConverter)), DefaultValue((string) null)]
        public SvgUnit Offset
        {
            get => 
                this.GetValueCore<SvgUnit>("Offset", true);
            internal set => 
                this.SetValueCore<SvgUnit>("Offset", value);
        }

        [SvgPropertyNameAlias("stop-color"), DefaultValue("")]
        public string StopColor
        {
            get => 
                this.GetValueCore<string>("StopColor", true);
            internal set => 
                this.SetValueCore<string>("StopColor", value);
        }

        [SvgPropertyNameAlias("stop-opacity"), DefaultValue((string) null)]
        public double? StopOpacity
        {
            get => 
                this.GetValueCore<double?>("Opacity", true);
            internal set => 
                this.SetValueCore<double?>("Opacity", value);
        }
    }
}

