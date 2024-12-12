namespace DevExpress.Xpf.Data.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Data;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class SyncSourceEventsHelper : VirtualSourceEventsHelper
    {
        private static readonly string CreateSourceToken = "CreateSource";
        private bool sourceRequested;
        private bool areSourceRowsThreadSafe;
        private PropertyDescriptor[] uiThreadProperties;
        private PropertyDescriptor[] workerThreadProperties;

        public SyncSourceEventsHelper(ISyncSourceEventsHelperClient client) : base((VirtualSourceBase) client)
        {
        }

        private AsyncTask CreateCreateSourceTask()
        {
            Action onStart = <>c.<>9__16_0;
            if (<>c.<>9__16_0 == null)
            {
                Action local1 = <>c.<>9__16_0;
                onStart = <>c.<>9__16_0 = delegate {
                };
            }
            Action onFinish = <>c.<>9__16_3;
            if (<>c.<>9__16_3 == null)
            {
                Action local2 = <>c.<>9__16_3;
                onFinish = <>c.<>9__16_3 = delegate {
                };
            }
            return AsyncTask.Create<CreateSourceState, object>(CreateSourceToken, new CreateSourceState(), onStart, delegate (CancellationToken cancellationToken, CreateSourceState state) {
                CreateSourceEventArgs e = new CreateSourceEventArgs(cancellationToken);
                EventHandler<CreateSourceEventArgs> handler = this.client.CreateSourceHandler();
                if (handler != null)
                {
                    handler(this.client, e);
                }
                return e.Source.Future<object>();
            }, delegate (object result, Exception exception) {
                this.Source = result;
            }, onFinish);
        }

        protected override AsyncWorkerBase CreateWorker() => 
            new AsyncSerialWorker(new Action(base.client.RaiseIsDataLoadingChanged), delegate {
                EventHandler<DisposeSourceEventArgs> handler = this.client.DisposeSourceHandler();
                if (handler != null)
                {
                    handler(this.client, new DisposeSourceEventArgs(this.Source));
                }
            });

        public Task<FetchRowsResult> GetFetchResult(object[] result, bool hasMoreRows, object nextSkipToken)
        {
            result ??= new object[0];
            return new FetchRowsResult(this.areSourceRowsThreadSafe ? result : ((object[]) (from x in result select new ThreadSafeProxy((from y in this.WorkerThreadProperties select y.GetValue(x)).ToArray<object>())).ToArray<ThreadSafeProxy>()), hasMoreRows, nextSkipToken).Future<FetchRowsResult>();
        }

        protected override PropertyDescriptorCollection GetItemPropertiesCore()
        {
            this.InitProperties();
            return new PropertyDescriptorCollection(this.uiThreadProperties);
        }

        public override Task<object[]> GetSummariesAsync(VirtualSourceEventsHelper.GetTotalSummariesState state, CancellationToken cancellationToken)
        {
            GetSummariesEventArgs e = new GetSummariesEventArgs(cancellationToken, this.Source, state.Summaries, state.Filter);
            this.client.GetTotalSummariesHandler()(this.client, e);
            return e.Result.Future<object[]>();
        }

        public override Task<Either<object[], ValueAndCount[]>> GetUniqueValuesAsync(VirtualSourceEventsHelper.GetUniqueValuesState state, CancellationToken cancellationToken)
        {
            GetUniqueValuesEventArgs e = new GetUniqueValuesEventArgs(cancellationToken, this.Source, state.PropertyName, state.Filter);
            this.client.GetUniqueValuesHandler()(this.client, e);
            return ((e.ResultWithCounts != null) ? Either<object[], ValueAndCount[]>.Right(e.ResultWithCounts) : Either<object[], ValueAndCount[]>.Left(e.Result ?? new object[0])).Future<Either<object[], ValueAndCount[]>>();
        }

        private void InitProperties()
        {
            if (this.uiThreadProperties == null)
            {
                this.workerThreadProperties = base.client.GetSourceProperties();
                if (this.areSourceRowsThreadSafe)
                {
                    this.uiThreadProperties = this.workerThreadProperties;
                }
                else
                {
                    this.workerThreadProperties = this.workerThreadProperties.Where<PropertyDescriptor>(delegate (PropertyDescriptor x) {
                        if (base.client.HasCustomProperties)
                        {
                            return true;
                        }
                        Type propertyType = x.PropertyType;
                        if (propertyType == typeof(Guid))
                        {
                            return true;
                        }
                        TypeCode typeCode = Type.GetTypeCode(Nullable.GetUnderlyingType(propertyType) ?? propertyType);
                        return ((typeCode != TypeCode.Object) && (typeCode != TypeCode.Empty));
                    }).ToArray<PropertyDescriptor>();
                    Func<PropertyDescriptor, int, DynamicPropertyDescriptor> selector = <>c.<>9__23_1;
                    if (<>c.<>9__23_1 == null)
                    {
                        Func<PropertyDescriptor, int, DynamicPropertyDescriptor> local1 = <>c.<>9__23_1;
                        selector = <>c.<>9__23_1 = (x, i) => new DynamicPropertyDescriptor(x.Name, x.PropertyType, obj => ((ThreadSafeProxy) obj).Values[i], x.Attributes.Cast<Attribute>().ToArray<Attribute>());
                    }
                    this.uiThreadProperties = this.workerThreadProperties.Cast<PropertyDescriptor>().Select<PropertyDescriptor, DynamicPropertyDescriptor>(selector).ToArray<DynamicPropertyDescriptor>();
                }
            }
        }

        public override void RequestSourceIfNeeded()
        {
            if (!this.sourceRequested)
            {
                this.sourceRequested = true;
                base.Worker.ReplaceOrAddTask(this.CreateCreateSourceTask());
            }
        }

        public override Task<UpdateRowResult> UpdateRowAsync(VirtualSourceEventsHelper.UpdateRowState state, CancellationToken cancellationToken)
        {
            UpdateRowEventArgs e = new UpdateRowEventArgs(cancellationToken, this.Source, state.Row);
            this.client.UpdateRowHandler()(this.client, e);
            return e.Result.Future<UpdateRowResult>();
        }

        public object Source { get; set; }

        public bool AreSourceRowsThreadSafe
        {
            get
            {
                base.client.VerifyAccess();
                return this.areSourceRowsThreadSafe;
            }
            set
            {
                base.client.VerifyAccess();
                if (this.areSourceRowsThreadSafe != value)
                {
                    base.CheckPropertiesNotSupplied("AreSourceRowsThreadSafe");
                    this.areSourceRowsThreadSafe = value;
                }
            }
        }

        private ISyncSourceEventsHelperClient client =>
            (ISyncSourceEventsHelperClient) base.client;

        private PropertyDescriptor[] WorkerThreadProperties
        {
            get
            {
                this.InitProperties();
                return this.workerThreadProperties;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SyncSourceEventsHelper.<>c <>9 = new SyncSourceEventsHelper.<>c();
            public static Action <>9__16_0;
            public static Action <>9__16_3;
            public static Func<PropertyDescriptor, int, DynamicPropertyDescriptor> <>9__23_1;

            internal void <CreateCreateSourceTask>b__16_0()
            {
            }

            internal void <CreateCreateSourceTask>b__16_3()
            {
            }

            internal DynamicPropertyDescriptor <InitProperties>b__23_1(PropertyDescriptor x, int i) => 
                new DynamicPropertyDescriptor(x.Name, x.PropertyType, obj => ((SyncSourceEventsHelper.ThreadSafeProxy) obj).Values[i], x.Attributes.Cast<Attribute>().ToArray<Attribute>());
        }

        private class CreateSourceState
        {
            public override bool Equals(object obj) => 
                obj is SyncSourceEventsHelper.CreateSourceState;

            public override int GetHashCode()
            {
                throw new NotImplementedException();
            }
        }

        private class ThreadSafeProxy
        {
            public readonly object[] Values;

            public ThreadSafeProxy(object[] values)
            {
                this.Values = values;
            }
        }
    }
}

