namespace DevExpress.Xpf.Editors.Native
{
    using System;
    using System.ComponentModel;
    using System.Dynamic;
    using System.Runtime.CompilerServices;

    public class DynamicObjectPropertyDescriptor : PropertyDescriptor
    {
        public DynamicObjectPropertyDescriptor(string path) : base(path, null)
        {
            this.GetMemberBinder = new DynamicObjectMemberBinder(path);
        }

        public override bool CanResetValue(object component) => 
            false;

        public override object GetValue(object component)
        {
            object obj3;
            return (!(component as DynamicObject).TryGetMember(this.GetMemberBinder, out obj3) ? null : obj3);
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
        }

        public override bool ShouldSerializeValue(object component) => 
            false;

        private System.Dynamic.GetMemberBinder GetMemberBinder { get; set; }

        public override Type ComponentType =>
            typeof(object);

        public override bool IsReadOnly =>
            false;

        public override Type PropertyType =>
            typeof(object);
    }
}

