namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Collections.Generic;

    public interface IAttributesProvider
    {
        IEnumerable<Attribute> GetAttributes(string propertyName);
    }
}

