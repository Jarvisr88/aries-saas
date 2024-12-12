namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public abstract class CalculatorStrategyBase
    {
        public CalculatorStrategyBase(CalculatorViewBase view)
        {
            this.View = view;
        }

        protected void AddToHistory(string text)
        {
            this.View.AddToHistory(text);
        }

        protected virtual void Error(string message)
        {
            this.GetCustomErrorText(ref message);
            this.DisplayText = message;
            this.DisplayValue = 0M;
            this.Status = CalcStatus.Error;
            this.Result = 0M;
        }

        protected void GetCustomErrorText(ref string errorText)
        {
            this.View.GetCustomErrorText(ref errorText);
        }

        public virtual void Init(decimal value, bool resetMemory)
        {
            this.IsModified = false;
            this.PrevButtonID = null;
        }

        public virtual void ProcessButtonClick(object buttonID)
        {
            try
            {
                this.ProcessButtonClickInternal(buttonID);
            }
            catch
            {
                this.Error(EditorLocalizer.GetString(EditorStringId.CalculatorError));
            }
            finally
            {
                this.PrevButtonID = buttonID;
            }
        }

        protected abstract void ProcessButtonClickInternal(object buttonID);
        public virtual void SetDisplayText(string text)
        {
        }

        protected void SynchronizeValue()
        {
            this.View.SynchronizeValue();
        }

        public virtual void UpdateFormatting()
        {
        }

        protected CultureInfo Culture =>
            this.View.Culture;

        protected string DisplayText
        {
            get => 
                this.View.DisplayText;
            set => 
                this.View.DisplayText = value;
        }

        protected decimal DisplayValue
        {
            get => 
                this.View.DisplayValue;
            set => 
                this.View.DisplayValue = value;
        }

        public string HistoryString
        {
            get => 
                this.View.HistoryString;
            set => 
                this.View.HistoryString = value;
        }

        protected bool IsModified
        {
            get => 
                this.View.IsModified;
            set => 
                this.View.IsModified = value;
        }

        protected decimal Memory
        {
            get => 
                this.View.Memory;
            set => 
                this.View.Memory = value;
        }

        protected int Precision =>
            this.View.Precision;

        protected object PrevButtonID { get; private set; }

        protected decimal Result
        {
            get => 
                this.View.Result;
            set => 
                this.View.Result = value;
        }

        protected CalcStatus Status
        {
            get => 
                this.View.Status;
            set => 
                this.View.Status = value;
        }

        protected CalculatorViewBase View { get; private set; }
    }
}

