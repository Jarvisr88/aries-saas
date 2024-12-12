namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;

    public abstract class ActiveWindowFinderBase<T> where T: ActiveWindowFinderBase<T>, new()
    {
        private static readonly T instance;

        static ActiveWindowFinderBase()
        {
            ActiveWindowFinderBase<T>.instance = Activator.CreateInstance<T>();
        }

        protected ActiveWindowFinderBase()
        {
        }

        public Window GetActiveWindow(FrameworkElement element = null)
        {
            Window window3;
            Window window = element.With<FrameworkElement, Window>(new Func<FrameworkElement, Window>(Window.GetWindow));
            if ((window != null) && (window.Dispatcher.CheckAccess() && window.IsLoaded))
            {
                return window;
            }
            if ((Application.Current != null) && Application.Current.Dispatcher.CheckAccess())
            {
                Func<Application, Window> evaluator = <>c<T>.<>9__3_0;
                if (<>c<T>.<>9__3_0 == null)
                {
                    Func<Application, Window> local1 = <>c<T>.<>9__3_0;
                    evaluator = <>c<T>.<>9__3_0 = delegate (Application app) {
                        Func<Window, bool> predicate = <>c<T>.<>9__3_1;
                        if (<>c<T>.<>9__3_1 == null)
                        {
                            Func<Window, bool> local1 = <>c<T>.<>9__3_1;
                            predicate = <>c<T>.<>9__3_1 = x => x.IsActive;
                        }
                        return app.Windows.OfType<Window>().SingleOrDefault<Window>(predicate);
                    };
                }
                return Application.Current.With<Application, Window>(evaluator);
            }
            Func<HwndSource, Visual> selector = <>c<T>.<>9__3_2;
            if (<>c<T>.<>9__3_2 == null)
            {
                Func<HwndSource, Visual> local2 = <>c<T>.<>9__3_2;
                selector = <>c<T>.<>9__3_2 = x => x.RootVisual;
            }
            IEnumerable<Window> source = PresentationSource.CurrentSources.OfType<HwndSource>().Select<HwndSource, Visual>(selector).OfType<Window>();
            using (IEnumerator<Window> enumerator = source.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Window current = enumerator.Current;
                        if ((current == null) || !this.IsSuitableWindow(current))
                        {
                            continue;
                        }
                        window3 = current;
                    }
                    else
                    {
                        return source.LastOrDefault<Window>();
                    }
                    break;
                }
            }
            return window3;
        }

        protected virtual bool IsSuitableWindow(Window window) => 
            false;

        public static T Instance =>
            ActiveWindowFinderBase<T>.instance;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ActiveWindowFinderBase<T>.<>c <>9;
            public static Func<Window, bool> <>9__3_1;
            public static Func<Application, Window> <>9__3_0;
            public static Func<HwndSource, Visual> <>9__3_2;

            static <>c()
            {
                ActiveWindowFinderBase<T>.<>c.<>9 = new ActiveWindowFinderBase<T>.<>c();
            }

            internal Window <GetActiveWindow>b__3_0(Application app)
            {
                Func<Window, bool> predicate = ActiveWindowFinderBase<T>.<>c.<>9__3_1;
                if (ActiveWindowFinderBase<T>.<>c.<>9__3_1 == null)
                {
                    Func<Window, bool> local1 = ActiveWindowFinderBase<T>.<>c.<>9__3_1;
                    predicate = ActiveWindowFinderBase<T>.<>c.<>9__3_1 = x => x.IsActive;
                }
                return app.Windows.OfType<Window>().SingleOrDefault<Window>(predicate);
            }

            internal bool <GetActiveWindow>b__3_1(Window x) => 
                x.IsActive;

            internal Visual <GetActiveWindow>b__3_2(HwndSource x) => 
                x.RootVisual;
        }
    }
}

