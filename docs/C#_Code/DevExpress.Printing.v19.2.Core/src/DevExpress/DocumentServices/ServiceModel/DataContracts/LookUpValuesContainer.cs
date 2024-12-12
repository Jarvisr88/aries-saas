namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class LookUpValuesContainer
    {
        public ParameterLookUpValues[] LookUpValues { get; set; }
    }
}

