namespace DevExpress.Xpf.Core.ReflectionExtensions.Attributes
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Interface, AllowMultiple=true)]
    public class AssignableFromAttribute : Attribute
    {
        private string typeName;

        public AssignableFromAttribute(string typeName)
        {
            this.typeName = typeName;
        }

        public AssignableFromAttribute(Type type) : this(type.FullName)
        {
        }

        public string GetTypeName() => 
            this.typeName;

        public bool Inverse { get; set; }
    }
}

