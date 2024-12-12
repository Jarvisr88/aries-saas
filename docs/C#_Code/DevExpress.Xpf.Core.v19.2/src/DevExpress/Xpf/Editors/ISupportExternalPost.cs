namespace DevExpress.Xpf.Editors
{
    using System;

    public interface ISupportExternalPost
    {
        object GetEditableValueForExternalEditor();
        void SetEditableValueFromExternalEditor(object value);
    }
}

