namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class SvgStyleElement : SvgGradientStop
    {
        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgStyleElement>(updateStyle);

        protected override void SetValueCore<T>(object key, T value)
        {
            base.valueHash[key] = value;
        }

        public virtual bool TryGetValue<T>(object key, T defaultValue, out T result) => 
            this.TryGetValueCore<T>(key, defaultValue, out result);

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

        [SvgPropertyNameAlias("enable-background"), TypeConverter(typeof(SvgBackgroundTypeConverter))]
        public SvgViewBox Background
        {
            get => 
                this.GetValueCore<SvgViewBox>("Background", false);
            internal set => 
                this.SetValueCore<SvgViewBox>("Background", value);
        }

        [SvgPropertyNameAlias("text-anchor"), DefaultValue(0)]
        public virtual SvgTextAnchor TextAnchor
        {
            get => 
                this.GetValueCore<SvgTextAnchor>("TextAnchor", true);
            internal set => 
                this.SetValueCore<SvgTextAnchor>("TextAnchor", value);
        }

        [SvgPropertyNameAlias("font-style"), DefaultValue(1)]
        public SvgFontStyle FontStyle
        {
            virtual get => 
                this.GetValueCore<SvgFontStyle>("FontStyle", false);
            private set => 
                this.SetValueCore<SvgFontStyle>("FontStyle", value);
        }

        [SvgPropertyNameAlias("font-variant"), DefaultValue(0)]
        public SvgFontVariant FontVariant
        {
            virtual get => 
                this.GetValueCore<SvgFontVariant>("FontVariant", false);
            private set => 
                this.SetValueCore<SvgFontVariant>("FontVariant", value);
        }

        [SvgPropertyNameAlias("text-decoration"), DefaultValue(0)]
        public SvgTextDecoration TextDecoration
        {
            virtual get => 
                this.GetValueCore<SvgTextDecoration>("TextDecoration", false);
            private set => 
                this.SetValueCore<SvgTextDecoration>("TextDecoration", value);
        }

        [SvgPropertyNameAlias("font-weight"), DefaultValue(0)]
        public SvgFontWeight FontWeight
        {
            virtual get => 
                this.GetValueCore<SvgFontWeight>("FontWeight", false);
            private set => 
                this.SetValueCore<SvgFontWeight>("FontWeight", value);
        }
    }
}

