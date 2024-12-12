namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public static class SealHelper
    {
        public static void SealIfSealable(object obj)
        {
            Freezable freezable = obj as Freezable;
            if ((freezable != null) && freezable.CanFreeze)
            {
                freezable.Freeze();
            }
            else
            {
                FrameworkTemplate template = obj as FrameworkTemplate;
                if ((template != null) && !template.IsSealed)
                {
                    template.Seal();
                }
                else
                {
                    Style style = obj as Style;
                    if ((style != null) && !style.IsSealed)
                    {
                        style.Seal();
                    }
                }
            }
        }
    }
}

