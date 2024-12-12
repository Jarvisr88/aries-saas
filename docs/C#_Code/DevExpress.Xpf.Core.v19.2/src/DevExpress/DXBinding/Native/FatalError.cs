namespace DevExpress.DXBinding.Native
{
    using System;

    internal class FatalError : Exception
    {
        public FatalError(string m) : base(m)
        {
        }
    }
}

