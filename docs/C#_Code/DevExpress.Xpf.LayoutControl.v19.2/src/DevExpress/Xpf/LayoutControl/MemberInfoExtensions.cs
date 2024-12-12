namespace DevExpress.Xpf.LayoutControl
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal static class MemberInfoExtensions
    {
        internal static T GetAttribute<T>(this MemberInfo memberInfo) where T: Attribute => 
            memberInfo.GetCustomAttributes(typeof(T), true).FirstOrDefault<object>();
    }
}

