namespace DevExpress.Xpf.Data.Native
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Data;
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Threading.Tasks;

    public abstract class VirtualSourceEventsHelper
    {
        protected readonly VirtualSourceBase client;
        public readonly AsyncWorkerBase Worker;
        private bool propertiesSupplied;

        internal VirtualSourceEventsHelper(VirtualSourceBase client)
        {
            this.client = client;
            this.Worker = this.CreateWorker();
        }

        public void CheckPropertiesNotSupplied(string propertyName)
        {
            if (this.propertiesSupplied)
            {
                throw new InvalidOperationException($"{propertyName} cannot be assigned after the ITypedList.GetItemProperties method has been called.");
            }
        }

        protected abstract AsyncWorkerBase CreateWorker();
        public PropertyDescriptorCollection GetItemProperties()
        {
            this.propertiesSupplied = true;
            return this.GetItemPropertiesCore();
        }

        protected abstract PropertyDescriptorCollection GetItemPropertiesCore();
        public abstract Task<object[]> GetSummariesAsync(GetTotalSummariesState state, CancellationToken cancellationToken);
        public abstract Task<Either<object[], ValueAndCount[]>> GetUniqueValuesAsync(GetUniqueValuesState state, CancellationToken cancellationToken);
        public abstract void RequestSourceIfNeeded();
        public abstract Task<UpdateRowResult> UpdateRowAsync(UpdateRowState state, CancellationToken cancellationToken);

        public class GetTotalSummariesState
        {
            public readonly SummaryDefinition[] Summaries;
            public readonly CriteriaOperator Filter;

            public GetTotalSummariesState(SummaryDefinition[] summaries, CriteriaOperator filter)
            {
                this.Summaries = summaries;
                this.Filter = filter;
            }

            public override bool Equals(object obj)
            {
                VirtualSourceEventsHelper.GetTotalSummariesState state = obj as VirtualSourceEventsHelper.GetTotalSummariesState;
                return ((state != null) && (this.Summaries.SafeListsEqual<SummaryDefinition>(state.Summaries) && Equals(this.Filter, state.Filter)));
            }

            public override int GetHashCode()
            {
                throw new NotImplementedException();
            }
        }

        public class GetUniqueValuesState
        {
            public readonly string PropertyName;
            public readonly CriteriaOperator Filter;

            public GetUniqueValuesState(string propertyName, CriteriaOperator filter)
            {
                this.PropertyName = propertyName;
                this.Filter = filter;
            }

            public override bool Equals(object obj)
            {
                VirtualSourceEventsHelper.GetUniqueValuesState state = obj as VirtualSourceEventsHelper.GetUniqueValuesState;
                return ((state != null) && ((this.PropertyName == state.PropertyName) && Equals(this.Filter, state.Filter)));
            }

            public override int GetHashCode()
            {
                throw new NotImplementedException();
            }
        }

        public class UpdateRowState
        {
            public readonly object Row;

            public UpdateRowState(object row)
            {
                this.Row = row;
            }

            public override bool Equals(object obj)
            {
                throw new NotSupportedException();
            }

            public override int GetHashCode()
            {
                throw new NotSupportedException();
            }
        }
    }
}

