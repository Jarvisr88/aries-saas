namespace DevExpress.Utils.IoC
{
    using System;
    using System.Runtime.CompilerServices;

    public class InstanceRegistration : Registration
    {
        public InstanceRegistration(object instance)
        {
            this.Instance = instance;
        }

        public object Instance { get; private set; }
    }
}

