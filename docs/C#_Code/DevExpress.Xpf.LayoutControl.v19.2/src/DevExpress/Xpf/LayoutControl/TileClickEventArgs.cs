namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Runtime.CompilerServices;

    public class TileClickEventArgs : EventArgs
    {
        public TileClickEventArgs(DevExpress.Xpf.LayoutControl.Tile tile)
        {
            this.Tile = tile;
        }

        public DevExpress.Xpf.LayoutControl.Tile Tile { get; private set; }
    }
}

