namespace DevExpress.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    public class ToastNotificationFailedException : Exception
    {
        public ToastNotificationFailedException(Exception inner, int errorCode) : base(null, inner)
        {
            this.ErrorCode = errorCode;
        }

        [SecuritySafeCritical]
        internal static ToastNotificationFailedException ToException(int hResult)
        {
            try
            {
                Marshal.ThrowExceptionForHR(hResult);
                return null;
            }
            catch (Exception exception1)
            {
                return new ToastNotificationFailedException(exception1, hResult);
            }
        }

        public override string Message =>
            base.InnerException.Message;

        public int ErrorCode { get; private set; }
    }
}

