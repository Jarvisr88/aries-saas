namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf.ContentGeneration.Interop;
    using DevExpress.Pdf.Localization;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    public static class PdfGDIPlusGraphicsConverter
    {
        public static DXBrush ConvertBrush(Brush brush)
        {
            if (brush == null)
            {
                throw new ArgumentNullException("brush");
            }
            SolidBrush brush2 = brush as SolidBrush;
            if (brush2 != null)
            {
                return new DXSolidBrush(ConvertColor(brush2.Color));
            }
            HatchBrush brush3 = brush as HatchBrush;
            if (brush3 != null)
            {
                return new DXHatchBrush((DXHatchStyle) brush3.HatchStyle, ConvertColor(brush3.ForegroundColor), ConvertColor(brush3.BackgroundColor));
            }
            LinearGradientBrush brush4 = brush as LinearGradientBrush;
            if (brush4 != null)
            {
                return CreateFromLinearGradientBrush(brush4);
            }
            PathGradientBrush brush5 = brush as PathGradientBrush;
            if (brush5 != null)
            {
                return CreateFromPathGradientBrush(brush5);
            }
            TextureBrush brush6 = brush as TextureBrush;
            if (brush6 == null)
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgUnsupportedBrushType), "brush");
            }
            DXTextureBrush brush7 = new DXTextureBrush((Image) brush6.Image.Clone(), (DXWrapMode) brush6.WrapMode);
            using (Matrix matrix = brush6.Transform)
            {
                brush7.Transform = ConvertMatrix(matrix);
            }
            return brush7;
        }

        public static ARGBColor ConvertColor(Color color) => 
            ARGBColor.FromArgb(color.A, color.R, color.G, color.B);

        private static DXTransformationMatrix ConvertMatrix(Matrix matrix)
        {
            float[] elements = matrix.Elements;
            return new DXTransformationMatrix(elements[0], elements[1], elements[2], elements[3], elements[4], elements[5]);
        }

        [SecuritySafeCritical]
        public static EmfMetafile ConvertMetafile(Metafile metafile)
        {
            EmfMetafile metafile3;
            using (Metafile metafile2 = (Metafile) metafile.Clone())
            {
                IntPtr henhmetafile = metafile2.GetHenhmetafile();
                uint cbBuffer = Gdi32Interop.GetEnhMetaFileBits(henhmetafile, 0, null);
                if ((cbBuffer == 0) && (Marshal.GetLastWin32Error() != 0))
                {
                    Gdi32Interop.DeleteMetaFile(henhmetafile);
                    metafile3 = null;
                }
                else
                {
                    byte[] lpbBuffer = new byte[cbBuffer];
                    Gdi32Interop.GetEnhMetaFileBits(henhmetafile, cbBuffer, lpbBuffer);
                    Gdi32Interop.DeleteEnhMetaFile(henhmetafile);
                    metafile3 = EmfMetafile.Create(lpbBuffer);
                }
            }
            return metafile3;
        }

        public static DXPen ConvertPen(Pen pen)
        {
            DXPen pen2 = new DXPen(ConvertBrush(pen.Brush), (double) pen.Width);
            LineCap startCap = pen.StartCap;
            LineCap endCap = pen.EndCap;
            pen2.Alignment = (DXPenAlignment) pen.Alignment;
            pen2.StartCap = (DXLineCap) startCap;
            pen2.EndCap = (DXLineCap) endCap;
            pen2.MiterLimit = pen.MiterLimit;
            pen2.DashCap = (DXDashCap) pen.DashCap;
            if (pen.DashStyle != DashStyle.Solid)
            {
                pen2.DashPattern = pen.DashPattern;
            }
            pen2.DashStyle = (DXDashStyle) pen.DashStyle;
            pen2.DashOffset = pen.DashOffset;
            pen2.LineJoin = (DXLineJoin) pen.LineJoin;
            pen2.CustomStartCap = (startCap == LineCap.Custom) ? CreateCustomCap(pen.CustomStartCap) : null;
            pen2.CustomEndCap = (endCap == LineCap.Custom) ? CreateCustomCap(pen.CustomEndCap) : null;
            return pen2;
        }

        public static PointF[] ConvertPoints(DXPointF[] source)
        {
            Converter<DXPointF, PointF> converter = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Converter<DXPointF, PointF> local1 = <>c.<>9__7_0;
                converter = <>c.<>9__7_0 = point => new PointF(point.X, point.Y);
            }
            return Array.ConvertAll<DXPointF, PointF>(source, converter);
        }

        public static byte[] ConvertPointTypes(DXPathPointTypes[] types)
        {
            Converter<DXPathPointTypes, byte> converter = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Converter<DXPathPointTypes, byte> local1 = <>c.<>9__8_0;
                converter = <>c.<>9__8_0 = type => (byte) type;
            }
            return Array.ConvertAll<DXPathPointTypes, byte>(types, converter);
        }

        public static RectangleF ConvertRectangle(DXRectangleF rectangle) => 
            new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

        private static DXCustomLineCap CreateCustomCap(CustomLineCap customLineCap)
        {
            AdjustableArrowCap cap = customLineCap as AdjustableArrowCap;
            return ((cap != null) ? new DXCustomLineCap((double) cap.Width, (double) cap.Height, (double) cap.WidthScale, cap.Filled) : null);
        }

        private static DXLinearGradientBrush CreateFromLinearGradientBrush(LinearGradientBrush brush)
        {
            RectangleF rectangle = brush.Rectangle;
            DXLinearGradientBrush brush2 = new DXLinearGradientBrush(new DXRectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height), ConvertColor(brush.LinearColors[0]), ConvertColor(brush.LinearColors[1])) {
                WrapMode = (DXWrapMode) brush.WrapMode
            };
            using (Matrix matrix = brush.Transform)
            {
                brush2.Transform = ConvertMatrix(matrix);
            }
            if (brush.Blend != null)
            {
                Converter<float, double> converter = <>c.<>9__2_1;
                if (<>c.<>9__2_1 == null)
                {
                    Converter<float, double> local2 = <>c.<>9__2_1;
                    converter = <>c.<>9__2_1 = p => (double) p;
                }
                brush2.Blend = new DXBlend(Array.ConvertAll<float, double>(brush.Blend.Positions, converter), Array.ConvertAll<float, double>(brush.Blend.Factors, <>c.<>9__2_2 ??= p => ((double) p)));
            }
            else
            {
                DXColorBlend blend = new DXColorBlend();
                ColorBlend interpolationColors = brush.InterpolationColors;
                Converter<float, double> converter = <>c.<>9__2_0;
                if (<>c.<>9__2_0 == null)
                {
                    Converter<float, double> local1 = <>c.<>9__2_0;
                    converter = <>c.<>9__2_0 = p => (double) p;
                }
                blend.Positions = Array.ConvertAll<float, double>(interpolationColors.Positions, converter);
                blend.Colors = Array.ConvertAll<Color, ARGBColor>(interpolationColors.Colors, new Converter<Color, ARGBColor>(PdfGDIPlusGraphicsConverter.ConvertColor));
                brush2.InterpolationColors = blend;
            }
            return brush2;
        }

        [SecuritySafeCritical]
        private static DXPathGradientBrush CreateFromPathGradientBrush(PathGradientBrush brush)
        {
            DXPathGradientBrush brush2 = null;
            using (Bitmap bitmap = new Bitmap(1, 1))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    DXPathGradientBrush brush4;
                    IntPtr hdc = graphics.GetHdc();
                    using (Metafile metafile = new Metafile(new MemoryStream(), hdc, EmfType.EmfPlusOnly))
                    {
                        using (Graphics graphics2 = Graphics.FromImage(metafile))
                        {
                            graphics2.FillRectangle(brush, new Rectangle(0, 0, 100, 100));
                        }
                        EmfMetafile metafile2 = ConvertMetafile(metafile);
                        if (metafile2 != null)
                        {
                            using (IEnumerator<EmfRecord> enumerator = metafile2.Records.GetEnumerator())
                            {
                                while (true)
                                {
                                    if (!enumerator.MoveNext())
                                    {
                                        break;
                                    }
                                    EmfRecord current = enumerator.Current;
                                    EmfCommentEmfPlusRecord record2 = current as EmfCommentEmfPlusRecord;
                                    if (record2 != null)
                                    {
                                        using (IEnumerator<EmfPlusRecord> enumerator2 = record2.Records.GetEnumerator())
                                        {
                                            while (true)
                                            {
                                                if (!enumerator2.MoveNext())
                                                {
                                                    break;
                                                }
                                                EmfPlusRecord record3 = enumerator2.Current;
                                                EmfPlusObjectRecord record4 = record3 as EmfPlusObjectRecord;
                                                if (record4 != null)
                                                {
                                                    EmfPlusBrush brush3 = record4.Value as EmfPlusBrush;
                                                    if (brush3 != null)
                                                    {
                                                        brush2 = brush3.Brush as DXPathGradientBrush;
                                                        continue;
                                                    }
                                                    return null;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        goto TR_0010;
                    }
                    return brush4;
                TR_0010:
                    graphics.ReleaseHdc(hdc);
                    return brush2;
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfGDIPlusGraphicsConverter.<>c <>9 = new PdfGDIPlusGraphicsConverter.<>c();
            public static Converter<float, double> <>9__2_0;
            public static Converter<float, double> <>9__2_1;
            public static Converter<float, double> <>9__2_2;
            public static Converter<DXPointF, PointF> <>9__7_0;
            public static Converter<DXPathPointTypes, byte> <>9__8_0;

            internal PointF <ConvertPoints>b__7_0(DXPointF point) => 
                new PointF(point.X, point.Y);

            internal byte <ConvertPointTypes>b__8_0(DXPathPointTypes type) => 
                (byte) type;

            internal double <CreateFromLinearGradientBrush>b__2_0(float p) => 
                (double) p;

            internal double <CreateFromLinearGradientBrush>b__2_1(float p) => 
                (double) p;

            internal double <CreateFromLinearGradientBrush>b__2_2(float p) => 
                (double) p;
        }
    }
}

