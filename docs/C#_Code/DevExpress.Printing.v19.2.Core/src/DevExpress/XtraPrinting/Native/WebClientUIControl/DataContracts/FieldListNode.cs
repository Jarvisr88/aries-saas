namespace DevExpress.XtraPrinting.Native.WebClientUIControl.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class FieldListNode
    {
        [DataMember(Name="name")]
        public string Name { get; set; }

        [DataMember(Name="displayName")]
        public string DisplayName { get; set; }

        [DataMember(Name="isList")]
        public bool IsList { get; set; }

        [DataMember(Name="specifics")]
        public string Specifics { get; set; }
    }
}

