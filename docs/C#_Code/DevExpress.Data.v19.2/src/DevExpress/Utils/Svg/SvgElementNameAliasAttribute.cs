namespace DevExpress.Utils.Svg
{
    using System;
    using System.Runtime.CompilerServices;

    public class SvgElementNameAliasAttribute : Attribute
    {
        public SvgElementNameAliasAttribute(string name)
        {
            this.Name = name;
        }

        public sealed override bool Equals(object obj)
        {
            SvgElementNameAliasAttribute attribute = obj as SvgElementNameAliasAttribute;
            return ((attribute != null) && (this.Name == attribute.Name));
        }

        public sealed override int GetHashCode() => 
            this.Name.GetHashCode();

        public string Name { get; private set; }
    }
}

