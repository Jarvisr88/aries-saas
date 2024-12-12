namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class BorderRenderer
    {
        private BaseEdit edit;

        public BorderRenderer(BaseEdit edit)
        {
            this.edit = edit;
        }

        public Size ArrangeOverride(Size arrangeSize)
        {
            if (this.CanRenderBorder)
            {
                UIElement editCore = this.EditCore;
                Rect rt = new Rect(new Point(0.0, 0.0), arrangeSize);
                Rect finalRect = this.DeflateRect(rt, this.BorderThickness);
                if (editCore != null)
                {
                    editCore.Arrange(finalRect);
                }
            }
            return arrangeSize;
        }

        protected Size CollapseThickness(Thickness th) => 
            new Size(th.Left + th.Right, th.Top + th.Bottom);

        protected Rect DeflateRect(Rect rt, Thickness thick) => 
            new Rect(rt.Left + thick.Left, rt.Top + thick.Top, Math.Max((double) 0.0, (double) ((rt.Width - thick.Left) - thick.Right)), Math.Max((double) 0.0, (double) ((rt.Height - thick.Top) - thick.Bottom)));

        protected bool IsThicknessNotEmpty(double value) => 
            value > 0.0;

        public Size MeasureOverride(Size constraint)
        {
            if (!this.CanRenderBorder)
            {
                return constraint;
            }
            UIElement editCore = this.EditCore;
            Size size = new Size();
            Size size2 = this.CollapseThickness(this.BorderThickness);
            Size size3 = new Size(0.0, 0.0);
            if (editCore == null)
            {
                return constraint;
            }
            Size size4 = new Size(size2.Width + size3.Width, size2.Height + size3.Height);
            Size availableSize = new Size(Math.Max((double) 0.0, (double) (constraint.Width - size4.Width)), Math.Max((double) 0.0, (double) (constraint.Height - size4.Height)));
            editCore.Measure(availableSize);
            Size desiredSize = editCore.DesiredSize;
            size.Width = desiredSize.Width + size4.Width;
            size.Height = desiredSize.Height + size4.Height;
            return size;
        }

        public void Render(DrawingContext drawingContext)
        {
            if (this.CanRenderBorder)
            {
                if (this.IsThicknessNotEmpty(this.BorderThickness.Left))
                {
                    this.RenderBorderLine(drawingContext, new RenderLineDelegate(this.RenderLeftBorder), this.BorderThickness.Left);
                }
                if (this.IsThicknessNotEmpty(this.BorderThickness.Right))
                {
                    this.RenderBorderLine(drawingContext, new RenderLineDelegate(this.RenderRightBorder), this.BorderThickness.Right);
                }
                if (this.IsThicknessNotEmpty(this.BorderThickness.Top))
                {
                    this.RenderBorderLine(drawingContext, new RenderLineDelegate(this.RenderTopBorder), this.BorderThickness.Top);
                }
                if (this.IsThicknessNotEmpty(this.BorderThickness.Bottom))
                {
                    this.RenderBorderLine(drawingContext, new RenderLineDelegate(this.RenderBottomBorder), this.BorderThickness.Bottom);
                }
                if (this.Edit.Background != null)
                {
                    drawingContext.DrawRectangle(this.Edit.Background, null, this.DeflateRect(new Rect(this.RenderSize), this.BorderThickness));
                }
            }
        }

        protected void RenderBorderLine(DrawingContext context, RenderLineDelegate renderLinkDelegate, double penThickness)
        {
            Pen pen1 = new Pen();
            pen1.Brush = this.BorderBrush;
            Pen pen = pen1;
            pen.Thickness = penThickness;
            renderLinkDelegate(context, pen, pen.Thickness * 0.5);
        }

        private void RenderBottomBorder(DrawingContext context, Pen pen, double lineThickness)
        {
            context.DrawLine(pen, new Point(0.0, this.RenderSize.Height - lineThickness), new Point(this.RenderSize.Width, this.RenderSize.Height - lineThickness));
        }

        private void RenderLeftBorder(DrawingContext context, Pen pen, double lineThickness)
        {
            context.DrawLine(pen, new Point(lineThickness, 0.0), new Point(lineThickness, this.RenderSize.Height));
        }

        private void RenderRightBorder(DrawingContext context, Pen pen, double lineThickness)
        {
            context.DrawLine(pen, new Point(this.RenderSize.Width - lineThickness, 0.0), new Point(this.RenderSize.Width - lineThickness, this.RenderSize.Height));
        }

        private void RenderTopBorder(DrawingContext context, Pen pen, double lineThickness)
        {
            context.DrawLine(pen, new Point(0.0, lineThickness), new Point(this.RenderSize.Width, lineThickness));
        }

        protected BaseEdit Edit =>
            this.edit;

        protected Thickness BorderThickness =>
            this.Edit.BorderThickness;

        protected Brush BorderBrush =>
            this.Edit.GetPrintBorderBrush();

        protected Size RenderSize =>
            this.Edit.RenderSize;

        protected FrameworkElement EditCore =>
            this.Edit.EditCore;

        public virtual bool CanRenderBorder =>
            this.Edit.IsPrintingMode && (this.EditCore != null);

        protected delegate void RenderLineDelegate(DrawingContext context, Pen pen, double lineThickness);
    }
}

