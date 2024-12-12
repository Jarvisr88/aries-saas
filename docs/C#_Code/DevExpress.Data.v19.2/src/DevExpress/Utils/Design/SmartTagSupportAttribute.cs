namespace DevExpress.Utils.Design
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class SmartTagSupportAttribute : Attribute
    {
        private Type boundsProviderType;
        private SmartTagCreationMode creationType;

        public SmartTagSupportAttribute(Type boundsProviderType, SmartTagCreationMode creationType)
        {
            this.creationType = creationType;
            this.boundsProviderType = boundsProviderType;
        }

        public Type BoundsProviderType =>
            this.boundsProviderType;

        public SmartTagCreationMode CreationType =>
            this.creationType;

        public enum SmartTagCreationMode
        {
            UseComponentDesigner,
            Auto
        }
    }
}

