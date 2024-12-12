namespace DevExpress.Mvvm.ModuleInjection
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public static class VisualStateServiceExtensions
    {
        public static bool IsDefaultState(this IVisualStateService service)
        {
            string str;
            string str2;
            Verifier.VerifyVisualSerializationService(service);
            return IsDefaultState(service, out str, out str2);
        }

        private static bool IsDefaultState(IVisualStateService service, out string layout, out string defaultLayout)
        {
            defaultLayout = service.DefaultState;
            layout = service.GetCurrentState();
            return (!string.IsNullOrEmpty(defaultLayout) ? (!string.IsNullOrEmpty(layout) ? (layout == defaultLayout) : true) : true);
        }

        public static bool IsStateChanged(this IVisualStateService service)
        {
            string str;
            string str2;
            Verifier.VerifyVisualSerializationService(service);
            return IsStateChanged(service, out str, out str2);
        }

        private static bool IsStateChanged(IVisualStateService service, out string state, out string savedState)
        {
            state = service.GetCurrentState();
            savedState = service.GetSavedState();
            if (string.IsNullOrEmpty(state))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(savedState))
            {
                return (state != savedState);
            }
            string defaultState = service.DefaultState;
            return (string.IsNullOrEmpty(defaultState) || (state != defaultState));
        }

        public static void RestoreDefaultState(this IVisualStateService service)
        {
            string str;
            string str2;
            Verifier.VerifyVisualSerializationService(service);
            if (!IsDefaultState(service, out str, out str2))
            {
                service.RestoreState(str2);
            }
        }

        public static void RestoreState(this IVisualStateService service)
        {
            string str;
            string str2;
            Verifier.VerifyVisualSerializationService(service);
            if (IsStateChanged(service, out str, out str2))
            {
                service.RestoreState(str2);
            }
        }

        public static void SaveState(this IVisualStateService service)
        {
            string str;
            string str2;
            Verifier.VerifyVisualSerializationService(service);
            if (IsStateChanged(service, out str, out str2))
            {
                service.SaveState(str);
            }
        }
    }
}

