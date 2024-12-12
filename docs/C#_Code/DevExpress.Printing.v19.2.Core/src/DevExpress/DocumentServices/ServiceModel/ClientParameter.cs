namespace DevExpress.DocumentServices.ServiceModel
{
    using DevExpress.Utils;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class ClientParameter : IClientParameter
    {
        internal event EventHandler ValueChanged;

        internal ClientParameter(Parameter parameter) : this(parameter, string.Empty)
        {
        }

        internal ClientParameter(Parameter parameter, string path)
        {
            Guard.ArgumentNotNull(parameter, "parameter");
            this.<OriginalParameter>k__BackingField = parameter;
            this.<Path>k__BackingField = path;
        }

        public string Description
        {
            get => 
                this.OriginalParameter.Description;
            set => 
                this.OriginalParameter.Description = value;
        }

        public string Name =>
            this.OriginalParameter.Name;

        public System.Type Type =>
            this.OriginalParameter.Type;

        public object Value
        {
            get => 
                this.OriginalParameter.Value;
            set
            {
                if ((value == null) && this.Type.IsValueType)
                {
                    throw new ArgumentException("Cannot set null value for a value type parameter.");
                }
                if ((value != null) && (!this.MultiValue && !this.Type.IsInstanceOfType(value)))
                {
                    throw new ArgumentException("Cannot set value of unrelated type.");
                }
                if ((value != null) && (this.MultiValue && !(value is IEnumerable)))
                {
                    throw new ArgumentException("Value must be an enumerable.");
                }
                this.OriginalParameter.Value = value;
                if (this.ValueChanged == null)
                {
                    EventHandler valueChanged = this.ValueChanged;
                }
                else
                {
                    this.ValueChanged(this, EventArgs.Empty);
                }
            }
        }

        public bool MultiValue =>
            this.OriginalParameter.MultiValue;

        public bool AllowNull =>
            this.OriginalParameter.AllowNull;

        public bool Visible
        {
            get => 
                this.OriginalParameter.Visible;
            set => 
                this.OriginalParameter.Visible = value;
        }

        internal Parameter OriginalParameter { get; }

        internal string Path { get; }

        [DefaultValue(false)]
        internal bool IsFilteredLookUpSettings { get; set; }
    }
}

