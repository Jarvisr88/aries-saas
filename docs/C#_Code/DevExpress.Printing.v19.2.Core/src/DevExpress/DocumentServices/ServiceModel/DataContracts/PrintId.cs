namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using DevExpress.Printing.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class PrintId
    {
        public PrintId()
        {
        }

        public PrintId(string value)
        {
            this.Value = value;
        }

        public static PrintId GenerateNew() => 
            new PrintId(IdGenerator.GenerateRandomId());

        public override string ToString() => 
            "PrintId_" + this.Value;

        [DataMember]
        public string Value { get; set; }
    }
}

