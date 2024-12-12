namespace DevExpress.Utils.Serializing.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public class ObjectEventArgs
    {
        public ObjectEventArgs(object obj)
        {
            this.Object = obj;
        }

        public object Object { get; private set; }
    }
}

