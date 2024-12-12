namespace DevExpress.XtraPrinting
{
    using DevExpress.Printing;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(LocalizableExpandableObjectTypeConverter)), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfPermissionsOptions")]
    public class PdfPermissionsOptions : ICloneable
    {
        private DevExpress.XtraPrinting.PrintingPermissions printingPermissions;
        private DevExpress.XtraPrinting.ChangingPermissions changingPermissions;
        private bool enableCopying;
        private bool enableScreenReaders = true;

        public void Assign(PdfPermissionsOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }
            this.printingPermissions = options.printingPermissions;
            this.changingPermissions = options.changingPermissions;
            this.enableCopying = options.enableCopying;
            this.enableScreenReaders = options.enableScreenReaders;
        }

        public object Clone()
        {
            PdfPermissionsOptions options = new PdfPermissionsOptions();
            options.Assign(this);
            return options;
        }

        public override bool Equals(object obj)
        {
            PdfPermissionsOptions options = obj as PdfPermissionsOptions;
            return ((options != null) ? ((this.printingPermissions == options.printingPermissions) && ((this.changingPermissions == options.changingPermissions) && ((this.enableCopying == options.enableCopying) && (this.enableScreenReaders == options.enableScreenReaders)))) : false);
        }

        public override int GetHashCode() => 
            base.GetHashCode();

        internal bool ShouldSerialize() => 
            (this.printingPermissions != DevExpress.XtraPrinting.PrintingPermissions.None) || ((this.changingPermissions != DevExpress.XtraPrinting.ChangingPermissions.None) || (this.enableCopying || !this.enableScreenReaders));

        private bool ShouldSerializeEnableCoping() => 
            false;

        [Description("Specifies the permissions for printing the exported PDF document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfPermissionsOptions.PrintingPermissions"), DefaultValue(0), Localizable(true), XtraSerializableProperty]
        public DevExpress.XtraPrinting.PrintingPermissions PrintingPermissions
        {
            get => 
                this.printingPermissions;
            set => 
                this.printingPermissions = value;
        }

        [Description("Specifies the permissions for changing the exported PDF document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfPermissionsOptions.ChangingPermissions"), DefaultValue(0), Localizable(true), XtraSerializableProperty]
        public DevExpress.XtraPrinting.ChangingPermissions ChangingPermissions
        {
            get => 
                this.changingPermissions;
            set => 
                this.changingPermissions = value;
        }

        [Description("Specifies the permissions for copying the content of the exported PDF document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfPermissionsOptions.EnableCopying"), DefaultValue(false), Localizable(true), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public bool EnableCopying
        {
            get => 
                this.enableCopying;
            set => 
                this.enableCopying = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty, Obsolete("Use the EnableCopying property instead")]
        public bool EnableCoping
        {
            get => 
                this.EnableCopying;
            set => 
                this.EnableCopying = value;
        }

        [Description("Specifies the permissions for screen readers access to the exported PDF document."), DXDisplayName(typeof(ResFinder), "DevExpress.XtraPrinting.PdfPermissionsOptions.EnableScreenReaders"), DefaultValue(true), Localizable(true), TypeConverter(typeof(BooleanTypeConverter)), XtraSerializableProperty]
        public bool EnableScreenReaders
        {
            get => 
                this.enableScreenReaders;
            set => 
                this.enableScreenReaders = value;
        }
    }
}

