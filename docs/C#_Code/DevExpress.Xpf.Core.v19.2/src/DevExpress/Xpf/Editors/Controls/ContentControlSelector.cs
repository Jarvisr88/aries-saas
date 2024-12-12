namespace DevExpress.Xpf.Editors.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ContentControlSelector : ContentControl
    {
        public ContentControlSelector()
        {
            base.Loaded += (<sender>, <e>) => this.UpdateContentTemplate();
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            this.UpdateContentTemplate();
        }

        protected virtual void UpdateContentTemplate()
        {
            if (base.Content != null)
            {
                string name = base.Content.GetType().Name;
                if (!string.IsNullOrEmpty(name))
                {
                    base.ContentTemplate = base.Resources[name] as DataTemplate;
                }
            }
        }
    }
}

