namespace DevExpress.Services.Internal
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ServiceListChangedEventArgs : EventArgs
    {
        public ServiceListChangedEventArgs(Type service, System.ComponentModel.ListChangedType listChangedType)
        {
            this.Service = service;
            this.ListChangedType = listChangedType;
        }

        public Type Service { get; private set; }

        public System.ComponentModel.ListChangedType ListChangedType { get; private set; }
    }
}

