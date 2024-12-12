namespace DevExpress.Xpf.Data.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public class AsyncTask
    {
        public readonly object Token;
        public readonly object State;
        public readonly Action OnStart;
        public readonly Func<CancellationToken, object, Task<object>> AsyncJob;
        public readonly Action<object, Exception> SyncJob;
        public readonly Action OnFinish;

        private AsyncTask(object token, object state, Action onStart, Func<CancellationToken, object, Task<object>> asyncJob, Action<object, Exception> syncJob, Action onFinish)
        {
            this.Token = token;
            this.State = state;
            this.OnStart = onStart;
            this.AsyncJob = asyncJob;
            this.SyncJob = syncJob;
            this.OnFinish = onFinish;
        }

        public static AsyncTask Create<TIn, TOut>(object token, TIn state, Action onStart, Func<CancellationToken, TIn, Task<TOut>> asyncJob, Action<TOut, Exception> syncJob, Action onFinish) => 
            new AsyncTask(token, state, onStart, delegate (CancellationToken cancellationToken, object x) {
                Func<TOut, object> selector = <>c__0<TIn, TOut>.<>9__0_1;
                if (<>c__0<TIn, TOut>.<>9__0_1 == null)
                {
                    Func<TOut, object> local1 = <>c__0<TIn, TOut>.<>9__0_1;
                    selector = <>c__0<TIn, TOut>.<>9__0_1 = y => y;
                }
                return asyncJob(cancellationToken, (TIn) x).Linq<TOut>(((TaskScheduler) null)).Select<TOut, object>(selector).Schedule<object>(TaskScheduler.Default);
            }, delegate (object x, Exception e) {
                syncJob((TOut) x, e);
            }, onFinish);

        [Serializable, CompilerGenerated]
        private sealed class <>c__0<TIn, TOut>
        {
            public static readonly AsyncTask.<>c__0<TIn, TOut> <>9;
            public static Func<TOut, object> <>9__0_1;

            static <>c__0()
            {
                AsyncTask.<>c__0<TIn, TOut>.<>9 = new AsyncTask.<>c__0<TIn, TOut>();
            }

            internal object <Create>b__0_1(TOut y) => 
                y;
        }
    }
}

