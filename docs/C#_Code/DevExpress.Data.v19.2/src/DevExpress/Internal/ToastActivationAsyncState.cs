namespace DevExpress.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public static class ToastActivationAsyncState
    {
        public static string ActivationArgs(this Task<ToastNotificationResultInternal> task) => 
            ((State) task.AsyncState).Arguments;

        public static object Create() => 
            new State();

        public static void SetActivationArgs(this Task<ToastNotificationResultInternal> task, string args)
        {
            ((State) task.AsyncState).Arguments = args;
        }

        private sealed class State
        {
            public string Arguments;
        }
    }
}

