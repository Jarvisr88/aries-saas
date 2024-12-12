namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class Invoker
    {
        private bool lockedBeginInvoke;
        private SynchronizationContext context = (UseSynchronizationContext ? SynchronizationContext.Current : null);

        static Invoker()
        {
            UseSynchronizationContext = true;
        }

        public void BeginInvoke(Action action, bool force = false)
        {
            if (!this.lockedBeginInvoke | force)
            {
                this.lockedBeginInvoke = true;
                if ((this.context != null) && !ReferenceEquals(SynchronizationContext.Current, this.context))
                {
                    this.context.Post(delegate (object state) {
                        action();
                        this.lockedBeginInvoke = false;
                    }, null);
                }
                else
                {
                    action();
                    this.lockedBeginInvoke = false;
                }
            }
        }

        public void Invoke(Action action)
        {
            if ((this.context != null) && !ReferenceEquals(SynchronizationContext.Current, this.context))
            {
                this.context.Send(state => action(), null);
            }
            else
            {
                action();
            }
        }

        internal static bool UseSynchronizationContext { get; set; }
    }
}

