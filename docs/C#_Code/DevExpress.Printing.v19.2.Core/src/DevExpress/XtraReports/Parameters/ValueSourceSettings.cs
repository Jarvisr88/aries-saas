namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ValueSourceSettings : IObject
    {
        protected ValueSourceSettings()
        {
        }

        protected internal virtual void SyncParameterType(Type type)
        {
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DevExpress.XtraReports.Parameters.Parameter Parameter { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(-1)]
        public string ObjectType
        {
            get
            {
                Type type = base.GetType();
                return $"{type.FullName}, {type.Assembly.GetName().Name}";
            }
        }
    }
}

