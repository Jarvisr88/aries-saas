namespace DevExpress.Data.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public static class InconsistentHelper
    {
        private static void DoPostponedReload(object state);
        private static void FailUnderAspOrAnotherNonPostEnvironment(InconsistentHelper.MyMethodInvoker failMethod);
        private static bool IsGoodContext(SynchronizationContext context);
        public static void PostponedInconsistent(InconsistentHelper.MyMethodInvoker refreshMethod, InconsistentHelper.MyMethodInvoker failMethod);

        public delegate void MyMethodInvoker();

        private class PostState
        {
            public bool ShouldFailWithException;
            public InconsistentHelper.MyMethodInvoker RefreshMethod;
            public InconsistentHelper.MyMethodInvoker FailMethod;
        }
    }
}

