namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BarCode;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class BarCodePainter : System.Windows.Controls.Control, IGraphicsBase
    {
        public static readonly DependencyProperty SymbologyProperty;
        public static readonly DependencyProperty BarCodeEditProperty;
        private Grid grid;
        private Canvas canvas;
        private Path path;
        private Path backgroundPath;
        protected const int WidthAndHeightMaximumDiscrepancy = 8;
        protected const int mMinHeight = 0x12;

        static BarCodePainter()
        {
            SymbologyProperty = DependencyPropertyManager.Register("Symbology", typeof(BarCodeStyleSettings), typeof(BarCodePainter), new PropertyMetadata(null, (d, e) => ((BarCodePainter) d).SymbologyChanged(e)));
            BarCodeEditProperty = DependencyPropertyManager.Register("BarCodeEdit", typeof(DevExpress.Xpf.Editors.BarCodeEdit), typeof(BarCodePainter), new PropertyMetadata(null, (d, e) => ((BarCodePainter) d).OnBarCodeEditChanged(e)));
        }

        public BarCodePainter()
        {
            this.GraphicsBase.PageUnit = GraphicsUnit.Pixel;
            base.DefaultStyleKey = typeof(BarCodePainter);
            base.SizeChanged += new SizeChangedEventHandler(this.BarCodePainter_SizeChanged);
        }

        private void BarCodePainter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.InvalidateBarCodePainter();
        }

        void IGraphicsBase.ApplyTransformState(MatrixOrder order, bool removeState)
        {
        }

        void IGraphicsBase.DrawCheckBox(RectangleF rect, CheckState state)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.DrawEllipse(System.Drawing.Pen pen, RectangleF rect)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.DrawEllipse(System.Drawing.Pen pen, float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.DrawImage(System.Drawing.Image image, System.Drawing.Point point)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.DrawImage(System.Drawing.Image image, RectangleF rect)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.DrawImage(System.Drawing.Image image, RectangleF rect, System.Drawing.Color underlyingColor)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.DrawLine(System.Drawing.Pen pen, PointF pt1, PointF pt2)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.DrawLine(System.Drawing.Pen pen, float x1, float y1, float x2, float y2)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.DrawLines(System.Drawing.Pen pen, PointF[] points)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.DrawPath(System.Drawing.Pen pen, GraphicsPath path)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.DrawRectangle(System.Drawing.Pen pen, RectangleF bounds)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.DrawString(string s, Font font, System.Drawing.Brush brush, PointF point)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.DrawString(string s, Font font, System.Drawing.Brush brush, RectangleF bounds)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.DrawString(string s, Font font, System.Drawing.Brush brush, PointF point, StringFormat format)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.DrawString(string text, Font font, System.Drawing.Brush brush, RectangleF bounds, StringFormat format)
        {
            this.DrawText(text, bounds, format.Alignment);
        }

        void IGraphicsBase.FillEllipse(System.Drawing.Brush brush, RectangleF rect)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.FillEllipse(System.Drawing.Brush brush, float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.FillPath(System.Drawing.Brush brush, GraphicsPath path)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.FillRectangle(System.Drawing.Brush brush, RectangleF bounds)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.FillRectangle(System.Drawing.Brush brush, float x, float y, float width, float height)
        {
            this.FillRectangle(brush, x, y, width, height);
        }

        System.Drawing.Brush IGraphicsBase.GetBrush(System.Drawing.Color color) => 
            new SolidBrush(color);

        void IGraphicsBase.IntersectClip(GraphicsPath path)
        {
            throw new NotImplementedException();
        }

        SizeF IGraphicsBase.MeasureString(string text, Font font, GraphicsUnit graphicsUnit) => 
            this.MesureString(text);

        SizeF IGraphicsBase.MeasureString(string text, Font font, PointF location, StringFormat stringFormat, GraphicsUnit graphicsUnit) => 
            this.MesureString(text);

        SizeF IGraphicsBase.MeasureString(string text, Font font, SizeF size, StringFormat stringFormat, GraphicsUnit graphicsUnit)
        {
            throw new NotImplementedException();
        }

        SizeF IGraphicsBase.MeasureString(string text, Font font, float width, StringFormat stringFormat, GraphicsUnit graphicsUnit) => 
            this.MesureString(text);

        void IGraphicsBase.ResetTransform()
        {
        }

        void IGraphicsBase.Restore(IGraphicsState gstate)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.RotateTransform(float angle)
        {
        }

        void IGraphicsBase.RotateTransform(float angle, MatrixOrder order)
        {
            throw new NotImplementedException();
        }

        IGraphicsState IGraphicsBase.Save()
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.SaveTransformState()
        {
        }

        void IGraphicsBase.ScaleTransform(float sx, float sy)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.ScaleTransform(float sx, float sy, MatrixOrder order)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.TranslateTransform(float dx, float dy)
        {
            throw new NotImplementedException();
        }

        void IGraphicsBase.TranslateTransform(float dx, float dy, MatrixOrder matrixOrder)
        {
        }

        protected virtual void DrawText(string text, RectangleF bounds, StringAlignment stringAlignment)
        {
            FormattedText formattedText = this.GetFormattedText(text);
            TextBlock block1 = new TextBlock();
            block1.Text = formattedText.Text;
            block1.Foreground = new SolidColorBrush(Colors.Black);
            TextBlock element = block1;
            Canvas.SetLeft(element, (double) bounds.X);
            Canvas.SetTop(element, (double) bounds.Y);
            element.Width = bounds.Width;
            element.Height = bounds.Height;
            element.TextAlignment = WindowsFormsHelper.ConvertStringToTextAlignment(stringAlignment);
            this.canvas.Children.Add(element);
        }

        protected virtual void FillRectangle(System.Drawing.Brush brush, float x, float y, float width, float height)
        {
            System.Windows.Media.Color color = WindowsFormsHelper.ConvertBrushToColor(brush);
            Rect rect = new Rect(Math.Floor((double) x), Math.Floor((double) y), Math.Ceiling((double) width), Math.Ceiling((double) height));
            if (color == ((SolidColorBrush) this.backgroundPath.Fill).Color)
            {
                RectangleGeometry geometry1 = new RectangleGeometry();
                geometry1.Rect = rect;
                this.backgroundPath.Data = geometry1;
            }
            else
            {
                RectangleGeometry geometry2 = new RectangleGeometry();
                geometry2.Rect = rect;
                this.GeometryGroup.Children.Add(geometry2);
            }
        }

        private FormattedText GetFormattedText(string s) => 
            new FormattedText(s, CultureInfo.CurrentUICulture, base.FlowDirection, new Typeface(base.FontFamily.Source), base.FontSize, base.Foreground);

        private RectangleF GetPainterRect() => 
            new RectangleF(0f, 0f, (float) base.ActualWidth, (float) base.ActualHeight);

        public virtual void InvalidateBarCodePainter()
        {
            if ((this.Symbology != null) && ((this.grid != null) && (this.BarCodeData != null)))
            {
                this.grid.Children.Clear();
                System.Windows.Media.GeometryGroup group1 = new System.Windows.Media.GeometryGroup();
                group1.FillRule = FillRule.Nonzero;
                Path path1 = new Path();
                path1.Data = group1;
                path1.Fill = WindowsFormsHelper.ConvertColorToBrush(this.BarCodeData.Style.ForeColor);
                this.path = path1;
                Path path2 = new Path();
                path2.Data = new System.Windows.Media.GeometryGroup();
                path2.Fill = WindowsFormsHelper.ConvertColorToBrush(this.BarCodeData.Style.BackColor);
                this.backgroundPath = path2;
                this.grid.Children.Add(this.backgroundPath);
                this.grid.Children.Add(this.path);
                this.canvas.Children.Clear();
                this.UpdateSymbology();
                this.Symbology.GeneratorBase.DrawContent(this, this.GetPainterRect(), this.BarCodeData);
            }
        }

        protected override System.Windows.Size MeasureOverride(System.Windows.Size availableSize)
        {
            System.Windows.Size size = base.MeasureOverride(availableSize);
            if (this.Symbology == null)
            {
                return size;
            }
            DevExpress.Xpf.Editors.Internal.BarCodeData data = new DevExpress.Xpf.Editors.Internal.BarCodeData(this.BarCodeData) {
                AutoModule = false
            };
            BarCodeDrawingViewInfo viewInfo = new BarCodeDrawingViewInfo(new RectangleF(0f, 0f, float.PositiveInfinity, float.PositiveInfinity), data);
            this.Symbology.GeneratorBase.CalculateDrawingViewInfo(viewInfo, this);
            double num = Math.Max(viewInfo.BestSize.Width, viewInfo.BestSize.Height / 8f);
            double num2 = Math.Max(viewInfo.BestSize.Height, viewInfo.BestSize.Width / 8f);
            double width = double.IsInfinity(availableSize.Width) ? num : Math.Min(availableSize.Width, num);
            double num4 = double.IsInfinity(availableSize.Height) ? num2 : Math.Min(availableSize.Height, num2);
            return new System.Windows.Size(width, (this.BarCodeEdit.EditMode != EditMode.Standalone) ? Math.Min(num4, 18.0) : Math.Max(num4, 18.0));
        }

        private SizeF MesureString(string text)
        {
            TextBlock block1 = new TextBlock();
            block1.Text = text;
            TextBlock block = block1;
            block.Measure(new System.Windows.Size(double.MaxValue, double.MaxValue));
            return new SizeF((float) block.DesiredSize.Width, (float) block.DesiredSize.Height);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.grid = (Grid) base.GetTemplateChild("Part_Grid");
            this.canvas = (Canvas) base.GetTemplateChild("Part_Canvas");
            this.InvalidateBarCodePainter();
        }

        private void OnBarCodeEditChanged(DependencyPropertyChangedEventArgs e)
        {
            this.BarCodeEdit.Do<DevExpress.Xpf.Editors.BarCodeEdit>(x => x.BarCodePainter = this);
        }

        private void SymbologyChanged(DependencyPropertyChangedEventArgs e)
        {
            this.InvalidateBarCodePainter();
        }

        public virtual void UpdateSymbology()
        {
            if (this.Symbology != null)
            {
                BarCode2DGenerator generatorBase = this.Symbology.GeneratorBase as BarCode2DGenerator;
                if (generatorBase != null)
                {
                    generatorBase.Update(this.BarCodeData.Text, this.BarCodeData.BinaryData);
                }
            }
        }

        public BarCodeStyleSettings Symbology
        {
            get => 
                (BarCodeStyleSettings) base.GetValue(SymbologyProperty);
            set => 
                base.SetValue(SymbologyProperty, value);
        }

        public DevExpress.Xpf.Editors.BarCodeEdit BarCodeEdit
        {
            get => 
                (DevExpress.Xpf.Editors.BarCodeEdit) base.GetValue(BarCodeEditProperty);
            set => 
                base.SetValue(BarCodeEditProperty, value);
        }

        public IFullBarCodeData BarCodeData =>
            this.BarCodeEdit;

        RectangleF IGraphicsBase.ClipBounds
        {
            get => 
                new RectangleF(0f, 0f, (float) base.Width, (float) base.Height);
            set
            {
            }
        }

        private IGraphicsBase GraphicsBase =>
            this;

        private System.Windows.Media.GeometryGroup GeometryGroup =>
            (System.Windows.Media.GeometryGroup) this.path.Data;

        GraphicsUnit IGraphicsBase.PageUnit { get; set; }

        SmoothingMode IGraphicsBase.SmoothingMode
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        System.Drawing.Drawing2D.Matrix IGraphicsBase.Transform
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarCodePainter.<>c <>9 = new BarCodePainter.<>c();

            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BarCodePainter) d).SymbologyChanged(e);
            }

            internal void <.cctor>b__2_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BarCodePainter) d).OnBarCodeEditChanged(e);
            }
        }
    }
}

