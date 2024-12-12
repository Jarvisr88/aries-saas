namespace DevExpress.XtraEditors
{
    using System;

    public interface IRangeControlClientExtension : IRangeControlClient
    {
        object NativeValue(double normalizedValue);
    }
}

