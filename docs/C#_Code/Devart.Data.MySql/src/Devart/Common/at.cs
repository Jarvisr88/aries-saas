namespace Devart.Common
{
    using System;
    using System.ComponentModel;

    internal class at : License
    {
        private string a;
        private string b;
        private string c;
        private bool d;
        internal int e;
        internal string f;

        public at(string A_0, string A_1, bool A_2, int A_3, string A_4)
        {
            this.a = (A_0 != "") ? A_0 : null;
            this.b = A_1;
            this.d = A_2;
            this.e = A_3;
            this.f = A_4;
            this.c = ab.f().a(A_3, A_4);
        }

        public string a() => 
            this.c;

        public bool b() => 
            this.d;

        public string d() => 
            this.b;

        public override void Dispose()
        {
        }

        public override string System.ComponentModel.License.LicenseKey =>
            this.a;
    }
}

