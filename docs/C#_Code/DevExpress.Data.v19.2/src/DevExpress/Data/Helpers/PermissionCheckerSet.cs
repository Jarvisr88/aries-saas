namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Security;

    internal class PermissionCheckerSet
    {
        private List<PermissionChecker> checkers;

        public PermissionCheckerSet();
        private PermissionChecker GetChecker(IPermission permission);
        public bool IsGranted(IPermission permission);
    }
}

