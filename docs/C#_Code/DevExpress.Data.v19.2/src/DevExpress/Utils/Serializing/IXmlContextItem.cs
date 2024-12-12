namespace DevExpress.Utils.Serializing
{
    using System;

    public interface IXmlContextItem
    {
        string ValueToString();

        string Name { get; }

        object Value { get; }

        object DefaultValue { get; }
    }
}

