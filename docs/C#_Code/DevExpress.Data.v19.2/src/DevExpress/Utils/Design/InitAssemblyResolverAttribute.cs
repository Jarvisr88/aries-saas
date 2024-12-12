namespace DevExpress.Utils.Design
{
    using System;

    public class InitAssemblyResolverAttribute : Attribute
    {
        static InitAssemblyResolverAttribute()
        {
            DXAssemblyResolverEx.Init();
        }
    }
}

