namespace DevExpress.Xpf.Editors.Helpers
{
    using System;

    public class LocalCurrentPlainDataView : LocalCurrentDataView
    {
        public LocalCurrentPlainDataView(bool selectNullValue, object listSource, object handle, string valueMember, string displayMember, bool lazyInitialization) : base(selectNullValue, listSource, handle, valueMember, displayMember, lazyInitialization)
        {
        }
    }
}

