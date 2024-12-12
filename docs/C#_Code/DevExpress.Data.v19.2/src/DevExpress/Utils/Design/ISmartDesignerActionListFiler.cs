namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;

    public interface ISmartDesignerActionListFiler
    {
        bool PreFilterMember(MemberDescriptor member);
    }
}

