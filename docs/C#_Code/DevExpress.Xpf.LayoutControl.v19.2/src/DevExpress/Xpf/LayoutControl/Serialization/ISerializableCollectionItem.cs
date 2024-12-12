namespace DevExpress.Xpf.LayoutControl.Serialization
{
    using System;

    public interface ISerializableCollectionItem
    {
        string Name { get; set; }

        string TypeName { get; }

        string ParentName { get; set; }

        string ParentCollectionName { get; set; }
    }
}

