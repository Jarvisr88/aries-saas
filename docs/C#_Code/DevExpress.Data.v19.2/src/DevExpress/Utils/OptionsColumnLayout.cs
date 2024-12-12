namespace DevExpress.Utils
{
    using DevExpress.Data;
    using DevExpress.Utils.Controls;
    using DevExpress.Utils.Design;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    public class OptionsColumnLayout : BaseOptions
    {
        private bool storeLayout = true;
        private bool storeAppearance = false;
        private bool storeAllOptions = false;
        private bool addNewColumns = true;
        private bool removeOldColumns = true;

        public override void Assign(BaseOptions options)
        {
            this.BeginUpdate();
            try
            {
                base.Assign(options);
                OptionsColumnLayout layout = options as OptionsColumnLayout;
                if (layout != null)
                {
                    this.storeAppearance = layout.StoreAppearance;
                    this.storeLayout = layout.StoreLayout;
                    this.storeAllOptions = layout.StoreAllOptions;
                    this.addNewColumns = layout.AddNewColumns;
                    this.removeOldColumns = layout.RemoveOldColumns;
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }

        protected internal bool ShouldSerialize(IComponent owner) => 
            base.ShouldSerialize(owner);

        [Description("Gets or sets whether the columns that exist in a layout when it's restored but that don't exist in the current control should be discarded or added to the control."), Category("Columns"), DXDisplayName(typeof(ResFinder), "DevExpress.Utils.OptionsColumnLayout.RemoveOldColumns"), TypeConverter(typeof(BooleanTypeConverter)), DefaultValue(true), XtraSerializableProperty, NotifyParentProperty(true), AutoFormatDisable]
        public bool RemoveOldColumns
        {
            get => 
                this.removeOldColumns;
            set
            {
                if (this.RemoveOldColumns != value)
                {
                    bool removeOldColumns = this.RemoveOldColumns;
                    this.removeOldColumns = value;
                    this.OnChanged(new BaseOptionChangedEventArgs("RemoveOldColumns", removeOldColumns, this.RemoveOldColumns));
                }
            }
        }

        [Description("Gets or sets whether the columns that exist in the current control but do not exist in a layout when it's restored should be retained."), Category("Columns"), DXDisplayName(typeof(ResFinder), "DevExpress.Utils.OptionsColumnLayout.AddNewColumns"), TypeConverter(typeof(BooleanTypeConverter)), DefaultValue(true), XtraSerializableProperty, NotifyParentProperty(true), AutoFormatDisable]
        public bool AddNewColumns
        {
            get => 
                this.addNewColumns;
            set
            {
                if (this.AddNewColumns != value)
                {
                    bool addNewColumns = this.AddNewColumns;
                    this.addNewColumns = value;
                    this.OnChanged(new BaseOptionChangedEventArgs("AddNewColumns", addNewColumns, this.AddNewColumns));
                }
            }
        }

        [Description("Gets or sets whether the position, width and visibility of the columns and bands are stored when the layout is saved to storage and restored when the layout is restored from storage."), Category("Options"), DXDisplayName(typeof(ResFinder), "DevExpress.Utils.OptionsColumnLayout.StoreLayout"), TypeConverter(typeof(BooleanTypeConverter)), DefaultValue(true), XtraSerializableProperty, NotifyParentProperty(true), AutoFormatDisable]
        public bool StoreLayout
        {
            get => 
                this.storeLayout;
            set
            {
                if (this.StoreLayout != value)
                {
                    bool storeLayout = this.StoreLayout;
                    this.storeLayout = value;
                    this.OnChanged(new BaseOptionChangedEventArgs("StoreLayout", storeLayout, this.StoreLayout));
                }
            }
        }

        [Description("Gets or sets whether the appearance settings of the columns and bands are also stored when the layout is saved to storage and restored when the layout is restored from storage."), Category("Options"), DXDisplayName(typeof(ResFinder), "DevExpress.Utils.OptionsColumnLayout.StoreAppearance"), TypeConverter(typeof(BooleanTypeConverter)), DefaultValue(false), XtraSerializableProperty, NotifyParentProperty(true), AutoFormatDisable]
        public bool StoreAppearance
        {
            get => 
                this.storeAppearance;
            set
            {
                if (this.StoreAppearance != value)
                {
                    bool storeAppearance = this.StoreAppearance;
                    this.storeAppearance = value;
                    this.OnChanged(new BaseOptionChangedEventArgs("StoreAppearance", storeAppearance, this.StoreAppearance));
                }
            }
        }

        [Description("Gets or sets whether all the settings of a control's columns/bands (except for the appearance settings) are stored when the layout is saved to storage and restored when the layout is restored from storage."), Category("Options"), DXDisplayName(typeof(ResFinder), "DevExpress.Utils.OptionsColumnLayout.StoreAllOptions"), TypeConverter(typeof(BooleanTypeConverter)), DefaultValue(false), XtraSerializableProperty, NotifyParentProperty(true), AutoFormatDisable]
        public bool StoreAllOptions
        {
            get => 
                this.storeAllOptions;
            set
            {
                if (this.StoreAllOptions != value)
                {
                    bool storeAllOptions = this.StoreAllOptions;
                    this.storeAllOptions = value;
                    this.OnChanged(new BaseOptionChangedEventArgs("StoreAllOptions", storeAllOptions, this.StoreAllOptions));
                }
            }
        }
    }
}

