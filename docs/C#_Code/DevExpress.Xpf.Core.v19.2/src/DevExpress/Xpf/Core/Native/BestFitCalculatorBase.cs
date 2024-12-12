namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class BestFitCalculatorBase
    {
        public const int DefaultBestFitMaxRowCount = -1;
        public const int SmartModeRowCountThreshold = 0xbb8;

        protected BestFitCalculatorBase();
        public RowsRange CalcBestFitRowsRange(IBestFitColumn column);
        protected virtual RowsRange CalcBestFitRowsRange(int rowCount);
        public virtual double CalcColumnBestFitWidth(IBestFitColumn column);
        protected virtual double CalcColumnBestFitWidthCore(IBestFitColumn column);
        protected void CalcDataBestFit(IBestFitColumn column, ref double result);
        protected virtual void CalcDistinctValuesBestFit(FrameworkElement bestFitControl, IBestFitColumn column, ref double result);
        protected abstract BestFitCalculatorBase.RowsBestFitCalculatorBase CreateBestFitCalculator(IEnumerable<int> rows);
        protected abstract FrameworkElement CreateBestFitControl(IBestFitColumn column);
        protected virtual int GetBestFitMaxRowCount(IBestFitColumn column);
        protected virtual BestFitMode GetBestFitMode(IBestFitColumn column);
        protected virtual BestFitCalculatorBase.CalcBestFitDelegate GetCalcBestFitDelegate(BestFitMode bestFitMode, IBestFitColumn column);
        protected virtual double GetDesiredSize(FrameworkElement bestFitElement);
        protected abstract int GetRowCount(IBestFitColumn column);
        protected BestFitMode GetSmartBestFitMode(IBestFitColumn column);
        protected virtual BestFitCalculatorBase.CalcBestFitDelegate GetSmartModeCalcBestFitDelegate(IBestFitColumn column);
        protected abstract object[] GetUniqueValues(IBestFitColumn column);
        protected abstract void SetBestFitElement(FrameworkElement bestFitElement);
        protected abstract void UpdateBestFitControl(FrameworkElement bestFitControl, IBestFitColumn column, object cellValue);
        protected void UpdateBestFitResult(FrameworkElement bestFitElement, ref double result);
        protected void UpdateBestFitResult(FrameworkElement bestFitElement, ref double result, double correction);

        protected virtual int VisibleRowCount { get; }

        protected virtual bool IsServerMode { get; }

        protected delegate void CalcBestFitDelegate(FrameworkElement bestFitControl, IBestFitColumn column, ref double result);

        protected abstract class RowsBestFitCalculatorBase
        {
            private readonly BestFitCalculatorBase owner;
            private readonly IEnumerable<int> rows;

            public RowsBestFitCalculatorBase(BestFitCalculatorBase owner, IEnumerable<int> rows);
            public virtual void CalcRowsBestFit(FrameworkElement bestFitControl, IBestFitColumn column, ref double result);
            protected virtual bool IsFocusedCell(IBestFitColumn column, int rowHandle);
            protected virtual bool IsValidRowHandle(int rowHandle);
            protected abstract void UpdateBestFitControl(FrameworkElement bestFitControl, IBestFitColumn column, int rowHandle);

            protected BestFitCalculatorBase Owner { get; }

            protected IEnumerable<int> Rows { get; }
        }
    }
}

