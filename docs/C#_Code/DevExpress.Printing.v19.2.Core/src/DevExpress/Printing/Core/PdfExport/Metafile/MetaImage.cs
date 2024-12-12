namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    public class MetaImage
    {
        private Metafile image;
        private PdfDrawContext template;
        private PdfVectorImage owner;
        private PdfGraphicsImpl graphics;
        private Matrix baseTransform;
        private PointF offset;
        private Stack<RectangleF> clipStack;
        private bool hasUnsupportedRecords;
        public static Action startTest;
        public static Func<EmfPlusRecordType, bool> test;

        public MetaImage(PdfVectorImage owner, Metafile image, PdfDrawContext template, PdfGraphicsImpl graphics, PointF offset) : this(owner, image, new MetaState(image), template, graphics, offset)
        {
        }

        internal MetaImage(PdfVectorImage owner, Metafile image, MetaState state, PdfDrawContext template, PdfGraphicsImpl graphics, PointF offset)
        {
            this.clipStack = new Stack<RectangleF>();
            this.graphics = graphics;
            this.owner = owner;
            this.image = image;
            this.template = template;
            this.State = state;
            this.offset = offset;
            this.baseTransform = this.CreateBaseTransform(graphics.PageUnit);
            graphics.SetTransform(this.baseTransform);
            graphics.ClipBounds = this.GetBoundingBox(graphics.PageUnit);
        }

        private void AddTransform(Matrix matrix, bool append)
        {
            if (!append)
            {
                this.graphics.MultiplyTransform(matrix, MatrixOrder.Prepend);
            }
            else
            {
                using (Matrix matrix2 = this.graphics.Transform.Clone())
                {
                    using (Matrix matrix3 = this.baseTransform.Clone())
                    {
                        matrix3.Invert();
                        matrix2.Multiply(matrix3, MatrixOrder.Append);
                        matrix2.Multiply(matrix, MatrixOrder.Append);
                        matrix2.Multiply(this.baseTransform, MatrixOrder.Append);
                        this.graphics.SetTransform(matrix2);
                    }
                }
            }
        }

        private Matrix CreateBaseTransform(GraphicsUnit pageUnit)
        {
            Matrix matrix = new Matrix();
            matrix.Scale(((float) this.State.LogicalDpi.X) / this.State.HorizontalResolution, ((float) this.State.LogicalDpi.Y) / this.State.VerticalResolution);
            matrix.Translate((-this.offset.X * GraphicsDpi.UnitToDpiI(pageUnit)) / ((float) this.State.LogicalDpi.X), (-this.offset.Y * GraphicsDpi.UnitToDpiI(pageUnit)) / ((float) this.State.LogicalDpi.Y));
            return matrix;
        }

        private void DrawImage(Image image, RectangleF source, RectangleF destination)
        {
            this.template.GSave();
            this.template.Rectangle(destination.X, destination.Y, destination.Width, destination.Height);
            this.template.Clip();
            this.template.NewPath();
            this.template.Concat(new Matrix((destination.Width * image.Width) / source.Width, 0f, 0f, (destination.Height * image.Height) / source.Height, destination.X - (source.X / source.Width), destination.Y - (source.Y / source.Height)));
            this.template.ExecuteXObject(this.owner.CreateImage(image));
            this.template.GRestore();
        }

        private void DrawPath(PointF[] points)
        {
            this.template.MoveTo(points[0].X, points[0].Y);
            for (int i = 1; i < points.Length; i++)
            {
                this.template.LineTo(points[i].X, points[i].Y);
            }
        }

        private void DrawUnsupportedRecord(EmfPlusRecordType recordType, MetaReader reader, int flagsRaw, int dataSize)
        {
            this.image.PlayRecord(recordType, flagsRaw, dataSize, reader.ReadToEnd());
        }

        internal void FillAndStroke()
        {
            if ((this.State.CurrentPen == null) || (this.State.CurrentPen.Color == Color.Transparent))
            {
                this.template.ClosePath();
                if (this.State.PolygonFillMode == PolyFillMode.ALTERNATE)
                {
                    this.template.EoFill();
                }
                else
                {
                    this.template.Fill();
                }
            }
            else if ((this.State.CurrentBrush == null) || ((this.State.CurrentBrush is NullBrush) || ((this.State.CurrentBrush is HatchBrush) && (this.State.BackgroundMode == MixMode.OPAQUE))))
            {
                this.template.ClosePathAndStroke();
            }
            else if (this.State.PolygonFillMode == PolyFillMode.ALTERNATE)
            {
                this.template.ClosePathEoFillAndStroke();
            }
            else
            {
                this.template.ClosePathFillAndStroke();
            }
        }

        private RectangleF GetBoundingBox(GraphicsUnit pageUnit)
        {
            double num = GraphicsDpi.UnitToDpiI(pageUnit) / ((float) this.State.LogicalDpi.X);
            double num2 = GraphicsDpi.UnitToDpiI(pageUnit) / ((float) this.State.LogicalDpi.Y);
            return new RectangleF((float) (num * this.offset.X), (float) (num2 * this.offset.Y), (float) (num * this.State.ImageSize.Width), (float) (num2 * this.State.ImageSize.Height));
        }

        private float GetFontSize(EmfPlusFont emfPlusFont)
        {
            GraphicsUnit sizeUnit = (GraphicsUnit) emfPlusFont.SizeUnit;
            return (((sizeUnit >= GraphicsUnit.Point) || (this.graphics.PageUnit < GraphicsUnit.Point)) ? (((sizeUnit < GraphicsUnit.Point) || (this.graphics.PageUnit > GraphicsUnit.Point)) ? GraphicsUnitConverter.Convert(emfPlusFont.EmSize, GraphicsDpi.UnitToDpiI(sizeUnit), (float) 72f) : GraphicsUnitConverter.Convert(GraphicsUnitConverter.Convert(emfPlusFont.EmSize, GraphicsDpi.UnitToDpiI(sizeUnit), (float) this.State.LogicalDpi.X), (float) 96f, (float) 72f)) : GraphicsUnitConverter.Convert(emfPlusFont.EmSize, GraphicsDpi.UnitToDpiI(this.graphics.PageUnit), (float) 72f));
        }

        private Pen GetPenEmfPlus(byte[] flags)
        {
            byte index = flags[0];
            return (Pen) this.State.GetObject(index);
        }

        public static string GetStringData(MetaReader reader, int length)
        {
            byte[] bytes = new byte[length];
            int index = 0;
            while (index < length)
            {
                byte num2 = reader.ReadByte();
                if (num2 != 0)
                {
                    bytes[index] = num2;
                    index++;
                }
            }
            try
            {
                return Encoding.GetEncoding(0x4e4).GetString(bytes, 0, index);
            }
            catch
            {
                return Encoding.ASCII.GetString(bytes, 0, index);
            }
        }

        private static StringFormat GetStringFormat(EmfPlusStringFormat emfPlusStringFormat, bool forceNoWrap)
        {
            StringFormat format;
            if (emfPlusStringFormat == null)
            {
                format = (StringFormat) StringFormat.GenericDefault.Clone();
            }
            else
            {
                if (!emfPlusStringFormat.IsGenericTypographic)
                {
                    format = new StringFormat(emfPlusStringFormat.FormatFlags, emfPlusStringFormat.Language);
                }
                else
                {
                    format = (StringFormat) StringFormat.GenericTypographic.Clone();
                    format.FormatFlags = emfPlusStringFormat.FormatFlags;
                }
                format.Alignment = emfPlusStringFormat.Alignment;
                format.LineAlignment = emfPlusStringFormat.LineAlignment;
                format.Trimming = emfPlusStringFormat.Trimming;
                format.SetDigitSubstitution(emfPlusStringFormat.DigitLanguage, (StringDigitSubstitute) emfPlusStringFormat.DigitSubstitution);
                format.SetTabStops(emfPlusStringFormat.FirstTabOffset, emfPlusStringFormat.TabStops);
                if (emfPlusStringFormat.RangeCount > 0)
                {
                    format.SetMeasurableCharacterRanges(emfPlusStringFormat.CharRange);
                }
            }
            if (forceNoWrap)
            {
                format.FormatFlags |= StringFormatFlags.NoWrap;
            }
            return format;
        }

        public static string GetUnicodeStringData(MetaReader reader, int length)
        {
            byte[] buffer = new byte[2 * length];
            reader.Read(buffer, 0, buffer.Length);
            return Encoding.Unicode.GetString(buffer);
        }

        private bool IsNullStrokeFill()
        {
            bool flag = (this.State.CurrentPen == null) || (this.State.CurrentPen.Color == Color.Transparent);
            bool flag2 = (this.State.CurrentBrush != null) ? ((this.State.CurrentBrush is SolidBrush) || ((this.State.CurrentBrush is HatchBrush) && (this.State.BackgroundMode == MixMode.OPAQUE))) : false;
            return (flag && !flag2);
        }

        [SecuritySafeCritical]
        internal bool MetafileCallback(EmfPlusRecordType recordType, int flags, int dataSize, IntPtr data, PlayRecordCallback callbackData)
        {
            byte[] destination = null;
            if (data != IntPtr.Zero)
            {
                destination = new byte[dataSize];
                Marshal.Copy(data, destination, 0, dataSize);
            }
            MemoryStream stream = (destination != null) ? new MemoryStream(destination) : new MemoryStream();
            this.ProcessRecord(recordType, new MetaReader(stream), flags, dataSize);
            return true;
        }

        private void ProcessEmfPlus_Clear(MetaReader reader)
        {
            using (Brush brush = new SolidBrush(reader.ReadEmfPlusARGB()))
            {
                this.graphics.FillRectangle(brush, this.GetBoundingBox(this.graphics.PageUnit));
            }
        }

        private void ProcessEmfPlus_DrawArc(MetaReader reader, byte[] flags)
        {
            bool compressed = (flags[1] & 0x40) == 0x40;
            Pen penEmfPlus = this.GetPenEmfPlus(flags);
            float startAngle = reader.ReadSingle();
            float sweepAngle = reader.ReadSingle();
            RectangleF rect = reader.ReadRectangles(compressed, 1L)[0];
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(rect, startAngle, sweepAngle);
                this.graphics.DrawPath(penEmfPlus, path);
            }
        }

        private void ProcessEmfPlus_DrawBeziers(MetaReader reader, byte[] flags)
        {
            bool compressed = (flags[1] & 0x40) == 0x40;
            Pen penEmfPlus = this.GetPenEmfPlus(flags);
            uint num = reader.ReadUInt32();
            this.graphics.DrawBeziers(penEmfPlus, this.ReadPoints(reader, (long) num, compressed, (flags[1] & 8) == 8));
        }

        private void ProcessEmfPlus_DrawEllipse(MetaReader reader, byte[] flags)
        {
            bool compressed = (flags[1] & 0x40) == 0x40;
            Pen penEmfPlus = this.GetPenEmfPlus(flags);
            this.graphics.DrawEllipse(penEmfPlus, this.ReadRectangles(reader, 1L, compressed)[0]);
        }

        private void ProcessEmfPlus_DrawImagePoints(MetaReader reader, byte[] flags)
        {
            bool compressed = (flags[1] & 0x40) == 0x40;
            bool flag2 = (flags[1] & 0x20) == 0x20;
            byte index = flags[0];
            uint num2 = reader.ReadUInt32();
            GraphicsUnit unit = (GraphicsUnit) reader.ReadInt32();
            RectangleF rect = reader.ReadRectF();
            uint num3 = reader.ReadUInt32();
            PointF[] tfArray = this.ReadPoints(reader, (long) num3, compressed, (flags[1] & 8) == 8);
            RectangleF bounds = new RectangleF(tfArray[0], new SizeF(tfArray[1].X - tfArray[0].X, tfArray[2].Y - tfArray[0].Y));
            Image original = (Image) this.State.GetObject(index);
            if (original is Metafile)
            {
                Bitmap image = BitmapCreator.CreateBitmapWithResolutionLimit(original, Color.Transparent);
                BufferedGraphicsContext current = BufferedGraphicsManager.Current;
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    BufferedGraphics graphics2 = current.Allocate(graphics, new Rectangle((int) bounds.X, (int) bounds.Y, (int) bounds.Width, (int) bounds.Height));
                    graphics2.Graphics.Clear(Color.White);
                    graphics2.Graphics.DrawImage(original, rect);
                    graphics2.Render();
                }
                original = image;
            }
            this.graphics.DrawImage(original, bounds, rect, Color.Empty);
        }

        private void ProcessEmfPlus_DrawLines(MetaReader reader, byte[] flags)
        {
            bool compressed = (flags[1] & 0x40) == 0x40;
            bool flag2 = (flags[1] & 0x20) == 0x20;
            Pen penEmfPlus = this.GetPenEmfPlus(flags);
            uint num = reader.ReadUInt32();
            this.graphics.DrawLines(penEmfPlus, this.ReadPoints(reader, (long) num, compressed, (flags[1] & 8) == 8));
        }

        private void ProcessEmfPlus_DrawPath(MetaReader reader, byte[] flags)
        {
            byte index = flags[0];
            Pen pen = this.ReadPenEmfPlus(reader);
            this.graphics.DrawPath(pen, (GraphicsPath) this.State.GetObject(index));
        }

        private void ProcessEmfPlus_DrawPie(MetaReader reader, byte[] flags)
        {
            bool compressed = (flags[1] & 0x40) == 0x40;
            Pen penEmfPlus = this.GetPenEmfPlus(flags);
            float startAngle = reader.ReadSingle();
            float sweepAngle = reader.ReadSingle();
            RectangleF ef = reader.ReadRectangles(compressed, 1L)[0];
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPie(ef.X, ef.Y, ef.Width, ef.Height, startAngle, sweepAngle);
                this.graphics.DrawPath(penEmfPlus, path);
            }
        }

        private void ProcessEmfPlus_DrawRects(MetaReader reader, byte[] flags)
        {
            bool compressed = (flags[1] & 0x40) == 0x40;
            Pen penEmfPlus = this.GetPenEmfPlus(flags);
            uint num = reader.ReadUInt32();
            foreach (RectangleF ef in this.ReadRectangles(reader, (long) num, compressed))
            {
                this.graphics.DrawRectangle(penEmfPlus, ef);
            }
        }

        private void ProcessEmfPlus_DrawString(MetaReader reader, byte[] flags)
        {
            byte index = flags[0];
            EmfPlusFont emfPlusFont = (EmfPlusFont) this.State.GetObject(index);
            Brush brush = this.ReadBrushEmfPlus(reader, flags);
            uint num2 = reader.ReadUInt32();
            uint num3 = reader.ReadUInt32();
            RectangleF bounds = reader.ReadRectF();
            string unicodeStringData = GetUnicodeStringData(reader, (int) num3);
            float fontSize = this.GetFontSize(emfPlusFont);
            using (Font font2 = new Font(emfPlusFont.FamilyName, fontSize, emfPlusFont.FontStyleFlags))
            {
                using (StringFormat format = GetStringFormat((EmfPlusStringFormat) this.State.GetObject((int) num2), bounds.IsEmpty))
                {
                    if (bounds.IsEmpty)
                    {
                        this.graphics.DrawString(unicodeStringData, font2, brush, new PointF(bounds.X, bounds.Y), format);
                    }
                    else
                    {
                        this.graphics.DrawString(unicodeStringData, font2, brush, bounds, format);
                    }
                }
            }
        }

        private void ProcessEmfPlus_FillEllipse(MetaReader reader, byte[] flags)
        {
            Brush brush = this.ReadBrushEmfPlus(reader, flags);
            bool compressed = (flags[1] & 0x40) == 0x40;
            this.graphics.FillEllipse(brush, this.ReadRectangles(reader, 1L, compressed)[0]);
        }

        private void ProcessEmfPlus_FillPath(MetaReader reader, byte[] flags)
        {
            byte index = flags[0];
            Brush brush = this.ReadBrushEmfPlus(reader, flags);
            this.graphics.FillPath(brush, (GraphicsPath) this.State.GetObject(index));
        }

        private void ProcessEmfPlus_FillPie(MetaReader reader, byte[] flags)
        {
            bool compressed = (flags[1] & 0x40) == 0x40;
            Brush brush = this.ReadBrushEmfPlus(reader, flags);
            float startAngle = reader.ReadSingle();
            float sweepAngle = reader.ReadSingle();
            RectangleF ef = reader.ReadRectangles(compressed, 1L)[0];
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddPie(ef.X, ef.Y, ef.Width, ef.Height, startAngle, sweepAngle);
                this.graphics.FillPath(brush, path);
            }
        }

        private void ProcessEmfPlus_FillPolygon(MetaReader reader, byte[] flags)
        {
            bool compressed = (flags[1] & 0x40) == 0x40;
            Brush brush = this.ReadBrushEmfPlus(reader, flags);
            uint num = reader.ReadUInt32();
            PointF[] pts = this.ReadPoints(reader, (long) num, compressed, (flags[1] & 8) == 8);
            byte[] types = new byte[] { 0 };
            for (int i = 1; i < types.Length; i++)
            {
                types[i] = 1;
            }
            this.graphics.FillPath(brush, new GraphicsPath(pts, types, FillMode.Alternate));
        }

        private void ProcessEmfPlus_FillRects(MetaReader reader, byte[] flags)
        {
            Brush brush = this.ReadBrushEmfPlus(reader, flags);
            bool compressed = (flags[1] & 0x40) == 0x40;
            uint num = reader.ReadUInt32();
            foreach (RectangleF ef in this.ReadRectangles(reader, (long) num, compressed))
            {
                this.graphics.FillRectangle(brush, ef);
            }
        }

        private void ProcessEmfPlus_Header(MetaReader reader)
        {
            EmfPlusGraphicsVersion version1 = new EmfPlusGraphicsVersion(reader);
            uint num = reader.ReadUInt32();
            uint num2 = reader.ReadUInt32();
            uint num3 = reader.ReadUInt32();
            this.State.LogicalDpi = new Point((int) num2, (int) num2);
        }

        private void ProcessEmfPlus_MultiplyWorldTransform(MetaReader reader, byte[] flags)
        {
            bool append = (flags[1] & 0x20) == 0x20;
            EmfPlusTransformMatrix matrix = new EmfPlusTransformMatrix(reader);
            using (Matrix matrix2 = new Matrix(matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.Dx, matrix.Dy))
            {
                this.AddTransform(matrix2, append);
            }
        }

        private void ProcessEmfPlus_Object(MetaReader reader, byte[] flags)
        {
            bool flag = (flags[1] & 0x80) == 0x80;
            byte index = flags[0];
            object metaObject = null;
            switch ((flags[1] & 0x7f))
            {
                case 1:
                    metaObject = new EmfPlusBrush(reader).Brush;
                    break;

                case 2:
                    metaObject = new EmfPlusPen(reader).Pen;
                    break;

                case 3:
                    metaObject = new EmfPlusPath(reader).Path;
                    break;

                case 4:
                    metaObject = new EmfPlusRegion(reader).Region;
                    break;

                case 5:
                    metaObject = new EmfPlusImage(reader).Image;
                    break;

                case 6:
                    metaObject = new EmfPlusFont(reader, this.owner);
                    break;

                case 7:
                    metaObject = new EmfPlusStringFormat(reader);
                    break;

                default:
                    break;
            }
            this.State.AddObject(index, metaObject);
        }

        private void ProcessEmfPlus_ResetWorldTransform()
        {
            this.graphics.SetTransform(this.baseTransform);
        }

        private void ProcessEmfPlus_Restore(MetaReader reader)
        {
            uint num = reader.ReadUInt32();
            this.graphics.ResetTransform();
            this.graphics.ApplyTransformState(MatrixOrder.Append, true);
            this.graphics.ClipBounds = this.clipStack.Pop();
        }

        private void ProcessEmfPlus_RotateWorldTransform(MetaReader reader, byte[] flags)
        {
            bool append = (flags[1] & 0x20) == 0x20;
            float angle = reader.ReadSingle();
            using (Matrix matrix = new Matrix())
            {
                matrix.Rotate(angle);
                this.AddTransform(matrix, append);
            }
        }

        private void ProcessEmfPlus_Save(MetaReader reader)
        {
            uint num = reader.ReadUInt32();
            this.clipStack.Push(this.graphics.ClipBounds);
            this.graphics.SaveTransformState();
        }

        private void ProcessEmfPlus_ScaleWorldTransform(MetaReader reader, byte[] flags)
        {
            bool append = (flags[1] & 0x20) == 0x20;
            float scaleX = reader.ReadSingle();
            float scaleY = reader.ReadSingle();
            using (Matrix matrix = new Matrix())
            {
                matrix.Scale(scaleX, scaleY);
                this.AddTransform(matrix, append);
            }
        }

        private void ProcessEmfPlus_SetClipPath(byte[] flags)
        {
            byte index = flags[0];
            GraphicsPath path = (GraphicsPath) this.State.GetObject(index);
            this.graphics.SetClip(path, ((CombineMode) flags[1]) & ((CombineMode) 15));
        }

        private void ProcessEmfPlus_SetClipRect(MetaReader reader, byte[] flags)
        {
            RectangleF rect = reader.ReadRectF();
            this.graphics.SetClip(rect, ((CombineMode) flags[1]) & ((CombineMode) 15));
        }

        private void ProcessEmfPlus_SetClipRegion(byte[] flags)
        {
            byte index = flags[0];
            Region region = (Region) this.State.GetObject(index);
            this.graphics.SetClip(region, ((CombineMode) flags[1]) & ((CombineMode) 15));
        }

        private void ProcessEmfPlus_SetPageTransform(MetaReader reader, byte[] flags)
        {
            GraphicsUnit pageUnit = (GraphicsUnit) flags[0];
            Matrix baseTransform = this.baseTransform;
            baseTransform.Invert();
            this.baseTransform = this.CreateBaseTransform(pageUnit);
            baseTransform.Multiply(this.baseTransform, MatrixOrder.Append);
            this.graphics.PageUnit = pageUnit;
            this.graphics.MultiplyTransform(baseTransform, MatrixOrder.Append);
            baseTransform.Dispose();
            float num = reader.ReadSingle();
        }

        private void ProcessEmfPlus_SetWorldTransform(MetaReader reader)
        {
            EmfPlusTransformMatrix matrix = new EmfPlusTransformMatrix(reader);
            Matrix matrix2 = new Matrix(matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.Dx, matrix.Dy);
            matrix2.Multiply(this.baseTransform, MatrixOrder.Append);
            this.graphics.SetTransform(matrix2);
        }

        private void ProcessEmfPlus_TranslateWorldTransform(MetaReader reader, byte[] flags)
        {
            bool append = (flags[1] & 0x20) == 0x20;
            float offsetX = reader.ReadSingle();
            float offsetY = reader.ReadSingle();
            using (Matrix matrix = new Matrix())
            {
                matrix.Translate(offsetX, offsetY);
                this.AddTransform(matrix, append);
            }
        }

        [SecuritySafeCritical]
        public void ProcessRecord(EmfPlusRecordType recordType, MetaReader reader, int flagsRaw = 0, int dataSize = 0)
        {
            byte[] bytes = BitConverter.GetBytes((ushort) flagsRaw);
            if ((test != null) && test(recordType))
            {
                return;
            }
            if (recordType > EmfPlusRecordType.WmfPolygon)
            {
                if (recordType > EmfPlusRecordType.WmfEscape)
                {
                    if (recordType > EmfPlusRecordType.WmfBitBlt)
                    {
                        if (recordType > EmfPlusRecordType.WmfStretchBlt)
                        {
                            if ((recordType == EmfPlusRecordType.WmfDibStretchBlt) || (recordType == EmfPlusRecordType.WmfSetDibToDev))
                            {
                                return;
                            }
                            else if (recordType == EmfPlusRecordType.WmfStretchDib)
                            {
                                TernaryRasterOperation operation = (TernaryRasterOperation) reader.ReadUInt32();
                                ColorUsage usage = (ColorUsage) reader.ReadUInt16();
                                int num21 = reader.ReadInt16();
                                int num22 = reader.ReadInt16();
                                int num23 = reader.ReadInt16();
                                RectangleF source = new RectangleF((float) reader.ReadInt16(), (float) num23, (float) num22, (float) num21);
                                int num25 = reader.ReadInt16();
                                int num26 = reader.ReadInt16();
                                int num27 = reader.ReadInt16();
                                int num28 = reader.ReadInt16();
                                Bitmap image = DIBHelper.CreateBitmapFromDIB(reader.ReadToEnd());
                                this.DrawImage(image, source, this.State.Transform(new RectangleF((float) num28, (float) num27, (float) num26, (float) num25)));
                                return;
                            }
                        }
                        else if (recordType == EmfPlusRecordType.WmfDibBitBlt)
                        {
                            return;
                        }
                        else
                        {
                            if (recordType == EmfPlusRecordType.WmfExtTextOut)
                            {
                                reader.ReadPointYX();
                                return;
                            }
                            if (recordType == EmfPlusRecordType.WmfStretchBlt)
                            {
                                return;
                            }
                        }
                    }
                    else if (recordType > EmfPlusRecordType.WmfArc)
                    {
                        if ((recordType == EmfPlusRecordType.WmfPie) || ((recordType == EmfPlusRecordType.WmfChord) || (recordType == EmfPlusRecordType.WmfBitBlt)))
                        {
                            return;
                        }
                    }
                    else if ((recordType == EmfPlusRecordType.WmfCreateRegion) || (recordType == EmfPlusRecordType.WmfArc))
                    {
                        return;
                    }
                }
                else if (recordType > EmfPlusRecordType.WmfAnimatePalette)
                {
                    if (recordType > EmfPlusRecordType.WmfExtFloodFill)
                    {
                        if ((recordType == EmfPlusRecordType.WmfRoundRect) || ((recordType == EmfPlusRecordType.WmfPatBlt) || (recordType == EmfPlusRecordType.WmfEscape)))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (recordType == EmfPlusRecordType.WmfTextOut)
                        {
                            int num5 = reader.ReadInt16();
                            byte[] buffer2 = new byte[num5];
                            int index = 0;
                            while (true)
                            {
                                string str;
                                if (index < num5)
                                {
                                    byte num20 = reader.ReadByte();
                                    if (num20 != 0)
                                    {
                                        buffer2[index] = num20;
                                        index++;
                                        continue;
                                    }
                                }
                                if ((num5 % 2) == 1)
                                {
                                    reader.ReadByte();
                                }
                                try
                                {
                                    str = Encoding.GetEncoding(0x4e4).GetString(buffer2, 0, index);
                                }
                                catch
                                {
                                    str = Encoding.ASCII.GetString(buffer2, 0, index);
                                }
                                int num7 = reader.ReadInt16();
                                int num8 = reader.ReadInt16();
                                return;
                            }
                        }
                        if (recordType == EmfPlusRecordType.WmfPolyPolygon)
                        {
                            if (!this.IsNullStrokeFill())
                            {
                                ushort num17 = reader.ReadUInt16();
                                ushort[] numArray = new ushort[num17];
                                for (int i = 0; i < num17; i++)
                                {
                                    numArray[i] = reader.ReadUInt16();
                                }
                                for (int j = 0; j < num17; j++)
                                {
                                    PointF[] points = this.ReadPoints(reader, (long) numArray[j], true, false);
                                    points = this.State.Transform(points);
                                    this.DrawPath(points);
                                }
                                this.FillAndStroke();
                                return;
                            }
                            return;
                        }
                        else if (recordType == EmfPlusRecordType.WmfExtFloodFill)
                        {
                            return;
                        }
                    }
                }
                else if (recordType > EmfPlusRecordType.WmfRectangle)
                {
                    if ((recordType == EmfPlusRecordType.WmfSetPixel) || ((recordType == EmfPlusRecordType.WmfFrameRegion) || (recordType == EmfPlusRecordType.WmfAnimatePalette)))
                    {
                        return;
                    }
                }
                else
                {
                    if (recordType == EmfPlusRecordType.WmfPolyline)
                    {
                        ushort num16 = reader.ReadUInt16();
                        PointF[] points = this.ReadPoints(reader, (long) num16, true, false);
                        points = this.State.Transform(points);
                        this.DrawPath(points);
                        this.Stroke();
                        return;
                    }
                    switch (recordType)
                    {
                        case EmfPlusRecordType.WmfScaleWindowExt:
                        {
                            int num10 = reader.ReadInt16();
                            int num11 = reader.ReadInt16();
                            int num12 = reader.ReadInt16();
                            int num13 = reader.ReadInt16();
                            this.State.WindowExtent = new PointF((this.State.WindowExtent.X * num13) / ((float) num12), (this.State.WindowExtent.Y * num11) / ((float) num10));
                            return;
                        }
                        case EmfPlusRecordType.WmfScaleViewportExt:
                        case EmfPlusRecordType.WmfExcludeClipRect:
                        case EmfPlusRecordType.WmfIntersectClipRect:
                        case EmfPlusRecordType.WmfFloodFill:
                            return;

                        case EmfPlusRecordType.WmfEllipse:
                        {
                            int x = reader.ReadInt16();
                            int num4 = reader.ReadInt16();
                            this.template.Arc(this.State.Transform(new Point(num4, reader.ReadInt16())), this.State.Transform(new Point(x, reader.ReadInt16())), 0, 360);
                            this.FillAndStroke();
                            return;
                        }
                        case EmfPlusRecordType.WmfRectangle:
                            if (!this.IsNullStrokeFill())
                            {
                                PointF tf4 = this.State.Transform(reader.ReadPointYX());
                                PointF tf5 = this.State.Transform(reader.ReadPointYX());
                                this.template.Rectangle(tf5.X, tf5.Y, tf4.X - tf5.X, tf4.Y - tf5.Y);
                                this.FillAndStroke();
                                return;
                            }
                            return;

                        default:
                            break;
                    }
                }
                goto TR_0002;
            }
            else
            {
                if (recordType > EmfPlusRecordType.WmfResizePalette)
                {
                    if (recordType > EmfPlusRecordType.WmfMoveTo)
                    {
                        if (recordType > EmfPlusRecordType.WmfSetMapperFlags)
                        {
                            if (recordType == EmfPlusRecordType.WmfSelectPalette)
                            {
                                int objectIndex = reader.Read();
                                this.State.SelectObject(objectIndex, this.template);
                                return;
                            }
                            switch (recordType)
                            {
                                case EmfPlusRecordType.WmfCreatePenIndirect:
                                {
                                    WmfPenObject obj3 = new WmfPenObject();
                                    obj3.Read(reader);
                                    this.State.AddObject(obj3.Pen);
                                    return;
                                }
                                case EmfPlusRecordType.WmfCreateFontIndirect:
                                {
                                    WmfFontObject metaObject = new WmfFontObject();
                                    metaObject.Read(reader);
                                    this.State.AddObject(metaObject);
                                    return;
                                }
                                case EmfPlusRecordType.WmfCreateBrushIndirect:
                                {
                                    WmfLogBrush brush = new WmfLogBrush();
                                    brush.Read(reader);
                                    this.State.AddObject(brush.Brush);
                                    return;
                                }
                                default:
                                    if (recordType != EmfPlusRecordType.WmfPolygon)
                                    {
                                        break;
                                    }
                                    if (!this.IsNullStrokeFill())
                                    {
                                        ushort num15 = reader.ReadUInt16();
                                        PointF[] points = this.ReadPoints(reader, (long) num15, true, false);
                                        points = this.State.Transform(points);
                                        this.DrawPath(points);
                                        this.FillAndStroke();
                                        return;
                                    }
                                    return;
                            }
                        }
                        else if ((recordType == EmfPlusRecordType.WmfOffsetCilpRgn) || ((recordType == EmfPlusRecordType.WmfFillRegion) || (recordType == EmfPlusRecordType.WmfSetMapperFlags)))
                        {
                            return;
                        }
                    }
                    else if (recordType > EmfPlusRecordType.WmfSetLayout)
                    {
                        if (recordType == EmfPlusRecordType.WmfDeleteObject)
                        {
                            this.State.RemoveObject(reader.ReadUInt16());
                            return;
                        }
                        if (recordType == EmfPlusRecordType.WmfCreatePatternBrush)
                        {
                            return;
                        }
                        else
                        {
                            switch (recordType)
                            {
                                case EmfPlusRecordType.WmfSetBkColor:
                                    this.State.BackgroundColor = reader.ReadColorRGB();
                                    return;

                                case EmfPlusRecordType.WmfSetTextColor:
                                    this.State.TextColor = reader.ReadColorRGB();
                                    return;

                                case EmfPlusRecordType.WmfSetTextJustification:
                                case EmfPlusRecordType.WmfSetViewportOrg:
                                case EmfPlusRecordType.WmfSetViewportExt:
                                case EmfPlusRecordType.WmfOffsetViewportOrg:
                                    return;

                                case EmfPlusRecordType.WmfSetWindowOrg:
                                    this.State.WindowOrigin = reader.ReadPointYX();
                                    return;

                                case EmfPlusRecordType.WmfSetWindowExt:
                                    this.State.WindowExtent = (PointF) reader.ReadPointYX();
                                    return;

                                case EmfPlusRecordType.WmfOffsetWindowOrg:
                                {
                                    Point point = reader.ReadPointYX();
                                    this.State.WindowOrigin = new Point(this.State.WindowOrigin.X + point.X, this.State.WindowOrigin.Y + point.Y);
                                    return;
                                }
                                case EmfPlusRecordType.WmfLineTo:
                                {
                                    PointF tf2 = this.State.Transform(reader.ReadPointYX());
                                    this.template.LineTo(tf2.X, tf2.Y);
                                    this.Stroke();
                                    return;
                                }
                                case EmfPlusRecordType.WmfMoveTo:
                                {
                                    PointF tf3 = this.State.Transform(reader.ReadPointYX());
                                    this.template.MoveTo(tf3.X, tf3.Y);
                                    return;
                                }
                                default:
                                    break;
                            }
                        }
                    }
                    else if ((recordType == EmfPlusRecordType.WmfDibCreatePatternBrush) || (recordType == EmfPlusRecordType.WmfSetLayout))
                    {
                        return;
                    }
                }
                else if (recordType > EmfPlusRecordType.WmfSaveDC)
                {
                    if (recordType > EmfPlusRecordType.WmfCreatePalette)
                    {
                        switch (recordType)
                        {
                            case EmfPlusRecordType.WmfSetBkMode:
                                this.State.BackgroundMode = (MixMode) reader.ReadInt16();
                                return;

                            case EmfPlusRecordType.WmfSetMapMode:
                                this.State.SetMapMode((MapMode) reader.ReadUInt16());
                                return;

                            case EmfPlusRecordType.WmfSetROP2:
                            case EmfPlusRecordType.WmfSetRelAbs:
                            case EmfPlusRecordType.WmfSetStretchBltMode:
                            case EmfPlusRecordType.WmfSetTextCharExtra:
                                return;

                            case EmfPlusRecordType.WmfSetPolyFillMode:
                                this.State.PolygonFillMode = (PolyFillMode) reader.ReadInt16();
                                return;

                            default:
                                switch (recordType)
                                {
                                    case EmfPlusRecordType.WmfRestoreDC:
                                    case EmfPlusRecordType.WmfInvertRegion:
                                    case EmfPlusRecordType.WmfPaintRegion:
                                    case EmfPlusRecordType.WmfSelectClipRegion:
                                        return;

                                    case (EmfPlusRecordType.WmfSetTextCharExtra | EmfPlusRecordType.EmfScaleWindowExtEx):
                                    case (EmfPlusRecordType.WmfSetTextCharExtra | EmfPlusRecordType.EmfSaveDC):
                                        break;

                                    case EmfPlusRecordType.WmfSelectObject:
                                    {
                                        int objectIndex = reader.ReadUInt16();
                                        this.State.SelectObject(objectIndex, this.template);
                                        return;
                                    }
                                    case EmfPlusRecordType.WmfSetTextAlign:
                                        this.State.TextAlign = (TextAlignmentMode) reader.ReadInt16();
                                        return;

                                    default:
                                        if (recordType != EmfPlusRecordType.WmfResizePalette)
                                        {
                                            break;
                                        }
                                        return;
                                }
                                break;
                        }
                    }
                    else if ((recordType == EmfPlusRecordType.WmfRealizePalette) || (recordType == EmfPlusRecordType.WmfSetPalEntries))
                    {
                        return;
                    }
                    else if (recordType == EmfPlusRecordType.WmfCreatePalette)
                    {
                        WmfPaletteObject metaObject = new WmfPaletteObject();
                        metaObject.Read(reader);
                        this.State.AddObject(metaObject);
                        return;
                    }
                }
                else if (recordType > EmfPlusRecordType.EmfEof)
                {
                    switch (recordType)
                    {
                        case EmfPlusRecordType.Header:
                            this.ProcessEmfPlus_Header(reader);
                            return;

                        case EmfPlusRecordType.EndOfFile:
                        case EmfPlusRecordType.SetAntiAliasMode:
                        case EmfPlusRecordType.SetTextRenderingHint:
                        case EmfPlusRecordType.SetPixelOffsetMode:
                            return;

                        case EmfPlusRecordType.Comment:
                        case EmfPlusRecordType.GetDC:
                        case EmfPlusRecordType.MultiFormatStart:
                        case EmfPlusRecordType.MultiFormatSection:
                        case EmfPlusRecordType.MultiFormatEnd:
                        case EmfPlusRecordType.FillRegion:
                        case EmfPlusRecordType.FillClosedCurve:
                        case EmfPlusRecordType.DrawClosedCurve:
                        case EmfPlusRecordType.DrawCurve:
                        case EmfPlusRecordType.DrawImage:
                        case EmfPlusRecordType.SetRenderingOrigin:
                        case EmfPlusRecordType.SetTextContrast:
                        case EmfPlusRecordType.SetInterpolationMode:
                        case EmfPlusRecordType.SetCompositingMode:
                        case EmfPlusRecordType.SetCompositingQuality:
                        case EmfPlusRecordType.ResetClip:
                            break;

                        case EmfPlusRecordType.Object:
                            this.ProcessEmfPlus_Object(reader, bytes);
                            return;

                        case EmfPlusRecordType.Clear:
                            this.ProcessEmfPlus_Clear(reader);
                            return;

                        case EmfPlusRecordType.FillRects:
                            this.ProcessEmfPlus_FillRects(reader, bytes);
                            return;

                        case EmfPlusRecordType.DrawRects:
                            this.ProcessEmfPlus_DrawRects(reader, bytes);
                            return;

                        case EmfPlusRecordType.FillPolygon:
                            this.ProcessEmfPlus_FillPolygon(reader, bytes);
                            return;

                        case EmfPlusRecordType.DrawLines:
                            this.ProcessEmfPlus_DrawLines(reader, bytes);
                            return;

                        case EmfPlusRecordType.FillEllipse:
                            this.ProcessEmfPlus_FillEllipse(reader, bytes);
                            return;

                        case EmfPlusRecordType.DrawEllipse:
                            this.ProcessEmfPlus_DrawEllipse(reader, bytes);
                            return;

                        case EmfPlusRecordType.FillPie:
                            this.ProcessEmfPlus_FillPie(reader, bytes);
                            return;

                        case EmfPlusRecordType.DrawPie:
                            this.ProcessEmfPlus_DrawPie(reader, bytes);
                            return;

                        case EmfPlusRecordType.DrawArc:
                            this.ProcessEmfPlus_DrawArc(reader, bytes);
                            return;

                        case EmfPlusRecordType.FillPath:
                            this.ProcessEmfPlus_FillPath(reader, bytes);
                            return;

                        case EmfPlusRecordType.DrawPath:
                            this.ProcessEmfPlus_DrawPath(reader, bytes);
                            return;

                        case EmfPlusRecordType.DrawBeziers:
                            this.ProcessEmfPlus_DrawBeziers(reader, bytes);
                            return;

                        case EmfPlusRecordType.DrawImagePoints:
                            this.ProcessEmfPlus_DrawImagePoints(reader, bytes);
                            return;

                        case EmfPlusRecordType.DrawString:
                            this.ProcessEmfPlus_DrawString(reader, bytes);
                            return;

                        case EmfPlusRecordType.Save:
                            this.ProcessEmfPlus_Save(reader);
                            return;

                        case EmfPlusRecordType.Restore:
                            this.ProcessEmfPlus_Restore(reader);
                            return;

                        case EmfPlusRecordType.BeginContainer:
                        case EmfPlusRecordType.BeginContainerNoParams:
                        case EmfPlusRecordType.EndContainer:
                            goto TR_0003;

                        case EmfPlusRecordType.SetWorldTransform:
                            this.ProcessEmfPlus_SetWorldTransform(reader);
                            return;

                        case EmfPlusRecordType.ResetWorldTransform:
                            this.ProcessEmfPlus_ResetWorldTransform();
                            return;

                        case EmfPlusRecordType.MultiplyWorldTransform:
                            this.ProcessEmfPlus_MultiplyWorldTransform(reader, bytes);
                            return;

                        case EmfPlusRecordType.TranslateWorldTransform:
                            this.ProcessEmfPlus_TranslateWorldTransform(reader, bytes);
                            return;

                        case EmfPlusRecordType.ScaleWorldTransform:
                            this.ProcessEmfPlus_ScaleWorldTransform(reader, bytes);
                            return;

                        case EmfPlusRecordType.RotateWorldTransform:
                            this.ProcessEmfPlus_RotateWorldTransform(reader, bytes);
                            return;

                        case EmfPlusRecordType.SetPageTransform:
                            this.ProcessEmfPlus_SetPageTransform(reader, bytes);
                            return;

                        case EmfPlusRecordType.SetClipRect:
                            this.ProcessEmfPlus_SetClipRect(reader, bytes);
                            return;

                        case EmfPlusRecordType.SetClipPath:
                            this.ProcessEmfPlus_SetClipPath(bytes);
                            return;

                        case EmfPlusRecordType.SetClipRegion:
                            this.ProcessEmfPlus_SetClipRegion(bytes);
                            return;

                        default:
                            if ((recordType != EmfPlusRecordType.WmfRecordBase) && (recordType != EmfPlusRecordType.WmfSaveDC))
                            {
                                break;
                            }
                            return;
                    }
                }
                else if ((recordType == EmfPlusRecordType.EmfHeader) || (recordType == EmfPlusRecordType.EmfEof))
                {
                    goto TR_0003;
                }
                goto TR_0002;
            }
            goto TR_0003;
        TR_0002:
            this.hasUnsupportedRecords = true;
            this.DrawUnsupportedRecord(recordType, reader, flagsRaw, dataSize);
            return;
        TR_0003:
            this.DrawUnsupportedRecord(recordType, reader, flagsRaw, dataSize);
        }

        private Brush ReadBrushEmfPlus(MetaReader reader, byte[] flags)
        {
            uint num = reader.ReadUInt32();
            return (((flags[1] & 0x80) != 0x80) ? ((Brush) this.State.GetObject((int) num)) : new SolidBrush(Color.FromArgb((int) num)));
        }

        private Pen ReadPenEmfPlus(MetaReader reader)
        {
            uint num = reader.ReadUInt32();
            return (Pen) this.State.GetObject((int) num);
        }

        private PointF[] ReadPoints(MetaReader reader, long numberOfPoints, bool compressed = true, bool relative = false)
        {
            PointF[] tfArray = reader.ReadPoints(numberOfPoints, compressed);
            for (int i = 0; i < tfArray.Length; i++)
            {
                if (relative && (i > 0))
                {
                    tfArray[i] = new PointF(tfArray[i].X + tfArray[i - 1].X, tfArray[i].Y + tfArray[i - 1].Y);
                }
            }
            return tfArray;
        }

        private RectangleF[] ReadRectangles(MetaReader reader, long count, bool compressed = true) => 
            reader.ReadRectangles(compressed, count);

        internal void Stroke()
        {
            if ((this.State.CurrentPen == null) || (this.State.CurrentPen.Color != Color.Transparent))
            {
                this.template.Stroke();
            }
        }

        public void Write()
        {
            Bitmap image = new Bitmap(this.State.ImageSize.Width, this.State.ImageSize.Height);
            image.SetResolution(this.State.HorizontalResolution, this.State.VerticalResolution);
            this.hasUnsupportedRecords = false;
            if (this.image != null)
            {
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    if (startTest != null)
                    {
                        startTest();
                    }
                    graphics.EnumerateMetafile(this.image, new Point(0, 0), new Graphics.EnumerateMetafileProc(this.MetafileCallback));
                }
            }
            if (this.hasUnsupportedRecords)
            {
                this.graphics.SetTransform(this.baseTransform);
                this.graphics.DrawImage(image, new RectangleF((PointF) Point.Empty, (SizeF) image.Size), Color.Transparent);
            }
            this.graphics.ResetTransform();
        }

        internal MetaState State { get; set; }
    }
}

