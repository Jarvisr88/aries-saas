namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public interface IFrameworkElementAPISupport : IUIElementAPI, IAnimatable, IFrameworkInputElement, IInputElement, ISupportInitialize, IQueryAmbient
    {
        event ContextMenuEventHandler ContextMenuClosing;

        event ContextMenuEventHandler ContextMenuOpening;

        event DependencyPropertyChangedEventHandler DataContextChanged;

        event EventHandler Initialized;

        event RoutedEventHandler Loaded;

        event RequestBringIntoViewEventHandler RequestBringIntoView;

        event SizeChangedEventHandler SizeChanged;

        event EventHandler<DataTransferEventArgs> SourceUpdated;

        event EventHandler<DataTransferEventArgs> TargetUpdated;

        event ToolTipEventHandler ToolTipClosing;

        event ToolTipEventHandler ToolTipOpening;

        event RoutedEventHandler Unloaded;

        void BeginStoryboard(Storyboard storyboard);
        void BeginStoryboard(Storyboard storyboard, HandoffBehavior handoffBehavior);
        void BeginStoryboard(Storyboard storyboard, HandoffBehavior handoffBehavior, bool isControllable);
        void BringIntoView();
        void BringIntoView(Rect targetRectangle);
        object FindResource(object resourceKey);
        BindingExpression GetBindingExpression(DependencyProperty dp);
        void OnApplyTemplate();
        BindingExpression SetBinding(DependencyProperty dp, string path);
        BindingExpressionBase SetBinding(DependencyProperty dp, BindingBase binding);
        void SetResourceReference(DependencyProperty dp, object name);
        [EditorBrowsable(EditorBrowsableState.Never)]
        bool ShouldSerializeResources();
        object TryFindResource(object resourceKey);

        System.Windows.Style Style { get; set; }

        bool OverridesDefaultStyle { get; set; }

        bool UseLayoutRounding { get; set; }

        ResourceDictionary Resources { get; set; }

        [Localizability(LocalizationCategory.NeverLocalize), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        object DataContext { get; set; }

        System.Windows.Data.BindingGroup BindingGroup { get; set; }

        XmlLanguage Language { get; set; }

        [Localizability(LocalizationCategory.NeverLocalize)]
        object Tag { get; set; }

        System.Windows.Input.InputScope InputScope { get; set; }

        Transform LayoutTransform { get; set; }

        double Width { get; set; }

        double MinWidth { get; set; }

        double MaxWidth { get; set; }

        double Height { get; set; }

        double MinHeight { get; set; }

        double MaxHeight { get; set; }

        System.Windows.FlowDirection FlowDirection { get; set; }

        Thickness Margin { get; set; }

        System.Windows.HorizontalAlignment HorizontalAlignment { get; set; }

        System.Windows.VerticalAlignment VerticalAlignment { get; set; }

        System.Windows.Style FocusVisualStyle { get; set; }

        TriggerCollection Triggers { get; }

        DependencyObject TemplatedParent { get; }

        bool IsInitialized { get; }

        bool ForceCursor { get; set; }

        double ActualWidth { get; }

        double ActualHeight { get; }

        System.Windows.Input.Cursor Cursor { get; set; }

        bool IsLoaded { get; }

        System.Windows.Controls.ContextMenu ContextMenu { get; set; }
    }
}

