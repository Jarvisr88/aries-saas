namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ReferenceSerializationHelper
    {
        private readonly List<object> serializedObjects;
        private readonly List<object> referencedObjects;
        private readonly IDictionary<object, string> objectReferences;
        private int refID;

        public event ShouldSerializeEventHandler ShouldSerializeObject;

        public ReferenceSerializationHelper() : this(new Dictionary<object, string>())
        {
        }

        public ReferenceSerializationHelper(IDictionary<object, string> objectReferences)
        {
            this.serializedObjects = new List<object>();
            this.referencedObjects = new List<object>();
            Guard.ArgumentNotNull(objectReferences, "objectReferences");
            this.objectReferences = objectReferences;
        }

        public string GetReference(object obj)
        {
            string str = string.Empty;
            if ((obj != null) && !this.objectReferences.TryGetValue(obj, out str))
            {
                str = this.refID.ToString();
                this.objectReferences.Add(obj, str);
                this.refID++;
            }
            return str;
        }

        public bool IsSerializedObject(object obj) => 
            this.serializedObjects.Contains(obj);

        public void OnPropertySerialize(object component, PropertyDescriptor property)
        {
            if ((component != null) && (property is ReferenceLinkPropertyDescriptor))
            {
                object item = ((ReferenceLinkPropertyDescriptor) property).RealProperty.GetValue(component);
                if ((item != null) && !this.referencedObjects.Contains(item))
                {
                    this.referencedObjects.Add(item);
                }
            }
        }

        private bool OnShouldSerializeObject(object obj) => 
            (this.ShouldSerializeObject != null) ? this.ShouldSerializeObject(this, new ObjectEventArgs(obj)) : true;

        public PropertyDescriptorCollection ProcessProperties(object obj, PropertyDescriptorCollection properties)
        {
            this.serializedObjects.Add(obj);
            List<PropertyDescriptor> list = new List<PropertyDescriptor>(properties.Count);
            foreach (PropertyDescriptor descriptor in properties)
            {
                XtraSerializableProperty property = descriptor.Attributes[typeof(XtraSerializableProperty)] as XtraSerializableProperty;
                if ((property == null) || (property.Visibility != XtraSerializationVisibility.Reference))
                {
                    list.Add(descriptor);
                    continue;
                }
                object obj2 = descriptor.GetValue(obj);
                if ((obj2 != null) && this.OnShouldSerializeObject(obj2))
                {
                    list.Add(new ReferenceLinkPropertyDescriptor(descriptor, "#Ref-" + this.GetReference(obj2)));
                }
            }
            list.Add(new ReferencePropertyDescriptor(this.GetReference(obj)));
            return new PropertyDescriptorCollection(list.ToArray());
        }

        public List<object> ReferencedObjects =>
            this.referencedObjects;

        public List<object> SerializedObjects =>
            this.serializedObjects;

        private class ReferenceLinkPropertyDescriptor : PropertyDescriptor
        {
            private static XtraSerializableProperty visibleAttribute = new XtraSerializableProperty(XtraSerializationVisibility.Visible);
            private string referenceLink;
            private PropertyDescriptor realProperty;

            public ReferenceLinkPropertyDescriptor(PropertyDescriptor realProperty, string referenceLink) : base(realProperty.Name, attributeArray1)
            {
                Attribute[] attributeArray1 = new Attribute[] { visibleAttribute };
                this.realProperty = realProperty;
                this.referenceLink = referenceLink;
            }

            public override bool CanResetValue(object component) => 
                true;

            public override object GetValue(object component) => 
                this.referenceLink;

            public override void ResetValue(object component)
            {
                this.referenceLink = null;
            }

            public override void SetValue(object component, object value)
            {
                this.referenceLink = value as string;
            }

            public override bool ShouldSerializeValue(object component) => 
                this.realProperty.ShouldSerializeValue(component);

            public PropertyDescriptor RealProperty =>
                this.realProperty;

            public override Type ComponentType =>
                null;

            public override bool IsReadOnly =>
                false;

            public override Type PropertyType =>
                this.referenceLink.GetType();
        }

        private class ReferencePropertyDescriptor : PropertyDescriptor
        {
            private static XtraSerializableProperty visibleAttribute = new XtraSerializableProperty(XtraSerializationVisibility.Visible, -100);
            private string reference;

            public ReferencePropertyDescriptor(string reference) : base("Ref", attributeArray1)
            {
                Attribute[] attributeArray1 = new Attribute[] { visibleAttribute };
                this.reference = reference;
            }

            public override bool CanResetValue(object component) => 
                true;

            public override object GetValue(object component) => 
                this.reference;

            public override void ResetValue(object component)
            {
                this.reference = null;
            }

            public override void SetValue(object component, object value)
            {
                this.reference = value as string;
            }

            public override bool ShouldSerializeValue(object component) => 
                true;

            public override Type ComponentType =>
                null;

            public override bool IsReadOnly =>
                false;

            public override Type PropertyType =>
                typeof(string);
        }
    }
}

