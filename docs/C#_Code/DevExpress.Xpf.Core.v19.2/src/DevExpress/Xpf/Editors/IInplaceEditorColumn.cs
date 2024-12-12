namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public interface IInplaceEditorColumn : IDefaultEditorViewInfo
    {
        event ColumnContentChangedEventHandler ContentChanged;

        BaseEditSettings EditSettings { get; }

        DataTemplateSelector EditorTemplateSelector { get; }

        ControlTemplate EditTemplate { get; }

        ControlTemplate DisplayTemplate { get; }
    }
}

