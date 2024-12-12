namespace DevExpress.Xpf.Office.UI
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Windows;

    [DXToolboxBrowsable(false)]
    public class FontComboBoxEdit : ComboBoxEdit
    {
        static FontComboBoxEdit()
        {
            FontComboBoxEditSettings.RegisterEditor();
        }

        public FontComboBoxEdit()
        {
            base.DefaultStyleKey = typeof(FontComboBoxEdit);
            base.ProcessNewValue += new ProcessNewValueEventHandler(this.OnProcessNewValue);
        }

        protected internal override BaseEditSettings CreateEditorSettings() => 
            new FontComboBoxEditSettings();

        protected internal override string GetDisplayText(object editValue, bool applyFormatting)
        {
            string displayText = base.GetDisplayText(editValue, applyFormatting);
            if (!string.IsNullOrEmpty(displayText))
            {
                return displayText;
            }
            string str2 = editValue as string;
            return (string.IsNullOrEmpty(str2) ? string.Empty : str2);
        }

        private void OnProcessNewValue(DependencyObject sender, ProcessNewValueEventArgs e)
        {
            base.EditValue = e.DisplayText;
        }
    }
}

