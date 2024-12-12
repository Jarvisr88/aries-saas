namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using DevExpress.Printing.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class DocumentId
    {
        public DocumentId()
        {
        }

        public DocumentId(string value)
        {
            this.Value = value;
        }

        public static DocumentId GenerateNew() => 
            new DocumentId(IdGenerator.GenerateRandomId());

        public override string ToString() => 
            "DocumentId_" + this.Value;

        [DataMember]
        public string Value { get; set; }
    }
}

