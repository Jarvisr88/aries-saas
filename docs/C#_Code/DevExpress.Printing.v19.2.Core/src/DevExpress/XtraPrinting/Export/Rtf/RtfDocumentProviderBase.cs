namespace DevExpress.XtraPrinting.Export.Rtf
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.TextRotation;
    using DevExpress.XtraPrinting.Shape;
    using DevExpress.XtraRichEdit.Export.Rtf;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public abstract class RtfDocumentProviderBase : RtfExportProviderBase, IRtfExportProvider, ITableExportProvider
    {
        private static readonly string newLine;
        private readonly DevExpress.XtraPrinting.Native.RtfExportContext rtfExportContext;
        private string pageNumberingInfo;

        static RtfDocumentProviderBase()
        {
            char[] chArray1 = new char[] { '\r', '\n' };
            newLine = new string(chArray1);
        }

        protected RtfDocumentProviderBase(Stream stream, DevExpress.XtraPrinting.Native.RtfExportContext rtfExportContext) : base(stream, rtfExportContext.RtfExportHelper)
        {
            this.rtfExportContext = rtfExportContext;
        }

        private static void AppendNonZeroValue(StringBuilder sb, string tag, int value)
        {
            if (value != 0)
            {
                sb.Append(string.Format(tag, value));
            }
        }

        protected static int ConvertToTwips(float value, float fromDpi) => 
            (int) GraphicsUnitConverter.Convert(value, fromDpi, (float) 1440f);

        public virtual void CreateDocument()
        {
            this.GetContent();
            base.Commit();
        }

        void IRtfExportProvider.SetAnchor(string anchorName)
        {
            if (!string.IsNullOrEmpty(anchorName))
            {
                this.SetContent(string.Format(RtfTags.Bookmark, anchorName));
            }
        }

        void IRtfExportProvider.SetAngle(float angle)
        {
            this.SetAngle(angle);
        }

        void IRtfExportProvider.SetContent(string content)
        {
            this.SetContent(content);
        }

        void IRtfExportProvider.SetPageInfo(PageInfo pageInfo, int startPageNumber, string text, string hyperLink, bool hasCrossReference)
        {
            if (pageInfo > PageInfo.RomHiNumber)
            {
                if (pageInfo == PageInfo.UserName)
                {
                    this.SetContent(string.Format(RtfTags.FieldInstructionTemplate, RtfTags.FieldInstructionUserName, text));
                    return;
                }
                if (pageInfo == PageInfo.Total)
                {
                    this.SetContent(string.Format(RtfTags.FieldInstructionTemplate, RtfTags.FieldInstructionPageCount, "#"));
                    return;
                }
                goto TR_0001;
            }
            else
            {
                switch (pageInfo)
                {
                    case PageInfo.Number:
                    case PageInfo.RomLowNumber:
                        goto TR_0003;

                    case PageInfo.NumberOfTotal:
                        this.SetContent(string.Format(RtfTags.FieldInstructionTemplate, RtfTags.FieldInstructionPageNumber, "#") + "{/}" + string.Format(RtfTags.FieldInstructionTemplate, RtfTags.FieldInstructionPageCount, "#"));
                        return;

                    case (PageInfo.None | PageInfo.Number | PageInfo.NumberOfTotal):
                        break;

                    default:
                        if (pageInfo != PageInfo.RomHiNumber)
                        {
                            break;
                        }
                        goto TR_0003;
                }
                goto TR_0001;
            }
            goto TR_0003;
        TR_0001:
            this.SetCellTextWithUrl(text, hyperLink, hasCrossReference);
            return;
        TR_0003:
            this.SetContent(string.Format(RtfTags.FieldInstructionTemplate, RtfTags.FieldInstructionPageNumber, "#"));
            this.pageNumberingInfo = (pageInfo != PageInfo.RomHiNumber) ? ((pageInfo != PageInfo.RomLowNumber) ? @"\pgndec" : @"\pgnlcrm") : @"\pgnucrm";
            if (startPageNumber > 1)
            {
                this.pageNumberingInfo = this.pageNumberingInfo + $"\pgnstarts{startPageNumber}\pgnrestart\";
            }
        }

        void ITableExportProvider.SetCellImage(System.Drawing.Image image, TableCellImageInfo imageInfo, Rectangle bounds, PaddingInfo padding)
        {
            if (image != null)
            {
                System.Drawing.Image image2 = (this.CurrentData.TableCell is ImageBrick) ? ((ImageBrick) this.CurrentData.TableCell).Image : null;
                ImageFormat imageFormat = (!(image is Bitmap) || !(image2 is Bitmap)) ? image.RawFormat : image2.RawFormat;
                string content = GetRtfImageContent(image, imageFormat, padding);
                if (!string.IsNullOrEmpty(this.CurrentData.TableCell.Url))
                {
                    content = MakeHyperlink(content, this.CurrentData.TableCell.Url, this.CurrentData.TableCell.HasCrossReference);
                }
                this.OverrideFontSizeToPreventIncorrectImageAlignment();
                this.SetImageContent(content);
            }
        }

        void ITableExportProvider.SetCellShape(ShapeBase shape, TableCellLineInfo lineInfo, Color fillColor, int angle, PaddingInfo padding)
        {
        }

        protected int GetBackColorIndex() => 
            base.rtfExportHelper.GetColorIndex(FormattedTextExportHelper.GetBackColor(this.CurrentStyle.BackColor, this.PageColor));

        protected int GetBorderColorIndex() => 
            base.rtfExportHelper.GetColorIndex(FormattedTextExportHelper.GetBorderColor(this.CurrentStyle.BorderColor, this.PageColor));

        protected abstract void GetContent();
        protected int GetFakeBorderColorIndex() => 
            base.rtfExportHelper.GetColorIndex(FormattedTextExportHelper.GetBorderColor(this.CurrentStyle.BackColor, this.PageColor));

        protected string GetFontString()
        {
            Font font = this.CurrentStyle.Font;
            string str = string.Empty;
            if (font.Strikeout)
            {
                str = str + RtfTags.Strikeout;
            }
            if (font.Underline)
            {
                str = str + RtfTags.Underline;
            }
            if (font.Italic)
            {
                str = str + RtfTags.Italic;
            }
            if (font.Bold)
            {
                str = str + RtfTags.Bold;
            }
            return (str + string.Format(RtfTags.FontWithSize, base.rtfExportHelper.GetFontNameIndex(font.Name, RtfThemeFontType.None), RtfExportHelper.GetFontSize(font)));
        }

        protected int GetForeColorIndex() => 
            base.rtfExportHelper.GetColorIndex(FormattedTextExportHelper.GetForeColor(this.CurrentStyle.ForeColor, this.CurrentStyle.BackColor, this.PageColor));

        public virtual Rectangle GetFrameBounds() => 
            Rectangle.Empty;

        public virtual PrintingParagraphAppearance GetParagraphAppearance() => 
            new PrintingParagraphAppearance();

        public static string GetRtfImageContent(System.Drawing.Image image) => 
            GetRtfImageContent(image, image.RawFormat);

        public static string GetRtfImageContent(System.Drawing.Image image, PaddingInfo padding) => 
            GetRtfImageContent(image, image.RawFormat, padding);

        public static string GetRtfImageContent(System.Drawing.Image image, ImageFormat imageFormat) => 
            GetRtfImageContent(image, imageFormat, PaddingInfo.Empty);

        private static string GetRtfImageContent(System.Drawing.Image image, ImageFormat imageFormat, PaddingInfo padding)
        {
            if (image == null)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            byte[] buffer = PSConvert.ImageToArray(image, imageFormat);
            if (buffer.Length == 0)
            {
                return string.Empty;
            }
            WriteTagsToStringBuilder(GraphicsUnitConverter.Convert(image.Width, image.HorizontalResolution, 1440f), GraphicsUnitConverter.Convert(image.Height, image.VerticalResolution, 1440f), GraphicsUnitConverter.Convert(padding.Top, padding.Dpi, 1440f), GraphicsUnitConverter.Convert(padding.Bottom, padding.Dpi, 1440f), GraphicsUnitConverter.Convert(padding.Left, padding.Dpi, 1440f), GraphicsUnitConverter.Convert(padding.Right, padding.Dpi, 1440f), sb, imageFormat);
            WriteImageToStringBuilder(buffer, sb);
            return sb.ToString();
        }

        internal static bool IsSpecialSymbol(char ch) => 
            (ch == '{') || ((ch == '}') || (ch == '\\'));

        protected static string MakeHyperlink(string content, string hyperLink, bool hasCrossReference)
        {
            MakeStringUnicodeCompatible(ref hyperLink);
            hyperLink = hyperLink.Replace(@"\", @"\\");
            return (!hasCrossReference ? string.Format(RtfTags.Hyperlink, hyperLink, content) : string.Format(RtfTags.LocalHyperlink, hyperLink, content));
        }

        public static void MakeLineBreaks(ref string stringData)
        {
            stringData = stringData.Replace(newLine, RtfTags.EndOfLine);
        }

        public static void MakeStringUnicodeCompatible(ref string stringData)
        {
            int startIndex = 0;
            while (startIndex < stringData.Length)
            {
                char ch = stringData[startIndex];
                ushort num2 = Convert.ToUInt16(ch);
                if (num2 > 0x7f)
                {
                    string str = string.Format(RtfTags.UnicodeCharacter + "  ", num2);
                    stringData = stringData.Insert(startIndex + 1, str);
                    stringData = stringData.Remove(startIndex, 1);
                    startIndex += str.Length;
                    continue;
                }
                if (IsSpecialSymbol(ch))
                {
                    stringData = stringData.Insert(startIndex, @"\");
                    startIndex++;
                }
                startIndex++;
            }
        }

        protected virtual void OverrideFontSizeToPreventDisappearBottomBorder()
        {
            if (this.CurrentData.Bounds.Height <= 6)
            {
                this.SetContent(string.Format(RtfTags.FontSize, 1));
            }
        }

        protected void OverrideFontSizeToPreventIncorrectImageAlignment()
        {
            this.SetContent(string.Format(RtfTags.FontSize, 1));
        }

        protected abstract void SetAngle(float angle);
        public abstract void SetCellStyle();
        public void SetCellText(object textValue)
        {
            this.SetCellTextWithUrl(textValue as string, null, false);
        }

        public void SetCellTextWithUrl(string text, string hyperLink, bool hasCrossReference)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (!this.CurrentStyle.StringFormat.FormatFlags.HasFlag(StringFormatFlags.MeasureTrailingSpaces))
                {
                    text = text.TrimEnd(new char[0]);
                }
                text = HotkeyPrefixHelper.PreprocessHotkeyPrefixesInString(text, this.CurrentStyle);
                MakeStringUnicodeCompatible(ref text);
                if (!string.IsNullOrEmpty(hyperLink))
                {
                    text = MakeHyperlink(text, hyperLink, hasCrossReference);
                }
                MakeLineBreaks(ref text);
            }
            this.SetFrameText(text);
        }

        protected abstract void SetCellUnion();
        protected abstract void SetContent(string content);
        protected void SetCurrentCell()
        {
            if (this.CurrentStyle != null)
            {
                this.SetCellStyle();
            }
            this.OverrideFontSizeToPreventDisappearBottomBorder();
            ((BrickExporter) this.ExportContext.PrintingSystem.ExportersFactory.GetExporter(this.CurrentData.TableCell)).FillRtfTableCell(this);
            this.SetCellUnion();
        }

        protected void SetDirection()
        {
            if (this.RightToLeft)
            {
                this.SetContent(RtfTags.RightToLeftParagraph);
            }
        }

        protected abstract void SetFrameText(string text);
        protected abstract void SetImageContent(string content);
        protected abstract override void WriteContent();
        protected virtual void WriteDocumentHeaderFooter()
        {
        }

        private void WriteDXVersionInfo()
        {
            base.writer.WriteLine(string.Format(RtfTags.DXVersionInfo, "19.2.9.0"));
        }

        protected override void WriteHeader()
        {
            base.WriteHeader();
            this.WriteDXVersionInfo();
            this.WritePageBackColor();
            this.WriteSpecialInstructions();
        }

        private static void WriteImageToStringBuilder(byte[] buffer, StringBuilder sb)
        {
            if (buffer != null)
            {
                StringBuilder builder = new StringBuilder(0x100);
                int length = buffer.Length;
                int index = 0;
                int num3 = 0;
                int num4 = 0;
                num3 = 0;
                while (num3 < length)
                {
                    num4 = 0;
                    while (true)
                    {
                        if ((num4 >= 0x40) || ((num3 + num4) >= length))
                        {
                            sb.Append(builder.ToString());
                            builder.Length = 0;
                            sb.Append(RtfTags.NewLine);
                            num3 += 0x40;
                            break;
                        }
                        builder.AppendFormat("{0:X2}", buffer[index]);
                        index++;
                        num4++;
                    }
                }
                sb.Append('}');
            }
        }

        protected abstract void WriteMargins();
        private void WritePageBackColor()
        {
            if (!DXColor.IsTransparentColor(this.PageColor))
            {
                base.writer.Write(RtfTags.ShowBackgroundShape);
                base.writer.Write(RtfTags.DocumentBackground);
                base.writer.Write(RtfTags.Shape);
                base.writer.Write(RtfTags.ShapeInstall);
                base.writer.Write(string.Format(RtfTags.ShapeColor, DXColor.ToWin32(FormattedTextExportHelper.GetBackColor(this.PageColor, Color.White))));
                base.writer.WriteLine("}}}}");
            }
        }

        protected abstract void WritePageBounds();
        protected virtual void WritePageNumberingInfo()
        {
        }

        private void WriteSpecialInstructions()
        {
            base.writer.WriteLine(RtfTags.DontUseLineBreakForAsianText);
            base.writer.WriteLine(RtfTags.DefaultDocumentCharacterProperties);
            base.writer.WriteLine(RtfTags.DefaultDocumentParagraphProperties);
        }

        private static void WriteTagsToStringBuilder(int picwgoal, int pichgoal, int piccropt, int piccropb, int piccropl, int piccropr, StringBuilder sb, ImageFormat imageFormat)
        {
            string str = ReferenceEquals(imageFormat, ImageFormat.Wmf) ? string.Format(RtfTags.WindowsMetafile, 8) : RtfTags.PngPictType;
            sb.Append(string.Format("{{" + RtfTags.Picture + RtfTags.NewLine, str));
            sb.Append(string.Format(RtfTags.PictureDesiredWidth, picwgoal));
            sb.Append(string.Format(RtfTags.PictureDesiredHeight, pichgoal));
            AppendNonZeroValue(sb, RtfTags.PictureCropTop, piccropt);
            AppendNonZeroValue(sb, RtfTags.PictureCropBottom, piccropb);
            AppendNonZeroValue(sb, RtfTags.PictureCropLeft, piccropl);
            AppendNonZeroValue(sb, RtfTags.PictureCropRight, piccropr);
            sb.Append(RtfTags.NewLine);
        }

        protected string PageNumberingInfo =>
            this.pageNumberingInfo;

        protected DevExpress.XtraPrinting.RtfExportOptions RtfExportOptions =>
            this.rtfExportContext.RtfExportOptions;

        public DevExpress.XtraPrinting.Native.ExportContext ExportContext =>
            this.rtfExportContext;

        public abstract BrickViewData CurrentData { get; }

        protected BrickStyle CurrentStyle =>
            this.CurrentData.Style;

        protected float BorderWidth =>
            (float) ConvertToTwips(this.CurrentStyle.BorderWidth, 96f);

        protected BorderDashStyle BorderStyle =>
            this.CurrentStyle.BorderDashStyle;

        protected bool RightToLeft =>
            this.CurrentStyle.StringFormat.RightToLeft;

        protected bool RightToLeftLayout =>
            this.rtfExportContext.RightToLeftLayout;

        protected Color PageColor =>
            this.ExportContext.PrintingSystem.Graph.PageBackColor;

        public DevExpress.XtraPrinting.Native.RtfExportContext RtfExportContext =>
            this.rtfExportContext;

        protected class ImageWatermarkRtfExportProvider : RtfDocumentProviderBase.WatermarkRtfExportProviderBase
        {
            private Size pictureSizeInPixels;
            private Point pictureLocationInPixels;

            public ImageWatermarkRtfExportProvider(Page page) : this(page.ActualWatermark, page.PageData)
            {
            }

            public ImageWatermarkRtfExportProvider(PageWatermark actualWatermark, ReadonlyPageData pageData) : base(actualWatermark, pageData, new Point(pageData.Margins.Left, pageData.Margins.Top))
            {
            }

            private System.Drawing.Image DrawImage() => 
                (!base.Watermark.ImageTiling || (base.Watermark.ImageViewMode == ImageViewMode.Stretch)) ? ((System.Drawing.Image) this.WatermarkImage.Clone()) : ImageHelper.CreateTileImage(this.WatermarkImage, base.PageSize, base.Watermark.ImageViewMode == ImageViewMode.Zoom);

            protected override Rectangle GetBounds(Measurer measurer)
            {
                this.pictureLocationInPixels = Point.Empty;
                if (!base.Watermark.ImageTiling && (base.Watermark.ImageViewMode != ImageViewMode.Stretch))
                {
                    this.SetNoStretchBounds();
                }
                else
                {
                    this.pictureSizeInPixels = base.PageSize;
                }
                this.pictureLocationInPixels.Offset(-base.TopLeftMarginPoint.X, -base.TopLeftMarginPoint.Y);
                return new Rectangle(this.PictureLocationInTwips, this.PictureSizeInTwips);
            }

            private void GetClipingBounds()
            {
                int width = GraphicsUnitConverter.Convert(this.WatermarkImage.Width, this.WatermarkImage.HorizontalResolution, 96f);
                this.pictureSizeInPixels = new Size(width, GraphicsUnitConverter.Convert(this.WatermarkImage.Height, this.WatermarkImage.VerticalResolution, 96f));
                Rectangle rect = new Rectangle(this.pictureLocationInPixels, this.pictureSizeInPixels);
                Rectangle baseRect = new Rectangle(Point.Empty, base.PageSize);
                this.pictureLocationInPixels = RectHelper.AlignRectangle(rect, baseRect, base.Watermark.ImageAlign).Location;
            }

            private void GetZoomBounds()
            {
                this.pictureSizeInPixels = this.GetZoomSize();
                int x = (this.pictureSizeInPixels.Height != base.PageSize.Height) ? 0 : (this.BottomRightLocation.X / 2);
                this.pictureLocationInPixels = new Point(x, (this.pictureSizeInPixels.Width != base.PageSize.Width) ? 0 : (this.BottomRightLocation.Y / 2));
            }

            private Size GetZoomSize()
            {
                int num = GraphicsUnitConverter.Convert(this.WatermarkImage.Width, this.WatermarkImage.HorizontalResolution, 96f);
                int num2 = GraphicsUnitConverter.Convert(this.WatermarkImage.Height, this.WatermarkImage.VerticalResolution, 96f);
                float num3 = ((float) base.PageSize.Width) / ((float) num);
                float num4 = ((float) base.PageSize.Height) / ((float) num2);
                return (((num * num4) > base.PageSize.Width) ? new Size(base.PageSize.Width, (int) (num2 * num3)) : new Size((int) (num * num4), base.PageSize.Height));
            }

            protected override void SetIndividualWatermarkSettings()
            {
                using (System.Drawing.Image image = this.DrawImage())
                {
                    base.Content.AppendFormat(RtfTags.ShapePicture, RtfDocumentProviderBase.GetRtfImageContent(image));
                }
            }

            private void SetNoStretchBounds()
            {
                if (base.Watermark.ImageViewMode == ImageViewMode.Clip)
                {
                    this.GetClipingBounds();
                }
                else
                {
                    this.GetZoomBounds();
                }
            }

            protected override void SetShapeType()
            {
                base.Content.Append(RtfTags.ImageShape);
            }

            private Size PictureSizeInTwips =>
                GraphicsUnitConverter.Convert(this.pictureSizeInPixels, (float) 96f, (float) 1440f);

            private Point PictureLocationInTwips =>
                GraphicsUnitConverter.Convert(this.pictureLocationInPixels, (float) 96f, (float) 1440f);

            private Point BottomRightLocation =>
                new Point(base.PageSize.Width - this.pictureSizeInPixels.Width, base.PageSize.Height - this.pictureSizeInPixels.Height);

            private System.Drawing.Image WatermarkImage =>
                ImageSource.GetImage(base.Watermark.ActualImageSource);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PrintingParagraphAppearance
        {
            public Rectangle Bounds;
            public BorderSide BorderSides;
            public int BorderWidth;
            public int BorderColorIndex;
            public int BackgroundColorIndex;
            public PrintingParagraphAppearance(Rectangle bounds, BorderSide borderSides, int borderWidth, int borderColorIndex, int backgroundColorIndex)
            {
                this.Bounds = bounds;
                this.BorderSides = borderSides;
                this.BorderWidth = borderWidth;
                this.BorderColorIndex = borderColorIndex;
                this.BackgroundColorIndex = backgroundColorIndex;
            }
        }

        protected class TextWatermarkRtfExportProvider : RtfDocumentProviderBase.WatermarkRtfExportProviderBase
        {
            private float angle;

            public TextWatermarkRtfExportProvider(Page page) : this(page.ActualWatermark, page.PageData)
            {
            }

            public TextWatermarkRtfExportProvider(PageWatermark actualWatermark, ReadonlyPageData pageData) : base(actualWatermark, pageData, new Point(pageData.Margins.Left, pageData.Margins.Top))
            {
            }

            private void CalculateAngle()
            {
                switch (base.Watermark.TextDirection)
                {
                    case DirectionMode.Horizontal:
                        this.angle = 0f;
                        return;

                    case DirectionMode.ForwardDiagonal:
                        this.angle = 310f;
                        return;

                    case DirectionMode.BackwardDiagonal:
                        this.angle = 50f;
                        return;

                    case (DirectionMode.BackwardDiagonal | DirectionMode.ForwardDiagonal):
                        break;

                    case DirectionMode.Vertical:
                        this.angle = 270f;
                        break;

                    default:
                        return;
                }
            }

            protected override Rectangle GetBounds(Measurer measurer)
            {
                this.CalculateAngle();
                using (RotatedTextHelper helper = new RotatedTextHelper(base.Watermark.Font, base.Watermark.StringFormat, base.Watermark.Text))
                {
                    RectangleF val = helper.GetBounds(RectangleF.Empty, 0f, DevExpress.XtraPrinting.Native.TextRotation.TextRotation.LeftBottom, 0f, GraphicsUnit.Pixel, measurer);
                    val.Width *= 0.92f;
                    val.Height *= 0.75f;
                    if (((this.angle > 45f) && (this.angle < 135f)) || ((this.angle > 225f) && (this.angle < 315f)))
                    {
                        val.Width = val.Height;
                        val.Height = val.Width;
                    }
                    val.Size = GraphicsUnitConverter.Convert(val.Size, GraphicsDpi.Pixel, (float) 96f);
                    val.Offset((base.PageSize.Width - val.Width) / 2f, (base.PageSize.Height - val.Height) / 2f);
                    val.Offset((float) -base.TopLeftMarginPoint.X, (float) -base.TopLeftMarginPoint.Y);
                    return Rectangle.Ceiling(GraphicsUnitConverter.Convert(val, (float) 96f, (float) 1440f));
                }
            }

            private string GetUnicodeString()
            {
                string text = base.Watermark.Text;
                RtfDocumentProviderBase.MakeStringUnicodeCompatible(ref text);
                return text;
            }

            protected override void SetIndividualWatermarkSettings()
            {
                base.Content.AppendFormat(RtfTags.ShapeRotation, (int) (this.angle * 65536f));
                base.Content.AppendFormat(RtfTags.UnicodeShapeString, this.GetUnicodeString());
                base.Content.AppendFormat(RtfTags.ShapeFont, base.Watermark.Font.Name);
                base.Content.AppendFormat(RtfTags.ShapeColor, DXColor.ToWin32(base.Watermark.ForeColor));
                base.Content.AppendFormat(RtfTags.ShapeBoldText, base.Watermark.Font.Bold ? 1 : 0);
                base.Content.AppendFormat(RtfTags.ShapeItalicText, base.Watermark.Font.Italic ? 1 : 0);
                base.Content.AppendFormat(RtfTags.ShapeOpacity, 0x10000 - (base.Watermark.TextTransparency * 0x100));
            }

            protected override void SetShapeType()
            {
                base.Content.Append(RtfTags.TextShape);
            }
        }

        protected abstract class WatermarkRtfExportProviderBase
        {
            private readonly StringBuilder content = new StringBuilder();
            private readonly Point topLeftMarginPoint;
            private readonly ReadonlyPageData pageData;
            private readonly PageWatermark actualWatermark;

            public WatermarkRtfExportProviderBase(PageWatermark actualWatermark, ReadonlyPageData pageData, Point topLeftMarginPoint)
            {
                this.actualWatermark = actualWatermark;
                this.pageData = pageData;
                this.topLeftMarginPoint = topLeftMarginPoint;
            }

            protected void AppendBounds(Rectangle bounds)
            {
                object[] args = new object[] { bounds.Left, bounds.Top, bounds.Right, bounds.Bottom };
                this.Content.AppendFormat(RtfTags.ShapeBounds, args);
            }

            protected abstract Rectangle GetBounds(Measurer measurer);
            public string GetRtfContent(ExportContext exportContext)
            {
                this.SetShapeHeader();
                this.AppendBounds(this.GetBounds(exportContext.Measurer));
                this.SetIndividualWatermarkSettings();
                this.SetBehindWatermark();
                return this.Content.ToString();
            }

            protected void SetBehindWatermark()
            {
                this.Content.AppendFormat(RtfTags.ShapeBehind, this.Watermark.ShowBehind ? 1 : 0);
                this.Content.Append("}}");
            }

            protected abstract void SetIndividualWatermarkSettings();
            protected void SetShapeHeader()
            {
                this.Content.Append(RtfTags.Shape);
                this.Content.Append(RtfTags.ShapeInstall);
                this.Content.Append(RtfTags.NoneWrapShape);
                this.Content.Append(RtfTags.NoShapeBorderLine);
                this.SetShapeType();
            }

            protected abstract void SetShapeType();

            protected PageWatermark Watermark =>
                this.actualWatermark;

            protected ReadonlyPageData PageData =>
                this.pageData;

            protected Point TopLeftMarginPoint =>
                this.topLeftMarginPoint;

            protected Size PageSize =>
                GraphicsUnitConverter.Convert(this.PageData.Landscape ? new Size(this.PageData.Size.Height, this.PageData.Size.Width) : this.PageData.Size, (float) 100f, (float) 96f);

            protected StringBuilder Content =>
                this.content;
        }
    }
}

