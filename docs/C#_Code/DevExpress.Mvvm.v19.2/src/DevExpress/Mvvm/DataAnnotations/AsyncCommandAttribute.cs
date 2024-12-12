namespace DevExpress.Mvvm.DataAnnotations
{
    using System;

    public class AsyncCommandAttribute : CommandAttribute
    {
        public AsyncCommandAttribute()
        {
        }

        public AsyncCommandAttribute(bool isAsyncCommand) : base(isAsyncCommand)
        {
        }

        public bool AllowMultipleExecution
        {
            get => 
                base.AllowMultipleExecutionCore;
            set => 
                base.AllowMultipleExecutionCore = value;
        }
    }
}

