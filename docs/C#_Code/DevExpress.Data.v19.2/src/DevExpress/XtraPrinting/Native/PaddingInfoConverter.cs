namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using System;

    public class PaddingInfoConverter : StructIntConverter
    {
        public static readonly PaddingInfoConverter Instance = new PaddingInfoConverter();

        private PaddingInfoConverter()
        {
        }

        protected override object CreateObject(int[] values) => 
            new PaddingInfo(values[0], values[1], values[2], values[3], (float) values[4]);

        protected override int[] GetValues(object obj)
        {
            PaddingInfo info = (PaddingInfo) obj;
            return new int[] { info.Left, info.Right, info.Top, info.Bottom, ((int) info.Dpi) };
        }

        public override System.Type Type =>
            typeof(PaddingInfo);
    }
}

