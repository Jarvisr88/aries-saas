namespace DevExpress.Xpf.Office.UI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class FontComboBoxEditItem
    {
        public FontComboBoxEditItem(string editValue, string displayText, System.Windows.Media.FontFamily fontFamily)
        {
            this.EditValue = editValue;
            this.DisplayText = displayText;
            this.FontFamily = fontFamily;
        }

        public override string ToString() => 
            this.DisplayText;

        public string DisplayText { get; set; }

        public string EditValue { get; set; }

        public System.Windows.Media.FontFamily FontFamily { get; set; }
    }
}

