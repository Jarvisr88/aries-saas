namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public abstract class RatingEditStrategyBase : EditStrategyBase
    {
        private EndInitPostponedAction MinMaxPropertyChangedAction;

        public RatingEditStrategyBase(BaseEdit editor) : base(editor)
        {
            this.MinMaxPropertyChangedAction = new EndInitPostponedAction(() => base.IsInSupportInitialize);
            this.MinMaxUpdater = this.CreateMinMaxHelper();
            this.MinMaxChangeLocker = new Locker();
        }

        protected abstract MinMaxUpdateHelper CreateMinMaxHelper();
        protected virtual IComparableWrapper CreateMinMaxWrapper(double value) => 
            new IComparableWrapper(value, false, false);

        protected virtual double GetActualMaximumValue(double value) => 
            value;

        protected virtual double GetActualMinimumValue(double value) => 
            value;

        public virtual void MaximumChanged(double value)
        {
            if (!this.MinMaxChangeLocker)
            {
                this.MinMaxUpdater.MaxValue = this.CreateMinMaxWrapper(value);
                using (this.MinMaxChangeLocker.Lock())
                {
                    this.MinMaxPropertyChangedAction.PerformIfNotLoading(() => this.MinMaxUpdater.Update<double>(MinMaxUpdateSource.MaxChanged), null);
                }
                if (!base.ShouldLockUpdate)
                {
                    value = this.GetActualMaximumValue(value);
                    if (ObjectToDoubleConverter.TryConvert(base.ValueContainer.EditValue) > value)
                    {
                        base.ValueContainer.SetEditValue(value, UpdateEditorSource.ValueChanging);
                    }
                }
            }
        }

        public virtual void MinimumChanged(double value)
        {
            if (!this.MinMaxChangeLocker)
            {
                this.MinMaxUpdater.MinValue = this.CreateMinMaxWrapper(value);
                using (this.MinMaxChangeLocker.Lock())
                {
                    this.MinMaxPropertyChangedAction.PerformIfNotLoading(() => this.MinMaxUpdater.Update<double>(MinMaxUpdateSource.MinChanged), null);
                }
                if (!base.ShouldLockUpdate)
                {
                    value = this.GetActualMinimumValue(value);
                    if (ObjectToDoubleConverter.TryConvert(base.ValueContainer.EditValue) < value)
                    {
                        base.ValueContainer.SetEditValue(value, UpdateEditorSource.ValueChanging);
                    }
                }
            }
        }

        public override void OnInitialized()
        {
            base.OnInitialized();
            using (this.MinMaxChangeLocker.Lock())
            {
                this.MinMaxPropertyChangedAction.PerformIfNotLoading(() => this.MinMaxUpdater.Update<double>(MinMaxUpdateSource.ISupportInitialize), null);
            }
        }

        public virtual void OrientationChanged(Orientation orientation)
        {
        }

        protected internal abstract void ValuePropertyChanged(double oldValue, double value);

        protected MinMaxUpdateHelper MinMaxUpdater { get; set; }

        private Locker MinMaxChangeLocker { get; set; }
    }
}

