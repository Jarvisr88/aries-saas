namespace DevExpress.Data.Browsing.Design
{
    using System;
    using System.Collections;

    public interface INode
    {
        void Expand(EventHandler callback);
        bool HasDataSource(object dataSource);

        bool IsDummyNode { get; }

        bool IsDataMemberNode { get; }

        bool IsDataSourceNode { get; }

        bool IsEmpty { get; }

        bool IsList { get; }

        bool IsComplex { get; }

        IList ChildNodes { get; }

        object Parent { get; }

        string DataMember { get; }
    }
}

