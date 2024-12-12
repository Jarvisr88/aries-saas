namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using DevExpress.Data;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class SortingFieldInfoContract
    {
        [DataMember(Name="fieldName")]
        public string FieldName { get; set; }

        [DataMember(Name="sortOrder")]
        public ColumnSortOrder SortOrder { get; set; }
    }
}

