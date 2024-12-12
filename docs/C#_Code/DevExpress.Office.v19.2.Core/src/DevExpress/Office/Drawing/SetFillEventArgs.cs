namespace DevExpress.Office.Drawing
{
    using System;

    public class SetFillEventArgs : EventArgs
    {
        private readonly IDrawingFill fill;

        public SetFillEventArgs(IDrawingFill fill)
        {
            this.fill = fill;
        }

        public IDrawingFill Fill =>
            this.fill;
    }
}

