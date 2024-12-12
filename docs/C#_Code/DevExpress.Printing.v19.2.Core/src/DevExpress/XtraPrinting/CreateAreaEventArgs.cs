namespace DevExpress.XtraPrinting
{
    using System;

    public class CreateAreaEventArgs : EventArgs
    {
        private BrickGraphics graph;

        internal CreateAreaEventArgs(BrickGraphics graph)
        {
            this.graph = graph;
        }

        public BrickGraphics Graph =>
            this.graph;
    }
}

