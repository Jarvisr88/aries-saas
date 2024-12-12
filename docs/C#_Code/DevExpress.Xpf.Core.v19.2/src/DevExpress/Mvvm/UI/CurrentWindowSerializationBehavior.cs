namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Utils.Serializing;
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CurrentWindowSerializationBehavior : Behavior<DependencyObject>
    {
        private List<Tuple<string, Action<Window>>> onceLoaded = new List<Tuple<string, Action<Window>>>();
        private Window window;
        private FrameworkElement fe;
        private bool initialized;
        private double normalStateWidth;
        private double normalStateHeight;

        static CurrentWindowSerializationBehavior()
        {
            DXSerializer.SerializationIDDefaultProperty.OverrideMetadata(typeof(CurrentWindowSerializationBehavior), new UIPropertyMetadata("activeWindowId"));
        }

        private void DoOrPostpone(string id, Action<Window> action)
        {
            if (!this.initialized)
            {
                this.onceLoaded.Add(Tuple.Create<string, Action<Window>>(id, action));
            }
            else if (this.window != null)
            {
                action(this.window);
            }
        }

        private void HandleAssociatedObjectInitialized()
        {
            if (!this.initialized && (this.window != null))
            {
                this.initialized = true;
                this.window.SizeChanged += new SizeChangedEventHandler(this.window_SizeChanged);
                Func<Tuple<string, Action<Window>>, string> keySelector = <>c.<>9__9_0;
                if (<>c.<>9__9_0 == null)
                {
                    Func<Tuple<string, Action<Window>>, string> local1 = <>c.<>9__9_0;
                    keySelector = <>c.<>9__9_0 = x => x.Item1;
                }
                Func<IGrouping<string, Tuple<string, Action<Window>>>, Action<Window>> selector = <>c.<>9__9_1;
                if (<>c.<>9__9_1 == null)
                {
                    Func<IGrouping<string, Tuple<string, Action<Window>>>, Action<Window>> local2 = <>c.<>9__9_1;
                    selector = <>c.<>9__9_1 = x => x.Last<Tuple<string, Action<Window>>>().Item2;
                }
                this.onceLoaded.GroupBy<Tuple<string, Action<Window>>, string>(keySelector).Select<IGrouping<string, Tuple<string, Action<Window>>>, Action<Window>>(selector).ForEach<Action<Window>>(x => x(this.window));
                this.onceLoaded = null;
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.fe = base.AssociatedObject as FrameworkElement;
            if (this.fe != null)
            {
                EventHandler handler = null;
                this.TryGetWindow(this.fe);
                this.HandleAssociatedObjectInitialized();
                handler = delegate (object s, EventArgs e) {
                    this.TryGetWindow(this.fe);
                    this.HandleAssociatedObjectInitialized();
                    this.window_SizeChanged(null, null);
                    this.fe.Initialized -= handler;
                };
                this.fe.Initialized += handler;
                RoutedEventHandler loadedHandler = null;
                loadedHandler = delegate (object s, RoutedEventArgs e) {
                    this.TryGetWindow(this.fe);
                    this.window_SizeChanged(null, null);
                    this.fe.Loaded -= loadedHandler;
                };
                this.fe.Loaded += loadedHandler;
            }
        }

        private void TryGetWindow(FrameworkElement fe)
        {
            Window window = this.window;
            if (this.window == null)
            {
                Window local1 = this.window;
                Window window1 = Window.GetWindow(fe);
                window = window1;
                if (window1 == null)
                {
                    Window local2 = window1;
                    Window local3 = LayoutTreeHelper.GetVisualParents(base.AssociatedObject, null).OfType<Window>().FirstOrDefault<Window>();
                    window = local3;
                    if (local3 == null)
                    {
                        Window local4 = local3;
                        window = (Application.Current == null) ? null : Application.Current.Windows.OfType<Window>().FirstOrDefault<Window>();
                    }
                }
            }
            this.window = window;
        }

        private void window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if ((this.window != null) && (this.window.WindowState == System.Windows.WindowState.Normal))
            {
                this.normalStateWidth = this.window.Width;
                this.normalStateHeight = this.window.Height;
            }
        }

        [XtraSerializableProperty]
        public System.Windows.WindowState WindowState
        {
            get => 
                ((this.window == null) || (this.window.WindowState == System.Windows.WindowState.Minimized)) ? System.Windows.WindowState.Normal : this.window.WindowState;
            set => 
                this.DoOrPostpone(BindableBase.GetPropertyName<System.Windows.WindowState>(System.Linq.Expressions.Expression.Lambda<Func<System.Windows.WindowState>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(CurrentWindowSerializationBehavior)), (MethodInfo) methodof(CurrentWindowSerializationBehavior.get_WindowState)), new ParameterExpression[0])), w => w.WindowState = value);
        }

        [XtraSerializableProperty]
        public double Width
        {
            get => 
                this.normalStateWidth;
            set => 
                this.DoOrPostpone(BindableBase.GetPropertyName<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(CurrentWindowSerializationBehavior)), (MethodInfo) methodof(CurrentWindowSerializationBehavior.get_Width)), new ParameterExpression[0])), w => w.Width = value);
        }

        [XtraSerializableProperty]
        public double Height
        {
            get => 
                this.normalStateHeight;
            set => 
                this.DoOrPostpone(BindableBase.GetPropertyName<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(CurrentWindowSerializationBehavior)), (MethodInfo) methodof(CurrentWindowSerializationBehavior.get_Height)), new ParameterExpression[0])), w => w.Height = value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CurrentWindowSerializationBehavior.<>c <>9 = new CurrentWindowSerializationBehavior.<>c();
            public static Func<Tuple<string, Action<Window>>, string> <>9__9_0;
            public static Func<IGrouping<string, Tuple<string, Action<Window>>>, Action<Window>> <>9__9_1;

            internal string <HandleAssociatedObjectInitialized>b__9_0(Tuple<string, Action<Window>> x) => 
                x.Item1;

            internal Action<Window> <HandleAssociatedObjectInitialized>b__9_1(IGrouping<string, Tuple<string, Action<Window>>> x) => 
                x.Last<Tuple<string, Action<Window>>>().Item2;
        }
    }
}

