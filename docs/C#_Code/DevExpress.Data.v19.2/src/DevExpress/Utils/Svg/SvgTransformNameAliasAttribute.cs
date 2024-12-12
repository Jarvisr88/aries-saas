namespace DevExpress.Utils.Svg
{
    using System;
    using System.Runtime.CompilerServices;

    public class SvgTransformNameAliasAttribute : Attribute
    {
        public SvgTransformNameAliasAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}

