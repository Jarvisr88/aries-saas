namespace DevExpress.Utils.Design
{
    using System;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Assembly)]
    public class AssemblyServiceClassAttribute : Attribute
    {
        private string typeName;

        public AssemblyServiceClassAttribute(string typeName)
        {
            this.typeName = typeName;
        }

        public static IDXImagesProvider CreateDXImagesProvider(Assembly imagesAssembly)
        {
            AssemblyServiceClassAttribute customAttribute = GetCustomAttribute(imagesAssembly, typeof(AssemblyServiceClassAttribute)) as AssemblyServiceClassAttribute;
            return ((customAttribute != null) ? (imagesAssembly.CreateInstance(customAttribute.TypeName) as IDXImagesProvider) : null);
        }

        public string TypeName =>
            this.typeName;
    }
}

