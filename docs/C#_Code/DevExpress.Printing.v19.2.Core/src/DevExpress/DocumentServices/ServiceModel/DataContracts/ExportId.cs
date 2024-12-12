namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using DevExpress.Printing.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class ExportId
    {
        public ExportId()
        {
        }

        public ExportId(string exportIdValue)
        {
            this.Value = exportIdValue;
        }

        public static ExportId GenerateNew() => 
            new ExportId(IdGenerator.GenerateRandomId());

        public override string ToString() => 
            "ExportId_" + this.Value;

        [DataMember]
        public string Value { get; set; }
    }
}

