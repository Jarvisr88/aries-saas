namespace DMEWorks.Core
{
    using System;
    using System.Collections;
    using System.Text;

    public class LoggingProcessor : IDisposable
    {
        private readonly DateTime _starttime;
        private readonly ArrayList _entries;
        private readonly LoggingProcessor _parent;

        protected LoggingProcessor() : this(null)
        {
        }

        protected LoggingProcessor(LoggingProcessor parent)
        {
            this._starttime = DateTime.Now;
            this._parent = parent;
            this._entries = new ArrayList();
        }

        protected void Append(LogEntry entry)
        {
            if (this._parent != null)
            {
                this._parent.Append(entry);
            }
            this._entries.Add(entry);
        }

        public virtual void Dispose()
        {
            this._entries.Clear();
        }

        protected string GetLogText(IFilter filter)
        {
            int capacity = 0;
            for (int i = 0; i < this._entries.Count; i++)
            {
                LogEntry entry = (LogEntry) this._entries[i];
                if (filter.Accept(entry))
                {
                    TimeSpan span = (TimeSpan) (entry.Time - this._starttime);
                    if (span.Days != 0)
                    {
                        capacity += 6;
                    }
                    else if (span.Hours != 0)
                    {
                        capacity += 3;
                    }
                    capacity += 10 + entry.Message.Length;
                }
            }
            StringBuilder builder = new StringBuilder(capacity);
            for (int j = 0; j < this._entries.Count; j++)
            {
                LogEntry entry = (LogEntry) this._entries[j];
                if (filter.Accept(entry))
                {
                    TimeSpan span2 = (TimeSpan) (entry.Time - this._starttime);
                    builder.Append('[');
                    if (span2.Days != 0)
                    {
                        builder.Append(span2.Days);
                        builder.Append('.');
                        builder.Append(span2.Hours.ToString("d2"));
                        builder.Append(':');
                    }
                    else if (span2.Hours != 0)
                    {
                        builder.Append(span2.Hours.ToString("d2"));
                        builder.Append(':');
                    }
                    builder.Append(span2.Minutes.ToString("d2"));
                    builder.Append(':');
                    builder.Append(span2.Seconds.ToString("d2"));
                    builder.Append(']');
                    builder.Append(' ');
                    builder.Append(entry.Message);
                    builder.Append(Environment.NewLine);
                }
            }
            return builder.ToString();
        }

        protected void InternalTraceEvent(TraceEventType eventType, string message)
        {
            this.Append(new LogEntry(this, eventType, message));
        }

        protected void TraceError(object message)
        {
            this.InternalTraceEvent(TraceEventType.Error, (message != null) ? message.ToString() : "");
        }

        protected void TraceError(string format, object arg0)
        {
            this.InternalTraceEvent(TraceEventType.Error, string.Format(format, arg0));
        }

        protected void TraceError(string format, params object[] arg)
        {
            this.InternalTraceEvent(TraceEventType.Error, string.Format(format, arg));
        }

        protected void TraceError(string format, object arg0, object arg1)
        {
            this.InternalTraceEvent(TraceEventType.Error, string.Format(format, arg0, arg1));
        }

        protected void TraceError(string format, object arg0, object arg1, object arg2)
        {
            this.InternalTraceEvent(TraceEventType.Error, string.Format(format, arg0, arg1, arg2));
        }

        protected void TraceEvent(object message)
        {
            this.InternalTraceEvent(TraceEventType.Event, (message != null) ? message.ToString() : "");
        }

        protected void TraceEvent(string format, object arg0)
        {
            this.InternalTraceEvent(TraceEventType.Event, string.Format(format, arg0));
        }

        protected void TraceEvent(string format, params object[] arg)
        {
            this.InternalTraceEvent(TraceEventType.Event, string.Format(format, arg));
        }

        protected void TraceEvent(string format, object arg0, object arg1)
        {
            this.InternalTraceEvent(TraceEventType.Event, string.Format(format, arg0, arg1));
        }

        protected void TraceEvent(string format, object arg0, object arg1, object arg2)
        {
            this.InternalTraceEvent(TraceEventType.Event, string.Format(format, arg0, arg1, arg2));
        }

        protected void TraceVerbose(object message)
        {
            this.InternalTraceEvent(TraceEventType.Verbose, (message != null) ? message.ToString() : "");
        }

        protected void TraceVerbose(string format, object arg0)
        {
            this.InternalTraceEvent(TraceEventType.Verbose, string.Format(format, arg0));
        }

        protected void TraceVerbose(string format, params object[] arg)
        {
            this.InternalTraceEvent(TraceEventType.Verbose, string.Format(format, arg));
        }

        protected void TraceVerbose(string format, object arg0, object arg1)
        {
            this.InternalTraceEvent(TraceEventType.Verbose, string.Format(format, arg0, arg1));
        }

        protected void TraceVerbose(string format, object arg0, object arg1, object arg2)
        {
            this.InternalTraceEvent(TraceEventType.Verbose, string.Format(format, arg0, arg1, arg2));
        }

        public string LogText =>
            this.GetLogText(Filter_All.Default);

        private class Filter_All : LoggingProcessor.IFilter
        {
            public static readonly LoggingProcessor.IFilter Default = new LoggingProcessor.Filter_All();

            private Filter_All()
            {
            }

            public bool Accept(LoggingProcessor.LogEntry entry) => 
                (entry.EventType == LoggingProcessor.TraceEventType.Event) || (entry.EventType == LoggingProcessor.TraceEventType.Error);
        }

        private class Filter_Verbose : LoggingProcessor.IFilter
        {
            public static readonly LoggingProcessor.IFilter Default = new LoggingProcessor.Filter_Verbose();

            private Filter_Verbose()
            {
            }

            public bool Accept(LoggingProcessor.LogEntry entry) => 
                true;
        }

        protected interface IFilter
        {
            bool Accept(LoggingProcessor.LogEntry entry);
        }

        protected class LogEntry
        {
            public readonly DateTime Time = DateTime.Now;
            public readonly LoggingProcessor Owner;
            public readonly string Message;
            public readonly LoggingProcessor.TraceEventType EventType;

            public LogEntry(LoggingProcessor owner, LoggingProcessor.TraceEventType eventType, string message)
            {
                this.Owner = owner;
                this.EventType = eventType;
                this.Message = (message != null) ? message : "";
            }
        }

        protected enum TraceEventType
        {
            Verbose,
            Event,
            Error
        }
    }
}

