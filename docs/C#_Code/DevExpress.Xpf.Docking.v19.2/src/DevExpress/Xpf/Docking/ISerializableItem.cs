namespace DevExpress.Xpf.Docking
{
    using System;

    public interface ISerializableItem
    {
        string Name { get; set; }

        string TypeName { get; }

        string ParentName { get; set; }

        string ParentCollectionName { get; set; }
    }
}

