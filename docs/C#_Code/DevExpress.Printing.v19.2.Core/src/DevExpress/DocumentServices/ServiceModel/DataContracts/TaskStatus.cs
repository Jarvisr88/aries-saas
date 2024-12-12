namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public enum TaskStatus
    {
        [EnumMember]
        Fault = 0,
        [EnumMember]
        InProgress = 1,
        [EnumMember]
        Complete = 2
    }
}

