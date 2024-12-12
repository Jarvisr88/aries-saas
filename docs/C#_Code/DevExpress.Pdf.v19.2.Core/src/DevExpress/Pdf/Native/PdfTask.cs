namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf.Interop;
    using System;
    using System.Collections.Generic;
    using System.Security;
    using System.Threading;
    using System.Threading.Tasks;

    public class PdfTask : PdfDisposableObject
    {
        private readonly int index;
        private readonly Action<int> action;
        private readonly AutoResetEvent completedEvent;
        private Task task;
        private Exception exception;

        private PdfTask(int index, Action<int> action)
        {
            this.index = index;
            this.action = action;
            this.completedEvent = new AutoResetEvent(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if ((this.task != null) && this.task.IsCompleted)
                {
                    this.task.Dispose();
                }
                this.completedEvent.Dispose();
            }
        }

        private void Execute()
        {
            try
            {
                this.action(this.index);
            }
            catch (Exception exception)
            {
                this.exception = exception;
            }
            finally
            {
                this.completedEvent.Set();
            }
        }

        [SecuritySafeCritical]
        public static void Execute(Action<int> action, int count)
        {
            PdfTask[] taskArray = new PdfTask[count];
            try
            {
                IntPtr[] handles = new IntPtr[count];
                int index = 0;
                while (true)
                {
                    if (index >= count)
                    {
                        Kernel32Interop.WaitForMultipleObjects(count, handles, true, -1);
                        IList<Exception> innerExceptions = new List<Exception>();
                        PdfTask[] taskArray2 = taskArray;
                        int num2 = 0;
                        while (true)
                        {
                            if (num2 >= taskArray2.Length)
                            {
                                if (innerExceptions.Count <= 0)
                                {
                                    break;
                                }
                                throw new AggregateException(innerExceptions);
                            }
                            PdfTask task2 = taskArray2[num2];
                            if (task2.exception != null)
                            {
                                innerExceptions.Add(task2.exception);
                            }
                            num2++;
                        }
                        break;
                    }
                    PdfTask task = new PdfTask(index, action);
                    taskArray[index] = task;
                    handles[index] = task.CompletedEventHandle;
                    task.Run();
                    index++;
                }
            }
            finally
            {
                foreach (PdfTask task3 in taskArray)
                {
                    task3.Dispose();
                }
            }
        }

        private void Run()
        {
            this.task = Task.Factory.StartNew(new Action(this.Execute), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
        }

        private IntPtr CompletedEventHandle =>
            this.completedEvent.SafeWaitHandle.DangerousGetHandle();
    }
}

