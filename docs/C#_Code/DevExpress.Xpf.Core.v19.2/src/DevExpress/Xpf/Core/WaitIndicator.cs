namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public class WaitIndicator : ContentControl
    {
        public static readonly DependencyProperty DeferedVisibilityProperty;
        public static readonly DependencyProperty ActualContentProperty;
        public static readonly DependencyProperty ShowShadowProperty;
        internal static readonly DependencyPropertyKey ActualContentPropertyKey;
        public static readonly DependencyProperty ContentPaddingProperty;

        static WaitIndicator()
        {
            DesiredFrameRate = (int?) Timeline.DesiredFrameRateProperty.DefaultMetadata.DefaultValue;
            Type ownerType = typeof(WaitIndicator);
            DeferedVisibilityProperty = DependencyProperty.Register("DeferedVisibility", typeof(bool), ownerType, new PropertyMetadata(false, new PropertyChangedCallback(WaitIndicator.OnDeferedVisibilityPropertyChanged)));
            ShowShadowProperty = DependencyProperty.Register("ShowShadow", typeof(bool), ownerType, new PropertyMetadata(true));
            Thickness defaultValue = new Thickness();
            ContentPaddingProperty = DependencyProperty.Register("ContentPadding", typeof(Thickness), ownerType, new PropertyMetadata(defaultValue));
            ActualContentPropertyKey = DependencyProperty.RegisterReadOnly("ActualContent", typeof(object), ownerType, new FrameworkPropertyMetadata(null));
            ActualContentProperty = ActualContentPropertyKey.DependencyProperty;
        }

        public WaitIndicator()
        {
            this.SetDefaultStyleKey(typeof(WaitIndicator));
        }

        private void ChangeContentIfNeed(object newContent)
        {
            object localizedString = newContent;
            if (newContent == null)
            {
                object local1 = newContent;
                localizedString = EditorLocalizer.Active.GetLocalizedString(EditorStringId.WaitIndicatorText);
            }
            this.ActualContent = localizedString;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.OnDeferedVisibilityChanged();
            this.ChangeContentIfNeed(base.Content);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            this.ChangeContentIfNeed(newContent);
        }

        private void OnDeferedVisibilityChanged()
        {
            if (this.DeferedVisibility)
            {
                VisualStateManager.GoToState(this, "Visible", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "Collapsed", true);
            }
        }

        private static void OnDeferedVisibilityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WaitIndicator) d).OnDeferedVisibilityChanged();
        }

        public static int? DesiredFrameRate { get; set; }

        public bool DeferedVisibility
        {
            get => 
                (bool) base.GetValue(DeferedVisibilityProperty);
            set => 
                base.SetValue(DeferedVisibilityProperty, value);
        }

        public object ActualContent
        {
            get => 
                base.GetValue(ActualContentProperty);
            internal set => 
                base.SetValue(ActualContentPropertyKey, value);
        }

        public bool ShowShadow
        {
            get => 
                (bool) base.GetValue(ShowShadowProperty);
            set => 
                base.SetValue(ShowShadowProperty, value);
        }

        public Thickness ContentPadding
        {
            get => 
                (Thickness) base.GetValue(ContentPaddingProperty);
            set => 
                base.SetValue(ContentPaddingProperty, value);
        }
    }
}

