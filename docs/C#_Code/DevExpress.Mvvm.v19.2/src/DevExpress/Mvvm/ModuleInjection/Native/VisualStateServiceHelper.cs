namespace DevExpress.Mvvm.ModuleInjection.Native
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.ModuleInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class VisualStateServiceHelper
    {
        public static void CheckServices(object viewModel, bool throwIfNotSupportServices, bool checkServiceIds)
        {
            IEnumerable<IVisualStateServiceImplementation> enumerable;
            GetServices(viewModel, throwIfNotSupportServices, checkServiceIds, out enumerable);
        }

        public static IEnumerable<IVisualStateServiceImplementation> GetServices(object viewModel, bool throwIfNotSupportServices, bool checkServiceIds)
        {
            IEnumerable<IVisualStateServiceImplementation> enumerable;
            GetServices(viewModel, throwIfNotSupportServices, checkServiceIds, out enumerable);
            return enumerable;
        }

        private static void GetServices(object viewModel, bool throwIfNotSupportServices, bool checkServiceIds, out IEnumerable<IVisualStateServiceImplementation> services)
        {
            if (throwIfNotSupportServices)
            {
                Verifier.VerifyViewModelISupportServices(viewModel);
            }
            if (!(viewModel is ISupportServices))
            {
                services = new IVisualStateServiceImplementation[0];
            }
            else
            {
                IServiceContainer serviceContainer = ((ISupportServices) viewModel).ServiceContainer;
                services = serviceContainer.GetServices<IVisualStateService>(true).Cast<IVisualStateServiceImplementation>();
                if (checkServiceIds)
                {
                    Func<IVisualStateServiceImplementation, string> keySelector = <>c.<>9__2_0;
                    if (<>c.<>9__2_0 == null)
                    {
                        Func<IVisualStateServiceImplementation, string> local1 = <>c.<>9__2_0;
                        keySelector = <>c.<>9__2_0 = x => x.Id;
                    }
                    Func<IGrouping<string, IVisualStateServiceImplementation>, bool> predicate = <>c.<>9__2_1;
                    if (<>c.<>9__2_1 == null)
                    {
                        Func<IGrouping<string, IVisualStateServiceImplementation>, bool> local2 = <>c.<>9__2_1;
                        predicate = <>c.<>9__2_1 = x => x.Count<IVisualStateServiceImplementation>() > 1;
                    }
                    if (services.GroupBy<IVisualStateServiceImplementation, string>(keySelector).Where<IGrouping<string, IVisualStateServiceImplementation>>(predicate).Any<IGrouping<string, IVisualStateServiceImplementation>>())
                    {
                        ModuleInjectionException.VisualStateServiceName();
                    }
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly VisualStateServiceHelper.<>c <>9 = new VisualStateServiceHelper.<>c();
            public static Func<IVisualStateServiceImplementation, string> <>9__2_0;
            public static Func<IGrouping<string, IVisualStateServiceImplementation>, bool> <>9__2_1;

            internal string <GetServices>b__2_0(IVisualStateServiceImplementation x) => 
                x.Id;

            internal bool <GetServices>b__2_1(IGrouping<string, IVisualStateServiceImplementation> x) => 
                x.Count<IVisualStateServiceImplementation>() > 1;
        }
    }
}

