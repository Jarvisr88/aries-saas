namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class DataProxySearchDescriptor : PropertyDescriptor
    {
        private readonly string name;

        protected DataProxySearchDescriptor(MemberDescriptor descr) : base(descr)
        {
        }

        protected DataProxySearchDescriptor(MemberDescriptor descr, Attribute[] attrs) : base(descr, attrs)
        {
        }

        protected DataProxySearchDescriptor(string name, Attribute[] attrs) : base(name, attrs)
        {
        }

        public DataProxySearchDescriptor(string name, Func<DataProxy, object> getFunc, Action<DataProxy, object> setFunc) : this(string.IsNullOrEmpty(name) ? "Column" : name, null)
        {
            this.GetFunc = getFunc;
            this.SetFunc = setFunc;
            this.name = name;
        }

        public override bool CanResetValue(object component) => 
            false;

        public override object GetValue(object component) => 
            this.GetFunc((DataProxy) component);

        public override void ResetValue(object component)
        {
            this.SetValue(component, null);
        }

        public override void SetValue(object component, object value)
        {
            if (this.SetFunc != null)
            {
                this.SetFunc((DataProxy) component, value);
            }
        }

        public override bool ShouldSerializeValue(object component) => 
            false;

        private Func<DataProxy, object> GetFunc { get; set; }

        private Action<DataProxy, object> SetFunc { get; set; }

        public override Type ComponentType =>
            typeof(DataProxy);

        public override bool IsReadOnly =>
            false;

        public override Type PropertyType =>
            typeof(object);

        public override string Name =>
            this.name;
    }
}

