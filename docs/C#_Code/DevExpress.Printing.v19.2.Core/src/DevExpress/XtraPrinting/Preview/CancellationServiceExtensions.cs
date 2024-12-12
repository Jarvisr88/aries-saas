namespace DevExpress.XtraPrinting.Preview
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public static class CancellationServiceExtensions
    {
        public static bool CanBeCanceled(this ICancellationService serv) => 
            (serv != null) && ((serv.TokenSource != null) && CanBeCanceled(serv.TokenSource.Token));

        private static bool CanBeCanceled(CancellationToken token) => 
            token.CanBeCanceled && !token.IsCancellationRequested;

        public static bool IsCancellationRequested(this ICancellationService serv) => 
            (serv != null) && ((serv.TokenSource != null) && serv.TokenSource.IsCancellationRequested);

        public static bool TryRegister(this ICancellationService serv, Action callback, bool useSynchronizationContext)
        {
            if ((serv == null) || (serv.TokenSource == null))
            {
                return false;
            }
            serv.TokenSource.Token.Register(callback, useSynchronizationContext);
            return true;
        }
    }
}

