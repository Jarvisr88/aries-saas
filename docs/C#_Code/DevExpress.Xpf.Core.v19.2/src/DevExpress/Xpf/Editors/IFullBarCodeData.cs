namespace DevExpress.Xpf.Editors
{
    using DevExpress.XtraPrinting.BarCode;
    using System;

    public interface IFullBarCodeData : IBarCodeData
    {
        byte[] BinaryData { get; }
    }
}

