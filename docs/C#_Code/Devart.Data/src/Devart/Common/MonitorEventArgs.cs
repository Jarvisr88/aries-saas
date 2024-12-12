namespace Devart.Common
{
    using System;

    public class MonitorEventArgs : EventArgs
    {
        private Devart.Common.c a;
        private MonitorEventType b;
        private string c;
        private MonitorTracePoint d;
        private string e;
        private string[] f;
        private double g;

        internal MonitorEventArgs(Devart.Common.c A_0, string A_1, MonitorTracePoint A_2, string A_3, string[] A_4, double A_5)
        {
            this.a = A_0;
            this.b = this.a(A_0);
            this.c = A_1;
            this.d = A_2;
            this.e = A_3;
            this.f = A_4;
            this.g = A_5;
        }

        private MonitorEventType a(Devart.Common.c A_0)
        {
            int num = (int) A_0;
            if (num <= 8)
            {
                return (MonitorEventType) num;
            }
            switch (A_0)
            {
                case Devart.Common.c.e:
                case Devart.Common.c.j:
                case Devart.Common.c.k:
                case Devart.Common.c.p:
                    return MonitorEventType.Custom;

                case Devart.Common.c.l:
                case Devart.Common.c.m:
                    return MonitorEventType.Connect;

                case Devart.Common.c.n:
                    return MonitorEventType.ActivateInPool;

                case Devart.Common.c.o:
                    return MonitorEventType.ReturnToPool;
            }
            throw new NotSupportedException(A_0.ToString());
        }

        public MonitorEventType EventType =>
            this.b;

        internal Devart.Common.c EventTypeInternal =>
            this.a;

        internal bool IsUserEvent
        {
            get
            {
                Devart.Common.c a = this.a;
                return ((a != Devart.Common.c.j) && ((a != Devart.Common.c.k) && (a != Devart.Common.c.p)));
            }
        }

        public string Description =>
            this.c;

        public MonitorTracePoint TracePoint =>
            this.d;

        public string ExtraInfo =>
            this.e;

        public string[] CallStack =>
            this.f;

        public double Duration =>
            this.g;
    }
}

