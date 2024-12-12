namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Threading;

    public class ComboBoxEditSettings : LookUpEditSettingsBase, IItemsProviderOwner
    {
        private ListItemCollection items;
        private ObservableCollection<System.Windows.Controls.GroupStyle> groupStyle;

        static ComboBoxEditSettings()
        {
            Type type = typeof(ComboBoxEditSettings);
            LookUpEditSettingsBase.IncrementalFilteringProperty.OverrideMetadata(typeof(ComboBoxEditSettings), new FrameworkPropertyMetadata(null, new PropertyChangedCallback(BaseEditSettings.OnSettingsPropertyChanged)));
            PopupBaseEditSettings.PopupMaxHeightProperty.OverrideMetadata(typeof(ComboBoxEditSettings), new FrameworkPropertyMetadata(SystemParameters.PrimaryScreenHeight / 3.0));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            ComboBoxEdit edit2 = edit as ComboBoxEdit;
            if ((edit2 != null) && !ReferenceEquals(edit2.Settings, this))
            {
                edit2.Items.Assign(this.Items);
            }
        }

        public bool GetApplyImageTemplateToSelectedItem() => 
            ((ComboBoxEdit) base.Editor).ApplyImageTemplateToSelectedItem;

        [Category("Data"), Description("Provides access to the collection of items when the editor is not bound to a data source.")]
        public ListItemCollection Items
        {
            get
            {
                ListItemCollection items = this.items;
                if (this.items == null)
                {
                    ListItemCollection local1 = this.items;
                    items = this.items = new ListItemCollection(this);
                }
                return items;
            }
        }

        ListItemCollection IItemsProviderOwner.Items =>
            this.Items;

        public ObservableCollection<System.Windows.Controls.GroupStyle> GroupStyle
        {
            get
            {
                ObservableCollection<System.Windows.Controls.GroupStyle> groupStyle = this.groupStyle;
                if (this.groupStyle == null)
                {
                    ObservableCollection<System.Windows.Controls.GroupStyle> local1 = this.groupStyle;
                    groupStyle = this.groupStyle = new ObservableCollection<System.Windows.Controls.GroupStyle>();
                }
                return groupStyle;
            }
        }

        Dispatcher IItemsProviderOwner.Dispatcher =>
            base.Dispatcher;
    }
}

