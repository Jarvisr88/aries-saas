namespace Dapper
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    internal static class Extensions
    {
        internal static Task<TTo> CastResult<TFrom, TTo>(this Task<TFrom> task) where TFrom: TTo
        {
            if (task == null)
            {
                throw new ArgumentNullException("task");
            }
            if (task.Status == TaskStatus.RanToCompletion)
            {
                return Task.FromResult<TTo>((TTo) task.Result);
            }
            TaskCompletionSource<TTo> state = new TaskCompletionSource<TTo>();
            task.ContinueWith(new Action<Task<TFrom>, object>(Extensions.OnTaskCompleted<TFrom, TTo>), state, TaskContinuationOptions.ExecuteSynchronously);
            return state.Task;
        }

        private static void OnTaskCompleted<TFrom, TTo>(Task<TFrom> completedTask, object state) where TFrom: TTo
        {
            TaskCompletionSource<TTo> source = (TaskCompletionSource<TTo>) state;
            switch (completedTask.Status)
            {
                case TaskStatus.RanToCompletion:
                    source.SetResult((TTo) completedTask.Result);
                    return;

                case TaskStatus.Canceled:
                    source.SetCanceled();
                    return;

                case TaskStatus.Faulted:
                    source.SetException(completedTask.Exception.InnerExceptions);
                    return;
            }
        }
    }
}

