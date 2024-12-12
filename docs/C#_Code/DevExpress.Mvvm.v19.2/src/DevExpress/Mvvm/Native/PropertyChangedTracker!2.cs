namespace DevExpress.Mvvm.Native
{
    using DevExpress.Mvvm;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Windows.Threading;

    public class PropertyChangedTracker<T, TProperty> where T: class
    {
        private readonly T obj;
        private readonly Func<T, TProperty> propertyAccessor;
        private readonly Dispatcher dispatcher;
        private readonly string propertyName;
        private readonly Action changedCallBack;

        public PropertyChangedTracker(T obj, Expression<Func<T, TProperty>> propertyExpression, Action changedCallBack)
        {
            this.obj = obj;
            this.propertyName = BindableBase.GetPropertyNameFast(propertyExpression);
            this.propertyAccessor = propertyExpression.Compile();
            this.dispatcher = Dispatcher.CurrentDispatcher;
            this.changedCallBack = changedCallBack;
            ((INotifyPropertyChanged) obj).PropertyChanged += new PropertyChangedEventHandler(this.OnPropertyChanged);
            this.UpdateValue();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.propertyName == e.PropertyName)
            {
                this.dispatcher.VerifyAccess();
                int changeCount = this.ChangeCount;
                this.ChangeCount = changeCount + 1;
                this.UpdateValue();
                this.changedCallBack();
            }
        }

        private void UpdateValue()
        {
            this.Value = this.propertyAccessor(this.obj);
        }

        public TProperty Value { get; private set; }

        public int ChangeCount { get; private set; }
    }
}

