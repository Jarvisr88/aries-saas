namespace DMEWorks.Core
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;

    public abstract class BaseTraceListener : TraceListener
    {
        public BaseTraceListener()
        {
        }

        public BaseTraceListener(string name) : base(name)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Close();
            }
        }

        internal bool IsEnabled(TraceOptions opts) => 
            (opts & base.TraceOutputOptions) != TraceOptions.None;

        public override void TraceData(TraceEventCache eventCache, string source, System.Diagnostics.TraceEventType eventType, int id, params object[] data)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
            {
                this.WriteHeader(source, eventType, id);
                StringBuilder builder = new StringBuilder();
                if (data != null)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (i != 0)
                        {
                            builder.Append(", ");
                        }
                        if (data[i] != null)
                        {
                            builder.Append(data[i].ToString());
                        }
                    }
                }
                this.WriteLine(builder.ToString());
                this.WriteFooter(eventCache);
            }
        }

        public override void TraceData(TraceEventCache eventCache, string source, System.Diagnostics.TraceEventType eventType, int id, object data)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                this.WriteHeader(source, eventType, id);
                this.WriteLine((data != null) ? data.ToString() : string.Empty);
                this.WriteFooter(eventCache);
            }
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, System.Diagnostics.TraceEventType eventType, int id, string message)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
            {
                this.WriteHeader(source, eventType, id);
                this.WriteLine(message);
                this.WriteFooter(eventCache);
            }
        }

        public override void TraceEvent(TraceEventCache eventCache, string source, System.Diagnostics.TraceEventType eventType, int id, string format, params object[] args)
        {
            if ((base.Filter == null) || base.Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
            {
                this.WriteHeader(source, eventType, id);
                if (args != null)
                {
                    this.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
                }
                else
                {
                    this.WriteLine(format);
                }
                this.WriteFooter(eventCache);
            }
        }

        private void WriteFooter(TraceEventCache eventCache)
        {
            if (eventCache != null)
            {
                base.IndentLevel++;
                if (this.IsEnabled(TraceOptions.ProcessId))
                {
                    this.WriteLine("ProcessId=" + eventCache.ProcessId.ToString());
                }
                if (this.IsEnabled(TraceOptions.LogicalOperationStack))
                {
                    this.Write("LogicalOperationStack=");
                    bool flag = true;
                    foreach (object obj2 in eventCache.LogicalOperationStack)
                    {
                        if (!flag)
                        {
                            this.Write(", ");
                        }
                        else
                        {
                            flag = false;
                        }
                        this.Write(obj2.ToString());
                    }
                    this.WriteLine(string.Empty);
                }
                if (this.IsEnabled(TraceOptions.ThreadId))
                {
                    this.WriteLine("ThreadId=" + eventCache.ThreadId);
                }
                if (this.IsEnabled(TraceOptions.DateTime))
                {
                    this.WriteLine("DateTime=" + eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
                }
                if (this.IsEnabled(TraceOptions.Timestamp))
                {
                    this.WriteLine("Timestamp=" + eventCache.Timestamp.ToString());
                }
                if (this.IsEnabled(TraceOptions.Callstack))
                {
                    this.WriteLine("Callstack=" + eventCache.Callstack);
                }
                base.IndentLevel--;
            }
        }

        private void WriteHeader(string source, System.Diagnostics.TraceEventType eventType, int id)
        {
        }
    }
}

