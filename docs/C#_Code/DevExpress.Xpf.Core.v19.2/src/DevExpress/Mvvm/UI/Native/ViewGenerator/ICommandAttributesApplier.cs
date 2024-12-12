namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using System;

    public interface ICommandAttributesApplier
    {
        void SetCaption(string caption);
        void SetHint(string hint);
        void SetImageUri(string imageName);
        void SetLargeImageUri(string imageName);
        void SetParameter(string parameterPropertyName);
        void SetPropertyName(string propertyName);
    }
}

