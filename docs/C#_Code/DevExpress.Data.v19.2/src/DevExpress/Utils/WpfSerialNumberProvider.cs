namespace DevExpress.Utils
{
    using DevExpress.Utils.About;
    using System;
    using System.ComponentModel;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public static class WpfSerialNumberProvider
    {
        public static string GetSerial() => 
            Utility.GetSerial(ProductKind.Default | ProductKind.DXperienceWPF, ProductInfoStage.Registered);
    }
}

