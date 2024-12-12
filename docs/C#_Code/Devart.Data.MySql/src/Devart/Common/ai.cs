namespace Devart.Common
{
    using System;
    using System.Security;

    internal class ai
    {
        private string a;
        private SecureString b;
        private int? c;

        public ai(string A_0, SecureString A_1)
        {
            if (string.IsNullOrEmpty(A_0) || (A_1 == null))
            {
                throw new ArgumentNullException("Cannot set null or empty Credential.");
            }
            this.a = A_0;
            this.b = A_1;
        }

        public SecureString a() => 
            this.b;

        public override bool a(object A_0)
        {
            ai ai = A_0 as ai;
            return ((ai != null) ? ((this.d() == ai.d()) ? (!ReferenceEquals(this.a(), ai.a()) ? ((this.b() == ai.b()) && aw.a(this.a(), ai.a())) : true) : false) : false);
        }

        internal int b()
        {
            if (this.c == null)
            {
                this.c = new int?(aw.a(this.b));
            }
            return this.c.Value;
        }

        public override int c() => 
            this.d().GetHashCode() | this.b();

        public string d() => 
            this.a;
    }
}

