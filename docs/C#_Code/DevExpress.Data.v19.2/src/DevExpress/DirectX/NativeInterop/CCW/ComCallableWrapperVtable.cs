namespace DevExpress.DirectX.NativeInterop.CCW
{
    using System;
    using System.Collections.Generic;

    internal class ComCallableWrapperVtable
    {
        private IList<Guid> interfaceIds;
        private IList<IntPtr> methods;

        public ComCallableWrapperVtable(IList<Guid> interfaceIds, IList<IntPtr> methods)
        {
            this.interfaceIds = interfaceIds;
            this.methods = methods;
        }

        public IList<Guid> InterfaceIds =>
            this.interfaceIds;

        public IList<IntPtr> Methods =>
            this.methods;
    }
}

