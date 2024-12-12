namespace Devart.Common
{
    using System;

    public class SelectStatementNode
    {
        internal int a = -1;
        internal int b = -1;
        private bool c;

        internal void a(int A_0)
        {
            if (this.a != -1)
            {
                this.a += A_0;
            }
            if (this.b != -1)
            {
                this.b += A_0;
            }
        }

        internal void e()
        {
            this.c = false;
        }

        internal void g()
        {
            this.c = true;
        }

        internal virtual string ToString(string asKeyword) => 
            this.ToString();

        internal int Length =>
            !this.Current ? this.ToString().Length : ((this.b - this.a) + 1);

        internal bool Current =>
            !this.c && this.Binded;

        internal bool Binded =>
            this.a != -1;

        internal bool Marker =>
            (this.a != -1) && (this.b == -1);
    }
}

