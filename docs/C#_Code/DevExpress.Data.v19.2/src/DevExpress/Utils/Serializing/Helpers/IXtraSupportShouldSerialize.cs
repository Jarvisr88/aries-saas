namespace DevExpress.Utils.Serializing.Helpers
{
    using System;

    public interface IXtraSupportShouldSerialize
    {
        bool ShouldSerialize(string propertyName);
    }
}

