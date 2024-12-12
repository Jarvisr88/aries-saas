namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ServiceKnownTypeProvider
    {
        private static readonly object syncLock = new object();
        private static bool configurationLocked;
        private static readonly HashSet<Type> knownTypes;
        public const string GetKnownTypesMethodName = "GetKnownTypes";

        static ServiceKnownTypeProvider()
        {
            HashSet<Type> set = new HashSet<Type> {
                typeof(string),
                typeof(string[]),
                typeof(DateTime),
                typeof(DateTime[]),
                typeof(Range<DateTime>),
                typeof(int),
                typeof(int[]),
                typeof(Range<int>),
                typeof(long),
                typeof(long[]),
                typeof(Range<long>),
                typeof(float),
                typeof(float[]),
                typeof(Range<float>),
                typeof(double),
                typeof(double[]),
                typeof(Range<double>),
                typeof(decimal),
                typeof(decimal[]),
                typeof(Range<decimal>),
                typeof(bool),
                typeof(bool[]),
                typeof(Guid),
                typeof(Guid[]),
                typeof(uint),
                typeof(uint[]),
                typeof(Range<uint>),
                typeof(short),
                typeof(short[]),
                typeof(Range<short>),
                typeof(ushort),
                typeof(ushort[]),
                typeof(Range<ushort>),
                typeof(ulong),
                typeof(ulong[]),
                typeof(Range<ulong>),
                typeof(byte),
                typeof(byte[]),
                typeof(Range<byte>),
                typeof(sbyte),
                typeof(sbyte[]),
                typeof(Range<sbyte>),
                typeof(Range<string>)
            };
            knownTypes = set;
        }

        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider)
        {
            object syncLock = ServiceKnownTypeProvider.syncLock;
            lock (syncLock)
            {
                configurationLocked = true;
                return knownTypes.ToArray<Type>();
            }
        }

        public static bool IsRegistered(Type type)
        {
            Guard.ArgumentNotNull(type, "type");
            object syncLock = ServiceKnownTypeProvider.syncLock;
            lock (syncLock)
            {
                return knownTypes.Contains(type);
            }
        }

        public static void Register<T>()
        {
            Register(typeof(T));
        }

        public static void Register(IEnumerable<Type> types)
        {
            Guard.ArgumentNotNull(types, "types");
            foreach (Type type in types)
            {
                Register(type);
            }
        }

        public static void Register(Type type)
        {
            Guard.ArgumentNotNull(type, "type");
            object syncLock = ServiceKnownTypeProvider.syncLock;
            lock (syncLock)
            {
                if (configurationLocked)
                {
                    throw new InvalidOperationException("Can not register a new type because ServiceKnownTypeProvider is already read-only. See the following article for more information: https://go.devexpress.com/Jan2019_Deserialization_Issue_ServiceKnownTypeProvider.aspx");
                }
                knownTypes.Add(type);
            }
        }

        public static void Register(params Type[] types)
        {
            Register((IEnumerable<Type>) types);
        }
    }
}

