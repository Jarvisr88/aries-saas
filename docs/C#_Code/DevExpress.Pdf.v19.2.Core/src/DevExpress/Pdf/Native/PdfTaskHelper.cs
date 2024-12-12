namespace DevExpress.Pdf.Native
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public static class PdfTaskHelper
    {
        public static void RunParallel<T>(int elementsCount, T sharedData, Action<int, int, T> action)
        {
            int num = Math.Min(elementsCount, Math.Min(10, Environment.ProcessorCount));
            Tuple<int, int>[] source = new Tuple<int, int>[num];
            int num2 = (int) Math.Ceiling((double) (((float) elementsCount) / ((float) num)));
            for (int i = 0; i < num; i++)
            {
                int num4 = Math.Min(num2, elementsCount - (i * num2));
                source[i] = new Tuple<int, int>(i * num2, num4);
            }
            if (num <= 1)
            {
                action(0, elementsCount, sharedData);
            }
            else
            {
                Action<object> action2 = delegate (object obj) {
                    Tuple<int, int> tuple = (Tuple<int, int>) obj;
                    int num = tuple.Item1;
                    action(num, tuple.Item2, sharedData);
                };
                Task[] tasks = new Task[num - 1];
                try
                {
                    int index = 0;
                    while (true)
                    {
                        if (index >= (num - 1))
                        {
                            action2(source.Last<Tuple<int, int>>());
                            Task.WaitAll(tasks);
                            break;
                        }
                        tasks[index] = Task.Factory.StartNew(action2, source[index], CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
                        index++;
                    }
                }
                finally
                {
                    foreach (Task task in tasks)
                    {
                        task.Dispose();
                    }
                }
            }
        }
    }
}

