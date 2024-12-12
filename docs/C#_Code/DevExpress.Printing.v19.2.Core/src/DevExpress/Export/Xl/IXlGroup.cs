namespace DevExpress.Export.Xl
{
    using System;

    public interface IXlGroup
    {
        int OutlineLevel { get; set; }

        bool IsCollapsed { get; set; }
    }
}

