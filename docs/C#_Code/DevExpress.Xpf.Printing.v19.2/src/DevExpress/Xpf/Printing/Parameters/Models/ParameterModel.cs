namespace DevExpress.Xpf.Printing.Parameters.Models
{
    using DevExpress.Data;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [POCOViewModel]
    public class ParameterModel : IDataErrorInfo, INotifyPropertyChanged
    {
        private string error;
        private readonly object initialValue;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler ValueChanged;

        protected ParameterModel(DevExpress.XtraReports.Parameters.Parameter parameter, LookUpValueCollection lookUpValues)
        {
            this.Parameter = parameter;
            this.Value = this.initialValue = this.IsRangeParameter ? this.GetRangeValue() : parameter.Value;
            this.LookUpValues = lookUpValues;
            this.IsChanged = false;
        }

        public static ParameterModel CreateParameterModel(DevExpress.XtraReports.Parameters.Parameter parameter, LookUpValueCollection lookUpValues)
        {
            <>c__DisplayClass45_0 class_;
            Expression[] expressionArray1 = new Expression[] { Expression.Field(Expression.Constant(class_, typeof(<>c__DisplayClass45_0)), fieldof(<>c__DisplayClass45_0.parameter)), Expression.Field(Expression.Constant(class_, typeof(<>c__DisplayClass45_0)), fieldof(<>c__DisplayClass45_0.lookUpValues)) };
            return ViewModelSource.Create<ParameterModel>(Expression.Lambda<Func<ParameterModel>>(Expression.New((ConstructorInfo) methodof(ParameterModel..ctor), (IEnumerable<Expression>) expressionArray1), new ParameterExpression[0]));
        }

        internal void Destroy()
        {
            this.Parameter = null;
        }

        private bool GetActualIsChanged() => 
            (PrintingSettings.ParameterPanelResetMode == ParameterPanelResetMode.RestoreOriginalValue) ? !Equals(this.Value, this.initialValue) : (this.Value != this.Parameter.Value);

        private Range<System.DateTime>? GetRangeValue()
        {
            System.DateTime? nullable = ((IRangeRootParameter) this.Parameter).StartParameter.Value as System.DateTime?;
            System.DateTime? nullable2 = ((IRangeRootParameter) this.Parameter).EndParameter.Value as System.DateTime?;
            if ((nullable != null) && (nullable2 != null))
            {
                return new Range<System.DateTime>(nullable.Value, nullable2.Value);
            }
            return null;
        }

        protected void OnValueChanged()
        {
            this.IsChanged = this.GetActualIsChanged();
            if (this.ValueChanged == null)
            {
                EventHandler valueChanged = this.ValueChanged;
            }
            else
            {
                this.ValueChanged(this, EventArgs.Empty);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void RaisePropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private void ResetValue()
        {
            if (PrintingSettings.ParameterPanelResetMode == ParameterPanelResetMode.RestoreOriginalValue)
            {
                this.Value = this.initialValue;
            }
            else
            {
                this.Value = this.IsRangeParameter ? this.GetRangeValue() : this.Parameter.Value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetError(string error)
        {
            this.error = error;
            this.RaisePropertyChanged("");
        }

        private void SetRangeValue()
        {
            Range<System.DateTime>? nullable = this.Value as Range<System.DateTime>?;
            if (nullable == null)
            {
                throw new InvalidOperationException();
            }
            ((IRangeRootParameter) this.Parameter).StartParameter.Value = nullable.Value.Start;
            ((IRangeRootParameter) this.Parameter).EndParameter.Value = nullable.Value.End;
        }

        private void SubmitValue()
        {
            if (this.IsRangeParameter)
            {
                this.SetRangeValue();
            }
            else
            {
                this.Parameter.Value = this.Value;
            }
        }

        internal void UpdateLookUpValues(LookUpValueCollection values)
        {
            if (values != null)
            {
                this.Value = !this.MultiValue ? (values.Any<LookUpValue>(x => x.Value.Equals(this.Value)) ? this.Value : ((values.Count > 0) ? values[0].Value : null)) : null;
                this.LookUpValues = values;
                this.RaisePropertyChanged("");
            }
        }

        internal void UpdateParameter(UpdateAction action)
        {
            if (action == UpdateAction.Submit)
            {
                this.SubmitValue();
            }
            else
            {
                this.ResetValue();
            }
            this.IsChanged = this.GetActualIsChanged();
        }

        internal DevExpress.XtraReports.Parameters.Parameter Parameter { get; private set; }

        public string Name =>
            this.Parameter.Name;

        public string Description =>
            this.Parameter.Description;

        public object Tag =>
            this.Parameter.Tag;

        public bool MultiValue =>
            this.Parameter.MultiValue;

        public bool AllowNull =>
            this.Parameter.AllowNull;

        public bool Visible =>
            this.Parameter.Visible;

        public string Path { get; set; }

        public bool IsRangeParameter =>
            ((IRangeRootParameter) this.Parameter).IsRange;

        public System.Type Type
        {
            get
            {
                if (!this.Parameter.AllowNull || !this.Parameter.Type.IsValueType)
                {
                    return this.Parameter.Type;
                }
                System.Type[] typeArguments = new System.Type[] { this.Parameter.Type };
                return typeof(Nullable<>).MakeGenericType(typeArguments);
            }
        }

        public virtual LookUpValueCollection LookUpValues { get; set; }

        public virtual object Value { get; set; }

        internal bool IsChanged { get; set; }

        public bool IsFilteredLookUpSettings { get; internal set; }

        string IDataErrorInfo.Error =>
            string.Empty;

        string IDataErrorInfo.this[string columnName] =>
            this.error;
    }
}

