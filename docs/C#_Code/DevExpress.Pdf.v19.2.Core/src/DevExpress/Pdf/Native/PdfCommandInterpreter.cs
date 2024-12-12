namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class PdfCommandInterpreter : PdfDisposableObject
    {
        private const string fallbackFontName = "Arial";
        protected const double GlyphPositionToTextSpaceFactor = 0.001;
        private readonly Stack<PdfGraphicsState> graphicsStateStack;
        private readonly PdfPage page;
        private readonly PdfRectangle boundingBox;
        private PdfGraphicsState graphicsState;
        private List<PdfGraphicsPath> paths;

        protected PdfCommandInterpreter(PdfPage page, PdfRectangle boundingBox)
        {
            this.graphicsStateStack = new Stack<PdfGraphicsState>();
            this.paths = new List<PdfGraphicsPath>();
            this.page = page;
            this.boundingBox = boundingBox;
            this.graphicsState = new PdfGraphicsState();
            double scaleX = (page == null) ? 1.0 : page.UserUnit;
            this.graphicsState.TransformationMatrix = PdfTransformationMatrix.Scale(new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, -boundingBox.Left, -boundingBox.Bottom), scaleX, scaleX);
        }

        protected PdfCommandInterpreter(PdfPage page, PdfRectangle boundingBox, PdfGraphicsState initalState)
        {
            this.graphicsStateStack = new Stack<PdfGraphicsState>();
            this.paths = new List<PdfGraphicsPath>();
            this.page = page;
            this.boundingBox = boundingBox;
            this.graphicsState = initalState;
        }

        public virtual void AppendPathBezierSegment(PdfPoint controlPoint2, PdfPoint endPoint)
        {
            PdfGraphicsPath currentPath = this.CurrentPath;
            if (currentPath != null)
            {
                currentPath.AppendBezierSegment(currentPath.EndPoint, controlPoint2, endPoint);
            }
        }

        public virtual void AppendPathBezierSegment(PdfPoint controlPoint1, PdfPoint controlPoint2, PdfPoint endPoint)
        {
            PdfGraphicsPath currentPath = this.CurrentPath;
            if (currentPath != null)
            {
                currentPath.AppendBezierSegment(controlPoint1, controlPoint2, endPoint);
            }
        }

        public virtual void AppendPathLineSegment(PdfPoint endPoint)
        {
            PdfGraphicsPath currentPath = this.CurrentPath;
            if (currentPath != null)
            {
                currentPath.AppendLineSegment(endPoint);
            }
        }

        public virtual void AppendRectangle(double x, double y, double width, double height)
        {
            this.paths.Add(new PdfRectangularGraphicsPath(x, y, width, height));
        }

        public void ApplyGraphicsStateParameters(PdfGraphicsStateParameters parameters)
        {
            this.UpdateGraphicsState(this.graphicsState.ApplyParameters(parameters));
        }

        public virtual void BeginPath(PdfPoint startPoint)
        {
            this.paths.Add(new PdfGraphicsPath(startPoint));
        }

        public virtual void BeginText()
        {
            this.SetTextMatrix(new PdfTransformationMatrix());
        }

        public void Clip(bool useNonzeroWindingRule)
        {
            this.ClipUseNonZeroWindingRule = new bool?(useNonzeroWindingRule);
        }

        public virtual void ClipAndClearPaths()
        {
            bool? clipUseNonZeroWindingRule = this.ClipUseNonZeroWindingRule;
            if (clipUseNonZeroWindingRule != null)
            {
                if (this.paths.Count > 0)
                {
                    this.ClipPaths();
                }
                clipUseNonZeroWindingRule = null;
                this.ClipUseNonZeroWindingRule = clipUseNonZeroWindingRule;
            }
            this.paths = new List<PdfGraphicsPath>();
        }

        protected abstract void ClipPaths();
        protected void ClipRectangle(PdfRectangle rectangle)
        {
            this.paths = new List<PdfGraphicsPath>();
            this.AppendRectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
            this.Clip(true);
            this.ClipAndClearPaths();
        }

        public virtual void ClosePath()
        {
            PdfGraphicsPath currentPath = this.CurrentPath;
            if (currentPath != null)
            {
                PdfPoint startPoint = currentPath.StartPoint;
                PdfPoint endPoint = currentPath.EndPoint;
                if ((startPoint.X != endPoint.X) || (startPoint.Y != endPoint.Y))
                {
                    currentPath.AppendLineSegment(currentPath.StartPoint);
                }
                currentPath.Closed = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
        }

        public abstract void DrawForm(PdfForm form);
        public void DrawForm(PdfTransformationMatrix matrix, PdfForm form)
        {
            this.SaveGraphicsState();
            try
            {
                this.UpdateTransformationMatrix(matrix);
                this.DrawForm(form);
            }
            finally
            {
                this.RestoreGraphicsState();
            }
        }

        public abstract void DrawImage(PdfImage image);
        public abstract void DrawShading(PdfShading shading);
        protected abstract void DrawString(PdfStringData stringData);
        public void DrawString(byte[] data, double[] offsets)
        {
            PdfTextState textState = this.graphicsState.TextState;
            PdfFont font = textState.Font;
            if (font != null)
            {
                PdfStringCommandData stringData = font.ActualEncoding.GetStringData(data, offsets);
                byte[][] charCodes = stringData.CharCodes;
                short[] str = stringData.Str;
                double[] numArray2 = stringData.Offsets;
                int length = str.Length;
                double[] advances = new double[length];
                double[] widths = new double[length];
                double characterSpacing = textState.CharacterSpacing;
                double wordSpacing = textState.WordSpacing;
                double num4 = textState.HorizontalScaling / 100.0;
                double fontSize = textState.FontSize;
                double widthFactor = font.WidthFactor;
                double num7 = 0.0;
                int index = 0;
                int num10 = 1;
                while (true)
                {
                    if (index >= length)
                    {
                        double num8 = (numArray2[0] * 0.001) * fontSize;
                        PdfTransformationMatrix textMatrix = textState.TextMatrix;
                        this.graphicsState.TextState.TextMatrix = PdfTransformationMatrix.Multiply(new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, -num8, 0.0), textMatrix);
                        this.DrawString(new PdfStringData(stringData, widths, advances));
                        this.graphicsState.TextState.TextMatrix = PdfTransformationMatrix.Multiply(new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, num7 - num8, 0.0), textMatrix);
                        break;
                    }
                    double num11 = (font.GetCharacterWidth(str[index]) * widthFactor) * fontSize;
                    double num12 = (num11 - ((numArray2[num10] * 0.001) * fontSize)) + characterSpacing;
                    if ((charCodes[index].Length == 1) && (charCodes[index][0] == 0x20))
                    {
                        num12 += wordSpacing;
                    }
                    num12 *= num4;
                    advances[index] = num12;
                    widths[index] = num11;
                    num7 += num12;
                    index++;
                    num10++;
                }
            }
        }

        public virtual void DrawTransparencyGroup(PdfGroupForm form)
        {
            this.DrawForm(form);
        }

        protected virtual void DrawType3FontString(PdfStringData data, PdfType3Font type3Font)
        {
            double[] advances = data.Advances;
            PdfGraphicsState graphicsState = this.GraphicsState;
            PdfTextState textState = graphicsState.TextState;
            PdfTransformationMatrix transformationMatrix = graphicsState.TransformationMatrix;
            PdfTransformationMatrix textMatrix = textState.TextMatrix;
            PdfTransformationMatrix textLineMatrix = textState.TextLineMatrix;
            try
            {
                PdfTransformationMatrix textSpaceMatrix = this.TextSpaceMatrix;
                textSpaceMatrix = PdfTransformationMatrix.Multiply(PdfTransformationMatrix.Multiply(PdfTransformationMatrix.Multiply(type3Font.FontMatrix, textSpaceMatrix), textMatrix), transformationMatrix);
                PdfTransformationMatrix matrix5 = PdfTransformationMatrix.Multiply(textMatrix, transformationMatrix);
                matrix5 = new PdfTransformationMatrix(matrix5.A, matrix5.B, matrix5.C, matrix5.D, 0.0, 0.0);
                this.SetTransformationMatrix(textSpaceMatrix);
                PdfSimpleFontEncoding encoding = type3Font.Encoding;
                short[] str = data.Str;
                int length = str.Length;
                double num2 = 0.0;
                for (int i = 0; i < length; i++)
                {
                    PdfType3FontGlyph glyph = type3Font.GetGlyph(encoding.GetGlyphName((byte) str[i]));
                    if (glyph != null)
                    {
                        this.DrawType3Glyph(glyph);
                    }
                    PdfPoint point = matrix5.Transform(num2 + advances[i], 0.0);
                    this.SetTransformationMatrix(PdfTransformationMatrix.Translate(textSpaceMatrix, point.X, point.Y));
                }
            }
            finally
            {
                textState = this.GraphicsState.TextState;
                textState.TextMatrix = textMatrix;
                textState.TextLineMatrix = textLineMatrix;
                this.SetTransformationMatrix(transformationMatrix);
            }
        }

        protected virtual void DrawType3Glyph(PdfType3FontGlyph glyph)
        {
            this.SaveGraphicsState();
            try
            {
                this.Execute(glyph.Commands);
            }
            finally
            {
                this.RestoreGraphicsState();
            }
        }

        public virtual void EndText()
        {
        }

        public void Execute()
        {
            this.Execute(this.page.Commands);
        }

        protected void Execute(IEnumerable<PdfCommand> commands)
        {
            foreach (PdfCommand command in commands)
            {
                try
                {
                    command.Execute(this);
                }
                catch
                {
                }
            }
        }

        public abstract void FillPaths(bool useNonzeroWindingRule);
        public void MoveToNextLine()
        {
            this.SetTextMatrix(0.0, -this.graphicsState.TextState.Leading);
        }

        public virtual void RestoreGraphicsState()
        {
            if (this.graphicsStateStack.Count > this.MinGraphicsStateCount)
            {
                this.graphicsState = this.graphicsStateStack.Pop();
            }
        }

        public virtual void SaveGraphicsState()
        {
            this.graphicsStateStack.Push(this.graphicsState.Clone());
        }

        public void SetCharacterSpacing(double characterSpacing)
        {
            this.graphicsState.TextState.CharacterSpacing = characterSpacing;
        }

        public virtual void SetColorForNonStrokingOperations(PdfColor color)
        {
            PdfColorSpace nonStrokingColorSpace = this.graphicsState.NonStrokingColorSpace;
            this.graphicsState.NonStrokingColor = (nonStrokingColorSpace == null) ? color : nonStrokingColorSpace.Transform(color);
        }

        public virtual void SetColorForStrokingOperations(PdfColor color)
        {
            PdfColorSpace strokingColorSpace = this.graphicsState.StrokingColorSpace;
            this.graphicsState.StrokingColor = (strokingColorSpace == null) ? color : strokingColorSpace.Transform(color);
        }

        public void SetColorSpaceForNonStrokingOperations(PdfColorSpace colorSpace)
        {
            this.graphicsState.NonStrokingColorSpace = colorSpace;
        }

        public void SetColorSpaceForStrokingOperations(PdfColorSpace colorSpace)
        {
            this.graphicsState.StrokingColorSpace = colorSpace;
        }

        public void SetFlatnessTolerance(double flatnessTolerance)
        {
            this.graphicsState.FlatnessTolerance = flatnessTolerance;
        }

        public virtual void SetFont(PdfFont font, double fontSize)
        {
            PdfTextState textState = this.graphicsState.TextState;
            if (font != null)
            {
                textState.Font = font;
            }
            else
            {
                PdfFontDescriptor fontDescriptor = new PdfFontDescriptor(new PdfFontDescriptorData(new PdfFontMetrics(1854.0, 434.0, 2288.0, 2048.0), PdfFontFlags.None, 0.0, false, 0));
                textState.Font = new PdfTrueTypeFont("Arial", new PdfSimpleFontEncoding("Arial", "WinAnsiEncoding", null), fontDescriptor, 0, null);
            }
            textState.FontSize = fontSize;
        }

        public void SetLineCapStyle(PdfLineCapStyle lineCapStyle)
        {
            this.graphicsState.LineCap = lineCapStyle;
        }

        public void SetLineJoinStyle(PdfLineJoinStyle lineJoinStyle)
        {
            this.graphicsState.LineJoin = lineJoinStyle;
        }

        public void SetLineStyle(PdfLineStyle lineStyle)
        {
            this.graphicsState.LineStyle = lineStyle;
        }

        public void SetLineWidth(double lineWidth)
        {
            this.graphicsState.LineWidth = lineWidth;
        }

        public void SetMiterLimit(double miterLimit)
        {
            this.graphicsState.MiterLimit = miterLimit;
        }

        public void SetRenderingIntent(PdfRenderingIntent renderingIntent)
        {
            this.graphicsState.RenderingIntent = renderingIntent;
        }

        public void SetTextHorizontalScaling(double scaling)
        {
            this.graphicsState.TextState.AbsoluteHorizontalScaling = Math.Abs(scaling);
            this.graphicsState.TextState.HorizontalScaling = scaling;
        }

        public void SetTextLeading(double leading)
        {
            this.graphicsState.TextState.Leading = leading;
        }

        public virtual void SetTextMatrix(PdfTransformationMatrix matrix)
        {
            PdfTextState textState = this.graphicsState.TextState;
            textState.TextLineMatrix = matrix;
            textState.TextMatrix = matrix.Clone();
        }

        public void SetTextMatrix(double offsetTextByX, double offsetTextByY)
        {
            this.SetTextMatrix(PdfTransformationMatrix.Multiply(new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, offsetTextByX, offsetTextByY), this.graphicsState.TextState.TextLineMatrix));
        }

        public virtual void SetTextRenderingMode(PdfTextRenderingMode renderingMode)
        {
            this.graphicsState.TextState.RenderingMode = renderingMode;
        }

        public void SetTextRise(double rise)
        {
            this.graphicsState.TextState.Rise = rise;
        }

        protected virtual void SetTransformationMatrix(PdfTransformationMatrix matrix)
        {
            this.graphicsState.TransformationMatrix = matrix;
        }

        public void SetWordSpacing(double wordSpacing)
        {
            this.graphicsState.TextState.WordSpacing = wordSpacing;
        }

        public abstract void StrokePaths();
        protected abstract void UpdateGraphicsState(PdfGraphicsStateChange change);
        public virtual void UpdateTransformationMatrix(PdfTransformationMatrix matrix)
        {
            this.SetTransformationMatrix(PdfTransformationMatrix.Multiply(matrix, this.graphicsState.TransformationMatrix));
        }

        protected bool? ClipUseNonZeroWindingRule { get; private set; }

        protected int GraphicsStateStackSize =>
            this.graphicsStateStack.Count;

        public PdfResources PageResources =>
            this.page.Resources;

        public PdfGraphicsState GraphicsState =>
            this.graphicsState;

        public PdfPage Page =>
            this.page;

        private PdfGraphicsPath CurrentPath
        {
            get
            {
                int count = this.paths.Count;
                return ((count == 0) ? null : this.paths[count - 1]);
            }
        }

        protected PdfRectangle BoundingBox =>
            this.boundingBox;

        protected IList<PdfGraphicsPath> Paths =>
            this.paths;

        protected PdfTransformationMatrix TextSpaceMatrix =>
            this.graphicsState.TextState.TextSpaceMatrix;

        protected virtual int MinGraphicsStateCount =>
            0;
    }
}

