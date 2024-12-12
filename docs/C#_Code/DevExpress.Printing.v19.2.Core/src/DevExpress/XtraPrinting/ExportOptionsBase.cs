namespace DevExpress.XtraPrinting
{
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.ComponentModel;

    [Serializable, TypeConverter(typeof(LocalizableExpandableObjectTypeConverter)), SerializationContext(typeof(PrintingSystemSerializationContext))]
    public abstract class ExportOptionsBase : ICloneable
    {
        public ExportOptionsBase()
        {
        }

        protected ExportOptionsBase(ExportOptionsBase source)
        {
            this.Assign(source);
        }

        public abstract void Assign(ExportOptionsBase source);
        protected internal abstract ExportOptionsBase CloneOptions();
        protected internal virtual void Correct()
        {
        }

        protected internal virtual bool GetShowOptionsBeforeExport(bool defaultValue) => 
            defaultValue;

        protected internal abstract bool ShouldSerialize();
        object ICloneable.Clone() => 
            this.CloneOptions();

        protected internal virtual bool UseActionAfterExportAndSaveModeValue =>
            true;

        internal abstract DevExpress.XtraPrinting.ExportModeBase ExportModeBase { get; }
    }
}

