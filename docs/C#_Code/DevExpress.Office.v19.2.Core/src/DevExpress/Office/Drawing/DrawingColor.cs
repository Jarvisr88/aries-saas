namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using DevExpress.Utils;
    using System;
    using System.Drawing;

    public class DrawingColor : DrawingUndoableIndexBasedObjectEx<DrawingColorModelInfo>, ISupportsCopyFrom<DrawingColor>, IDrawingColor, IDrawingOriginalColor, IDrawingBullet
    {
        public static readonly PropertyKey RgbPropertyKey = new PropertyKey(0);
        public static readonly PropertyKey SystemPropertyKey = new PropertyKey(1);
        public static readonly PropertyKey SchemePropertyKey = new PropertyKey(2);
        public static readonly PropertyKey PresetPropertyKey = new PropertyKey(3);
        public static readonly PropertyKey HslPropertyKey = new PropertyKey(4);
        public static readonly PropertyKey ScRgbPropertyKey = new PropertyKey(5);
        private readonly ColorTransformCollection transforms;

        public DrawingColor(IDocumentModel documentModel) : base(documentModel.MainPart)
        {
            this.transforms = new ColorTransformCollection(documentModel);
        }

        private void AssignColorRGB(Color color)
        {
            int a = color.A;
            if (a == 0xff)
            {
                base.AssignInfo(DrawingColorModelInfo.CreateARGB(color));
            }
            else
            {
                base.AssignInfo(DrawingColorModelInfo.CreateRGB(color));
                this.Transforms.AddInternal(AlphaColorTransform.CreateFromA(a));
            }
        }

        public void Clear()
        {
            if (base.IsUpdateLocked)
            {
                base.Info.CopyFrom(base.DrawingCache.DrawingColorModelInfoCache.DefaultItem);
            }
            else
            {
                this.ChangeIndexCore(0, PropertyKey.Undefined);
            }
            this.transforms.Clear();
        }

        public DrawingColor CloneTo(IDocumentModel documentModel)
        {
            DrawingColor color = new DrawingColor(documentModel);
            color.CopyFrom(this);
            return color;
        }

        public void CopyFrom(DrawingColor other)
        {
            base.CopyFrom(other);
            this.transforms.CopyFrom(other.transforms);
        }

        public static DrawingColor Create(IDocumentModel documentModel, DrawingColorModelInfo colorInfo)
        {
            DrawingColor color = new DrawingColor(documentModel);
            color.AssignInfo(colorInfo);
            return color;
        }

        public static DrawingColor Create(IDocumentModel documentModel, Color color)
        {
            DrawingColor color2 = new DrawingColor(documentModel);
            color2.AssignColorRGB(color);
            return color2;
        }

        IDrawingBullet IDrawingBullet.CloneTo(IDocumentModel documentModel) => 
            this.CloneTo(documentModel);

        void IDrawingOriginalColor.SetColorFromRGB(Color rgb)
        {
            int a = rgb.A;
            if (a == 0xff)
            {
                this.SetARGBColor(rgb);
            }
            else
            {
                base.DocumentModel.BeginUpdate();
                try
                {
                    this.SetARGBColor(DXColor.FromArgb(0xff, rgb.R, rgb.G, rgb.B));
                    this.transforms.Add(AlphaColorTransform.CreateFromA(a));
                }
                finally
                {
                    base.DocumentModel.EndUpdate();
                }
            }
        }

        public override bool Equals(object obj)
        {
            DrawingColor color = obj as DrawingColor;
            return ((color != null) ? ((base.Index == color.Index) && this.transforms.Equals(color.transforms)) : false);
        }

        protected internal override UniqueItemsCache<DrawingColorModelInfo> GetCache(IDocumentModel documentModel) => 
            base.DrawingCache.DrawingColorModelInfoCache;

        public override int GetHashCode() => 
            base.Index ^ this.transforms.GetHashCode();

        private void SetARGBColor(Color argb)
        {
            if ((base.Info.Rgb != argb) || (base.Info.ColorType != DrawingColorType.Rgb))
            {
                this.SetPropertyValue<Color>(new UndoableIndexBasedObject<DrawingColorModelInfo, PropertyKey>.SetPropertyValueDelegate<Color>(this.SetARGBColorCore), argb);
            }
        }

        private PropertyKey SetARGBColorCore(DrawingColorModelInfo info, Color value)
        {
            info.Rgb = value;
            return RgbPropertyKey;
        }

        private PropertyKey SetHslColor(DrawingColorModelInfo info, ColorHSL value)
        {
            info.Hsl = value;
            return HslPropertyKey;
        }

        private PropertyKey SetPresetColor(DrawingColorModelInfo info, string value)
        {
            info.Preset = value;
            return PresetPropertyKey;
        }

        private PropertyKey SetSchemeColor(DrawingColorModelInfo info, SchemeColorValues value)
        {
            info.SchemeColor = value;
            return SchemePropertyKey;
        }

        private PropertyKey SetScRgbColor(DrawingColorModelInfo info, ScRGBColor value)
        {
            info.ScRgb = value;
            return ScRgbPropertyKey;
        }

        private PropertyKey SetSystemColor(DrawingColorModelInfo info, SystemColorValues value)
        {
            info.SystemColor = value;
            return SystemPropertyKey;
        }

        public Color ToRgb(Color styleColor)
        {
            ThemeDrawingColorCollection colors = base.DocumentModel.OfficeTheme.Colors;
            Color color = base.Info.ToRgb(colors, styleColor);
            if (this.HasColorTransform)
            {
                color = this.transforms.ApplyTransform(color);
            }
            return (((color.A != 0) || ((color.R != 0) || ((color.G != 0) || (color.B != 0)))) ? color : Color.Empty);
        }

        public void Visit(IDrawingBulletVisitor visitor)
        {
            visitor.Visit(this);
        }

        private bool HasColorTransform =>
            this.transforms.Count != 0;

        public bool IsEmpty =>
            !this.HasColorTransform && base.Info.IsEmpty;

        public Color FinalColor =>
            this.ToRgb(DXColor.Empty);

        public ColorTransformCollection Transforms =>
            this.transforms;

        public IDrawingOriginalColor OriginalColor =>
            this;

        Color IDrawingOriginalColor.Rgb
        {
            get => 
                base.Info.Rgb;
            set => 
                this.SetARGBColor(value);
        }

        SystemColorValues IDrawingOriginalColor.System
        {
            get => 
                base.Info.SystemColor;
            set
            {
                if ((base.Info.SystemColor != value) || (base.Info.ColorType != DrawingColorType.System))
                {
                    this.SetPropertyValue<SystemColorValues>(new UndoableIndexBasedObject<DrawingColorModelInfo, PropertyKey>.SetPropertyValueDelegate<SystemColorValues>(this.SetSystemColor), value);
                }
            }
        }

        SchemeColorValues IDrawingOriginalColor.Scheme
        {
            get => 
                base.Info.SchemeColor;
            set
            {
                if ((base.Info.SchemeColor != value) || (base.Info.ColorType != DrawingColorType.Scheme))
                {
                    this.SetPropertyValue<SchemeColorValues>(new UndoableIndexBasedObject<DrawingColorModelInfo, PropertyKey>.SetPropertyValueDelegate<SchemeColorValues>(this.SetSchemeColor), value);
                }
            }
        }

        string IDrawingOriginalColor.Preset
        {
            get => 
                base.Info.Preset;
            set
            {
                if ((base.Info.Preset != value) || (base.Info.ColorType != DrawingColorType.Preset))
                {
                    this.SetPropertyValue<string>(new UndoableIndexBasedObject<DrawingColorModelInfo, PropertyKey>.SetPropertyValueDelegate<string>(this.SetPresetColor), value);
                }
            }
        }

        ColorHSL IDrawingOriginalColor.Hsl
        {
            get => 
                base.Info.Hsl;
            set
            {
                if (!base.Info.Hsl.Equals(value) || (base.Info.ColorType != DrawingColorType.Hsl))
                {
                    this.SetPropertyValue<ColorHSL>(new UndoableIndexBasedObject<DrawingColorModelInfo, PropertyKey>.SetPropertyValueDelegate<ColorHSL>(this.SetHslColor), value);
                }
            }
        }

        ScRGBColor IDrawingOriginalColor.ScRgb
        {
            get => 
                base.Info.ScRgb;
            set
            {
                if (!base.Info.ScRgb.Equals(value) || (base.Info.ColorType != DrawingColorType.ScRgb))
                {
                    this.SetPropertyValue<ScRGBColor>(new UndoableIndexBasedObject<DrawingColorModelInfo, PropertyKey>.SetPropertyValueDelegate<ScRGBColor>(this.SetScRgbColor), value);
                }
            }
        }

        public DrawingColorType ColorType =>
            base.Info.ColorType;

        public DrawingBulletType Type =>
            DrawingBulletType.Color;
    }
}

