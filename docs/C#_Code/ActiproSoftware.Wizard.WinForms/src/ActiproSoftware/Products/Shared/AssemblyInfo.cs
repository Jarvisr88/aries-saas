namespace ActiproSoftware.Products.Shared
{
    using #H;
    using ActiproSoftware.Products;
    using System;
    using System.Windows.Forms;

    public sealed class AssemblyInfo : ActiproSoftware.Products.AssemblyInfo
    {
        private static ActiproSoftware.Products.Shared.AssemblyInfo #gd;

        private AssemblyInfo()
        {
        }

        public Cursor GetCursor(CursorResource cursor) => 
            this.GetCursor(cursor.ToString() + #G.#eg(0x2496));

        public static ActiproSoftware.Products.Shared.AssemblyInfo Instance
        {
            get
            {
                #gd ??= new ActiproSoftware.Products.Shared.AssemblyInfo();
                return #gd;
            }
        }

        public override AssemblyLicenseType LicenseType =>
            AssemblyLicenseType.Full;

        public override AssemblyPlatform Platform =>
            AssemblyPlatform.WindowsForms;

        public sealed override string ProductCode =>
            #G.#eg(0x249f);

        public sealed override int ProductId =>
            0;
    }
}

