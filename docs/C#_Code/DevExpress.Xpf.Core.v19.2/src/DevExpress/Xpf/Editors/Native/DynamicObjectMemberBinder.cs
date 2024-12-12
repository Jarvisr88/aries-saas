namespace DevExpress.Xpf.Editors.Native
{
    using System;
    using System.Dynamic;

    public class DynamicObjectMemberBinder : GetMemberBinder
    {
        public DynamicObjectMemberBinder(string name) : base(name, true)
        {
        }

        public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion) => 
            null;
    }
}

