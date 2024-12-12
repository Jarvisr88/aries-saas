namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Windows;

    public interface IPopupBrushEdit : IBrushEdit, IBaseEdit, IInputElement
    {
        PopupBrushValue GetPopupBrushValue(DevExpress.Xpf.Editors.BrushType brushType);

        bool AllowEditBrushType { get; }

        DevExpress.Xpf.Editors.BrushType BrushType { get; }
    }
}

