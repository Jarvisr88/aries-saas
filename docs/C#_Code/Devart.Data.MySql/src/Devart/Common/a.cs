namespace Devart.Common
{
    using System;
    using System.ComponentModel;

    internal class a : PropertyDescriptor
    {
        private Type a;
        private Type b;
        private bool c;
        private bool d;
        private string e;

        public a(string A_0, Type A_1, Type A_2, bool A_3, Attribute[] A_4) : base(A_0, A_4)
        {
            this.a = A_1;
            this.b = A_2;
            this.c = A_3;
            this.e = base.DisplayName;
            for (int i = 0; i < A_4.Length; i++)
            {
                Attribute attribute = A_4[i];
                if (attribute is Devart.Common.DisplayNameAttribute)
                {
                    this.e = ((Devart.Common.DisplayNameAttribute) attribute).DisplayName;
                    return;
                }
            }
        }

        protected virtual void a(DbConnectionStringBuilder A_0)
        {
        }

        internal void a(bool A_0)
        {
            this.d = A_0;
        }

        public override void a(object A_0)
        {
            DbConnectionStringBuilder builder = A_0 as DbConnectionStringBuilder;
            if (builder != null)
            {
                builder.Remove(this.DisplayName);
                if (this.b())
                {
                    this.a(builder);
                }
            }
        }

        public override void a(object A_0, object A_1)
        {
            DbConnectionStringBuilder builder = A_0 as DbConnectionStringBuilder;
            if (builder != null)
            {
                if (ReferenceEquals(this.PropertyType, typeof(string)) && A_1.Equals(string.Empty))
                {
                    A_1 = null;
                }
                builder[this.DisplayName] = A_1;
                if (this.b())
                {
                    this.a(builder);
                }
            }
        }

        internal bool b() => 
            this.d;

        public override bool b(object A_0)
        {
            DbConnectionStringBuilder builder = A_0 as DbConnectionStringBuilder;
            return ((builder != null) && builder.ShouldSerialize(this.DisplayName));
        }

        public override bool c(object A_0)
        {
            DbConnectionStringBuilder builder = A_0 as DbConnectionStringBuilder;
            return ((builder != null) && builder.ShouldSerialize(this.DisplayName));
        }

        public override object d(object A_0)
        {
            object obj2;
            DbConnectionStringBuilder builder = A_0 as DbConnectionStringBuilder;
            return (((builder == null) || !builder.TryGetValue(this.DisplayName, out obj2)) ? null : obj2);
        }

        public override string get_DisplayName() => 
            this.e;

        public override Type System.ComponentModel.PropertyDescriptor.ComponentType =>
            this.a;

        public override bool System.ComponentModel.PropertyDescriptor.IsReadOnly =>
            this.c;

        public override Type System.ComponentModel.PropertyDescriptor.PropertyType =>
            this.b;
    }
}

