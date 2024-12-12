namespace DevExpress.Xpf.Controls
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    public class LocalizationRes
    {
        private static System.Resources.ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal LocalizationRes()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                resourceMan ??= new System.Resources.ResourceManager("DevExpress.Xpf.Controls.LocalizationRes", typeof(LocalizationRes).Assembly);
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static CultureInfo Culture
        {
            get => 
                resourceCulture;
            set => 
                resourceCulture = value;
        }

        public static string AgBookStringId_MenuCmd_MakeColumnVisible =>
            ResourceManager.GetString("AgBookStringId.MenuCmd_MakeColumnVisible", resourceCulture);
    }
}

