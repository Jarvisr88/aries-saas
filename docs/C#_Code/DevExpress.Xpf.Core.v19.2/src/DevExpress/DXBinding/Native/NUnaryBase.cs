namespace DevExpress.DXBinding.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal class NUnaryBase : NBase
    {
        protected NUnaryBase(NBase value)
        {
            this.Value = value;
        }

        public NBase Value { get; set; }
    }
}

