namespace DevExpress.XtraPrinting.Preview
{
    using System;
    using System.Runtime.CompilerServices;

    public static class WaitIndicatorExtensions
    {
        public static bool TryHide(this IWaitIndicator serv, object result) => 
            (serv != null) ? serv.Hide(result) : false;

        public static object TryShow(this IWaitIndicator serv, string description) => 
            serv?.Show(description);
    }
}

