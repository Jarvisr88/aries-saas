namespace DevExpress.Utils.Svg
{
    using System;
    using System.Runtime.CompilerServices;

    public class SvgPropertyNameAliasAttribute : Attribute
    {
        public SvgPropertyNameAliasAttribute(string name)
        {
            this.Name = name;
        }

        public sealed override bool Equals(object obj)
        {
            SvgPropertyNameAliasAttribute attribute = obj as SvgPropertyNameAliasAttribute;
            return ((attribute != null) && string.Equals(this.Name, attribute.Name, StringComparison.OrdinalIgnoreCase));
        }

        public sealed override int GetHashCode() => 
            this.Name.GetHashCode();

        public string Name { get; private set; }
    }
}

