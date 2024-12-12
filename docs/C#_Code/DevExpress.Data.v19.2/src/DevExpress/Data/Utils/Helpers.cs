namespace DevExpress.Data.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Security.Policy;
    using System.Threading;

    public class Helpers
    {
        private static readonly Dictionary<Type, object> enumGenericValuesCache;
        private static readonly Dictionary<Type, List<Enum>> enumValuesCache;

        static Helpers();
        public static string[] GetEnumNames(Type enumType);
        public static T[] GetEnumValues<T>();
        public static T[] GetEnumValues<T>(bool useCache);
        public static Enum[] GetEnumValues(Type enumType);
        public static Enum[] GetEnumValues(Type enumType, bool useCache);
        public static Version GetFrameworkVersion();
        private static Version GetFrameworkVersion_3_X(string keyName, string installValueName);
        public static Assembly LoadWithPartialName(string partialName);
        public static Assembly LoadWithPartialName(string partialName, bool throwException);
        public static Assembly LoadWithPartialName(string partialName, Evidence securityEvidence);
        public static bool WaitOne(WaitHandle waitHandle, int millisecondsTimeout);

        public static bool IsWin8OrHigher { get; }

        public static bool IsWin10CreatorsOrHigher { get; }
    }
}

