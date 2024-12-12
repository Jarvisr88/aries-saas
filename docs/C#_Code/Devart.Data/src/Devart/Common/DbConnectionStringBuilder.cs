namespace Devart.Common
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data.Common;
    using System.Reflection;

    public class DbConnectionStringBuilder : System.Data.Common.DbConnectionStringBuilder, ICustomTypeDescriptor
    {
        protected string initializationCommandInternal;
        protected string runOnceCommandInternal;
        private PropertyDescriptorCollection a;

        private PropertyDescriptorCollection a()
        {
            PropertyDescriptorCollection a = this.a;
            if (a == null)
            {
                Hashtable propertyDescriptors = new Hashtable();
                this.GetProperties(propertyDescriptors);
                a = this.a(propertyDescriptors);
                this.a = a;
            }
            return a;
        }

        private PropertyDescriptorCollection a(Hashtable A_0)
        {
            ICollection keys = this.Keys;
            PropertyDescriptor[] sourceArray = new PropertyDescriptor[keys.Count];
            object[] array = new object[keys.Count];
            keys.CopyTo(array, 0);
            int length = 0;
            for (int i = 0; i < array.Length; i++)
            {
                PropertyDescriptor descriptor = (PropertyDescriptor) A_0[array[i]];
                if (descriptor != null)
                {
                    sourceArray[length++] = descriptor;
                }
            }
            PropertyDescriptor[] destinationArray = new PropertyDescriptor[length];
            Array.Copy(sourceArray, destinationArray, length);
            return new PropertyDescriptorCollection(destinationArray);
        }

        private PropertyDescriptorCollection a(Attribute[] A_0)
        {
            PropertyDescriptorCollection descriptors = this.a();
            if ((A_0 == null) || (A_0.Length == 0))
            {
                return descriptors;
            }
            PropertyDescriptor[] sourceArray = new PropertyDescriptor[descriptors.Count];
            int index = 0;
            foreach (PropertyDescriptor descriptor in descriptors)
            {
                bool flag = true;
                Attribute[] attributeArray = A_0;
                int num2 = 0;
                while (true)
                {
                    if (num2 < attributeArray.Length)
                    {
                        Attribute attribute = attributeArray[num2];
                        Attribute attribute2 = descriptor.Attributes[attribute.GetType()];
                        if (((attribute2 != null) || attribute.IsDefaultAttribute()) && attribute2.Match(attribute))
                        {
                            num2++;
                            continue;
                        }
                        flag = false;
                    }
                    if (flag)
                    {
                        sourceArray[index] = descriptor;
                        index++;
                    }
                    break;
                }
            }
            PropertyDescriptor[] destinationArray = new PropertyDescriptor[index];
            Array.Copy(sourceArray, destinationArray, index);
            return new PropertyDescriptorCollection(destinationArray);
        }

        private void a(string A_0, string A_1)
        {
            if (string.IsNullOrEmpty(A_1))
            {
                this.Remove(A_0);
            }
            else
            {
                base[A_0] = A_1;
            }
        }

        protected internal void ClearPropertyDescriptors()
        {
            this.a = null;
        }

        public virtual bool EquivalentTo(Devart.Common.DbConnectionStringBuilder connectionStringBuilder, bool loginOnly) => 
            this.EquivalentTo(connectionStringBuilder);

        protected void SetValue(string keyword, bool value)
        {
            this.a(keyword, value.ToString(null));
        }

        protected void SetValue(string keyword, int value)
        {
            this.a(keyword, value.ToString((IFormatProvider) null));
        }

        protected void SetValue(string keyword, string value)
        {
            this.a(keyword, value);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties() => 
            this.a();

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes) => 
            this.a(attributes);

        public override object this[string keyword]
        {
            get => 
                base[keyword];
            set
            {
                if ((value != null) && (!(value is string) || (((string) value) != string.Empty)))
                {
                    base[keyword] = value;
                }
                else
                {
                    this.Remove(keyword);
                }
            }
        }

        [DisplayName("Initialization Command"), RefreshProperties(RefreshProperties.All), Category("Provider Behaviour"), y("DbConnectionString_InitializationCommand")]
        public string InitializationCommand
        {
            get => 
                this.initializationCommandInternal;
            set
            {
                this.SetValue("Initialization Command", value);
                this.initializationCommandInternal = value;
            }
        }

        [RefreshProperties(RefreshProperties.All), DisplayName("Run Once Command"), y("DbConnectionString_RunOnceCommand"), Category("Provider Behaviour")]
        public string RunOnceCommand
        {
            get => 
                this.runOnceCommandInternal;
            set
            {
                this.SetValue("Run Once Command", value);
                this.runOnceCommandInternal = value;
            }
        }
    }
}

