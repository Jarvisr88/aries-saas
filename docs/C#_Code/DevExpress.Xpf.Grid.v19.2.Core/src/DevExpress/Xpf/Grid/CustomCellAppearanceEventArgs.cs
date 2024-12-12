namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CustomCellAppearanceEventArgs : CustomRowAppearanceEventArgs
    {
        public CustomCellAppearanceEventArgs(CustomAppearanceEventArgs args) : base(args)
        {
        }

        public CustomCellAppearanceEventArgs(DependencyProperty property, object originalValue, object conditionalValue) : base(property, originalValue, conditionalValue)
        {
        }

        public ColumnBase Column { get; internal set; }

        public SelectionState CellSelectionState { get; internal set; }

        public bool IsEditorVisible { get; internal set; }
    }
}

