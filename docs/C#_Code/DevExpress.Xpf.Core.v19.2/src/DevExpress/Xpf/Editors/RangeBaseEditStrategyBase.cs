namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class RangeBaseEditStrategyBase : RatingEditStrategyBase
    {
        public RangeBaseEditStrategyBase(BaseEdit editor) : base(editor)
        {
            this.Info = this.CreateEditInfo();
        }

        protected virtual RangeEditBaseInfo CreateEditInfo() => 
            new RangeEditBaseInfo();

        protected override MinMaxUpdateHelper CreateMinMaxHelper()
        {
            MinMaxUpdateHelper helper1 = new MinMaxUpdateHelper(this.Editor, RangeBaseEdit.MinimumProperty, RangeBaseEdit.MaximumProperty);
            helper1.MinValue = new IComparableWrapper(this.Editor.Minimum, false, false);
            helper1.MaxValue = new IComparableWrapper(this.Editor.Maximum, false, false);
            return helper1;
        }

        public virtual void DecrementLarge(object parameter)
        {
            this.IncrementDecrement(this.Editor.LargeStep, false, true);
        }

        public virtual void DecrementSmall(object parameter)
        {
            this.IncrementDecrement(this.Editor.SmallStep, false, false);
        }

        protected virtual double GetNextValue(double result, double step, bool increment, bool isLarge) => 
            (increment ? (<>c.<>9__28_0 ??= (r, v) => (r + v)) : (<>c.<>9__28_1 ??= (r, v) => (r - v)))(result, step);

        protected internal double GetNormalValue()
        {
            double normalValue = this.GetNormalValue(this.ToRange(ObjectToDoubleConverter.TryConvert(base.ValueContainer.EditValue)));
            return this.ToNormalRange(normalValue);
        }

        protected double GetNormalValue(double value)
        {
            double num = this.Editor.Maximum - this.Editor.Minimum;
            num = (num.CompareTo((double) 0.0) == 0) ? 1.0 : num;
            return this.ToNormalRange((ObjectToDoubleConverter.TryConvert(value) - this.Editor.Minimum) / num);
        }

        protected double GetRange()
        {
            double num = this.Editor.Maximum - this.Editor.Minimum;
            return ((num.CompareTo((double) 0.0) != 0) ? num : 1.0);
        }

        protected internal double GetRealValue(double normalValue)
        {
            double num = this.Editor.Maximum - this.Editor.Minimum;
            num = (num.CompareTo((double) 0.0) == 0) ? 1.0 : num;
            return (this.Editor.Minimum + (num * normalValue));
        }

        protected internal virtual void IncrementDecrement(double step, bool increment, bool isLarge)
        {
            if (base.AllowEditing)
            {
                double result = ObjectToDoubleConverter.TryConvert(base.ValueContainer.EditValue);
                double num2 = this.IncrementDecrementInternal(step, increment, isLarge, result);
                base.ValueContainer.SetEditValue(this.ToRange(num2), UpdateEditorSource.TextInput);
            }
        }

        protected virtual double IncrementDecrementInternal(double step, bool increment, bool isLarge, double result) => 
            this.GetNextValue(result, step, increment, isLarge);

        public virtual void IncrementLarge(object parameter)
        {
            this.IncrementDecrement(this.Editor.LargeStep, true, true);
        }

        public virtual void IncrementSmall(object parameter)
        {
            this.IncrementDecrement(this.Editor.SmallStep, true, false);
        }

        protected bool InRange(double value) => 
            (value >= this.Editor.Minimum) && (value <= this.Editor.Maximum);

        public virtual void Maximize(object parameter)
        {
            base.ValueContainer.SetEditValue(this.Editor.Maximum, UpdateEditorSource.TextInput);
        }

        public override void MaximumChanged(double value)
        {
            base.MaximumChanged(value);
            this.UpdatePositions();
        }

        public virtual void Minimize(object parameter)
        {
            base.ValueContainer.SetEditValue(this.Editor.Minimum, UpdateEditorSource.TextInput);
        }

        public override void MinimumChanged(double value)
        {
            base.MinimumChanged(value);
            this.UpdatePositions();
        }

        protected internal virtual void OnApplyTemplate()
        {
        }

        public override void OnLoaded()
        {
            base.OnLoaded();
            this.UpdatePositions();
        }

        public override void OrientationChanged(Orientation orientation)
        {
            base.OrientationChanged(orientation);
            base.ApplyStyleSettings(base.StyleSettings);
        }

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            PropertyCoercionHandler getBaseValueHandler = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__13_0;
                getBaseValueHandler = <>c.<>9__13_0 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(RangeBaseEdit.ValueProperty, getBaseValueHandler, <>c.<>9__13_1 ??= ((PropertyCoercionHandler) (baseValue => ObjectToDoubleConverter.TryConvert(baseValue))));
        }

        protected void SetNormalValue(double value)
        {
            base.ValueContainer.SetEditValue(this.GetRealValue(value), UpdateEditorSource.TextInput);
        }

        protected override void SyncWithValueInternal()
        {
            base.SyncWithValueInternal();
            this.UpdatePositions();
        }

        private double ToNormalRange(double value) => 
            Math.Max(Math.Min(1.0, value), 0.0);

        protected virtual double ToRange(double value) => 
            (value >= this.Editor.Minimum) ? ((value <= this.Editor.Maximum) ? value : this.Editor.Maximum) : this.Editor.Minimum;

        public override void UpdateDataContext(DependencyObject target)
        {
            base.UpdateDataContext(target);
            RangeEditBaseInfo.SetLayoutInfo(target, this.Info);
        }

        protected abstract void UpdateDisplayValue();
        public virtual void UpdatePositions()
        {
            if (this.Panel != null)
            {
                double normalValue = this.GetNormalValue();
                this.Info.LeftSidePosition = normalValue;
                this.Info.RightSidePosition = 1.0 - normalValue;
                this.UpdateDisplayText();
            }
        }

        protected internal override void ValuePropertyChanged(double oldValue, double value)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(RangeBaseEdit.ValueProperty, oldValue, value);
            }
        }

        protected override bool IsNullTextSupported =>
            true;

        protected override bool ApplyDisplayTextConversion =>
            true;

        protected RangeBaseEdit Editor =>
            (RangeBaseEdit) base.Editor;

        protected RangeEditBasePanel Panel =>
            this.Editor.Panel;

        protected RangeEditBaseInfo Info { virtual get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RangeBaseEditStrategyBase.<>c <>9 = new RangeBaseEditStrategyBase.<>c();
            public static PropertyCoercionHandler <>9__13_0;
            public static PropertyCoercionHandler <>9__13_1;
            public static Func<double, double, double> <>9__28_0;
            public static Func<double, double, double> <>9__28_1;

            internal double <GetNextValue>b__28_0(double r, double v) => 
                r + v;

            internal double <GetNextValue>b__28_1(double r, double v) => 
                r - v;

            internal object <RegisterUpdateCallbacks>b__13_0(object baseValue) => 
                baseValue;

            internal object <RegisterUpdateCallbacks>b__13_1(object baseValue) => 
                ObjectToDoubleConverter.TryConvert(baseValue);
        }
    }
}

