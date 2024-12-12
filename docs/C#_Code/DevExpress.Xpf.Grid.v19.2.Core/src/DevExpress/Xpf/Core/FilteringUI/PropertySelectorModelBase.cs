namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public abstract class PropertySelectorModelBase : ImmutableObject
    {
        internal PropertySelectorModelBase(object content, DataTemplateSelector captionTemplateSelector, DataTemplateSelector selectedCaptionTemplateSelector, int id, int parentID)
        {
            this.<ID>k__BackingField = id;
            this.<ParentID>k__BackingField = parentID;
            this.<Content>k__BackingField = content;
            this.<CaptionTemplateSelector>k__BackingField = captionTemplateSelector;
            this.<SelectedCaptionTemplateSelector>k__BackingField = selectedCaptionTemplateSelector;
        }

        public object Content { get; }

        public string ContentString
        {
            get
            {
                object content = this.Content;
                if (content != null)
                {
                    return content.ToString();
                }
                object local1 = content;
                return null;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DataTemplateSelector Selector =>
            this.CaptionTemplateSelector;

        public DataTemplateSelector CaptionTemplateSelector { get; }

        public DataTemplateSelector SelectedCaptionTemplateSelector { get; }

        public int ID { get; }

        public int ParentID { get; }
    }
}

