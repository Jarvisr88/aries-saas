namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.Serialization;

    [DataContract, KnownType(typeof(ReportNameIdentity))]
    public abstract class InstanceIdentity
    {
        protected InstanceIdentity()
        {
        }
    }
}

