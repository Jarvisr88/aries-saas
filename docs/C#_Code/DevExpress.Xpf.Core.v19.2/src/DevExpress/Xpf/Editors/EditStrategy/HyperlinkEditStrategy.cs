namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Services;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows;
    using System.Windows.Input;

    public class HyperlinkEditStrategy : EditStrategyBase
    {
        public HyperlinkEditStrategy(HyperlinkEdit editor) : base(editor)
        {
        }

        public override string CoerceDisplayText(string displayText)
        {
            string str = base.IsInSupportInitialize ? string.Empty : this.GetDisplayText();
            return base.CoerceDisplayText(str);
        }

        protected internal override object ConvertEditValueForFormatDisplayText(object convertedValue)
        {
            if (!string.IsNullOrEmpty(this.Editor.Text))
            {
                return this.Editor.Text;
            }
            if (string.IsNullOrEmpty(this.Editor.DisplayMember))
            {
                return convertedValue;
            }
            HyperlinkPropertyAccessor propertyAccessor = this.Settings.PropertyAccessor;
            return propertyAccessor.GetDisplayValues(propertyAccessor.GetProxy(convertedValue));
        }

        protected override BaseEditingSettingsService CreateTextInputSettingsService() => 
            new HyperlinkEditEditSettingsService(this.Editor);

        public virtual void DisplayMemberChanged(string oldValue, string newValue)
        {
            this.Settings.DisplayMember = newValue;
            this.UpdateDisplayText();
        }

        protected override string FormatAfterException(string formatString, object editValue) => 
            string.Empty;

        protected override string FormatDisplayTextFast(object editValue)
        {
            object[] source = editValue as object[];
            if (source == null)
            {
                return base.FormatDisplayTextFast(editValue);
            }
            StringBuilder seed = new StringBuilder();
            Func<StringBuilder, object, StringBuilder> func = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Func<StringBuilder, object, StringBuilder> local1 = <>c.<>9__19_0;
                func = <>c.<>9__19_0 = delegate (StringBuilder acc, object str) {
                    Func<object, string> evaluator = <>c.<>9__19_1;
                    if (<>c.<>9__19_1 == null)
                    {
                        Func<object, string> local1 = <>c.<>9__19_1;
                        evaluator = <>c.<>9__19_1 = x => x.ToString();
                    }
                    string str2 = str.With<object, string>(evaluator);
                    acc.Append(str2);
                    return acc;
                };
            }
            source.Aggregate<object, StringBuilder>(seed, func);
            return seed.ToString();
        }

        private string FormatNavigateLink(object value)
        {
            if (this.Editor.IsPropertySet(HyperlinkEdit.NavigationUrlProperty))
            {
                return this.Editor.NavigationUrl;
            }
            DataProxy proxy = this.Settings.PropertyAccessor.GetProxy(value);
            object[] navigateValues = this.Settings.PropertyAccessor.GetNavigateValues(proxy);
            if (!string.IsNullOrEmpty(FormatStringConverter.GetDisplayFormat(this.Editor.NavigationUrlFormat)))
            {
                try
                {
                    return string.Format(FormatStringConverter.GetDisplayFormat(this.Editor.NavigationUrlFormat), navigateValues);
                }
                catch (FormatException)
                {
                    return string.Empty;
                }
            }
            StringBuilder seed = new StringBuilder();
            Func<StringBuilder, object, StringBuilder> func = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Func<StringBuilder, object, StringBuilder> local1 = <>c.<>9__11_0;
                func = <>c.<>9__11_0 = delegate (StringBuilder acc, object str) {
                    Func<object, string> evaluator = <>c.<>9__11_1;
                    if (<>c.<>9__11_1 == null)
                    {
                        Func<object, string> local1 = <>c.<>9__11_1;
                        evaluator = <>c.<>9__11_1 = x => x.ToString();
                    }
                    string str2 = str.With<object, string>(evaluator);
                    acc.Append(str2);
                    return acc;
                };
            }
            navigateValues.Aggregate<object, StringBuilder>(seed, func);
            return seed.ToString();
        }

        private HyperlinkEditRequestNavigationEventArgs GenerateNavigateEventArgs(RoutedEvent routedEvent)
        {
            HyperlinkEditRequestNavigationEventArgs args1 = new HyperlinkEditRequestNavigationEventArgs(base.ValueContainer.EditValue, this.Editor.ActualNavigationUrl);
            args1.RoutedEvent = routedEvent;
            return args1;
        }

        protected override object GetInternalToolTipContent() => 
            (this.Editor.ActualNavigationUrl != null) ? new ToolTipContent(this.Editor.ActualNavigationUrl) : null;

        public virtual void HighlightedTextChanged(string newValue)
        {
            this.Settings.HighlightedText = newValue;
            this.EditBox.HighlightedText = newValue;
        }

        public virtual void HighlightedTextCriteriaChanged(HighlightedTextCriteria newValue)
        {
            this.Settings.HighlightedTextCriteria = newValue;
            this.EditBox.HighlightedTextCriteria = newValue;
        }

        public void NavigateUrlChanged(string oldValue, string newValue)
        {
            this.UpdateDisplayText();
        }

        public void NavigateUrlFormatChanged(string oldValue, string newValue)
        {
            this.UpdateDisplayText();
        }

        public virtual void NavigateUrlMemberChanged(string oldValue, string newValue)
        {
            this.Settings.NavigationUrlMember = newValue;
            this.UpdateDisplayText();
        }

        public virtual void PerformNavigate()
        {
            HyperlinkEditRequestNavigationEventArgs e = this.GenerateNavigateEventArgs(HyperlinkEdit.RequestNavigationEvent);
            this.Editor.RaiseEvent(e);
            if (!e.Handled)
            {
                this.Settings.RaiseRequestNavigation(this.Editor, e);
            }
            if (!e.Handled || !e.Cancel)
            {
                this.ProcessCommand();
                if (this.Editor.AllowAutoNavigate)
                {
                    ProcessLauncher.Launch(e.NavigationUrl);
                }
            }
        }

        protected virtual void ProcessCommand()
        {
            this.Editor.Command.TryExecute(this.Editor.CommandParameter);
        }

        protected override void ProcessPreviewKeyDownInternal(KeyEventArgs e)
        {
            base.ProcessPreviewKeyDownInternal(e);
            if ((e.Key == Key.Space) || (e.Key == Key.Return))
            {
                this.PerformNavigate();
                e.Handled = true;
            }
        }

        protected override bool ShowInternalToolTip() => 
            this.Editor.ShowNavigationUrlToolTip;

        public virtual void TextChanged(string oldValue, string newValue)
        {
            this.UpdateDisplayText();
        }

        protected override string TryFormatDisplayTextInternal(string formatString, object editValue)
        {
            object[] args = editValue as object[];
            return ((args == null) ? base.TryFormatDisplayTextInternal(formatString, editValue) : string.Format(CultureInfo.CurrentCulture, formatString, args));
        }

        protected override void UpdateDisplayTextInternal()
        {
            base.UpdateDisplayTextInternal();
            this.Editor.ActualNavigationUrl = this.FormatNavigateLink(base.ValueContainer.EditValue);
        }

        protected override void UpdateEditCoreTextInternal(string displayText)
        {
            this.EditBox.EditValue = displayText;
        }

        private HyperlinkEdit Editor =>
            (HyperlinkEdit) base.Editor;

        private HyperlinkEditSettings Settings =>
            (HyperlinkEditSettings) this.Editor.Settings;

        private EditBoxWrapper EditBox =>
            this.Editor.EditBox;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HyperlinkEditStrategy.<>c <>9 = new HyperlinkEditStrategy.<>c();
            public static Func<object, string> <>9__11_1;
            public static Func<StringBuilder, object, StringBuilder> <>9__11_0;
            public static Func<object, string> <>9__19_1;
            public static Func<StringBuilder, object, StringBuilder> <>9__19_0;

            internal StringBuilder <FormatDisplayTextFast>b__19_0(StringBuilder acc, object str)
            {
                Func<object, string> evaluator = <>9__19_1;
                if (<>9__19_1 == null)
                {
                    Func<object, string> local1 = <>9__19_1;
                    evaluator = <>9__19_1 = x => x.ToString();
                }
                string str2 = str.With<object, string>(evaluator);
                acc.Append(str2);
                return acc;
            }

            internal string <FormatDisplayTextFast>b__19_1(object x) => 
                x.ToString();

            internal StringBuilder <FormatNavigateLink>b__11_0(StringBuilder acc, object str)
            {
                Func<object, string> evaluator = <>9__11_1;
                if (<>9__11_1 == null)
                {
                    Func<object, string> local1 = <>9__11_1;
                    evaluator = <>9__11_1 = x => x.ToString();
                }
                string str2 = str.With<object, string>(evaluator);
                acc.Append(str2);
                return acc;
            }

            internal string <FormatNavigateLink>b__11_1(object x) => 
                x.ToString();
        }
    }
}

