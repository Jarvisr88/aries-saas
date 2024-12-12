namespace DevExpress.Utils
{
    using DevExpress.Utils.Controls;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    public class OptionsLayoutGrid : OptionsLayoutBase
    {
        private bool storeAppearance = false;
        private bool storeVisualOptions = true;
        private bool storeAllOptions = false;
        private bool storeDataSettings = true;
        private bool storeFormatRules = false;
        private OptionsColumnLayout columns;

        public OptionsLayoutGrid()
        {
            this.columns = this.CreateOptionsColumn();
        }

        public override void Assign(BaseOptions options)
        {
            this.BeginUpdate();
            try
            {
                base.Assign(options);
                OptionsLayoutGrid grid = options as OptionsLayoutGrid;
                if (grid != null)
                {
                    this.storeFormatRules = grid.StoreFormatRules;
                    this.storeAppearance = grid.StoreAppearance;
                    this.storeVisualOptions = grid.StoreVisualOptions;
                    this.storeAllOptions = grid.StoreAllOptions;
                    this.storeDataSettings = grid.StoreDataSettings;
                    this.columns.Assign(grid.Columns);
                }
            }
            finally
            {
                this.EndUpdate();
            }
        }

        protected virtual OptionsColumnLayout CreateOptionsColumn() => 
            new OptionsColumnLayout();

        public override void Reset()
        {
            this.BeginUpdate();
            try
            {
                base.Reset();
                this.Columns.Reset();
            }
            finally
            {
                this.EndUpdate();
            }
        }

        private bool ShouldSerializeColumns() => 
            this.Columns.ShouldSerialize(null);

        [Description("Gets or sets whether the control's appearance settings are also stored when the layout is saved to storage and restored when the layout is restored from storage."), Category("Options"), DefaultValue(false), XtraSerializableProperty, NotifyParentProperty(true), AutoFormatDisable]
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

        [Description("Gets or sets whether MS Excel-style conditional formatting rules are stored when the layout is saved to storage and restored when the layout is restored from storage."), Category("Options"), DefaultValue(false), XtraSerializableProperty, NotifyParentProperty(true), AutoFormatDisable]
        public bool StoreFormatRules
        {
            get => 
                this.storeFormatRules;
            set
            {
                if (this.StoreFormatRules != value)
                {
                    bool storeFormatRules = this.StoreFormatRules;
                    this.storeFormatRules = value;
                    this.OnChanged(new BaseOptionChangedEventArgs("StoreFormatRules", storeFormatRules, this.StoreFormatRules));
                }
            }
        }

        [Description("Gets or sets whether the control's visual options are stored when the layout is saved to storage and restored when the layout is restored from storage."), Category("Options"), DefaultValue(true), XtraSerializableProperty, NotifyParentProperty(true), AutoFormatDisable]
        public bool StoreVisualOptions
        {
            get => 
                this.storeVisualOptions;
            set
            {
                if (this.StoreVisualOptions != value)
                {
                    bool storeVisualOptions = this.StoreVisualOptions;
                    this.storeVisualOptions = value;
                    this.OnChanged(new BaseOptionChangedEventArgs("StoreVisualOptions", storeVisualOptions, this.StoreVisualOptions));
                }
            }
        }

        [Description("Gets or sets whether all the control's settings (except for the appearance settings and format rules) are stored when the layout is saved to storage and restored when the layout is restored from storage."), Category("Options"), DefaultValue(false), XtraSerializableProperty, NotifyParentProperty(true), AutoFormatDisable]
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

        [Description("Gets or sets whether the control's grouping, sorting, filtering settings and summaries are stored when the layout is saved to storage and restored when the layout is restored from storage."), Category("Options"), DefaultValue(true), XtraSerializableProperty, NotifyParentProperty(true), AutoFormatDisable]
        public bool StoreDataSettings
        {
            get => 
                this.storeDataSettings;
            set
            {
                if (this.StoreDataSettings != value)
                {
                    bool storeDataSettings = this.StoreDataSettings;
                    this.storeDataSettings = value;
                    this.OnChanged(new BaseOptionChangedEventArgs("StoreDataSettings", storeDataSettings, this.StoreDataSettings));
                }
            }
        }

        [Description("Contains options that specify how the columns' and bands' settings are stored to and restored from storage (a stream, xml file or sysytem registry)."), Category("Columns"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), XtraSerializableProperty(XtraSerializationVisibility.Content, XtraSerializationFlags.DefaultValue), NotifyParentProperty(true), AutoFormatDisable]
        public OptionsColumnLayout Columns =>
            this.columns;
    }
}

