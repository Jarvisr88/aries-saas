namespace DevExpress.Data.Helpers
{
    using System;
    using System.Security;

    internal class WeakReferencePermission : IPermission, ISecurityEncodable
    {
        public IPermission Copy();
        public void Demand();
        public void FromXml(SecurityElement e);
        public IPermission Intersect(IPermission target);
        public bool IsSubsetOf(IPermission target);
        public SecurityElement ToXml();
        public IPermission Union(IPermission target);
    }
}

