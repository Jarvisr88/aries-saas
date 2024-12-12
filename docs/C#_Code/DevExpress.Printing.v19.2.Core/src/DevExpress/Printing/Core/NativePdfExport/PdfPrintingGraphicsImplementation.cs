namespace DevExpress.Printing.Core.NativePdfExport
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Text.Fonts;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;

    internal class PdfPrintingGraphicsImplementation : PdfGraphicsImplementation
    {
        private readonly PdfStringFormat textDrawFormat;
        private readonly ProgressReflector progressReflector;

        public PdfPrintingGraphicsImplementation(Stream stream, PdfExportOptions exportOptions, ProgressReflector progressReflector, bool rightToLeftLayout) : base(new PdfExportDocument(stream, exportOptions, rightToLeftLayout))
        {
            this.textDrawFormat = (PdfStringFormat) PdfStringFormat.GenericTypographic.Clone();
            this.progressReflector = progressReflector;
        }

        private void ApplyStringFromat(StringFormat validFormat)
        {
            float num;
            this.textDrawFormat.Alignment = ConvertAlignment(validFormat.Alignment);
            this.textDrawFormat.LineAlignment = ConvertAlignment(validFormat.LineAlignment);
            float[] tabStops = validFormat.GetTabStops(out num);
            num = (tabStops.Length != 0) ? tabStops[0] : 0f;
            this.textDrawFormat.TabStopInterval = num;
            this.textDrawFormat.DirectionRightToLeft = (validFormat.FormatFlags & StringFormatFlags.DirectionRightToLeft) != 0;
            this.textDrawFormat.FormatFlags = ConvertFlags(validFormat.FormatFlags);
        }

        private void ApplyTransform(float x, float y)
        {
            base.TranslateTransform(-x, -y, MatrixOrder.Append);
            base.RotateTransform(90f, MatrixOrder.Append);
            base.TranslateTransform(x, y, MatrixOrder.Append);
        }

        private static float CalculateCenterX(StringAlignment alignment, RectangleF bounds)
        {
            switch (alignment)
            {
                case StringAlignment.Near:
                    return bounds.Left;

                case StringAlignment.Center:
                    return (bounds.Left + (bounds.Width / 2f));

                case StringAlignment.Far:
                    return bounds.Right;
            }
            throw new ArgumentException();
        }

        private static float CalculateCenterY(StringAlignment lineAlignment, RectangleF bounds)
        {
            switch (lineAlignment)
            {
                case StringAlignment.Near:
                    return bounds.Top;

                case StringAlignment.Center:
                    return (bounds.Top + (bounds.Height / 2f));

                case StringAlignment.Far:
                    return bounds.Bottom;
            }
            throw new ArgumentException();
        }

        public override void ClosePage()
        {
            base.ClosePage();
            this.progressReflector.RangeValue++;
        }

        private static PdfStringAlignment ConvertAlignment(StringAlignment alignment) => 
            (alignment == StringAlignment.Center) ? PdfStringAlignment.Center : ((alignment == StringAlignment.Far) ? PdfStringAlignment.Far : PdfStringAlignment.Near);

        private static PdfStringFormatFlags ConvertFlags(StringFormatFlags formatFlags)
        {
            PdfStringFormatFlags flags = 0;
            if (formatFlags.HasFlag(StringFormatFlags.MeasureTrailingSpaces))
            {
                flags |= PdfStringFormatFlags.MeasureTrailingSpaces;
            }
            if (formatFlags.HasFlag(StringFormatFlags.NoWrap))
            {
                flags |= PdfStringFormatFlags.NoWrap;
            }
            if (formatFlags.HasFlag(StringFormatFlags.LineLimit))
            {
                flags |= PdfStringFormatFlags.LineLimit;
            }
            if (formatFlags.HasFlag(StringFormatFlags.NoClip))
            {
                flags |= PdfStringFormatFlags.NoClip;
            }
            return flags;
        }

        public void DrawString(string s, Font font, Brush brush, RectangleF bounds, StringFormat format, Measurer measurer)
        {
            if (DoNotIgnoreDraw(brush))
            {
                base.ValidateConstructor();
                if (s == null)
                {
                    throw new ArgumentNullException("s");
                }
                if (font == null)
                {
                    throw new ArgumentNullException("font");
                }
                if (brush == null)
                {
                    throw new ArgumentNullException("brush");
                }
                SolidBrush solidBrush = brush as SolidBrush;
                if (solidBrush == null)
                {
                    throw new NotSupportedException("The brush must be solid");
                }
                if ((solidBrush.Color.A != 0) && !bounds.IsEmpty)
                {
                    using (StringFormat format2 = PrepareStringFormat(format))
                    {
                        s = HotkeyPrefixHelper.PreprocessHotkeyPrefixesInString(s, format2.HotkeyPrefix);
                        bool flag = false;
                        if (font.Unit != GraphicsUnit.Point)
                        {
                            font = new Font(font.FontFamily, FontSizeHelper.GetSizeInPoints(font), font.Style);
                            flag = true;
                        }
                        StringFormatFlags formatFlags = format2.FormatFlags;
                        PdfGraphicsClip clip = this.SetStringClipping(bounds, format2);
                        try
                        {
                            if (base.Clip != null)
                            {
                                string actualString = RemoveChar(s, '\r');
                                if ((formatFlags & StringFormatFlags.DirectionVertical) != 0)
                                {
                                    base.SaveTransformState();
                                    this.ApplyTransform(CalculateCenterX(format2.Alignment, bounds), CalculateCenterY(format2.LineAlignment, bounds));
                                }
                                try
                                {
                                    PdfExportFontInfo fontInfo = base.ExportDocument.GetFontInfo(font);
                                    GraphicsBase.EnsureStringFormat(font, bounds, base.PageUnit, format2, delegate (StringFormat validFormat) {
                                        if ((Environment.OSVersion.Platform != PlatformID.Win32NT) && !PdfGraphicsImplementation.UseLegacyFormatterOnLinux)
                                        {
                                            Action <>9__2;
                                            this.ApplyStringFromat(validFormat);
                                            Action action = <>9__2;
                                            if (<>9__2 == null)
                                            {
                                                Action local1 = <>9__2;
                                                action = <>9__2 = delegate {
                                                    PdfGraphicsCommandConstructor commandConstructor = this.CommandConstructor;
                                                    commandConstructor.SetBrush(brush);
                                                    if (fontInfo.ShouldSetStrokingColor)
                                                    {
                                                        commandConstructor.SetUnscaledPen(solidBrush.Color, fontInfo.FontLineSize);
                                                    }
                                                    commandConstructor.DrawString(actualString, fontInfo, bounds, this.textDrawFormat, DXKerningMode.Always);
                                                };
                                            }
                                            this.PerformIsolatedAction(action);
                                        }
                                        else
                                        {
                                            string[] lines = TextFormatter.CreateInstance(this.PageUnit, measurer).FormatMultilineText(actualString, font, bounds.Width, bounds.Height, validFormat);
                                            if (lines.Length != 0)
                                            {
                                                this.ApplyStringFromat(validFormat);
                                                this.textDrawFormat.FormatFlags |= PdfStringFormatFlags.NoWrap;
                                                this.PerformIsolatedAction(delegate {
                                                    PdfGraphicsCommandConstructor commandConstructor = this.CommandConstructor;
                                                    commandConstructor.SetBrush(brush);
                                                    if (fontInfo.ShouldSetStrokingColor)
                                                    {
                                                        commandConstructor.SetUnscaledPen(solidBrush.Color, fontInfo.FontLineSize);
                                                    }
                                                    commandConstructor.DrawFormattedLines(lines, fontInfo, bounds, this.textDrawFormat, this.ClipBounds);
                                                });
                                            }
                                        }
                                    });
                                }
                                finally
                                {
                                    bool flag2;
                                    if (flag2)
                                    {
                                        base.ResetTransform();
                                        base.ApplyTransformState(MatrixOrder.Append, true);
                                    }
                                }
                            }
                        }
                        finally
                        {
                            base.Clip = clip;
                            if (flag)
                            {
                                font.Dispose();
                            }
                        }
                    }
                }
            }
        }

        private static StringFormat PrepareStringFormat(StringFormat format)
        {
            if (format == null)
            {
                return (StringFormat) StringFormat.GenericTypographic.Clone();
            }
            StringFormat format2 = (StringFormat) format.Clone();
            StringAlignment alignment = format2.Alignment;
            StringAlignment lineAlignment = format2.LineAlignment;
            if ((format2.FormatFlags & StringFormatFlags.DirectionVertical) != 0)
            {
                format2.Alignment = lineAlignment;
                format2.LineAlignment = alignment;
            }
            return format2;
        }

        private static string RemoveChar(string str, char ch)
        {
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (str[i] == ch)
                {
                    str = str.Remove(i, 1);
                }
            }
            return str;
        }

        private PdfGraphicsClip SetStringClipping(RectangleF bounds, StringFormat format)
        {
            PdfGraphicsClip clip = base.Clip;
            if (((format.FormatFlags & StringFormatFlags.NoClip) == 0) && PdfGraphicsClip.IsNotRotated(base.Transform))
            {
                base.Clip = clip.Intersect(PdfGraphicsClip.Create(bounds, base.Transform));
            }
            return clip;
        }
    }
}

