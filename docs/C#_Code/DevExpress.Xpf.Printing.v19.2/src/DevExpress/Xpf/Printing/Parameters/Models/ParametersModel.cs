namespace DevExpress.Xpf.Printing.Parameters.Models
{
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Printing.Parameters;
    using DevExpress.XtraReports.Parameters.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [POCOViewModel]
    public class ParametersModel : INotifyPropertyChanged
    {
        private static readonly IEnumerable<ParameterModel> emptyParameters = Enumerable.Empty<ParameterModel>();
        private ILookUpValuesProvider lookUpValuesProvider;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler Submit;

        private event EventHandler<ValidateParameterEventArgs> validate;

        public event EventHandler<ValidateParameterEventArgs> Validate
        {
            add
            {
                this.validate += value;
                this.ValidateParameters();
            }
            remove
            {
                this.validate -= value;
            }
        }

        protected ParametersModel()
        {
            this.Parameters = new ReadOnlyCollection<ParameterModel>(new List<ParameterModel>());
            this.CanEdit = true;
        }

        public void AssignParameters(IList<ParameterModel> parameters)
        {
            this.IsSubmitted = false;
            if (!this.IsInDesignMode())
            {
                if (this.Parameters != null)
                {
                    this.UnsubscribeParameters();
                }
                List<ParameterModel> list = (this.Parameters ?? emptyParameters).ToList<ParameterModel>();
                this.Parameters = new ReadOnlyCollection<ParameterModel>(parameters ?? emptyParameters.ToList<ParameterModel>());
                Action<ParameterModel> action = <>c.<>9__34_0;
                if (<>c.<>9__34_0 == null)
                {
                    Action<ParameterModel> local3 = <>c.<>9__34_0;
                    action = <>c.<>9__34_0 = x => x.Destroy();
                }
                list.ForEach(action);
                this.RaisePropertyChanged("HasVisibleParameters");
                this.SubscribeParameters();
                this.ValidateParameters();
            }
        }

        private bool CalculateIsChanged()
        {
            bool isChanged = false;
            this.Parameters.ForEach<ParameterModel>(delegate (ParameterModel x) {
                isChanged |= x.IsChanged;
            });
            return isChanged;
        }

        public bool CanResetParameters() => 
            this.CanEdit && this.IsChanged;

        public bool CanSubmitParameters()
        {
            if (!this.CanEdit)
            {
                return false;
            }
            Func<IDataErrorInfo, bool> predicate = <>c.<>9__38_0;
            if (<>c.<>9__38_0 == null)
            {
                Func<IDataErrorInfo, bool> local1 = <>c.<>9__38_0;
                predicate = <>c.<>9__38_0 = x => !string.IsNullOrEmpty(x.Error) || !string.IsNullOrEmpty(x["Value"]);
            }
            return !this.Parameters.Cast<IDataErrorInfo>().Any<IDataErrorInfo>(predicate);
        }

        public static ParametersModel CreateParametersModel() => 
            ViewModelSource.Create<ParametersModel>(Expression.Lambda<Func<ParametersModel>>(Expression.New(typeof(ParametersModel)), new ParameterExpression[0]));

        public ParameterModel FindParameterByName(string name) => 
            this.Parameters.FirstOrDefault<ParameterModel>(x => x.Name == name);

        private void OnParameterPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Visible")
            {
                this.RaisePropertyChanged("HasVisibleParameters");
            }
        }

        private void OnParameterValueChanged(object sender, EventArgs e)
        {
            ParameterModel changedParameterModel = sender as ParameterModel;
            this.IsChanged = this.CalculateIsChanged();
            this.ValidateParameters();
            Func<ParameterModel, bool> predicate = <>c.<>9__43_0;
            if (<>c.<>9__43_0 == null)
            {
                Func<ParameterModel, bool> local1 = <>c.<>9__43_0;
                predicate = <>c.<>9__43_0 = x => x.IsFilteredLookUpSettings;
            }
            if (this.Parameters.Any<ParameterModel>(predicate))
            {
                this.UpdateLookUpValues(changedParameterModel);
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

        private void RaiseValidate(ParameterModel model)
        {
            if (this.validate != null)
            {
                ValidateParameterEventArgs e = new ValidateParameterEventArgs(model);
                this.validate(this, e);
                model.SetError(e.Error);
            }
        }

        public void ResetParameters()
        {
            this.UnsubscribeParameters();
            Action<ParameterModel> action = <>c.<>9__35_0;
            if (<>c.<>9__35_0 == null)
            {
                Action<ParameterModel> local1 = <>c.<>9__35_0;
                action = <>c.<>9__35_0 = x => x.UpdateParameter(UpdateAction.Reset);
            }
            this.Parameters.ForEach<ParameterModel>(action);
            this.UpdateLookUpValues(this.Parameters[0]);
            this.IsChanged = this.CalculateIsChanged();
        }

        public void SubmitParameters()
        {
            Action<ParameterModel> action = <>c.<>9__37_0;
            if (<>c.<>9__37_0 == null)
            {
                Action<ParameterModel> local1 = <>c.<>9__37_0;
                action = <>c.<>9__37_0 = x => x.UpdateParameter(UpdateAction.Submit);
            }
            this.Parameters.ForEach<ParameterModel>(action);
            this.IsSubmitted = true;
            if (this.Submit != null)
            {
                this.Submit(this, EventArgs.Empty);
            }
            this.IsChanged = this.CalculateIsChanged();
        }

        private void SubscribeParameters()
        {
            this.Parameters.ForEach<ParameterModel>(delegate (ParameterModel x) {
                x.PropertyChanged += new PropertyChangedEventHandler(this.OnParameterPropertyChanged);
                x.ValueChanged += new EventHandler(this.OnParameterValueChanged);
            });
        }

        private void UnsubscribeParameters()
        {
            this.Parameters.ForEach<ParameterModel>(delegate (ParameterModel x) {
                x.PropertyChanged -= new PropertyChangedEventHandler(this.OnParameterPropertyChanged);
                x.ValueChanged -= new EventHandler(this.OnParameterValueChanged);
            });
        }

        private void UpdateLookUpEditors(IEnumerable<ParameterLookUpValuesContainer> lookUpValues)
        {
            foreach (ParameterLookUpValuesContainer item in lookUpValues)
            {
                this.Parameters.SingleOrDefault<ParameterModel>(model => ReferenceEquals(model.Parameter, item.Parameter)).Do<ParameterModel>(m => m.UpdateLookUpValues(item.LookUpValues));
            }
        }

        private void UpdateLookUpValues(ParameterModel changedParameterModel)
        {
            <>c__DisplayClass47_0 class_;
            this.UnsubscribeParameters();
            this.LookUpValuesProvider.Do<ILookUpValuesProvider>(delegate (ILookUpValuesProvider x) {
                this.CanEdit = false;
                ParameterValueProvider editorValueProvider = new ParameterValueProvider(this.Parameters);
                this.LookUpValuesProvider.GetLookUpValues(changedParameterModel.Parameter, editorValueProvider).ContinueWith(delegate (Task<IEnumerable<ParameterLookUpValuesContainer>> t) {
                    AggregateException exception = t.Exception;
                    class_.UpdateLookUpEditors(t.Result);
                    class_.SubscribeParameters();
                    class_.CanEdit = true;
                    class_.IsChanged = class_.CalculateIsChanged();
                });
            });
        }

        private void ValidateParameters()
        {
            this.Parameters.ForEach<ParameterModel>(x => this.RaiseValidate(x));
        }

        public virtual ReadOnlyCollection<ParameterModel> Parameters { get; protected set; }

        protected internal virtual bool IsChanged { get; protected set; }

        internal bool IsSubmitted { get; set; }

        public bool HasVisibleParameters
        {
            get
            {
                Func<ParameterModel, bool> predicate = <>c.<>9__18_0;
                if (<>c.<>9__18_0 == null)
                {
                    Func<ParameterModel, bool> local1 = <>c.<>9__18_0;
                    predicate = <>c.<>9__18_0 = x => x.Visible;
                }
                return this.Parameters.Any<ParameterModel>(predicate);
            }
        }

        public virtual bool CanEdit { get; set; }

        internal ILookUpValuesProvider LookUpValuesProvider
        {
            get => 
                this.lookUpValuesProvider;
            set => 
                this.lookUpValuesProvider = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ParametersModel.<>c <>9 = new ParametersModel.<>c();
            public static Func<ParameterModel, bool> <>9__18_0;
            public static Action<ParameterModel> <>9__34_0;
            public static Action<ParameterModel> <>9__35_0;
            public static Action<ParameterModel> <>9__37_0;
            public static Func<IDataErrorInfo, bool> <>9__38_0;
            public static Func<ParameterModel, bool> <>9__43_0;

            internal void <AssignParameters>b__34_0(ParameterModel x)
            {
                x.Destroy();
            }

            internal bool <CanSubmitParameters>b__38_0(IDataErrorInfo x) => 
                !string.IsNullOrEmpty(x.Error) || !string.IsNullOrEmpty(x["Value"]);

            internal bool <get_HasVisibleParameters>b__18_0(ParameterModel x) => 
                x.Visible;

            internal bool <OnParameterValueChanged>b__43_0(ParameterModel x) => 
                x.IsFilteredLookUpSettings;

            internal void <ResetParameters>b__35_0(ParameterModel x)
            {
                x.UpdateParameter(UpdateAction.Reset);
            }

            internal void <SubmitParameters>b__37_0(ParameterModel x)
            {
                x.UpdateParameter(UpdateAction.Submit);
            }
        }
    }
}

