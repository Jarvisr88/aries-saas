namespace DevExpress.XtraReports.Native
{
    using DevExpress.Data.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class BindingValueConverterService
    {
        private readonly object syncRoot = new object();
        private readonly Dictionary<Type, EnumConverter> enumConverters = new Dictionary<Type, EnumConverter>();

        public void ClearState()
        {
            this.enumConverters.Clear();
        }

        public static object Convert<T>(T state, Func<T, BindingValueConverterService> getInstance, object value) => 
            !(value is Enum) ? value : GetInstance<T>(state, getInstance).GetEnumConverter(value.GetType()).ConvertToString(value);

        protected virtual EnumConverter CreateEnumConverter(Type type) => 
            new DisplayNameEnumConverter(type);

        public static TypeConverter GetConverter(Type type, IServiceProvider provider = null) => 
            GetConverter<BindingValueConverterService>(TypeDescriptor.GetConverter(type), type, (provider != null) ? provider.GetService<BindingValueConverterService>() : null, <>c.<>9__7_0 ??= x => x);

        public static TypeConverter GetConverter<T>(Type type, T state, Func<T, BindingValueConverterService> getInstance = null) => 
            GetConverter<T>(null, type, state, getInstance);

        private static TypeConverter GetConverter<T>(TypeConverter converter, Type type, T state, Func<T, BindingValueConverterService> getInstance = null) => 
            ((converter?.GetType() == typeof(EnumConverter)) || ((converter == null) && typeof(Enum).IsAssignableFrom(type))) ? GetInstance<T>(state, getInstance).GetEnumConverter(type) : converter;

        private EnumConverter GetEnumConverter(Type type) => 
            this.GetOrCreateEnumConverter(type, new Func<Type, EnumConverter>(this.CreateEnumConverter));

        private static BindingValueConverterService GetInstance<T>(T state, Func<T, BindingValueConverterService> getInstance)
        {
            BindingValueConverterService local1 = getInstance?.Invoke(state);
            BindingValueConverterService instance = local1;
            if (local1 == null)
            {
                BindingValueConverterService local2 = local1;
                instance = Instance;
            }
            return instance;
        }

        private EnumConverter GetOrCreateEnumConverter(Type type, Func<Type, EnumConverter> createConverter)
        {
            object syncRoot = this.syncRoot;
            lock (syncRoot)
            {
                EnumConverter converter;
                if (!this.enumConverters.TryGetValue(type, out converter))
                {
                    converter = createConverter(type);
                    this.enumConverters.Add(type, converter);
                }
                return converter;
            }
        }

        public static BindingValueConverterService Instance { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BindingValueConverterService.<>c <>9 = new BindingValueConverterService.<>c();
            public static Func<BindingValueConverterService, BindingValueConverterService> <>9__7_0;

            internal BindingValueConverterService <GetConverter>b__7_0(BindingValueConverterService x) => 
                x;
        }
    }
}

