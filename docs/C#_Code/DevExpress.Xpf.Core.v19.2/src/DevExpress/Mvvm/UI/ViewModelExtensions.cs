namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public static class ViewModelExtensions
    {
        public static readonly object NotSetParameter = new object();
        public static readonly DependencyProperty ParameterProperty;
        public static readonly DependencyProperty ParentViewModelProperty;
        public static readonly DependencyProperty DocumentOwnerProperty;
        public static readonly DependencyProperty DocumentTitleProperty;

        static ViewModelExtensions()
        {
            ParameterProperty = DependencyProperty.RegisterAttached("Parameter", typeof(object), typeof(ViewModelExtensions), new PropertyMetadata(NotSetParameter, (d, e) => OnParameterChanged(d, e.NewValue)));
            ParentViewModelProperty = DependencyProperty.RegisterAttached("ParentViewModel", typeof(object), typeof(ViewModelExtensions), new PropertyMetadata(null, (d, e) => OnParentViewModelChanged(d, e.NewValue)));
            DocumentOwnerProperty = DependencyProperty.RegisterAttached("DocumentOwner", typeof(IDocumentOwner), typeof(ViewModelExtensions), new PropertyMetadata(null, (d, e) => OnDocumentOwnerChanged(d, e.NewValue as IDocumentOwner)));
            DocumentTitleProperty = DependencyProperty.RegisterAttached("DocumentTitle", typeof(object), typeof(ViewModelExtensions), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        }

        public static IDocumentOwner GetDocumentOwner(DependencyObject obj)
        {
            IDocumentOwner owner1 = (IDocumentOwner) obj.GetValue(DocumentOwnerProperty);
            IDocumentOwner local5 = owner1;
            if (owner1 == null)
            {
                IDocumentOwner local1 = owner1;
                IDocumentContent evaluator = ViewHelper.GetViewModelFromView(obj).With<object, IDocumentContent>(<>c.<>9__7_0 ??= x => (x as IDocumentContent));
                if (<>c.<>9__7_1 == null)
                {
                    IDocumentContent local3 = ViewHelper.GetViewModelFromView(obj).With<object, IDocumentContent>(<>c.<>9__7_0 ??= x => (x as IDocumentContent));
                    evaluator = (IDocumentContent) (<>c.<>9__7_1 = x => x.DocumentOwner);
                }
                local5 = ((IDocumentContent) <>c.<>9__7_1).Return<IDocumentContent, IDocumentOwner>(evaluator, null);
            }
            return local5;
        }

        public static object GetDocumentTitle(DependencyObject d) => 
            d.GetValue(DocumentTitleProperty);

        public static object GetParameter(DependencyObject obj)
        {
            object obj1 = obj.GetValue(ParameterProperty);
            object local5 = obj1;
            if (obj1 == null)
            {
                object local1 = obj1;
                ISupportParameter evaluator = ViewHelper.GetViewModelFromView(obj).With<object, ISupportParameter>(<>c.<>9__5_0 ??= x => (x as ISupportParameter));
                if (<>c.<>9__5_1 == null)
                {
                    ISupportParameter local3 = ViewHelper.GetViewModelFromView(obj).With<object, ISupportParameter>(<>c.<>9__5_0 ??= x => (x as ISupportParameter));
                    evaluator = (ISupportParameter) (<>c.<>9__5_1 = x => x.Parameter);
                }
                local5 = ((ISupportParameter) <>c.<>9__5_1).Return<ISupportParameter, object>(evaluator, null);
            }
            return local5;
        }

        public static object GetParentViewModel(DependencyObject obj)
        {
            object obj1 = obj.GetValue(ParentViewModelProperty);
            object local5 = obj1;
            if (obj1 == null)
            {
                object local1 = obj1;
                ISupportParentViewModel evaluator = ViewHelper.GetViewModelFromView(obj).With<object, ISupportParentViewModel>(<>c.<>9__6_0 ??= x => (x as ISupportParentViewModel));
                if (<>c.<>9__6_1 == null)
                {
                    ISupportParentViewModel local3 = ViewHelper.GetViewModelFromView(obj).With<object, ISupportParentViewModel>(<>c.<>9__6_0 ??= x => (x as ISupportParentViewModel));
                    evaluator = (ISupportParentViewModel) (<>c.<>9__6_1 = x => x.ParentViewModel);
                }
                local5 = ((ISupportParentViewModel) <>c.<>9__6_1).Return<ISupportParentViewModel, object>(evaluator, null);
            }
            return local5;
        }

        private static void OnDocumentOwnerChanged(DependencyObject d, IDocumentOwner newValue)
        {
            ViewModelInitializer.SetViewModelDocumentOwner(d, newValue);
            ParameterAndParentViewModelSyncBehavior.AttachTo(d);
        }

        private static void OnParameterChanged(DependencyObject d, object newValue)
        {
            if (NotSetParameter != newValue)
            {
                ViewModelInitializer.SetViewModelParameter(d, newValue);
                ParameterAndParentViewModelSyncBehavior.AttachTo(d);
            }
        }

        private static void OnParentViewModelChanged(DependencyObject d, object newValue)
        {
            ViewModelInitializer.SetViewModelParentViewModel(d, newValue);
            ParameterAndParentViewModelSyncBehavior.AttachTo(d);
        }

        public static void SetDocumentOwner(DependencyObject obj, IDocumentOwner value)
        {
            obj.SetValue(DocumentOwnerProperty, value);
        }

        public static void SetDocumentTitle(DependencyObject d, object value)
        {
            d.SetValue(DocumentTitleProperty, value);
        }

        public static void SetParameter(DependencyObject obj, object value)
        {
            obj.SetValue(ParameterProperty, value);
        }

        public static void SetParentViewModel(DependencyObject obj, object value)
        {
            obj.SetValue(ParentViewModelProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ViewModelExtensions.<>c <>9 = new ViewModelExtensions.<>c();
            public static Func<object, ISupportParameter> <>9__5_0;
            public static Func<ISupportParameter, object> <>9__5_1;
            public static Func<object, ISupportParentViewModel> <>9__6_0;
            public static Func<ISupportParentViewModel, object> <>9__6_1;
            public static Func<object, IDocumentContent> <>9__7_0;
            public static Func<IDocumentContent, IDocumentOwner> <>9__7_1;

            internal void <.cctor>b__17_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ViewModelExtensions.OnParameterChanged(d, e.NewValue);
            }

            internal void <.cctor>b__17_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ViewModelExtensions.OnParentViewModelChanged(d, e.NewValue);
            }

            internal void <.cctor>b__17_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ViewModelExtensions.OnDocumentOwnerChanged(d, e.NewValue as IDocumentOwner);
            }

            internal IDocumentContent <GetDocumentOwner>b__7_0(object x) => 
                x as IDocumentContent;

            internal IDocumentOwner <GetDocumentOwner>b__7_1(IDocumentContent x) => 
                x.DocumentOwner;

            internal ISupportParameter <GetParameter>b__5_0(object x) => 
                x as ISupportParameter;

            internal object <GetParameter>b__5_1(ISupportParameter x) => 
                x.Parameter;

            internal ISupportParentViewModel <GetParentViewModel>b__6_0(object x) => 
                x as ISupportParentViewModel;

            internal object <GetParentViewModel>b__6_1(ISupportParentViewModel x) => 
                x.ParentViewModel;
        }

        private class ParameterAndParentViewModelSyncBehavior : Behavior<DependencyObject>
        {
            private ParameterAndParentViewModelSyncBehavior()
            {
            }

            public static void AttachTo(DependencyObject obj)
            {
                if ((obj is FrameworkElement) || (obj is FrameworkContentElement))
                {
                    BehaviorCollection source = Interaction.GetBehaviors(obj);
                    Func<Behavior, bool> predicate = <>c.<>9__0_0;
                    if (<>c.<>9__0_0 == null)
                    {
                        Func<Behavior, bool> local1 = <>c.<>9__0_0;
                        predicate = <>c.<>9__0_0 = x => x is ViewModelExtensions.ParameterAndParentViewModelSyncBehavior;
                    }
                    if (((ViewModelExtensions.ParameterAndParentViewModelSyncBehavior) source.FirstOrDefault<Behavior>(predicate)) == null)
                    {
                        source.Add(new ViewModelExtensions.ParameterAndParentViewModelSyncBehavior());
                    }
                }
            }

            private void OnAssociatedObjectDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
            {
                ViewModelInitializer.SetViewModelParameter(base.AssociatedObject, ViewModelExtensions.GetParameter(base.AssociatedObject));
                ViewModelInitializer.SetViewModelParentViewModel(base.AssociatedObject, ViewModelExtensions.GetParentViewModel(base.AssociatedObject));
                ViewModelInitializer.SetViewModelDocumentOwner(base.AssociatedObject, ViewModelExtensions.GetDocumentOwner(base.AssociatedObject));
            }

            private void OnAssociatedObjectUnloaded(object sender, RoutedEventArgs e)
            {
                this.Unsubscribe();
            }

            protected override void OnAttached()
            {
                base.OnAttached();
                this.Subscribe();
            }

            protected override void OnDetaching()
            {
                this.Unsubscribe();
                base.OnDetaching();
            }

            private void Subscribe()
            {
                this.Unsubscribe();
                (base.AssociatedObject as FrameworkElement).Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.Unloaded += new RoutedEventHandler(this.OnAssociatedObjectUnloaded);
                });
                (base.AssociatedObject as FrameworkElement).Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.DataContextChanged += new DependencyPropertyChangedEventHandler(this.OnAssociatedObjectDataContextChanged);
                });
                (base.AssociatedObject as FrameworkContentElement).Do<FrameworkContentElement>(delegate (FrameworkContentElement x) {
                    x.Unloaded += new RoutedEventHandler(this.OnAssociatedObjectUnloaded);
                });
                (base.AssociatedObject as FrameworkContentElement).Do<FrameworkContentElement>(delegate (FrameworkContentElement x) {
                    x.DataContextChanged += new DependencyPropertyChangedEventHandler(this.OnAssociatedObjectDataContextChanged);
                });
            }

            private void Unsubscribe()
            {
                (base.AssociatedObject as FrameworkElement).Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.Unloaded -= new RoutedEventHandler(this.OnAssociatedObjectUnloaded);
                });
                (base.AssociatedObject as FrameworkElement).Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.DataContextChanged -= new DependencyPropertyChangedEventHandler(this.OnAssociatedObjectDataContextChanged);
                });
                (base.AssociatedObject as FrameworkContentElement).Do<FrameworkContentElement>(delegate (FrameworkContentElement x) {
                    x.Unloaded -= new RoutedEventHandler(this.OnAssociatedObjectUnloaded);
                });
                (base.AssociatedObject as FrameworkContentElement).Do<FrameworkContentElement>(delegate (FrameworkContentElement x) {
                    x.DataContextChanged -= new DependencyPropertyChangedEventHandler(this.OnAssociatedObjectDataContextChanged);
                });
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly ViewModelExtensions.ParameterAndParentViewModelSyncBehavior.<>c <>9 = new ViewModelExtensions.ParameterAndParentViewModelSyncBehavior.<>c();
                public static Func<Behavior, bool> <>9__0_0;

                internal bool <AttachTo>b__0_0(Behavior x) => 
                    x is ViewModelExtensions.ParameterAndParentViewModelSyncBehavior;
            }
        }
    }
}

