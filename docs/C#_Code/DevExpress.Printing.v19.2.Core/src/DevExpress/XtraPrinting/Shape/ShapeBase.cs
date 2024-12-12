namespace DevExpress.XtraPrinting.Shape
{
    using DevExpress.Printing.Utils.DocumentStoring;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Shape.Native;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public abstract class ShapeBase : StorableObjectBase, ICloneable
    {
        protected ShapeBase()
        {
        }

        protected virtual RectangleF AdjustClientRectangle(RectangleF clientBounds, float lineWidth) => 
            clientBounds;

        protected abstract ShapeBase CloneShape();
        protected internal abstract ShapeCommandCollection CreateCommands(RectangleF bounds, int angle);
        protected static RectangleF DeflateLineWidth(RectangleF rect, float lineWidth) => 
            RectangleF.Inflate(rect, -lineWidth / 2f, -lineWidth / 2f);

        internal void DrawContent(IGraphics gr, RectangleF clientBounds, IShapeDrawingInfo info)
        {
            clientBounds = this.AdjustClientRectangle(clientBounds, info.LineWidth);
            ShapeCommandCollection commands = this.CreateCommands(clientBounds, info.Angle);
            BoundedCommandsRotator.Rotate(commands, clientBounds, info.Angle, info.Stretch);
            Brush brush = gr.GetBrush(info.FillColor);
            using (Pen pen = new Pen(info.ForeColor, info.LineWidth))
            {
                if (info.LineStyle != DashStyle.Custom)
                {
                    if (info.LineStyle == DashStyle.Solid)
                    {
                        pen.DashStyle = DashStyle.Solid;
                    }
                    else
                    {
                        pen.DashStyle = DashStyle.Custom;
                        pen.DashPattern = VisualBrick.GetDashPattern(info.LineStyle);
                    }
                }
                commands.Iterate(new ShapeCommandPainter(gr, pen, brush));
            }
        }

        object ICloneable.Clone() => 
            this.CloneShape();

        [Browsable(false), XtraSerializableProperty]
        public string ShapeName =>
            this.ShapeId.ToString();

        internal abstract DevExpress.XtraPrinting.Shape.Native.ShapeId ShapeId { get; }

        protected internal virtual bool SupportsFillColor =>
            true;
    }
}

