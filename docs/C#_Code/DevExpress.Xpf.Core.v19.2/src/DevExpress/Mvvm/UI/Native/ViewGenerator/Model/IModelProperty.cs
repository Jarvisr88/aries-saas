namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;

    public interface IModelProperty
    {
        void ClearValue();
        IModelItem SetValue(object value);

        IModelItem Parent { get; }

        string Name { get; }

        bool IsSet { get; }

        bool IsReadOnly { get; }

        object ComputedValue { get; }

        IModelItemCollection Collection { get; }

        IModelItemDictionary Dictionary { get; }

        IModelItem Value { get; }

        Type PropertyType { get; }
    }
}

