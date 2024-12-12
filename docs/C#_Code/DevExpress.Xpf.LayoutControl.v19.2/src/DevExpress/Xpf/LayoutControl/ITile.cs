namespace DevExpress.Xpf.LayoutControl
{
    using System;

    public interface ITile
    {
        void Click();

        TileSize Size { get; }
    }
}

