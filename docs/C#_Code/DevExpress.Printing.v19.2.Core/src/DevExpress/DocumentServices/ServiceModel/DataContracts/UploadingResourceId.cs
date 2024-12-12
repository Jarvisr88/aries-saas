namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using DevExpress.Printing.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class UploadingResourceId
    {
        public UploadingResourceId()
        {
        }

        public UploadingResourceId(string value)
        {
            this.Value = value;
        }

        public static UploadingResourceId GenerateNew() => 
            new UploadingResourceId(IdGenerator.GenerateRandomId());

        public override string ToString() => 
            "UploadingResourceId_" + this.Value;

        [DataMember]
        public string Value { get; set; }
    }
}

