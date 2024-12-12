namespace DevExpress.Printing.Core.NativePdfExport
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Pdf.Native;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class PdfGraphicsImplementation : PdfDisposableObject
    {
        public static bool UseLegacyFormatterOnLinux;
        private const float maxPageSize = 14400f;
        private readonly Stack<Matrix> matrixStack = new Stack<Matrix>();
        private readonly Stack<DevExpress.Printing.Core.NativePdfExport.PdfGraphicsState> stateStack = new Stack<DevExpress.Printing.Core.NativePdfExport.PdfGraphicsState>();
        private PdfExportDocument exportDocument;
        private Matrix currentTransform = new Matrix();
        private bool isDocumentFinalized;
        private PdfGraphicsCommandConstructor commandConstructor;
        private PdfGraphicsClip clip;
        private SizeF pageSize = SizeF.Empty;
        private Color backColor = DXColor.Empty;
        private GraphicsUnit pageUnit = GraphicsUnit.Document;
        private Dictionary<PdfAcroFormVisualField, PdfRectangle> acroFormVisualFields = new Dictionary<PdfAcroFormVisualField, PdfRectangle>();
        private Dictionary<string, PdfButtonFormField> acroFormRadioGroupDictionary = new Dictionary<string, PdfButtonFormField>();

        public PdfGraphicsImplementation(PdfExportDocument exportDocument)
        {
            this.exportDocument = exportDocument;
        }

        private void AddCheckBoxField(CheckEditingField checkEditingField, string acroFormFieldName, int pageNumber, PdfRectangle pdfRect)
        {
            PdfAcroFormCheckBoxField key = new PdfAcroFormCheckBoxField(acroFormFieldName, pageNumber, pdfRect) {
                ButtonStyle = PdfAcroFormButtonStyle.Check,
                Appearance = { BorderAppearance = this.CreateCheckBoxBorderAppearance() },
                IsChecked = checkEditingField.CheckState != CheckState.Unchecked,
                ReadOnly = checkEditingField.ReadOnly
            };
            this.acroFormVisualFields.Add(key, pdfRect);
        }

        public void AddCheckEditingField(CheckEditingField checkEditingField, int pageNumber, PdfRectangle pdfRect)
        {
            string acroFormFieldName = this.ExportDocument.GetAcroFormFieldName(checkEditingField.ID);
            if (string.IsNullOrEmpty(checkEditingField.GroupID))
            {
                this.AddCheckBoxField(checkEditingField, acroFormFieldName, pageNumber, pdfRect);
            }
            else
            {
                this.AddRadioCheckBoxField(checkEditingField, acroFormFieldName, pageNumber, pdfRect);
            }
        }

        public void AddCombTextEditingField(TextEditingField textEditingField, int pageNumber, PdfRectangle pdfRect, string text, int length)
        {
            PdfAcroFormTextBoxField formField = new PdfAcroFormCombTextBoxField(this.ExportDocument.GetAcroFormFieldName(textEditingField.ID), pageNumber, pdfRect, length);
            this.SetupTextBoxFieldCore(textEditingField, formField, text);
            formField.Multiline = false;
            this.acroFormVisualFields.Add(formField, pdfRect);
        }

        public void AddPage(SizeF pageSize, int pageIndex)
        {
            this.AddPage(pageSize, pageIndex, GraphicsDpi.UnitToDpi(GraphicsUnit.Document), true);
        }

        public void AddPage(SizeF pageSize, int pageIndex, float dpi, bool roundSize)
        {
            this.ClosePage();
            if (this.FlushPageContent)
            {
                this.exportDocument.FlushPage();
            }
            float num = GraphicsUnitConverter.Convert((float) 14400f, GraphicsDpi.UnitToDpi(GraphicsUnit.Point), dpi);
            pageSize = new SizeF(Math.Min(num, pageSize.Width), Math.Min(num, pageSize.Height));
            this.commandConstructor = this.exportDocument.CreateCommandConstructor(pageSize, pageIndex, dpi, roundSize);
            this.ClipBounds = new RectangleF(PointF.Empty, pageSize);
            this.pageSize = pageSize;
        }

        private void AddRadioCheckBoxField(CheckEditingField checkEditingField, string acroFormFieldName, int pageNumber, PdfRectangle pdfRect)
        {
            PdfAcroFormFieldAppearance appearance = new PdfAcroFormFieldAppearance();
            PdfAcroFormRadioButton key = new PdfAcroFormRadioButton(this.GetRadioGroupField(checkEditingField, pageNumber), checkEditingField.CheckState != CheckState.Unchecked, pdfRect, acroFormFieldName, pageNumber) {
                ReadOnly = checkEditingField.ReadOnly,
                Appearance = { BorderAppearance = this.CreateCheckBoxBorderAppearance() }
            };
            this.acroFormVisualFields.Add(key, pdfRect);
        }

        public void AddTextEditingField(TextEditingField textEditingField, int pageNumber, PdfRectangle pdfRect, string text)
        {
            PdfAcroFormTextBoxField formField = new PdfAcroFormTextBoxField(this.ExportDocument.GetAcroFormFieldName(textEditingField.ID), pageNumber, pdfRect);
            this.SetupTextBoxFieldCore(textEditingField, formField, text);
            VisualBrick brick = textEditingField.Brick;
            formField.Multiline = brick.Text.Contains(Environment.NewLine) || brick.Style.StringFormat.WordWrap;
            this.acroFormVisualFields.Add(formField, pdfRect);
        }

        private void ApplyPageUnitToClipArray(GraphicsUnit newPageUnit)
        {
            if (this.clip != null)
            {
                float num = GraphicsDpi.UnitToDpiI(newPageUnit) / GraphicsDpi.UnitToDpiI(this.pageUnit);
                using (Matrix matrix = this.currentTransform.Clone())
                {
                    using (Matrix matrix2 = new Matrix(num, 0f, 0f, num, 0f, 0f))
                    {
                        matrix2.Multiply(matrix, MatrixOrder.Prepend);
                        matrix.Invert();
                        matrix2.Multiply(matrix, MatrixOrder.Append);
                        this.clip.ApplyTransform(matrix2);
                    }
                }
            }
        }

        public void ApplyTransformState(MatrixOrder order, bool removeState)
        {
            Matrix matrix = removeState ? this.matrixStack.Pop() : this.matrixStack.Peek();
            this.currentTransform.Multiply(matrix, order);
            if (removeState)
            {
                matrix.Dispose();
            }
        }

        public virtual void ClosePage()
        {
            if (this.commandConstructor != null)
            {
                this.ResetTransform();
                foreach (Matrix matrix in this.matrixStack)
                {
                    matrix.Dispose();
                }
                this.matrixStack.Clear();
                this.exportDocument.ApplyConstructor(this.commandConstructor);
                this.commandConstructor.Dispose();
                this.commandConstructor = null;
            }
            Func<KeyValuePair<PdfAcroFormVisualField, PdfRectangle>, double> keySelector = <>c.<>9__75_0;
            if (<>c.<>9__75_0 == null)
            {
                Func<KeyValuePair<PdfAcroFormVisualField, PdfRectangle>, double> local1 = <>c.<>9__75_0;
                keySelector = <>c.<>9__75_0 = x => Math.Round(x.Value.Top, 3);
            }
            Func<KeyValuePair<PdfAcroFormVisualField, PdfRectangle>, double> func2 = <>c.<>9__75_1;
            if (<>c.<>9__75_1 == null)
            {
                Func<KeyValuePair<PdfAcroFormVisualField, PdfRectangle>, double> local2 = <>c.<>9__75_1;
                func2 = <>c.<>9__75_1 = x => x.Value.Left;
            }
            Func<KeyValuePair<PdfAcroFormVisualField, PdfRectangle>, PdfAcroFormVisualField> selector = <>c.<>9__75_2;
            if (<>c.<>9__75_2 == null)
            {
                Func<KeyValuePair<PdfAcroFormVisualField, PdfRectangle>, PdfAcroFormVisualField> local3 = <>c.<>9__75_2;
                selector = <>c.<>9__75_2 = x => x.Key;
            }
            foreach (PdfAcroFormVisualField field in this.acroFormVisualFields.OrderByDescending<KeyValuePair<PdfAcroFormVisualField, PdfRectangle>, double>(keySelector).ThenBy<KeyValuePair<PdfAcroFormVisualField, PdfRectangle>, double>(func2).Select<KeyValuePair<PdfAcroFormVisualField, PdfRectangle>, PdfAcroFormVisualField>(selector))
            {
                this.ExportDocument.AddFormField(field);
            }
            this.acroFormVisualFields.Clear();
        }

        private PdfAcroFormBorderAppearance CreateCheckBoxBorderAppearance()
        {
            PdfAcroFormBorderAppearance appearance1 = new PdfAcroFormBorderAppearance();
            appearance1.Style = PdfAcroFormBorderStyle.Solid;
            appearance1.Color = new PdfRGBColor(0.5, 0.5, 0.5);
            appearance1.Width = 1.0;
            return appearance1;
        }

        private PdfRGBColor CreatePdfRGBColorFromColor(Color color) => 
            new PdfRGBColor((double) (((float) color.R) / 255f), (double) (((float) color.G) / 255f), (double) (((float) color.B) / 255f));

        public PdfForm CreateSignatureForm(RectangleF rectangle)
        {
            this.ValidateConstructor();
            int index = this.exportDocument.Document.Pages.IndexOf(this.exportDocument.CurrentPage);
            PdfFormSignatureAppearance appearance = this.commandConstructor.CreateSignatureAppearance(rectangle, index);
            this.exportDocument.SetSignatureAppearanceForm(appearance);
            return appearance.Form;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.exportDocument != null))
            {
                this.FinalizeDocument();
                this.currentTransform.Dispose();
                foreach (DevExpress.Printing.Core.NativePdfExport.PdfGraphicsState state in this.stateStack)
                {
                    state.Dispose();
                }
            }
        }

        protected static bool DoNotIgnoreDraw(Brush brush)
        {
            SolidBrush brush2 = brush as SolidBrush;
            return ((brush2 == null) || (brush2.Color.A != 0));
        }

        public PdfForm DrawDeferredForm(RectangleF rect)
        {
            this.ValidateConstructor();
            PdfForm form = null;
            this.PerformIsolatedAction(delegate {
                form = this.commandConstructor.DrawDeferredForm(rect);
            });
            return form;
        }

        public void DrawEllipse(Pen pen, RectangleF rect)
        {
            if (DoNotIgnoreDraw(pen.Brush))
            {
                this.ValidateConstructor();
                this.PerformIsolatedAction(delegate {
                    this.commandConstructor.SetPen(pen);
                    this.commandConstructor.DrawEllipse(rect);
                });
            }
        }

        public void DrawEllipse(Pen pen, float x, float y, float width, float height)
        {
            this.DrawEllipse(pen, new RectangleF(x, y, width, height));
        }

        public void DrawImage(Image image, Point point)
        {
            this.ValidateConstructor();
            this.PerformIsolatedAction(() => this.commandConstructor.DrawXObject(this.exportDocument.AddImage(image), (PointF) point));
        }

        public void DrawImage(Image image, RectangleF rect)
        {
            this.DrawImage(image, rect, this.backColor);
        }

        public void DrawImage(Image image, RectangleF rect, Color underlyingColor)
        {
            this.ValidateConstructor();
            if ((!this.ShouldConvertMetafiles || !this.ExportDocument.ConvertImagesToJpeg) && (this.exportDocument.SupportsTransparency || (!(image is Bitmap) || !image.PixelFormat.HasFlag(PixelFormat.Alpha))))
            {
                this.PerformIsolatedAction(() => this.commandConstructor.DrawXObject(this.exportDocument.AddImage(image), rect));
            }
            else
            {
                Color backColor = this.GetActualBackColor(underlyingColor);
                this.PerformIsolatedAction(() => this.commandConstructor.DrawXObject(this.exportDocument.AddImage(image, backColor), rect));
            }
        }

        public void DrawImage(EmfMetafile image, Image bitmap, RectangleF rect, Color underlyingColor)
        {
            if (this.exportDocument.ConvertImagesToJpeg && (Environment.OSVersion.Platform == PlatformID.Win32NT))
            {
                this.DrawImage(bitmap, rect, underlyingColor);
            }
            else
            {
                this.ValidateConstructor();
                this.PerformIsolatedAction(() => this.commandConstructor.DrawXObject(this.exportDocument.AddImage(image), rect));
            }
        }

        public void DrawLine(Pen pen, PointF pt1, PointF pt2)
        {
            this.DrawLine(pen, pt1.X, pt1.Y, pt2.X, pt2.Y);
        }

        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            if (DoNotIgnoreDraw(pen.Brush))
            {
                this.ValidateConstructor();
                this.PerformIsolatedAction(delegate {
                    this.commandConstructor.SetPen(pen);
                    this.commandConstructor.DrawLine(x1, y1, x2, y2);
                });
            }
        }

        public void DrawLines(Pen pen, PointF[] points)
        {
            if (DoNotIgnoreDraw(pen.Brush))
            {
                this.ValidateConstructor();
                this.PerformIsolatedAction(delegate {
                    this.commandConstructor.SetPen(pen);
                    this.commandConstructor.DrawLines(points);
                });
            }
        }

        public void DrawPath(Pen pen, GraphicsPath path)
        {
            if (DoNotIgnoreDraw(pen.Brush) && (path.PointCount > 0))
            {
                this.ValidateConstructor();
                this.PerformIsolatedAction(delegate {
                    this.commandConstructor.SetPen(pen);
                    this.commandConstructor.DrawPath(path.PathPoints, path.PathTypes);
                });
            }
        }

        public void DrawRectangle(Pen pen, RectangleF bounds)
        {
            if (DoNotIgnoreDraw(pen.Brush))
            {
                this.ValidateConstructor();
                this.PerformIsolatedAction(delegate {
                    this.commandConstructor.SetPen(pen);
                    this.commandConstructor.DrawRectangle(bounds);
                });
            }
        }

        public void FillEllipse(Brush brush, RectangleF rect)
        {
            if (DoNotIgnoreDraw(brush))
            {
                this.ValidateConstructor();
                this.PerformIsolatedAction(delegate {
                    this.commandConstructor.SetBrush(brush);
                    this.commandConstructor.FillEllipse(rect);
                });
            }
        }

        public void FillEllipse(Brush brush, float x, float y, float width, float height)
        {
            this.FillEllipse(brush, new RectangleF(x, y, width, height));
        }

        public void FillPath(Brush brush, GraphicsPath path)
        {
            if (DoNotIgnoreDraw(brush) && (path.PointCount > 0))
            {
                this.ValidateConstructor();
                this.PerformIsolatedAction(delegate {
                    this.commandConstructor.SetBrush(brush);
                    this.commandConstructor.FillPath(path.PathPoints, path.PathTypes, path.FillMode == FillMode.Winding);
                });
            }
        }

        public void FillPolygon(Brush brush, PointF[] points)
        {
            if (DoNotIgnoreDraw(brush))
            {
                this.ValidateConstructor();
                this.PerformIsolatedAction(delegate {
                    this.commandConstructor.SetBrush(brush);
                    this.commandConstructor.FillPolygon(points, true);
                });
            }
        }

        public void FillRectangle(Brush brush, RectangleF bounds)
        {
            if (DoNotIgnoreDraw(brush))
            {
                this.ValidateConstructor();
                this.PerformIsolatedAction(delegate {
                    this.commandConstructor.SetBrush(brush);
                    this.commandConstructor.FillRectangle(bounds);
                });
            }
        }

        public void FillRectangle(Brush brush, float x, float y, float width, float height)
        {
            this.FillRectangle(brush, new RectangleF(x, y, width, height));
        }

        public void FinalizeDocument()
        {
            if (!this.isDocumentFinalized)
            {
                this.isDocumentFinalized = true;
                this.ClosePage();
                if (this.exportDocument != null)
                {
                    this.exportDocument.FinalizeDocument();
                    this.exportDocument.Dispose();
                    this.exportDocument = null;
                }
            }
        }

        public void FlushFormCommands(PdfForm form)
        {
            form.ReplaceCommands(this.commandConstructor.Commands);
            this.commandConstructor.Dispose();
            this.commandConstructor = null;
        }

        private Color GetActualBackColor(Color underlyingColor) => 
            this.exportDocument.SupportsTransparency ? Color.Transparent : (!DXColor.IsTransparentColor(underlyingColor) ? underlyingColor : Color.White);

        private PdfButtonFormField GetRadioGroupField(CheckEditingField checkEditingField, int pageNumber)
        {
            PdfButtonFormField field;
            if (!this.acroFormRadioGroupDictionary.TryGetValue(checkEditingField.GroupID, out field))
            {
                PdfAcroFormRadioGroupField radioGroup = new PdfAcroFormRadioGroupField(this.ExportDocument.GetAcroFormRadioGroupName(checkEditingField.GroupID), pageNumber);
                radioGroup.ReadOnly = checkEditingField.ReadOnly;
                field = new PdfButtonFormField(radioGroup, this.ExportDocument.Document, null);
                this.acroFormRadioGroupDictionary.Add(checkEditingField.GroupID, field);
            }
            return field;
        }

        public void IntersectClip(GraphicsPath path)
        {
            if (path.PointCount > 0)
            {
                this.clip = this.clip.Intersect(PdfGraphicsClip.Create(path, this.currentTransform));
            }
        }

        protected void PerformIsolatedAction(Action action)
        {
            this.commandConstructor.SaveGraphicsState();
            if (this.clip != null)
            {
                this.clip.Apply(this.commandConstructor);
            }
            if (!this.currentTransform.IsIdentity)
            {
                float[] elements = this.currentTransform.Elements;
                this.commandConstructor.UpdateTransformationMatrix(new PdfTransformationMatrix((double) elements[0], (double) elements[1], (double) elements[2], (double) elements[3], (double) elements[4], (double) elements[5]));
            }
            action();
            this.commandConstructor.RestoreGraphicsState();
        }

        public void ResetTransform()
        {
            this.currentTransform.Reset();
        }

        public void Restore(IGraphicsState state)
        {
            while ((this.stateStack.Count > 0) && (state != this.stateStack.Peek()))
            {
                this.stateStack.Pop().Dispose();
            }
            if (this.stateStack.Count > 0)
            {
                using (DevExpress.Printing.Core.NativePdfExport.PdfGraphicsState state2 = this.stateStack.Pop())
                {
                    this.currentTransform = state2.Matrix;
                    this.clip = state2.Clip;
                    this.PageUnit = state2.PageUnit;
                }
            }
        }

        public void RotateTransform(float angle, MatrixOrder order)
        {
            this.currentTransform.Rotate(angle, order);
        }

        public IGraphicsState Save()
        {
            DevExpress.Printing.Core.NativePdfExport.PdfGraphicsState item = new DevExpress.Printing.Core.NativePdfExport.PdfGraphicsState(this.currentTransform, this.clip, this.pageUnit);
            this.stateStack.Push(item);
            return item;
        }

        public void SaveTransformState()
        {
            this.matrixStack.Push(this.currentTransform.Clone());
        }

        public void ScaleTransform(float sx, float sy, MatrixOrder order)
        {
            this.currentTransform.Scale(sx, sy, order);
        }

        public void SetDrawingForm(PdfForm form)
        {
            this.commandConstructor = this.exportDocument.CreateFormCommandConstructor(form, GraphicsDpi.UnitToDpi(GraphicsUnit.Document));
        }

        private void SetupTextBoxFieldCore(TextEditingField textEditingField, PdfAcroFormTextBoxField formField, string text)
        {
            VisualBrick brick = textEditingField.Brick;
            Font font = textEditingField.Brick.Style.Font;
            formField.TextAlignment = (PdfAcroFormStringAlignment) GraphicsConvertHelper.ToHorzStringAlignment(brick.Style.TextAlignment);
            formField.ReadOnly = textEditingField.ReadOnly;
            formField.Text = text;
            formField.Appearance.FontSize = font.SizeInPoints;
            formField.Appearance.FontFamily = font.FontFamily.Name;
            formField.Appearance.FontStyle = (PdfFontStyle) font.Style;
            formField.Appearance.ForeColor = this.CreatePdfRGBColorFromColor(brick.Style.ForeColor);
        }

        public void SetUriArea(string uri, RectangleF bounds)
        {
            this.ValidateConstructor();
            this.exportDocument.AddLinkToUri(this.commandConstructor.TransformRectangle(bounds), uri);
        }

        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            this.currentTransform.Translate(dx, dy, order);
        }

        protected void ValidateConstructor()
        {
            if (this.commandConstructor == null)
            {
                throw new Exception("The current page undefined");
            }
        }

        protected PdfGraphicsClip Clip
        {
            get => 
                this.clip;
            set => 
                this.clip = value;
        }

        public Matrix Transform
        {
            get => 
                this.currentTransform;
            set => 
                this.currentTransform = value;
        }

        public RectangleF ClipBounds
        {
            get => 
                (this.clip != null) ? this.clip.GetBounds(this.currentTransform) : RectangleF.Empty;
            set => 
                this.clip = PdfGraphicsClip.Create(value, this.currentTransform);
        }

        public GraphicsUnit PageUnit
        {
            get => 
                this.pageUnit;
            set
            {
                this.pageUnit = value;
                this.ApplyPageUnitToClipArray(this.pageUnit);
                if (this.commandConstructor != null)
                {
                    float dpiX = GraphicsDpi.UnitToDpi(this.pageUnit);
                    this.commandConstructor.SetDpi(dpiX, dpiX);
                }
            }
        }

        public PdfGraphicsCommandConstructor CommandConstructor =>
            this.commandConstructor;

        public PdfExportDocument ExportDocument =>
            this.exportDocument;

        public SizeF PageSize =>
            this.pageSize;

        public bool FlushPageContent { get; set; }

        protected virtual bool ShouldConvertMetafiles =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfGraphicsImplementation.<>c <>9 = new PdfGraphicsImplementation.<>c();
            public static Func<KeyValuePair<PdfAcroFormVisualField, PdfRectangle>, double> <>9__75_0;
            public static Func<KeyValuePair<PdfAcroFormVisualField, PdfRectangle>, double> <>9__75_1;
            public static Func<KeyValuePair<PdfAcroFormVisualField, PdfRectangle>, PdfAcroFormVisualField> <>9__75_2;

            internal double <ClosePage>b__75_0(KeyValuePair<PdfAcroFormVisualField, PdfRectangle> x) => 
                Math.Round(x.Value.Top, 3);

            internal double <ClosePage>b__75_1(KeyValuePair<PdfAcroFormVisualField, PdfRectangle> x) => 
                x.Value.Left;

            internal PdfAcroFormVisualField <ClosePage>b__75_2(KeyValuePair<PdfAcroFormVisualField, PdfRectangle> x) => 
                x.Key;
        }
    }
}

