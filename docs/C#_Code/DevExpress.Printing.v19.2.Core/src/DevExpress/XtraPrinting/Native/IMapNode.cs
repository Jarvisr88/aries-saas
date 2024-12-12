namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public interface IMapNode
    {
        RectangleF Bounds { get; }

        Dictionary<string, object> Content { get; }

        string Indexes { get; }
    }
}

