namespace DevExpress.Utils.Design
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class SmartTagFilterAttribute : Attribute
    {
        private Type filterProviderType;

        public SmartTagFilterAttribute(Type filterProviderType)
        {
            this.filterProviderType = filterProviderType;
        }

        public Type FilterProviderType =>
            this.filterProviderType;
    }
}

