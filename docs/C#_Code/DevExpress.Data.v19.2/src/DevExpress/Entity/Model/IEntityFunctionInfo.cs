namespace DevExpress.Entity.Model
{
    using System;

    public interface IEntityFunctionInfo
    {
        string Name { get; }

        IFunctionParameterInfo[] Parameters { get; }

        IEdmComplexTypePropertyInfo[] ResultTypeProperties { get; }
    }
}

