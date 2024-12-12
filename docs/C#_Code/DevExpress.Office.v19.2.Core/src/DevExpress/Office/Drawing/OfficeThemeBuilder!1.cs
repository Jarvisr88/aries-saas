namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.DrawingML;
    using DevExpress.Office.Model;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class OfficeThemeBuilder<TFormat>
    {
        public const int DefaultOfficeThemeVersion1 = 0x1e542;
        public const int DefaultOfficeThemeVersion2 = 0x1e3ac;
        public const string DefaultPartOfficeName = "Office";
        public const string DefaultOfficeName = "Office Theme";
        private OfficeThemeBase<TFormat> theme;
        private OfficeThemePreset preset;

        private OfficeThemeBuilder(OfficeThemeBase<TFormat> theme, OfficeThemePreset preset)
        {
            this.theme = theme;
            this.preset = preset;
        }

        private void AddOuterShadowEffectToContainer(OuterShadowEffect shadowEffect)
        {
            DrawingEffectStyle item = new DrawingEffectStyle(this.theme);
            item.ContainerEffect.SetApplyEffectListCore(true);
            item.ContainerEffect.Effects.Add(shadowEffect);
            this.theme.FormatScheme.EffectStyleList.Add(item);
        }

        private void Build()
        {
            this.theme.BeginUpdate();
            try
            {
                this.GetDelegateCollection()[this.preset]();
            }
            finally
            {
                this.theme.EndUpdate();
            }
        }

        private void CalculateDefaultOfficeBackgroundFillStyleList()
        {
            this.theme.FormatScheme.BackgroundFillStyleList.Add(this.CreateSolidFill());
            DrawingGradientFill item = this.CreateGradientFill(false, GradientType.Circle);
            DrawingGradientStop stop = this.CreateGradientStopWithTint(0, 0x9c40, 0x55730);
            DrawingGradientStop stop2 = this.CreateGradientStopWithTintShade(0x9c40, 0xafc8, 0x182b8, 0x55730);
            DrawingGradientStop stop3 = this.CreateGradientStopWithShade(0x186a0, 0x4e20, 0x3e418);
            item.GradientStops.Add(stop);
            item.GradientStops.Add(stop2);
            item.GradientStops.Add(stop3);
            item.FillRect = new RectangleOffset(0x2bf20, 0xc350, 0xc350, -80000);
            this.theme.FormatScheme.BackgroundFillStyleList.Add(item);
            DrawingGradientFill fill2 = this.CreateGradientFill(false, GradientType.Circle);
            DrawingGradientStop stop4 = this.CreateGradientStopWithTint(0, 0x13880, 0x493e0);
            DrawingGradientStop stop5 = this.CreateGradientStopWithShade(0x186a0, 0x7530, 0x30d40);
            fill2.GradientStops.Add(stop4);
            fill2.GradientStops.Add(stop5);
            fill2.FillRect = new RectangleOffset(0xc350, 0xc350, 0xc350, 0xc350);
            this.theme.FormatScheme.BackgroundFillStyleList.Add(fill2);
        }

        private void CalculateDefaultOfficeEffectStyleList()
        {
            OuterShadowEffect shadowEffect = this.CreateOuterShadowEffect(0x4e20L, 0x9470);
            this.AddOuterShadowEffectToContainer(shadowEffect);
            OuterShadowEffect effect2 = this.CreateOuterShadowEffect(0x59d8L, 0x88b8);
            this.AddOuterShadowEffectToContainer(effect2);
            DrawingEffectStyle item = new DrawingEffectStyle(this.theme);
            OuterShadowEffect effect3 = this.CreateOuterShadowEffect(0x59d8L, 0x88b8);
            Scene3DProperties properties = new Scene3DProperties(this.theme) {
                Camera = { 
                    Preset = PresetCameraType.OrthographicFront,
                    Lat = 0
                },
                LightRig = { 
                    Preset = LightRigPreset.ThreePt,
                    Direction = LightRigDirection.Top,
                    Rev = 0x124f80
                }
            };
            Shape3DProperties properties2 = new Shape3DProperties(this.theme) {
                TopBevel = { 
                    Width = 100,
                    Height = 40
                }
            };
            item.ContainerEffect.Effects.Add(effect3);
            item.ContainerEffect.SetApplyEffectListCore(true);
            item.Scene3DProperties.CopyFrom(properties);
            item.Shape3DProperties.CopyFrom(properties2);
            this.theme.FormatScheme.EffectStyleList.Add(item);
        }

        private void CalculateDefaultOfficeFillStyleList()
        {
            this.theme.FormatScheme.FillStyleList.Add(this.CreateSolidFill());
            DrawingGradientFill item = this.CreateGradientFill(true, GradientType.Linear);
            DrawingGradientStop stop = this.CreateGradientStopWithTint(0, 0xc350, 0x493e0);
            DrawingGradientStop stop2 = this.CreateGradientStopWithTint(0x88b8, 0x9088, 0x493e0);
            DrawingGradientStop stop3 = this.CreateGradientStopWithTint(0x186a0, 0x3a98, 0x55730);
            item.GradientStops.Add(stop);
            item.GradientStops.Add(stop2);
            item.GradientStops.Add(stop3);
            this.theme.FormatScheme.FillStyleList.Add(item);
            DrawingGradientFill fill2 = this.CreateGradientFill(false, GradientType.Linear);
            DrawingGradientStop stop4 = this.CreateGradientStopWithShade(0, 0xc738, 0x1fbd0);
            DrawingGradientStop stop5 = this.CreateGradientStopWithShade(0x13880, 0x16b48, 0x1fbd0);
            DrawingGradientStop stop6 = this.CreateGradientStopWithShade(0x186a0, 0x16f30, 0x20f58);
            fill2.GradientStops.Add(stop4);
            fill2.GradientStops.Add(stop5);
            fill2.GradientStops.Add(stop6);
            this.theme.FormatScheme.FillStyleList.Add(fill2);
        }

        private void CalculateDefaultOfficeOutlineList()
        {
            Outline item = this.CreateOutline(15);
            item.Fill = this.CreateSolidFill(0x17318, 0x19a28);
            this.theme.FormatScheme.LineStyleList.Add(item);
            Outline outline2 = this.CreateOutline(40);
            outline2.Fill = this.CreateSolidFill();
            this.theme.FormatScheme.LineStyleList.Add(outline2);
            Outline outline3 = this.CreateOutline(60);
            outline3.Fill = this.CreateSolidFill();
            this.theme.FormatScheme.LineStyleList.Add(outline3);
        }

        private void CalculateDefaultOfficeThemeColorProperties()
        {
            ThemeDrawingColorCollection colors = this.theme.Colors;
            colors.Name = "Office";
            colors.SetColor(ThemeColorIndex.Light1, SystemColorValues.ScWindow);
            colors.SetColor(ThemeColorIndex.Dark1, SystemColorValues.ScWindowText);
            colors.SetColor(ThemeColorIndex.Light2, DXColor.FromArgb(0xee, 0xec, 0xe1));
            colors.SetColor(ThemeColorIndex.Dark2, DXColor.FromArgb(0x1f, 0x49, 0x7d));
            colors.SetColor(ThemeColorIndex.Accent1, DXColor.FromArgb(0x4f, 0x81, 0xbd));
            colors.SetColor(ThemeColorIndex.Accent2, DXColor.FromArgb(0xc0, 80, 0x4d));
            colors.SetColor(ThemeColorIndex.Accent3, DXColor.FromArgb(0x9b, 0xbb, 0x59));
            colors.SetColor(ThemeColorIndex.Accent4, DXColor.FromArgb(0x80, 100, 0xa2));
            colors.SetColor(ThemeColorIndex.Accent5, DXColor.FromArgb(0x4b, 0xac, 0xc6));
            colors.SetColor(ThemeColorIndex.Accent6, DXColor.FromArgb(0xf7, 150, 70));
            colors.SetColor(ThemeColorIndex.Hyperlink, DXColor.FromArgb(0, 0, 0xff));
            colors.SetColor(ThemeColorIndex.FollowedHyperlink, DXColor.FromArgb(0x80, 0, 0x80));
        }

        private void CalculateDefaultOfficeThemeFontProperties()
        {
            ThemeFontScheme fontScheme = this.theme.FontScheme;
            fontScheme.Name = "Office";
            ThemeFontSchemePart majorFont = fontScheme.MajorFont;
            majorFont.Latin.Typeface = "Cambria";
            majorFont.AddSupplementalFont("Jpan", "ＭＳ Ｐゴシック");
            majorFont.AddSupplementalFont("Hang", "맑은 고딕");
            majorFont.AddSupplementalFont("Hans", "宋体");
            majorFont.AddSupplementalFont("Hant", "新細明體");
            majorFont.AddSupplementalFont("Arab", "Times New Roman");
            majorFont.AddSupplementalFont("Hebr", "Times New Roman");
            majorFont.AddSupplementalFont("Thai", "Tahoma");
            majorFont.AddSupplementalFont("Ethi", "Nyala");
            majorFont.AddSupplementalFont("Beng", "Vrinda");
            majorFont.AddSupplementalFont("Gujr", "Shruti");
            majorFont.AddSupplementalFont("Khmr", "MoolBoran");
            majorFont.AddSupplementalFont("Knda", "Tunga");
            majorFont.AddSupplementalFont("Guru", "Raavi");
            majorFont.AddSupplementalFont("Cans", "Euphemia");
            majorFont.AddSupplementalFont("Cher", "Plantagenet Cherokee");
            majorFont.AddSupplementalFont("Yiii", "Microsoft Yi Baiti");
            majorFont.AddSupplementalFont("Tibt", "Microsoft Himalaya");
            majorFont.AddSupplementalFont("Thaa", "MV Boli");
            majorFont.AddSupplementalFont("Deva", "Mangal");
            majorFont.AddSupplementalFont("Telu", "Gautami");
            majorFont.AddSupplementalFont("Taml", "Latha");
            majorFont.AddSupplementalFont("Syrc", "Estrangelo Edessa");
            majorFont.AddSupplementalFont("Orya", "Kalinga");
            majorFont.AddSupplementalFont("Mlym", "Kartika");
            majorFont.AddSupplementalFont("Laoo", "DokChampa");
            majorFont.AddSupplementalFont("Sinh", "Iskoola Pota");
            majorFont.AddSupplementalFont("Mong", "Mongolian Baiti");
            majorFont.AddSupplementalFont("Viet", "Times New Roman");
            majorFont.AddSupplementalFont("Uigh", "Microsoft Uighur");
            majorFont.AddSupplementalFont("Geor", "Sylfaen");
            majorFont.IsValid = true;
            ThemeFontSchemePart minorFont = fontScheme.MinorFont;
            minorFont.Latin.Typeface = "Calibri";
            minorFont.AddSupplementalFont("Jpan", "ＭＳ Ｐゴシック");
            minorFont.AddSupplementalFont("Hang", "맑은 고딕");
            minorFont.AddSupplementalFont("Hans", "宋体");
            minorFont.AddSupplementalFont("Hant", "新細明體");
            minorFont.AddSupplementalFont("Arab", "Arial");
            minorFont.AddSupplementalFont("Hebr", "Arial");
            minorFont.AddSupplementalFont("Thai", "Tahoma");
            minorFont.AddSupplementalFont("Ethi", "Nyala");
            minorFont.AddSupplementalFont("Beng", "Vrinda");
            minorFont.AddSupplementalFont("Gujr", "Shruti");
            minorFont.AddSupplementalFont("Khmr", "DaunPenh");
            minorFont.AddSupplementalFont("Knda", "Tunga");
            minorFont.AddSupplementalFont("Guru", "Raavi");
            minorFont.AddSupplementalFont("Cans", "Euphemia");
            minorFont.AddSupplementalFont("Cher", "Plantagenet Cherokee");
            minorFont.AddSupplementalFont("Yiii", "Microsoft Yi Baiti");
            minorFont.AddSupplementalFont("Tibt", "Microsoft Himalaya");
            minorFont.AddSupplementalFont("Thaa", "MV Boli");
            minorFont.AddSupplementalFont("Deva", "Mangal");
            minorFont.AddSupplementalFont("Telu", "Gautami");
            minorFont.AddSupplementalFont("Taml", "Latha");
            minorFont.AddSupplementalFont("Syrc", "Estrangelo Edessa");
            minorFont.AddSupplementalFont("Orya", "Kalinga");
            minorFont.AddSupplementalFont("Mlym", "Kartika");
            minorFont.AddSupplementalFont("Laoo", "DokChampa");
            minorFont.AddSupplementalFont("Sinh", "Iskoola Pota");
            minorFont.AddSupplementalFont("Mong", "Mongolian Baiti");
            minorFont.AddSupplementalFont("Viet", "Arial");
            minorFont.AddSupplementalFont("Uigh", "Microsoft Uighur");
            minorFont.AddSupplementalFont("Geor", "Sylfaen");
            minorFont.IsValid = true;
        }

        private void CalculateDefaultOfficeThemeFormatProperties()
        {
            this.theme.FormatScheme.Name = "Office";
            this.CalculateDefaultOfficeFillStyleList();
            this.CalculateDefaultOfficeBackgroundFillStyleList();
            this.CalculateDefaultOfficeOutlineList();
            this.CalculateDefaultOfficeEffectStyleList();
        }

        private void CalculateDefaultOfficeThemeProperties()
        {
            this.theme.Name = "Office Theme";
            this.CalculateDefaultOfficeThemeColorProperties();
            this.CalculateDefaultOfficeThemeFontProperties();
            this.CalculateDefaultOfficeThemeFormatProperties();
        }

        private DrawingGradientFill CreateGradientFill(bool scaled, GradientType type) => 
            new DrawingGradientFill(this.theme) { 
                RotateWithShape = true,
                GradientType = type,
                Angle = 0xf73140,
                Scaled = scaled
            };

        private DrawingGradientStop CreateGradientStopCore(int position) => 
            new DrawingGradientStop(this.theme) { 
                Position = position,
                Color = { OriginalColor = { Scheme = SchemeColorValues.Style } }
            };

        private DrawingGradientStop CreateGradientStopWithShade(int position, int shade, int satMod)
        {
            DrawingGradientStop stop = this.CreateGradientStopCore(position);
            stop.Color.Transforms.AddCore(new ShadeColorTransform(shade));
            stop.Color.Transforms.AddCore(new SaturationModulationColorTransform(satMod));
            return stop;
        }

        private DrawingGradientStop CreateGradientStopWithTint(int position, int tint, int satMod)
        {
            DrawingGradientStop stop = this.CreateGradientStopCore(position);
            stop.Color.Transforms.AddCore(new TintColorTransform(tint));
            stop.Color.Transforms.AddCore(new SaturationModulationColorTransform(satMod));
            return stop;
        }

        private DrawingGradientStop CreateGradientStopWithTintShade(int position, int tint, int shade, int satMod)
        {
            DrawingGradientStop stop = this.CreateGradientStopCore(position);
            stop.Color.Transforms.AddCore(new TintColorTransform(tint));
            stop.Color.Transforms.AddCore(new ShadeColorTransform(shade));
            stop.Color.Transforms.AddCore(new SaturationModulationColorTransform(satMod));
            return stop;
        }

        private OuterShadowEffect CreateOuterShadowEffect(long distance, int alpha)
        {
            DrawingColor color = DrawingColor.Create(this.theme, DrawingColorModelInfo.CreateARGB(DXColor.FromArgb(0, 0, 0)));
            color.Transforms.AddCore(new AlphaColorTransform(alpha));
            return new OuterShadowEffect(color, new OuterShadowEffectInfo(new OffsetShadowInfo(distance, 0x5265c0), new ScalingFactorInfo(0x186a0, 0x186a0), new SkewAnglesInfo(0, 0), RectangleAlignType.Bottom, 0x9c40L, false));
        }

        private Outline CreateOutline(int width) => 
            new Outline(this.theme) { 
                Width = width,
                EndCapStyle = OutlineEndCapStyle.Flat,
                CompoundType = OutlineCompoundType.Single,
                StrokeAlignment = OutlineStrokeAlignment.Center,
                Dashing = OutlineDashing.Solid
            };

        private DrawingSolidFill CreateSolidFill() => 
            new DrawingSolidFill(this.theme) { Color = { OriginalColor = { Scheme = SchemeColorValues.Style } } };

        private DrawingSolidFill CreateSolidFill(int shade, int satMod)
        {
            DrawingSolidFill fill = this.CreateSolidFill();
            fill.Color.OriginalColor.Scheme = SchemeColorValues.Style;
            fill.Color.Transforms.AddCore(new ShadeColorTransform(shade));
            fill.Color.Transforms.AddCore(new SaturationModulationColorTransform(satMod));
            return fill;
        }

        public static OfficeThemeBase<TFormat> CreateTheme(OfficeThemePreset preset)
        {
            OfficeThemeBase<TFormat> theme = new OfficeThemeBase<TFormat>();
            new OfficeThemeBuilder<TFormat>(theme, preset).Build();
            return theme;
        }

        private Dictionary<OfficeThemePreset, CalculateOfficeThemeProperties<TFormat>> GetDelegateCollection() => 
            new Dictionary<OfficeThemePreset, CalculateOfficeThemeProperties<TFormat>> { { 
                OfficeThemePreset.Office,
                new CalculateOfficeThemeProperties<TFormat>(this.CalculateDefaultOfficeThemeProperties)
            } };

        public static bool IsDefaultOfficeThemeVersion(int themeVersion) => 
            (themeVersion == 0x1e542) || (themeVersion == 0x1e3ac);

        private delegate void CalculateOfficeThemeProperties();
    }
}

