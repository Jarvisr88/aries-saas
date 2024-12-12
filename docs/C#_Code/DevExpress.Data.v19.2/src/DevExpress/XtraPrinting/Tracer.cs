namespace DevExpress.XtraPrinting
{
    using DevExpress.Data.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public static class Tracer
    {
        [ThreadStatic]
        private static Dictionary<string, TraceSource> traceSources;

        public static TraceSource GetSource(string traceSource)
        {
            TraceSource source;
            if (!TraceSources.TryGetValue(traceSource, out source))
            {
                source = new TraceSource(traceSource, SourceLevels.Off);
                if (SecurityHelper.IsUnmanagedCodeGranted)
                {
                    RemoveListener(source, "Default");
                }
                TraceSources[traceSource] = source;
            }
            return source;
        }

        public static TraceSource GetSource(string traceSource, SourceLevels mandatoryLevel)
        {
            TraceSource source = GetSource(traceSource);
            if ((source.Switch.Level & mandatoryLevel) != mandatoryLevel)
            {
                SourceSwitch switch1 = source.Switch;
                switch1.Level |= mandatoryLevel;
            }
            return source;
        }

        [Conditional("DEBUGTEST")]
        public static void InitializeSourceForDebug(string traceSource)
        {
            TraceSource source;
            if (Debugger.IsAttached && !TraceSources.TryGetValue(traceSource, out source))
            {
                TraceSources[traceSource] = new TraceSource(traceSource, SourceLevels.Error);
            }
        }

        private static void RemoveListener(TraceSource ts, string name)
        {
            ts.Listeners.Remove(name);
        }

        public static void TraceData(string traceSource, TraceEventType eventType, Func<object> getData)
        {
            TraceSource source;
            if ((traceSources != null) && traceSources.TryGetValue(traceSource, out source))
            {
                object data = getData();
                if (data != null)
                {
                    source.TraceData(eventType, 0, data);
                }
            }
        }

        public static void TraceData(string traceSource, TraceEventType eventType, object data)
        {
            TraceSource source;
            if ((traceSources != null) && traceSources.TryGetValue(traceSource, out source))
            {
                source.TraceData(eventType, 0, data);
            }
        }

        public static void TraceError(string traceSource, object data)
        {
            TraceData(traceSource, TraceEventType.Error, data);
        }

        public static void TraceInformation(string traceSource, object data)
        {
            TraceData(traceSource, TraceEventType.Information, data);
        }

        [Conditional("DEBUG")]
        public static void TraceInformationTest(string traceSource, object data)
        {
            TraceData(traceSource, TraceEventType.Information, data);
        }

        public static void TraceWarning(string traceSource, object data)
        {
            TraceData(traceSource, TraceEventType.Warning, data);
        }

        private static Dictionary<string, TraceSource> TraceSources
        {
            get
            {
                traceSources ??= new Dictionary<string, TraceSource>();
                return traceSources;
            }
        }
    }
}

