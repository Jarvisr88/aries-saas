namespace DevExpress.Utils.Design.DataAccess
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
    public class DataAccessMetadataAttribute : Attribute
    {
        private string supportedTechnologiesCore;

        public DataAccessMetadataAttribute(string technologies)
        {
            this.supportedTechnologiesCore = technologies;
            this.EnableDirectBinding = true;
            this.EnableInMemoryCollectionViewBinding = true;
            this.EnableBindingToXPOServerMode = true;
        }

        public string SupportedTechnologies =>
            this.supportedTechnologiesCore;

        public string SupportedProcessingModes { get; set; }

        public string DataSourceProperty { get; set; }

        public string DataMemberProperty { get; set; }

        public string Platform { get; set; }

        public bool EnableDirectBinding { get; set; }

        public bool EnableInMemoryCollectionViewBinding { get; set; }

        public bool EnableBindingToEnum { get; set; }

        public bool EnableBindingToControlRows { get; set; }

        public bool EnableBindingToObjectDataSource { get; set; }

        public bool EnableBindingToXPOServerMode { get; set; }
    }
}

