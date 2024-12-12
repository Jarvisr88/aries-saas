namespace DevExpress.DocumentServices.ServiceModel.DataContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class ServiceFault
    {
        public ServiceFault()
        {
        }

        public ServiceFault(Exception exception)
        {
            this.Message = exception.Message;
            this.FullMessage = "";
        }

        public override string ToString()
        {
            string str = (!string.IsNullOrEmpty(this.Message) && !string.IsNullOrEmpty(this.FullMessage)) ? ": " : "";
            return (this.Message + str + this.FullMessage);
        }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string FullMessage { get; set; }
    }
}

