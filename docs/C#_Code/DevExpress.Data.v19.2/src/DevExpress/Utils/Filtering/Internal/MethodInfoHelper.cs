namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    internal static class MethodInfoHelper
    {
        private static TMemberInfo GetMemberInfo<TMemberInfo>(Type sourceType, Func<Type, TMemberInfo> getMember) where TMemberInfo: MemberInfo
        {
            Type[] types = GetTypes(sourceType, sourceType.GetInterfaces());
            for (int i = 0; i < types.Length; i++)
            {
                TMemberInfo local = getMember(types[i]);
                if (local != null)
                {
                    return local;
                }
            }
            return default(TMemberInfo);
        }

        internal static MethodInfo GetMethodInfo(Type sourceType, string methodName) => 
            GetMethodInfo(sourceType, methodName, Type.EmptyTypes, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

        internal static MethodInfo GetMethodInfo(Type sourceType, string methodName, Type[] types, BindingFlags flags = 0x34) => 
            GetMemberInfo<MethodInfo>(sourceType, type => type.GetMethod(methodName, flags, null, types, null));

        private static Type[] GetTypes(Type sourceType, Type[] interfaces)
        {
            Type[] destinationArray = new Type[interfaces.Length + 1];
            Array.Copy(interfaces, destinationArray, interfaces.Length);
            destinationArray[interfaces.Length] = sourceType;
            return destinationArray;
        }
    }
}

