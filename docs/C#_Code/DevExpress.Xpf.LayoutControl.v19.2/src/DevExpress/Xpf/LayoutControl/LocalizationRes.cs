namespace DevExpress.Xpf.LayoutControl
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
                resourceMan ??= new System.Resources.ResourceManager("DevExpress.Xpf.LayoutControl.LocalizationRes", typeof(LocalizationRes).Assembly);
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

        public static string LayoutControl_Customization_AvailableItems =>
            ResourceManager.GetString("LayoutControl_Customization_AvailableItems", resourceCulture);

        public static string LayoutControl_Customization_NewGroupBox =>
            ResourceManager.GetString("LayoutControl_Customization_NewGroupBox", resourceCulture);

        public static string LayoutControl_Customization_NewItems =>
            ResourceManager.GetString("LayoutControl_Customization_NewItems", resourceCulture);

        public static string LayoutControl_Customization_NewTabbedGroup =>
            ResourceManager.GetString("LayoutControl_Customization_NewTabbedGroup", resourceCulture);

        public static string LayoutControl_Customization_Rename =>
            ResourceManager.GetString("LayoutControl_Customization_Rename", resourceCulture);

        public static string LayoutGroup_TabHeader_Default =>
            ResourceManager.GetString("LayoutGroup_TabHeader_Default", resourceCulture);

        public static string LayoutGroup_TabHeader_Empty =>
            ResourceManager.GetString("LayoutGroup_TabHeader_Empty", resourceCulture);

        public static string TileLayoutControl_GroupHeader_Empty =>
            ResourceManager.GetString("TileLayoutControl_GroupHeader_Empty", resourceCulture);
    }
}

