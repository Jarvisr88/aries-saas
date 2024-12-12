namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Filtering;
    using System;
    using System.ComponentModel;

    public abstract class FilterCriteriaBindingTarget : IFilterCriteriaBindingTarget, INotifyPropertyChanged
    {
        private static readonly PropertyChangedEventArgs FilterCriteriaChangedArgs = new PropertyChangedEventArgs("FilterCriteria");
        private int lockFilterCriteriaChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                this.AddFilterCriteriaChanged(value);
            }
            remove
            {
                this.RemoveFilterCriteriaChanged(value);
            }
        }

        protected FilterCriteriaBindingTarget()
        {
        }

        protected abstract void AddFilterCriteriaChanged(PropertyChangedEventHandler handler);
        IDisposable IFilterCriteriaBindingTarget.Lock() => 
            new LockFilterCriteriaChangedToken(this);

        void IFilterCriteriaBindingTarget.RaiseFilterCriteriaChanged()
        {
            if (this.lockFilterCriteriaChanged <= 0)
            {
                PropertyChangedEventHandler filterCriteriaChanged = this.GetFilterCriteriaChanged();
                if (filterCriteriaChanged != null)
                {
                    filterCriteriaChanged(this, FilterCriteriaChangedArgs);
                }
            }
        }

        protected abstract PropertyChangedEventHandler GetFilterCriteriaChanged();
        protected abstract void RemoveFilterCriteriaChanged(PropertyChangedEventHandler handler);

        public abstract CriteriaOperator FilterCriteria { get; set; }

        private sealed class LockFilterCriteriaChangedToken : IDisposable
        {
            private readonly FilterCriteriaBindingTarget target;

            internal LockFilterCriteriaChangedToken(FilterCriteriaBindingTarget target)
            {
                this.target = target;
                target.lockFilterCriteriaChanged++;
            }

            void IDisposable.Dispose()
            {
                this.target.lockFilterCriteriaChanged--;
            }
        }
    }
}

