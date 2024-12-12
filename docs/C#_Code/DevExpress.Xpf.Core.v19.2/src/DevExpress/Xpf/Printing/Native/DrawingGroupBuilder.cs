namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Pdf.ContentGeneration;
    using System;
    using System.Windows.Media;

    public class DrawingGroupBuilder : DrawingBuilder<DrawingGroup>
    {
        public DrawingGroupBuilder(DrawingGroup group) : base(group)
        {
        }

        public override void GenerateData(PdfGraphicsCommandConstructor constructor)
        {
            if (base.Drawing.Opacity != 0.0)
            {
                constructor.DoWithState(delegate {
                    foreach (Drawing drawing in this.Drawing.Children)
                    {
                        DrawingBuilder builder = Create(drawing);
                        if (builder != null)
                        {
                            builder.GenerateData(constructor);
                        }
                    }
                }, base.Drawing.Transform, base.Drawing.ClipGeometry);
            }
        }
    }
}

