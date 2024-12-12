namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using System;

    public interface IDisplayFormatAttributesApplier
    {
        void SetDisplayFormat(string format);
        void SetNotConvertEmptyStringsToNull();
        void SetNullText(string nullText);
    }
}

