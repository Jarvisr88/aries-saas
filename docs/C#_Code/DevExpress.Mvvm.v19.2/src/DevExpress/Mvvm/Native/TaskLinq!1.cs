namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Threading.Tasks;

    public sealed class TaskLinq<T>
    {
        internal readonly Task<T> Task;
        internal readonly DevExpress.Mvvm.Native.TaskLinq.Chain Chain;

        public TaskLinq(Task<T> task, DevExpress.Mvvm.Native.TaskLinq.Chain chain)
        {
            this.Task = task;
            this.Chain = chain;
        }
    }
}

