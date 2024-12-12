namespace DevExpress.XtraPrinting
{
    using System;
    using System.Runtime.CompilerServices;

    public class BrickResolveEventArgs : ResolveEventArgs
    {
        public BrickResolveEventArgs(string name) : base(name)
        {
        }

        public DevExpress.XtraPrinting.Brick Brick { get; set; }
    }
}

