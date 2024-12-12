namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CustomRowAppearanceEventArgs : CustomAppearanceEventArgs
    {
        public CustomRowAppearanceEventArgs(CustomAppearanceEventArgs args) : base(args)
        {
        }

        public CustomRowAppearanceEventArgs(DependencyProperty property, object originalValue, object conditionalValue) : base(property, originalValue, conditionalValue)
        {
        }

        internal void SetActualResult(CustomAppearanceEventArgs target)
        {
            target.Result = base.Result;
            target.Handled = base.Handled;
        }

        public int RowHandle { get; internal set; }

        public SelectionState RowSelectionState { get; internal set; }

        public DataViewBase Source { get; internal set; }
    }
}

