namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interop;

    public abstract class ViewServiceBase : ServiceBase
    {
        public static readonly DependencyProperty ViewLocatorProperty;
        public static readonly DependencyProperty ViewTemplateProperty;
        public static readonly DependencyProperty ViewTemplateSelectorProperty;

        static ViewServiceBase()
        {
            ViewLocatorProperty = DependencyProperty.Register("ViewLocator", typeof(IViewLocator), typeof(ViewServiceBase), new PropertyMetadata(null, (d, e) => ((ViewServiceBase) d).OnViewLocatorChanged((IViewLocator) e.OldValue, (IViewLocator) e.NewValue)));
            ViewTemplateProperty = DependencyProperty.Register("ViewTemplate", typeof(DataTemplate), typeof(ViewServiceBase), new PropertyMetadata(null, (d, e) => ((ViewServiceBase) d).OnViewTemplateChanged((DataTemplate) e.OldValue, (DataTemplate) e.NewValue)));
            ViewTemplateSelectorProperty = DependencyProperty.Register("ViewTemplateSelector", typeof(DataTemplateSelector), typeof(ViewServiceBase), new PropertyMetadata(null, (d, e) => ((ViewServiceBase) d).OnViewTemplateSelectorChanged((DataTemplateSelector) e.OldValue, (DataTemplateSelector) e.NewValue)));
        }

        protected ViewServiceBase()
        {
        }

        protected object CreateAndInitializeView(string documentType, object viewModel, object parameter, object parentViewModel, IDocumentOwner documentOwner = null) => 
            ViewHelper.CreateAndInitializeView(this.ViewLocator, documentType, viewModel, parameter, parentViewModel, documentOwner, this.ViewTemplate, this.ViewTemplateSelector);

        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        private static extern IntPtr GetActiveWindow();
        protected Style GetDocumentContainerStyle(DependencyObject documentContainer, object view, Style style, StyleSelector styleSelector) => 
            style ?? styleSelector.With<StyleSelector, Style>(s => s.SelectStyle(ViewHelper.GetViewModelFromView(view), documentContainer));

        protected void InitializeDocumentContainer(FrameworkElement documentContainer, DependencyProperty documentContainerViewProperty, Style documentContainerStyle)
        {
            ViewHelper.SetBindingToViewModel(documentContainer, FrameworkElement.DataContextProperty, new PropertyPath(documentContainerViewProperty));
            if (documentContainerStyle != null)
            {
                documentContainer.Style = documentContainerStyle;
            }
        }

        protected virtual void OnViewLocatorChanged(IViewLocator oldValue, IViewLocator newValue)
        {
        }

        protected virtual void OnViewTemplateChanged(DataTemplate oldValue, DataTemplate newValue)
        {
        }

        protected virtual void OnViewTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue)
        {
        }

        protected void UpdateThemeName(FrameworkElement target)
        {
            UpdateThemeName(target, base.AssociatedObject);
        }

        public static void UpdateThemeName(FrameworkElement target, FrameworkElement associatedObject)
        {
            if (associatedObject != null)
            {
                if (!target.IsPropertySet(FrameworkElement.FlowDirectionProperty))
                {
                    target.SetCurrentValue(FrameworkElement.FlowDirectionProperty, associatedObject.FlowDirection);
                }
                if (!target.IsPropertySet(ThemeManager.ThemeNameProperty))
                {
                    ThemeTreeWalker treeWalker = ThemeManager.GetTreeWalker(target);
                    ThemeTreeWalker objB = ThemeManager.GetTreeWalker(associatedObject);
                    if ((objB != null) && !ReferenceEquals(treeWalker, objB))
                    {
                        string windowThemeName = ThemeHelper.GetWindowThemeName(associatedObject);
                        if ((windowThemeName == null) && (objB.ThemeName == Theme.DeepBlue.Name))
                        {
                            windowThemeName = Theme.DeepBlue.Name;
                        }
                        if (ApplicationThemeHelper.ApplicationThemeName != windowThemeName)
                        {
                            ThemeManager.SetThemeName(target, windowThemeName);
                        }
                    }
                }
            }
        }

        [SecuritySafeCritical]
        internal static void UpdateWindowOwner(Window w, DependencyObject ownerObject)
        {
            if (ownerObject != null)
            {
                if (ViewModelBase.IsInDesignMode)
                {
                    new WindowInteropHelper(w).Owner = GetActiveWindow();
                }
                else
                {
                    Window local1 = LayoutTreeHelper.GetVisualParents(ownerObject, null).OfType<Window>().FirstOrDefault<Window>();
                    Window window = local1;
                    if (local1 == null)
                    {
                        Window local2 = local1;
                        window = Window.GetWindow(ownerObject);
                    }
                    w.Owner = window;
                }
            }
        }

        public IViewLocator ViewLocator
        {
            get => 
                (IViewLocator) base.GetValue(ViewLocatorProperty);
            set => 
                base.SetValue(ViewLocatorProperty, value);
        }

        public DataTemplate ViewTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ViewTemplateProperty);
            set => 
                base.SetValue(ViewTemplateProperty, value);
        }

        public DataTemplateSelector ViewTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ViewTemplateSelectorProperty);
            set => 
                base.SetValue(ViewTemplateSelectorProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ViewServiceBase.<>c <>9 = new ViewServiceBase.<>c();

            internal void <.cctor>b__23_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ViewServiceBase) d).OnViewLocatorChanged((IViewLocator) e.OldValue, (IViewLocator) e.NewValue);
            }

            internal void <.cctor>b__23_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ViewServiceBase) d).OnViewTemplateChanged((DataTemplate) e.OldValue, (DataTemplate) e.NewValue);
            }

            internal void <.cctor>b__23_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ViewServiceBase) d).OnViewTemplateSelectorChanged((DataTemplateSelector) e.OldValue, (DataTemplateSelector) e.NewValue);
            }
        }
    }
}

