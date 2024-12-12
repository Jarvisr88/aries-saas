namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;

    public abstract class BaseBestFitControl : BestFitControlBase
    {
        private FrameworkElement cellContentPresenter;
        private static readonly DependencyPropertyKey IsFocusedCellPropertyKey;
        public static readonly DependencyProperty IsFocusedCellProperty;

        static BaseBestFitControl()
        {
            Type ownerType = typeof(BaseBestFitControl);
            IsFocusedCellPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsFocusedCell", typeof(bool), ownerType, new PropertyMetadata(false));
            IsFocusedCellProperty = IsFocusedCellPropertyKey.DependencyProperty;
        }

        protected BaseBestFitControl(DataViewBase view, ColumnBase column) : base(view, column)
        {
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.cellContentPresenter = base.GetTemplateChild("PART_CellContentPresenter") as FrameworkElement;
            this.cellContentPresenter.DataContext = base.CellData;
            this.cellContentPresenter.Style = base.Column.ActualCellStyle;
        }

        public override void UpdateIsFocusedCell(bool isFocusedCell)
        {
            this.IsFocusedCell = isFocusedCell;
        }

        public bool IsFocusedCell
        {
            get => 
                (bool) base.GetValue(IsFocusedCellProperty);
            private set => 
                base.SetValue(IsFocusedCellPropertyKey, value);
        }
    }
}

