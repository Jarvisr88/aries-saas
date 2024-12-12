namespace DevExpress.Printing.Core.PdfExport.Metafile
{
    using DevExpress.XtraPrinting.Export.Pdf;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    public class MetaState
    {
        private PolyFillMode polygonFillMode;
        internal MetafileObjectTable objectTable;

        public MetaState()
        {
            this.polygonFillMode = PolyFillMode.ALTERNATE;
            this.objectTable = new MetafileObjectTable();
            this.LogicalDpi = new Point(0x60, 0x60);
        }

        public MetaState(Image image) : this()
        {
            this.HorizontalResolution = 72f;
            this.VerticalResolution = 72f;
            if (image != null)
            {
                this.ImageSize = image.Size;
                this.HorizontalResolution = image.HorizontalResolution;
                this.VerticalResolution = image.VerticalResolution;
            }
        }

        public void AddObject(object metaObject)
        {
            for (int i = 0; i < this.objectTable.Count; i++)
            {
                if (this.objectTable[i] == null)
                {
                    this.objectTable[i] = metaObject;
                    return;
                }
            }
            this.objectTable.Add(metaObject);
        }

        public void AddObject(int index, object metaObject)
        {
            this.objectTable[index] = metaObject;
        }

        public object GetObject(int index) => 
            this.objectTable[index];

        public void RemoveObject(int objectIndex)
        {
            this.objectTable.Remove(objectIndex);
        }

        public void SelectObject(int objectIndex, PdfDrawContext template)
        {
            object obj2 = this.objectTable[objectIndex];
            this.SelectObject(obj2, template);
        }

        public unsafe void SelectObject(object obj, PdfDrawContext template)
        {
            switch (obj)
            {
                case (WmfFontObject _):
                    this.CurrentFont = (WmfFontObject) obj;
                    break;

                case (Brush _):
                {
                    Brush brush = (Brush) obj;
                    this.CurrentBrush = brush;
                    if (brush is SolidBrush)
                    {
                        Color color = ((SolidBrush) this.CurrentBrush).Color;
                        template.SetRGBFillColor(color);
                    }
                    else if (brush is HatchBrush)
                    {
                        Color backgroundColor = ((HatchBrush) brush).BackgroundColor;
                        template.SetRGBFillColor(backgroundColor);
                    }
                    else if (brush is LinearGradientBrush)
                    {
                        Color color = ((LinearGradientBrush) brush).LinearColors[0];
                        template.SetRGBFillColor(color);
                    }
                    break;
                }
                default:
                    if (obj is Pen)
                    {
                        Pen pen = (Pen) obj;
                        this.CurrentPen = pen;
                        if (pen.Color != Color.Transparent)
                        {
                            float[] dashPattern;
                            template.SetRGBStrokeColor(pen.Color);
                            float width = pen.Width;
                            if (!this.WindowExtent.IsEmpty)
                            {
                                width *= ((float) this.ImageSize.Height) / this.WindowExtent.Y;
                            }
                            width *= 72f / this.VerticalResolution;
                            template.SetLineWidth(Math.Abs(width));
                            switch (pen.DashStyle)
                            {
                                case DashStyle.Solid:
                                    dashPattern = new float[0];
                                    break;

                                case DashStyle.Dash:
                                    dashPattern = new float[] { 3f, 1f };
                                    break;

                                case DashStyle.Dot:
                                    dashPattern = new float[] { 1f };
                                    break;

                                case DashStyle.DashDot:
                                    dashPattern = new float[] { 3f, 1f, 1f, 1f };
                                    break;

                                case DashStyle.DashDotDot:
                                    dashPattern = new float[] { 3f, 1f, 1f, 1f, 1f, 1f };
                                    break;

                                case DashStyle.Custom:
                                    dashPattern = pen.DashPattern;
                                    break;

                                default:
                                    dashPattern = new float[0];
                                    break;
                            }
                            int index = 0;
                            while (true)
                            {
                                if (index >= dashPattern.Length)
                                {
                                    template.SetDash(dashPattern, 0);
                                    break;
                                }
                                float* singlePtr1 = &(dashPattern[index]);
                                singlePtr1[0] *= pen.Width;
                                index++;
                            }
                        }
                    }
                    break;
            }
        }

        public void SetMapMode(MapMode mapMode)
        {
        }

        public PointF[] Transform(PointF[] points)
        {
            PointF[] tfArray = new PointF[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                tfArray[i] = this.Transform(points[i]);
            }
            return tfArray;
        }

        public PointF Transform(Point point) => 
            this.Transform(new PointF((float) point.X, (float) point.Y));

        public PointF Transform(PointF point) => 
            new PointF(this.TransformX(point.X), this.TransformY(point.Y));

        public RectangleF Transform(RectangleF rectangle) => 
            new RectangleF(this.TransformX(rectangle.X), this.TransformY(rectangle.Y), this.TransformX(rectangle.Width) - this.TransformX(0f), this.TransformY(rectangle.Height) - this.TransformY(0f));

        internal float TransformX(float x)
        {
            float num = x - this.WindowOrigin.X;
            if (!this.ImageSize.IsEmpty && !this.WindowExtent.IsEmpty)
            {
                num = (num * (((float) this.ImageSize.Width) / this.WindowExtent.X)) * (72f / this.HorizontalResolution);
            }
            return num;
        }

        internal float TransformY(float y)
        {
            float num = y - this.WindowOrigin.Y;
            if (!this.ImageSize.IsEmpty)
            {
                if (!this.WindowExtent.IsEmpty)
                {
                    num *= ((float) this.ImageSize.Height) / this.WindowExtent.Y;
                }
                num = (this.ImageSize.Height - num) * (72f / this.VerticalResolution);
            }
            return num;
        }

        internal Color BackgroundColor { get; set; }

        internal Color TextColor { get; set; }

        internal MixMode BackgroundMode { get; set; }

        internal TextAlignmentMode TextAlign { get; set; }

        internal WmfFontObject CurrentFont { get; set; }

        internal Brush CurrentBrush { get; set; }

        internal Pen CurrentPen { get; set; }

        internal Point LogicalDpi { get; set; }

        public Point WindowOrigin { get; set; }

        public PointF WindowExtent { get; set; }

        public PolyFillMode PolygonFillMode
        {
            get => 
                this.polygonFillMode;
            set => 
                this.polygonFillMode = value;
        }

        public Size ImageSize { get; set; }

        public float HorizontalResolution { get; set; }

        public float VerticalResolution { get; set; }
    }
}

