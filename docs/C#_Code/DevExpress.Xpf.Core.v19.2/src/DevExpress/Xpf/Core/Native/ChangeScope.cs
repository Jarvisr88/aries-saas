namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Security;

    [SecuritySafeCritical]
    internal class ChangeScope : DisposableObject
    {
        private readonly WindowGlowWorker worker;

        public ChangeScope(WindowGlowWorker worker);
        protected override void DisposeManagedResources();
    }
}

