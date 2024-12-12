namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public abstract class CalculatorViewBase : DependencyObject
    {
        private string displayText;
        private decimal displayValue;
        private decimal memory;
        private int precision;
        private decimal result;
        private CalcStatus status;
        private decimal value;
        private Locker initLocker = new Locker();
        private Locker ownerValueSynchronizationLocker = new Locker();
        private Locker valueSynchronizationLocker = new Locker();

        public CalculatorViewBase(ICalculatorViewOwner owner)
        {
            this.Owner = owner;
            this.Strategy = this.CreateStrategy();
        }

        protected internal void AddToHistory(string text)
        {
            if (this.Owner != null)
            {
                this.Owner.AddToHistory(text);
            }
        }

        protected internal virtual bool CanCopy() => 
            true;

        protected internal virtual bool CanPaste() => 
            this.Status != CalcStatus.Error;

        protected internal virtual void Copy()
        {
            DXClipboard.SetText(this.DisplayText);
        }

        protected abstract CalculatorStrategyBase CreateStrategy();
        protected abstract object GetButtonIDByKey(KeyEventArgs e);
        protected abstract object GetButtonIDByTextInput(TextCompositionEventArgs e);
        protected internal void GetCustomErrorText(ref string errorText)
        {
            if (this.Owner != null)
            {
                this.Owner.GetCustomErrorText(ref errorText);
            }
        }

        public virtual void Init(decimal value, bool resetMemory)
        {
            this.Strategy.Init(value, resetMemory);
        }

        public virtual void OnKeyDown(KeyEventArgs e)
        {
            object buttonIDByKey = this.GetButtonIDByKey(e);
            e.Handled = buttonIDByKey != null;
            if (e.Handled)
            {
                this.ProcessButtonClickByKeyboard(buttonIDByKey);
            }
        }

        public virtual void OnTextInput(TextCompositionEventArgs e)
        {
            object buttonIDByTextInput = this.GetButtonIDByTextInput(e);
            e.Handled = buttonIDByTextInput != null;
            if (e.Handled)
            {
                this.ProcessButtonClickByKeyboard(buttonIDByTextInput);
            }
        }

        protected internal virtual void Paste()
        {
            if (DXClipboard.ContainsText())
            {
                this.Strategy.SetDisplayText(DXClipboard.GetText());
            }
        }

        public virtual void ProcessButtonClick(object buttonID)
        {
            this.IsModified = true;
            this.Strategy.ProcessButtonClick(buttonID);
        }

        protected virtual void ProcessButtonClickByKeyboard(object buttonID)
        {
            if (this.Owner != null)
            {
                this.Owner.AnimateButtonClick(buttonID);
            }
            this.ProcessButtonClick(buttonID);
        }

        public void ResetDisplayValue()
        {
            this.ownerValueSynchronizationLocker.DoLockedAction<decimal>(delegate {
                decimal num = new decimal(-1, -1, -1, true, 0);
                this.DisplayValue = num;
                return num;
            });
        }

        private void SynchronizeOwnerValue(decimal value)
        {
            if (this.Owner != null)
            {
                this.ownerValueSynchronizationLocker.Lock();
                try
                {
                    this.Owner.SetValue(value);
                }
                finally
                {
                    this.ownerValueSynchronizationLocker.Unlock();
                }
            }
        }

        protected internal void SynchronizeValue()
        {
            if (!this.initLocker.IsLocked)
            {
                this.valueSynchronizationLocker.Lock();
                try
                {
                    this.Value = decimal.Round(this.DisplayValue, this.Precision);
                }
                finally
                {
                    this.valueSynchronizationLocker.Unlock();
                }
            }
        }

        public virtual void UpdateFormatting()
        {
            this.Strategy.UpdateFormatting();
        }

        public CultureInfo Culture =>
            CultureInfo.CurrentCulture;

        public string DisplayText
        {
            get => 
                this.displayText;
            set
            {
                if (value != this.displayText)
                {
                    this.displayText = value;
                    if (this.Owner != null)
                    {
                        this.Owner.SetDisplayText(value);
                    }
                }
            }
        }

        public decimal DisplayValue
        {
            get => 
                this.displayValue;
            set
            {
                if (value != this.displayValue)
                {
                    this.displayValue = value;
                    this.SynchronizeValue();
                }
            }
        }

        public string HistoryString { get; set; }

        public bool IsModified { get; set; }

        public decimal Memory
        {
            get => 
                this.memory;
            set
            {
                if (value != this.memory)
                {
                    this.memory = value;
                    if (this.Owner != null)
                    {
                        this.Owner.SetMemory(value);
                    }
                }
            }
        }

        public int Precision
        {
            get => 
                this.precision;
            set
            {
                if (value != this.precision)
                {
                    this.precision = value;
                    this.UpdateFormatting();
                }
            }
        }

        public decimal Result
        {
            get => 
                this.result;
            set
            {
                if (value != this.result)
                {
                    this.result = value;
                }
            }
        }

        public CalcStatus Status
        {
            get => 
                this.status;
            set
            {
                if (value != this.status)
                {
                    this.status = value;
                    if (this.Owner != null)
                    {
                        this.Owner.SetHasError(value == CalcStatus.Error);
                    }
                }
            }
        }

        public decimal Value
        {
            get => 
                this.value;
            set
            {
                if (!this.ownerValueSynchronizationLocker.IsLocked && (value != this.value))
                {
                    this.value = value;
                    this.SynchronizeOwnerValue(value);
                    if (!this.valueSynchronizationLocker.IsLocked)
                    {
                        this.initLocker.Lock();
                        try
                        {
                            this.Init(value, false);
                        }
                        finally
                        {
                            this.initLocker.Unlock();
                        }
                    }
                }
            }
        }

        protected ICalculatorViewOwner Owner { get; private set; }

        protected CalculatorStrategyBase Strategy { get; private set; }
    }
}

