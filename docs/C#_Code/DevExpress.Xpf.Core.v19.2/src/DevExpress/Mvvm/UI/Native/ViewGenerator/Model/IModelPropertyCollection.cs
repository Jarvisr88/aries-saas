namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IModelPropertyCollection : IEnumerable<IModelProperty>, IEnumerable
    {
        IModelProperty Find(string propertyName);
        IModelProperty Find(Type propertyType, string propertyName);

        IModelProperty this[string propertyName] { get; }

        IModelProperty this[DXPropertyIdentifier propertyName] { get; }
    }
}

