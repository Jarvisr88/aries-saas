namespace DevExpress.Utils.IoC
{
    using System;
    using System.Runtime.CompilerServices;

    public class DependencyAttribute : Attribute
    {
        public DependencyAttribute() : this(true)
        {
        }

        public DependencyAttribute(bool isMandatory)
        {
            this.IsMandatory = isMandatory;
        }

        public bool IsMandatory { get; private set; }
    }
}

