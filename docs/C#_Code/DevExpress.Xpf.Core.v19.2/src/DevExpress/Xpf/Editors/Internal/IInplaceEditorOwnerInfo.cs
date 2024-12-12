namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;

    public interface IInplaceEditorOwnerInfo
    {
        object GetEditableValue();

        bool IsReadOnly { get; }

        IInplaceEditorColumn EditorColumn { get; }

        bool IsInTree { get; }

        bool IsInactiveEditorButtonVisible { get; }
    }
}

