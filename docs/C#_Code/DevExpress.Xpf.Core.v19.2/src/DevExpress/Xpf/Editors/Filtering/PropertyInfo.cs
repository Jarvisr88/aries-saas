namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class PropertyInfo
    {
        public PropertyInfo()
        {
        }

        public PropertyInfo(string name, string caption = null, System.Type type = null) : this()
        {
            Guard.ArgumentIsNotNullOrEmpty(name, "name");
            this.Name = name;
            string text1 = caption;
            if (caption == null)
            {
                string local1 = caption;
                text1 = SplitStringHelper.SplitPascalCaseString(this.Name.Replace('.', ' '));
            }
            this.Caption = text1;
            this.Type = type;
        }

        public static PropertyInfo FromPropertyName(string propertyName) => 
            new PropertyInfo(propertyName, null, null);

        public string Name { get; set; }

        public string Caption { get; set; }

        public System.Type Type { get; set; }
    }
}

