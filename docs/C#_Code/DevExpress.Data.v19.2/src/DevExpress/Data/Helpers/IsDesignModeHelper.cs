namespace DevExpress.Data.Helpers
{
    using System;
    using System.ComponentModel;

    public static class IsDesignModeHelper
    {
        private static bool _BypassDesignModeAlterationDetection;

        public static bool GetCurrentBypassDesignModeAlterationDetection();
        private static bool GetCurrentDesignMode(Component component);
        public static bool GetIsDesignMode(Component component, ref bool? isDesignTime);
        public static bool GetIsDesignModeBypassable(Component component, ref bool? isDesignTime);
        public static void Validate(Component component, ref bool? isDesignTime);

        [Obsolete("It is not recommended to use this option in your code. Refer to the www.devexpress.com/issue=T121952 KB Article for more details.")]
        public static bool BypassDesignModeAlterationDetection { get; set; }
    }
}

