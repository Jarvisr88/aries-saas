namespace DevExpress.Xpf.Bars.Native
{
    using DevExpress.Xpf.Bars;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class ElementBinder : IBarNameScopeDecorator, IElementBinder
    {
        private readonly object elementRegistratorKey;
        private readonly object linkRegistratorKey;
        private readonly ScopeSearchSettings elementSearchSettings;
        private readonly ScopeSearchSettings linkSearchSettings;
        private BarNameScope scope;

        protected ElementBinder(object elementRegistratorKey, object linkRegistratorKey, ScopeSearchSettings elementSearchSettings = 3, ScopeSearchSettings linkSearchSettings = 6);
        protected abstract bool CanLink(IBarNameScopeSupport first, IBarNameScopeSupport second);
        protected abstract bool CanUnlink(IBarNameScopeSupport first, IBarNameScopeSupport second);
        void IBarNameScopeDecorator.Attach(BarNameScope scope);
        void IBarNameScopeDecorator.Detach();
        bool IElementBinder.CanLink(IBarNameScopeSupport first, IBarNameScopeSupport second);
        bool IElementBinder.CanUnlink(IBarNameScopeSupport first, IBarNameScopeSupport second);
        void IElementBinder.Link(IBarNameScopeSupport first, IBarNameScopeSupport second);
        bool IElementBinder.OnRegistratorChanged(IElementBinder binder, DevExpress.Xpf.Bars.ElementRegistrator sender, ElementRegistratorChangedArgs e, bool isElementRegistrator);
        bool IElementBinder.PropagateRegistratorChanged(DevExpress.Xpf.Bars.ElementRegistrator sender, ElementRegistratorChangedArgs e, bool isElementRegistrator);
        void IElementBinder.Unlink(IBarNameScopeSupport first, IBarNameScopeSupport second);
        private bool IsOwnRegistrator(DevExpress.Xpf.Bars.ElementRegistrator registrator);
        protected abstract void Link(IBarNameScopeSupport element, IBarNameScopeSupport link);
        protected void OnRegistratorChanged(DevExpress.Xpf.Bars.ElementRegistrator sender, ElementRegistratorChangedArgs e, bool isElementRegistrator);
        private bool OnRegistratorChanged(object binder, DevExpress.Xpf.Bars.ElementRegistrator sender, ElementRegistratorChangedArgs e, bool isElementRegistrator);
        private bool PropagateRegistratorChanged(DevExpress.Xpf.Bars.ElementRegistrator sender, ElementRegistratorChangedArgs e, bool isElementRegistrator);
        protected abstract void Unlink(IBarNameScopeSupport element, IBarNameScopeSupport link);

        protected DevExpress.Xpf.Bars.ElementRegistrator ElementRegistrator { get; private set; }

        protected DevExpress.Xpf.Bars.ElementRegistrator LinkRegistrator { get; private set; }

        protected BarNameScope Scope { get; }

        private List<IBarNameScopeSupport> BoundElements { get; set; }

        private List<IBarNameScopeSupport> BoundLinks { get; set; }

        DevExpress.Xpf.Bars.ElementRegistrator IElementBinder.ElementRegistrator { get; }

        DevExpress.Xpf.Bars.ElementRegistrator IElementBinder.LinkRegistrator { get; }

        IList<IBarNameScopeSupport> IElementBinder.BoundElements { get; }

        IList<IBarNameScopeSupport> IElementBinder.BoundLinks { get; }

        BarNameScope IElementBinder.Scope { get; }

        ScopeSearchSettings IElementBinder.ElementSearchSettings { get; }

        ScopeSearchSettings IElementBinder.LinkSearchSettings { get; }
    }
}

