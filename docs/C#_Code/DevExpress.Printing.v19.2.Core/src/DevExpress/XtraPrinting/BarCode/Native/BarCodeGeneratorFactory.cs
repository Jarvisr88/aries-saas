namespace DevExpress.XtraPrinting.BarCode.Native
{
    using DevExpress.XtraPrinting.BarCode;
    using System;
    using System.Collections.Generic;

    public static class BarCodeGeneratorFactory
    {
        private static Type defaultCodeType;
        private static Dictionary<BarCodeSymbology, Type> codesHT;

        static BarCodeGeneratorFactory();
        public static BarCodeGeneratorBase Create(BarCodeSymbology symbologyCode);
    }
}

