namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public static class GraphicsUnitExtensions
    {
        public static float ToDpi(this GraphicsUnit unit) => 
            GraphicsDpi.UnitToDpi(unit);

        public static float ToDpiI(this GraphicsUnit unit) => 
            GraphicsDpi.UnitToDpiI(unit);
    }
}

