namespace DevExpress.Utils.Design.DataAccess
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class OLAPDataAccessMetadataAttribute : DataAccessMetadataAttribute
    {
        public OLAPDataAccessMetadataAttribute(string technologies) : base(technologies)
        {
        }

        public string OLAPConnectionStringProperty { get; set; }

        public string OLAPDataProviderProperty { get; set; }
    }
}

