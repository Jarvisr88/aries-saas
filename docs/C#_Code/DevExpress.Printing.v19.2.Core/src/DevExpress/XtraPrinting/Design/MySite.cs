namespace DevExpress.XtraPrinting.Design
{
    using System;
    using System.ComponentModel;

    public class MySite : ISite, IServiceProvider
    {
        private IServiceProvider sp;
        private IComponent comp;

        public MySite(IServiceProvider sp, IComponent comp)
        {
            this.sp = sp;
            this.comp = comp;
        }

        object IServiceProvider.GetService(Type t) => 
            this.sp?.GetService(t);

        IComponent ISite.Component =>
            this.comp;

        IContainer ISite.Container =>
            this.sp.GetService(typeof(IContainer)) as IContainer;

        bool ISite.DesignMode =>
            false;

        string ISite.Name
        {
            get => 
                null;
            set
            {
            }
        }
    }
}

