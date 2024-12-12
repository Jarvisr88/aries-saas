namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class SvgElement
    {
        protected readonly Hashtable valueHash = new Hashtable(13, StringComparer.Ordinal);
        private readonly Hashtable defaultValueHash = new Hashtable(9, StringComparer.Ordinal);
        public const string DefaultColor = "#000000";
        private IList<SvgElement> elementsCore;
        private Stack<SvgStyle> stylesCore;

        public SvgElement()
        {
            this.SetDefaultValueCore<string>("Fill", string.Empty);
            this.SetDefaultValueCore<string>("Stroke", string.Empty);
            this.SetDefaultValueCore<string>("StyleName", string.Empty);
            this.SetDefaultValueCore<bool>("UsePalette", true);
            this.SetDefaultValueCore<SvgStrokeLineCap>("StrokeLineCap", SvgStrokeLineCap.Butt);
            this.SetDefaultValueCore<SvgStrokeLineJoin>("StrokeLineJoin", SvgStrokeLineJoin.Miter);
            this.SetDefaultValueCore<SvgFillRule>("FillRule", SvgFillRule.NonZero);
            this.Transformations = new SvgTransformCollection();
            this.elementsCore = new List<SvgElement>();
            this.stylesCore = new Stack<SvgStyle>();
        }

        public void AddElement(SvgElement element)
        {
            this.AddElementCore(element);
        }

        protected virtual void AddElementCore(SvgElement element)
        {
            this.Elements.Add(element);
            element.SetParent(this);
        }

        protected virtual void Assign(SvgElementProperties properties)
        {
            if (properties != null)
            {
                this.Fill = properties.Fill;
                this.Opacity = properties.Opacity;
                this.FillOpacity = properties.FillOpacity;
                this.Id = properties.Id;
                this.StyleName = properties.StyleName;
                this.Transformations = properties.Transformations;
                this.Style = properties.Style;
                this.Stroke = properties.Stroke;
                this.StrokeWidth = properties.StrokeWidth;
                this.StrokeLineCap = properties.StrokeLineCap;
                this.StrokeLineJoin = properties.StrokeLineJoin;
                this.StrokeMiterLimit = properties.StrokeMiterLimit;
                this.StrokeDashArray = properties.StrokeDashArray;
                this.StrokeDashOffset = properties.StrokeDashOffset;
                this.StrokeOpacity = properties.StrokeOpacity;
                this.Display = properties.Display;
                this.FillRule = properties.FillRule;
                this.Brightness = properties.Brightness;
                this.UsePalette = properties.UsePalette;
            }
        }

        public SvgElement Clone(SvgElementProperties properties)
        {
            SvgElement element = this.DeepCopy(null);
            element.Assign(properties);
            return element;
        }

        public abstract SvgElement DeepCopy(Action<SvgElement, Hashtable> updateStyle = null);
        public virtual T DeepCopy<T>(Action<SvgElement, Hashtable> updateStyle = null) where T: SvgElement, new()
        {
            T local = Activator.CreateInstance<T>();
            foreach (DictionaryEntry entry in this.valueHash)
            {
                local.valueHash[entry.Key] = !(entry.Value is ICloneable) ? entry.Value : (entry.Value as ICloneable).Clone();
            }
            foreach (SvgTransform transform in this.Transformations)
            {
                local.Transformations.Add(transform.DeepCopy());
            }
            foreach (SvgStyle style in this.Styles.Reverse<SvgStyle>())
            {
                local.Styles.Push(style.DeepCopy());
            }
            foreach (SvgElement element in this.Elements)
            {
                SvgElement element2 = element.DeepCopy(updateStyle);
                local.AddElement(element2);
            }
            if (this.DefaultStyle != null)
            {
                local.DefaultStyle = this.DefaultStyle.DeepCopy();
            }
            if (updateStyle != null)
            {
                updateStyle(local, local.valueHash);
            }
            return local;
        }

        protected virtual T GetDefaultValueCore<T>(object key)
        {
            object obj2 = this.defaultValueHash[key];
            if (obj2 != null)
            {
                return (T) obj2;
            }
            return default(T);
        }

        protected virtual T GetValueCore<T>(object key, bool isInherit = false)
        {
            T local;
            if (this.DefaultStyle != null)
            {
                return this.GetValueFromDefaultStyle<T>(key);
            }
            T defaultValueCore = this.GetDefaultValueCore<T>(key);
            return (!this.TryGetValueFromStyle<T>(key, defaultValueCore, out local) ? (!this.valueHash.ContainsKey(key) ? ((!isInherit || !this.valueHash.Contains("Parent")) ? defaultValueCore : this.Parent.GetValueCore<T>(key, true)) : ((T) this.valueHash[key])) : local);
        }

        protected internal object GetValueForSerialize(object key, Type type)
        {
            object obj2 = this.defaultValueHash[key];
            obj2 ??= (type.IsValueType ? Activator.CreateInstance(type) : null);
            return (!this.valueHash.ContainsKey(key) ? obj2 : this.valueHash[key]);
        }

        private T GetValueFromDefaultStyle<T>(object key)
        {
            T local;
            if (this.valueHash.ContainsKey(key))
            {
                return (T) this.valueHash[key];
            }
            T defaultValueCore = this.GetDefaultValueCore<T>(key);
            return (((this.DefaultStyle == null) || !this.DefaultStyle.TryGetValue<T>(key, defaultValueCore, out local)) ? (!this.TryGetValueFromStyle<T>(key, defaultValueCore, out local) ? defaultValueCore : local) : local);
        }

        protected internal void SetAttribute(string key, string value)
        {
            this.SetValueCore<string>(key, value);
        }

        protected virtual void SetDefaultValueCore<T>(object key, T value)
        {
            this.defaultValueHash[key] = value;
        }

        protected internal void SetDouble(string key, double value)
        {
            this.SetValueCore<double>(key, value);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public void SetParent(SvgElement parent)
        {
            this.SetValueCore<SvgElement>("Parent", parent);
        }

        protected internal void SetUnit(string key, SvgUnit value)
        {
            this.SetValueCore<SvgUnit>(key, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetUsePalette(bool value)
        {
            this.UsePalette = value;
        }

        protected virtual void SetValueCore<T>(object key, T value)
        {
            T defaultValueCore = this.GetDefaultValueCore<T>(key);
            if (EqualityComparer<T>.Default.Equals(value, defaultValueCore))
            {
                this.valueHash.Remove(key);
            }
            else
            {
                this.valueHash[key] = value;
            }
        }

        protected virtual void SetValueCore<T>(object key, T value, Action valueChangedCallback)
        {
            this.SetValueCore<T>(key, value);
            if (valueChangedCallback != null)
            {
                valueChangedCallback();
            }
        }

        protected virtual bool TryGetValueCore<T>(object key, T defaultValue, out T result)
        {
            if (this.valueHash.ContainsKey(key))
            {
                result = (T) this.valueHash[key];
                return true;
            }
            result = defaultValue;
            return false;
        }

        private bool TryGetValueFromStyle<T>(object key, T defaultValue, out T result)
        {
            bool flag;
            using (Stack<SvgStyle>.Enumerator enumerator = this.Styles.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        SvgStyle current = enumerator.Current;
                        if (!current.TryGetValue<T>(key, defaultValue, out result))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        result = defaultValue;
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        public IList<SvgElement> Elements =>
            this.elementsCore;

        public Stack<SvgStyle> Styles =>
            this.stylesCore;

        public SvgElement Parent =>
            this.GetValueCore<SvgElement>("Parent", false);

        [SvgPropertyNameAlias("fill"), DefaultValue("")]
        public string Fill
        {
            get => 
                this.GetValueCore<string>("Fill", true);
            internal set => 
                this.SetValueCore<string>("Fill", value);
        }

        [SvgPropertyNameAlias("opacity"), DefaultValue((string) null)]
        public double? Opacity
        {
            get => 
                this.GetValueCore<double?>("Opacity", true);
            internal set => 
                this.SetValueCore<double?>("Opacity", value);
        }

        [SvgPropertyNameAlias("fill-opacity"), DefaultValue((string) null)]
        public double? FillOpacity
        {
            get => 
                this.GetValueCore<double?>("FillOpacity", true);
            internal set => 
                this.SetValueCore<double?>("FillOpacity", value);
        }

        [SvgPropertyNameAlias("id")]
        public string Id
        {
            get => 
                this.GetValueCore<string>("Id", false);
            internal set => 
                this.SetValueCore<string>("Id", value);
        }

        [SvgPropertyNameAlias("class")]
        public string StyleName
        {
            get => 
                this.GetValueCore<string>("StyleName", false);
            internal set => 
                this.SetValueCore<string>("StyleName", value);
        }

        [SvgPropertyNameAlias("transform"), TypeConverter(typeof(SvgTransformConverter))]
        public SvgTransformCollection Transformations { get; internal set; }

        [SvgPropertyNameAlias("style"), TypeConverter(typeof(SvgStyleConverter))]
        public SvgStyle Style
        {
            get => 
                this.GetValueCore<SvgStyle>("Style", false);
            internal set => 
                this.SetValueCore<SvgStyle>("Style", value);
        }

        [SvgPropertyNameAlias("stroke"), DefaultValue("")]
        public string Stroke
        {
            get => 
                this.GetValueCore<string>("Stroke", true);
            internal set => 
                this.SetValueCore<string>("Stroke", value);
        }

        [SvgPropertyNameAlias("stroke-width"), TypeConverter(typeof(SvgUnitConverter)), DefaultValue((string) null)]
        public SvgUnit StrokeWidth
        {
            get => 
                this.GetValueCore<SvgUnit>("StrokeWidth", true);
            internal set => 
                this.SetValueCore<SvgUnit>("StrokeWidth", value);
        }

        [SvgPropertyNameAlias("stroke-linecap"), DefaultValue(0)]
        public SvgStrokeLineCap StrokeLineCap
        {
            get => 
                this.GetValueCore<SvgStrokeLineCap>("StrokeLineCap", true);
            internal set => 
                this.SetValueCore<SvgStrokeLineCap>("StrokeLineCap", value);
        }

        [SvgPropertyNameAlias("stroke-linejoin"), DefaultValue(0)]
        public SvgStrokeLineJoin StrokeLineJoin
        {
            get => 
                this.GetValueCore<SvgStrokeLineJoin>("StrokeLineJoin", true);
            internal set => 
                this.SetValueCore<SvgStrokeLineJoin>("StrokeLineJoin", value);
        }

        [SvgPropertyNameAlias("stroke-miterlimit"), DefaultValue((string) null)]
        public double? StrokeMiterLimit
        {
            get => 
                this.GetValueCore<double?>("StrokeMiterLimit", true);
            internal set => 
                this.SetValueCore<double?>("StrokeMiterLimit", value);
        }

        [SvgPropertyNameAlias("stroke-dasharray"), TypeConverter(typeof(SvgUnitCollectionConverter))]
        public SvgUnitCollection StrokeDashArray
        {
            get => 
                this.GetValueCore<SvgUnitCollection>("StrokeDashArray", true);
            internal set => 
                this.SetValueCore<SvgUnitCollection>("StrokeDashArray", value);
        }

        [SvgPropertyNameAlias("stroke-dashoffset"), TypeConverter(typeof(SvgUnitConverter)), DefaultValue((string) null)]
        public SvgUnit StrokeDashOffset
        {
            get => 
                this.GetValueCore<SvgUnit>("StrokeDashOffset", true);
            internal set => 
                this.SetValueCore<SvgUnit>("StrokeDashOffset", value);
        }

        [SvgPropertyNameAlias("stroke-opacity"), DefaultValue((string) null)]
        public double? StrokeOpacity
        {
            get => 
                this.GetValueCore<double?>("StrokeOpacity", false);
            internal set => 
                this.SetValueCore<double?>("StrokeOpacity", value);
        }

        [SvgPropertyNameAlias("display"), DefaultValue("")]
        public string Display
        {
            get => 
                this.GetValueCore<string>("Display", true);
            internal set => 
                this.SetValueCore<string>("Display", value);
        }

        [SvgPropertyNameAlias("clip-path"), DefaultValue((string) null)]
        public Uri ClipPath
        {
            get => 
                this.GetValueCore<Uri>("ClipPath", false);
            internal set => 
                this.SetValueCore<Uri>("ClipPath", value);
        }

        [SvgPropertyNameAlias("clip-rule"), DefaultValue(0)]
        public SvgClipRule ClipRule
        {
            get => 
                this.GetValueCore<SvgClipRule>("ClipRule", false);
            internal set => 
                this.SetValueCore<SvgClipRule>("ClipRule", value);
        }

        [SvgPropertyNameAlias("fill-rule"), DefaultValue(0)]
        public SvgFillRule FillRule
        {
            get => 
                this.GetValueCore<SvgFillRule>("FillRule", true);
            internal set => 
                this.SetValueCore<SvgFillRule>("FillRule", value);
        }

        [SvgPropertyNameAlias("font-family")]
        public virtual string FontFamily
        {
            get => 
                this.GetValueCore<string>("FontFamily", true);
            internal set => 
                this.SetValueCore<string>("FontFamily", value);
        }

        [SvgPropertyNameAlias("font-size"), TypeConverter(typeof(SvgUnitConverter))]
        public virtual SvgUnit FontSize
        {
            get => 
                this.GetValueCore<SvgUnit>("FontSize", true);
            internal set => 
                this.SetValueCore<SvgUnit>("FontSize", value);
        }

        public double? Brightness
        {
            get => 
                this.GetValueCore<double?>("Brightness", false);
            internal set => 
                this.SetValueCore<double?>("Brightness", value);
        }

        public SvgStyle DefaultStyle { get; internal set; }

        public bool UsePalette
        {
            get => 
                this.GetValueCore<bool>("UsePalette", false);
            internal set => 
                this.SetValueCore<bool>("UsePalette", value);
        }

        [SvgPropertyNameAlias("tag"), DefaultValue((string) null), TypeConverter(typeof(SvgTagConverter))]
        public object Tag
        {
            get => 
                this.GetValueCore<object>("Tag", false);
            internal set => 
                this.SetValueCore<object>("Tag", value);
        }
    }
}

