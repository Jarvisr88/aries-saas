namespace DevExpress.XtraEditors.Filtering
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Reflection;

    public interface IBoundPropertyCollection : IEnumerable
    {
        void Add(IBoundProperty property);
        void Clear();
        IBoundPropertyCollection CreateChildrenProperties(IBoundProperty listProperty);
        string GetDisplayPropertyName(OperandProperty property, string fullPath);
        string GetValueScreenText(OperandProperty property, object value);

        IBoundProperty this[int index] { get; }

        IBoundProperty this[string fieldName] { get; }

        int Count { get; }
    }
}

