namespace DevExpress.ReportServer.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class ObjectNotFoundFault
    {
        public ObjectNotFoundFault(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}

