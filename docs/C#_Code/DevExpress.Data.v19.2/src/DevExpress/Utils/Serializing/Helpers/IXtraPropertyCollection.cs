namespace DevExpress.Utils.Serializing.Helpers
{
    using System;
    using System.Collections;
    using System.Reflection;

    public interface IXtraPropertyCollection : ICollection, IEnumerable
    {
        void Add(XtraPropertyInfo prop);
        void AddRange(ICollection props);

        XtraPropertyInfo this[string name] { get; }

        XtraPropertyInfo this[int index] { get; }

        bool IsSinglePass { get; }
    }
}

