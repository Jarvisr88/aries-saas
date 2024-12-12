namespace DevExpress.Xpf.Printing.Native.Lines
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class PropertyLine : LineBase
    {
        private readonly PropertyDescriptor property;
        private readonly object obj;

        public event EventHandler ValueChanged;

        protected PropertyLine(PropertyDescriptor property, object obj)
        {
            this.property = property;
            this.obj = obj;
        }

        protected virtual void OnValueSet()
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, EventArgs.Empty);
            }
        }

        public override void RefreshContent()
        {
            if ((this.obj is ExportOptionsBase) && (this.property.Converter != null))
            {
                bool flag = this.property.Converter.CanConvertFrom(new RuntimeTypeDescriptorContext(this.property, this.obj), typeof(string));
                this.Content.IsEnabled = flag;
                if (this.Header != null)
                {
                    this.Header.IsEnabled = flag;
                }
            }
        }

        public PropertyDescriptor Property =>
            this.property;

        public object Value
        {
            get => 
                this.property.GetValue(this.obj);
            set
            {
                if (!Equals(value, this.Value))
                {
                    this.property.SetValue(this.obj, value);
                    this.OnValueSet();
                }
            }
        }
    }
}

