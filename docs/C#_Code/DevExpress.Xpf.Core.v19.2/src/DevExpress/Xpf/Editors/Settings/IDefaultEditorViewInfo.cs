namespace DevExpress.Xpf.Editors.Settings
{
    using System;
    using System.Windows;

    public interface IDefaultEditorViewInfo
    {
        HorizontalAlignment DefaultHorizontalAlignment { get; }

        bool HasTextDecorations { get; }
    }
}

