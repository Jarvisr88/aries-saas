namespace DMEWorks.Forms
{
    using System;

    public interface IEntityField : IDataStorage
    {
        string Name { get; }

        string Error { get; set; }

        string Warning { get; set; }
    }
}

