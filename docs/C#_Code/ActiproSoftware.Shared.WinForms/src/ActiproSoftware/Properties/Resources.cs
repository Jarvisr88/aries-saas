namespace ActiproSoftware.Properties
{
    using #H;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    public class Resources
    {
        private static System.Resources.ResourceManager #0i;
        private static CultureInfo #1i;

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                #0i ??= new System.Resources.ResourceManager(#G.#eg(0xea1), Type.GetTypeFromHandle(typeof(Resources).TypeHandle).Assembly);
                return #0i;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static CultureInfo Culture
        {
            get => 
                #1i;
            set => 
                #1i = value;
        }

        public static Bitmap Licensed16 =>
            (Bitmap) ResourceManager.GetObject(#G.#eg(0xed2), #1i);

        public static Bitmap ProductBars16 =>
            (Bitmap) ResourceManager.GetObject(#G.#eg(0xee3), #1i);

        public static Bitmap ProductDocking16 =>
            (Bitmap) ResourceManager.GetObject(#G.#eg(0xef8), #1i);

        public static Bitmap ProductNavigation16 =>
            (Bitmap) ResourceManager.GetObject(#G.#eg(0xf11), #1i);

        public static Bitmap ProductShared16 =>
            (Bitmap) ResourceManager.GetObject(#G.#eg(0xf2e), #1i);

        public static Bitmap ProductSyntaxEditor16 =>
            (Bitmap) ResourceManager.GetObject(#G.#eg(0xf43), #1i);

        public static Bitmap ProductSyntaxEditorAddon16 =>
            (Bitmap) ResourceManager.GetObject(#G.#eg(0xf60), #1i);

        public static Bitmap ProductWizard16 =>
            (Bitmap) ResourceManager.GetObject(#G.#eg(0xf85), #1i);

        public static Bitmap Unlicensed16 =>
            (Bitmap) ResourceManager.GetObject(#G.#eg(0xf9a), #1i);
    }
}

