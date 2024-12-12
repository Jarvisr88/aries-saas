namespace DevExpress.XtraPrinting
{
    using System;
    using System.Reflection;

    public interface IBrickOwnerRepository
    {
        IBrickOwner this[string name] { get; }
    }
}

