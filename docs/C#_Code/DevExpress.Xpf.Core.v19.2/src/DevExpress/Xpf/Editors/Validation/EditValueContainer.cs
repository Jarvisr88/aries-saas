namespace DevExpress.Xpf.Editors.Validation
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Validation.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class EditValueContainer
    {
        private Locker editValueChanging = new Locker();
        private Locker postponedValueChanging = new Locker();
        private NullableContainer editValueCandidate = new NullableContainer();
        private NullableContainer tempEditValue = new NullableContainer();
        private DispatcherTimer postTimer;

        public EditValueContainer(BaseEdit editor)
        {
            this.Editor = editor;
        }

        private DispatcherTimer CreatePostTimer()
        {
            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Send);
            timer.Tick += new EventHandler(this.timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds((double) this.Editor.EditValuePostDelay);
            return timer;
        }

        public void FlushEditValue()
        {
            this.FlushEditValueImmediate();
            this.ResetPostTimer();
        }

        public void FlushEditValueCandidate(object editValue, UpdateEditorSource updateSource)
        {
            if (!this.HasValueCandidate)
            {
                this.FlushEditValue();
            }
            else
            {
                this.SetEditValueInternal(editValue, updateSource);
                this.FlushEditValue();
            }
        }

        private void FlushEditValueImmediate()
        {
            if (this.HasTempValue)
            {
                BaseEditHelper.SetCurrentValue(this.Editor, BaseEdit.EditValueProperty, this.TempEditValue);
            }
            this.ResetTempValue();
        }

        private void FlushEditValuePostpone()
        {
            this.postponedValueChanging.DoLockedAction(delegate {
                this.FlushEditValueImmediate();
                CommandManager.InvalidateRequerySuggested();
            });
        }

        public bool GetIsValid(UpdateEditorSource updateSource) => 
            (updateSource != UpdateEditorSource.ValueChanging) && ((updateSource != UpdateEditorSource.DoValidate) && (this.HasValueCandidate && Equals(this.EditValueCandidate, this.Editor.EditValue)));

        private void PostEditValue(object value, UpdateEditorSource updateSource)
        {
            this.editValueChanging.DoLockedActionIfNotLocked(() => this.PostEditValueInternal(value, updateSource));
        }

        private void PostEditValueInternal(object editValue, UpdateEditorSource updateSource)
        {
            this.UpdateSource = updateSource;
            if ((this.PostMode == DevExpress.Xpf.Editors.PostMode.Immediate) || (updateSource != UpdateEditorSource.TextInput))
            {
                object obj2;
                this.ProvideEditValue(editValue, out obj2, updateSource);
                BaseEditHelper.SetCurrentValue(this.Editor, BaseEdit.EditValueProperty, obj2);
            }
            else
            {
                this.StartPostTimer();
                this.TempEditValue = editValue;
                this.Editor.EditStrategy.ForceSyncWithValueInternal();
            }
        }

        public bool ProvideEditValue(object value, out object provideValue, UpdateEditorSource updateSource)
        {
            provideValue = this.Editor.PropertyProvider.ValueTypeConverter.ConvertBack(value);
            return true;
        }

        public void Reset()
        {
            this.editValueCandidate.Reset();
        }

        private void ResetPostTimer()
        {
            if (this.postTimer != null)
            {
                this.StopPostTimer();
                this.postTimer.Tick -= new EventHandler(this.timer_Tick);
                this.postTimer = null;
            }
        }

        private void ResetTempValue()
        {
            this.tempEditValue.Reset();
        }

        public void ResetUpdateSource()
        {
            this.UpdateSource = UpdateEditorSource.DontValidate;
        }

        public bool SetEditValue(object value, UpdateEditorSource updateSource)
        {
            object obj2;
            this.editValueCandidate.SetValue(value);
            if (!this.Strategy.DoValidateInternal(value, updateSource))
            {
                return false;
            }
            bool flag = this.Strategy.ProvideEditValue(value, out obj2, updateSource);
            if (flag)
            {
                this.SetEditValueInternal(obj2, updateSource);
            }
            return flag;
        }

        private void SetEditValueInternal(object value, UpdateEditorSource updateSource)
        {
            this.Reset();
            if (!Equals(this.Strategy.ConvertToBaseValue(this.EditValue), this.Strategy.ConvertToBaseValue(value)))
            {
                this.Strategy.ResetValidationError();
                this.PostEditValue(value, updateSource);
            }
        }

        private void StartPostTimer()
        {
            if (this.PostTimer.IsEnabled)
            {
                this.StopPostTimer();
            }
            this.postponedValueChanging.Lock();
            this.PostTimer.Start();
        }

        private void StopPostTimer()
        {
            if (this.PostTimer.IsEnabled)
            {
                this.PostTimer.Stop();
                this.postponedValueChanging.Unlock();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.FlushEditValuePostpone();
            this.StopPostTimer();
        }

        public void UndoTempValue()
        {
            this.ResetPostTimer();
            this.ResetTempValue();
        }

        public void Update(UpdateEditorSource updateSource)
        {
            this.SetEditValue(this.EditValue, updateSource);
        }

        public void UpdatePostMode()
        {
            this.ResetPostTimer();
        }

        public DevExpress.Xpf.Editors.PostMode PostMode =>
            this.Editor.EditValuePostMode;

        public bool IsLockedByValueChanging =>
            this.editValueChanging.IsLocked;

        public bool IsPostponedValueChanging =>
            this.postponedValueChanging.IsLocked;

        private BaseEdit Editor { get; set; }

        protected EditStrategyBase Strategy =>
            this.Editor.EditStrategy;

        private DispatcherTimer PostTimer
        {
            get
            {
                DispatcherTimer postTimer = this.postTimer;
                if (this.postTimer == null)
                {
                    DispatcherTimer local1 = this.postTimer;
                    postTimer = this.postTimer = this.CreatePostTimer();
                }
                return postTimer;
            }
        }

        public object EditValue =>
            !this.HasValueCandidate ? (this.HasTempValue ? this.TempEditValue : this.Editor.PropertyProvider.EditValue) : this.EditValueCandidate;

        public object TempEditValue
        {
            get => 
                this.tempEditValue.HasValue ? this.tempEditValue.Value : this.EditValue;
            set => 
                this.tempEditValue.SetValue(value);
        }

        public object EditValueCandidate =>
            this.editValueCandidate.Value;

        public bool HasValueCandidate =>
            this.editValueCandidate.HasValue;

        public bool HasTempValue =>
            this.tempEditValue.HasValue;

        public UpdateEditorSource UpdateSource { get; private set; }
    }
}

