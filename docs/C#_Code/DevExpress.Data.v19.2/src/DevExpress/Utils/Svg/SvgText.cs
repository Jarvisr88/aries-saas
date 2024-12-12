namespace DevExpress.Utils.Svg
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    [SvgElementNameAlias("text")]
    public class SvgText : SvgElement
    {
        public SvgText()
        {
            this.SetDefaultValueCore<SvgFontStyle>("FontStyle", SvgFontStyle.Normal);
            this.X = new SvgUnitCollection();
            this.Y = new SvgUnitCollection();
            this.Dx = new SvgUnitCollection();
            this.Dy = new SvgUnitCollection();
        }

        public override SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null) => 
            this.DeepCopy<SvgText>(updateStyle);

        [SvgPropertyNameAlias("text-anchor"), DefaultValue(0)]
        public virtual SvgTextAnchor TextAnchor
        {
            get => 
                this.GetValueCore<SvgTextAnchor>("TextAnchor", true);
            internal set => 
                this.SetValueCore<SvgTextAnchor>("TextAnchor", value);
        }

        [SvgPropertyNameAlias("baseline-shift")]
        public virtual string BaselineShift
        {
            get => 
                this.GetValueCore<string>("BaselineShift", true);
            internal set => 
                this.SetValueCore<string>("BaselineShift", value);
        }

        [SvgPropertyNameAlias("x"), TypeConverter(typeof(SvgUnitCollectionConverter))]
        public SvgUnitCollection X
        {
            get => 
                this.GetValueCore<SvgUnitCollection>("X", false);
            internal set => 
                this.SetValueCore<SvgUnitCollection>("X", value);
        }

        [SvgPropertyNameAlias("dx"), TypeConverter(typeof(SvgUnitCollectionConverter))]
        public SvgUnitCollection Dx
        {
            get => 
                this.GetValueCore<SvgUnitCollection>("DX", false);
            internal set => 
                this.SetValueCore<SvgUnitCollection>("DX", value);
        }

        [SvgPropertyNameAlias("y"), TypeConverter(typeof(SvgUnitCollectionConverter))]
        public SvgUnitCollection Y
        {
            get => 
                this.GetValueCore<SvgUnitCollection>("Y", false);
            internal set => 
                this.SetValueCore<SvgUnitCollection>("Y", value);
        }

        [SvgPropertyNameAlias("dy"), TypeConverter(typeof(SvgUnitCollectionConverter))]
        public SvgUnitCollection Dy
        {
            get => 
                this.GetValueCore<SvgUnitCollection>("DY", false);
            internal set => 
                this.SetValueCore<SvgUnitCollection>("DY", value);
        }

        [SvgPropertyNameAlias("rotate")]
        public string Rotate
        {
            get => 
                this.GetValueCore<string>("Rotate", false);
            internal set => 
                this.SetValueCore<string>("Rotate", value);
        }

        [SvgPropertyNameAlias("textLength")]
        public virtual SvgUnit TextLength
        {
            get => 
                this.GetValueCore<SvgUnit>("TextLength", false);
            internal set => 
                this.SetValueCore<SvgUnit>("RoTextLengthtate", value);
        }

        [SvgPropertyNameAlias("lengthAdjust"), DefaultValue(0)]
        public virtual SvgTextLengthAdjust LengthAdjust
        {
            get => 
                this.GetValueCore<SvgTextLengthAdjust>("LengthAdjust", false);
            internal set => 
                this.SetValueCore<SvgTextLengthAdjust>("LengthAdjust", value);
        }

        [SvgPropertyNameAlias("letter-spacing")]
        public virtual SvgUnit LetterSpacing
        {
            get => 
                this.GetValueCore<SvgUnit>("LetterSpacing", false);
            internal set => 
                this.SetValueCore<SvgUnit>("LetterSpacing", value);
        }

        [SvgPropertyNameAlias("word-spacing")]
        public virtual SvgUnit WordSpacing
        {
            get => 
                this.GetValueCore<SvgUnit>("WordSpacing", false);
            internal set => 
                this.SetValueCore<SvgUnit>("WordSpacing", value);
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

        [SvgPropertyNameAlias("font")]
        public virtual string Font
        {
            get => 
                string.Empty;
            set
            {
            }
        }

        private enum FontParseState
        {
            fontStyle,
            fontVariant,
            fontWeight,
            fontSize,
            fontFamilyNext,
            fontFamilyCurr
        }
    }
}

