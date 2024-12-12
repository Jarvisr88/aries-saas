namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TabbedWindowLayoutSerializationService : LayoutSerializationService
    {
        protected override FrameworkElement GetSerializationTarget()
        {
            Func<Application, IEnumerable<Window>> evaluator = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<Application, IEnumerable<Window>> local1 = <>c.<>9__0_0;
                evaluator = <>c.<>9__0_0 = x => x.Windows.OfType<Window>();
            }
            Func<IEnumerable<Window>, Window> func2 = <>c.<>9__0_1;
            if (<>c.<>9__0_1 == null)
            {
                Func<IEnumerable<Window>, Window> local2 = <>c.<>9__0_1;
                func2 = <>c.<>9__0_1 = delegate (IEnumerable<Window> windows) {
                    Func<Window, bool> predicate = <>c.<>9__0_2;
                    if (<>c.<>9__0_2 == null)
                    {
                        Func<Window, bool> local1 = <>c.<>9__0_2;
                        predicate = <>c.<>9__0_2 = x => x.IsActive;
                    }
                    Window local2 = windows.FirstOrDefault<Window>(predicate);
                    Window local4 = local2;
                    if (local2 == null)
                    {
                        Window local3 = local2;
                        local4 = windows.LastOrDefault<Window>();
                    }
                    return local4;
                };
            }
            return Application.Current.With<Application, IEnumerable<Window>>(evaluator).With<IEnumerable<Window>, Window>(func2);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabbedWindowLayoutSerializationService.<>c <>9 = new TabbedWindowLayoutSerializationService.<>c();
            public static Func<Application, IEnumerable<Window>> <>9__0_0;
            public static Func<Window, bool> <>9__0_2;
            public static Func<IEnumerable<Window>, Window> <>9__0_1;

            internal IEnumerable<Window> <GetSerializationTarget>b__0_0(Application x) => 
                x.Windows.OfType<Window>();

            internal Window <GetSerializationTarget>b__0_1(IEnumerable<Window> windows)
            {
                Func<Window, bool> predicate = <>9__0_2;
                if (<>9__0_2 == null)
                {
                    Func<Window, bool> local1 = <>9__0_2;
                    predicate = <>9__0_2 = x => x.IsActive;
                }
                Window local2 = windows.FirstOrDefault<Window>(predicate);
                Window local4 = local2;
                if (local2 == null)
                {
                    Window local3 = local2;
                    local4 = windows.LastOrDefault<Window>();
                }
                return local4;
            }

            internal bool <GetSerializationTarget>b__0_2(Window x) => 
                x.IsActive;
        }
    }
}

