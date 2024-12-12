namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("use")]
    public class SvgUse : SvgElement
    {
        public static SvgUse Create(SvgElementProperties properties, Uri referencedElement, SvgUnit x = null, SvgUnit y = null, SvgUnit width = null, SvgUnit height = null)
        {
            SvgUse use1 = new SvgUse();
            use1.X = x;
            use1.Y = y;
            use1.Width = width;
            use1.Height = height;
            use1.ReferencedElement = referencedElement;
            SvgUse use = use1;
            use.Assign(properties);
            return use;
        }

        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgUse>(updateStyle);

        [SvgPropertyNameAlias("x"), TypeConverter(typeof(SvgUnitConverter)), DefaultValue((string) null)]
        public SvgUnit X
        {
            get => 
                this.GetValueCore<SvgUnit>("X", false);
            private set => 
                this.SetValueCore<SvgUnit>("X", value);
        }

        [SvgPropertyNameAlias("y"), TypeConverter(typeof(SvgUnitConverter)), DefaultValue((string) null)]
        public SvgUnit Y
        {
            get => 
                this.GetValueCore<SvgUnit>("Y", false);
            private set => 
                this.SetValueCore<SvgUnit>("Y", value);
        }

        [SvgPropertyNameAlias("width"), TypeConverter(typeof(SvgUnitConverter)), DefaultValue((string) null)]
        public SvgUnit Width
        {
            get => 
                this.GetValueCore<SvgUnit>("Width", false);
            private set => 
                this.SetValueCore<SvgUnit>("Width", value);
        }

        [SvgPropertyNameAlias("height"), TypeConverter(typeof(SvgUnitConverter)), DefaultValue((string) null)]
        public SvgUnit Height
        {
            get => 
                this.GetValueCore<SvgUnit>("Height", false);
            private set => 
                this.SetValueCore<SvgUnit>("Height", value);
        }

        [SvgPropertyNameAlias("xlink:href")]
        public Uri ReferencedElement
        {
            get => 
                this.GetValueCore<Uri>("ReferencedElement", false);
            private set => 
                this.SetValueCore<Uri>("ReferencedElement", value);
        }
    }
}

