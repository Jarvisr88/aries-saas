namespace DevExpress.DocumentServices.ServiceModel
{
    using System;

    public interface IClientParameter
    {
        string Description { get; set; }

        string Name { get; }

        System.Type Type { get; }

        object Value { get; set; }

        bool MultiValue { get; }

        bool AllowNull { get; }

        bool Visible { get; set; }
    }
}

