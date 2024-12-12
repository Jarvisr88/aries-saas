namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class RangeEditorStrategy<T> : ButtonEditStrategy where T: struct, IComparable
    {
        private readonly PostponedAction minMaxPropertyChangedAction;

        public RangeEditorStrategy(ButtonEdit editor) : base(editor)
        {
            this.MinMaxChangedLocker = new Locker();
            this.minMaxPropertyChangedAction = new PostponedAction(() => base.IsInSupportInitialize);
            this.MinMaxUpdater = this.CreateMinMaxHelper();
        }

        public virtual bool CanMaximize() => 
            true;

        public virtual bool CanMinimize() => 
            true;

        public virtual object Correct(object baseValue) => 
            this.ShouldRoundToBounds ? this.ToRange(baseValue) : this.CreateValueConverter(baseValue).Value;

        protected abstract MinMaxUpdateHelper CreateMinMaxHelper();
        public RangeValueConverter<T> CreateValueConverter(object value) => 
            new RangeValueConverter<T>(this.IsNullValue(value) ? null : value, () => this.IsNativeNullValue(this.GetNullableValue()) ? this.GetDefaultValue() : this.GetNullableValue(), new Func<object>(this.GetDefaultValue));

        protected override object GetEditValueInternal()
        {
            object editValueInternal = base.GetEditValueInternal();
            if (!this.Editor.AllowNullInput && this.IsNullValue(editValueInternal))
            {
                editValueInternal = this.Correct(editValueInternal);
            }
            return editValueInternal;
        }

        protected internal abstract T GetMaxValue();
        protected internal abstract T GetMinValue();
        public bool InRange(object value)
        {
            if (this.Editor.AllowNullInput && this.IsNullValue(value))
            {
                return true;
            }
            RangeValueConverter<T> converter = this.CreateValueConverter(value);
            return ((converter.CompareTo(this.GetMinValue()) >= 0) && (converter.CompareTo(this.GetMaxValue()) <= 0));
        }

        public virtual void Maximize()
        {
            base.ValueContainer.SetEditValue(this.GetMaxValue(), UpdateEditorSource.TextInput);
        }

        protected internal virtual void MaxValueChanged(T? value)
        {
            if (!this.MinMaxChangedLocker)
            {
                this.MinMaxUpdater.MaxValue = new IComparableWrapper(this.GetMaxValue(), value == null, false);
                using (this.MinMaxChangedLocker.Lock())
                {
                    this.minMaxPropertyChangedAction.PerformPostpone(() => base.MinMaxUpdater.Update<T>(MinMaxUpdateSource.MaxChanged));
                }
            }
        }

        public virtual void Minimize()
        {
            base.ValueContainer.SetEditValue(this.GetMinValue(), UpdateEditorSource.TextInput);
        }

        protected internal virtual void MinValueChanged(T? value)
        {
            if (!this.MinMaxChangedLocker)
            {
                this.MinMaxUpdater.MinValue = new IComparableWrapper(this.GetMinValue(), value == null, false);
                using (this.MinMaxChangedLocker.Lock())
                {
                    this.minMaxPropertyChangedAction.PerformPostpone(() => base.MinMaxUpdater.Update<T>(MinMaxUpdateSource.MinChanged));
                }
            }
        }

        public override void OnInitialized()
        {
            using (this.MinMaxChangedLocker.Lock())
            {
                this.minMaxPropertyChangedAction.PerformForce(() => base.MinMaxUpdater.Update<T>(MinMaxUpdateSource.ISupportInitialize));
            }
            base.OnInitialized();
        }

        protected internal override void OnNullValueChanged(object nullValue)
        {
            base.OnNullValueChanged(nullValue);
            this.Editor.DoValidate();
        }

        protected internal override void PrepareForCheckAllowLostKeyboardFocus()
        {
            base.PrepareForCheckAllowLostKeyboardFocus();
            if (!base.IsInSupportInitialize && (!this.InRange(base.ValueContainer.EditValue) && (base.PropertyProvider.EditMode == EditMode.Standalone)))
            {
                base.TextInputService.SetInitialEditValue(base.ValueContainer.EditValue);
                base.ValueContainer.SetEditValue(base.ValueContainer.EditValue, UpdateEditorSource.ValueChanging);
            }
        }

        public override bool ProvideEditValue(object value, out object provideValue, UpdateEditorSource updateSource)
        {
            bool flag = this.ShouldRoundToBounds && !this.InRange(value);
            if (flag)
            {
                value = this.Correct(value);
            }
            base.ProvideEditValue(value, out provideValue, updateSource);
            if (flag)
            {
                base.TextInputService.SetInitialEditValue(provideValue);
            }
            return true;
        }

        public virtual void RoundToBoundsChanged(bool value)
        {
            if (!base.ShouldLockUpdate)
            {
                T editValue = this.CreateValueConverter(base.ValueContainer.EditValue).Value;
                base.ValueContainer.SetEditValue(editValue, UpdateEditorSource.TextInput);
            }
        }

        protected virtual bool ShouldCorrectEditValue() => 
            !this.InRange(base.ValueContainer.EditValue);

        public virtual object ToRange(object baseValue)
        {
            RangeValueConverter<T> converter = this.CreateValueConverter(baseValue);
            return ((converter.CompareTo(this.GetMinValue()) >= 0) ? ((converter.CompareTo(this.GetMaxValue()) <= 0) ? converter.Value : this.GetMaxValue()) : this.GetMinValue());
        }

        public abstract bool ShouldRoundToBounds { get; }

        private MinMaxUpdateHelper MinMaxUpdater { get; set; }

        private Locker MinMaxChangedLocker { get; set; }

        private ButtonEdit Editor =>
            base.Editor as ButtonEdit;
    }
}

