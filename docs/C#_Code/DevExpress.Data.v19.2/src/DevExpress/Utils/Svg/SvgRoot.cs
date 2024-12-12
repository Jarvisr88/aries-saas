namespace DevExpress.Utils.Svg
{
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.ComponentModel;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("svg")]
    public class SvgRoot : SvgElement
    {
        public SvgRoot()
        {
            this.SetDefaultValueCore<string>("Xmlns", "http://www.w3.org/2000/svg");
        }

        public static SvgRoot Create(SvgElementProperties properties, SvgUnit x = null, SvgUnit y = null, SvgUnit width = null, SvgUnit height = null, SvgViewBox viewBox = null)
        {
            SvgRoot root1 = new SvgRoot();
            root1.X = x;
            root1.Y = y;
            root1.Width = width;
            root1.Height = height;
            root1.ViewBox = viewBox;
            SvgRoot root = root1;
            root.Assign(properties);
            return root;
        }

        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgRoot>(updateStyle);

        public Matrix GetViewBoxTransform()
        {
            Matrix matrix = new Matrix();
            if ((this.ViewBox != null) && ((this.Width == null) || (this.Height == null)))
            {
                matrix.Translate(-((float) this.ViewBox.MinX), -((float) this.ViewBox.MinY));
            }
            if ((this.ViewBox != null) && ((this.Width != null) && (this.Height != null)))
            {
                float scaleX = (float) Math.Min((double) (this.Width.Value / this.ViewBox.Width), (double) (this.Height.Value / this.ViewBox.Height));
                float offsetX = (-((float) this.ViewBox.MinX) * scaleX) + ((((float) this.Width.Value) / 2f) - (((float) (this.ViewBox.Width / 2.0)) * scaleX));
                float offsetY = (-((float) this.ViewBox.MinY) * scaleX) + ((((float) this.Height.Value) / 2f) - (((float) (this.ViewBox.Height / 2.0)) * scaleX));
                Func<SvgUnit, float> get = <>c.<>9__32_0;
                if (<>c.<>9__32_0 == null)
                {
                    Func<SvgUnit, float> local1 = <>c.<>9__32_0;
                    get = <>c.<>9__32_0 = x => (float) x.Value;
                }
                float num8 = this.X.Get<SvgUnit, float>(get, 0f);
                Func<SvgUnit, float> func2 = <>c.<>9__32_1;
                if (<>c.<>9__32_1 == null)
                {
                    Func<SvgUnit, float> local2 = <>c.<>9__32_1;
                    func2 = <>c.<>9__32_1 = y => (float) y.Value;
                }
                matrix.Translate(num8, this.Y.Get<SvgUnit, float>(func2, 0f));
                matrix.Translate(offsetX, offsetY);
                matrix.Scale(scaleX, scaleX);
            }
            return matrix;
        }

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

        [SvgPropertyNameAlias("viewBox"), TypeConverter(typeof(SvgViewBoxTypeConverter))]
        public SvgViewBox ViewBox
        {
            get => 
                this.GetValueCore<SvgViewBox>("ViewBox", false);
            internal set => 
                this.SetValueCore<SvgViewBox>("ViewBox", value);
        }

        [SvgPropertyNameAlias("version")]
        public string Version
        {
            get => 
                this.GetValueCore<string>("Version", false);
            private set => 
                this.SetValueCore<string>("Version", value);
        }

        [SvgPropertyNameAlias("xmlns")]
        public string Xmlns
        {
            get => 
                this.GetValueCore<string>("Xmlns", false);
            private set => 
                this.SetValueCore<string>("Xmlns", value);
        }

        [SvgPropertyNameAlias("xmlns:xlink")]
        public string XmlnsXlink
        {
            get => 
                this.GetValueCore<string>("XmlnsXlink", false);
            private set => 
                this.SetValueCore<string>("XmlnsXlink", value);
        }

        [SvgPropertyNameAlias("enable-background"), TypeConverter(typeof(SvgBackgroundTypeConverter))]
        public SvgViewBox Background
        {
            get => 
                this.GetValueCore<SvgViewBox>("Background", false);
            internal set => 
                this.SetValueCore<SvgViewBox>("Background", value);
        }

        [SvgPropertyNameAlias("xml:space")]
        public string XmlSpace
        {
            get => 
                this.GetValueCore<string>("XmlSpace", false);
            private set => 
                this.SetValueCore<string>("XmlSpace", value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgRoot.<>c <>9 = new SvgRoot.<>c();
            public static Func<SvgUnit, float> <>9__32_0;
            public static Func<SvgUnit, float> <>9__32_1;

            internal float <GetViewBoxTransform>b__32_0(SvgUnit x) => 
                (float) x.Value;

            internal float <GetViewBoxTransform>b__32_1(SvgUnit y) => 
                (float) y.Value;
        }
    }
}

