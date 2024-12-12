namespace DevExpress.XtraPrinting
{
    using DevExpress.XtraPrinting.BrickExporters;
    using System;
    using System.ComponentModel;
    using System.Drawing;

    [BrickExporter(typeof(UserVisualBrickExporter))]
    public class UserVisualBrick : Brick
    {
        private IBrick userBrick;

        internal UserVisualBrick(IBrick userBrick)
        {
            this.userBrick = userBrick;
        }

        public override float ValidatePageBottom(RectangleF pageBounds, bool enforceSplitNonSeparable, RectangleF rect, IPrintingSystemContext context) => 
            (this.userBrick is Brick) ? ((Brick) this.userBrick).ValidatePageBottom(pageBounds, enforceSplitNonSeparable, rect, context) : base.ValidatePageBottom(pageBounds, enforceSplitNonSeparable, rect, context);

        public override float ValidatePageRight(float pageRight, RectangleF rect) => 
            (this.userBrick is Brick) ? ((Brick) this.userBrick).ValidatePageRight(pageRight, rect) : base.ValidatePageRight(pageRight, rect);

        [Description("Gets a user implementation of the IBrick interface drawn via the DrawBrick method.")]
        public IBrick UserBrick =>
            this.userBrick;

        [Description("Gets the text string, containing the brick type information.")]
        public override string BrickType =>
            "Default";
    }
}

