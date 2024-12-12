namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;

    internal static class BrickResolver
    {
        static BrickResolver()
        {
            BrickFactory.BrickResolve += new BrickResolveEventHandler(BrickResolver.BrickFactory_BrickResolve);
        }

        private static void BrickFactory_BrickResolve(object sender, BrickResolveEventArgs args)
        {
            if (args.Brick == null)
            {
                string name = args.Name;
                if (name == "RichText")
                {
                    args.Brick = new RichTextBrick();
                }
                else if (name == "FormattedRichText")
                {
                    args.Brick = new FormattedRichTextBrick();
                }
            }
        }

        public static void EnsureStaticConstructor()
        {
        }
    }
}

