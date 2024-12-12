namespace DevExpress.Mvvm
{
    using System;

    public class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException() : base("The target service is not found")
        {
        }
    }
}

