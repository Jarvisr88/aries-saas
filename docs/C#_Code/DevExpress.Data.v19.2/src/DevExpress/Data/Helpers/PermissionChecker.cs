namespace DevExpress.Data.Helpers
{
    using System;
    using System.Security;

    public class PermissionChecker
    {
        private bool? isPermissionGranted;
        private IPermission permission;

        public PermissionChecker(IPermission permission);
        private bool IsPermissionGrantedCore(IPermission permission);

        public IPermission Permission { get; }

        public bool IsPermissionGranted { get; }
    }
}

