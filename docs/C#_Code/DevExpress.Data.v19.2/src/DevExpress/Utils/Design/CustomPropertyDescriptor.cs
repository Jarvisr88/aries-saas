namespace DevExpress.Utils.Design
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class CustomPropertyDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor parentPropertyDescriptor;
        private bool browsable;
        private bool serializable;

        public CustomPropertyDescriptor(PropertyDescriptor parentPropertyDescriptor, bool visible) : this(parentPropertyDescriptor, visible, visible)
        {
        }

        public CustomPropertyDescriptor(PropertyDescriptor parentPropertyDescriptor, bool browsable, bool serializable) : base(parentPropertyDescriptor)
        {
            this.parentPropertyDescriptor = parentPropertyDescriptor;
            this.browsable = browsable;
            this.serializable = serializable;
        }

        public override bool CanResetValue(object component) => 
            this.parentPropertyDescriptor.CanResetValue(component);

        public override object GetValue(object component) => 
            this.parentPropertyDescriptor.GetValue(component);

        public override void ResetValue(object component)
        {
            this.parentPropertyDescriptor.ResetValue(component);
        }

        public override void SetValue(object component, object value)
        {
            this.parentPropertyDescriptor.SetValue(component, value);
        }

        public override bool ShouldSerializeValue(object component) => 
            this.parentPropertyDescriptor.ShouldSerializeValue(component);

        public override Type ComponentType =>
            this.parentPropertyDescriptor.ComponentType;

        public override bool IsReadOnly =>
            this.parentPropertyDescriptor.IsReadOnly;

        public override Type PropertyType =>
            this.parentPropertyDescriptor.PropertyType;

        public override AttributeCollection Attributes
        {
            get
            {
                List<Attribute> list = new List<Attribute>();
                DesignerSerializationVisibilityAttribute attribute = null;
                foreach (Attribute attribute2 in this.parentPropertyDescriptor.Attributes)
                {
                    if (attribute2.GetType() == typeof(DesignerSerializationVisibilityAttribute))
                    {
                        attribute = (DesignerSerializationVisibilityAttribute) attribute2;
                    }
                    if ((attribute2.GetType() != typeof(DesignerSerializationVisibilityAttribute)) && ((attribute2.GetType() != typeof(BrowsableAttribute)) && (attribute2.GetType() != typeof(EditorBrowsableAttribute))))
                    {
                        list.Add(attribute2);
                    }
                }
                DesignerSerializationVisibility visibility = (attribute == null) ? DesignerSerializationVisibility.Visible : attribute.Visibility;
                list.Add(new DesignerSerializationVisibilityAttribute(this.serializable ? visibility : DesignerSerializationVisibility.Hidden));
                list.Add(new BrowsableAttribute(this.browsable));
                list.Add(new EditorBrowsableAttribute(this.browsable ? EditorBrowsableState.Always : EditorBrowsableState.Never));
                Attribute[] array = new Attribute[list.Count];
                list.CopyTo(array);
                return new AttributeCollection(array);
            }
        }
    }
}

