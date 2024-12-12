namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Input;

    public class CheckEditAutomationPeer : BaseEditAutomationPeer, IToggleProvider
    {
        private const char AccessSymbol = '_';

        public CheckEditAutomationPeer(CheckEdit element) : base(element)
        {
        }

        private static ToggleState ConvertToToggleState(bool? isChecked) => 
            (isChecked == null) ? ToggleState.Indeterminate : (!isChecked.Value ? ToggleState.Off : ToggleState.On);

        private static int FindAccessKeyMarker(string text)
        {
            int length = text.Length;
            int startIndex = 0;
            string str = '_'.ToString();
            while (startIndex < length)
            {
                int num3 = text.IndexOf(str, startIndex, StringComparison.Ordinal);
                if (num3 == -1)
                {
                    return -1;
                }
                if (((num3 + 1) < length) && (text[num3 + 1] != '_'))
                {
                    return num3;
                }
                startIndex = num3 + 2;
            }
            return -1;
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.CheckBox;

        protected override string GetNameCore()
        {
            string nameCore = base.GetNameCore();
            if (!string.IsNullOrEmpty(nameCore))
            {
                if (this.Editor.Content is string)
                {
                    nameCore = RemoveAccessSymbol(nameCore);
                }
                return nameCore;
            }
            RoutedUICommand command = this.Editor.Command as RoutedUICommand;
            if ((command != null) && !string.IsNullOrEmpty(command.Text))
            {
                nameCore = command.Text;
            }
            return nameCore;
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.Toggle) ? base.GetPattern(patternInterface) : this;

        internal void RaiseToggleStatePropertyChangedEvent(bool? oldValue, bool? newValue)
        {
            bool? nullable = oldValue;
            bool? nullable2 = newValue;
            if ((nullable.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? ((nullable != null) != (nullable2 != null)) : true)
            {
                base.RaisePropertyChangedEvent(TogglePatternIdentifiers.ToggleStateProperty, ConvertToToggleState(oldValue), ConvertToToggleState(newValue));
            }
        }

        private static string RemoveAccessSymbol(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                string newValue = '_'.ToString();
                int startIndex = FindAccessKeyMarker(text);
                if ((startIndex >= 0) && (startIndex < (text.Length - 1)))
                {
                    text = text.Remove(startIndex, 1);
                }
                text = text.Replace(newValue + newValue, newValue);
            }
            return text;
        }

        void IToggleProvider.Toggle()
        {
            if (!base.IsEnabled())
            {
                throw new ElementNotEnabledException();
            }
            this.Editor.Toggle();
        }

        protected CheckEdit Editor =>
            base.Editor as CheckEdit;

        ToggleState IToggleProvider.ToggleState =>
            ConvertToToggleState(this.Editor.IsChecked);
    }
}

