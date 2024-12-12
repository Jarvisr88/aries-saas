namespace DevExpress.Utils.Design.DataAccess
{
    using System;

    public interface ICustomBindingProperty
    {
        string PropertyName { get; }

        string DisplayName { get; }

        string Description { get; }
    }
}

