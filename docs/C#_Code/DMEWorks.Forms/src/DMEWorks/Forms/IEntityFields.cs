namespace DMEWorks.Forms
{
    using System;
    using System.Collections;
    using System.Reflection;

    public interface IEntityFields : IEnumerable
    {
        int Count { get; }

        IEntityField this[string name] { get; }

        IEntityField this[int index] { get; }
    }
}

