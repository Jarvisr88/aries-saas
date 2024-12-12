namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class TextHighlightingBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty TextBlockTextProperty;
        public static readonly DependencyProperty IsTextBlockUpdatingProperty;
        private RoutedEventHandler HighlightTextChangedHandler;
        private BindingBase originalBinding;
        private Locker updateTextLocker = new Locker();
        private Action updateTextInfoPostpone;

        static TextHighlightingBehavior()
        {
            Type ownerType = typeof(TextHighlightingBehavior);
            TextBlockTextProperty = DependencyProperty.RegisterAttached("TextBlockText", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(TextHighlightingBehavior.TextBlockTextChanged)));
            IsTextBlockUpdatingProperty = DependencyProperty.RegisterAttached("IsTextBlockUpdating", typeof(bool), ownerType);
        }

        public static bool GetIsTextBlockUpdating(DependencyObject d) => 
            (bool) d.GetValue(IsTextBlockUpdatingProperty);

        public static string GetTextBlockText(DependencyObject d) => 
            (string) d.GetValue(TextBlockTextProperty);

        private void HighlightTextChanged(object sender, RoutedEventArgs e)
        {
            this.UpdateTextInfo(this.TextBlock);
        }

        private void Initialize()
        {
            this.IsInitialized = true;
            this.SetTextBinding();
            if (this.updateTextInfoPostpone != null)
            {
                this.updateTextInfoPostpone();
                this.updateTextInfoPostpone = null;
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.TextBlock != null)
            {
                if (this.TextBlock.IsInitialized)
                {
                    this.Initialize();
                }
                else
                {
                    this.TextBlock.Initialized += new EventHandler(this.TextBlockInitialized);
                }
                this.HighlightTextChangedHandler = new RoutedEventHandler(this.HighlightTextChanged);
                TextBlockService.AddHighlightedTextChangedHandler(this.TextBlock, this.HighlightTextChangedHandler);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.IsInitialized = false;
            if (this.originalBinding != null)
            {
                this.TextBlock.SetBinding(System.Windows.Controls.TextBlock.TextProperty, this.originalBinding);
                this.originalBinding = null;
            }
            BindingOperations.ClearBinding(this, TextBlockTextProperty);
            if (this.TextBlock != null)
            {
                TextBlockService.RemoveHighlightedTextChangedHandler(this.TextBlock, this.HighlightTextChangedHandler);
            }
        }

        internal void OnTextBlockTextChanged(System.Windows.Controls.TextBlock textBlock)
        {
            this.UpdateTextInfo(textBlock);
        }

        public static void SetIsTextBlockUpdating(DependencyObject d, bool value)
        {
            d.SetValue(IsTextBlockUpdatingProperty, value);
        }

        private void SetTextBinding()
        {
            BindingBase binding = null;
            MultiBindingExpression multiBindingExpression = BindingOperations.GetMultiBindingExpression(this.TextBlock, System.Windows.Controls.TextBlock.TextProperty);
            if (multiBindingExpression != null)
            {
                binding = this.originalBinding = multiBindingExpression.ParentMultiBinding;
            }
            else
            {
                BindingExpression bindingExpression = this.TextBlock.GetBindingExpression(System.Windows.Controls.TextBlock.TextProperty);
                if (bindingExpression != null)
                {
                    binding = this.originalBinding = bindingExpression.ParentBinding;
                }
                else
                {
                    Binding binding1 = new Binding("Text");
                    binding1.Source = this.TextBlock;
                    binding1.Mode = BindingMode.OneWay;
                    binding = binding1;
                }
            }
            BindingOperations.SetBinding(this.TextBlock, TextBlockTextProperty, binding);
        }

        public static void SetTextBlockText(DependencyObject d, string value)
        {
            d.SetValue(TextBlockTextProperty, value);
        }

        private void TextBlockInitialized(object sender, EventArgs e)
        {
            ((FrameworkElement) sender).Initialized -= new EventHandler(this.TextBlockInitialized);
            if (this.TextBlock != null)
            {
                this.Initialize();
            }
        }

        private static void TextBlockTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            System.Windows.Controls.TextBlock d = obj as System.Windows.Controls.TextBlock;
            if ((d != null) && !((bool) d.GetValue(IsTextBlockUpdatingProperty)))
            {
                Func<Behavior, bool> predicate = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Func<Behavior, bool> local1 = <>c.<>9__3_0;
                    predicate = <>c.<>9__3_0 = x => x is TextHighlightingBehavior;
                }
                TextHighlightingBehavior behavior = Interaction.GetBehaviors(d).FirstOrDefault<Behavior>(predicate) as TextHighlightingBehavior;
                if (behavior != null)
                {
                    behavior.OnTextBlockTextChanged(d);
                }
            }
        }

        private void UpdateTextInfo(System.Windows.Controls.TextBlock textBlock)
        {
            if (this.IsInitialized)
            {
                this.UpdateTextInfoWithLock(textBlock);
            }
            else
            {
                this.updateTextInfoPostpone = () => this.UpdateTextInfoWithLock(textBlock);
            }
        }

        private void UpdateTextInfoCore(System.Windows.Controls.TextBlock textBlock)
        {
            if (textBlock != null)
            {
                string str = (string) textBlock.GetValue(TextBlockTextProperty);
                if (string.IsNullOrEmpty(str) || !string.IsNullOrWhiteSpace(str))
                {
                    string str2 = (string) this.TextBlock.GetValue(TextBlockService.HighlightedTextProperty);
                    TextBlockInfo info1 = new TextBlockInfo();
                    info1.Text = str;
                    info1.HighlightedText = str2;
                    info1.HighlightedTextCriteria = (HighlightedTextCriteria) this.TextBlock.GetValue(TextBlockService.HighlightedTextCriteriaProperty);
                    info1.Appearance = (IHighlighterAppearance) this.TextBlock.GetValue(TextBlockService.HighlightedTextAppearanceProperty);
                    TextBlockInfo info = info1;
                    textBlock.SetValue(TextBlockService.TextInfoProperty, info);
                }
            }
        }

        private void UpdateTextInfoWithLock(System.Windows.Controls.TextBlock textBlock)
        {
            if (!this.updateTextLocker.IsLocked)
            {
                this.updateTextLocker.DoLockedAction(() => this.UpdateTextInfoCore(textBlock));
            }
        }

        private System.Windows.Controls.TextBlock TextBlock =>
            base.AssociatedObject as System.Windows.Controls.TextBlock;

        private bool IsInitialized { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TextHighlightingBehavior.<>c <>9 = new TextHighlightingBehavior.<>c();
            public static Func<Behavior, bool> <>9__3_0;

            internal bool <TextBlockTextChanged>b__3_0(Behavior x) => 
                x is TextHighlightingBehavior;
        }
    }
}

