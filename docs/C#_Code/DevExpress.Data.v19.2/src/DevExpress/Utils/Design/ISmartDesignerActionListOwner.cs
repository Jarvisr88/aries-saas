namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;

    public interface ISmartDesignerActionListOwner
    {
        bool AllowSmartTag(IComponent component);
    }
}

