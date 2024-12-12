namespace DevExpress.Utils.Design
{
    using System;
    using System.IO;
    using System.Reflection;

    public abstract class ToolboxImageAttribute : Attribute
    {
        private string imageName;
        private string assemblyName;
        private System.Reflection.Assembly assembly;

        public ToolboxImageAttribute(string resourceName)
        {
            char[] separator = new char[] { ',' };
            string[] strArray = resourceName.Split(separator, 2);
            this.imageName = strArray[0].Trim();
            this.assemblyName = string.Intern(strArray[1].Trim());
        }

        public override bool Equals(object obj)
        {
            ToolboxImageAttribute attribute = obj as ToolboxImageAttribute;
            return ((attribute == null) ? base.Equals(obj) : (Equals(this.imageName, attribute.imageName) && Equals(this.assemblyName, attribute.assemblyName)));
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        public Stream GetStream() => 
            this.Assembly.GetManifestResourceStream(this.imageName);

        private System.Reflection.Assembly Assembly
        {
            get
            {
                if (this.assembly == null)
                {
                    this.assembly = System.Reflection.Assembly.Load(this.assemblyName);
                }
                return this.assembly;
            }
        }
    }
}

