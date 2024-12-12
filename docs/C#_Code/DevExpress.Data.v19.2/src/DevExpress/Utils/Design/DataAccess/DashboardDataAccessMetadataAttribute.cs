namespace DevExpress.Utils.Design.DataAccess
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class DashboardDataAccessMetadataAttribute : DataAccessMetadataAttribute
    {
        public DashboardDataAccessMetadataAttribute(string technologies) : base(technologies)
        {
        }

        public string DesignTimeElementTypeProperty { get; set; }
    }
}

