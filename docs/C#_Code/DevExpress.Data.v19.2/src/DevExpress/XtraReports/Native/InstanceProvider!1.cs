namespace DevExpress.XtraReports.Native
{
    using System;
    using System.Collections.Generic;

    public class InstanceProvider<InstanceType> where InstanceType: class
    {
        private Dictionary<string, InstanceType> repository;

        public InstanceProvider();
        protected InstanceType GetInstance(string contextName);
        protected bool RemoveInstance(string contextName);
        protected void SetInstance(string contextName, InstanceType value);
    }
}

