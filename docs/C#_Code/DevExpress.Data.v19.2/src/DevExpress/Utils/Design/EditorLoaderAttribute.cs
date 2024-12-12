namespace DevExpress.Utils.Design
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
    public class EditorLoaderAttribute : Attribute
    {
        private readonly string typeName;
        private readonly string assemblyName;

        public EditorLoaderAttribute(string typeName, string assemblyName)
        {
            this.typeName = typeName;
            this.assemblyName = assemblyName;
        }

        public string TypeName =>
            this.typeName;

        public string AssemblyName =>
            this.assemblyName;
    }
}

