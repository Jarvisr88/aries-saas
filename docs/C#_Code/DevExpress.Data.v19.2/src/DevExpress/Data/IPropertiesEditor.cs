namespace DevExpress.Data
{
    using System;

    public interface IPropertiesEditor
    {
        void InvalidateData();
        void InvalidateRows();

        object SelectedObject { get; set; }

        bool AutoGenerateRows { get; set; }
    }
}

