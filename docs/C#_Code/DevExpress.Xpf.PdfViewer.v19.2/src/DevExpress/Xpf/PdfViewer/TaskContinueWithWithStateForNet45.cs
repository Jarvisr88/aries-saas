namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class TaskContinueWithWithStateForNet45
    {
        private static readonly ReflectionHelper Helper = new ReflectionHelper();

        public static Task ContinueWith<T>(this Task<T> task, Action<Task<T>, object> action, object state, CancellationToken token, TaskContinuationOptions continuation, TaskScheduler scheduler) => 
            Helper.GetInstanceMethodHandler<Func<Task<T>, Action<Task<T>, object>, object, CancellationToken, TaskContinuationOptions, TaskScheduler, Task>>(task, "ContinueWith", BindingFlags.Public | BindingFlags.Instance, task.GetType(), 5, null, true)(task, action, state, token, continuation, scheduler);
    }
}

