namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="PART_ButtonSave", Type=typeof(Button)), TemplatePart(Name="PART_ButtonRestore", Type=typeof(Button))]
    public class ButtonsPanel : psvControl
    {
        private SerializationHelper serializationHelper;

        static ButtonsPanel()
        {
            new DependencyPropertyRegistrator<ButtonsPanel>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ButtonSave = base.GetTemplateChild("PART_ButtonSave") as Button;
            this.ButtonRestore = base.GetTemplateChild("PART_ButtonRestore") as Button;
            this.ButtonSave.Content = DockingLocalizer.GetString(DockingStringId.ButtonSave);
            this.ButtonRestore.Content = DockingLocalizer.GetString(DockingStringId.ButtonRestore);
            this.ButtonSave.Click += new RoutedEventHandler(this.OnSaveButtonClick);
            this.ButtonRestore.Click += new RoutedEventHandler(this.OnRestoreButtonClick);
            base.Focusable = false;
            this.serializationHelper = new SerializationHelper(base.Container);
        }

        protected virtual void OnRestoreButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.serializationHelper != null)
            {
                this.serializationHelper.RestoreLayout();
            }
        }

        protected virtual void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.serializationHelper != null)
            {
                this.serializationHelper.SaveLayout();
            }
        }

        protected Button ButtonSave { get; private set; }

        protected Button ButtonRestore { get; private set; }
    }
}

