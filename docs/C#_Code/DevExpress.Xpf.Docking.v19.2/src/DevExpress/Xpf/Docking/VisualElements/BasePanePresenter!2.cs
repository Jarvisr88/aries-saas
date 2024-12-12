namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class BasePanePresenter<TVisual, TLogical> : psvContentPresenter where TVisual: DependencyObject, IDisposable where TLogical: BaseLayoutItem
    {
        private static readonly DataTemplate Empty;

        static BasePanePresenter()
        {
            BasePanePresenter<TVisual, TLogical>.Empty = new DataTemplate();
        }

        protected BasePanePresenter()
        {
            base.ContentTemplate = BasePanePresenter<TVisual, TLogical>.Empty;
        }

        protected abstract bool CanSelectTemplate(TLogical item);
        private void ChangeTemplate(TLogical item)
        {
            if (this.Owner != null)
            {
                DataTemplate template1 = this.SelectTemplate(item);
                DataTemplate empty = template1;
                if (template1 == null)
                {
                    DataTemplate local1 = template1;
                    empty = BasePanePresenter<TVisual, TLogical>.Empty;
                }
                this.ContentTemplate = empty;
            }
        }

        protected virtual TLogical ConvertToLogicalItem(object content) => 
            content as TLogical;

        protected internal void EnsureOwner(TVisual owner)
        {
            this.Owner = owner;
            this.ChangeTemplate(this.ConvertToLogicalItem(base.Content));
        }

        protected override void OnContentChanged(object content, object oldContent)
        {
            base.OnContentChanged(content, oldContent);
            TLogical item = this.ConvertToLogicalItem(content);
            if (this.Owner != null)
            {
                this.ChangeTemplate(item);
            }
        }

        protected override void OnDispose()
        {
            TLogical local = this.ConvertToLogicalItem(base.Content);
            if ((local != null) && (this.Owner is IUIElement))
            {
                local.UIElements.Remove((IUIElement) this.Owner);
            }
            base.OnDispose();
            TVisual local2 = default(TVisual);
            this.Owner = local2;
        }

        protected virtual void OnStylePropertyChanged()
        {
            this.ChangeTemplate(this.ConvertToLogicalItem(base.Content));
        }

        private DataTemplate SelectTemplate(TLogical item) => 
            ((item == null) || !this.CanSelectTemplate(item)) ? null : this.SelectTemplateCore(item);

        protected abstract DataTemplate SelectTemplateCore(TLogical item);

        public TVisual Owner { get; private set; }
    }
}

