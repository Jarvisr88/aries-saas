namespace DevExpress.XtraPrinting.NativeBricks
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    [BrickExporter(typeof(XETextPanelBrickExporter))]
    public class XETextPanelBrick : PanelBrick
    {
        public XETextPanelBrick()
        {
        }

        public XETextPanelBrick(BrickStyle style) : base(style)
        {
        }

        private XETextPanelBrick(XETextPanelBrick panelBrick) : base(panelBrick)
        {
        }

        public override object Clone()
        {
            PanelBrick brick = new XETextPanelBrick(this);
            foreach (Brick brick2 in this.Bricks)
            {
                brick.Bricks.Add((Brick) brick2.Clone());
            }
            return brick;
        }

        private RectangleF GetChildrenRegion()
        {
            RectangleF empty = RectangleF.Empty;
            for (int i = 0; i < this.Bricks.Count; i++)
            {
                empty = (i != 0) ? RectangleF.Union(empty, this.Bricks[i].Rect) : this.Bricks[i].Rect;
            }
            return empty;
        }

        protected internal override void OnAfterMerge()
        {
            RectangleF childrenRegion = this.GetChildrenRegion();
            float dy = ((this.InitialRect.Height - childrenRegion.Height) / 2f) - childrenRegion.Y;
            foreach (Brick brick in this.Bricks)
            {
                brick.InitialRect = RectFBase.Offset(brick.InitialRect, 0f, dy);
            }
        }

        public override string BrickType =>
            "XETextPanel";
    }
}

