namespace DevExpress.Mvvm.UI.Native.ViewGenerator
{
    using System;

    public interface IEditorAttributesApplier : IDisplayFormatAttributesApplier
    {
        void SetDescription(string description);
        void SetDisplayMember(string propertyName);
        void SetDisplayName(string shortName, string name);
        void SetEditable(bool? editable);
        void SetHidden();
        void SetInvisible();
        void SetReadOnly();
        void SetRequired();
    }
}

