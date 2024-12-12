namespace DevExpress.Xpf.Bars
{
    using System;

    public interface IRegistratorChangedListener
    {
        bool RegistratorChanged(object binder, ElementRegistrator registrator, ElementRegistratorChangedArgs args, bool isElementRegistrator);
    }
}

