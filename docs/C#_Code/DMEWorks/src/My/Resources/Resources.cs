namespace My.Resources
{
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [StandardModule, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated, HideModuleName]
    internal sealed class Resources
    {
        private static System.Resources.ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                {
                    resourceMan = new System.Resources.ResourceManager("Resources", typeof(My.Resources.Resources).Assembly);
                }
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

        internal static Bitmap ImageAction =>
            (Bitmap) ResourceManager.GetObject("ImageAction", resourceCulture);

        internal static Bitmap ImageClone =>
            (Bitmap) ResourceManager.GetObject("ImageClone", resourceCulture);

        internal static Bitmap ImageClone2 =>
            (Bitmap) ResourceManager.GetObject("ImageClone2", resourceCulture);

        internal static Bitmap ImageClose =>
            (Bitmap) ResourceManager.GetObject("ImageClose", resourceCulture);

        internal static Bitmap ImageDelete =>
            (Bitmap) ResourceManager.GetObject("ImageDelete", resourceCulture);

        internal static Bitmap ImageDelete2 =>
            (Bitmap) ResourceManager.GetObject("ImageDelete2", resourceCulture);

        internal static Bitmap ImageEdit =>
            (Bitmap) ResourceManager.GetObject("ImageEdit", resourceCulture);

        internal static Bitmap ImageEmail =>
            (Bitmap) ResourceManager.GetObject("ImageEmail", resourceCulture);

        internal static Bitmap ImageEnlarge =>
            (Bitmap) ResourceManager.GetObject("ImageEnlarge", resourceCulture);

        internal static Bitmap ImageFilter =>
            (Bitmap) ResourceManager.GetObject("ImageFilter", resourceCulture);

        internal static Bitmap ImageGo =>
            (Bitmap) ResourceManager.GetObject("ImageGo", resourceCulture);

        internal static Bitmap ImageGo2 =>
            (Bitmap) ResourceManager.GetObject("ImageGo2", resourceCulture);

        internal static Bitmap ImageLens =>
            (Bitmap) ResourceManager.GetObject("ImageLens", resourceCulture);

        internal static Bitmap ImageMissing =>
            (Bitmap) ResourceManager.GetObject("ImageMissing", resourceCulture);

        internal static Bitmap ImageNew =>
            (Bitmap) ResourceManager.GetObject("ImageNew", resourceCulture);

        internal static Bitmap ImageNew2 =>
            (Bitmap) ResourceManager.GetObject("ImageNew2", resourceCulture);

        internal static Bitmap ImageOpen =>
            (Bitmap) ResourceManager.GetObject("ImageOpen", resourceCulture);

        internal static Bitmap ImagePdfThumbnail =>
            (Bitmap) ResourceManager.GetObject("ImagePdfThumbnail", resourceCulture);

        internal static Bitmap ImagePrint =>
            (Bitmap) ResourceManager.GetObject("ImagePrint", resourceCulture);

        internal static Bitmap ImageRefresh =>
            (Bitmap) ResourceManager.GetObject("ImageRefresh", resourceCulture);

        internal static Bitmap ImageRefresh2 =>
            (Bitmap) ResourceManager.GetObject("ImageRefresh2", resourceCulture);

        internal static Bitmap ImageRefresh3 =>
            (Bitmap) ResourceManager.GetObject("ImageRefresh3", resourceCulture);

        internal static Bitmap ImageSave =>
            (Bitmap) ResourceManager.GetObject("ImageSave", resourceCulture);

        internal static Bitmap ImageSave2 =>
            (Bitmap) ResourceManager.GetObject("ImageSave2", resourceCulture);

        internal static Bitmap ImageSchedule =>
            (Bitmap) ResourceManager.GetObject("ImageSchedule", resourceCulture);

        internal static Bitmap ImageSearch =>
            (Bitmap) ResourceManager.GetObject("ImageSearch", resourceCulture);

        internal static Bitmap ImageSpinner =>
            (Bitmap) ResourceManager.GetObject("ImageSpinner", resourceCulture);

        internal static Bitmap ImageSpyglass =>
            (Bitmap) ResourceManager.GetObject("ImageSpyglass", resourceCulture);

        internal static Bitmap ImageSpyglass2 =>
            (Bitmap) ResourceManager.GetObject("ImageSpyglass2", resourceCulture);

        internal static Bitmap spinner =>
            (Bitmap) ResourceManager.GetObject("spinner", resourceCulture);

        internal static Icon Warning =>
            (Icon) ResourceManager.GetObject("Warning", resourceCulture);
    }
}

