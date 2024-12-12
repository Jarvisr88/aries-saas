namespace DevExpress.Xpf.Office.UI
{
    using DevExpress.Data;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Office.Internal;
    using System;
    using System.Collections.Generic;
    using System.Drawing.Printing;
    using System.Resources;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public abstract class PaperKindBarListItemBase : BarListItem
    {
        [ThreadStatic]
        private static OfficeDefaultBarItemDataTemplates defaultBarItemTemplates;
        public static readonly DependencyProperty PaperKindListProperty = DependencyPropertyManager.Register("PaperKindList", typeof(IList<PaperKind>), typeof(PaperKindBarListItemBase), new FrameworkPropertyMetadata(new PropertyChangedCallback(PaperKindBarListItemBase.OnPaperKindListChanged)));

        protected PaperKindBarListItemBase()
        {
        }

        protected internal virtual void AppendItem(PaperKind paperKind, string displayName)
        {
            BarButtonItem item = new BarButtonItem {
                Content = this.ObtainPaperKindCaption(paperKind, displayName),
                Command = this.CreateChangeSectionPaperKindCommand(paperKind),
                CommandParameter = this.Control,
                ContentTemplate = this.DefaultBarItemTemplates.PaperKindBarItemContentTemplate
            };
            base.InternalItems.Add(item);
        }

        protected internal abstract ICommand CreateChangeSectionPaperKindCommand(PaperKind paperKind);
        protected internal abstract string ObtainPaperKindCaption(PaperKind paperKind, string displayName);
        private IList<PaperKind> ObtainPaperKindList() => 
            ((this.PaperKindList == null) || (this.PaperKindList.Count <= 0)) ? this.DefaultPaperKindList : this.PaperKindList;

        protected internal virtual void OnPaperKindListChanged(IList<PaperKind> oldValue, IList<PaperKind> newValue)
        {
            this.UpdateItems();
        }

        protected static void OnPaperKindListChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PaperKindBarListItemBase base2 = d as PaperKindBarListItemBase;
            if (base2 != null)
            {
                base2.OnPaperKindListChanged((IList<PaperKind>) e.OldValue, (IList<PaperKind>) e.NewValue);
            }
        }

        protected override void UpdateItems()
        {
            base.InternalItems.BeginUpdate();
            try
            {
                base.InternalItems.Clear();
                if (this.Control != null)
                {
                    this.UpdateItemsCore();
                }
            }
            finally
            {
                base.InternalItems.EndUpdate();
            }
        }

        protected internal virtual void UpdateItemsCore()
        {
            ResourceManager resourceManager = OfficeLocalizationHelper.CreateResourceManager(typeof(ResFinder));
            IList<PaperKind> list = this.ObtainPaperKindList();
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                PaperKind kind = list[i];
                string paperKindString = OfficeLocalizationHelper.GetPaperKindString(resourceManager, kind);
                this.AppendItem(kind, paperKindString);
            }
        }

        private OfficeDefaultBarItemDataTemplates DefaultBarItemTemplates
        {
            get
            {
                if (defaultBarItemTemplates == null)
                {
                    defaultBarItemTemplates = new OfficeDefaultBarItemDataTemplates();
                    defaultBarItemTemplates.ApplyTemplate();
                }
                return defaultBarItemTemplates;
            }
        }

        public IList<PaperKind> PaperKindList
        {
            get => 
                (IList<PaperKind>) base.GetValue(PaperKindListProperty);
            set => 
                base.SetValue(PaperKindListProperty, value);
        }

        protected abstract System.Windows.Controls.Control Control { get; }

        protected abstract IList<PaperKind> DefaultPaperKindList { get; }
    }
}

