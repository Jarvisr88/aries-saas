namespace DevExpress.Printing
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    internal class SafeGetter
    {
        public static T Get<T>(Func<T> get, T defaultValue, bool? catchException = new bool?()) => 
            New<T>(get, defaultValue, catchException).Value;

        public static T Get<T>(Task<T> task, T defaultValue, bool? catchException = new bool?()) => 
            New<T>(task, defaultValue, catchException).Value;

        private static bool GetActualCatchException(bool? catchException) => 
            (catchException != null) ? catchException.Value : true;

        public static SafeGetter<T> New<T>(Func<T> get, T defaultValue, bool? catchException = new bool?()) => 
            new SafeGetter<T>(get, defaultValue, GetActualCatchException(catchException));

        public static SafeGetter<T> New<T>(Task<T> task, T defaultValue, bool? catchException = new bool?())
        {
            bool actualCatchException = GetActualCatchException(catchException);
            return new SafeGetter<T>(delegate {
                if (!task.IsFaulted || !actualCatchException)
                {
                    return task.Result;
                }
                Tracer.TraceError("DXperience.Reporting", task.Exception.GetBaseException());
                return defaultValue;
            }, defaultValue, actualCatchException);
        }
    }
}

