namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class HyperlinkEditSettings : BaseEditSettings, IHyperlinkPropertyOwner, ISupportTextHighlighting
    {
        private static readonly object RequestNavigationEventClick = new object();
        public static readonly DependencyProperty NavigationUrlMemberProperty;
        public static readonly DependencyProperty NavigationUrlFormatProperty;
        public static readonly DependencyProperty DisplayMemberProperty;
        private static readonly DependencyPropertyKey HighlightedTextPropertyKey;
        public static readonly DependencyProperty HighlightedTextProperty;
        private static readonly DependencyPropertyKey HighlightedTextCriteriaPropertyKey;
        public static readonly DependencyProperty HighlightedTextCriteriaProperty;
        public static readonly DependencyProperty AllowAutoNavigateProperty;
        private HyperlinkPropertyAccessor accessor;

        public event HyperlinkEditRequestNavigationEventHandler RequestNavigation
        {
            add
            {
                base.AddHandler(RequestNavigationEventClick, value);
            }
            remove
            {
                base.RemoveHandler(RequestNavigationEventClick, value);
            }
        }

        static HyperlinkEditSettings()
        {
            Type ownerType = typeof(HyperlinkEditSettings);
            NavigationUrlMemberProperty = DependencyPropertyManager.Register("NavigationUrlMember", typeof(string), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((HyperlinkEditSettings) o).NavigateUrlMemberChanged((string) args.OldValue, (string) args.NewValue)));
            NavigationUrlFormatProperty = DependencyPropertyManager.Register("NavigationUrlFormat", typeof(string), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));
            DisplayMemberProperty = DependencyProperty.Register("DisplayMember", typeof(string), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, (o, args) => ((HyperlinkEditSettings) o).DisplayMemberChanged((string) args.OldValue, (string) args.NewValue)));
            AllowAutoNavigateProperty = DependencyPropertyManager.Register("AllowAutoNavigate", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            HighlightedTextCriteriaPropertyKey = DependencyPropertyManager.RegisterReadOnly("HighlightedTextCriteria", typeof(DevExpress.Xpf.Editors.HighlightedTextCriteria), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.HighlightedTextCriteria.StartsWith));
            HighlightedTextCriteriaProperty = HighlightedTextCriteriaPropertyKey.DependencyProperty;
            HighlightedTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("HighlightedText", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty));
            HighlightedTextProperty = HighlightedTextPropertyKey.DependencyProperty;
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            HyperlinkEdit te = edit as HyperlinkEdit;
            if (te != null)
            {
                base.SetValueFromSettings(AllowAutoNavigateProperty, () => te.AllowAutoNavigate = this.AllowAutoNavigate);
                base.SetValueFromSettings(DisplayMemberProperty, () => te.DisplayMember = this.DisplayMember);
                base.SetValueFromSettings(NavigationUrlFormatProperty, () => te.NavigationUrlFormat = this.NavigationUrlFormat);
                base.SetValueFromSettings(NavigationUrlMemberProperty, () => te.NavigationUrlMember = this.NavigationUrlMember);
                base.SetValueFromSettings(HighlightedTextCriteriaProperty, () => te.HighlightedTextCriteria = this.HighlightedTextCriteria);
                base.SetValueFromSettings(HighlightedTextProperty, () => te.HighlightedText = this.HighlightedText);
            }
        }

        protected virtual HyperlinkPropertyAccessor CreatePropertyAccessor() => 
            new HyperlinkPropertyAccessor(this);

        void ISupportTextHighlighting.UpdateHighlightedText(string highlightedText, DevExpress.Xpf.Editors.HighlightedTextCriteria criteria)
        {
            this.HighlightedText = highlightedText;
            this.HighlightedTextCriteria = criteria;
        }

        protected virtual void DisplayMemberChanged(string oldValue, string newValue)
        {
            this.accessor = null;
        }

        protected virtual void NavigateUrlMemberChanged(string oldValue, string newValue)
        {
            this.accessor = null;
        }

        protected internal void RaiseRequestNavigation(object sender, HyperlinkEditRequestNavigationEventArgs args)
        {
            Delegate delegate2;
            if (base.Events.TryGetValue(RequestNavigationEventClick, out delegate2))
            {
                ((HyperlinkEditRequestNavigationEventHandler) delegate2)(sender, args);
            }
        }

        protected internal HyperlinkPropertyAccessor PropertyAccessor
        {
            get
            {
                HyperlinkPropertyAccessor accessor = this.accessor;
                if (this.accessor == null)
                {
                    HyperlinkPropertyAccessor local1 = this.accessor;
                    accessor = this.accessor = this.CreatePropertyAccessor();
                }
                return accessor;
            }
        }

        public bool AllowAutoNavigate
        {
            get => 
                (bool) base.GetValue(AllowAutoNavigateProperty);
            set => 
                base.SetValue(AllowAutoNavigateProperty, value);
        }

        public string NavigationUrlMember
        {
            get => 
                (string) base.GetValue(NavigationUrlMemberProperty);
            set => 
                base.SetValue(NavigationUrlMemberProperty, value);
        }

        public string NavigationUrlFormat
        {
            get => 
                (string) base.GetValue(NavigationUrlFormatProperty);
            set => 
                base.SetValue(NavigationUrlFormatProperty, value);
        }

        public string DisplayMember
        {
            get => 
                (string) base.GetValue(DisplayMemberProperty);
            set => 
                base.SetValue(DisplayMemberProperty, value);
        }

        public string HighlightedText
        {
            get => 
                (string) base.GetValue(HighlightedTextProperty);
            internal set => 
                base.SetValue(HighlightedTextPropertyKey, value);
        }

        public DevExpress.Xpf.Editors.HighlightedTextCriteria HighlightedTextCriteria
        {
            get => 
                (DevExpress.Xpf.Editors.HighlightedTextCriteria) base.GetValue(HighlightedTextCriteriaProperty);
            internal set => 
                base.SetValue(HighlightedTextCriteriaPropertyKey, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HyperlinkEditSettings.<>c <>9 = new HyperlinkEditSettings.<>c();

            internal void <.cctor>b__9_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((HyperlinkEditSettings) o).NavigateUrlMemberChanged((string) args.OldValue, (string) args.NewValue);
            }

            internal void <.cctor>b__9_1(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((HyperlinkEditSettings) o).DisplayMemberChanged((string) args.OldValue, (string) args.NewValue);
            }
        }
    }
}

