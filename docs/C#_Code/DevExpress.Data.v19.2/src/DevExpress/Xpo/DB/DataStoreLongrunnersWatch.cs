namespace DevExpress.Xpo.DB
{
    using DevExpress.Xpo.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    public class DataStoreLongrunnersWatch : IDataStore, IDataStoreSchemaExplorer, ICommandChannel, ICommandChannelAsync
    {
        public readonly IDataStore Nested;
        public readonly int WatchDelay;
        private Dictionary<TraceItem, object> traceItems = new Dictionary<TraceItem, object>();

        public event EventHandler<LongrunnersReportEventArgs> LongrunnersDetected;

        public DataStoreLongrunnersWatch(IDataStore nested, int watchDelay)
        {
            this.Nested = nested;
            this.WatchDelay = watchDelay;
        }

        private static int callcomp(Call x, Call y) => 
            y.StartTime.CompareTo(x.StartTime);

        protected virtual TraceItem CreateTraceItem(BaseStatement[] stmts) => 
            new TraceItem(this, stmts);

        public object Do(string command, object args) => 
            ((ICommandChannel) this.Nested).Do(command, args);

        public Task<object> DoAsync(string command, object args, CancellationToken cancellationToken = new CancellationToken())
        {
            ICommandChannelAsync nested = this.Nested as ICommandChannelAsync;
            if (nested != null)
            {
                return nested.DoAsync(command, args, cancellationToken);
            }
            object[] objArray1 = new object[] { this.Nested.GetType().FullName };
            throw new InvalidOperationException(DbRes.GetString("Async_CommandChannelDoesNotImplementICommandChannelAsync", objArray1));
        }

        public DBTable[] GetStorageTables(params string[] tables) => 
            ((IDataStoreSchemaExplorer) this.Nested).GetStorageTables(tables);

        public string[] GetStorageTablesList(bool includeViews) => 
            ((IDataStoreSchemaExplorer) this.Nested).GetStorageTablesList(includeViews);

        public ModificationResult ModifyData(params ModificationStatement[] dmlStatements)
        {
            using (this.Trace(dmlStatements))
            {
                return this.Nested.ModifyData(dmlStatements);
            }
        }

        public SelectedData SelectData(params SelectStatement[] selects)
        {
            using (this.Trace(selects))
            {
                return this.Nested.SelectData(selects);
            }
        }

        private void timer(object state)
        {
            EventHandler<LongrunnersReportEventArgs> longrunnersDetected = this.LongrunnersDetected;
            if (longrunnersDetected != null)
            {
                LongrunnersReportEventArgs args;
                Dictionary<TraceItem, object> traceItems = this.traceItems;
                lock (traceItems)
                {
                    if (((TraceItem) state).Timer != null)
                    {
                        args = new LongrunnersReportEventArgs();
                        foreach (TraceItem item2 in this.traceItems.Keys)
                        {
                            Call item = new Call {
                                Statements = item2.Stmts,
                                StartTime = item2.StartTime,
                                Tag = item2.Tag
                            };
                            args.Calls.Add(item);
                            if (item2.Timer != null)
                            {
                                item2.Timer.Dispose();
                                item2.Timer = null;
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                args.Calls.Sort(new Comparison<Call>(DataStoreLongrunnersWatch.callcomp));
                try
                {
                    longrunnersDetected(this, args);
                }
                catch
                {
                }
            }
        }

        private IDisposable Trace(BaseStatement[] stmts) => 
            this.CreateTraceItem(stmts);

        public UpdateSchemaResult UpdateSchema(bool doNotCreateIfFirstTableNotExist, params DBTable[] tables) => 
            this.Nested.UpdateSchema(doNotCreateIfFirstTableNotExist, tables);

        public DevExpress.Xpo.DB.AutoCreateOption AutoCreateOption =>
            this.Nested.AutoCreateOption;

        public class Call
        {
            public object Tag;
            public DateTime StartTime;
            public BaseStatement[] Statements;
        }

        public class LongrunnersReportEventArgs : EventArgs
        {
            public List<DataStoreLongrunnersWatch.Call> Calls = new List<DataStoreLongrunnersWatch.Call>();
            public DateTime ReportTime = DateTime.Now;
        }

        protected class TraceItem : IDisposable
        {
            public readonly DataStoreLongrunnersWatch Owner;
            public readonly BaseStatement[] Stmts;
            public readonly DateTime StartTime;
            public System.Threading.Timer Timer;
            public object Tag;

            public TraceItem(DataStoreLongrunnersWatch owner, BaseStatement[] stmts)
            {
                this.Owner = owner;
                this.Stmts = stmts;
                this.StartTime = DateTime.Now;
                this.Timer = new System.Threading.Timer(new TimerCallback(owner.timer), this, owner.WatchDelay, -1);
                Dictionary<DataStoreLongrunnersWatch.TraceItem, object> traceItems = owner.traceItems;
                lock (traceItems)
                {
                    owner.traceItems.Add(this, null);
                }
            }

            public void Dispose()
            {
                Dictionary<DataStoreLongrunnersWatch.TraceItem, object> traceItems = this.Owner.traceItems;
                lock (traceItems)
                {
                    this.Owner.traceItems.Remove(this);
                    if (this.Timer != null)
                    {
                        this.Timer.Dispose();
                        this.Timer = null;
                    }
                }
            }
        }
    }
}

