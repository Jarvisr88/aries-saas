namespace DevExpress.Xpf.Core
{
    using System;

    public interface IWorkspace
    {
        string Name { get; }

        object SerializationData { get; }
    }
}

