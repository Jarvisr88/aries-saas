namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.Model;
    using System;

    public class ModelShapeGradientFill : DrawingGradientFill
    {
        private readonly ShapeStyle shapeStyle;

        public ModelShapeGradientFill(IDocumentModel documentModel, ShapeStyle shapeStyle) : base(documentModel)
        {
            this.shapeStyle = shapeStyle;
        }

        public override IDrawingFill CloneTo(IDocumentModel documentModel)
        {
            DrawingGradientFill fill = new DrawingGradientFill(documentModel);
            fill.BeginUpdate();
            try
            {
                fill.GradientType = base.GradientType;
                fill.Flip = base.Flip;
                fill.RotateWithShape = base.RotateWithShape;
                fill.Scaled = base.Scaled;
                fill.Angle = base.Angle;
                fill.TileRect = base.TileRect;
                fill.FillRect = base.FillRect;
                foreach (DrawingGradientStop stop in base.GradientStops)
                {
                    DrawingGradientStop stop2 = new DrawingGradientStop(documentModel);
                    stop2.CopyFrom(stop);
                    fill.AddGradientStop(stop2);
                }
            }
            finally
            {
                fill.EndUpdate();
            }
            return fill;
        }

        protected override int GetDefaultAngle()
        {
            DrawingGradientFill themeFill = this.GetThemeFill();
            return ((themeFill == null) ? base.GetDefaultAngle() : themeFill.Angle);
        }

        protected override TileFlipType GetDefaultFlip()
        {
            DrawingGradientFill themeFill = this.GetThemeFill();
            return ((themeFill == null) ? base.GetDefaultFlip() : themeFill.Flip);
        }

        protected override GradientType GetDefaultGradientType()
        {
            DrawingGradientFill themeFill = this.GetThemeFill();
            return ((themeFill == null) ? base.GetDefaultGradientType() : themeFill.GradientType);
        }

        protected override bool GetDefaultRotateWithShape()
        {
            DrawingGradientFill themeFill = this.GetThemeFill();
            return ((themeFill == null) ? base.GetDefaultRotateWithShape() : themeFill.RotateWithShape);
        }

        protected override bool GetDefaultScaled()
        {
            DrawingGradientFill themeFill = this.GetThemeFill();
            return ((themeFill == null) ? base.GetDefaultScaled() : themeFill.Scaled);
        }

        protected override RectangleOffset GetFillRect()
        {
            RectangleOffset fillRect = base.GetFillRect();
            if (fillRect.Equals(RectangleOffset.Empty))
            {
                DrawingGradientFill themeFill = this.GetThemeFill();
                if (themeFill != null)
                {
                    return themeFill.FillRect;
                }
            }
            return fillRect;
        }

        protected override GradientStopCollection GetGradientStops()
        {
            GradientStopCollection gradientStops = base.GetGradientStops();
            if (gradientStops.Count == 0)
            {
                DrawingGradientFill themeFill = this.GetThemeFill();
                if ((themeFill != null) && (themeFill.GradientStops.Count > 0))
                {
                    return themeFill.GradientStops;
                }
            }
            return gradientStops;
        }

        private DrawingGradientFill GetThemeFill()
        {
            IOfficeTheme officeTheme = base.DocumentModel.OfficeTheme;
            return (((officeTheme == null) || (officeTheme.FormatScheme == null)) ? null : (officeTheme.FormatScheme.GetFill(this.shapeStyle.FillReferenceIdx) as DrawingGradientFill));
        }

        protected override RectangleOffset GetTileRect()
        {
            RectangleOffset tileRect = base.GetTileRect();
            if (tileRect.Equals(RectangleOffset.Empty))
            {
                DrawingGradientFill themeFill = this.GetThemeFill();
                if (themeFill != null)
                {
                    return themeFill.TileRect;
                }
            }
            return tileRect;
        }
    }
}

