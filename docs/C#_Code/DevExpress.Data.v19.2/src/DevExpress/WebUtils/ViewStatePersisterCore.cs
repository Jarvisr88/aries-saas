namespace DevExpress.WebUtils
{
    using System;

    public class ViewStatePersisterCore
    {
        private IViewBagOwner viewBagOwner;
        private string objectPath;

        public ViewStatePersisterCore() : this(null)
        {
        }

        public ViewStatePersisterCore(IViewBagOwner viewBagOwner) : this(viewBagOwner, string.Empty)
        {
        }

        public ViewStatePersisterCore(IViewBagOwner viewBagOwner, string objectPath)
        {
            this.viewBagOwner = viewBagOwner;
            this.objectPath = objectPath;
        }

        protected virtual T GetViewBagProperty<T>(string name, T value) => 
            (this.viewBagOwner != null) ? this.viewBagOwner.GetViewBagProperty<T>(this.ViewBagObjectPath, name, value) : value;

        protected virtual void SetViewBagProperty<T>(string name, T defaultValue, T value)
        {
            if (this.viewBagOwner != null)
            {
                this.viewBagOwner.SetViewBagProperty<T>(this.ViewBagObjectPath, name, defaultValue, value);
            }
        }

        protected IViewBagOwner ViewBagOwner =>
            this.viewBagOwner;

        protected virtual string ViewBagObjectPath =>
            (this.objectPath != null) ? this.objectPath : base.GetType().Name;
    }
}

