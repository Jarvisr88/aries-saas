namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Docking.Platform;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    internal static class XamlLoaderHelper
    {
        public static object LoadContentFromUri(Uri uri)
        {
            object content = null;
            object obj3 = Application.LoadComponent(uri);
            ContentControl control = obj3 as ContentControl;
            if (control != null)
            {
                if (!WindowHelper.IsXBAP)
                {
                    TryPreventWindowShowing(control);
                }
                if (control is UserControl)
                {
                    content = control;
                }
                else
                {
                    content = control.Content;
                    control.Content = null;
                }
            }
            Page page = obj3 as Page;
            if (page != null)
            {
                content = page.Content;
                page.Content = null;
            }
            if ((content != obj3) && ((content is DependencyObject) && (obj3 is DependencyObject)))
            {
                NameScope.SetNameScope((DependencyObject) content, NameScope.GetNameScope((DependencyObject) obj3));
            }
            return content;
        }

        private static void TryPreventWindowShowing(ContentControl control)
        {
            Window window = control as Window;
            if (window != null)
            {
                window.Close();
            }
        }
    }
}

