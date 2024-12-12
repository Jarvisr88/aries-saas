namespace DevExpress.Data.Access
{
    using System;

    internal interface IHasDefaultValue
    {
        bool HasDefaultValue { get; }

        object DefaultValue { get; }
    }
}

