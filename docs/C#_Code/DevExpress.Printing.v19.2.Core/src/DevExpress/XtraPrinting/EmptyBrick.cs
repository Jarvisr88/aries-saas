namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.BrickExporters;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    [BrickExporter(typeof(EmptyBrickExporter))]
    public class EmptyBrick : Brick
    {
        public void UnionRect(Brick brick)
        {
            if ((brick != null) && (brick.Modifier == base.Modifier))
            {
                this.InitialRect = this.InitialRect.IsEmpty ? brick.Rect : RectangleF.Union(this.InitialRect, brick.Rect);
            }
        }

        [Description("Gets the text string, containing the brick type information.")]
        public override string BrickType =>
            "Empty";
    }
}

