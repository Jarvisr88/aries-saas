namespace DevExpress.Xpo.DB.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class UnableToCreateDBObjectException : Exception
    {
        private string objectTypeName;
        private string objectName;
        private string parentObjectName;

        protected UnableToCreateDBObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UnableToCreateDBObjectException(string objectTypeName, string objectName, string parentObjectName, Exception innerException) : base(DbRes.GetString("ConnectionProvider_UnableToCreateDBObject", objArray1), innerException)
        {
            object[] objArray1 = new object[] { objectTypeName, objectName, parentObjectName, innerException.Message };
            this.objectTypeName = objectTypeName;
            this.objectName = objectName;
            this.parentObjectName = parentObjectName;
        }

        public string ObjectTypeName
        {
            get => 
                this.objectTypeName;
            set => 
                this.objectTypeName = value;
        }

        public string ObjectName
        {
            get => 
                this.objectName;
            set => 
                this.objectName = value;
        }

        public string ParentObjectName
        {
            get => 
                this.parentObjectName;
            set => 
                this.parentObjectName = value;
        }
    }
}

