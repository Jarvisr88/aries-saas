namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class NavigationPaneSettings : FrameworkElement
    {
        public static readonly DependencyProperty ActiveTabProperty;
        public static readonly DependencyProperty ShowDocumentMapTabProperty;
        public static readonly DependencyProperty ShowPagesTabProperty;
        public static readonly DependencyProperty UseCaseSensitiveSearchProperty;
        public static readonly DependencyProperty SearchWholeWordsProperty;
        private INavigationPaneUI navigationPane;

        static NavigationPaneSettings()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(NavigationPaneSettings), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<NavigationPaneSettings> registrator1 = DependencyPropertyRegistrator<NavigationPaneSettings>.New().Register<NavigationPaneTabType>(System.Linq.Expressions.Expression.Lambda<Func<NavigationPaneSettings, NavigationPaneTabType>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NavigationPaneSettings.get_ActiveTab)), parameters), out ActiveTabProperty, NavigationPaneTabType.SearchResults, (Func<NavigationPaneSettings, NavigationPaneTabType, NavigationPaneTabType>) ((d, n) => d.CoerceActiveTab(n)), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(NavigationPaneSettings), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<NavigationPaneSettings> registrator2 = registrator1.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<NavigationPaneSettings, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NavigationPaneSettings.get_ShowDocumentMapTab)), expressionArray2), out ShowDocumentMapTabProperty, true, d => d.OnShowDocumentMapChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(NavigationPaneSettings), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<NavigationPaneSettings> registrator3 = registrator2.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<NavigationPaneSettings, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NavigationPaneSettings.get_ShowPagesTab)), expressionArray3), out ShowPagesTabProperty, true, d => d.OnShowPagesTabChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(NavigationPaneSettings), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<NavigationPaneSettings> registrator4 = registrator3.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<NavigationPaneSettings, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NavigationPaneSettings.get_UseCaseSensitiveSearch)), expressionArray4), out UseCaseSensitiveSearchProperty, false, d => d.OnSearchSettingsChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(NavigationPaneSettings), "d");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator4.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<NavigationPaneSettings, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NavigationPaneSettings.get_SearchWholeWords)), expressionArray5), out SearchWholeWordsProperty, false, d => d.OnShowPagesTabChanged(), frameworkOptions);
        }

        internal void AssignNavigationPaneUI(INavigationPaneUI navigationPane)
        {
            this.navigationPane = navigationPane;
            Action<INavigationPaneUI> action = <>c.<>9__26_0;
            if (<>c.<>9__26_0 == null)
            {
                Action<INavigationPaneUI> local1 = <>c.<>9__26_0;
                action = <>c.<>9__26_0 = x => x.SyncSearchParametersWithModel();
            }
            navigationPane.Do<INavigationPaneUI>(action);
        }

        private NavigationPaneTabType CoerceActiveTab(NavigationPaneTabType newValue)
        {
            if (((newValue == NavigationPaneTabType.DocumentMap) && !this.ShowDocumentMapTab) || ((newValue == NavigationPaneTabType.Pages) && !this.ShowPagesTab))
            {
                return NavigationPaneTabType.SearchResults;
            }
            return newValue;
        }

        public void ForceFocusSearchBox()
        {
            Action<DocumentPreviewControl> action = <>c.<>9__27_0;
            if (<>c.<>9__27_0 == null)
            {
                Action<DocumentPreviewControl> local1 = <>c.<>9__27_0;
                action = <>c.<>9__27_0 = x => x.ShowNavigationPane = true;
            }
            (DocumentViewerControl.GetActualViewer((FrameworkElement) this.navigationPane) as DocumentPreviewControl).Do<DocumentPreviewControl>(action);
            DispatcherHelper.DoEvents(3);
            this.ActiveTab = NavigationPaneTabType.SearchResults;
            this.navigationPane.FocusSearchBox();
        }

        private void OnSearchSettingsChanged()
        {
            Action<INavigationPaneUI> action = <>c.<>9__25_0;
            if (<>c.<>9__25_0 == null)
            {
                Action<INavigationPaneUI> local1 = <>c.<>9__25_0;
                action = <>c.<>9__25_0 = x => x.SyncSearchParametersWithModel();
            }
            this.navigationPane.Do<INavigationPaneUI>(action);
        }

        private void OnShowDocumentMapChanged()
        {
            if (!this.ShowDocumentMapTab)
            {
                this.ActiveTab ??= NavigationPaneTabType.SearchResults;
            }
        }

        private void OnShowPagesTabChanged()
        {
            if (!this.ShowPagesTab && (this.ActiveTab == NavigationPaneTabType.Pages))
            {
                this.ActiveTab = NavigationPaneTabType.SearchResults;
            }
        }

        public NavigationPaneTabType ActiveTab
        {
            get => 
                (NavigationPaneTabType) base.GetValue(ActiveTabProperty);
            set => 
                base.SetValue(ActiveTabProperty, value);
        }

        public bool ShowDocumentMapTab
        {
            get => 
                (bool) base.GetValue(ShowDocumentMapTabProperty);
            set => 
                base.SetValue(ShowDocumentMapTabProperty, value);
        }

        public bool ShowPagesTab
        {
            get => 
                (bool) base.GetValue(ShowPagesTabProperty);
            set => 
                base.SetValue(ShowPagesTabProperty, value);
        }

        public bool UseCaseSensitiveSearch
        {
            get => 
                (bool) base.GetValue(UseCaseSensitiveSearchProperty);
            set => 
                base.SetValue(UseCaseSensitiveSearchProperty, value);
        }

        public bool SearchWholeWords
        {
            get => 
                (bool) base.GetValue(SearchWholeWordsProperty);
            set => 
                base.SetValue(SearchWholeWordsProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NavigationPaneSettings.<>c <>9 = new NavigationPaneSettings.<>c();
            public static Action<INavigationPaneUI> <>9__25_0;
            public static Action<INavigationPaneUI> <>9__26_0;
            public static Action<DocumentPreviewControl> <>9__27_0;

            internal NavigationPaneTabType <.cctor>b__21_0(NavigationPaneSettings d, NavigationPaneTabType n) => 
                d.CoerceActiveTab(n);

            internal void <.cctor>b__21_1(NavigationPaneSettings d)
            {
                d.OnShowDocumentMapChanged();
            }

            internal void <.cctor>b__21_2(NavigationPaneSettings d)
            {
                d.OnShowPagesTabChanged();
            }

            internal void <.cctor>b__21_3(NavigationPaneSettings d)
            {
                d.OnSearchSettingsChanged();
            }

            internal void <.cctor>b__21_4(NavigationPaneSettings d)
            {
                d.OnShowPagesTabChanged();
            }

            internal void <AssignNavigationPaneUI>b__26_0(INavigationPaneUI x)
            {
                x.SyncSearchParametersWithModel();
            }

            internal void <ForceFocusSearchBox>b__27_0(DocumentPreviewControl x)
            {
                x.ShowNavigationPane = true;
            }

            internal void <OnSearchSettingsChanged>b__25_0(INavigationPaneUI x)
            {
                x.SyncSearchParametersWithModel();
            }
        }
    }
}

