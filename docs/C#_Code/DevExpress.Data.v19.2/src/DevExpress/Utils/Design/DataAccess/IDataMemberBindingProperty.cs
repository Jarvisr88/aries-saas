namespace DevExpress.Utils.Design.DataAccess
{
    using System;

    public interface IDataMemberBindingProperty : ICustomBindingProperty
    {
        string DataMember { get; }
    }
}

