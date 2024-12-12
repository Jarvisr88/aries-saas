namespace DevExpress.Xpf.Editors.Settings
{
    using System;
    using System.Windows;

    public class EmptyDefaultEditorViewInfo : IDefaultEditorViewInfo
    {
        public static readonly EmptyDefaultEditorViewInfo Instance = new EmptyDefaultEditorViewInfo();

        private EmptyDefaultEditorViewInfo()
        {
        }

        HorizontalAlignment IDefaultEditorViewInfo.DefaultHorizontalAlignment =>
            HorizontalAlignment.Left;

        bool IDefaultEditorViewInfo.HasTextDecorations =>
            false;
    }
}

