namespace DevExpress.Data.Filtering
{
    using System;
    using System.ComponentModel;

    public class UserValueProcessingEventArgs : HandledEventArgs
    {
        public object Value;
        public string Tag;
        public string Data;
    }
}

