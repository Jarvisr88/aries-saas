namespace DevExpress.Xpf.Printing.PreviewControl.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    internal class PreviewSettingsDialogTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            PreviewSettingsViewModelBase base2 = item as PreviewSettingsViewModelBase;
            if (base2 != null)
            {
                switch (base2.SettingsType)
                {
                    case SettingsType.Export:
                        return this.ExportOptionsDialogTemplate;

                    case SettingsType.Send:
                        return this.SendOptionsDialogTemplate;

                    case SettingsType.Print:
                        return this.PrintOptionsDialogTemplate;

                    case SettingsType.Scale:
                        return this.ScaleDialogTemplate;
                }
            }
            return null;
        }

        public DataTemplate ExportOptionsDialogTemplate { get; set; }

        public DataTemplate SendOptionsDialogTemplate { get; set; }

        public DataTemplate ScaleDialogTemplate { get; set; }

        public DataTemplate PrintOptionsDialogTemplate { get; set; }
    }
}

