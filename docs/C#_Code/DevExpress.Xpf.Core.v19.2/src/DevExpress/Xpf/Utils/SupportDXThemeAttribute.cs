namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Runtime.CompilerServices;

    public class SupportDXThemeAttribute : Attribute
    {
        public SupportDXThemeAttribute()
        {
        }

        public SupportDXThemeAttribute(Type typeInAssembly)
        {
            this.TypeInAssembly = typeInAssembly;
        }

        public Type TypeInAssembly { get; set; }
    }
}

