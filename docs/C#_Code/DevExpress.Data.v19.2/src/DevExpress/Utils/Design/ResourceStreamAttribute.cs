namespace DevExpress.Utils.Design
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [Obsolete("The ResourceStreamAttribute has become obsolete. Use the ToolboxBitmap24Attribute or ToolboxBitmap32Attribute instead.")]
    public sealed class ResourceStreamAttribute : Attribute
    {
        private string resourceName;
        private System.Reflection.Assembly assembly;

        public ResourceStreamAttribute(string name, string resourceName)
        {
            this.Name = name;
            this.resourceName = resourceName;
        }

        public override bool Equals(object obj)
        {
            ResourceStreamAttribute attribute = obj as ResourceStreamAttribute;
            return ((attribute == null) ? base.Equals(obj) : (Equals(this.Name, attribute.Name) && Equals(this.resourceName, attribute.resourceName)));
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public Stream GetStream()
        {
            char[] separator = new char[] { ',' };
            string[] strArray = this.resourceName.Split(separator);
            return this.Assembly.GetManifestResourceStream(strArray[0].Trim());
        }

        public string Name { get; private set; }

        private System.Reflection.Assembly Assembly
        {
            get
            {
                if (this.assembly == null)
                {
                    char[] separator = new char[] { ',' };
                    string[] strArray = this.resourceName.Split(separator);
                    this.assembly = System.Reflection.Assembly.Load(strArray[1].Trim());
                }
                return this.assembly;
            }
        }
    }
}

