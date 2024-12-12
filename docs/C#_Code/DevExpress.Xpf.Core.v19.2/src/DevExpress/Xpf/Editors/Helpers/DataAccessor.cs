namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Access;
    using DevExpress.Data.Async.Helpers;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Editors.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class DataAccessor
    {
        [ThreadStatic]
        private static ReflectionHelper helper;
        private readonly Dictionary<string, PropertyDescriptor> descriptors = new Dictionary<string, PropertyDescriptor>();
        private readonly List<PropertyDescriptor> searchDescriptors = new List<PropertyDescriptor>();
        private readonly Locker endInitLocker = new Locker();
        private Func<object, object> getValueHandler;
        private Func<object, object> getDisplayValueHandler;
        private Action<object, object>[] setHandlers;
        private Func<object, object>[] getHandlers;
        private Dictionary<string, Func<object, object>> getHandlerNames;
        private Func<object, object>[] getFromSourceHandlers;

        public void BeginInit()
        {
            this.endInitLocker.Lock();
        }

        private PropertyDescriptor CalcGetValueFromDescriptor(PropertyDescriptor descriptor) => 
            DataListDescriptor.GetFastProperty(descriptor);

        public DataProxy CreateProxy(object component, int listSourceIndex)
        {
            DataProxy proxy = (DataProxy) Activator.CreateInstance(this.ElementType);
            proxy.f_component = component;
            proxy.f_visibleIndex = listSourceIndex;
            if (component != null)
            {
                for (int i = 0; i < this.getFromSourceHandlers.Length; i++)
                {
                    this.SetValue(proxy, i, this.getFromSourceHandlers[i](component));
                }
            }
            return proxy;
        }

        private Type CreateProxyType(IEnumerable<PropertyDescriptor> coll)
        {
            Func<KeyValuePair<string, PropertyDescriptor>, string> keySelector = <>c.<>9__57_0;
            if (<>c.<>9__57_0 == null)
            {
                Func<KeyValuePair<string, PropertyDescriptor>, string> local1 = <>c.<>9__57_0;
                keySelector = <>c.<>9__57_0 = pair => pair.Key;
            }
            return DynamicTypeBuilder.GetDynamicType(this.descriptors.ToDictionary<KeyValuePair<string, PropertyDescriptor>, string, Type>(keySelector, pair => this.GetPropertryTypeFromDescriptor(pair.Value)), typeof(DataProxy), null);
        }

        public void EndInit()
        {
            this.endInitLocker.Unlock();
            List<PropertyDescriptor> coll = this.descriptors.Values.Select<PropertyDescriptor, PropertyDescriptor>(new Func<PropertyDescriptor, PropertyDescriptor>(this.CalcGetValueFromDescriptor)).ToList<PropertyDescriptor>();
            this.ElementType = this.CreateProxyType(coll);
            this.InitializeGetValueHandlers(coll);
            this.InitializeSetValueHandlers(coll);
            this.InitializeGetDisplayAndValueHandlers();
            this.InitializeSearchDescriptors(coll);
            this.InitializeGetFromSourceHandlers(coll);
        }

        public void Fetch(string fieldName)
        {
            PropertyDescriptor descriptor;
            if (!this.endInitLocker.IsLocked)
            {
                throw new ArgumentException("lock");
            }
            if (!string.IsNullOrEmpty(fieldName) && !this.descriptors.TryGetValue(fieldName, out descriptor))
            {
                descriptor = new EditorsDataControllerWrappedDescriptor(LookUpPropertyDescriptorType.Value, fieldName, fieldName);
                this.descriptors.Add(fieldName, descriptor);
            }
        }

        private Func<object, object> FilterDBNull(Func<object, object> func) => 
            delegate (object x) {
                object obj2 = func(x);
                return (obj2 == DBNull.Value) ? null : obj2;
            };

        public void GenerateDefaultDescriptors(string valueMember, string displayMember, Func<string, PropertyDescriptor> getDescriptorHandler)
        {
            if (!this.endInitLocker.IsLocked)
            {
                throw new ArgumentException("lock");
            }
            this.ValueMember = valueMember;
            this.HasValueMember = !string.IsNullOrEmpty(valueMember);
            string arg = this.HasValueMember ? valueMember : "ValueColumn";
            Func<string, PropertyDescriptor> func1 = getDescriptorHandler;
            if (getDescriptorHandler == null)
            {
                Func<string, PropertyDescriptor> local1 = getDescriptorHandler;
                func1 = <>c.<>9__52_0;
                if (<>c.<>9__52_0 == null)
                {
                    Func<string, PropertyDescriptor> local2 = <>c.<>9__52_0;
                    func1 = <>c.<>9__52_0 = (Func<string, PropertyDescriptor>) (x => null);
                }
            }
            getDescriptorHandler = func1;
            PropertyDescriptor local3 = getDescriptorHandler(arg);
            PropertyDescriptor local7 = local3;
            if (local3 == null)
            {
                PropertyDescriptor local4 = local3;
                local7 = new LookUpPropertyDescriptor(LookUpPropertyDescriptorType.Value, arg, this.HasValueMember ? arg : string.Empty);
            }
            this.descriptors.Add(arg, local7);
            this.ValuePropertyName = arg;
            this.HasDisplayMember = !string.IsNullOrEmpty(displayMember);
            string key = this.HasDisplayMember ? displayMember : "DisplayColumn";
            if (!this.descriptors.ContainsKey(key))
            {
                this.descriptors.Add(key, getDescriptorHandler(key) ?? new LookUpPropertyDescriptor(LookUpPropertyDescriptorType.Display, key, this.HasDisplayMember ? key : string.Empty));
            }
            this.DisplayMember = displayMember;
            this.DisplayPropertyName = key;
        }

        public object GetDisplayValue(DataProxy proxy)
        {
            if (proxy == null)
            {
                return null;
            }
            object obj2 = this.getDisplayValueHandler(proxy);
            return ((LookUpPropertyDescriptorBase.UnsetValue == obj2) ? null : obj2);
        }

        private Type GetPropertryTypeFromDescriptor(PropertyDescriptor desc) => 
            (desc.GetType().FullName != "System.Data.DataColumnPropertyDescriptor") ? (!(desc.GetType() == typeof(ReadonlyThreadSafeProxyForObjectFromAnotherThreadPropertyDescriptor)) ? desc.PropertyType : typeof(object)) : typeof(object);

        public object GetPropertyValue(DataProxy proxy, string name)
        {
            Func<object, object> func;
            return ((this.getHandlerNames != null) ? (!this.getHandlerNames.TryGetValue(name, out func) ? null : func(proxy)) : null);
        }

        public object GetValue(DataProxy proxy) => 
            (proxy != null) ? this.getValueHandler(proxy) : null;

        private void InitializeGetDisplayAndValueHandlers()
        {
            int? parametersCount = null;
            this.getValueHandler = Helper.GetInstanceMethodHandler<Func<object, object>>(null, "get_" + this.ValuePropertyName, BindingFlags.Public | BindingFlags.Instance, this.ElementType, parametersCount, null, true);
            parametersCount = null;
            this.getDisplayValueHandler = Helper.GetInstanceMethodHandler<Func<object, object>>(null, "get_" + this.DisplayPropertyName, BindingFlags.Public | BindingFlags.Instance, this.ElementType, parametersCount, null, true);
        }

        private void InitializeGetFromSourceHandlers(List<PropertyDescriptor> coll)
        {
            this.getFromSourceHandlers = new Func<object, object>[coll.Count];
            for (int i = 0; i < coll.Count; i++)
            {
                PropertyDescriptor local1 = coll[i];
                this.getFromSourceHandlers[i] = this.FilterDBNull(new Func<object, object>(local1.GetValue));
            }
        }

        private void InitializeGetValueHandlers(IList<PropertyDescriptor> coll)
        {
            this.getHandlers = new Func<object, object>[this.descriptors.Count];
            this.getHandlerNames = new Dictionary<string, Func<object, object>>();
            for (int i = 0; i < coll.Count; i++)
            {
                string name = coll[i].Name;
                int? parametersCount = null;
                Func<object, object> func = Helper.GetInstanceMethodHandler<Func<object, object>>(null, "get_" + name, BindingFlags.Public | BindingFlags.Instance, this.ElementType, parametersCount, null, true);
                this.getHandlers[i] = func;
                this.getHandlerNames[name] = func;
            }
        }

        private void InitializeSearchDescriptors(IList<PropertyDescriptor> coll)
        {
            this.searchDescriptors.Clear();
            for (int i = 0; i < coll.Count; i++)
            {
                this.searchDescriptors.Add(new DataProxySearchDescriptor(coll[i].Name, (Func<DataProxy, object>) this.getHandlers[i], (Action<DataProxy, object>) this.setHandlers[i]));
            }
        }

        private void InitializeSetValueHandlers(IList<PropertyDescriptor> coll)
        {
            this.setHandlers = new Action<object, object>[this.descriptors.Count];
            for (int i = 0; i < coll.Count; i++)
            {
                int? parametersCount = null;
                this.setHandlers[i] = Helper.GetInstanceMethodHandler<Action<object, object>>(null, "set_" + coll[i].Name, BindingFlags.Public | BindingFlags.Instance, this.ElementType, parametersCount, null, true);
            }
        }

        public bool IsSame(DataAccessor dataAccessor) => 
            (dataAccessor != null) && Equals(this.ElementType, dataAccessor.ElementType);

        public void ResetDescriptors()
        {
            this.descriptors.Clear();
            this.ElementType = null;
        }

        private void SetValue(DataProxy proxy, int i, object value)
        {
            this.setHandlers[i](proxy, value);
        }

        public static ReflectionHelper Helper =>
            helper ??= new ReflectionHelper();

        public IEnumerable<PropertyDescriptor> Descriptors =>
            this.searchDescriptors;

        public string ValueMember { get; private set; }

        public bool HasValueMember { get; private set; }

        public bool HasDisplayMember { get; private set; }

        public string DisplayMember { get; private set; }

        public string DisplayPropertyName { get; private set; }

        public string ValuePropertyName { get; private set; }

        public Type ElementType { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataAccessor.<>c <>9 = new DataAccessor.<>c();
            public static Func<string, PropertyDescriptor> <>9__52_0;
            public static Func<KeyValuePair<string, PropertyDescriptor>, string> <>9__57_0;

            internal string <CreateProxyType>b__57_0(KeyValuePair<string, PropertyDescriptor> pair) => 
                pair.Key;

            internal PropertyDescriptor <GenerateDefaultDescriptors>b__52_0(string x) => 
                null;
        }
    }
}

