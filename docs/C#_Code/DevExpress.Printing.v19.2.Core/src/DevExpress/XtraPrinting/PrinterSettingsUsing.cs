namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(LocalizableExpandableObjectTypeConverter)), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PrinterSettingsUsing")]
    public class PrinterSettingsUsing
    {
        private bool usePaperKind;
        private bool useLandscape;

        public PrinterSettingsUsing()
        {
        }

        public PrinterSettingsUsing(PrinterSettingsUsing source) : this(source.usePaperKind, source.useLandscape)
        {
        }

        public PrinterSettingsUsing(bool usePaperKind, bool useLandscape)
        {
            this.usePaperKind = usePaperKind;
            this.useLandscape = useLandscape;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DXHelpExclude(true)]
        public PrinterSettingsUsing(bool useMargins, bool usePaperKind, bool useLandscape)
        {
            this.usePaperKind = usePaperKind;
            this.useLandscape = useLandscape;
        }

        internal bool ShouldSerialize() => 
            this.useLandscape || this.usePaperKind;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DXHelpExclude(true)]
        public virtual bool UseMargins
        {
            get => 
                false;
            set
            {
            }
        }

        [Description("Specifies whether or not the Paper Kind setting of the system's default printer is used when printing a document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PrinterSettingsUsing.UsePaperKind"), TypeConverter(typeof(BooleanTypeConverter)), NotifyParentProperty(true), DefaultValue(false), RefreshProperties(RefreshProperties.All)]
        public virtual bool UsePaperKind
        {
            get => 
                this.usePaperKind;
            set => 
                this.usePaperKind = value;
        }

        [Description("Specifies whether or not the Landscape setting of the system's default printer is used when printing a document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PrinterSettingsUsing.UseLandscape"), TypeConverter(typeof(BooleanTypeConverter)), NotifyParentProperty(true), DefaultValue(false), RefreshProperties(RefreshProperties.All)]
        public virtual bool UseLandscape
        {
            get => 
                this.useLandscape;
            set => 
                this.useLandscape = value;
        }

        [Browsable(false)]
        public bool AllSettingsUsed =>
            this.UseMargins && (this.UsePaperKind && this.UseLandscape);

        [Browsable(false)]
        public bool AnySettingUsed =>
            this.UseMargins || (this.UsePaperKind || this.UseLandscape);
    }
}

