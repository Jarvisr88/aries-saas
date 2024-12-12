namespace DMEWorks.Core
{
    using DMEWorks;
    using System;
    using System.Windows.Forms;

    public abstract class FormFactory
    {
        protected FormFactory()
        {
        }

        public abstract Form CreateForm();
        public abstract PermissionsStruct GetPermissions();
    }
}

