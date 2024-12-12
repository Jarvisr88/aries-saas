namespace DevExpress.Xpf.Editors
{
    using DevExpress.XtraPrinting.BarCode.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class BarCodeStyleSettingsStorage
    {
        private static Dictionary<BarCodeSymbology, Type> storage = new Dictionary<BarCodeSymbology, Type>();

        static BarCodeStyleSettingsStorage()
        {
            storage[BarCodeSymbology.Codabar] = typeof(CodabarStyleSettings);
            storage[BarCodeSymbology.Industrial2of5] = typeof(Industrial2of5StyleSettings);
            storage[BarCodeSymbology.Interleaved2of5] = typeof(Interleaved2of5StyleSettings);
            storage[BarCodeSymbology.Code39] = typeof(Code39StyleSettings);
            storage[BarCodeSymbology.Code39Extended] = typeof(Code39ExtendedStyleSettings);
            storage[BarCodeSymbology.Code93] = typeof(Code93StyleSettings);
            storage[BarCodeSymbology.Code93Extended] = typeof(Code93ExtendedStyleSettings);
            storage[BarCodeSymbology.Code128] = typeof(Code128StyleSettings);
            storage[BarCodeSymbology.Code11] = typeof(Code11StyleSettings);
            storage[BarCodeSymbology.CodeMSI] = typeof(CodeMSIStyleSettings);
            storage[BarCodeSymbology.PostNet] = typeof(PostNetStyleSettings);
            storage[BarCodeSymbology.EAN13] = typeof(EAN13StyleSettings);
            storage[BarCodeSymbology.UPCA] = typeof(UPCAStyleSettings);
            storage[BarCodeSymbology.EAN8] = typeof(EAN8StyleSettings);
            storage[BarCodeSymbology.EAN128] = typeof(EAN128StyleSettings);
            storage[BarCodeSymbology.UPCSupplemental2] = typeof(UPCSupplemental2StyleSettings);
            storage[BarCodeSymbology.UPCSupplemental5] = typeof(UPCSupplemental5StyleSettings);
            storage[BarCodeSymbology.UPCE0] = typeof(UPCE0StyleSettings);
            storage[BarCodeSymbology.UPCE1] = typeof(UPCE1StyleSettings);
            storage[BarCodeSymbology.Matrix2of5] = typeof(Matrix2of5StyleSettings);
            storage[BarCodeSymbology.PDF417] = typeof(PDF417StyleSettings);
            storage[BarCodeSymbology.DataMatrix] = typeof(DataMatrixStyleSettings);
            storage[BarCodeSymbology.DataMatrixGS1] = typeof(DataMatrixGS1StyleSettings);
            storage[BarCodeSymbology.QRCode] = typeof(QRCodeStyleSettings);
            storage[BarCodeSymbology.IntelligentMail] = typeof(IntelligentMailStyleSettings);
            storage[BarCodeSymbology.ITF14] = typeof(ITF14StyleSettings);
            storage[BarCodeSymbology.DataBar] = typeof(DataBarStyleSettings);
        }

        public static BarCodeStyleSettings Create(BarCodeSymbology symbologyCode)
        {
            Type type = storage[symbologyCode];
            if (type == null)
            {
                throw new ArgumentException();
            }
            return (BarCodeStyleSettings) Activator.CreateInstance(type);
        }

        public static IEnumerable<Type> GetSymbologyTypes()
        {
            Func<Type, string> keySelector = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<Type, string> local1 = <>c.<>9__3_0;
                keySelector = <>c.<>9__3_0 = s => s.Name;
            }
            return storage.Values.OrderBy<Type, string>(keySelector);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarCodeStyleSettingsStorage.<>c <>9 = new BarCodeStyleSettingsStorage.<>c();
            public static Func<Type, string> <>9__3_0;

            internal string <GetSymbologyTypes>b__3_0(Type s) => 
                s.Name;
        }
    }
}

