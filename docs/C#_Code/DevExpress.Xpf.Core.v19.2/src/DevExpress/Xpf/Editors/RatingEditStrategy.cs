namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class RatingEditStrategy : RatingEditStrategyBase
    {
        public RatingEditStrategy(RatingEdit editor) : base(editor)
        {
        }

        private bool CanProcessKeyUpDown(KeyEventArgs e)
        {
            bool flag = ModifierKeysHelper.ContainsModifiers(ModifierKeysHelper.GetKeyboardModifiers(e));
            return ((this.Editor.EditMode == EditMode.Standalone) ? !flag : flag);
        }

        public override string CoerceDisplayText(string displayText)
        {
            string str = base.IsInSupportInitialize ? string.Empty : this.GetDisplayText();
            return base.CoerceDisplayText(str);
        }

        public virtual void ContentAlignmentChanged(HorizontalAlignment horizontal, VerticalAlignment vertical)
        {
            this.Control.Do<RatingControl>(delegate (RatingControl x) {
                x.HorizontalAlignment = horizontal;
                x.VerticalAlignment = vertical;
            });
        }

        protected override MinMaxUpdateHelper CreateMinMaxHelper()
        {
            MinMaxUpdateHelper helper1 = new MinMaxUpdateHelper(this.Editor, RatingEdit.MinimumProperty, RatingEdit.MaximumProperty);
            helper1.MinValue = this.CreateMinMaxWrapper(this.Editor.Minimum);
            helper1.MaxValue = this.CreateMinMaxWrapper(this.Editor.Maximum);
            return helper1;
        }

        protected override IComparableWrapper CreateMinMaxWrapper(double value) => 
            new IComparableWrapper(value, false, double.IsNaN(value));

        internal void Decrease()
        {
            double num;
            double num2;
            double num3;
            this.GetMinMaxStep(out num, out num2, out num3);
            if (this.Editor.Value > num)
            {
                double num4 = Math.Max(this.Editor.Value - ((this.Editor.Precision == RatingPrecision.Full) ? num3 : (num3 / 2.0)), num);
                this.Editor.Value = num4;
            }
        }

        protected override double GetActualMaximumValue(double value) => 
            RatingControl.GetMinMax(this.Editor.Minimum, value, (double) this.Editor.ItemsCount).Item2;

        protected override double GetActualMinimumValue(double value) => 
            RatingControl.GetMinMax(value, this.Editor.Maximum, (double) this.Editor.ItemsCount).Item1;

        private void GetMinMaxStep(out double min, out double max, out double step)
        {
            min = 0.0;
            max = 0.0;
            step = 0.0;
            if (this.Editor.ItemsCount != 0)
            {
                bool flag = double.IsNaN(this.Editor.Minimum);
                bool flag2 = double.IsNaN(this.Editor.Maximum);
                min = flag ? 0.0 : this.Editor.Minimum;
                max = flag2 ? (min + this.Editor.ItemsCount) : this.Editor.Maximum;
                step = (max - min) / ((double) this.Editor.ItemsCount);
            }
        }

        internal void Increase()
        {
            double num;
            double num2;
            double num3;
            this.GetMinMaxStep(out num, out num2, out num3);
            if (this.Editor.Value < num2)
            {
                double num4 = Math.Min(this.Editor.Value + ((this.Editor.Precision == RatingPrecision.Full) ? num3 : (num3 / 2.0)), num2);
                this.Editor.Value = num4;
            }
        }

        public virtual void ItemsCountChanged(int value)
        {
            this.MaximumChanged(this.Editor.Maximum);
            this.Control.Do<RatingControl>(x => x.ItemsCount = value);
        }

        public virtual void ItemStyleChanged(Style value)
        {
            this.Control.Do<RatingControl>(x => x.ItemStyle = value);
        }

        public override void MaximumChanged(double value)
        {
            base.MaximumChanged(value);
            this.Control.Do<RatingControl>(x => x.Maximum = value);
        }

        public override void MinimumChanged(double value)
        {
            base.MinimumChanged(value);
            this.Control.Do<RatingControl>(x => x.Minimum = value);
        }

        public override void OrientationChanged(Orientation value)
        {
            base.OrientationChanged(value);
            this.Control.Do<RatingControl>(x => x.Orientation = value);
        }

        public virtual void PrecisionChanged(RatingPrecision value)
        {
            this.Control.Do<RatingControl>(x => x.Precision = value);
        }

        protected override void ProcessPreviewKeyDownInternal(KeyEventArgs e)
        {
            base.ProcessPreviewKeyDownInternal(e);
            if (((e.Key == Key.Left) || (e.Key == Key.Down)) && this.CanProcessKeyUpDown(e))
            {
                this.Decrease();
                e.Handled = true;
            }
            else if (((e.Key == Key.Right) || (e.Key == Key.Up)) && this.CanProcessKeyUpDown(e))
            {
                this.Increase();
                e.Handled = true;
            }
        }

        protected override void RegisterUpdateCallbacks()
        {
            base.RegisterUpdateCallbacks();
            PropertyCoercionHandler getBaseValueHandler = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                PropertyCoercionHandler local1 = <>c.<>9__5_0;
                getBaseValueHandler = <>c.<>9__5_0 = baseValue => baseValue;
            }
            base.PropertyUpdater.Register(RatingEdit.ValueProperty, getBaseValueHandler, <>c.<>9__5_1 ??= ((PropertyCoercionHandler) (baseValue => DevExpress.Xpf.Editors.ObjectToDoubleConverter.TryConvert(baseValue))));
        }

        protected override void SyncEditCorePropertiesInternal()
        {
            base.SyncEditCorePropertiesInternal();
            this.ContentAlignmentChanged(this.Editor.HorizontalContentAlignment, this.Editor.VerticalContentAlignment);
            this.ItemsCountChanged(this.Editor.ItemsCount);
            this.ItemStyleChanged(this.Editor.ItemStyle);
            this.PrecisionChanged(this.Editor.Precision);
            this.OrientationChanged(this.Editor.Orientation);
            this.MinimumChanged(this.Editor.Minimum);
            this.MaximumChanged(this.Editor.Maximum);
        }

        protected override void SyncWithEditorInternal()
        {
            this.Control.Do<RatingControl>(x => base.ValueContainer.SetEditValue(x.Value, UpdateEditorSource.TextInput));
        }

        protected override void SyncWithValueInternal()
        {
            base.SyncWithValueInternal();
            this.Control.Do<RatingControl>(x => x.Value = DevExpress.Xpf.Editors.ObjectToDoubleConverter.TryConvert(base.ValueContainer.EditValue));
        }

        protected internal override void ValuePropertyChanged(double oldValue, double value)
        {
            if (!base.ShouldLockUpdate)
            {
                base.SyncWithValue(RatingEdit.ValueProperty, oldValue, value);
            }
        }

        protected RatingEdit Editor =>
            (RatingEdit) base.Editor;

        private RatingControl Control =>
            this.Editor.EditCore as RatingControl;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RatingEditStrategy.<>c <>9 = new RatingEditStrategy.<>c();
            public static PropertyCoercionHandler <>9__5_0;
            public static PropertyCoercionHandler <>9__5_1;

            internal object <RegisterUpdateCallbacks>b__5_0(object baseValue) => 
                baseValue;

            internal object <RegisterUpdateCallbacks>b__5_1(object baseValue) => 
                DevExpress.Xpf.Editors.ObjectToDoubleConverter.TryConvert(baseValue);
        }
    }
}

