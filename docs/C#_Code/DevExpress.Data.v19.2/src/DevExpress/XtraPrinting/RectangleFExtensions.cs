namespace DevExpress.XtraPrinting
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    [Obsolete("Use the GraphicsUnitConverter class methods."), EditorBrowsable(EditorBrowsableState.Never)]
    public static class RectangleFExtensions
    {
        [Obsolete("Use the GraphicsUnitConverter.Convert method."), EditorBrowsable(EditorBrowsableState.Never)]
        public static RectangleF Convert(this RectangleF rect, float fromDpi, float toDpi) => 
            GraphicsUnitConverter.Convert(rect, fromDpi, toDpi);
    }
}

