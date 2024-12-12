namespace DevExpress.Xpf.Editors
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, DebuggerNonUserCode, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    internal class LocalizationRes
    {
        private static System.Resources.ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        internal LocalizationRes()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                resourceMan ??= new System.Resources.ResourceManager("DevExpress.Xpf.Editors.LocalizationRes", typeof(LocalizationRes).Assembly);
                return resourceMan;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get => 
                resourceCulture;
            set => 
                resourceCulture = value;
        }

        internal static string EditorStringId_Cancel =>
            ResourceManager.GetString("EditorStringId.Cancel", resourceCulture);

        internal static string EditorStringId_OK =>
            ResourceManager.GetString("EditorStringId.OK", resourceCulture);

        internal static string EditorStringId_SelectAll =>
            ResourceManager.GetString("EditorStringId.SelectAll", resourceCulture);
    }
}

